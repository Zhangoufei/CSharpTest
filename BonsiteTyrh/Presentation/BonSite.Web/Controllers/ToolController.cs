using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;


using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web.Controllers
{
    public class ToolController : Controller
    {
        //
        // GET: /Tool/

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ImageResult VerifyImage(int width=56,int height=20)
        {
            //获得用户唯一标示符sid
            string sid = SiteUtils.GetSidCookie();
            //当sid为空时
            if (sid == null)
            {
                //生成sid
                sid = Sessions.GenerateSid();
                //将sid保存到cookie中
                SiteUtils.SetSidCookie(sid);
            }

            //生成验证值
            string verifyValue = Randoms.CreateRandomValue(4, false).ToLower();
            //生成验证图片
            RandomImage verifyImage = Randoms.CreateRandomImage(verifyValue, width, height, Color.White, Color.Blue, Color.DarkRed);
            //将验证值保存到session中
            Sessions.SetItem(sid, "verifyCode", verifyValue);

            //输出验证图片
            return new ImageResult(verifyImage.Image, verifyImage.ContentType);
        
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
