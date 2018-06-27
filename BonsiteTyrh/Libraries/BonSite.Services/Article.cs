using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BonSite.Core;

namespace BonSite.Services
{
    public class Article
    {
        #region 读写删
        /// <summary>
        /// 新加文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ArticleInfo model)
        {
            return BonSite.Data.Article.Create(model);
        }

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="articleid"></param>
        /// <returns></returns>
        public static ArticleInfo GetModelByArticleID(int articleid)
        {
            if (articleid > 0)
                return BonSite.Data.Article.GetModelById(articleid);
            return null;
        }

        /// <summary>
        /// 更新文章内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ArticleInfo model)
        {
            return BonSite.Data.Article.Update(model);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static bool Del(int[] idlist)
        {
            if (idlist != null && idlist.Length > 0)
            {
                bool isSucces = BonSite.Data.Article.Delete(CommonHelper.IntArrayToString(idlist));
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_ARTICLECLASS_LIST + "\\d+");
                return isSucces;
            }
            else
            {
                return false;
            }
        } 
        #endregion

        #region 后台
        /// <summary>
        /// 后台获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetArticleList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Article.AdminGetArticleList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetArticleCount(string condition)
        {
            return BonSite.Data.Article.AdminGetArticleCount(condition);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="articleClassID"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string AdminGetArticleListCondition(int articleClassID, string title)
        {
            return BonSite.Data.Article.AdminGetArticleListCondition(articleClassID, title);
        }

        /// <summary>
        /// 后台获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetArticleListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Article.AdminGetArticleListSort(sortColumn, sortDirection);
        } 
        #endregion
        
        #region 前台
        /// <summary>
        /// 获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable GetArticleList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Article.GetArticleList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetArticleCount(string condition)
        {
            return BonSite.Data.Article.GetArticleCount(condition);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="articleClassID"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string GetArticleListCondition(int articleClassID, string title)
        {
            return BonSite.Data.Article.GetArticleListCondition(articleClassID, title);
        }

        public static string GetArticleListConditionList(List<int> articleClassID, string title) {
            return BonSite.Data.Article.GetArticleListConditionList(articleClassID, title);
        }

        /// <summary>
        /// 获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetArticleListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Article.GetArticleListSort(sortColumn, sortDirection);
        } 
        #endregion

        public static List<ArticleInfo> GetTopArticleList(int articleClassId, int count)
        {
            return BonSite.Data.Article.GetTopArticleList(articleClassId, count, "");
        }

        public static List<ArticleInfo> GetTopArticleList(string articleClassId, int count)
        {
            return BonSite.Data.Article.GetTopArticleList(articleClassId, count, "");
        }

        public static List<ArticleInfo> GetHomeArticleList(int articleClassId, int count)
        {
            return BonSite.Data.Article.GetHomeArticleList(articleClassId, count, "");
        }

        public static List<ArticleInfo> GetHomeArticleList(string articleClassId, int count)
        {
            return BonSite.Data.Article.GetHomeArticleList(articleClassId, count, "");
        }

        public static List<ArticleInfo> GetBestArticleList(int articleClassId, int count)
        {
            return BonSite.Data.Article.GetBestArticleList(articleClassId, count, "");
        }

        public static List<ArticleInfo> GetBestArticleList(string articleClassId, int count)
        {
            return BonSite.Data.Article.GetBestArticleList(articleClassId, count, "");
        }

        public static List<ArticleInfo> GetArticleList(int articleClassId, int count)
        {
            return BonSite.Data.Article.GetArticleList(articleClassId, count, "");
        }
        public static List<ArticleInfo> GetArticleList(int articleClassId, int count, string Sorting)
        {
            return BonSite.Data.Article.GetArticleList(articleClassId, count, Sorting);
        }

        public static List<ArticleInfo> GetArticleList(string articleClassId, int count)
        {
            return BonSite.Data.Article.GetArticleList(articleClassId, count, "");
        }

        public static int GetArticleIdByArticleClassId(int articleClassId)
        {
            return BonSite.Data.Article.GetArticleIdByArticleClassId(articleClassId);
        }
        public static int GetTopArticleIdByArticleClassId(int articleClassId)
        {
            return BonSite.Data.Article.GetTopArticleIdByArticleClassId(articleClassId);
        }
        public static int GetArticleClassIdByIsClassBrand()
        {
            return BonSite.Data.Article.GetArticleClassIdByIsClassBrand();
        }
        //获取下级菜单
        public static DataTable GetArtClaIdByParentArticleClassIDs(int ParentArticleClassID)
        {
            return BonSite.Data.Article.GetArtClaIdByParentArticleClassIDs(ParentArticleClassID);
        }
        //获取上级菜单名字
        public static DataTable GetNameByArticleClassID(int ArticleClassID)
        {
            return BonSite.Data.Article.GetNameByArticleClassID(ArticleClassID);
        }
        //获取上级菜单名字
        public static int GetNameByArticleClassID2(int ArticleClassID)
        {
            return BonSite.Data.Article.GetNameByArticleClassID2(ArticleClassID);
        }
        ////获取子菜单
        //public static ArticleClassInfo[] GetArtClaIdByParentArticleClassID(int ParentArticleClassID)
        //{
        //    ArticleClassInfo[] articleClassInfoList = BonSite.Data.Article.GetArtClaIdByParentArticleClassID(ParentArticleClassID);
        //    return articleClassInfoList;
        //}
    }
}
