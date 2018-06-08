using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Common
{
    public class CommonHelper
    {

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obiBaseInfo"></param>
        public static void SerializeScoreFieldsSetting<T>(T t, string fileName)
        {
            using (FileStream fs = new FileStream(@AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(fs, t);
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <returns></returns>
        public static T DeSerializeScoreFieldsSetting<T>(string fileName)
        {
            T obiBaseInfo;
            using (FileStream fs = new FileStream(@AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName, FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obiBaseInfo = (T)formatter.Deserialize(fs);
            }
            return obiBaseInfo;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="oldValue">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string oldValue)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(oldValue, "MD5");

        }

        private static string KEY = "JIEANKEJI";
        /// <summary>DES加密方法  
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt)
        {
            string sKey;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中    
            //原来使用的UTF8编码，我改成Unicode编码了，不行    
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //C# DES加密解密之建立加密对象的密钥和偏移量 

            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法    
            //使得输入密码必须输入英文文本  
            sKey = KEY.Substring(0, 8);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(
           ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write  the  byte  array  into  the  crypto  stream    
            //(It  will  end  up  in  the  memory  stream)    
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Get  the  data  back  from  the  memory  stream,  and  into  a  string    
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                //Format  as  hex    
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        /// <summary>  DES 解密方法
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string pToDecrypt)
        {
            string sKey = KEY.Substring(0,8);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //Put  the  input  string  into  the  byte  array    
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            // C# DES加密解密之建立加密对象的密钥和偏移量，此值重要，不能修改 

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,
           des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream    
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream    
            //建立StringBuild对象，  
            //CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象    
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            //return "172.17.173.1";
            //return "10.0.40.242";
            //return "192.168.1.18";

            String str;
            String Result = "";
            String hostName = Dns.GetHostName();
            IPAddress[] myIP = Dns.GetHostAddresses(hostName);
            foreach (IPAddress address in myIP)
            {
                str = address.ToString();
                bool blIPV4 = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] >= '0' && str[i] <= '9' || str[i] == '.')
                    {
                        blIPV4 = true;
                    }
                    else
                    {
                        blIPV4 = false;
                        break;
                    }
                }
                if (blIPV4)
                {
                    Result = str;
                }
            }
            return Result;
        }


    }
}
