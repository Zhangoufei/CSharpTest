using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BonSite.Core;

namespace BonSite.Services
{
    public class ArticleClass
    {

        #region 读写删
        /// <summary>
        /// 创建新分类，并返回分类ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ArticleClassInfo model)
        {
            return BonSite.Data.ArticleClass.Create(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ArticleClassInfo model)
        {
            return BonSite.Data.ArticleClass.Update(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="articleclassid"></param>
        /// <returns></returns>
        public static bool Delete(int articleclassid)
        {
            return BonSite.Data.ArticleClass.Delete(articleclassid);
        }
        #endregion


        #region 后台

        /// <summary>
        /// 判断该分类是否有直接下级
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool HaveChild(int articleClassId)
        {
            List<ArticleClassInfo> list = BonSite.Data.ArticleClass.GetList();
            foreach (ArticleClassInfo Model in list)
            {
                if (Model.ParentArticleClassID == articleClassId)
                {
                    return true;
                }
            }
            return false;

        }




        /// <summary>
        /// 获取后台管理菜单（后边要改－增加用户的限制）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adminGroupId">1:超级管理员，列出所有菜单；2：普通管理员</param>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetAdminMenuList(int userId, int adminGroupId)
        {
            List<ArticleClassInfo> list = BonSite.Core.BSCache.Get(string.Format(CacheKeys.SITE_ADMINMENU_LIST, userId, adminGroupId)) as List<ArticleClassInfo>;
            if (list == null)
            {
                if (adminGroupId == 1)
                    list = BonSite.Data.ArticleClass.GetAdminMenu(0);
                else
                    list = BonSite.Data.ArticleClass.GetAdminMenu(adminGroupId);
                BonSite.Core.BSCache.Insert(string.Format(CacheKeys.SITE_ADMINMENU_LIST, userId, adminGroupId), list);
            }
            return list;
        }






        /// <summary>
        /// 管理员获取分类列表
        /// </summary>
        /// <returns></returns>
        public static List<ArticleClassInfo> AdminGetArticleClassTreeList()
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            List<ArticleClassInfo> slist = BonSite.Data.ArticleClass.GetList();
            AdminCreateArticleTree(slist, list, 0, "");

            //分校区 添加列表
            AdminCreateArticleTree(slist, list, -1, "");
            AdminCreateArticleTree(slist, list, -2, "");

            return list;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public static List<ArticleClassInfo> AdminGetArticleClassTreeList(int ArticleClassID, bool isHaveChild)
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            List<ArticleClassInfo> slist = BonSite.Data.ArticleClass.GetList(ArticleClassID, isHaveChild);
            AdminCreateArticleTree(slist, list, 0, "");

            //分校区 添加列表
            AdminCreateArticleTree(slist, list, -1, "");
            AdminCreateArticleTree(slist, list, -2, "");

            return slist;
        }

        protected static void AdminCreateArticleTree(List<ArticleClassInfo> sourceList, List<ArticleClassInfo> resultList, int parentId, string fontStr)
        {
            foreach (ArticleClassInfo Model in sourceList)
            {
                if (Model.ParentArticleClassID == parentId)
                {
                    Model.ClassName = fontStr + Model.ClassName;
                    resultList.Add(Model);
                    AdminCreateArticleTree(sourceList, resultList, Model.ArticleClassID, fontStr + "　　");
                }
            }
        }

        /// <summary>
        /// 管理员获取所有文章分类
        /// </summary>
        /// <returns></returns>
        public static List<ArticleClassInfo> AdminGetList()
        {
            return BonSite.Data.ArticleClass.GetList();
        }

        /// <summary>
        /// 管理员获取文章分类
        /// </summary>
        /// <param name="articleclassid"></param>
        /// <returns></returns>
        public static ArticleClassInfo AdminGetModelById(int articleclassid)
        {
            if (articleclassid > 0)
            {
                foreach (ArticleClassInfo model in BonSite.Data.ArticleClass.GetList())
                {
                    if (model.ArticleClassID == articleclassid)

                        return model;
                }
            }
            return null;
        }
        #endregion


        #region 前台

        /// <summary>
        /// 前台获得分类列表(带缓存）
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetList()
        {
            List<NavInfo> list = BonSite.Core.BSCache.Get(CacheKeys.SITE_ARTICLECLASS_LIST) as List<NavInfo>;
            if (list == null)
            {
                list = GetListByIsWeb();
                BonSite.Core.BSCache.Insert(CacheKeys.SITE_ARTICLECLASS_LIST, list);
            }
            return list;
        }


        public static List<NavInfo> GetListByIsWeb()
        {
            List<NavInfo> list = new List<NavInfo>();
            foreach (ArticleClassInfo model in BonSite.Data.ArticleClass.GetList())
            {
                if (model.IsWeb.Equals(1))
                {
                    NavInfo info = new NavInfo();
                    info.Id = model.ArticleClassID;
                    info.Pid = model.ParentArticleClassID;
                    info.Name = model.ClassName;
                    info.Target = model.Target;
                    info.DisplayOrder = model.DisplayOrder;
                    info.WebUrl = model.WebUrl;
                    if (model.ClassType > 0)
                        info.Url = string.Format("/List/{0}-1.html", model.ArticleClassID);
                    else
                        info.Url = model.WebUrl;
                    list.Add(info);
                }
            }
            return list;
        }


        /// <summary>
        /// 根据ID获得分类（带缓存）
        /// </summary>
        /// <param name="articleclassid">新闻类型id</param>
        /// <returns></returns>
        public static ArticleClassInfo GetModelById(int articleclassid)
        {
            if (articleclassid > 0)
            {
                foreach (ArticleClassInfo model in AdminGetList())
                {
                    if (model.ArticleClassID == articleclassid)
                        return model;
                }
            }
            return null;
        }


        /// <summary>
        /// 前台获取导航树
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetNavList(int type)
        {
            List<NavInfo> list = BonSite.Core.BSCache.Get(CacheKeys.SITE_NAV_LIST) as List<NavInfo>;
            //if (list == null)
            //{
            list = new List<NavInfo>();
            List<ArticleClassInfo> slist = BonSite.Data.ArticleClass.GetNavList();
            CreateNavTree(slist, list, type);
            BonSite.Core.BSCache.Insert(CacheKeys.SITE_NAV_LIST, list);
            //}
            return list;
        }

        /// <summary>
        /// 前台获取导航树
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetNavList()
        {
            //List<NavInfo> list = BonSite.Core.BSCache.Get(CacheKeys.SITE_NAV_LIST) as List<NavInfo>;
            //if (list == null)
            //{
            //list = new List<NavInfo>();

            List<NavInfo> list = new List<NavInfo>();
            List<ArticleClassInfo> slist = BonSite.Data.ArticleClass.GetNavList();
            CreateNavTree(slist, list, 0);
            BonSite.Core.BSCache.Insert(CacheKeys.SITE_NAV_LIST, list);
            //}
            return list;
        }

        /// <summary>
        /// 前台获取导航树
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetToTalNavList()
        {
            //List<NavInfo> list = BonSite.Core.BSCache.Get(CacheKeys.SITE_NAV_LIST) as List<NavInfo>;
            //if (list == null)
            //{
            //list = new List<NavInfo>();

            List<NavInfo> list = new List<NavInfo>();
            List<ArticleClassInfo> slist = BonSite.Data.ArticleClass.GetNavList();
            CreateTotalNavTree(slist, list);
            BonSite.Core.BSCache.Insert(CacheKeys.SITE_NAV_LIST, list);
            //}
            return list;
        }



        /// <summary>
        /// 获取开放投稿栏目列表
        /// </summary>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetOpenArticleClassList()
        {
            return BonSite.Data.ArticleClass.GetOpenArticleClassList();
        }

        /// <summary>
        /// 递归创建导航树
        /// </summary>
        protected static void CreateNavTree(List<ArticleClassInfo> sourceList, List<NavInfo> resultList, int parentId)
        {
            foreach (ArticleClassInfo Model in sourceList)
            {
                if (Model.ParentArticleClassID == parentId)
                {
                    NavInfo info = new NavInfo();
                    info.Id = Model.ArticleClassID;
                    info.Pid = Model.ParentArticleClassID;
                    info.Name = Model.ClassName;
                    info.Target = Model.Target;
                    info.DisplayOrder = Model.DisplayOrder;
                    info.WebUrl = Model.WebUrl;
                    if (Model.WebUrl.Length > 1)
                        info.Url = Model.WebUrl;
                    else if (Model.ClassType > 0)
                        info.Url = string.Format("/List/{0}-1.html", Model.ArticleClassID);
                    else
                        info.Url = Model.WebUrl;

                    resultList.Add(info);
                    CreateNavTree(sourceList, resultList, Model.ArticleClassID);
                }
            }
        }

        /// <summary>
        /// 递归创建导航树
        /// </summary>
        protected static void CreateTotalNavTree(List<ArticleClassInfo> sourceList, List<NavInfo> resultList)
        {
            foreach (ArticleClassInfo Model in sourceList)
            {
                if (Model.ParentArticleClassID <= 0)
                {
                    NavInfo info = new NavInfo();
                    info.Id = Model.ArticleClassID;
                    info.Pid = Model.ParentArticleClassID;
                    info.Name = Model.ClassName;
                    info.Target = Model.Target;
                    info.DisplayOrder = Model.DisplayOrder;
                    info.WebUrl = Model.WebUrl;
                    if (Model.WebUrl.Length > 1)
                        info.Url = Model.WebUrl;
                    else if (Model.ClassType > 0)
                        info.Url = string.Format("/List/{0}-1.html", Model.ArticleClassID);
                    else
                        info.Url = Model.WebUrl;

                    resultList.Add(info);
                    CreateNavTree(sourceList, resultList, Model.ArticleClassID);
                }
            }
        }

        #endregion


        #region 通用
        /// <summary>
        /// 获取分类路径Path
        /// </summary>
        /// <param name="articleClassID"></param>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetArticleClassPath(int articleClassID)
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            CreateArticleClassPath(articleClassID, list);

            return list;
        }

        /// <summary>
        /// 递归创建分类路径Path
        /// </summary>
        protected static void CreateArticleClassPath(int articleClassID, List<ArticleClassInfo> resultList)
        {
            ArticleClassInfo model = GetModelById(articleClassID);
            if (model != null)
            {
                resultList.Insert(0, model);

                if (model.ParentArticleClassID != 0)
                    CreateArticleClassPath(model.ParentArticleClassID, resultList);
            }

        }



        public static string GetClassTypeName(int classTypeId)
        {
            switch (classTypeId)
            {
                case 0:
                    return "外部链接";
                case 1:
                    return "单页";
                case 2:
                    return "文章列表";
                case 3:
                    return "图片列表";
                case -1:
                    return "管理菜单";
                case 4:
                    return "微视频";
                default:
                    return "未知";
            }

        }
        #endregion

        ///// <summary>
        ///// 获得新闻类型
        ///// </summary>
        ///// <param name="name">新闻类型名称</param>
        ///// <returns></returns>
        //public static ArticleClassInfo GetNewsTypeByName(string name)
        //{
        //    if (!string.IsNullOrWhiteSpace(name))
        //    {
        //        foreach (NewsTypeInfo newsTypeInfo in GetNewsTypeList())
        //        {
        //            if (newsTypeInfo.Name == name)
        //                return newsTypeInfo;
        //        }
        //    }
        //    return null;
        //}
    }
}
