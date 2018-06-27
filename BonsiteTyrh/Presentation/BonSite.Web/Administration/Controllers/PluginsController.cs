using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonSite.Services;
using BonSite.Web.Admin.Models;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Controllers
{
    public class PluginsController : BaseAdminController
    {
        //
        // GET: /Plugins/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServiceEval(int pageSize = 50, int pageNumber = 1)
        {
            string condition = Services.ServiceEval.AdminGetServiceEvalListCondition("");
            string sort = Services.ServiceEval.AdminGetServiceEvalListSort("", "");

            PageModel pageModel = new PageModel(pageSize, pageNumber, Services.ServiceEval.AdminGetServiceEvalCount(condition));

            ServiceEvalListModel model = new ServiceEvalListModel()
            {
                DataList = Services.ServiceEval.AdminGetServiceEvalList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };

            SiteUtils.SetAdminRefererCookie(Url.Action("ServiceEval", "Plugins"));

            return View(model);
        }


        public ActionResult ProductFeedBacks(int pageSize = 50, int pageNumber = 1)
        {
            string condition = Services.ProductFeedBacks.AdminGetProductFeedbacksCondition("");
            string sort = Services.ProductFeedBacks.AdminGetProductFeedbacksListSort("", "");

            PageModel pageModel = new PageModel(pageSize, pageNumber, Services.ProductFeedBacks.AdminGetProductFeedbacksCount(condition));

            ProductFeedBacksListModel model = new ProductFeedBacksListModel()
            {
                DataList = Services.ProductFeedBacks.AdminGetProductFeedbacksList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };

            SiteUtils.SetAdminRefererCookie(Url.Action("ProductFeedBacks", "Plugins"));

            return View(model);
        }
    }
}
