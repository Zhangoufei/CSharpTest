using Common.ExcelHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试.基础学习
{
    public partial class ExcelTest : Form
    {
        public ExcelTest()
        {
            InitializeComponent();
        }
        ExcelHelper helper;
        private void button1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                helper = new ExcelHelper(openfile.FileName);
                DataTable table = helper.ExcelToDataTable("sheet2", true);
                dataGridView1.DataSource = table;
            }
           
        }
    }
}
