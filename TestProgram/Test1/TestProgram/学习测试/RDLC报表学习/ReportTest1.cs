using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 学习测试.RDLC报表学习
{
    public partial class ReportTest1 : Form
    {
        public ReportTest1()
        {
            InitializeComponent();
        }
        private void ReportTest1_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Report1.rdlc";

            CheckForIllegalCrossThreadCalls = false;
            Thread  thGetReport = new Thread(BindInfo);
            thGetReport.Start();
            this.reportViewer1.RefreshReport();

        }


        /// <summary>
        /// 绑定报表数据
        /// </summary>
        private void BindInfo()
        {
            ReportParameter params1 = new ReportParameter("Value1", "32423");
            //默认照片
            ReportParameter params2 = new ReportParameter("Value2", "324244444444443");
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { params1,params2 });

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();
        }





    }
}
