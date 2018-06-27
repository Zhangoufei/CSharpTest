using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BonSite.Web.Framework
{
    /// <summary>
    /// 后台分页类
    /// </summary>
    public class AdminPager : Pager
    {

        private string _routename = null;//路由名
        private string _pageparamname = "pageNumber";//页参数名
        private ViewContext _viewcontext = null;//视图上下文
        private RouteValueDictionary _routevalues = new RouteValueDictionary();//路由值集合

        public AdminPager(PageModel pageModel, ViewContext viewContext)
            : base(pageModel)
        {
            _viewcontext = viewContext;

            NameValueCollection queryString = _viewcontext.RequestContext.HttpContext.Request.QueryString;
            foreach (string key in queryString.AllKeys)
            {
                string value = queryString[key];
                _routevalues.Add(key, value);
            }

            if (!_routevalues.ContainsKey(_pageparamname))
                _routevalues.Add(_pageparamname, 1);
        }

        /// <summary>
        /// 返回 HTML 编码的字符串。
        /// </summary>
        /// <returns>HTML 编码的字符串。</returns>
        public sealed override string ToHtmlString()
        {
            if (_pagemodel.TotalCount == 0 || _pagemodel.TotalCount <= _pagemodel.PageSize)
                return null;

            StringBuilder html = new StringBuilder();


            //if (_showpagesize)
            //{
            //    html.AppendFormat("每页:<input type=\"text\" value=\"{0}\" id=\"pageSize\" name=\"pageSize\" size=\"1\"/>", _pagemodel.PageSize);
            //}

            if (_showfirst)
            {
                if (_pagemodel.IsFirstPage)
                    //html.Append("<a href=\"#\">首页</a>");
                    html.Append("");
                else
                    html.AppendFormat(" <a href=\"{0}\">首页</a>", CreateUrl(1));
                    //html.Append("<a href=\"#\" page=\"1\" class=\"bt\">首页</a>");
            }

            if (_showpre)
            {
                if (_pagemodel.HasPrePage)
                    html.AppendFormat("<a href=\"{0}\">上一页</a>", CreateUrl(_pagemodel.PrePageNumber));
                //    html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">上一页</a>", _pagemodel.PageNumber - 1);
                //else
                //    html.Append("<a href=\"#\">上一页</a>");
            }

            if (_showitems)
            {
                int startPageNumber = GetStartPageNumber();
                int endPageNumber = GetEndPageNumber();
                for (int i = startPageNumber; i <= endPageNumber; i++)
                {

                    if (_pagemodel.PageNumber == i)
                        //html.AppendFormat("<a href=\"javascript:void(0)\" class=\"die\">{0}</a>", i);
                        html.AppendFormat("<a href=\"javascript:void(0)\" class=\"sel\">{0}</a>", i);
                    else
                        html.AppendFormat("<a href=\"{1}\">{0}</a>", i, CreateUrl(i));

                    //if (_pagemodel.PageNumber != i)
                    //    html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">{0}</a>", i);
                    //else
                    //    html.AppendFormat("<a href=\"\" class=\"hot\">{0}</a>", i);
                }
            }

            if (_shownext)
            {
                if (_pagemodel.HasNextPage)
                    
                    html.AppendFormat("<a href=\"{0}\">下一页</a>", CreateUrl(_pagemodel.NextPageNumber));
                    //html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">下一页</a>", _pagemodel.PageNumber + 1);
                //else
                    //html.Append("<a href=\"#\">下一页</a>");
            }

            if (_showlast)
            {
                if (_pagemodel.IsLastPage)
                    //html.Append("<a href=\"#\">末页</a>");
                    html.Append("");
                else
                    //html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">末页</a>", _pagemodel.TotalPages);
                    html.AppendFormat("<a href=\"{0}\">末页</a>", CreateUrl(_pagemodel.TotalPages));
            }

            //if (_showgopage)
            //{
            //    html.AppendFormat("跳转到:<input type=\"text\" value=\"{0}\" id=\"pageNumber\" totalPages=\"{1}\" name=\"pageNumber\" size=\"1\"/>页", _pagemodel.PageNumber, _pagemodel.TotalPages);
            //}

            if (_showsummary)
            {
                //html.Append(string.Format("<div class=\"summary\">当前{2}/{1}页&nbsp;共{0}条记录</div>", _pagemodel.TotalCount, _pagemodel.TotalPages, _pagemodel.PageNumber));
                html.Append("&nbsp;");
                html.Append(string.Format("{2}/{1}&nbsp;共{0}条记录", _pagemodel.TotalCount, _pagemodel.TotalPages, _pagemodel.PageNumber));
            }

            return html.ToString();
        }

        /// <summary>
        /// 生成链接地址
        /// </summary>
        /// <param name="pageNumber">页数</param>
        /// <returns></returns>
        private string CreateUrl(int pageNumber)
        {
            _routevalues[_pageparamname] = pageNumber;
            return UrlHelper.GenerateUrl(_routename, null, null, _routevalues, RouteTable.Routes, _viewcontext.RequestContext, true);
        }
    }
}
