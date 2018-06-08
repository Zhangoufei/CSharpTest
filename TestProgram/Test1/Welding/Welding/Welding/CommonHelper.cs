using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using JAAJ.Model;
using JAAJExamManagementSys;
using UploadScore;
using Welding;

namespace PutoutFire.Common
{
    public class CommonHelper
    {
        public static JAAJ_ExamineeInfo examineeInfo;
        public static Window mainWindow;

        public static string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs.Length == 0) return str;
            var da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }
        public static void SpeechToPrompt(string strText)
        {
            try
            {
                var tts = SpeekTTS.GetInstance();
                tts.SpeechToPrompt(strText);
            }
            catch (Exception ex)
            {
                //LogImpl.Error(string.Format("{0}{2}{1}", ex.StackTrace, ex.Message, System.Environment.NewLine));
            }
        }

        JAAJ_ExamProceInfo oepiExamProceInfo = null;
        JAAJ_SubjectScoreInfo ossiJAAJ_SubjectScoreInfo = new JAAJ_SubjectScoreInfo();
        List<JAAJ_SubItemScoreInfo> osisiJAAJ_SubItemScoreInfoList = new List<JAAJ_SubItemScoreInfo>();
        /// <summary>
        /// 解析成绩XML保存考生成绩
        /// </summary>
        /// <param name="nvcBatchNO">批次号</param>
        /// <param name="iExamineeID">考生ID</param>
        public void SaveScore(string nvcBatchNO, int iExamineeID)
        {
            oepiExamProceInfo = ScoreData.GetExamProceInfoByExamineeID(iExamineeID);
            if (oepiExamProceInfo != null)
            {
                //int iSubjectItemID = oepiExamProceInfo.iSubjectItemID;
                //下面代码可以根据科目子项ID调出对应的子项对应的试题.....


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
        }

        /// 获得指定元素的父元素  
        /// </summary>  
        /// <typeparam name="T">指定页面元素</typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static T GetParentObject<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        public static T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);


                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
            return null;
        }

        /// <summary>  
        /// 获得指定元素的所有子元素  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static List<T> GetChildObjects<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child));
            }
            return childList;
        }

    }
}