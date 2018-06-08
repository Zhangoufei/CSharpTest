using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using BardCodeHooK;
using Common;
using HardWare.CardReader;
using 学习测试2.Test;

namespace 学习测试2
{
    public partial class Form1 : Form
    {
     //   public delegate void ShowInfoDelegate2(BarCodeHook.BarCodes barCode);

        private readonly BardCodeHooK.BardCodeHooK BarCode = new BardCodeHooK.BardCodeHooK();

      //  public BarCodeHook BarCode2 = new BarCodeHook();

        private readonly IDCard IC = new IDCard();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var test = new TestUDP();
            test.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ICCardVM();
        }

        public void ICCardVM()
        {
            Func<IDCard, IDCard> fuc = IC.ReadCard;
            fuc.BeginInvoke(IC, CanExam, null);
        }

        private void CanExam(IAsyncResult result)
        {
            LogImpl.Debug("读卡成功");
            var ar = (AsyncResult)result;
            var md = (Func<IDCard, IDCard>)ar.AsyncDelegate;
            var IC = md.EndInvoke(result);
            if (IC == null)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BarCode.BarCodeEvent += BarCode_BarCodeEvent1;
            BarCode.Start();

        }

        private void BarCode_BarCodeEvent1(BardCodeHooK.BardCodeHooK.BarCodes barCode)
        {
            ShowInfo(barCode);
        }

        private void ShowInfo(BardCodeHooK.BardCodeHooK.BarCodes barCode)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ShowInfoDelegate(ShowInfo), barCode);
            }
            else
            {
              
                textBox1.Text = barCode.KeyName;
                textBox2.Text = barCode.VirtKey.ToString();
                textBox3.Text = barCode.ScanCode.ToString();
                //  textBox4.Text = barCode.Ascll.ToString();
                textBox5.Text = barCode.Chr.ToString();
                textBox6.Text = barCode.IsValid ? barCode.BarCode : ""; //是否为扫描枪输入，如果为true则是 否则为键盘输入

                try
                {
                    string temp = barCode.IsValid ? barCode.BarCode : "";
                   // temp = temp.Replace("\0","").Replace("\r","");
                    textBox1.Text = CommonHelper.Decrypt(temp);
                   // this.Focus();
                }
                catch (Exception)
                {
                   textBox1.Text = "异常";
                }
              
                textBox7.Text += barCode.KeyName;
                //MessageBox.Show(barCode.IsValid.ToString());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  BarCode.Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //BarCode2.BarCodeEvent += BarCode_BarCodeEvent2;
            //BarCode2.Start();
        }

        //public void BarCode_BarCodeEvent2(BarCodeHook.BarCodes barCode)
        //{
        //    //  ShowInfo(barCode);
        //    string temp = barCode.IsValid ? barCode.BarCode : "";
        //    temp = temp.Replace("\0", "").Replace("\r", "");

        //    textBox3.Text = temp;
        //    try
        //    {
        //        listBox1.Items.Add(temp);
        //        textBox1.Text = CommonHelper.Decrypt(temp);

        //    }
        //    catch (Exception)
        //    {
        //        textBox1.Text = "异常";
        //    }

        //}

           private delegate void ShowInfoDelegate(BardCodeHooK.BardCodeHooK.BarCodes barCode);

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PCA测试 pcl = new PCA测试() {TopMost=true};
            pcl.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Plc通用 pcl = new Plc通用() {TopMost=true};
            pcl.ShowDialog();
        }

        private void btnRFID_Click(object sender, EventArgs e)
        {
            RFID rfid = new RFID() { TopMost = true };
            rfid.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string ipAddress = "10.0.60.218";
            int portNum = 1433;
            IPAddress ip = IPAddress.Parse(ipAddress);
            try
            {
                IPEndPoint point = new IPEndPoint(ip, portNum);
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    sock.Connect(point);
                    Console.WriteLine("连接{0}成功!", point);
                    sock.Close();
                }
            }
            catch (SocketException ee)
            {
                Console.WriteLine("连接{0}失败", ee.StackTrace);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Ping("10.0.60.218"))
            {
                MessageBox.Show("网络通信正常");
            }
        }

        public bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }
    }
}