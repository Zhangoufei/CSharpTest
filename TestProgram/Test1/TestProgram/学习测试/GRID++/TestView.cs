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
    public partial class TestView : Form
    {
        public TestView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintTestView print = new PrintTestView();
            print.ShowDialog();
        }
        DataTable table = new DataTable();
        private void TestView_Load(object sender, EventArgs e)
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
                row["S1"] = "张三"+i;
                row["S2"] = "男";
                row["S3"] = "@3232"+i;
                row["S4"] = "adress"+i;
                row["S5"] = "QQ"+i;
                row["S6"] = "郑州";
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestGrid1 test = new TestGrid1();
            test.Table = table;
            test.PrintView = false;
            try
            {
                test.Show();
            }
            catch (Exception)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GridViewTest grid = new GridViewTest();
            grid.printView();


        }
    }
}
