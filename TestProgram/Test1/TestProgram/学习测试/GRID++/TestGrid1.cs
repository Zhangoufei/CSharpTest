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
    public partial class TestGrid1 : Form
    {
        public TestGrid1()
        {
            InitializeComponent();
        }
        //定义Grid++Report报表主对象
        private GridppReport Report = new GridppReport();
        private void TestGrid1_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\test.grf";
            Report.LoadFromFile(path);

            //连接报表事件
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);

           // Report.Print(PrintView);  //打印预览
            Report.Print(PrintView);
        }
        private void ReportInitialize()
        {
            //在此记录下每个字段的接口指针

        }
      
        private DataTable table = null;

        public DataTable Table
        {
            get
            {
                return table;
            }

            set
            {
                table = value;
            }
        }

        public bool PrintView
        {
            get
            {
                return printView;
            }

            set
            {
                printView = value;
            }
        }

        private bool printView;

        private void ReportFetchRecord()
        {
            GridppReportDemo.Utility.FillRecordToReport(Report, Table);
         
            this.Close();

        }

    }
}
