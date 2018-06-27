using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin;
using BonSite.Web.Admin.Models;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using BonSite.Core.Domain.Site;

namespace BonSite.Web.Admin.Controllers
{
    /// <summary>
    /// Author WL
    /// </summary>
    public class SourceAdminController : BaseAdminController
    {
        //全局变量
        public static ArticleListModel modelConst = new ArticleListModel();

        #region 列表
        public ActionResult List(string articleTitle, string Keyword, string sortColumn, string sortDirection, int? articleClassId1, int pageSize = 10, int pageNumber = 1)
        {
            //获取当前登录用户信息
            string ck = WebHelper.GetCookie("bs", "uname");
            PartUserInfo partUserInfo = Users.GetPartUserByName(ck);
            string condition2 = "";
            ////微信编辑员不显示群发按钮
            //if (partUserInfo.AdminGroupID == WorkContext.SiteConfig.wxbj)
            //{
            //    ViewData["weixinadmin"]="display:none";
            //}
            //else
            //{
            //    ViewData["weixinadmin"] = "";
            //}
            if (!partUserInfo.AdminGroupID.Equals(WorkContext.SiteConfig.wzAdmin) && !partUserInfo.AdminGroupID.Equals(WorkContext.SiteConfig.wxAdmin))
            {
                condition2 = " and AdminID=" + partUserInfo.UserID;
                ViewData["weixinadmin"] = "display:none";
            }
            else
            {
                ViewData["weixinadmin"] = "";
            }
            pageSize = WorkContext.SiteConfig.pageSize;
            int articleClassId = 0;

            if (articleClassId1 == null)
            {
                articleClassId = 242;
            }
            else
            {
                articleClassId = (int)articleClassId1;
            }



            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(articleClassId);

            string condition = Article.AdminGetArticleListCondition(articleClassId, articleTitle);

            //string condition = "";
            //PageModel pageModel = new PageModel(pageSize, pageNumber, BonSite.Services.ArticleClass.AdminGetArticleClassTreeList().ToList().Count);
            //ArticleClassListmodel model = new ArticleClassListmodel
            //{
            //    DataInfoList = BonSite.Services.ArticleClass.AdminGetArticleClassTreeList().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
            //    PageModel = pageModel
            //};


            //string condition1 = condition + @" or [Keyword] = 4  or [ArticleClassID] in ( select ArticleClassID from bs_ArticleClass where IsWeb = 1 ) ";
            string condition1 = " ([DisplayType] in (0,2) " + " and ApprovalStatus=1 or ArticleClassID=242) " + condition2;
            string sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition1));

            ArticleListModel model = new ArticleListModel()
            {
                DataList = Article.AdminGetArticleList(pageModel.PageSize, pageModel.PageNumber, condition1, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ArticleClassID = articleClassId,
                ArticleTitle = articleTitle,
                ClassInfo = classInfo
            };
            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleClassId);
            ViewData["classPath"] = classPath;
            modelConst = model;
            SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&articleClassId={5}&newsTitle={6}",
                                                          Url.Action("List"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          articleClassId,
                                                          articleTitle));
            //当前管理员id
            ViewData["adminid"] = partUserInfo.UserID;
            //当前管理员角色
            ViewData["AdminGroupID"] = partUserInfo.AdminGroupID;
            //微信管理员id
            ViewData["wxAdmin"] = WorkContext.SiteConfig.wxAdmin;
            return View(model);
        }

        #endregion

        #region 微信群发

        /// <summary>
        /// 微信群发
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public string WeChatSend(string articleID)
        {
            #region 获取数据

            int articleIDInt = articleID == null ? 0 : Int32.Parse(articleID);
            ArticleInfo articleInfo = Article.GetModelByArticleID(articleIDInt);

            ArticleModel model = new ArticleModel()
            {
                ArticleClassID = articleInfo.ArticleClassID,
                Title = articleInfo.Title,
                SpecialID = articleInfo.SpecialID,
                DisplayType = articleInfo.DisplayType,
                Url = articleInfo.Url,
                Digest = articleInfo.Digest,
                ImgUrl = articleInfo.ImgUrl,
                Body = articleInfo.Body,
                Author = articleInfo.Author,
                ComeForm = articleInfo.ComeForm,
                IsShow = articleInfo.IsShow,
                IsHome = articleInfo.IsHome,
                IsBest = articleInfo.IsBest,
                IsTop = articleInfo.IsTop,
                AddTime = articleInfo.AddTime,
                Keyword = articleInfo.Keyword,
                Description = articleInfo.Description
            };
            //没有摘要读取文章前30个字
            if (string.IsNullOrEmpty(model.Digest))
            {
                string ddd1 = BonSite.Core.CommonHelper.DelHTML(model.Body);
                model.Digest = BonSite.Core.StringHelper.SubString(ddd1, 30);
            }
            #endregion

            #region 获取access_token

            //获取access_token
            string access_token = WeiXinGetFunction();

            #endregion

            #region 上传图片，获取mediaID

            //获取mediaID
            string strImg = System.AppDomain.CurrentDomain.BaseDirectory + @"upload\article\show\source\" + model.ImgUrl;
            if (string.IsNullOrEmpty(model.ImgUrl))
            {
                string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(articleInfo.Body);
                if (ImgUrls.Length > 0)
                {
                    if (ImgUrls[0].Contains("http"))
                    {
                        strImg = ImgUrls[0];
                    }
                    else
                    {
                        strImg = System.AppDomain.CurrentDomain.BaseDirectory + ImgUrls[0];
                    }

                }
            }
            else if (model.ImgUrl.Contains("upload"))
            {
                strImg = System.AppDomain.CurrentDomain.BaseDirectory + model.ImgUrl;
            }


            string mediaID = UploadMultiMedia(access_token, strImg, "image");
            if (mediaID == "0")
                return "请检查图片问题";

            #endregion

            #region 上传消息

            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=" + access_token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";

            //string content = model.Body;
            string strCon = string.Empty;
            //strCon = @"<p>微信群发1</p><p><img title='' src=" + '"' + "http://mmbiz.qpic.cn/mmbiz_png/liaG8uPvg9bsT5M9Pib1DfaIuPdGfeSR1f6VHCFyIV1LtXQ8OjbfF2pfl1M1zKIjsrKAmURlicuPuwZ8zC3DZQPcQ/0" + '"' + @"/></p><p>微信群发1111</p>";

            #region 正则表达式

            StringBuilder sb = new StringBuilder();
            string cotent = model.Body;
            sb.Append(cotent);
            //Regex reg = new Regex("<img([^\\/>src]+)src=\"([^\"]+)\"([^\\/]*)\\/?>");
            //string strReg = @"<img\s*src\s*=\s*[""']?\s*(?[^\s""'<>]*)\s*/?\s*>";
            Regex reg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>");
            MatchCollection mats = reg.Matches(cotent);
            Regex reg1 = null;
            string str;
            foreach (Match mat in mats)
            {
                str = mat.Value;
                reg1 = new Regex("src=\"([^\"]+)\"");
                string img = reg1.Match(str).Groups[1].ToString();
                if (img.Contains("http"))
                {
                    img = SaveImageFromWeb(img, System.AppDomain.CurrentDomain.BaseDirectory + @"upload\thumb\", "w_" + DateTime.Now.ToString("yyyymmddhhmmss"));
                }
                string filepath = System.AppDomain.CurrentDomain.BaseDirectory + img;
                string strImageUrl = GetImageUrl(access_token, filepath);
                sb.Replace(img, strImageUrl);
            }

            #endregion

            strCon = sb.ToString();

            //文章里图片上传到微信服务器中，获取Url
            //string filepath = System.AppDomain.CurrentDomain.BaseDirectory + @"upload\article\show\source\" + model.ImgUrl;
            //string strImageUrl = GetImageUrl(access_token, filepath);

            //string str11 = @"[{ ""thumb_media_id"":""l58eHP-aY3cpMPldZapGDwLfbTaHomSJbB-SiCzZB7QyvdWAj9QoKcNiVjN-Lzd1"",""author"":""xxx"",""title"":""Have a Happy Day"",""content_source_url"":""www.qq.com"",""content"":""Have a Happy Day"",""digest"":""Have a Happy Day""}]";
            string str22 = @" ""thumb_media_id"":""{0}"",""author"":""{1}"",""title"":""{2}"",""content_source_url"":""{3}"",""content"":""{4}"",""digest"":""{5}""";
            //没有摘要读取文章前30个字
            if (string.IsNullOrEmpty(model.Digest))
            {
                string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                model.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
            }
            string commandText = string.Format(str22,
                                                mediaID,
                                                model.Author,
                                                model.Title,
                                                "",
                                                model.Body,
                                                model.Digest);

            string strContent = @"{ ""articles"": [{" + commandText + "}]}";

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            string media_id = jo["media_id"].ToString();

            string type = jo["type"].ToString();

            #endregion

            #region 预览

            //Preview(access_token, media_id);
            //return "预览成功";

            #endregion

            #region 群发

            string serviceAddress1 = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + access_token;
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(serviceAddress1);
            request1.Method = "POST";
            request1.ContentType = "application/json";

            string str1 = @"{ ""is_to_all"":false,""tag_id"":101}";
            string str2 = @"{ ""media_id"": " + '"' + media_id + '"' + "}";

            string str3 = @"{ ""filter"": " + str1 + ",";
            string str4 = '"' + "mpnews" + '"' + ":" + str2 + ",";
            string str5 = @"""msgtype"":""mpnews"",""send_ignore_reprint"":0}";

            string strContent1 = str3 + str4 + str5;

            using (StreamWriter dataStream = new StreamWriter(request1.GetRequestStream()))
            {
                dataStream.Write(strContent1);
                dataStream.Close();
            }
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            string encoding1 = response1.ContentEncoding;
            if (encoding1 == null || encoding1.Length < 1)
            {
                encoding1 = "UTF-8"; //默认编码  
            }
            StreamReader reader1 = new StreamReader(response1.GetResponseStream(), Encoding.GetEncoding(encoding1));
            string retString1 = reader1.ReadToEnd();

            #endregion

            #region 预览

            //Preview(access_token, media_id);

            #endregion

            return "发送成功";
        }

        public string WeChatSend(string[] parmArticles, string tagID, string typeID)
        {
            #region 获取access_token
            //获取access_token
            string appID = string.Empty;
            string secret = string.Empty;
            if (!string.IsNullOrEmpty(tagID))
            {
                string[] parm = tagID.Split('@');
                if (parm.Length > 0)
                {
                    appID = parm[0];
                    secret = parm[1];
                }
            }

            //客服接口
            if (typeID == "1")
            {
                return WeChatKeFuSend(parmArticles, tagID);
            }

            string access_token = WeiXinGetFunction(appID, secret);
            #endregion

            //#region 获取数据

            int articleIDInt = 0;

            articleIDInt = parmArticles[0] == null ? 0 : Int32.Parse(parmArticles[0]);
            ArticleInfo articleInfo1 = Article.GetModelByArticleID(articleIDInt);

            ArticleModel model1 = new ArticleModel()
            {
                ArticleClassID = articleInfo1.ArticleClassID,
                Title = articleInfo1.Title,
                SpecialID = articleInfo1.SpecialID,
                DisplayType = articleInfo1.DisplayType,
                Url = articleInfo1.Url,
                Digest = articleInfo1.Digest,
                ImgUrl = articleInfo1.ImgUrl,
                Body = articleInfo1.Body,
                Author = articleInfo1.Author,
                ComeForm = articleInfo1.ComeForm,
                IsShow = articleInfo1.IsShow,
                IsHome = articleInfo1.IsHome,
                IsBest = articleInfo1.IsBest,
                IsTop = articleInfo1.IsTop,
                AddTime = articleInfo1.AddTime,
                Keyword = articleInfo1.Keyword,
                Description = articleInfo1.Description
            };
            //没有摘要读取文章前30个字
            if (string.IsNullOrEmpty(model1.Digest))
            {
                string ddd = BonSite.Core.CommonHelper.DelHTML(model1.Body);
                model1.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
            }

        #endregion

            #region 上传图片，获取mediaID

            //获取mediaID
            string strImg = System.AppDomain.CurrentDomain.BaseDirectory + @"upload\article\show\source\" + model1.ImgUrl;

            if (model1.ImgUrl.Contains("upload"))
            {
                strImg = System.AppDomain.CurrentDomain.BaseDirectory + model1.ImgUrl;
            }
            else if (string.IsNullOrEmpty(model1.ImgUrl))
            {
                string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(model1.Body);
                if (ImgUrls.Length > 0)
                {
                    if (ImgUrls[0].Contains("http"))
                    {
                        strImg = ImgUrls[0];
                    }
                    else
                    {
                        strImg = System.AppDomain.CurrentDomain.BaseDirectory + ImgUrls[0];
                    }

                }
                else
                {
                    return "请添加封面图";
                }
                //mediaID1 = UploadMultiMedia(access_token, strImg1, "image");
                //if (mediaID1 == "0")
                //    return "请检查封面图片问题";
            }
            //if (string.IsNullOrEmpty(model1.ImgUrl))
            //    return "请选择封面";

            //string mediaID = UploadMultiMedia(access_token, strImg, "image");

            #endregion

            StringBuilder stringB = new StringBuilder();

            for (int i = 0; i < parmArticles.Length - 1; i++)
            {
                string articleID = parmArticles[i];
                if (!string.IsNullOrEmpty(articleID))
                {
                    articleIDInt = articleID == null ? 0 : Int32.Parse(articleID);

                    ArticleInfo articleInfo = Article.GetModelByArticleID(articleIDInt);

                    ArticleModel model = new ArticleModel()
                    {
                        ArticleClassID = articleInfo.ArticleClassID,
                        Title = articleInfo.Title,
                        SpecialID = articleInfo.SpecialID,
                        DisplayType = articleInfo.DisplayType,
                        Url = articleInfo.Url,
                        Digest = articleInfo.Digest,
                        ImgUrl = articleInfo.ImgUrl,
                        Body = articleInfo.Body,
                        Author = articleInfo.Author,
                        ComeForm = articleInfo.ComeForm,
                        IsShow = articleInfo.IsShow,
                        IsHome = articleInfo.IsHome,
                        IsBest = articleInfo.IsBest,
                        IsTop = articleInfo.IsTop,
                        AddTime = articleInfo.AddTime,
                        Keyword = articleInfo.Keyword,
                        Description = articleInfo.Description
                    };
                    //没有摘要读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        model.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
                    }

                    string strCon = string.Empty;

                    #region 正则表达式

                    StringBuilder sb = new StringBuilder();
                    string cotent = model.Body;
                    sb.Append(cotent);
                    Regex reg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>");
                    MatchCollection mats = reg.Matches(cotent);
                    Regex reg1 = null;
                    string str;
                    foreach (Match mat in mats)
                    {
                        str = mat.Value;
                        reg1 = new Regex("src=\"([^\"]+)\"");
                        string img = reg1.Match(str).Groups[1].ToString();
                        string imgPath = img;
                        if (img.Contains("http"))
                        {
                            imgPath = SaveImageFromWeb(img, @"upload\thumb\", "w_" + DateTime.Now.ToString("yyyymmddhhmmss"));
                        }
                        string filepath = System.AppDomain.CurrentDomain.BaseDirectory + imgPath;
                        string strImageUrl = GetImageUrl(access_token, filepath);
                        sb.Replace(img, strImageUrl);
                    }

                    #endregion

                    #region 上传图片，获取mediaID

                    //获取mediaID
                    string strImg1 = System.AppDomain.CurrentDomain.BaseDirectory + @"upload\article\show\source\" + model.ImgUrl;

                    string mediaID1 = "";
                    if (string.IsNullOrEmpty(model.ImgUrl))
                    {
                        string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(articleInfo.Body);
                        if (ImgUrls.Length > 0)
                        {
                            if (ImgUrls[0].Contains("http"))
                            {
                                strImg1 = ImgUrls[0];
                            }
                            else
                            {
                                strImg1 = System.AppDomain.CurrentDomain.BaseDirectory + ImgUrls[0];
                            }

                        }
                        else
                        {
                            return "请添加封面图";
                        }
                        //mediaID1 = UploadMultiMedia(access_token, strImg1, "image");
                        //if (mediaID1 == "0")
                        //    return "请检查封面图片问题";
                    }
                    else if (model.ImgUrl.Contains("upload"))
                    {
                        strImg1 = System.AppDomain.CurrentDomain.BaseDirectory + model.ImgUrl;
                        //mediaID1 = UploadMultiMedia(access_token, strImg1, "image");
                        //if (mediaID1 == "0")
                        //    return "请检查封面图片问题";
                    }
                    mediaID1 = UploadMultiMedia(access_token, strImg1, "image");
                    if (mediaID1 == "0")
                        return "请检查封面图片问题";
                    #endregion

                    strCon = sb.ToString();
                    strCon = strCon.Replace("\"", "'");


                    string str22 = @" ""thumb_media_id"":""{0}"",""author"":""{1}"",""title"":""{2}"",""content_source_url"":""{3}"",""content"":""{4}"",""show_cover_pic"":0,""digest"":""{5}""";
                    //没有摘要读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        model.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
                    }
                    string commandText = string.Format(str22,
                                                        mediaID1,
                                                        model.Author,
                                                        model.Title,
                                                        "",
                                                        strCon,
                                                        model.Digest);

                    string strText = "";

                    if (i > 0)
                    {
                        strText = @",{" + commandText + "}";
                    }
                    else
                    {
                        strText = @"{" + commandText + "}";
                    }

                    stringB.Append(strText);

                }

            }


            #region 上传消息

            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=" + access_token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";


            string strContent = @"{ ""articles"": [" + stringB.ToString() + "]}";

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            string media_id = "";
            if (jo["media_id"] != null)
            {
                media_id = jo["media_id"].ToString();
            }

            string type = jo["type"].ToString();
            if (jo["media_id"] != null)
            {
                media_id = jo["media_id"].ToString();
            }
            #endregion

            #region 预览

            //Preview(access_token, media_id);
            //return "预览成功";

            #endregion

            #region 群发

            string serviceAddress1 = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + access_token;
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(serviceAddress1);
            request1.Method = "POST";
            request1.ContentType = "application/json";

            string str1 = @"{ ""is_to_all"":true }";
            string str2 = @"{ ""media_id"": " + '"' + media_id + '"' + "}";

            string str3 = @"{ ""filter"": " + str1 + ",";
            string str4 = '"' + "mpnews" + '"' + ":" + str2 + ",";
            string str5 = @"""msgtype"":""mpnews"",""send_ignore_reprint"":1}";

            string strContent1 = str3 + str4 + str5;

            using (StreamWriter dataStream = new StreamWriter(request1.GetRequestStream()))
            {
                dataStream.Write(strContent1);
                dataStream.Close();
            }
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            string encoding1 = response1.ContentEncoding;
            if (encoding1 == null || encoding1.Length < 1)
            {
                encoding1 = "UTF-8"; //默认编码  
            }
            StreamReader reader1 = new StreamReader(response1.GetResponseStream(), Encoding.GetEncoding(encoding1));
            string retString1 = reader1.ReadToEnd();

            #endregion

            return "微信群发处理完毕";
        }

        /// <summary>
        /// 保存web图片到本地
        /// </summary>
        /// <param name="imgUrl">web图片路径</param>
        /// <param name="path">保存相对路径</param>
        /// <param name="fileName">保存文件名</param>
        /// <returns>文件相对</returns>
        public static string SaveImageFromWeb(string imgUrl, string path, string fileName)
        {
            if (path.Equals(""))
                throw new Exception("未指定保存文件的路径");
            string imgName = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("/") + 1);
            string defaultType = ".jpg";
            string[] imgTypes = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string imgType = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("."));
            string imgPath = "";
            foreach (string it in imgTypes)
            {
                if (imgType.ToLower().Equals(it))
                    break;
                if (it.Equals(".bmp"))
                    imgType = defaultType;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
            request.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)";
            request.Timeout = 3000;

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            if (response.ContentType.ToLower().StartsWith("image/"))
            {
                byte[] arrayByte = new byte[1024];
                int imgLong = (int)response.ContentLength;
                int l = 0;

                if (fileName == "")
                    fileName = imgName;

                FileStream fso = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + path + fileName + imgType, FileMode.Create);
                while (l < imgLong)
                {
                    int i = stream.Read(arrayByte, 0, 1024);
                    fso.Write(arrayByte, 0, i);
                    l += i;
                }

                fso.Close();
                stream.Close();
                response.Close();
                imgPath = path + fileName + imgType;
                return imgPath;
            }
            else
            {
                return "";
            }
        }
        private string GetImageUrl(string access_token, string filepath)
        {
            string result = "";
            string wxurl = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=" + access_token;

            //filepath = @"G:\1.jpg";
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filepath);
                result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
            }
            catch (Exception ex)
            {
                result = "Error:" + ex.Message;
            }
            string media_url = "";
            if (!string.IsNullOrEmpty(result))
            {
                JObject jo = JObject.Parse(result);
                if (jo["url"] != null)
                {
                    media_url = jo["url"].ToString();
                }
            }
           

            return media_url;
        }


        /// <summary>
        /// 微信客服推送
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public string WeChatKefuSend(string articleID)
        {
            #region 获取数据

            int articleIDInt = articleID == null ? 0 : Int32.Parse(articleID);
            ArticleInfo articleInfo = Article.GetModelByArticleID(articleIDInt);

            ArticleModel model = new ArticleModel()
            {
                ArticleClassID = articleInfo.ArticleClassID,
                Title = articleInfo.Title,
                SpecialID = articleInfo.SpecialID,
                DisplayType = articleInfo.DisplayType,
                Url = articleInfo.Url,
                Digest = articleInfo.Digest,
                ImgUrl = articleInfo.ImgUrl,
                Body = articleInfo.Body,
                Author = articleInfo.Author,
                ComeForm = articleInfo.ComeForm,
                IsShow = articleInfo.IsShow,
                IsHome = articleInfo.IsHome,
                IsBest = articleInfo.IsBest,
                IsTop = articleInfo.IsTop,
                AddTime = articleInfo.AddTime,
                Keyword = articleInfo.Keyword,
                Description = articleInfo.Description
            };
            //没有摘要读取文章前30个字
            if (string.IsNullOrEmpty(model.Digest))
            {
                string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                model.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
            }
            #endregion

            #region 获取access_token

            //获取access_token
            string access_token = WeiXinGetFunction();
            //string access_token = "XR0qhdfYDe1TUxwnR59cMLWUJXo-6gQ7o8U8s9DWQUEe_RDvlT5LMrHDEX3EXjI579wiIhM-FJjb9lQ3hjbHesdp7ectsq1MtKcTj6ymoKPkP5OhACO9mSuV2eiyfbxBWFIfAGADMP";

            #endregion

            #region 客服消息

            //KefuSend(access_token);
            string serviceAddress1 = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + access_token;
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(serviceAddress1);
            request1.Method = "POST";
            request1.ContentType = "application/json";

            string str22 = @" ""title"":""{0}"",""description"":""{1}"",""url"":""{2}"",""picurl"":""{3}""";

            //string str33 = string.Format(str22, "郑州八中",
            //                                    "郑州八中官网！！！",
            //                                    "http://www.zzbz.net",
            //                                    "http://www.zzbz.net/themes/zzbz/images/classtop/2.jpg");

            SiteConfigInfo siteConfigInfo = BSConfig.SiteConfig;
            string zzbz_host = siteConfigInfo.SiteUrl;
            string ImgUrl01 = zzbz_host + "upload/article/show/source/" + model.ImgUrl;
            if (model.ImgUrl.Contains("upload"))
            {
                ImgUrl01 = zzbz_host + model.ImgUrl;
            }
            else if (string.IsNullOrEmpty(model.ImgUrl))
            {
                string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(articleInfo.Body);
                if (ImgUrls.Length > 0)
                {
                    if (ImgUrls[0].Contains("http"))
                    {
                        ImgUrl01 = ImgUrls[0];
                    }
                    else
                    {
                        ImgUrl01 = zzbz_host + ImgUrls[0];
                    }

                }
            }
            //没有摘要读取文章前30个字
            if (string.IsNullOrEmpty(model.Digest))
            {
                string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                model.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
            }
            string str33 = string.Format(str22, model.Title,
                                                model.Digest,
                                                zzbz_host,
                                                 ImgUrl01);

            string str44 = @"{ ""articles"": [{" + str33 + "}]}";

            //string str55 = @" ""touser"":{0},""msgtype"":""{1}"",""news"":{2} ";

            //string str0 = "[" + '"' + "oCLawt5S-8oi4SrKhRBVLpp01Cg0"+ '"' + "," + '"' + "oCLawtz6K6xJcDRFgZKPSq6jCj8k"+ '"' + "]";

            string str55 = @" ""touser"":""{0}"",""msgtype"":""{1}"",""news"":{2} ";

            string str0 = "oCLawt5S-8oi4SrKhRBVLpp01Cg0";

            string str66 = string.Format(str55, str0,
                                                "news",
                                                str44);

            string strContent1 = "{" + str66 + "}";

            using (StreamWriter dataStream = new StreamWriter(request1.GetRequestStream()))
            {
                dataStream.Write(strContent1);
                dataStream.Close();
            }
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            string encoding1 = response1.ContentEncoding;
            if (encoding1 == null || encoding1.Length < 1)
            {
                encoding1 = "UTF-8"; //默认编码  
            }
            StreamReader reader1 = new StreamReader(response1.GetResponseStream(), Encoding.GetEncoding(encoding1));
            string retString1 = reader1.ReadToEnd();

            return "微信粉丝推送成功";

            #endregion
        }

        #region 客服消息

        public string KefuSend(string access_token)
        {
            //string media_id = "";
            string serviceAddress1 = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + access_token;
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(serviceAddress1);
            request1.Method = "POST";
            request1.ContentType = "application/json";

            string str22 = @" ""title"":""{0}"",""description"":""{1}"",""url"":""{2}"",""picurl"":""{3}""";

            string str33 = string.Format(str22, "郑州八中",
                                                "郑州八中官网！！！",
                                                "http://www.zzbz.net",
                                                "http://www.zzbz.net/themes/zzbz/images/classtop/2.jpg");

            string str44 = @"{ ""articles"": [{" + str33 + "}]}";

            string str55 = @" ""touser"":""{0}"",""msgtype"":""{1}"",""news"":{2} ";

            string str0 = "oCLawt5S-8oi4SrKhRBVLpp01Cg0";

            string str66 = string.Format(str55, str0,
                                                "news",
                                                str44);

            string strContent1 = "{" + str66 + "}";

            using (StreamWriter dataStream = new StreamWriter(request1.GetRequestStream()))
            {
                dataStream.Write(strContent1);
                dataStream.Close();
            }
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            string encoding1 = response1.ContentEncoding;
            if (encoding1 == null || encoding1.Length < 1)
            {
                encoding1 = "UTF-8"; //默认编码  
            }
            StreamReader reader1 = new StreamReader(response1.GetResponseStream(), Encoding.GetEncoding(encoding1));
            string retString1 = reader1.ReadToEnd();

            return "推送成功";
        }

        #endregion


        #region 获取用户信息

        //string serviceAddress = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=q0TCazG19F2GmjqEV5UU4dHgr-tEx_RXeV4jBIynkomyaEAPwmTZnYTun-JDHL7Cv8g2WmdHE41Cp2zs6aHmQylfohHxmJ-SYAT-nZyouVG6i-VKgQBg1MCETszb_5_TYAAeAFARTT&next_openid=";

        #endregion

        #region 用户管理，查询分组接口

        //string serviceAddress = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token=0N34FRHADitT8WHcm8nYDdhaz_Y8P30ihiwJT2oPhhWgUb_SWSnAieSl1a7OSSiRuSPF1032uog6-SstU_aEcFu4Q_N_-yShzDTnrC_c9HOLP68whDbLulEciyKHYl4PTJKeAFAYOP";

        #endregion

        /// <summary>
        /// 微信消息群发，预览
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public string Preview(string access_token, string media_id)
        {
            string serviceAddress1 = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token=" + access_token;
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(serviceAddress1);
            request1.Method = "POST";
            request1.ContentType = "application/json";

            string str2 = @"{ ""media_id"": " + '"' + media_id + '"' + "}";
            string str3 = @"{ ""towxname"": ""easy_happy_miles"",";
            string str4 = '"' + "mpnews" + '"' + ":" + str2 + ",";
            string str5 = @"""msgtype"":""mpnews""}";

            string strContent1 = str3 + str4 + str5;

            using (StreamWriter dataStream = new StreamWriter(request1.GetRequestStream()))
            {
                dataStream.Write(strContent1);
                dataStream.Close();
            }
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            string encoding1 = response1.ContentEncoding;
            if (encoding1 == null || encoding1.Length < 1)
            {
                encoding1 = "UTF-8"; //默认编码  
            }
            StreamReader reader1 = new StreamReader(response1.GetResponseStream(), Encoding.GetEncoding(encoding1));
            string retString1 = reader1.ReadToEnd();

            return "微信预览成功";
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public string WeiXinGetFunction()
        {
            //return "4HwhKzZALOREh-5yU20aqA2WNCikLAzgX3-a8j-QWJjl8FPIE93e5X5f7knhMsgKP6Zn666ZQ2VXbT4yWPN0Fjt_JgCdxzlOo6OW28uIm1m9TwR32H3IGQWfBOsJkEKyJIIbAHABAA";

            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx582552a19af521d6&secret=586fcf2df8c3312d744f465fed01e4ed";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            JObject jo = JObject.Parse(retString);

            string access_token = jo["access_token"].ToString();
            return access_token;
        }

        public string WeiXinGetFunction(string appID, string secret)
        {
            //return "4HwhKzZALOREh-5yU20aqA2WNCikLAzgX3-a8j-QWJjl8FPIE93e5X5f7knhMsgKP6Zn666ZQ2VXbT4yWPN0Fjt_JgCdxzlOo6OW28uIm1m9TwR32H3IGQWfBOsJkEKyJIIbAHABAA";

            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appID + "&secret=" + secret;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            JObject jo = JObject.Parse(retString);

            string access_token = jo["access_token"].ToString();
            return access_token;
        }


        public string GetUserInfoList(string appID, string secret)
        {
            string token = WeiXinGetFunction(appID, secret);

            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            JObject jo = JObject.Parse(retString);

            var openid = jo["data"]["openid"].ToString();
            return openid;
        }

        ////post方法调用接口获取json文件内容
        /// <summary>
        /// 获取素材列表
        /// </summary>
        public ArticleListModel WeiXinPostFunction(string access_token)
        {
            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token=" + access_token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";
            string strContent = @"{ ""type"": ""news"",""offset"": 0,""count"": 3}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);

            int itemCount = Convert.ToInt32(jo["item_count"].ToString());

            return null;
        }

        /// <summary>
        /// 消息推送
        /// </summary>
        public void WeiXinSend(string access_token)
        {
            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + access_token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";

            string str1 = @"{ ""is_to_all"":false,""tag_id"":101}";
            string str2 = @"{ ""media_id"": ""BhHOpuTv7Z2zn-Hv6-QqTcQbQaO_nC0UAXvhsP3tyNaOuRCaID6XcunXfD6deXSW""}";

            string str3 = @"{ ""filter"": " + str1 + ",";
            string str4 = '"' + "mpnews" + '"' + ":" + str2 + ",";
            string str5 = @"""msgtype"":""mpnews"",""send_ignore_reprint"":0}";

            string strContent = str3 + str4 + str5;

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
        }

        /// <summary>
        /// 上传多媒体文件,返回 MediaId
        /// </summary>
        /// <param name="ACCESS_TOKEN"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string UploadMultiMedia(string ACCESS_TOKEN, string filepath, string type)
        {
            string result = "";
            string wxurl = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + ACCESS_TOKEN + "&type=" + type;
            //filepath = @"G:\1.jpg";
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                byte[] responseArray = myWebClient.UploadFile(wxurl, "POST", filepath);
                result = System.Text.Encoding.Default.GetString(responseArray, 0, responseArray.Length);
            }
            catch (Exception ex)
            {
                result = "Error:" + ex.Message;
                string mediaid = "0";
                return mediaid;
            }

            JObject jo = JObject.Parse(result);
            string media_id = "";
            if (jo["media_id"] != null)
            {
                media_id = jo["media_id"].ToString();
            }

            return media_id;
        }

        #region 新的页面

        public ActionResult SendWeChat(string mydata)
        {
            #region 获取文章组信息

            List<ArticleModel> articleList = new List<ArticleModel>();
            string[] parm = mydata.Split('#');
            for (int i = 1; i < parm.Length; i++)
            {
                ArticleModel article = new ArticleModel();
                if (parm[i] == null || parm[i].Equals(""))
                {
                    continue;
                }
                else
                {
                    string[] parm1 = parm[i].Split('*');
                    if (parm1[0] != null)
                    {
                        article.ArticleClassID = Int32.Parse(parm1[0]);
                    }
                    if (parm1[1] != null)
                    {
                        article.Title = parm1[1];
                    }
                }

                articleList.Add(article);
            }



            #endregion

            #region 获取微信分组信息

            string access_token = WeiXinGetFunction();
            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token=" + access_token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            JObject jo = JObject.Parse(retString);

            var tags = jo["tags"];

            List<Tag> tagList = new List<Tag>();
            foreach (var tag in tags)
            {
                var id = tag["id"];
                var name = tag["name"].ToString();
                var count = tag["count"];
                Tag t = new Tag();
                t.id = Int32.Parse(id.ToString());
                t.name = name;
                t.count = Int32.Parse(count.ToString());
                tagList.Add(t);
            }

            ViewData["TagList"] = tagList;

            #endregion

            ViewData["ArticleList"] = articleList;

            //this.ControllerContext.RouteData.DataTokens.Remove("Namespaces");
            //this.ControllerContext.RouteData.DataTokens.Add("Namespaces", "BonSite.Web.Admin.Controllers");
            //this.ControllerContext.RouteData.DataTokens.Remove("area");
            //this.ControllerContext.RouteData.DataTokens.Add("area", "admin");
            //this.ControllerContext.RouteData.DataTokens.Remove("UseNamespaceFallback");
            //this.ControllerContext.RouteData.DataTokens.Add("UseNamespaceFallback", false);
            return View();
        }

        public ActionResult SendWeChat1(string articleTitle, string Keyword, string sortColumn, string sortDirection, int? articleClassId1, int pageSize = 10, int pageNumber = 1)
        {

            ArticleListModel WeChatModel = new ArticleListModel();
            WeChatModel = modelConst;

            #region 分页
            int articleClassId = 0;

            if (articleClassId1 == null)
            {
                articleClassId = 242;
            }
            else
            {
                articleClassId = (int)articleClassId1;
            }
            string condition1 = " ([DisplayType] in (0,2) " + " and ApprovalStatus=1 or ArticleClassID=242) ";
            string sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition1));
           
            #endregion
            #region 获取文章组信息

            //string mydata = "NOT_USE#3346*东海开渔第一网海鲜抵岸 渔民3天赚10万(组图)#3332*微信预览2#3319*微信群发3#3316*微信群发4#";

            //List<ArticleModel> articleList = new List<ArticleModel>();
            //string[] parm = mydata.Split('#');
            //for (int i = 1; i < parm.Length; i++)
            //{
            //    ArticleModel article = new ArticleModel();
            //    if (parm[i] == null || parm[i].Equals(""))
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        string[] parm1 = parm[i].Split('*');
            //        if (parm1[0] != null)
            //        {
            //            article.ArticleClassID = Int32.Parse(parm1[0]);
            //        }
            //        if (parm1[1] != null)
            //        {
            //            article.Title = parm1[1];
            //        }
            //    }

            //    articleList.Add(article);
            //}

            #endregion

            #region 获取微信分组信息

            WeChatModel model = new WeChatModel();
            model.WeChatList = WeChats.GetWeChatList();

            List<WeChatInfo> tagList = new List<WeChatInfo>();
            foreach (var tag in model.WeChatList)
            {
                var id = tag.id;
                var name = tag.name;
                var appid = tag.appid;
                var secret = tag.secret;
                WeChatInfo t = new WeChatInfo();
                t.id = Int32.Parse(id.ToString());
                t.name = name;
                t.appid = appid;
                t.secret = secret;
                tagList.Add(t);
            }

            ViewData["TagList"] = tagList;

            #endregion

            //ViewData["ArticleList"] = articleList;

            WeChatModel.DataList = Article.AdminGetArticleList(pageModel.PageSize, pageModel.PageNumber, condition1, sort);
            WeChatModel.PageModel = pageModel;
            return View("SendWeChat", WeChatModel);
        }

        public string SendWeChatSubmit(string mydata)
        {
            #region 获取文章组信息

            List<ArticleModel> articleList = new List<ArticleModel>();
            string[] parm = mydata.Split('*');

            string[] parmArticles = parm[0].Split('#');
            string[] parmTags = parm[1].Split('#');
            string[] parmTypes = parm[2].Split('#');

            #endregion

            string result = string.Empty;

            string tagID = "";
            if (parmTags.Length > 0)
                tagID = parmTags[0].ToString();

            string typeID = "";
            if (parmTypes.Length > 0)
                typeID = parmTypes[0].ToString();

            result = WeChatSend(parmArticles, tagID, typeID);

            return result;
        }

        #endregion

        #region 新的页面

        public ActionResult SendWeChatKefu(string mydata)
        {
            #region 获取文章组信息

            List<ArticleModel> articleList = new List<ArticleModel>();
            string[] parm = mydata.Split('#');
            for (int i = 1; i < parm.Length; i++)
            {
                ArticleModel article = new ArticleModel();
                if (parm[i] == null || parm[i].Equals(""))
                {
                    continue;
                }
                else
                {
                    string[] parm1 = parm[i].Split('*');
                    if (parm1[0] != null)
                    {
                        article.ArticleClassID = Int32.Parse(parm1[0]);
                    }
                    if (parm1[1] != null)
                    {
                        article.Title = parm1[1];
                    }
                }

                articleList.Add(article);
            }

            #endregion

            #region 获取微信分组信息

            string access_token = WeiXinGetFunction();
            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token=" + access_token;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            JObject jo = JObject.Parse(retString);

            var tags = jo["tags"];

            List<Tag> tagList = new List<Tag>();
            foreach (var tag in tags)
            {
                var id = tag["id"];
                var name = tag["name"].ToString();
                var count = tag["count"];
                Tag t = new Tag();
                t.id = Int32.Parse(id.ToString());
                t.name = name;
                t.count = Int32.Parse(count.ToString());
                tagList.Add(t);
            }

            ViewData["TagList"] = tagList;

            #endregion

            ViewData["ArticleList"] = articleList;

            this.ControllerContext.RouteData.DataTokens.Remove("Namespaces");
            this.ControllerContext.RouteData.DataTokens.Add("Namespaces", "BonSite.Web.Admin.Controllers");
            this.ControllerContext.RouteData.DataTokens.Remove("area");
            this.ControllerContext.RouteData.DataTokens.Add("area", "admin");
            this.ControllerContext.RouteData.DataTokens.Remove("UseNamespaceFallback");
            this.ControllerContext.RouteData.DataTokens.Add("UseNamespaceFallback", false);
            return View();
        }

        public string SendWeChatKefuSubmit(string mydata)
        {
            #region 获取文章组信息

            List<ArticleModel> articleList = new List<ArticleModel>();
            string[] parm = mydata.Split('*');

            string[] parmArticles = parm[0].Split('#');
            string[] parmTags = parm[1].Split('#');

            #endregion

            string result = string.Empty;

            for (int j = 0; j < parmTags.Length - 1; j++)
            {
                string tagID = parmTags[j];
                if (!string.IsNullOrEmpty(tagID))
                {
                    result = WeChatKeFuSend(parmArticles, tagID);
                }
            }

            return result;
        }

        public string WeChatKeFuSend(string[] parmArticles, string tagID)
        {
            #region 获取access_token
            //获取access_token
            string appID = string.Empty;
            string secret = string.Empty;
            if (!string.IsNullOrEmpty(tagID))
            {
                string[] parm = tagID.Split('@');
                if (parm.Length > 0)
                {
                    appID = parm[0];
                    secret = parm[1];
                }
            }
            string access_token = WeiXinGetFunction(appID, secret);
            #endregion

            int articleIDInt = 0;

            StringBuilder stringB = new StringBuilder();

            for (int i = 0; i < parmArticles.Length - 1; i++)
            {
                string articleID = parmArticles[i];
                if (!string.IsNullOrEmpty(articleID))
                {
                    articleIDInt = articleID == null ? 0 : Int32.Parse(articleID);

                    ArticleInfo articleInfo = Article.GetModelByArticleID(articleIDInt);

                    ArticleModel model = new ArticleModel()
                    {
                        ArticleClassID = articleInfo.ArticleClassID,
                        Title = articleInfo.Title,
                        SpecialID = articleInfo.SpecialID,
                        DisplayType = articleInfo.DisplayType,
                        Url = articleInfo.Url,
                        Digest = articleInfo.Digest,
                        ImgUrl = articleInfo.ImgUrl,
                        Body = articleInfo.Body,
                        Author = articleInfo.Author,
                        ComeForm = articleInfo.ComeForm,
                        IsShow = articleInfo.IsShow,
                        IsHome = articleInfo.IsHome,
                        IsBest = articleInfo.IsBest,
                        IsTop = articleInfo.IsTop,
                        AddTime = articleInfo.AddTime,
                        Keyword = articleInfo.Keyword,
                        Description = articleInfo.Description
                    };
                    //没有摘要读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd1 = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        model.Digest = BonSite.Core.StringHelper.SubString(ddd1, 30);
                    }
                    string str22 = @" ""title"":""{0}"",""description"":""{1}"",""url"":""{2}"",""picurl"":""{3}""";
                    SiteConfigInfo siteConfigInfo = BSConfig.SiteConfig;
                    string zzbz_host = siteConfigInfo.SiteUrl;
                    string imgUrl = siteConfigInfo.ImgUrl;
                    string imgUrl01 = zzbz_host + imgUrl + model.ImgUrl;
                    if (model.ImgUrl.Contains("upload"))
                    {
                        imgUrl01 = zzbz_host + model.ImgUrl;
                    }
                    else if (string.IsNullOrEmpty(model.ImgUrl))
                    {
                        string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(articleInfo.Body);
                        if (ImgUrls.Length > 0)
                        {
                            if (ImgUrls[0].Contains("http"))
                            {
                                imgUrl01 = ImgUrls[0];
                            }
                            else
                            {
                                imgUrl01 = zzbz_host + ImgUrls[0];
                            }

                        }
                    }
                    //没有摘要读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        model.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
                    }
                    string commandText = string.Format(str22, model.Title,
                                                        model.Digest,
                                                        zzbz_host + "WeChat/Index?id=" + articleIDInt,
                                                        imgUrl01
                    );
                    //  /upload/article/show/thumb225_155/ps_1708111752386943359.jpg
                    //    /upload/article/show/thumb225_155/ps_1708111807291630859.jpg
                    //http://localhost:24809/WeChat/Index?id=3316
                    //"http://localhost:24809/upload/article/show/source/" + model.ImgUrl);

                    string strText = "";

                    if (i > 0)
                    {
                        strText = @",{" + commandText + "}";
                    }
                    else
                    {
                        strText = @"{" + commandText + "}";
                    }

                    stringB.Append(strText);

                }

            }

            #region 客服消息

            string serviceAddress1 = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + access_token;
            //HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(serviceAddress1);
            //request1.Method = "POST";
            //request1.ContentType = "application/json";

            string str44 = @"{ ""articles"": [" + stringB.ToString() + "]}";

            string str55 = @" ""touser"":{0},""msgtype"":""{1}"",""news"":{2} ";

            //string str0 = "oCLawt5S-8oi4SrKhRBVLpp01Cg0";

            string appIDList = GetUserInfoList(appID, secret);

            string[] appIDParm = null;
            if (!string.IsNullOrEmpty(appIDList))
            {
                appIDParm = appIDList.Trim('[').Trim(']').Split(',');
            }

            if (appIDParm.Length > 0)
            {
                foreach (string userID in appIDParm)
                {
                    string str66 = string.Format(str55, userID,
                                                "news",
                                                str44);

                    string strContent1 = "{" + str66 + "}";
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(serviceAddress1);
                    request2.Method = "POST";
                    request2.ContentType = "application/json";
                    using (StreamWriter dataStream = new StreamWriter(request2.GetRequestStream()))
                    {
                        dataStream.Write(strContent1);
                        dataStream.Close();
                    }
                    HttpWebResponse response1 = (HttpWebResponse)request2.GetResponse();
                    string encoding1 = response1.ContentEncoding;
                    if (encoding1 == null || encoding1.Length < 1)
                    {
                        encoding1 = "UTF-8"; //默认编码  
                    }
                    StreamReader reader1 = new StreamReader(response1.GetResponseStream(), Encoding.GetEncoding(encoding1));
                    string retString1 = reader1.ReadToEnd();
                }
            }

            return "微信粉丝推送处理完毕";

            #endregion

        }

        #endregion
    }

}