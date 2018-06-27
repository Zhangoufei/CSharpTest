using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using System.IO;
using BonSite.Core.Helper;
using BonSite.Web.Admin.Models;

namespace BonSite.Web.Admin.Controllers
{
    public class ToolController : Controller
    {
        //
        // GET: /Tool/
        //工作上下文
        public AdminWorkContext WorkContext = new AdminWorkContext();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 上传新闻编辑器中的图片
        /// </summary>
        /// <returns>-1代表图片为空，-2代表图片类型错误，-3代表图片太大</returns>
        public ActionResult UploadArticleEditorImage(int? width, string haveWatermark)
        {
            int width1 = 0;
            int isWatermaerk = 0;
            if (width == null)
            {
                width1 = -1;
            }
            else
            {
                width1 = (int)width;
            }
            if (!string.IsNullOrEmpty(haveWatermark))
            {
                if (haveWatermark == "true")
                {
                    isWatermaerk = 2;
                }
                width1 = -1;
            }
            else
            {
                width1 = (int)width;
            }
            HttpPostedFileBase image = ControllerContext.RequestContext.HttpContext.Request.Files[0];
            string result = SiteUtils.SaveArticleEditorImage(image, width1, isWatermaerk);

            //获取图片描述
            string title = ControllerContext.RequestContext.HttpContext.Request.Form["pictitle"];

            string state = "SUCCESS";
            string url = "";
            if (result == "-1")
            {
                state = "上传图片不能为空";
            }
            else if (result == "-2")
            {
                state = "不允许的图片类型";
            }
            else if (result == "-3")
            {
                state = "图片大小超出网站限制";
            }
            else
            {
                url = result;

            }

            string oriName = "";
            //获取原始文件名
            if (ControllerContext.RequestContext.HttpContext.Request.Form["fileName"] != null)
            {
                oriName = ControllerContext.RequestContext.HttpContext.Request.Form["fileName"].Split(',')[1];
            }
            AddLog(title, state, oriName);
            return Content(string.Format("{4}'url':'upload/thumb/{0}','title':'{1}','original':'{2}','state':'{3}'{5}", url, title, oriName, state, "{", "}"));
        }

        private void AddLog(string title, string state, string oriName)
        {
            string clientIp = Request.ServerVariables.Get("Remote_Addr").ToString();
            CommonLog.AddCommonnLog(WorkContext.UserName, state + ":" + oriName + ":" + title, title, clientIp);
        }


        /// <summary>
        /// 上传新闻编辑器中的文件
        /// </summary>
        /// <returns>-1代表文件为空，-2代表文件类型错误，-3代表文件太大</returns>
        public ActionResult UplaodArticleEditorFile()
        {
            HttpPostedFileBase file = ControllerContext.RequestContext.HttpContext.Request.Files[0];
            string result = SiteUtils.SaveArticleEditorFile(file);

            string state = "SUCCESS";
            string url = "";
            if (result == "-1")
            {
                state = "上传文件不能为空";
            }
            else if (result == "-2")
            {
                state = "不允许的文件类型";
            }
            else if (result == "-3")
            {
                state = "文件大小超出网站限制";
            }
            else
            {
                url = result;
            }

            //获取图片描述
            string title = ControllerContext.RequestContext.HttpContext.Request.Form["pictitle"];
            //获取原始文件名
            string oriName = ControllerContext.RequestContext.HttpContext.Request.Form["fileName"];
            //ThumbnailController.GetImage(300, 200);
            AddLog(WorkContext.UserName, state + ":" + oriName + ":" + title, "上传新闻编辑器中的文件");
            return Content(string.Format("{4}'url':'upload/file/{0}','title':'{1}','original':'{2}','state':'{3}'{5}", url, title, oriName, state, "{", "}"));
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="productImage">图片</param>
        /// <returns></returns>
        public ActionResult UploadArticleImage(HttpPostedFileBase articleImage)
        {
            string result = SiteUtils.SaveUplaodArticleImage(articleImage);
            AddLog(WorkContext.UserName, result, "上传图片");
            return Content(result);
        }
        //上传视频
        public ActionResult UploadArticleVideo2(HttpPostedFileBase articleVideo)
        {
            //获取ftp上传地址，用户名和密码
            string ftpIp = WorkContext.SiteConfig.ftpIp;
            string ftpUser = WorkContext.SiteConfig.ftpUser;
            string ftpPwd = WorkContext.SiteConfig.ftpPwd;

            Stream videoStream = null;
            string fileName = "";
            if (articleVideo != null)
            {
                //使用flash控件上传
                videoStream = articleVideo.InputStream;
                fileName = articleVideo.FileName;
            }
            else
            {
                //使用h5上传
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                videoStream = files[0].InputStream;
                fileName = files[0].FileName;
            }
            string name = "ps_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
            string extension = Path.GetExtension(fileName).ToLower();
            string newFileName = name + extension;
            int result = FtpHelper.FtpUpload(videoStream, newFileName, ftpIp, ftpUser, ftpPwd);
            string tempResult = string.Empty;
            if (result > 0)
            {
                tempResult = "上传成功";
            }
            else
            {
                tempResult = "上传失败";
            }
            AddLog(WorkContext.UserName, tempResult + ":" + fileName + "", "上传视频");
            return Content(newFileName);
        }

        /// <summary>
        /// 上传大视频，分段上传
        /// </summary>
        /// <param name="articleVideo"></param>
        /// <returns></returns>
        public ActionResult UploadArticleVideo(HttpPostedFileBase articleVideo)
        {
            string fileDataJson = Request.Params["fileDataJson"];//文件信息json

            HttpPostedFileBase file = Request.Files["file"];

            FileDataInfo fileData = JsonHelper.DeserializeJsonToObject<FileDataInfo>(fileDataJson);//文件信息实体

            string tempPath = IOHelper.GetMapPath("/upload/article/video/tempFile/");//临时保存路径


            string filePath = IOHelper.GetMapPath("/upload/article/video/source/");//保存路径

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(fileData.fileName);//最终保存路劲


            string ZSFilePath = filePath + newFileName;

            string tempFilePath = tempPath + fileData.fileName;//临时保存路径

            bool result = false;
            CommonHelper.SaveFileToServer(tempFilePath, ZSFilePath, fileData, file, ref result);
            if (result)
            {
                AddLog(WorkContext.UserName, "上传成功" + ":" + ZSFilePath + "", "上传2g以下视频");
            }
            return Content(newFileName);

        }
        /// <summary>
        /// 上传友情链接logo
        /// </summary>
        /// <param name="friendLinkLogo">友情链接logo</param>
        /// <returns></returns>
        public ActionResult UploadFriendLinkLogo(HttpPostedFileBase friendLinkLogo)
        {
            string result = SiteUtils.SaveUploadFriendLinkLogo(friendLinkLogo);
            return Content(result);

        }
        /// <summary>
        /// 上传栏目管理logo
        /// </summary>
        /// <param name="friendLinkLogo">栏目管理logo</param>
        /// <returns></returns>
        public ActionResult UploadArticleClassLogo(HttpPostedFileBase articleClassLogo)
        {
            string result = SiteUtils.SaveUploadArticleClassLogo(articleClassLogo);
            return Content(result);

        }

        /// <summary>
        /// 上传专题图片
        /// </summary>
        /// <param name="specialimg"></param>
        /// <returns></returns>
        public ActionResult UploadSpecialImg(HttpPostedFileBase specialimg)
        {
            string result = SiteUtils.SaveUploadSpecialImg(specialimg);
            return Content(result);
        }

        public ActionResult uploadAdvertBody(HttpPostedFileBase advertBody)
        {
            string result = SiteUtils.SaveUploadAdvertBody(advertBody);
            return Content(result);

        }

        /// <summary>
        /// 上传banner图片
        /// </summary>
        /// <param name="bannerImg">banner图片</param>
        /// <returns></returns>
        public ActionResult UploadBannerImg(HttpPostedFileBase bannerImg)
        {
            string result = SiteUtils.SaveUploadBannerImg(bannerImg);
            return Content(result);

        }

        /// <summary>
        /// 上传店铺照片
        /// </summary>
        /// <param name="shopImg"></param>
        /// <returns></returns>
        public ActionResult UploadShopImage(HttpPostedFileBase shopImage)
        {
            string result = SiteUtils.SaveUploadShopImg(shopImage);
            return Content(result);
        }
    }
}
