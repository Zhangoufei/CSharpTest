﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace BonSite.Web.Framework
{
    /// <summary>
    /// 前台视图页面基类型
    /// </summary>
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public WebWorkContext WorkContext;

        public override void InitHelpers()
        {
            base.InitHelpers();
            WorkContext = ((BaseWebController)(this.ViewContext.Controller)).WorkContext;
        }
    }

    /// <summary>
    /// 前台视图页面基类型
    /// </summary>
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
