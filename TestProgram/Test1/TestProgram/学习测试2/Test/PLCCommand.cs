using System;
using System.Collections.Generic;
using Common;
using OPC.Data;
using OPC.Data.Interface;

namespace LowVoltageTechnicianOperation.Common
{
    public class PLCCommand
    {
        #region field
        private static PLCCommand _instance = null;
        private static readonly object SyncLock = new object();

        private readonly string StrSrvName = System.Configuration.ConfigurationManager.AppSettings["PLCSrvName"];
        private readonly string StrItemId = System.Configuration.ConfigurationManager.AppSettings["PLCItemId"];
        private readonly string[] _arrGrps = new string[] { "MotorPNRun","THAMotor","SHMeter" };
        private readonly string[] _arrItems = new string[] { "Set,DetectionStart,DetectionEnd,Result", "Set,DetectionStart,DetectionEnd,Result,MotorRun", "Set,DetectionStart,DetectionEnd,Result,Lamps" };
        private OpcServer _opcSrv = new OpcServer();
        private Dictionary<string, OpcGroup> _opcGrpsDic = new Dictionary<string, OpcGroup>();
        private Dictionary<string, Dictionary<string, OPCItemResult>> _opcResultsDic = new Dictionary<string, Dictionary<string, OPCItemResult>>();
        #endregion

        #region property

        public Dictionary<string, OpcGroup> OpcGrpsDic
        {
            get
            {
                return _opcGrpsDic;
            }
        }

        public enum GroupName
        {
            /// <summary>电动机单向连续运转接线</summary>
            MotorPNRun,

            /// <summary>
            /// 三相异步电动机正反的接线及安全操作
            /// </summary>
            THAMotor,

            /// <summary>
            /// 单相电能表带照明灯的安装及接线
            /// </summary>
            SHMeter
        }

        public enum ItemName
        {
            /// <summary>设置实际操作项目</summary>
            Set,

            /// <summary>开始检测</summary>
            DetectionStart,

            /// <summary>检测结束</summary>
            DetectionEnd,

            /// <summary>检测操作结果</summary>
            Result,

            /// <summary> 日光灯开关信号 </summary>
            Lamps,
            /// <summary>
            /// 电动机正反转使能信号
            /// </summary>
            MotorRun,
        }
        #endregion

        #region constructor
        private PLCCommand()
        {
            try
            {
                _opcSrv.Connect(StrSrvName);
                for (int i = 0; i < _arrGrps.Length; i++)
                {
                    string strGrp = _arrGrps[i];
                    OpcGroup grp = _opcSrv.AddGroup(strGrp, true, 9600);
                    grp.UpdateRate = 100;
                    grp.Active = true;
                    grp.PercentDeadband = 0;

                    _opcGrpsDic.Add(strGrp, grp);

                    string[] arrCurrentItems = _arrItems[i].Split(',');

                    OPCItemDef[] opcItems = new OPCItemDef[arrCurrentItems.Length];
                    OPCItemResult[] opcResults = new OPCItemResult[arrCurrentItems.Length];
                    for (int j = 0; j < arrCurrentItems.Length; j++)
                    {
                        string strItemName = $"{StrItemId}.{strGrp}.{arrCurrentItems[j]}";
                        OPCItemDef item = new OPCItemDef(strItemName, true, opcResults.Length, System.Runtime.InteropServices.VarEnum.VT_BOOL);
                        opcItems[j] = item;
                    }
                    Dictionary<string, OPCItemResult> oDic = new Dictionary<string, OPCItemResult>();
                    grp.AddItems(opcItems, out opcResults);

                    for (int k = 0; k < opcResults.Length; k++)
                    {
                        oDic.Add(arrCurrentItems[k], opcResults[k]);
                    }
                    _opcResultsDic.Add(strGrp, oDic);
                }
            }
            catch (Exception ee)
            {
                LogImpl.Debug("错误"+ee.ToString());
            }
           
        }
        #endregion

        #region private method
        #endregion

        #region public method
        public static PLCCommand GetInstance()
        {
            if (_instance == null)
            {
                lock (SyncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PLCCommand();
                    }
                }
            }
            return _instance;
        }

        public static void Dispose()
        {
            if (_instance != null)
            {
                try
                {
                    foreach (var item in _instance._opcGrpsDic)
                    {
                        item.Value.Remove(false);
                    }
                    _instance._opcSrv.Disconnect();
                    _instance = null;
                }
                catch (Exception ex)
                {
                    LogImpl.Error(string.Format("{0}{2}{1}", ex.StackTrace, ex.Message, Environment.NewLine));
                }
            }
        }

        public bool SyncRead(GroupName enumGrp, ItemName[] enumArryItem, out bool[] arrResult)
        {
            bool bResult;
            string strGrp = enumGrp.ToString();
            var grp = _opcResultsDic[strGrp];
            arrResult = new bool[enumArryItem.Length];
            int[] arrHandle = new int[enumArryItem.Length];
            for (int i = 0; i < enumArryItem.Length; i++)
            {
                string strItem = enumArryItem[i].ToString();
                arrHandle[i] = grp[strItem].HandleServer;
            }

            OPCItemState[] arrState = new OPCItemState[enumArryItem.Length];

            try
            {
                bResult = _opcGrpsDic[strGrp].SyncRead(OPCDATASOURCE.OPC_DS_DEVICE, arrHandle, out arrState);
            }
            catch (Exception)
            {
                bResult = false;
            }

            for (int i = 0; i < arrState.Length; i++)
            {
                arrResult[i] = (bool)arrState[i].DataValue;
            }

            return bResult;
        }

        public bool SyncRead(GroupName enumGrp, ItemName enumItem, out bool iResult)
        {
            bool bExec;
            string strGrp = enumGrp.ToString();
            LogImpl.Debug(strGrp);

            int[] arrHandle = { _opcResultsDic[strGrp][enumItem.ToString()].HandleServer };
            OPCItemState[] arrState = new OPCItemState[1];

            try
            {
                bExec = _opcGrpsDic[strGrp].SyncRead(OPCDATASOURCE.OPC_DS_DEVICE, arrHandle, out arrState);
            }
            catch (Exception)
            {
                bExec = false;
            }

            iResult = (bool)arrState[0].DataValue;
            return bExec;
        }

        public bool SyncWrite(GroupName enumGrp, ItemName[] enumArryItem, int[] arrWriteVal)
        {
            bool bResult;
            string strGrp = enumGrp.ToString();
            var grp = _opcResultsDic[strGrp];
            int[] arrHandle = new int[enumArryItem.Length];
            object[] arrObj = new object[enumArryItem.Length];
            int[] arrErr;
            for (int i = 0; i < enumArryItem.Length; i++)
            {
                string strItem = enumArryItem[i].ToString();
                arrHandle[i] = grp[strItem].HandleServer;
                arrObj[i] = arrWriteVal[i];
            }

            try
            {
                bResult = _opcGrpsDic[strGrp].SyncWrite(arrHandle, arrObj, out arrErr);
                for (int i = 0; i < arrErr.Length; i++)
                {
                    //todo:未对写入返回的错误信息进行处理
                }
            }
            catch (Exception)
            {
                bResult = false;
            }
            return bResult;
        }

        public bool SyncWrite(GroupName enumGrp, ItemName enumItem, int iWriteVal)
        {
            bool bExec;
            string strGrp = enumGrp.ToString();
            int[] arrErr = new int[1];
            try
            {
                int[] arrHandle = { _opcResultsDic[strGrp][enumItem.ToString()].HandleServer };
                object[] arrObj = new object[] { iWriteVal };
                bExec = _opcGrpsDic[strGrp].SyncWrite(arrHandle, arrObj, out arrErr);
            }
            catch (Exception ex)
            {
                bExec = false;
            }
            if (arrErr[0] == 1)
            {
                bExec = false;
            }
            return bExec;
        }
        #endregion
    }
}
