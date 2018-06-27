using System;
using System.IO;
using System.Web;
using BonSite.Core;

namespace BonSite.Services
{
    public class SiteUtils
    {
        private static object _locker = new object();//锁对象

        #region 清除所有缓存

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void ClearAllCache()
        {
            BSCache.Clear();
        }

        public static int GetCache()
        {
            return BSCache.GetCache();
        }

        #endregion

        #region  加密/解密

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">加密字符串</param>
        public static string AESEncrypt(string encryptStr)
        {
            return SecureHelper.AESEncrypt(encryptStr, BSConfig.SiteConfig.SecretKey);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">解密字符串</param>
        public static string AESDecrypt(string decryptStr)
        {
            return SecureHelper.AESDecrypt(decryptStr, BSConfig.SiteConfig.SecretKey);
        }

        #endregion

        #region Cookie

        /// <summary>
        /// 获得用户sid
        /// </summary>
        /// <returns></returns>
        public static string GetSidCookie()
        {
            return WebHelper.GetCookie("bssid");
        }

        /// <summary>
        /// 设置用户sid
        /// </summary>
        public static void SetSidCookie(string sid)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["bssid"];
            if (cookie == null)
                cookie = new HttpCookie("bssid");

            cookie.Value = sid;
            cookie.Expires = DateTime.Now.AddDays(15);
            string cookieDomain = BSConfig.SiteConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <returns></returns>
        public static int GetUidCookie()
        {
            return TypeHelper.StringToInt(GetBSCookie("uid"), -1);
        }

        /// <summary>
        /// 设置用户id
        /// </summary>
        public static void SetUidCookie(int uid)
        {
            SetBSCookie("uid", uid.ToString());
        }

        /// <summary>
        /// 获得cookie密码
        /// </summary>
        /// <returns></returns>
        public static string GetCookiePassword()
        {
            return WebHelper.UrlDecode(GetBSCookie("password"));
        }

        /// <summary>
        /// 解密cookie密码
        /// </summary>
        /// <param name="cookiePassword">cookie密码</param>
        /// <returns></returns>
        public static string DecryptCookiePassword(string cookiePassword)
        {
            return AESDecrypt(cookiePassword).Trim();
        }

        /// <summary>
        /// 设置cookie密码
        /// </summary>
        public static void SetCookiePassword(string password)
        {
            SetBSCookie("password", WebHelper.UrlEncode(AESEncrypt(password)));
        }

        /// <summary>
        /// 设置用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="sid">sid</param>
        /// <param name="expires">过期时间</param>
        public static void SetUserCookie(PartUserInfo partUserInfo, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["bs"];
            if (cookie == null)
                cookie = new HttpCookie("bs");

            cookie.Values["uid"] = partUserInfo.UserID.ToString();
            cookie.Values["password"] = WebHelper.UrlEncode(AESEncrypt(partUserInfo.Password));
            cookie.Values["uname"] = WebHelper.UrlEncode(partUserInfo.UserName);
            if (expires > 0)
            {
                cookie.Values["expires"] = expires.ToString();
                cookie.Expires = DateTime.Now.AddDays(expires);
            }
            string cookieDomain = BSConfig.SiteConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetBSCookie(string key)
        {
            return WebHelper.GetCookie2("bs", key);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetBSCookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["bs"];
            if (cookie == null)
                cookie = new HttpCookie("bs");

            cookie[key] = value;

            int expires = TypeHelper.StringToInt(cookie.Values["expires"]);
            if (expires > 0)
                cookie.Expires = DateTime.Now.AddDays(expires);

            string cookieDomain = BSConfig.SiteConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得访问referer
        /// </summary>
        public static string GetRefererCookie()
        {
            string referer = WebHelper.UrlDecode(WebHelper.GetCookie("referer"));
            if (referer.Length == 0)
                referer = "/";
            return referer;
        }

        /// <summary>
        /// 设置访问referer
        /// </summary>
        public static void SetRefererCookie(string url)
        {
            WebHelper.SetCookie("referer", WebHelper.UrlEncode(url));
        }

        /// <summary>
        /// 获得后台访问referer
        /// </summary>
        public static string GetAdminRefererCookie()
        {
            string adminReferer = WebHelper.UrlDecode(WebHelper.GetCookie("adminreferer"));
            if (adminReferer.Length == 0)
                adminReferer = "/admin/home/siteruninfo";
            return adminReferer;
        }

        /// <summary>
        /// 设置后台访问referer
        /// </summary>
        public static void SetAdminRefererCookie(string url)
        {
            WebHelper.SetCookie("adminreferer", WebHelper.UrlEncode(url));
        }

        #endregion

        #region  上传

        /// <summary>
        /// 保存上传的用户头像
        /// </summary>
        /// <param name="avatar">头像</param>
        /// <returns></returns>
        public static string SaveUploadUserAvatar(HttpPostedFileBase avatar)
        {
            if (avatar == null)
                return "-1";

            SiteConfigInfo shopConfig = BSConfig.SiteConfig;

            string fileName = avatar.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
                return "-2";

            int fileSize = avatar.ContentLength;
            if (fileSize > shopConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/user/");
            string newFileName = string.Format("ua_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);
            string[] sizeList = StringHelper.SplitString(shopConfig.UserAvatarThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            avatar.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }

        /// <summary>
        /// 保存上传的用户等级头像
        /// </summary>
        /// <param name="avatar">头像</param>
        /// <returns></returns>
        public static string SaveUploadUserRankAvatar(HttpPostedFileBase avatar)
        {
            if (avatar == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = avatar.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = avatar.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/userrank/");
            string newFileName = string.Format("ura_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);
            string[] sizeList = StringHelper.SplitString(siteConfig.UserRankAvatarThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            avatar.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }

        ///// <summary>
        ///// 保存上传的品牌logo
        ///// </summary>
        ///// <param name="brandLogo">品牌logo</param>
        ///// <returns></returns>
        //public static string SaveUploadBrandLogo(HttpPostedFileBase brandLogo)
        //{
        //    if (brandLogo == null)
        //        return "-1";

        //    ShopConfigInfo shopConfig = BSPConfig.ShopConfig;

        //    string fileName = brandLogo.FileName;
        //    string extension = Path.GetExtension(fileName);
        //    if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, shopConfig.UploadImgType))
        //        return "-2";

        //    int fileSize = brandLogo.ContentLength;
        //    if (fileSize > shopConfig.UploadImgSize)
        //        return "-3";

        //    string dirPath = IOHelper.GetMapPath("/upload/brand/");
        //    string newFileName = string.Format("b_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);
        //    string[] sizeList = StringHelper.SplitString(shopConfig.BrandThumbSize);

        //    string sourceDirPath = dirPath + "source/";
        //    if (!Directory.Exists(sourceDirPath))
        //        Directory.CreateDirectory(sourceDirPath);

        //    string sourcePath = sourceDirPath + newFileName;
        //    brandLogo.SaveAs(sourcePath);

        //    foreach (string size in sizeList)
        //    {
        //        string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
        //        if (!Directory.Exists(thumbDirPath))
        //            Directory.CreateDirectory(thumbDirPath);
        //        string[] widthAndHeight = StringHelper.SplitString(size, "_");
        //        IOHelper.GenerateThumb(sourcePath,
        //                               thumbDirPath + newFileName,
        //                               TypeHelper.StringToInt(widthAndHeight[0]),
        //                               TypeHelper.StringToInt(widthAndHeight[1]),
        //                               "H");
        //    }
        //    return newFileName;
        //}


        /// <summary>
        /// 保存上传的文章题图
        /// </summary>
        /// <param name="articleImage">题图</param>
        /// <returns></returns>
        public static string SaveUplaodArticleImage(HttpPostedFileBase articleImage)
        {
            //使用h5获取或者flash获取数据
            string fileName;
            HttpFileCollection files = null;
            int fileSize;
            if (articleImage == null)
            {
                //使用h5上传文件
                files = System.Web.HttpContext.Current.Request.Files;
                fileName = files[0].FileName;
                fileSize = files[0].ContentLength;
            }
            else
            {
                fileName = articleImage.FileName;
                fileSize = articleImage.ContentLength;
            }
            SiteConfigInfo siteConfig = BSConfig.SiteConfig;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";


            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/article/show/");
            string name = "ps_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
            string newFileName = name + extension;
            string[] sizeList = StringHelper.SplitString(siteConfig.ArticleImgThumbSize);

            string sourceDirPath = string.Format("{0}source/", dirPath);
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;

            if (articleImage == null)
            {
                files[0].SaveAs(sourcePath);
            }
            else
            {
                articleImage.SaveAs(sourcePath);
            }


            if (siteConfig.WatermarkType == 1)//文字水印
            {
                string path = string.Format("{0}{1}_text{2}", sourceDirPath, name, extension);
                IOHelper.GenerateTextWatermark(sourcePath, path, siteConfig.WatermarkText, siteConfig.WatermarkTextSize, siteConfig.WatermarkTextFont, siteConfig.WatermarkPosition, siteConfig.WatermarkQuality);
                sourcePath = path;
            }
            else if (siteConfig.WatermarkType == 2)//图片水印
            {
                string path = string.Format("{0}{1}_img{2}", sourceDirPath, name, extension);
                string watermarkPath = IOHelper.GetMapPath("/watermarks/" + siteConfig.WatermarkImg);
                IOHelper.GenerateImageWatermark(sourcePath, watermarkPath, path, siteConfig.WatermarkPosition, siteConfig.WatermarkImgOpacity, siteConfig.WatermarkQuality);
                sourcePath = path;
            }

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }

        /// <summary>
        /// 保存上传的文章视频
        /// </summary>
        /// <param name="articleImage">视频</param>
        /// <returns></returns>
        public static string SaveUplaodArticleVideo(HttpPostedFileBase articleVideo)
        {
            if (articleVideo == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = articleVideo.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            //|| !CommonHelper.IsInArray(extension, siteConfig.UploadVideoType)
            if (!ValidateHelper.IsVideoFileName(fileName))
                return "-2";

            //int fileSize = articleVideo.ContentLength;
            //if (fileSize > siteConfig.UploadVideoSize)
            //    return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/article/video/");
            string name = "ps_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
            string newFileName = name + extension;


            string sourceDirPath = string.Format("{0}source/", dirPath);
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            articleVideo.SaveAs(sourcePath);

            if (siteConfig.WatermarkType == 1)//文字水印
            {
                string path = string.Format("{0}{1}_text{2}", sourceDirPath, name, extension);
                IOHelper.GenerateTextWatermark(sourcePath, path, siteConfig.WatermarkText, siteConfig.WatermarkTextSize, siteConfig.WatermarkTextFont, siteConfig.WatermarkPosition, siteConfig.WatermarkQuality);
                sourcePath = path;
            }
            else if (siteConfig.WatermarkType == 2)//图片水印
            {
                string path = string.Format("{0}{1}_img{2}", sourceDirPath, name, extension);
                string watermarkPath = IOHelper.GetMapPath("/watermarks/" + siteConfig.WatermarkImg);
                IOHelper.GenerateImageWatermark(sourcePath, watermarkPath, path, siteConfig.WatermarkPosition, siteConfig.WatermarkImgOpacity, siteConfig.WatermarkQuality);
                sourcePath = path;
            }
            return newFileName;
        }

        /// <summary>
        /// 保存新闻编辑器中的图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static string SaveArticleEditorImage(HttpPostedFileBase image, int width1, int wartertype)
        {
            if (image == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            string[] imgTypeList = StringHelper.SplitString(BSConfig.SiteConfig.UploadImgType, "@");
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, imgTypeList[0]))
                return "-2";

            int fileSize = image.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/");
            string name = "a_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
            string newFileName = name + extension;

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            image.SaveAs(sourcePath);

            //if (siteConfig.WatermarkType == 1)//文字水印

            //如果是gif图片，不能添加水印


            if (wartertype == 1)//文字水印
            {
                string path = string.Format("{0}{1}_text{2}", sourceDirPath, name, extension);
                IOHelper.GenerateTextWatermark(sourcePath, path, siteConfig.WatermarkText, siteConfig.WatermarkTextSize, siteConfig.WatermarkTextFont, siteConfig.WatermarkPosition, siteConfig.WatermarkQuality);
                sourcePath = path;
            }
            else if (wartertype == 2)//图片水印
            {
                string path = string.Format("{0}{1}_img{2}", sourceDirPath, name, extension);
                string watermarkPath = IOHelper.GetMapPath("/Content/" + siteConfig.WatermarkImg);
                IOHelper.GenerateImageWatermark(sourcePath, watermarkPath, path, siteConfig.WatermarkPosition, siteConfig.WatermarkImgOpacity, siteConfig.WatermarkQuality);
                sourcePath = path;
            }

            string thumbDirPath = dirPath + "thumb/";
            if (!Directory.Exists(thumbDirPath))
                Directory.CreateDirectory(thumbDirPath);

            string isextension = sourcePath.Substring(sourcePath.LastIndexOf(".")).ToLower();
            if (isextension.Contains(".gif"))
            {
                //保存图片
                image.SaveAs(thumbDirPath+ newFileName);
                return newFileName;
            }

            if (width1 == -1)
            {
                IOHelper.GenerateThumb(sourcePath,
                                  thumbDirPath + newFileName,
                                  -1,
                                  -1,
                                  "HW");
            }
            else
            {

                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       width1,
                                       -1,
                                       "W");

            }
            return newFileName;
        }

        /// <summary>
        /// 保存新闻编辑器中的文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public static string SaveArticleEditorFile(HttpPostedFileBase file)
        {
            if (file == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!CommonHelper.IsInArray(extension, siteConfig.UploadAttType))
                return "-2";

            int fileSize = file.ContentLength;
            if (fileSize > siteConfig.UploadAttSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/file/");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            file.SaveAs(dirPath + fileName);

            return fileName;
        }





        ///// <summary>
        ///// 保存帮助编辑器中的图片
        ///// </summary>
        ///// <param name="image">图片</param>
        ///// <returns></returns>
        //public static string SaveHelpEditorImage(HttpPostedFileBase image)
        //{
        //    if (image == null)
        //        return "-1";

        //    SiteConfigInfo siteConfig = BSConfig.SiteConfig;

        //    string fileName = image.FileName;
        //    string extension = Path.GetExtension(fileName);
        //    if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
        //        return "-2";

        //    int fileSize = image.ContentLength;
        //    if (fileSize > siteConfig.UploadImgSize)
        //        return "-3";

        //    string dirPath = IOHelper.GetMapPath("/upload/help/");
        //    string name = "h_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
        //    string newFileName = name + extension;

        //    string sourceDirPath = dirPath + "source/";
        //    if (!Directory.Exists(sourceDirPath))
        //        Directory.CreateDirectory(sourceDirPath);

        //    string sourcePath = sourceDirPath + newFileName;
        //    image.SaveAs(sourcePath);

        //    if (siteConfig.WatermarkType == 1)//文字水印
        //    {
        //        string path = string.Format("{0}{1}_text{2}", sourceDirPath, name, extension);
        //        IOHelper.GenerateTextWatermark(sourcePath, path, siteConfig.WatermarkText, siteConfig.WatermarkTextSize, siteConfig.WatermarkTextFont, siteConfig.WatermarkPosition, siteConfig.WatermarkQuality);
        //        sourcePath = path;
        //    }
        //    else if (siteConfig.WatermarkType == 2)//图片水印
        //    {
        //        string path = string.Format("{0}{1}_img{2}", sourceDirPath, name, extension);
        //        string watermarkPath = IOHelper.GetMapPath("/watermarks/" + siteConfig.WatermarkImg);
        //        IOHelper.GenerateImageWatermark(sourcePath, watermarkPath, path, siteConfig.WatermarkPosition, siteConfig.WatermarkImgOpacity, siteConfig.WatermarkQuality);
        //        sourcePath = path;
        //    }

        //    string thumbDirPath = dirPath + "thumb/";
        //    if (!Directory.Exists(thumbDirPath))
        //        Directory.CreateDirectory(thumbDirPath);
        //    IOHelper.GenerateThumb(sourcePath,
        //                           thumbDirPath + newFileName,
        //                           -1,
        //                           -1,
        //                           "HW");

        //    return newFileName;
        //}

        ///// <summary>
        ///// 保存帮助编辑器中的文件
        ///// </summary>
        ///// <param name="file">文件</param>
        ///// <returns></returns>
        //public static string SaveHelpEditorFile(HttpPostedFileBase file)
        //{
        //    if (file == null)
        //        return "-1";

        //    SiteConfigInfo siteConfig = BSConfig.SiteConfig;

        //    string fileName = file.FileName;
        //    string extension = Path.GetExtension(fileName);
        //    if (!CommonHelper.IsInArray(extension, siteConfig.UploadAttType))
        //        return "-2";

        //    int fileSize = file.ContentLength;
        //    if (fileSize > siteConfig.UploadAttSize)
        //        return "-3";

        //    string dirPath = IOHelper.GetMapPath("/upload/help/file/");
        //    if (!Directory.Exists(dirPath))
        //        Directory.CreateDirectory(dirPath);
        //    file.SaveAs(dirPath + fileName);

        //    return fileName;
        //}



        /// <summary>
        /// 保存上传的banner图片
        /// </summary>
        /// <param name="bannerImg">banner图片</param>
        /// <returns></returns>
        public static string SaveUploadBannerImg(HttpPostedFileBase bannerImg)
        {
            if (bannerImg == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = bannerImg.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = bannerImg.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/banner/");
            string newFileName = string.Format("bn_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string path = dirPath + newFileName;
            bannerImg.SaveAs(path);

            return newFileName;
        }

        /// <summary>
        /// 保存上传的广告主体
        /// </summary>
        /// <param name="advertBody">广告主体</param>
        /// <returns></returns>
        public static string SaveUploadAdvertBody(HttpPostedFileBase advertBody)
        {
            if (advertBody == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = advertBody.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = advertBody.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/advert/");
            string newFileName = string.Format("ad_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string path = dirPath + newFileName;
            advertBody.SaveAs(path);

            return newFileName;
        }

        /// <summary>
        /// 保存上传的友情链接Logo
        /// </summary>
        /// <param name="friendLinkLogo">友情链接logo</param>
        /// <returns></returns>
        public static string SaveUploadFriendLinkLogo(HttpPostedFileBase friendLinkLogo)
        {
            if (friendLinkLogo == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = friendLinkLogo.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = friendLinkLogo.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/friendlink/");
            string newFileName = string.Format("fr_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名
            string[] sizeList = StringHelper.SplitString(siteConfig.FriendLinkThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            friendLinkLogo.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }


        /// <summary>
        /// 保存上传的栏目管理照片
        /// </summary>

        /// <returns></returns>
        public static string SaveUploadArticleClassLogo(HttpPostedFileBase articleClassLogo)
        {
            if (articleClassLogo == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = articleClassLogo.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = articleClassLogo.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/articleclass/");
            string newFileName = string.Format("fr_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名
            string[] sizeList = StringHelper.SplitString(siteConfig.ArticleClassThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            articleClassLogo.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }


        /// <summary>
        /// 保存上传的专题图片
        /// </summary>
        /// <param name="specialImg">专题图片</param>
        /// <returns></returns>
        public static string SaveUploadSpecialImg(HttpPostedFileBase specialImg)
        {
            if (specialImg == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = specialImg.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = specialImg.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/special/");
            string newFileName = string.Format("si_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名
            string[] sizeList = StringHelper.SplitString(siteConfig.SpecialImgThumbSize);

            string sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            string sourcePath = sourceDirPath + newFileName;
            specialImg.SaveAs(sourcePath);

            foreach (string size in sizeList)
            {
                string thumbDirPath = string.Format("{0}thumb{1}/", dirPath, size);
                if (!Directory.Exists(thumbDirPath))
                    Directory.CreateDirectory(thumbDirPath);
                string[] widthAndHeight = StringHelper.SplitString(size, "_");
                IOHelper.GenerateThumb(sourcePath,
                                       thumbDirPath + newFileName,
                                       TypeHelper.StringToInt(widthAndHeight[0]),
                                       TypeHelper.StringToInt(widthAndHeight[1]),
                                       "H");
            }
            return newFileName;
        }


        public static string SaveUploadShopImg(HttpPostedFileBase shopImg)
        {
            if (shopImg == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = shopImg.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!ValidateHelper.IsImgFileName(fileName) || !CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = shopImg.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/shop/");
            string newFileName = string.Format("sp_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string path = dirPath + newFileName;
            shopImg.SaveAs(path);

            return newFileName;
        }



        public static string SavePluginsImg(HttpPostedFileBase file)
        {
            if (file == null)
                return "-1";

            SiteConfigInfo siteConfig = BSConfig.SiteConfig;

            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (!CommonHelper.IsInArray(extension, siteConfig.UploadImgType))
                return "-2";

            int fileSize = file.ContentLength;
            if (fileSize > siteConfig.UploadImgSize)
                return "-3";

            string dirPath = IOHelper.GetMapPath("/upload/plugins/");
            string newFileName = string.Format("plugins_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), extension);//生成文件名


            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            file.SaveAs(dirPath + newFileName);

            return newFileName;
        }


        public static string SavePluginsImg(string baseFile)
        {
            try
            {
                string dirPath = IOHelper.GetMapPath("/upload/plugins/");
                string newFileName = string.Format("plugins_{0}{1}", DateTime.Now.ToString("yyMMddHHmmssfffffff"), ".jpg");//生成文件名

                byte[] bytes = Convert.FromBase64String(baseFile);

                Stream s = new FileStream(dirPath + newFileName, FileMode.Append);

                s.Write(bytes, 0, bytes.Length);

                s.Close();

                return newFileName;

            }
            catch (Exception)
            {

                throw;
                return "";
            }
        }

        #endregion

        #region  日志

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input">输入内容</param>
        public static void WriteLogFile(Exception ex)
        {
            WriteLogFile(string.Format("方法:{0},异常信息:{1}", ex.TargetSite, ex.Message));
        }

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="input">输入内容</param>
        public static void WriteLogFile(string input)
        {
            lock (_locker)
            {
                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    string fileName = IOHelper.GetMapPath("/App_Data/exlogs/") + DateTime.Now.ToString("yyyyMMdd") + ".log";

                    FileInfo fileInfo = new FileInfo(fileName);
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }
                    if (!fileInfo.Exists)
                    {
                        fileInfo.Create().Close();
                    }
                    else if (fileInfo.Length > 2048)
                    {
                        fileInfo.Delete();
                    }

                    fs = fileInfo.OpenWrite();
                    sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.Write("Log Entry : ");
                    sw.Write("{0}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
                    sw.Write(Environment.NewLine);
                    sw.Write(input);
                    sw.Write(Environment.NewLine);
                    sw.Write("------------------------------------");
                    sw.Write(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Flush();
                        sw.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }
        }

        #endregion
    }
}
