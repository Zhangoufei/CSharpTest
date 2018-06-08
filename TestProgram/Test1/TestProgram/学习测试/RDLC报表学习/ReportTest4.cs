using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试.RDLC报表学习
{
    public partial class ReportTest4 : Form
    {
        public ReportTest4()
        {
            InitializeComponent();
        }

        private void ReportTest4_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            this.pageSetupDialog1.AllowMargins = true;
            this.pageSetupDialog1.AllowOrientation = true;
            this.pageSetupDialog1.AllowPaper = true;
            this.pageSetupDialog1.AllowPrinter = true;
            this.pageSetupDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            printDialog1.AllowPrintToFile = true;
            printDialog1.AllowCurrentPage = true;
            printDialog1.AllowSelection = true;
            printDialog1.AllowSomePages = true;
            printDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要预览打印文档", "打印预览", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.printPreviewDialog1.UseAntiAlias = true;
                this.printPreviewDialog1.Document = this.printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                this.printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("神舟十号今天下午五点三十八分上天", new Font("黑体", 18), Brushes.Red, 80, 80);
        }
    }
}
