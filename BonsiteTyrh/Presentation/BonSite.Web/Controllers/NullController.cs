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
    public class NullController : BaseWebController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           
            return View();
        }
    }
}
