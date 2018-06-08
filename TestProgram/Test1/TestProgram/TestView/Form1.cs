using Common;
using JaaJ.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestView.ViewModel;

namespace TestView
{
    public partial class Form1 : Form
    {
        Test test = new Test();
        public Form1()
        {
            InitializeComponent();

            test.MathEvent2 += new MathHandle(Operation);
        }
        MathHandle math;
        private void button1_Click(object sender, EventArgs e)
        {

            //用委托处理

            //  test.MathEvent += new MathHandle(test.Add);
            // MessageBox.Show(test.MathEvent(23,44).ToString ());

            //平常委托使用
            math += new MathHandle(test.Add);

            //  MessageBox.Show(math(11, 22).ToString());

            //使用事件处理
            //  test.MathEvent2 += new MathHandle(test.Add);  
            //  MessageBox.Show(test.Operation(1232, 323).ToString());   //Test内部定义了这个Operation方法  
            //使用事件处理2
            // test.Operation(1,2);

            //使用事件不带new 
            test.MathEvent2 += test.Add;

        }
        //添加一个执行方法的
        public int Operation(int a, int b)
        {
            MessageBox.Show((a + b).ToString());
            return 1;
        }


        //定义一个代理
        public delegate int MathHandle(int a, int b);

        public class Test
        {
            public MathHandle MathEvent;

            //定义一个事件
            public event MathHandle MathEvent2;
            public int Add(int a, int b)
            {
                return a + b;
            }

            //添加一个执行方法的
            public int Operation(int a, int b)
            {
                return MathEvent2(a, b);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            MessageBox.Show(Message(new A())); //传入对象的引用
        }

        public string Message(TestA test)
        {

            string temp = test.ShowInt(1020, 2).ToString();
            return temp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string temp = "脚手架";

            RealityScores RealityScores = new RealityScores();
            RealityScores.Step = 0;
            RealityScores.Content = "请考生选择正确的消防器材，布置消防设备摆放场地。";
            RealityScores.Score = 4;

            RealityScores.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum=0 ,Step = 0, Enable = true }  } ;
            RealityScores.ComBoxValue = new ComboxValue() { Enable = true,ComBoxVlaueList =new string[] {"吨","千克","克" } , MaxValue=5000,MinValue=10 };



            RealityScores RealityScores1 = new RealityScores();
            RealityScores1.Step = 1;
            RealityScores1.Content = "请考生选择正确垫木和底座，在指定区域内摆放垫木和底座，摆放平稳。";
            RealityScores1.Score = 6;
            RealityScores1.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 1, Enable = true }};

            RealityScores RealityScores2 = new RealityScores();
            RealityScores2.Step = 2;
            RealityScores2.Content = "请考生摆放纵向扫地杆，逐根竖立杆与纵向扫地杆扣紧。";
            RealityScores2.Score = 8;
            RealityScores2.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 2, Enable = true } };

            RealityScores RealityScores3 = new RealityScores();
            RealityScores3.Step = 3;
            RealityScores3.Content = "请考生搭设全部横向扫地杆。";
            RealityScores3.Score = 6;
            RealityScores3.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 3, Enable = true } };

            RealityScores RealityScores4 = new RealityScores();
            RealityScores4.Step = 4;
            RealityScores4.Content = "请考生搭设全部纵向水平杆和横向水平杆。";
            RealityScores4.Score = 8;
            RealityScores4.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 4, Enable = true } };

            RealityScores RealityScores5 = new RealityScores();
            RealityScores5.Step = 5;
            RealityScores5.Content = "请考生安装连墙件。";
            RealityScores5.Score = 6;
            RealityScores5.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 5, Enable = true } };


            RealityScores RealityScores6 = new RealityScores();
            RealityScores6.Step = 6;
            RealityScores6.Content = "请考生搭设十字斜撑和剪刀撑。";
            RealityScores6.Score = 6;
            RealityScores6.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 6, Enable = true } };

            RealityScores RealityScores7 = new RealityScores();
            RealityScores7.Step = 7;
            RealityScores7.Content = "请考生搭设外侧栏杆（防护栏杆）及脚手板、挡脚板。";
            RealityScores7.Score = 8;
            RealityScores7.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 7, Enable = true } };
            RealityScores RealityScores8 = new RealityScores();
            RealityScores8.Step = 8;
            RealityScores8.Content = "请考生安装外侧防护安全网和悬挂警示牌。";
            RealityScores8.Score = 6;
            RealityScores8.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 8, Enable = true } };

            RealityScores8.ReadCardValue = new ReadCardValue() { Enable = false, Address = "x0113", Epc = "451111100000000", EpcEanble = false };
            RealityScores RealityScores9 = new RealityScores();
            RealityScores9.Step = 9;
            RealityScores9.Content = "请考生利用工具测量所搭建的脚手架，测量纵距是___CM，横距是___CM。。";
            RealityScores9.Score = 6;
            RealityScores9.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 9, Enable = true } };

            RealityScores RealityScores10 = new RealityScores();
            RealityScores10.Step = 10;
            RealityScores10.Content = "请考生利用工具测量指定的4个扣件拧紧扭力矩，其中___个符合钢管脚手架扣件扭力矩标准。";
            RealityScores10.Score = 6;
            RealityScores10.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 10, Enable = true } };


            RealityScores RealityScores11 = new RealityScores();
            RealityScores11.Step = 11;
            RealityScores11.Content = "请考生进行脚手架拆除作业，首先设置安全警戒区。";
            RealityScores11.Score = 4;
            RealityScores11.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 11, Enable = true } };

            RealityScores RealityScores12 = new RealityScores();
            RealityScores12.Step = 12;
            RealityScores12.Content = "请考生从高到低拆除防护栏杆和脚手板、挡脚板，放回原处。";
            RealityScores12.Score = 4;
            RealityScores12.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 12, Enable = true } };


            RealityScores RealityScores13 = new RealityScores();
            RealityScores13.Step = 13;
            RealityScores13.Content = "请考生从高到低拆除每层外侧防护安全网以及纵横水平杆，放回原处。";
            RealityScores13.Score = 4;
            RealityScores13.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 13, Enable = true } };


            RealityScores RealityScores14 = new RealityScores();
            RealityScores14.Step = 14;
            RealityScores14.Content = "请考生拆除纵横扫地杆以及底座和垫木，放回原处。";
            RealityScores14.Score = 4;
            RealityScores14.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 14, Enable = true } };

            RealityScores RealityScores15 = new RealityScores();
            RealityScores15.Step = 15;
            RealityScores15.Content = "请考生拆除消防场地并把消防器材放回原处，然后脱下穿戴的安全用具和使用的工具放回工具柜。";
            RealityScores15.Score = 4;
            RealityScores15.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 15, Enable = true } };

            RealityScores RealityScores16 = new RealityScores();
            RealityScores16.Step = 16;
            RealityScores16.Content = "请考生在终端机上提交考试。";
            RealityScores16.Score = 0;
            RealityScores16.Submit = true;

            List<RealityScores> listRealityScores = new List<RealityScores>();
            listRealityScores.Add(RealityScores);
            listRealityScores.Add(RealityScores1);
            listRealityScores.Add(RealityScores2);
            listRealityScores.Add(RealityScores3);
            listRealityScores.Add(RealityScores4);
            listRealityScores.Add(RealityScores5);
            listRealityScores.Add(RealityScores6);
            listRealityScores.Add(RealityScores7);
            listRealityScores.Add(RealityScores8);
            listRealityScores.Add(RealityScores9);
            listRealityScores.Add(RealityScores10);
            listRealityScores.Add(RealityScores11);
            listRealityScores.Add(RealityScores12);
            listRealityScores.Add(RealityScores13);
            listRealityScores.Add(RealityScores14);
            listRealityScores.Add(RealityScores15);
            listRealityScores.Add(RealityScores16);

            CommonHelper.SerializeScoreFieldsSetting<List<RealityScores>>(listRealityScores, "RealityScores.xml");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<RealityScores> list = CommonHelper.DeSerializeScoreFieldsSetting<List<RealityScores>>("RealityScores.xml");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string temp = "跨越架";

            RealityScores RealityScores = new RealityScores();
            RealityScores.Step = 0;
            RealityScores.Content = "请考生选择正确的消防器材，布置消防设备摆放场地。";
            RealityScores.Score = 4;

            RealityScores.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 0, Enable = true } };


            RealityScores RealityScores1 = new RealityScores();
            RealityScores1.Step = 1;
            RealityScores1.Content = "请考生选择正确垫木和底座，在指定区域内摆放垫木和底座，摆放平稳。";
            RealityScores1.Score = 6;
            RealityScores1.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 1, Enable = true } };

            RealityScores RealityScores2 = new RealityScores();
            RealityScores2.Step = 2;
            RealityScores2.Content = "请考生摆放纵向扫地杆，逐根竖立杆与纵向扫地杆扣紧。";
            RealityScores2.Score = 8;
            RealityScores2.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 2, Enable = true } };

            RealityScores RealityScores3 = new RealityScores();
            RealityScores3.Step = 3;
            RealityScores3.Content = "请考生搭设全部横向扫地杆。";
            RealityScores3.Score = 6;
            RealityScores3.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 3, Enable = true } };

            RealityScores RealityScores4 = new RealityScores();
            RealityScores4.Step = 4;
            RealityScores4.Content = "请考生搭设全部纵向水平杆和横向水平杆。";
            RealityScores4.Score = 8;
            RealityScores4.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 4, Enable = true } };

            RealityScores RealityScores5 = new RealityScores();
            RealityScores5.Step = 5;
            RealityScores5.Content = "请考生搭设正面、侧面三角撑。";
            RealityScores5.Score = 8;
            RealityScores5.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 5, Enable = true } };


            RealityScores RealityScores6 = new RealityScores();
            RealityScores6.Step = 6;
            RealityScores6.Content = "请考生搭设十字斜撑和剪刀撑。";
            RealityScores6.Score = 8;
            RealityScores6.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 6, Enable = true } };

            RealityScores RealityScores7 = new RealityScores();
            RealityScores7.Step = 7;
            RealityScores7.Content = "请考生搭设斜拉杆。";
            RealityScores7.Score = 6;
            RealityScores7.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 7, Enable = true } };
            RealityScores RealityScores8 = new RealityScores();
            RealityScores8.Step = 8;
            RealityScores8.Content = "请考生搭设羊角撑和悬挂警示牌。。";
            RealityScores8.Score = 6;
            RealityScores8.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 8, Enable = true } };

            RealityScores8.ReadCardValue = new ReadCardValue() { Enable = false, Address = "x0113", Epc = "451111100000000", EpcEanble = false };

            RealityScores RealityScores9 = new RealityScores();
            RealityScores9.Step = 9;
            RealityScores9.Content = "请考生利用工具测量所搭建的跨越架，测量纵距是___CM，横距是___CM。。";
            RealityScores9.Score = 6;
            RealityScores9.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 9, Enable = true } };

            RealityScores RealityScores10 = new RealityScores();
            RealityScores10.Step = 10;
            RealityScores10.Content = "请考生利用工具测量指定的4个扣件拧紧扭力矩，其中___个符合钢管脚手架扣件扭力矩标准。";
            RealityScores10.Score = 4;
            RealityScores10.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 10, Enable = true } };


            RealityScores RealityScores11 = new RealityScores();
            RealityScores11.Step = 11;
            RealityScores11.Content = "请考生进行脚手架拆除作业，首先设置安全警戒区。";
            RealityScores11.Score = 4;
            RealityScores11.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 11, Enable = true } };

            RealityScores RealityScores12 = new RealityScores();
            RealityScores12.Step = 12;
            RealityScores12.Content = "请考生从高到低拆除羊角撑和正面、侧面三角撑，放回原处。";
            RealityScores12.Score = 4;
            RealityScores12.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 12, Enable = true } };


            RealityScores RealityScores13 = new RealityScores();
            RealityScores13.Step = 13;
            RealityScores13.Content = "请考生拆除剪刀撑和纵向、横向水平杆。";
            RealityScores13.Score = 4;
            RealityScores13.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 13, Enable = true } };


            RealityScores RealityScores14 = new RealityScores();
            RealityScores14.Step = 14;
            RealityScores14.Content = "请考生拆除纵横扫地杆以及底座和垫木，放回原处。";
            RealityScores14.Score = 4;
            RealityScores14.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 14, Enable = true } };

            RealityScores RealityScores15 = new RealityScores();
            RealityScores15.Step = 15;
            RealityScores15.Content = "请考生拆除消防场地并把消防器材放回原处，然后脱下穿戴的安全用具和使用的工具放回工具柜。";
            RealityScores15.Score = 4;
            RealityScores15.ScreenValue = new ScreenValue[] { new ScreenValue() { ScreenNum = 0, Step = 15, Enable = true } };

            RealityScores RealityScores16 = new RealityScores();
            RealityScores16.Step = 16;
            RealityScores16.Content = "请考生在终端机上提交考试。";
            RealityScores16.Score = 0;
            RealityScores16.Submit = true;

            List<RealityScores> listRealityScores = new List<RealityScores>();
            listRealityScores.Add(RealityScores);
            listRealityScores.Add(RealityScores1);
            listRealityScores.Add(RealityScores2);
            listRealityScores.Add(RealityScores3);
            listRealityScores.Add(RealityScores4);
            listRealityScores.Add(RealityScores5);
            listRealityScores.Add(RealityScores6);
            listRealityScores.Add(RealityScores7);
            listRealityScores.Add(RealityScores8);
            listRealityScores.Add(RealityScores9);
            listRealityScores.Add(RealityScores10);
            listRealityScores.Add(RealityScores11);
            listRealityScores.Add(RealityScores12);
            listRealityScores.Add(RealityScores13);
            listRealityScores.Add(RealityScores14);
            listRealityScores.Add(RealityScores15);
            listRealityScores.Add(RealityScores16);

            CommonHelper.SerializeScoreFieldsSetting<List<RealityScores>>(listRealityScores, "RealityScores.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CommonHelper.GetLocalIP());
        }
    }
    class A : TestA
    {
        public int ShowInt(int a, int b)
        {
            return a + b * 100;
        }
    }
    public interface TestA
    {
    
        int ShowInt(int a, int b);
    }
}
