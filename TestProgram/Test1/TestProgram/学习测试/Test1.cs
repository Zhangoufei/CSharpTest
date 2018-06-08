using Common;
using HardWare;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using 学习测试.GRID__;
using 学习测试.RDLC报表学习;
using 学习测试.基础学习;

namespace 学习测试
{
    public partial class Test1 : Form
    {
        public Test1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CommonHelper.MD5(textBox1.Text.Trim()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportTest1 report = new ReportTest1();
            report.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportTest2 report = new ReportTest2();
            report.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportTest3 tea = new ReportTest3();
            tea.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReportTest4 tea = new ReportTest4();
            tea.Show();
        }


        /// <summary>
        /// 例子1  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            int[] A = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

            int[] B = new int[] { 2, 4, 7, 8, 9 };

            List<int> c = new List<int>();
            //如果 a中和b中相同的添加 到c 中

            foreach (var a in A)
            {
                foreach (var b in B)
                {
                    if (a == b)
                    {
                        c.Add(a);
                    }

                }
            }
            //输出c  //直接看 比较麻烦  使用linq 写法

            var query = from a in A
                        from b in B
                        where a == b
                        select a;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var array = new int[] { 1, 2, 3 };


            var query = from arr in array
                        where arr > 1
                        select arr;
        }

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            string temp = "32";
            MessageBox.Show(temp.GetString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //使用findAll
            List<int> list = new List<int>();
            list.AddRange(new int[] { 1, 2, 3, 5, 2, 44, 22, 455 });


            //使用传统的委托调用 findALL
            Predicate<int> callback = new Predicate<int>(IsEventNumber);
            List<int> eventNumbers = list.FindAll(callback);
            foreach (var item in eventNumbers)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

        }

        private bool IsEventNumber(int i)
        {
            return i % 2 == 0;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var tem = new methodDelegate(methodDelegateTest);
            //  tem.Invoke("32432",4243);
            tem("543543", 4343);
        }

        public delegate void methodDelegate(string temp, int num);

        private void methodDelegateTest(string temp, int num)
        {

            MessageBox.Show(temp + num + 1);
        }

        private void Test1_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, file + ".config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            config.AppSettings.Settings["app"].Value = "New Value";
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");

        }

        private static void NewMethod(Configuration config)
        {
            config.AppSettings.Settings["app123"].Value = "123456";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetAddressIP());
        }


        public string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TestView grid = new TestView();
            grid.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string temp = "aaaa";
            var temp2 = temp = "bbb";
            string temp4 = "cc";
            var temp3 = temp4 = "aaa";
            MessageBox.Show(temp3 + ":" + temp4);


            int tempint = 0;
            var tempint2 = tempint == 1;
            if (!tempint2)
            {
                MessageBox.Show(tempint2.ToString());
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            LogImpl.Debug("Debug");
            LogImpl.Error("错误信息");
            LogImpl.Fatal("Fatal");
            LogImpl.Info("Info");
            LogImpl.Warn("警告Warn");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int i = 100;
            try
            {
                int sum = i / 0;
            }
            catch (Exception)
            {
                throw;
            }


        }

        private void button18_Click(object sender, EventArgs e)
        {

            List<Test> list = new List<Test>();

            Test test = new Test();
            list.Add(test);
            MessageBox.Show(list[0].Id);
            test.Id = "434343";
            MessageBox.Show(list[0].Id);

        }

        class Test
        {
            private string id;

            public string Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value;
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("123", "456");
            dic.Add("44", "4444");
            dic.Add("11", "4444");
            dic.Add("22", "123");

        }

        private void button21_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("序号");
            table.Columns.Add("姓名");
            table.Columns.Add("年龄");
            for (int i = 0; i < 20; i++)
            {
                DataRow row = table.NewRow();
                row["序号"] = i + 1;
                row["姓名"] = "张三" + i;
                row["年龄"] = 20 + i;
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;

        }
        List<string> list = new List<string>();
        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    list.Add(item.Cells[0].Value.ToString());
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            视频 视频 = new 视频();
            视频.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ExcelTest test = new ExcelTest();
            test.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string temp = CommonHelper.Encrypt(textBox1.Text);
            textBox2.Text = temp + "长度为" + temp.Length;

        }

        private void button26_Click(object sender, EventArgs e)
        {
            string temp = CommonHelper.Decrypt(textBox2.Text);
            MessageBox.Show(temp + "长度为" + temp.Length);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            LogIMP.HelloWorld();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            LogImpl.Debug("temp");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            MultipleScreen screen = new MultipleScreen();
            screen.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            EventTest test=new EventTest();
            test.Show();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            RFIDTest test=new RFIDTest();
            test.Show();
        }
    }
    public static class TestV
    {
        public static string GetString(this string input)
        {
            return input;
        }
    }



    public class LogIMP
    {
        static string temp;
        static LogIMP()
        {
            temp = "ni hao ";
        }

        public static string HelloWorld()
        {

            return "HelloWorld" + temp;
        }
    }
}
