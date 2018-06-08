using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test001
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : System.Windows.Controls.Page
    {
        public Window1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 测试字节数组  list添加指定数组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            byte[] byteTemp = new byte[3];
            byteTemp[0] = 120;
            byteTemp[1] = 23;
            byteTemp[2] = 45;
            List<byte> list = new List<byte>();
            list.AddRange(byteTemp);

            string temp1 = ByteToString(byteTemp);  //把字节型转换成十六进制字符串

           // byte[] strToToHexByteTemp = strToToHexByte(byteTemp);  //字节型转十六进制字符串

            string byteToHexStrTemp = byteToHexStr(byteTemp);  //字节型转十六进制字符串


            //十六进制转换为字符串
            byte bytComAdr = 0x02;
            byte bytBaud = 0x5;

            string temp = bytComAdr.ToString();

            string temp2 = bytBaud.ToString();
        }

        #region 字节型转换16

        /// <summary>

        /// 把字节型转换成十六进制字符串

        /// </summary>

        /// <param name="InBytes"></param>

        /// <returns></returns>

        public static string ByteToString(byte[] InBytes)
        {

            string StringOut = "";

            foreach (byte InByte in InBytes)

            {

                StringOut = StringOut + String.Format("{0:X2} ", InByte);

            }

            return StringOut;

        }

        #endregion

        #region 十六进制字符串转字节型

        /// <summary>

        /// 把十六进制字符串转换成字节型(方法1)

        /// </summary>

        /// <param name="InString"></param>

        /// <returns></returns>

        public static byte[] StringToByte(string InString)

        {

            string[] ByteStrings;

            ByteStrings = InString.Split(" ".ToCharArray());

            byte[] ByteOut;

            ByteOut = new byte[ByteStrings.Length];

            for (int i = 0; i <= ByteStrings.Length - 1; i++)

            {

                //ByteOut[i] = System.Text.Encoding.ASCII.GetBytes(ByteStrings[i]);

                ByteOut[i] = Byte.Parse(ByteStrings[i], System.Globalization.NumberStyles.HexNumber);

                //ByteOut[i] =Convert.ToByte("0x" + ByteStrings[i]);

            }

            return ByteOut;

        }

        #endregion

        #region 十六进制字符串转字节型

        /// <summary>

        /// 字符串转16进制字节数组(方法2)

        /// </summary>

        /// <param name="hexString"></param>

        /// <returns></returns>

        public static byte[] strToToHexByte(string hexString)

        {

            hexString = hexString.Replace(" ", "");

            if ((hexString.Length % 2) != 0)

                hexString += " ";

            byte[] returnBytes = new byte[hexString.Length / 2];

            for (int i = 0; i < returnBytes.Length; i++)

                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

            return returnBytes;

        }

        #endregion

        #region 字节型转十六进制字符串

        /// <summary>

        /// 字节数组转16进制字符串

        /// </summary>

        /// <param name="bytes"></param>

        /// <returns></returns>

        public static string byteToHexStr(byte[] bytes)

        {

            string returnStr = "";

            if (bytes != null)

            {

                for (int i = 0; i < bytes.Length; i++)

                {

                    returnStr += bytes[i].ToString("X2");

                }

            }

            return returnStr;

        }

        #endregion

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            倒计时 mm = new 倒计时();
            mm.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> temp = new ObservableCollection<string>();
            temp.Add("212121");
            temp.Add("AAAAAAAA");
            temp.Add("BBBBBBBB");
            foreach (var item in temp)
            {
                MessageBox.Show(item);
            }
        }
    }
}
