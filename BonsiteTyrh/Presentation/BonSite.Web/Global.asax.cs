using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;

namespace BonSite.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            //带分页的文章列表
            routes.MapRoute("ListPage",
                            "List/{ClassId}-{page}.html",
                            new { controller = "Article", action = "List" },
                            new[] { "BonSite.Web.Controllers" });
            //pc带分页的文章列表
            routes.MapRoute("ListPagepc",
                            "ListPc/{ClassId}-{page}.html",
                            new { controller = "Article", action = "ListPc" },
                            new[] { "BonSite.Web.Controllers" });

            //pc带分页的视频列表
            routes.MapRoute("ListVideo",
                            "ListVideo/{ClassId}-{page}.html",
                            new { controller = "Article", action = "ListVideo" },
                            new[] { "BonSite.Web.Controllers" });


            //文章列表路由
            routes.MapRoute("List",
                            "List/{ClassId}.html",
                            new { controller = "Article", action = "List" },
                            new[] { "BonSite.Web.Controllers" });
            //pc文章列表路由
            routes.MapRoute("ListPc",
                            "ListPc/{ClassId}.html",
                            new { controller = "Article", action = "ListPc" },
                            new[] { "BonSite.Web.Controllers" });

            //文章详情
            routes.MapRoute("View",
                            "View/{id}.html",
                            new { controller = "Article", action = "Details" },
                            new[] { "BonSite.Web.Controllers" });
            //pc文章详情
            routes.MapRoute("ViewPc",
                            "ViewPc/{id}.html",
                            new { controller = "Article", action = "DetailsPc" },
                            new[] { "BonSite.Web.Controllers" });
            //pc文章详情videoshows
            routes.MapRoute("videoshows",
                            "videoshows/{id}.html",
                            new { controller = "Article", action = "videoshows" },
                            new[] { "BonSite.Web.Controllers" });
            //专题
            routes.MapRoute("Special",
                            "Special/{id}.html",
                            new { controller = "Article", action = "Special" },
                            new[] { "BonSite.Web.Controllers" });

            //带分页的产品列表
            routes.MapRoute("ProductListPage",
                            "Product/Class-{ClassId}-{page}.html",
                            new { controller = "Product", action = "List" },
                            new[] { "BonSite.Web.Controllers" });

            //产品列表路由
            routes.MapRoute("ProductList",
                            "Product/Class-{ClassId}.html",
                            new { controller = "Product", action = "List" },
                            new[] { "BonSite.Web.Controllers" });
            //产品详情
            routes.MapRoute("ProductView",
                            "Product/{id}.html",
                            new { controller = "Product", action = "Details" },
                            new[] { "BonSite.Web.Controllers" });

                      
            //载入配置文件中的route规则
            foreach (map map in BSConfig.RouteConfig.maps)
            {
                RouteValueDictionary defaults = new RouteValueDictionary();
                RouteValueDictionary constraints = new RouteValueDictionary();

                if (map.controller != string.Empty)
                    defaults.Add("controller", map.controller);

                if (map.action != string.Empty)
                    defaults.Add("action", map.action);

                foreach (parames param in map.parameters)
                {
                    defaults.Add(param.name, param.value);
                    if (!string.IsNullOrEmpty(param.constraint))
                    {
                        constraints.Add(param.name, param.constraint);
                    }
                }

                MapRoute(routes, map.name, map.url, defaults, constraints);
            
            }
            
            
            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "start", id = UrlParameter.Optional }, // 参数默认值
                new []{"BonSite.Web.Controllers"}

            );


        }

        protected void Application_Start()
        {

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeRazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            // 默认情况下对 Entity Framework 使用 LocalDB
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


        /// <summary>
        /// 添加自定义Route
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="constraints"></param>
        public static void MapRoute(
            RouteCollection routes,
            string name,
            string url,
            RouteValueDictionary defaults,
            RouteValueDictionary constraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            Route route = new Route(url, new MvcRouteHandler());
            route.Defaults = defaults;
            route.Constraints = constraints;
            route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add("namespaces", new string[] { "BonSite.Web.Controllers" });
            routes.Add(name, route);
        }
    }
}