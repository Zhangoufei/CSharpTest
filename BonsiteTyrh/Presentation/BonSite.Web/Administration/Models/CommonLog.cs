using BonSite.Core;
using BonSite.Core.Domain.Log;
using BonSite.Services;
using BonSite.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonSite.Web.Admin.Models
{
    public class CommonLog : BaseAdminController
    {

        


        /// <summary>
        /// 添加日志--文章
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <param name="title"></param>
        public static void AddLog(ArticleInfo articleInfo, string title,string ip)
        {
            //添加日志
            LogInfo logInfo = new LogInfo();
            logInfo.Title = title;
            logInfo.CreateTime = DateTime.Now;
            logInfo.UserName = articleInfo.Author;
            logInfo.Content = title + "-" + articleInfo.Title;
            logInfo.Ip = ip;
            Log.Add(logInfo);
        }
        /// <summary>
        /// 添加日志---分类模块
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <param name="title"></param>
        public static void AddArticleClassLog(ArticleClassInfo articleClassInfo, string title,string ip)
        {
            //添加日志
            LogInfo logInfo = new LogInfo();
            logInfo.Title = title;
            logInfo.CreateTime = DateTime.Now;
            logInfo.UserName = articleClassInfo.Auditor;
            logInfo.Content = title + "-" + articleClassInfo.ClassName;
            logInfo.Ip = ip;
            Log.Add(logInfo);
        }

        /// <summary>
        /// 添加日志---分类模块
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <param name="title"></param>
        public static void AddCommonnLog(string username,string content, string title, string ip)
        {
            //添加日志
            LogInfo logInfo = new LogInfo();
            logInfo.Title = title;
            logInfo.CreateTime = DateTime.Now;
            logInfo.UserName = username;
            logInfo.Content = title + "-" + content;
            logInfo.Ip = ip;
            Log.Add(logInfo);
        }
       
    }
}