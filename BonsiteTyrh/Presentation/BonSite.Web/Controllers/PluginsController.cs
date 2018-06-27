using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Controllers
{
    public class PluginsController : BaseWebController
    {
        //
        // GET: /Plugins/

        public string appID = BSConfig.SiteConfig.WeChatAppId;
        //wxda13945d24575202
        //测试 wx92441d7e0ada0696
        public string appsecret = BSConfig.SiteConfig.WeChatAppSecret;
        //c456b5f494b3a46791c1bba872325885
        //测试 3843c2664d52c6647d832f3828b08d7c

        public string getCodeUrl =
            "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect";

        public string getAccessTokenUrl =
            "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";

        public string getUserInfoUrl =
            "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";


        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult ServiceEval(string code="",string state="")
        {


            Core.ServiceEvalInfo model = new Core.ServiceEvalInfo()
            {
                EvalProduct = 3,
                EvalLogistics = 3,
                EvalService = 3,
                State = 0
            };

            string useragent = Request.UserAgent;
            if (useragent.ToLower().Contains("micromessenger"))
            {

                //1.不包含code和state的情况下，转去微信认证
                if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(state))
                {
                    //转向微信认证
                    string url = "http://www.kingsdown.com.cn/Plugins/ServiceEval";
                    string requestUrl = string.Format(getCodeUrl, appID, System.Web.HttpUtility.UrlEncode(url), Guid.NewGuid().ToString());
                    return Redirect(requestUrl);
                }

                //2.在有code和state的情况下，获取access_token
                accessTokenInfo tokenInfo = GetAccessTokenInfo(getAccessTokenUrl, appID, appsecret, code);


                if (tokenInfo != null)
                {
                    new LogHelper().Write(tokenInfo.access_token);

                    weChatUserInfo wechatInfo = GetWeChatUserInfo(getUserInfoUrl, tokenInfo.access_token,
                        tokenInfo.openid);

                    if (wechatInfo != null)
                    {
                        model.Name = wechatInfo.nickname;
                        model.WeChatOpenId = wechatInfo.openid;
                        model.WeChatName = wechatInfo.nickname;
                        model.Province = wechatInfo.province;
                        model.City = wechatInfo.city;
                    }

                }

                List<SelectListItem> selList = new List<SelectListItem>();
                selList.Add(new SelectListItem() { Text = "01", Value = "01" });
                selList.Add(new SelectListItem() { Text = "02", Value = "02" });
                selList.Add(new SelectListItem() { Text = "03", Value = "03" });
                selList.Add(new SelectListItem() { Text = "04", Value = "04" });

                ViewData["selList"] = selList;


            }



            return View(model);
        }


        [HttpPost]
        public ActionResult ServiceEval(Core.ServiceEvalInfo model,string code="",string state="")
        {

            model.State = 0;

            if (ModelState.IsValid)
            {
                model.CreateTime = DateTime.Now;
                model.Courier = Request.Params["Courier"].ToString();
                if (Services.ServiceEval.Create(model) > 0)
                    return RedirectToAction("ServiceEvalSuccess");
                else
                    return Content("提交失败");
            }


            foreach (var s in ModelState.Values)
            {
                foreach (var p in s.Errors)
                {

                    new LogHelper().Write(p.ErrorMessage);
                }
            }
            return View(model);
        }



        public ActionResult ServiceEvalSuccess()
        {
            return View();
        }


        public ActionResult ProductFeedBacks(string code = "", string state = "")
        {
            Core.ProductFeedbacksInfo model=new Core.ProductFeedbacksInfo();




            string useragent = Request.UserAgent;
            if (useragent.ToLower().Contains("micromessenger"))
            {

                //1.不包含code和state的情况下，转去微信认证
                if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(state))
                {
                    //转向微信认证
                    string url = "http://www.kingsdown.com.cn/Plugins/ProductFeedBacks";
                    string requestUrl = string.Format(getCodeUrl, appID, System.Web.HttpUtility.UrlEncode(url), Guid.NewGuid().ToString());
                    return Redirect(requestUrl);
                }

                //2.在有code和state的情况下，获取access_token
                accessTokenInfo tokenInfo = GetAccessTokenInfo(getAccessTokenUrl, appID, appsecret, code);


                if (tokenInfo != null)
                {
                    new LogHelper().Write(tokenInfo.access_token);

                    weChatUserInfo wechatInfo = GetWeChatUserInfo(getUserInfoUrl, tokenInfo.access_token,
                        tokenInfo.openid);

                    if (wechatInfo != null)
                    {
                        model.CustomerName = wechatInfo.nickname;
                        model.WeChatOpenId = wechatInfo.openid;
                        model.WeChatName = wechatInfo.nickname;
                        model.CityName = wechatInfo.city;
                    }

                }

            }


            return View(model);
        }


        [HttpPost]
        public ActionResult ProductFeedBacks(Core.ProductFeedbacksInfo model, string code = "", string state = "")
        {
            model.State = 0;
            if (ModelState.IsValid)
            {
                model.CreateTime = DateTime.Now;

                if (Services.ProductFeedBacks.Create(model) > 0)
                    return RedirectToAction("ProductFeedBacksSuccess");
                else
                    return Content("提交失败");
            }
            else
            {
                foreach (var s in ModelState.Values)
                {
                    foreach (var p in s.Errors)
                    {

                        new LogHelper().Write(p.ErrorMessage);
                    }
                }
            }

            return View(model);
        }

        public ActionResult ProductFeedBacksSuccess()
        {
            return View();
        }

        public ActionResult ImgUpload(string type="",string value="")
        {

            //HttpPostedFileBase image = ControllerContext.RequestContext.HttpContext.Request.Files[0];
            string result = SiteUtils.SavePluginsImg(value);
            return Content(result);
        }



        public static accessTokenInfo GetAccessTokenInfo(string getAccessTokenUrl, string appID, string appsecret,string code)
        {
            string accessurl = string.Format(getAccessTokenUrl, appID, appsecret, code);

            string result = WebHelper.GetRequestData(accessurl, "get", "");


            if (result.Contains("errmsg"))
            {
                return null;
            }
            try
            {
                accessTokenInfo tokenInfo= JsonHelper.DeserializeJsonToObject<accessTokenInfo>(result);

                return tokenInfo;

            }
            catch(Exception e)
            {
                return null;
            }
        }


        public static weChatUserInfo GetWeChatUserInfo(string getUserInfoUrl, string accessToken, string openid)
        {
            string accessurl = string.Format(getUserInfoUrl, accessToken, openid);


            string result = WebHelper.GetRequestData(accessurl, "get", "");

            if (result.Contains("errmsg"))
            {
                return null;
            }
            try
            {
                weChatUserInfo userInfo= JsonHelper.DeserializeJsonToObject<weChatUserInfo>(result);

                return userInfo;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [Serializable]
        public class accessTokenInfo
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string openid { get; set; }
            public string scope { get; set; }
            public string unionid { get; set; }
        }


        [Serializable]
        public class weChatUserInfo
        {
            public string openid { get; set; }
            public string nickname { get; set; }
            public string sex { get;set; }
            public string province { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string headimgurl { get; set; }
            public string unionid { get; set; }
            public string[] privilege { get; set; }
        }
    }
}
