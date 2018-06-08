using Common.ExcelHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckExcelTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataTable table = null;
        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "";

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All files（*.*）|*.*|All files(*.*)|*.* ";
            if (open.ShowDialog() == DialogResult.OK)
            {
                fileName = open.FileName;
            }
            else
            {
                MessageBox.Show("请选择文件");
            }

            ExcelHelper excelhelper = new ExcelHelper(fileName);

            table = excelhelper.ExcelToDataTable("", true);

            dataGridView1.DataSource = table;
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;

            string sourceFile = Read(path + "\\content\\format.txt");

            string[] sourceTemp = sourceFile.Split('@');

            string header = sourceTemp[0] + "\r\n";
            string end = sourceTemp[1] + "\r\n";

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    try
                    {
                        if (table.Rows[i][1].ToString().Length <= 0) continue;
                        StringBuilder tempBuider = new StringBuilder();
                        tempBuider.Append("\r\n");
                        tempBuider.Append("<tr>");
                        string pngName = table.Rows[i][0].ToString();
                        tempBuider.Append(" <td colspan=\"4\" style=\"text - align:center\"><img src=\"pic /" + pngName + ".png\" /></td>\r\n");
                        tempBuider.Append("</tr>\r\n");

                        string checkMethod = table.Rows[0][3].ToString();

                        string checkMethodName = table.Rows[i][3].ToString();
                        tempBuider.Append("<tr>\r\n");
                        tempBuider.Append(" <td colspan=\"4\"  style=\"text - align:center; \">" + checkMethod + "：" + checkMethodName + "</td>\r\n");
                        tempBuider.Append("</tr>\r\n");

                        tempBuider.Append("<tr>\r\n");
                        tempBuider.Append(" <td class=\"td - title\">检修标准：</td>\r\n");
                        tempBuider.Append(" <td class=\"td - no\">1.</td>\r\n");

                        string content = table.Rows[i][4].ToString();
                        tempBuider.Append("" + content + "\r\n");
                        tempBuider.Append(" <td class=\"td - none\"></td>	\r\n");
                        tempBuider.Append("<td class=\"\"></td> \r\n");
                        tempBuider.Append("</tr>  \r\n");

                        StringBuilder endFile = new StringBuilder();
                        endFile.Append(header);
                        endFile.Append(tempBuider.ToString());
                        endFile.Append(end);

                        string fileName = path + "\\大网页\\" + table.Rows[i][1].ToString() + "_L.html";

                        Write(fileName, endFile.ToString());
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("出现异常;" + ee.StackTrace);
                    }
                   
                }
            }


            //Write(path+"hell.html");


        }

        public void Write(string path, string content)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(content);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        public string Read(string path)
        {
            StringBuilder buiderTemp = new StringBuilder();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                buiderTemp.Append(line.ToString());
            }
            return buiderTemp.ToString();
        }
    }
}
