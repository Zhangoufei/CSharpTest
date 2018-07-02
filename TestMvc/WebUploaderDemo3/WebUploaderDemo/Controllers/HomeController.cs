using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebUploaderDemo.Controllers
{
    public class HomeController : Controller
    {
        public static List<FileDataInfo> fileInfoList = new List<FileDataInfo>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }
        public ActionResult FileUpload()
        {
            string fileDataJson = Request.Params["fileDataJson"];//文件信息json

            FileDataInfo fileData = JsonHelper.DeserializeJsonToObject<FileDataInfo>(fileDataJson);//文件信息实体
            fileInfoList.Add(fileData);

            HttpPostedFileBase file = Request.Files["file"];
            string tempPath = IOHelper.GetMapPath("/upload/TempFile/");//临时保存路径
            string filePath = IOHelper.GetMapPath("/upload/Files/");//保存路径


            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(fileData.fileName);//最终保存路劲 
            string ZSFilePath = filePath + filename;
            string tempFilePath = tempPath + fileData.fileName;//临时保存路径

            if (!System.IO.File.Exists(tempFilePath))//如果文件不存在则保存一个
            {
                if (fileData.fileSize == fileData.fileEnd)
                {
                    file.SaveAs(ZSFilePath);
                }
                else
                {
                    IsFileInUse(tempFilePath);
                    file.SaveAs(tempFilePath);
                }
                return Content("{\"result\":\"True\",\"msg\":\"\"}");
            }
            #region 开始保存文件到服务器
            try
            {
                IsFileInUse(tempFilePath);
                using (FileStream fStream = new FileStream(tempFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    //偏移指针
                    fStream.Seek(fileData.fileStart, SeekOrigin.Begin);
                    //从客户端的请求中获取文件流
                    using (BinaryReader bReader = new BinaryReader(file.InputStream))
                    {
                        long upLoadLength = file.InputStream.Length;//文件的长度
                        byte[] data = new byte[upLoadLength];
                        bReader.Read(data, 0, (int)upLoadLength);//读取上传文件
                        fStream.Write(data, 0, (int)upLoadLength);//将读取的文件写入服务器   
                        bReader.Dispose();
                        bReader.Close();
                    }
                    fStream.Dispose();
                    fStream.Close();
                }
            }
            catch (Exception)
            {
                System.IO.FileInfo fileInfo = new FileInfo(tempFilePath);
                fileInfo.Delete();//删除源文件
                throw;
            }
            #endregion
            //转移文件
            if (fileData.fileSize == fileData.fileEnd)//判断文件大小是不是等于文件结束大小
            {
                System.IO.FileInfo fileInfo = new FileInfo(tempFilePath);
                fileInfo.CopyTo(ZSFilePath);//另存为正式目录
                fileInfo.Delete();//删除源文件
            }
            return Content(filename);
        }
        public void IsFileInUse(string fileName)
        {
            bool inUse = true;
            for (int i = 0; i >= 0; i++)
            {
                FileStream fs = null;
                try
                {
                    if (System.IO.File.Exists(fileName))//如果文件不存在则保存一个
                    {
                        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    }
                    inUse = false;
                }
                catch { }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
                if (inUse)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
