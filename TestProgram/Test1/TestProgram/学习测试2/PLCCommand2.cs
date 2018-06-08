using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using LowVoltageTechnicianOperation.Common;
using OPC.Common;
using OPC.Data;
using OPC.Data.Interface;

namespace JAAJ.PoleMountedTransformer.Common
{
    public class PLCCommand2
    {
        #region field
        private static PLCCommand2 instance = null;
        private static object syncLock = new object();

        private const string strSrvName = "S7200.OPCServer";
        private const string strItemID = "Microwin.PLC";
        private readonly string[] arrGrps = new string[] { "Main" };
        private readonly string[] arrItems = new string[] { "HF_A_OK,HF_B_OK,HF_C_OK,HP_A_OK,LD_A_OK,LD_B_OK,LD_C_OK,LS_OK,LT_A_OK,HP_TD,LP_TD,ResetPosition" };
        private OpcServer opcSrv = new OpcServer();
        private Dictionary<string, OpcGroup> opcGrpsDic = new Dictionary<string, OpcGroup>();
        private Dictionary<string, Dictionary<string, OPCItemResult>> opcResultsDic = new Dictionary<string, Dictionary<string, OPCItemResult>>();
        #endregion

        #region property

        public enum GroupName
        {
            /// <summary>MainGroup</summary>
            Main
        }

        public enum ItemName
        {
            /// <summary>高压熔断器A相断开确认</summary>
            HF_A_OK,

            /// <summary>高压熔断器B相断开确认</summary>
            HF_B_OK,

            /// <summary>高压熔断器C相断开确认</summary>
            HF_C_OK,

            /// <summary>低压隔离开关A相断开确认</summary>
            LD_A_OK,

            /// <summary>低压隔离开关B相断开确认</summary>
            LD_B_OK,

            /// <summary>低压隔离开关C相断开确认</summary>
            LD_C_OK,

            /// <summary>低压负荷开关断开确认</summary>
            LS_OK,
             
            /// <summary>高压侧挂设地线A确认</summary>
            HP_A_OK,

            /// <summary>低压出线侧挂设地线确认</summary>
            LT_A_OK,

            /// <summary>高压出线侧拆除地线判断确认</summary>
            HP_TD,

            /// <summary>低压出线侧拆除地线判断确认</summary>
            LP_TD,

            /// <summary>重置各采集点位</summary>
            ResetPosition
        }
        #endregion

        #region constructor
        private PLCCommand2()
        {
            try
            {
                opcSrv.Connect(strSrvName);
                for (int i = 0; i < arrGrps.Length; i++)
                {
                    string strGrp = arrGrps[i];
                    OpcGroup grp = opcSrv.AddGroup(strGrp, true, 9600);
                    opcGrpsDic.Add(strGrp, grp);

                    string[] arrCurrentItems = arrItems[i].Split(',');

                    OPCItemDef[] opcItems = new OPCItemDef[arrCurrentItems.Length];
                    OPCItemResult[] opcResults = new OPCItemResult[arrCurrentItems.Length];
                    for (int j = 0; j < arrCurrentItems.Length; j++)
                    {
                        string strItemName = string.Format("{0}.{1}.{2}", strItemID, strGrp, arrCurrentItems[j]);
                        OPCItemDef item = new OPCItemDef(strItemName, true, opcResults.Length, System.Runtime.InteropServices.VarEnum.VT_BOOL);
                        opcItems[j] = item;
                    }
                    Dictionary<string, OPCItemResult> oDic = new Dictionary<string, OPCItemResult>();
                    grp.AddItems(opcItems, out opcResults);

                    for (int k = 0; k < opcResults.Length; k++)
                    {
                        oDic.Add(arrCurrentItems[k], opcResults[k]);
                    }
                    opcResultsDic.Add(strGrp, oDic);
                }
            }
            catch (Exception ee)
            {
                LogImpl.Error("plc初始化："+ee.StackTrace);
            }
           
        }
        #endregion

        #region private method
        #endregion

        #region public method
        public static PLCCommand2 GetInstance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new PLCCommand2();
                    }
                }
            }
            return instance;
        }

        public static void Dispose()
        {
            if (instance != null)
            {
                try
                {
                    foreach (var item in instance.opcGrpsDic)
                    {
                        item.Value.Remove(false);
                    }
                    instance.opcSrv.Disconnect();
                    instance = null;
                }
                catch (Exception ex)
                {
                    LogImpl.Error(string.Format("{0}{2}{1}", ex.StackTrace, ex.Message, System.Environment.NewLine));
                }
            }
        }

        public bool SyncRead(GroupName enumGrp, ItemName[] enumArryItem, out bool[] arrResult)
        {
            bool bResult;
            string strGrp = enumGrp.ToString();
            var grp = opcResultsDic[strGrp];
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
                bResult = opcGrpsDic[strGrp].SyncRead(OPCDATASOURCE.OPC_DS_DEVICE, arrHandle, out arrState);
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
            int[] arrHandle = new int[] { opcResultsDic[strGrp][enumItem.ToString()].HandleServer };
            OPCItemState[] arrState = new OPCItemState[1];

            try
            {
                bExec = opcGrpsDic[strGrp].SyncRead(OPCDATASOURCE.OPC_DS_DEVICE, arrHandle, out arrState);
            }
            catch (Exception ee)
            {
                bExec = false;
                LogImpl.Debug("plc错误:"+ee.ToString());
            }

            iResult = (bool)arrState[0].DataValue;
            return bExec;
        }

        public bool SyncWrite(GroupName enumGrp, ItemName[] enumArryItem, int[] arrWriteVal)
        {
            bool bResult;
            string strGrp = enumGrp.ToString();
            var grp = opcResultsDic[strGrp];
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
                bResult = opcGrpsDic[strGrp].SyncWrite(arrHandle, arrObj, out arrErr);
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
            int[] arrHandle = new int[] { opcResultsDic[strGrp][enumItem.ToString()].HandleServer };
            object[] arrObj = new object[] { iWriteVal };
            int[] arrErr = new int[1];

            try
            {
                bExec = opcGrpsDic[strGrp].SyncWrite(arrHandle, arrObj, out arrErr);
            }
            catch (Exception)
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
