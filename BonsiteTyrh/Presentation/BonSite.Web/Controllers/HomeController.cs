using System;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace BonSite.Web.Controllers
{
    public class HomeController : BaseWebController
    {
        //
        // GET: /Home/

        public ActionResult Index(int type = 0)
        {
            WorkContext.Type = type;
            WorkContext.NavList = ArticleClass.GetNavList(type);
            return View();
        }
        //加载页
        public ActionResult Start()
        {
            return View();
        }
        public ActionResult IndexPc(int type = 0)
        {
            List<SpecialInfo> a =BonSite.Services.Special.GetList();
            SpecialInfo info = BonSite.Services.Special.GetModelById(a[0].SpecialID);
            WorkContext.Type = type;
            WorkContext.NavList = ArticleClass.GetNavList(type);
            return View();
        }
    }
}
