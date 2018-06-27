using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BonSite.Core.Helper
{
    public class FtpHelper
    {
        public static int FtpUpload(Stream fileStream, string fileName, string ftpServer, string ftpUserID, string ftpPassword)
        {
            #region 初始化信息
            //上传是否成功
            int val = 0;
            //为文件命名,然后保存
            //string fileExtension;
            string ftpServerIP = ftpServer;
            //FileInfo fileInf = new FileInfo(filename);
            //string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            string uri = "ftp://" + ftpServerIP +"/upload/article/video/source"+"/" + fileName;
            FtpWebRequest reqFTP;

            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            //重命名文件的新名称 by minjiang 07-09-05
            reqFTP.RenameTo = fileName;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;

            // 指定执行什么命令

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // 指定数据传输类型
            reqFTP.UseBinary = true;

            // 上传文件时通知服务器文件的大小
            // reqFTP.ContentLength = fileInf.Length;
            reqFTP.ContentLength = fileStream.Length;

            // 缓冲大小设置为2kb
            int buffLength = 2048;

            byte[] buff = new byte[buffLength];
            int contentLen;
            //设置上传进度变量
            //double dProgess = 1.00;
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件

            //FileStream fs = fileInf.OpenRead();

            FtpWebResponse resFTP = null;
            #endregion
            try
            {
                // 把上传的文件写入流

                Stream strm = reqFTP.GetRequestStream();

                // 每次读文件流的2kb
                //contentLen = fs.Read(buff, 0, buffLength);
                contentLen = fileStream.Read(buff, 0, buffLength);

                // 流内容没有结束

                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream
                    strm.Write(buff, 0, contentLen);

                    //contentLen = fs.Read(buff, 0, buffLength);
                    contentLen = fileStream.Read(buff, 0, buffLength);

                }


                // 关闭两个流

                strm.Close();
                //fs.Close();
                fileStream.Close();
                val = 1;
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                if (resFTP != null)
                {
                    string sResFTP = resFTP.StatusCode.ToString();

                }
                val = 0;
            }
            finally
            {
                fileStream.Close();

            }
            return val;

        }
    }
}