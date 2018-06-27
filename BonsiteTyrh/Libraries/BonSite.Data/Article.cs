using System.Collections.Generic;
using System.Data;

using BonSite.Core;

namespace BonSite.Data
{
    public class Article
    {

        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ArticleInfo
        /// </summary>
        public static ArticleInfo BuildFromReader(IDataReader reader)
        {
            ArticleInfo model = new ArticleInfo();

            model.ArticleID = TypeHelper.ObjectToInt(reader["ArticleID"]);
            model.ArticleClassID = TypeHelper.ObjectToInt(reader["ArticleClassID"]);
            model.SpecialID = TypeHelper.ObjectToInt(reader["SpecialID"]);
            model.DisplayType = TypeHelper.ObjectToInt(reader["DisplayType"]);
            model.IsShow = TypeHelper.ObjectToInt(reader["IsShow"]);
            model.IsTop = TypeHelper.ObjectToInt(reader["IsTop"]);
            model.IsHome = TypeHelper.ObjectToInt(reader["IsHome"]);
            model.IsBest = TypeHelper.ObjectToInt(reader["IsBest"]);
            model.Title = reader["Title"].ToString();
            if (reader["Body"] != null)
                model.Body = reader["Body"].ToString();
            else
                model.Body = "";
            model.AddTime = TypeHelper.ObjectToDateTime(reader["AddTime"]);
            model.UpdateTime = TypeHelper.ObjectToDateTime(reader["UpdateTime"]);
            model.Author = reader["Author"].ToString();
            model.ComeForm = reader["ComeForm"].ToString();
            model.ImgUrl = reader["ImgUrl"].ToString();
            model.Url = reader["Url"].ToString();
            model.Digest = reader["Digest"].ToString();
            if (reader["Keys"] != null)
                model.Keys = reader["Keys"].ToString();
            else
                model.Keys = "";
            model.UserID = TypeHelper.ObjectToInt(reader["UserID"]);
            model.AdminID = TypeHelper.ObjectToInt(reader["AdminID"]);
            model.Hits = TypeHelper.ObjectToInt(reader["Hits"]);
            if (reader["Keyword"] != null)
                model.Keyword = reader["Keyword"].ToString();
            else
                model.Keyword = "";

            if (reader["Description"] != null)
                model.Description = reader["Description"].ToString();
            else
                model.Description = "";

            model.InformType = reader["InformType"].ToString();
            model.EndTime = TypeHelper.ObjectToDateTime(reader["EndTime"]);
            model.InformGroup = reader["InformGroup"].ToString();
            model.IsClassBrand = TypeHelper.ObjectToInt(reader["IsClassBrand"]);
            model.MicroVideo = reader["MicroVideo"].ToString();
            model.ApprovalStatus = TypeHelper.ObjectToInt(reader["ApprovalStatus"]);
            model.Praise = TypeHelper.ObjectToInt(reader["Praise"]);
            model.Auditor = reader["Auditor"].ToString();
            return model;
        }

        #endregion



        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ArticleInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateArticle(model);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleid"></param>
        /// <returns></returns>
        public static bool Delete(string articleidlist)
        {
            return BonSite.Core.BSData.RDBS.DeleteArticleById(articleidlist);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ArticleInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateArticle(model);
        }
        
        /// <summary>
        /// 获得文章
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public static ArticleInfo GetModelById(int articleid)
        {
            ArticleInfo model = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetArticleById(articleid);
            if (reader.Read())
            {
                model = BuildFromReader(reader);
            }

            reader.Close();
            return model;
        }

        /// <summary>
        /// 后台获得文章列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetArticleList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.AdminGetArticleList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得文章列表搜索条件
        /// </summary>
        /// <param name="articleClassID">文章类型id</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static string AdminGetArticleListCondition(int articleClassID, string title)
        {
            return BonSite.Core.BSData.RDBS.AdminGetArticleListCondition(articleClassID, title);
        }

        /// <summary>
        /// 后台获得文章列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetArticleListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Core.BSData.RDBS.AdminGetArticleListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得文章数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetArticleCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.AdminGetArticleCount(condition);
        }


        /// <summary>
        /// 获得文章列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable GetArticleList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.GetArticleList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 获得文章列表搜索条件
        /// </summary>
        /// <param name="articleClassID">文章类型id</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static string GetArticleListCondition(int articleClassID, string title)
        {
            return BonSite.Core.BSData.RDBS.GetArticleListCondition(articleClassID, title);
        }


        /// <summary>
        /// 获得文章列表搜索条件
        /// </summary>
        /// <param name="articleClassID">文章类型id</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static string GetArticleListConditionList(List<int> articleClassID, string title)
        {
            return BonSite.Core.BSData.RDBS.GetArticleListConditionList(articleClassID, title);
        }

        /// <summary>
        /// 获得文章列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetArticleListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Core.BSData.RDBS.GetArticleListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得文章数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetArticleCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.GetArticleCount(condition);
        }






        /// <summary>
        /// 前台获取首页推荐文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetHomeArticleList(int articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetHomeArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取首页推荐文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetHomeArticleList(string articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetHomeArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取置顶文件列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetTopArticleList(int articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetTopArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取置顶文件列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetTopArticleList(string articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetTopArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取推荐文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetBestArticleList(int articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetBestArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 前台获取推荐文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetBestArticleList(string articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetBestArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取最新文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetArticleList(int articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取最新文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetArticleList(string articleClassId, int count, string sortColumn)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetArticleList(articleClassId, count, sortColumn);
            while (reader.Read())
            {
                ArticleInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取某个分类中，第一个文章的ID
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static int GetArticleIdByArticleClassId(int articleClassId)
        {
            return BonSite.Core.BSData.RDBS.GetArticleIdByArticleClassId(articleClassId);
        }
        public static int GetTopArticleIdByArticleClassId(int articleClassId)
        {
            return BonSite.Core.BSData.RDBS.GetTopArticleIdByArticleClassId(articleClassId);
        }
        public static int GetArticleClassIdByIsClassBrand()
        {
            return BonSite.Core.BSData.RDBS.GetArticleClassIdByIsClassBrand();
        }
        //获取下级菜单
        public static DataTable GetArtClaIdByParentArticleClassIDs(int ParentArticleClassID)
        {
            return BonSite.Core.BSData.RDBS.GetArtClaIdByParentArticleClassID(ParentArticleClassID);
        }
        //获取上级菜单名字
        public static DataTable GetNameByArticleClassID(int ArticleClassID)
        {
            return BonSite.Core.BSData.RDBS.GetNameByArticleClassID(ArticleClassID);
        }
        //获取上级菜单名字
        public static int GetNameByArticleClassID2(int ArticleClassID)
        {
            return BonSite.Core.BSData.RDBS.GetNameByArticleClassID2(ArticleClassID);
        }

        ////获取子菜单
        //public static ArticleClassInfo[] GetArtClaIdByParentArticleClassID(int ParentArticleClassID)
        //{

        //    DataTable dt = BonSite.Core.BSData.RDBS.GetArtClaIdByParentArticleClassID(ParentArticleClassID);
        //    ArticleClassInfo[] articleClassInfoList = new ArticleClassInfo[dt.Rows.Count];

        //    int index = 0;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        ArticleClassInfo articleClassInfo = new ArticleClassInfo();
        //        articleClassInfo.ArticleClassID = TypeHelper.ObjectToInt(row["ArticleClassID"]);
        //        articleClassInfo.ClassName = row["ClassName"].ToString();
        //        articleClassInfo.ParentArticleClassID = TypeHelper.ObjectToInt(row["ParentArticleClassID"]);
        //        articleClassInfo.ClassType = TypeHelper.ObjectToInt(row["ClassType"]);
        //        articleClassInfo.Target = TypeHelper.ObjectToInt(row["Target"]);
        //        articleClassInfo.IsNav = TypeHelper.ObjectToInt(row["IsNav"]);
        //        articleClassInfo.IsWeb = TypeHelper.ObjectToInt(row["IsWeb"]);
        //        articleClassInfo.WebUrl = row["WebUrl"].ToString();
        //        articleClassInfo.IsAdmin = TypeHelper.ObjectToInt(row["IsAdmin"]);
        //        articleClassInfo.AdminUrl = row["AdminUrl"].ToString();
        //        articleClassInfo.DisplayOrder = TypeHelper.ObjectToInt(row["DisplayOrder"]);
        //        articleClassInfo.IsOpen = TypeHelper.ObjectToInt(row["IsOpen"]);
        //        articleClassInfo.ListView = row["ListView"].ToString();
        //        articleClassInfo.ContentView = row["ContentView"].ToString();
        //        articleClassInfo.Code = row["Code"].ToString();
        //        articleClassInfo.ImgUrl = row["ImgUrl"].ToString();
        //        articleClassInfo.Keyword = row["Keyword"].ToString();
        //        articleClassInfo.Description = row["Description"].ToString();
        //        articleClassInfo.IsClassBrand = TypeHelper.ObjectToInt(row["IsClassBrand"]);
        //        articleClassInfo.Subhead = row["Subhead"].ToString();
        //        articleClassInfoList[index] = articleClassInfo;
        //        index++;
        //    }
        //    return articleClassInfoList;
        //}
    }
}
