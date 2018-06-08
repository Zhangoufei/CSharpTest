using gregn6Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试.GRID__
{
    public partial class PrintTestView : Form
    {
        public PrintTestView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }
        //定义Grid++Report报表主对象
        private GridppReport Report = new GridppReport();

        DataTable table = new DataTable();
        private void PrintTestView_Load(object sender, EventArgs e)
        {

            table.Columns.Add("S1");
            table.Columns.Add("S2");
            table.Columns.Add("S3");
            table.Columns.Add("S4");
            table.Columns.Add("S5");
            table.Columns.Add("S6");

            for (int i = 0; i < 1; i++)
            {
                DataRow row = table.NewRow();
                row["S1"] = "15516962912";
                row["S2"] = "男";
                row["S3"] = "@3232" + i;
                row["S4"] = "adress" + i;
                row["S5"] = "QQ" + i;
                row["S6"] = "郑州";
                table.Rows.Add(row);
            }

            string path = Application.StartupPath + "\\TestCode.grf";
            Report.LoadFromFile(path);

            //连接报表事件
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);

        }
        private void ReportInitialize()
        {
            //在此记录下每个字段的接口指针
        }

        private void ReportFetchRecord()
        {
            GridppReportDemo.Utility.FillRecordToReport(Report, table);

           // this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report.Print(false);
        }
    }
}
