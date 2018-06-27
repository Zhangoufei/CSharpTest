using System.Collections.Generic;
using System.Data;

using BonSite.Core;

namespace BonSite.Data
{
    public class ArticleClass
    {
        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ArticleClassInfo
        /// </summary>
        public static ArticleClassInfo BuildFromReader(IDataReader reader)
        {
            ArticleClassInfo model = new ArticleClassInfo();
            model.ArticleClassID = TypeHelper.ObjectToInt(reader["ArticleClassID"]);
            model.ClassName = reader["ClassName"].ToString();
            model.ParentArticleClassID = TypeHelper.ObjectToInt(reader["ParentArticleClassID"]);
            model.ClassType = TypeHelper.ObjectToInt(reader["ClassType"]);
            model.Target = TypeHelper.ObjectToInt(reader["Target"]);
            model.IsNav = TypeHelper.ObjectToInt(reader["IsNav"]);
            model.IsWeb = TypeHelper.ObjectToInt(reader["IsWeb"]);
            model.WebUrl = reader["WebUrl"].ToString();
            model.IsAdmin = TypeHelper.ObjectToInt(reader["IsAdmin"]);
            model.AdminUrl = reader["AdminUrl"].ToString();
            model.DisplayOrder = TypeHelper.ObjectToInt(reader["DisplayOrder"]);
            model.ListView = reader["ListView"].ToString();
            model.ContentView = reader["ContentView"].ToString();
            model.Code = reader["code"].ToString();
            model.ImgUrl = reader["ImgUrl"].ToString();
            if (reader["Keyword"] != null)
                model.Keyword = reader["Keyword"].ToString();
            else
                model.Keyword = "";

            if (reader["Description"] != null)
                model.Description = reader["Description"].ToString();
            else
                model.Description = "";
            model.IsClassBrand = TypeHelper.ObjectToInt(reader["IsClassBrand"]);
            model.Subhead = reader["Subhead"].ToString();
            model.Auditor = reader["Auditor"].ToString();
            return model;
        }

       

        #endregion
        
        #region 读写删
        /// <summary>
        /// 创建文章类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ArticleClassInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateArticleClass(model);
        }

        /// <summary>
        /// 删除文章类型
        /// </summary>
        /// <param name="articleclassid"></param>
        /// <returns></returns>
        public static bool Delete(int articleclassid)
        {
            return BonSite.Core.BSData.RDBS.DeleteArticleClassById(articleclassid);
        }

        /// <summary>
        /// 更新文章类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ArticleClassInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateArticleClass(model);
        } 
        #endregion
        
        #region 后台
        /// <summary>
        /// 获取文章类型列表
        /// </summary>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetList()
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetArticleClassList();
            while (reader.Read())
            {
                ArticleClassInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ArticleClassID"></param>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetList(int ArticleClassID ,bool isHaveChild)
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetArticleClassList(ArticleClassID, isHaveChild);
            while (reader.Read())
            {
                ArticleClassInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }


        /// <summary>
        /// 管理员获取管理菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetAdminMenu(int adminGroupId)
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetAdminMenu(adminGroupId);
            while (reader.Read())
            {
                ArticleClassInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;        
        }


      


        #endregion

        #region 前台
        /// <summary>
        /// 获取导航列表
        /// </summary>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetNavList()
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetNavList();
            while (reader.Read())
            {
                ArticleClassInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;

        }

        /// <summary>
        /// 获取可投稿栏目列表
        /// </summary>
        /// <returns></returns>
        public static List<ArticleClassInfo> GetOpenArticleClassList()
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetOpenArticleClassList();
            while (reader.Read())
            {
                ArticleClassInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
            
        }
        #endregion

    }
}
