using JAAJ.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JAAJ.DAL;

namespace TestProgram.Model
{
    /// <summary>
    /// SocreSubmit.xaml 的交互逻辑
    /// </summary>
    public partial class SocreSubmit : UserControl
    {
        public SocreSubmit()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //提交成绩
            List<JAAJ_SubItemScoreInfo> listsuItemScoreInfos = new List<JAAJ_SubItemScoreInfo>();


            string cardNum = "";

            int batchId = 0;

            //查询考生信息
            StringBuilder buiderSql = new StringBuilder();
            buiderSql.Append("select ");
            buiderSql.Append("exam.iexamineeid,exam.nvcName,exam.bsex,exam.nvcNation,");
            buiderSql.Append("exam.nvcIDNum,sub.iSubjectID,sub.nvcSubjectItemIDs,bat.nvcBatchNo ");
            buiderSql.Append(" from JAAJ_Batchs bat  ");
            buiderSql.Append("left join JAAJ_BatchExamineeMap map  on bat.iBatchID=map.iBatchID  ");
            buiderSql.Append("left join JAAJ_Examinees exam on map.iExamineeID=exam.iExamineeID  ");
            buiderSql.Append("left join JAAJ_Exams exams on exams.nvcBatchNO=bat.nvcBatchNO  ");
            buiderSql.Append(" left join JAAJ_ExamSubjects sub on sub.iExamID=exams.iExamID ");
            buiderSql.Append("  where exam.nvcIDNum = '" + cardNum + "' and bat.iBatchID='" + batchId + "'");
            DataTable table = SQLHelp.GetDataTable(buiderSql.ToString());
            if (table == null) return ;
            JAAJ_SubjectScoreInfo scorInfo = new JAAJ_SubjectScoreInfo();
            scorInfo.iExamineeID = int.Parse(table.Rows[0]["iexamineeid"].ToString());
            scorInfo.iSubjectID = int.Parse(table.Rows[0]["iSubjectID"].ToString());
            scorInfo.iSubjectItemID = int.Parse(table.Rows[0]["nvcSubjectItemIDs"].ToString());
            scorInfo.nvcBatchNO = table.Rows[0]["nvcBatchNo"].ToString();

            scorInfo.datScoreDate = DateTime.Now;

            scorInfo.nvcIDNum = cardNum;
            scorInfo.nvcName = table.Rows[0]["nvcName"].ToString();

            scorInfo.decTotalScore = 100;

            JAAJ_SubItemScoreInfo itemScoreInfo = new JAAJ_SubItemScoreInfo();
            itemScoreInfo.nvcTitleName = "人工考试";
            itemScoreInfo.iExamineeID = int.Parse(table.Rows[0]["iexamineeid"].ToString());

            for (int i = 0; i < 10; i++)
            {
                JAAJ_StepScoreInfo stepScoreInfo = new JAAJ_StepScoreInfo();
                stepScoreInfo.decStepScore = 10;
                stepScoreInfo.decTitleScore = 10;
                stepScoreInfo.nvcStepName = "步骤" + i+1 + "操作正确";

                itemScoreInfo.decSubjectItemScore += stepScoreInfo.decStepScore;

                itemScoreInfo.JAAJ_StepScoreInfoList.Add(stepScoreInfo);

                scorInfo.sumScore += stepScoreInfo.decStepScore;
            }






        }
    }
}
