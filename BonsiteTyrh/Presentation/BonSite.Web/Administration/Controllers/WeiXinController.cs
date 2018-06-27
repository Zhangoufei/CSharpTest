using System;
using System.Web.Mvc;

using BonSite.Web.Framework;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using BonSite.Web.Admin.Models;
using BonSite.Core;
using BonSite.Services;



namespace BonSite.Web.Admin.Controllers
{
    public class WeiXinController: BaseAdminController
    {
        /// <summary>
        /// 微信素材库列表
        /// Author：WL
        /// </summary>
        public ActionResult List()
        {
            string access_token = WeiXinGetFunction();
            ArticleListModel newsListModel = new ArticleListModel();
            newsListModel = WeiXinPostFunction(access_token);
            SiteUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(newsListModel);
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public string WeiXinGetFunction()
        {

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

            List<ArticleInfo> newsList = new List<ArticleInfo>();

            for (int i = 0; i < itemCount; i++)
            {
                string content1 = jo["item"][i]["content"].ToString();
                string media_id = jo["item"][i]["media_id"].ToString();
                string update_time = jo["item"][i]["update_time"].ToString();

                string news_item = jo["item"][i]["content"]["news_item"].ToString();

                string title = jo["item"][i]["content"]["news_item"][0]["title"].ToString();
                string author = jo["item"][i]["content"]["news_item"][0]["author"].ToString();
                string digest = jo["item"][i]["content"]["news_item"][0]["digest"].ToString();
                string content = jo["item"][i]["content"]["news_item"][0]["content"].ToString();
                string content_source_url = jo["item"][i]["content"]["news_item"][0]["content_source_url"].ToString();
                string thumb_media_id = jo["item"][i]["content"]["news_item"][0]["thumb_media_id"].ToString();
                string show_cover_pic = jo["item"][i]["content"]["news_item"][0]["show_cover_pic"].ToString();

                string url = jo["item"][i]["content"]["news_item"][0]["url"].ToString();
                string need_open_comment = jo["item"][i]["content"]["news_item"][0]["need_open_comment"].ToString();
                string only_fans_can_comment = jo["item"][i]["content"]["news_item"][0]["only_fans_can_comment"].ToString();

                ArticleInfo news = new ArticleInfo();

                news.Title = title;
                news.Body = content;
                news.Digest = digest;
                news.Description = author;
               
                newsList.Add(news);
            }

            ArticleListModel newsListModel = new ArticleListModel();
            newsListModel.DataInfoList = newsList;
            return newsListModel;
        }

        /// <summary>
        /// 消息推送
        /// </summary>
        public void Send()
        {
            string serviceAddress = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=HM7WSmONH1ufFAc_4WxGC8XHk8kL5HCv_rcuuUgkSxogMdpcFgYDxhs6QAANPGAG8uW0aJWFvytzymFcIAF639ZfiyQCQTsg_0mgdGOIU5vUL7ptPiOKkuKM0Zx6yB3oCQTgAIAWKN";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";

            string str1 = @"{ ""is_to_all"":false,""tag_id"":2}";
            string str2 = @"{ ""media_id"": ""J5W-Dcez8E8Didc59rX-cEPjp8qZP86IN7uoFlsOiQo""}";

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
            //Response.Write(jo["item"]["mmmm"].ToString());
        }

        #region 

        public class News
        {
            public string title { get; set; }

            public string author { get; set; }
            public string digest { get; set; }

            public string content { get; set; }
            public string content_source_url { get; set; }
            public string thumb_media_id { get; set; }
            public string show_cover_pic { get; set; }
            public string url { get; set; }
            public string need_open_comment { get; set; }
            public string only_fans_can_comment { get; set; }
        }

        public class NewsListModel
        {
            public News[] NewsList { get; set; }
        }

        #endregion

    }
}