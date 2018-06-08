using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JAAJ.Model;
using JAAJ.Common;
using JAAJExamManagementSys;

namespace UploadScore
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }
        JAAJ_ExamineeInfo oeiJAAJ_ExamineeInfo = null;
        JAAJ_ExamProceInfo oepiExamProceInfo = null;
        /// <summary>
        /// 开始考试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            #region 验证考试是否应该参与考试
            JAAJ_ExamInfo oeiJAAJ_ExamInfo = ScoreData.GetCurrentExamInfo();
            if (oeiJAAJ_ExamInfo != null)
            {
                oeiJAAJ_ExamineeInfo = ScoreData.GetExamineeInfoByIDNum(txtIDNum.Text.Trim(), oeiJAAJ_ExamInfo.nvcBatchNO);
                if (oeiJAAJ_ExamineeInfo == null)
                {
                    MessageBox.Show("不存在该考生信息", "提示");
                    return;
                }
            }
            #endregion

            #region 调出对应考试科目的试题显示在终端界面上
            oepiExamProceInfo = ScoreData.GetExamProceInfoByExamineeID(oeiJAAJ_ExamineeInfo.iExamineeID);
            if (oepiExamProceInfo != null)
            {
                int iSubjectItemID = oepiExamProceInfo.iSubjectItemID;
                //下面代码可以根据科目子项ID调出对应的子项对应的试题.....
            }

            #endregion

            #region 修改设备状态为忙碌状态
            EnumSubmitResult oResult = ScoreData.UpdateDeviceStatus(1);
            #endregion
        }

        JAAJ_SubjectScoreInfo ossiJAAJ_SubjectScoreInfo = new JAAJ_SubjectScoreInfo();
        List<JAAJ_SubItemScoreInfo> osisiJAAJ_SubItemScoreInfoList = new List<JAAJ_SubItemScoreInfo>();

        /// <summary>
        /// 手动编写步骤成绩保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //保存成绩
            SaveScore();
            //其他操作
            ScoreData.UpdateDeviceStatusCallListDeleteExamProcess(oeiJAAJ_ExamineeInfo.iExamineeID);
        }

        public void SaveScore()
        {

            //本次考题得分总数；
            int FireSum = 0;
            //第一步分数;
            int OneFire = 0; ;
            //第二步骤得分;
            int twoFire = 0;

            //判断着火的类型与灭火器选择的分数；
            //String  //FireStruct.sFireType = s[2];//着火的类型   1：水基灭火
            int nTemp = 0;
            if (nTemp == 0)
            {
                OneFire = 15;
            }
            else
            {
                OneFire = 0;
            }
            //是否成功对准火焰根部灭火；
            int nTemp1 = 0;
            if (nTemp1 == 0)
            {
                twoFire = 10;
            }
            else
            {
                twoFire = 0;
            }

            FireSum = twoFire + OneFire;

            string sFireSum = Convert.ToString(FireSum);
            sFireSum = "本次灭火总成绩：" + sFireSum;


            JAAJ_SubjectItemInfo osiiSubjectItemInfo = ScoreData.GetSubjectItemInfoByID(oepiExamProceInfo.iSubjectItemID);
            if (osiiSubjectItemInfo != null)
            {
                #region 给子项总分值赋值
                //给子项总分值赋值
                ossiJAAJ_SubjectScoreInfo = new JAAJ_SubjectScoreInfo();
                ossiJAAJ_SubjectScoreInfo.nvcBatchNO = oeiJAAJ_ExamineeInfo.nvcBatchNO;
                ossiJAAJ_SubjectScoreInfo.iExamineeID = oeiJAAJ_ExamineeInfo.iExamineeID;
                ossiJAAJ_SubjectScoreInfo.iSubjectID = osiiSubjectItemInfo.iSubjectID;
                ossiJAAJ_SubjectScoreInfo.iSubjectItemID = osiiSubjectItemInfo.iSubjectItemID;
                ossiJAAJ_SubjectScoreInfo.decSubjectScore = FireSum;   //考生本次考试所考子项总分值
                ossiJAAJ_SubjectScoreInfo.datScoreDate = DateTime.Now;
                ossiJAAJ_SubjectScoreInfo.nvcMemo = "";
                #endregion



                #region 第一题
                //第一题
                JAAJ_SubItemScoreInfo osisiJAAJ_SubItemScoreInfo = new JAAJ_SubItemScoreInfo();
                osisiJAAJ_SubItemScoreInfo.iExamineeID = oeiJAAJ_ExamineeInfo.iExamineeID;
                osisiJAAJ_SubItemScoreInfo.iTitleID = 0;
                osisiJAAJ_SubItemScoreInfo.nvcTitleName = "厨房灭火";
                osisiJAAJ_SubItemScoreInfo.nvcDescription = "厨房灭火";
                osisiJAAJ_SubItemScoreInfo.decSubjectItemScore = FireSum;  //本道题本次考试所得分值
                osisiJAAJ_SubItemScoreInfo.nvcMemo = "";


                #region  第一题试题步骤实体集赋值

                #region 第一步骤
                JAAJ_StepScoreInfo ossiJAAJ_StepScoreInfo = new JAAJ_StepScoreInfo();
                ossiJAAJ_StepScoreInfo.nvcStepName = "灭火器选择";
                ossiJAAJ_StepScoreInfo.nvcDescription = "灭火器选择";
                ossiJAAJ_StepScoreInfo.decStepScore = OneFire;//本步骤本次考试所得分值
                ossiJAAJ_StepScoreInfo.nvcMemo = "";
                osisiJAAJ_SubItemScoreInfo.JAAJ_StepScoreInfoList.Add(ossiJAAJ_StepScoreInfo);
                #endregion

                #region 第二步骤
                ossiJAAJ_StepScoreInfo = new JAAJ_StepScoreInfo();
                ossiJAAJ_StepScoreInfo.nvcStepName = "是否对准火焰根部，开始灭火";
                ossiJAAJ_StepScoreInfo.nvcDescription = "是否对准火焰根部，开始灭火";
                ossiJAAJ_StepScoreInfo.decStepScore = twoFire;//本步骤本次考试所得分值
                ossiJAAJ_StepScoreInfo.nvcMemo = "";
                osisiJAAJ_SubItemScoreInfo.JAAJ_StepScoreInfoList.Add(ossiJAAJ_StepScoreInfo);
                #endregion

                #endregion

                osisiJAAJ_SubItemScoreInfoList.Add(osisiJAAJ_SubItemScoreInfo);

                #endregion
                EnumSubmitResult enResult = ScoreData.Save(ossiJAAJ_SubjectScoreInfo, osisiJAAJ_SubItemScoreInfoList);

                if (enResult == EnumSubmitResult.Success)
                {
                    //MessageBox.Show("分值提交成功", "提示");
                }
                else
                {
                    //MessageBox.Show("分值提交失败", "提示");
                }
            }
        }
        /// <summary>
        /// 解析XML文件保存成绩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //保存成绩
            SaveScore(oeiJAAJ_ExamineeInfo.nvcBatchNO, oeiJAAJ_ExamineeInfo.iExamineeID);
            //其他操作
            ScoreData.UpdateDeviceStatusCallListDeleteExamProcess(oeiJAAJ_ExamineeInfo.iExamineeID);
        }


        /// <summary>
        /// 解析成绩XML保存考生成绩
        /// </summary>
        /// <param name="nvcBatchNO">批次号</param>
        /// <param name="iExamineeID">考生ID</param>
        public void SaveScore(string nvcBatchNO, int iExamineeID)
        {
            BaseInfo obiBaseInfo = JAAJExamManagementSys.Common.CurrentScoreFieldsSetting;
            foreach (SubjectItem osiSubjectItem in obiBaseInfo.SubjectItemList)
            {
                JAAJ_SubjectItemInfo osiiSubjectItemInfo = ScoreData.GetSubjectItemInfoByID(oepiExamProceInfo.iSubjectItemID);
                if (osiiSubjectItemInfo != null)
                {
                    #region 给子项分值赋值
                    ossiJAAJ_SubjectScoreInfo = new JAAJ_SubjectScoreInfo();
                    ossiJAAJ_SubjectScoreInfo.nvcBatchNO = nvcBatchNO;                             //批次号
                    ossiJAAJ_SubjectScoreInfo.iExamineeID = iExamineeID;                           //考生ID，从考生表查
                    ossiJAAJ_SubjectScoreInfo.iSubjectID = osiiSubjectItemInfo.iSubjectID;
                    ossiJAAJ_SubjectScoreInfo.iSubjectItemID = osiiSubjectItemInfo.iSubjectItemID;
                    ossiJAAJ_SubjectScoreInfo.decSubjectScore = osiSubjectItem.SubjectItemScore;   //考生本次考试所考子项总分值
                    ossiJAAJ_SubjectScoreInfo.datScoreDate = DateTime.Now;
                    ossiJAAJ_SubjectScoreInfo.nvcMemo = "";
                    #endregion

                    foreach (Title otTitle in osiSubjectItem.TitleList[0].TitleList)
                    {
                        #region 加入试题成绩
                        JAAJ_SubItemScoreInfo osisiJAAJ_SubItemScoreInfo = new JAAJ_SubItemScoreInfo();
                        osisiJAAJ_SubItemScoreInfo.iExamineeID = iExamineeID;                 //需要根据身份证号查出赋值
                        osisiJAAJ_SubItemScoreInfo.iTitleID = 0;
                        osisiJAAJ_SubItemScoreInfo.nvcTitleName = otTitle.TitleName;
                        osisiJAAJ_SubItemScoreInfo.nvcDescription = otTitle.TitleDescription;
                        osisiJAAJ_SubItemScoreInfo.decSubjectItemScore = otTitle.TitleScore;  //本道题本次考试所得分值
                        osisiJAAJ_SubItemScoreInfo.nvcMemo = "";
                        #endregion

                        #region 循环加入试题对应的步骤成绩
                        if (otTitle.StepList.Count > 0)
                        {
                            foreach (Step osStep in otTitle.StepList[0].StepList)
                            {
                                JAAJ_StepScoreInfo ossiJAAJ_StepScoreInfo = new JAAJ_StepScoreInfo();
                                ossiJAAJ_StepScoreInfo.nvcStepName = osStep.StepName;
                                ossiJAAJ_StepScoreInfo.nvcDescription = osStep.StepDescription;
                                ossiJAAJ_StepScoreInfo.decStepScore = osStep.StepScore;          //本步骤本次考试所得分值
                                ossiJAAJ_StepScoreInfo.nvcMemo = "";
                                osisiJAAJ_SubItemScoreInfo.JAAJ_StepScoreInfoList.Add(ossiJAAJ_StepScoreInfo);
                            }
                        }
                        #endregion

                        osisiJAAJ_SubItemScoreInfoList.Add(osisiJAAJ_SubItemScoreInfo);
                    }

                    EnumSubmitResult enResult = ScoreData.Save(ossiJAAJ_SubjectScoreInfo, osisiJAAJ_SubItemScoreInfoList);

                    if (enResult == EnumSubmitResult.Success)
                    {
                        //MessageBox.Show("分值提交成功", "提示");
                    }
                    else
                    {
                        //MessageBox.Show("分值提交失败", "提示");
                    }
                }
            }
        }
        /// <summary>
        /// 部分终端刷完卡未开始考试更新设备状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            ScoreData.UpdateDeviceStatus(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ScoreData.UpdateDeviceStatusCallListDeleteExamProcess(2);
        }
    }
}
