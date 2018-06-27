using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

using BonSite.Core;
using BonSite.Core.Domain.Site;

namespace BonSite.RDBSStrategy.SqlServer
{
    public partial class RDBSStrategy : IRDBSStrategy
    {
        #region 管理员菜单
        /// <summary>
        /// 创建管理菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int CreateAdminMenu(AdminMenuInfo model)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassId", SqlDbType.Int, 4, model.ArticleClassID),
                                    GenerateInParam("@UserId", SqlDbType.Int,4,model.UserId)
                                    };

            string commandText = string.Format("INSERT INTO [{0}AdminMenu](ArticleClassId,userId) VALUES(@ArticleClassId,@userId);SELECT SCOPE_IDENTITY()",
                                                RDBSHelper.RDBSTablePre);
            //RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }
        /// <summary>
        /// 删除管理菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public bool DeleteAdminMenu(int userId, int articleClassId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassId", SqlDbType.Int, 4, articleClassId),
                                    GenerateInParam("@UserId", SqlDbType.Int,4,userId)
                                    };
            string commandText = string.Format("DELETE FROM [{0}AdminMenu] WHERE [ArticleClassId]=@ArticleClassId and [UserId]=@UserId",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除管理员菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteAdminMenu(int userId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@UserId", SqlDbType.Int,4,userId)
                                    };
            string commandText = string.Format("DELETE FROM [{0}AdminMenu] WHERE [UserId]=@UserId",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }


        public bool ExistsAdminMenu(int userId, int articleClassId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassId", SqlDbType.Int, 4, articleClassId),
                                    GenerateInParam("@UserId", SqlDbType.Int,4,userId)
                                    };
            string commandText = string.Format("Select count(*) FROM [{0}AdminMenu] WHERE [ArticleClassId]=@ArticleClassId and [UserId]=@UserId",
                                                RDBSHelper.RDBSTablePre);


            if (TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText,parms), 0) > 0)
                return true;
            else
                return false;
        }

        #endregion

        


        #region 文章分类
        /// <summary>
        /// 创建文章分类,并返回分类的ID
        /// </summary>
        public int CreateArticleClass(ArticleClassInfo model)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ClassName", SqlDbType.NVarChar, 50, model.ClassName),
                                    GenerateInParam("@ParentArticleClassID", SqlDbType.Int,4,model.ParentArticleClassID),
                                    GenerateInParam("@ClassType", SqlDbType.Int,4,model.ClassType),
                                    GenerateInParam("@Target", SqlDbType.Int,4,model.Target),
                                    GenerateInParam("@IsNav", SqlDbType.Int,4,model.IsNav),
                                    GenerateInParam("@IsWeb", SqlDbType.Int,4,model.IsWeb),
                                    GenerateInParam("@WebUrl", SqlDbType.NVarChar,250,model.WebUrl),
                                    GenerateInParam("@IsAdmin", SqlDbType.Int,4,model.IsAdmin),
                                    GenerateInParam("@AdminUrl", SqlDbType.NVarChar,250,model.AdminUrl),
                                    GenerateInParam("@DisplayOrder", SqlDbType.NVarChar,250,model.DisplayOrder),
                                    GenerateInParam("@IsOpen", SqlDbType.Int,4,model.IsOpen),
                                    GenerateInParam("@ListView", SqlDbType.NVarChar,50,model.ListView),
                                    GenerateInParam("@ContentView", SqlDbType.NVarChar,50,model.ContentView),
                                    GenerateInParam("@Code", SqlDbType.NVarChar,250,model.Code),
                                    GenerateInParam("@ImgUrl", SqlDbType.NVarChar,250,model.ImgUrl),
                                    GenerateInParam("@Keyword",SqlDbType.NVarChar,250,model.Keyword),
                                    GenerateInParam("@Description",SqlDbType.NVarChar,250,model.Description),
                                    GenerateInParam("@IsClassBrand", SqlDbType.Int,4,model.IsClassBrand),
                                    GenerateInParam("@Subhead", SqlDbType.NVarChar,255,model.Subhead),
                                    GenerateInParam("@Auditor", SqlDbType.NVarChar,255,model.Auditor),
                                    GenerateInParam("@IsShowNews", SqlDbType.Int,4,model.IsShowNews)
                                    };

            string commandText = string.Format("INSERT INTO [{0}ArticleClass](ClassName,ParentArticleClassID,ClassType,Target,IsNav,IsWeb,WebUrl,IsAdmin,AdminUrl,DisplayOrder,IsOpen,ListView,ContentView,Code,ImgUrl,Keyword,Description,IsClassBrand,Subhead,Auditor,IsShowNews) VALUES(@ClassName,@ParentArticleClassID,@ClassType,@Target,@IsNav,@IsWeb,@WebUrl,@IsAdmin,@AdminUrl,@DisplayOrder,@IsOpen,@ListView,@ContentView,@Code,@ImgUrl,@Keyword,@Description,@IsClassBrand,@Subhead,@Auditor,@IsShowNews);SELECT SCOPE_IDENTITY()",
                                                RDBSHelper.RDBSTablePre);
            //RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 删除新闻类型
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        public bool DeleteArticleClassById(int ArticleClassId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassId", SqlDbType.Int,4,ArticleClassId)
                                   };
            string commandText = string.Format("DELETE FROM [{0}Article] WHERE [ArticleClassId]=@ArticleClassId;DELETE FROM [{0}ArticleClass] WHERE [ArticleClassId]=@ArticleClassId;",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新新闻类型
        /// </summary>
        public bool UpdateArticleClass(ArticleClassInfo model)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassID", SqlDbType.Int, 4, model.ArticleClassID),
                                    GenerateInParam("@ClassName", SqlDbType.NVarChar, 50, model.ClassName),
                                    GenerateInParam("@ParentArticleClassID", SqlDbType.Int,4,model.ParentArticleClassID),
                                    GenerateInParam("@ClassType", SqlDbType.Int,4,model.ClassType),
                                    GenerateInParam("@Target", SqlDbType.Int,4,model.Target),
                                    GenerateInParam("@IsNav", SqlDbType.Int,4,model.IsNav),
                                    GenerateInParam("@IsWeb", SqlDbType.Int,4,model.IsWeb),
                                    GenerateInParam("@WebUrl", SqlDbType.NVarChar,250,model.WebUrl),
                                    GenerateInParam("@IsAdmin", SqlDbType.Int,4,model.IsAdmin),
                                    GenerateInParam("@AdminUrl", SqlDbType.NVarChar,250,model.AdminUrl),
                                    GenerateInParam("@DisplayOrder", SqlDbType.NVarChar,250,model.DisplayOrder),
                                    GenerateInParam("@IsOpen", SqlDbType.Int,4,model.IsOpen),
                                    GenerateInParam("@ListView", SqlDbType.NVarChar,50,model.ListView),
                                    GenerateInParam("@ContentView", SqlDbType.NVarChar,50,model.ContentView),
                                    GenerateInParam("@Code", SqlDbType.NVarChar,250,model.Code),
                                    GenerateInParam("@ImgUrl", SqlDbType.NVarChar,50,model.ImgUrl),
                                    GenerateInParam("@Keyword",SqlDbType.NVarChar,250,model.Keyword),
                                    GenerateInParam("@Description",SqlDbType.NVarChar,250,model.Description),
                                    GenerateInParam("@IsClassBrand", SqlDbType.Int,4,model.IsClassBrand),
                                    GenerateInParam("@Subhead", SqlDbType.NVarChar,255,model.Subhead),
                                    GenerateInParam("@Auditor", SqlDbType.NVarChar,255,model.Auditor),
                                    GenerateInParam("@IsShowNews", SqlDbType.Int,4,model.IsShowNews)

                                    };
            string commandText = string.Format("UPDATE [{0}ArticleClass] SET ClassName=@ClassName, ParentArticleClassID=@ParentArticleClassID, ClassType=@ClassType, Target=@Target, IsNav=@IsNav, IsWeb=@IsWeb, WebUrl=@WebUrl, IsAdmin=@IsAdmin, AdminUrl=@AdminUrl, DisplayOrder=@DisplayOrder, IsOpen=@IsOpen, ListView=@ListView , ContentView=@ContentView,Code=@Code,ImgUrl=@ImgUrl,Keyword=@Keyword,Description=@Description,IsClassBrand=@IsClassBrand,Subhead=@Subhead,Auditor=@Auditor,IsShowNews=@IsShowNews WHERE [ArticleClassID]=@ArticleClassID",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获得所有文章类型列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetArticleClassList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] ORDER BY [displayorder],articleClassID ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ARTICLE_CLASS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataReader GetArticleClassList(int ArticleClassID,bool isHaveChild)
        {
            string commandText = "";
            if (isHaveChild)
            {
                commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where ParentArticleClassID= {2} ORDER BY [displayorder],articleClassID ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ARTICLE_CLASS,
                                                ArticleClassID);
            }
            else
            {
                commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where ArticleClassID= {2} ORDER BY [displayorder],articleClassID ",
                                               RDBSHelper.RDBSTablePre,
                                               RDBSFields.ARTICLE_CLASS,
                                               ArticleClassID);
            }
            
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得所有主导航和前台显示的分类
        /// </summary>
        /// <returns></returns>
        public IDataReader GetNavList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where IsNav =1 ORDER BY [displayorder],articleClassID ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ARTICLE_CLASS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获取开放投稿栏目列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetOpenArticleClassList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where IsNav =1 and IsOpen=1 ORDER BY [displayorder],articleClassID ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ARTICLE_CLASS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        
        }

        /// <summary>
        /// 获取管理菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IDataReader GetAdminMenu(int adminGroupId)
        {
            string commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where IsAdmin =1 and ArticleClassId in (select ArticleClassId FROM [{0}RoleMenu] where RoleId = {2}) ORDER BY [displayorder],articleClassID ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ARTICLE_CLASS,
                                                adminGroupId);
                                             
            if (adminGroupId == 0)
                commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where IsAdmin =1  ORDER BY [displayorder],articleClassID ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ARTICLE_CLASS
                                                );
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }



        ///// <summary>
        ///// 获取管理菜单
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public IDataReader GetAdminMenu(int userId)
        //{
        //    string commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where IsAdmin =1 and ArticleClassId in (select ArticleClassId FROM [{0}RoleMenu] where RoleId = {2})  ORDER BY [displayorder],articleClassID ",
        //                                        RDBSHelper.RDBSTablePre,
        //                                        RDBSFields.ARTICLE_CLASS,
        //                                        userId);
        //    if (roleId == 0)
        //       commandText = string.Format("SELECT {1} FROM [{0}ArticleClass] where IsAdmin =1 ORDER BY [displayorder],articleClassID ",
        //                                        RDBSHelper.RDBSTablePre,
        //                                        RDBSFields.ARTICLE_CLASS,
        //                                        userId);
        //    return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        //}
        #endregion

        #region 文章
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <returns></returns>
        public int CreateArticle(ArticleInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@Body",  SqlDbType.NText, 0,model.Body),                         
                         GenerateInParam("@AddTime",  SqlDbType.DateTime,8, model.AddTime),                       
                         GenerateInParam("@UpdateTime",  SqlDbType.DateTime,8, model.UpdateTime),
                         GenerateInParam("@Author",  SqlDbType.NVarChar,50, model.Author),                        
                         GenerateInParam("@ComeForm",  SqlDbType.NVarChar,50, model.ComeForm), 
                         GenerateInParam("@ImgUrl",  SqlDbType.NVarChar,250, model.ImgUrl), 
                         GenerateInParam("@Url",  SqlDbType.NVarChar,250, model.Url),                         
                         GenerateInParam("@Digest",  SqlDbType.NText,0, model.Digest),                         
                         GenerateInParam("@Keys",  SqlDbType.VarChar,50, model.Keys),                         
                         GenerateInParam("@UserID",  SqlDbType.Int,4, model.UserID),                         
                         GenerateInParam("@ArticleClassID",  SqlDbType.Int,4, model.ArticleClassID),
                         GenerateInParam("@AdminID",  SqlDbType.Int,4, model.AdminID),                         
                         GenerateInParam("@Hits",  SqlDbType.Int,4, model.IsClassBrand),                         
                         GenerateInParam("@SpecialID",  SqlDbType.Int,4, model.SpecialID),
                         GenerateInParam("@DisplayType",  SqlDbType.Int,4, model.DisplayType),
                         GenerateInParam("@IsShow",  SqlDbType.Int,4, model.IsShow),                         
                         GenerateInParam("@IsTop",  SqlDbType.Int,4, model.IsTop),                         
                         GenerateInParam("@IsHome",  SqlDbType.Int,4, model.IsHome),                         
                         GenerateInParam("@IsBest",  SqlDbType.Int,4, model.IsBest),                         
                         GenerateInParam("@Title",  SqlDbType.NVarChar,250, model.Title),
                         GenerateInParam("@Keyword",SqlDbType.NVarChar,250,model.Keyword),
                         GenerateInParam("@Description",SqlDbType.NVarChar,250,model.Description),
                         GenerateInParam("@InformType",SqlDbType.NVarChar,250,model.InformType),
                         GenerateInParam("@EndTime",  SqlDbType.DateTime,8, model.EndTime),
                         GenerateInParam("@InformGroup",SqlDbType.NVarChar,250,model.InformGroup),
                         GenerateInParam("@IsClassBrand",  SqlDbType.Int,4, model.IsClassBrand),
                         GenerateInParam("@PushStatus",  SqlDbType.Int,4, model.PushStatus),
                         GenerateInParam("@MicroVideo",  SqlDbType.NVarChar,250, model.MicroVideo),
                         GenerateInParam("@ApprovalStatus",  SqlDbType.Int,4, model.ApprovalStatus),
                         GenerateInParam("@Praise",  SqlDbType.Int,4, model.Praise)
  
              
            };

            string commandText = string.Format("insert into {0}Article(Body,AddTime,UpdateTime,Author,ComeForm,ImgUrl,Url,Digest,Keys,UserID,ArticleClassID,AdminID,Hits,SpecialID,DisplayType,IsShow,IsTop,IsHome,IsBest,Title,Keyword,Description,InformType,EndTime,InformGroup,IsClassBrand,PushStatus,MicroVideo,ApprovalStatus,Praise) values (@Body,@AddTime,@UpdateTime,@Author,@ComeForm,@ImgUrl,@Url,@Digest,@Keys,@UserID,@ArticleClassID,@AdminID,@Hits,@SpecialID,@DisplayType,@IsShow,@IsTop,@IsHome,@IsBest,@Title,@Keyword,@Description,@InformType,@EndTime,@InformGroup,@IsClassBrand,@PushStatus,@MicroVideo,@ApprovalStatus,@Praise) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 删除文章列表
        /// </summary>
        /// <param name="ArticleIdList"></param>
        /// <returns></returns>
        public bool DeleteArticleById(string ArticleIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}Article] WHERE [ArticleId] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                ArticleIdList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateArticle(ArticleInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@Body",  SqlDbType.NText,0, model.Body),                         
                         GenerateInParam("@AddTime",  SqlDbType.DateTime,8, model.AddTime),      
                         GenerateInParam("@UpdateTime",  SqlDbType.DateTime,8, model.UpdateTime),  
                         GenerateInParam("@Author",  SqlDbType.NVarChar,50, model.Author),        
                         GenerateInParam("@ComeForm",  SqlDbType.NVarChar,50, model.ComeForm),    
                         GenerateInParam("@ImgUrl",  SqlDbType.NVarChar,250, model.ImgUrl),    
                         GenerateInParam("@Url",  SqlDbType.NVarChar,250, model.Url),                         
                         GenerateInParam("@Digest",  SqlDbType.NText,0, model.Digest),                         
                         GenerateInParam("@Keys",  SqlDbType.VarChar,50, model.Keys),                         
                         GenerateInParam("@UserID",  SqlDbType.Int,4, model.UserID),                         
                         GenerateInParam("@ArticleClassID",  SqlDbType.Int,4, model.ArticleClassID), 
                         GenerateInParam("@AdminID",  SqlDbType.Int,4, model.AdminID),                         
                         GenerateInParam("@Hits",  SqlDbType.Int,4, model.Hits),                         
                         GenerateInParam("@SpecialID",  SqlDbType.Int,4, model.SpecialID),                        
                         GenerateInParam("@DisplayType",  SqlDbType.Int,4, model.DisplayType),  
                         GenerateInParam("@IsShow",  SqlDbType.Int,4, model.IsShow),                         
                         GenerateInParam("@IsTop",  SqlDbType.Int,4, model.IsTop),                         
                         GenerateInParam("@IsHome",  SqlDbType.Int,4, model.IsHome),                         
                         GenerateInParam("@IsBest",  SqlDbType.Int,4, model.IsBest),                         
                         GenerateInParam("@Title",  SqlDbType.NVarChar,250, model.Title),
                         GenerateInParam("@Keyword",SqlDbType.NVarChar,250,model.Keyword),
                         GenerateInParam("@Description",SqlDbType.NVarChar,250,model.Description),
                         GenerateInParam("@ArticleID",SqlDbType.Int,4,model.ArticleID),
                         GenerateInParam("@InformType",SqlDbType.NVarChar,250,model.InformType),
                         GenerateInParam("@EndTime",  SqlDbType.DateTime,8, model.EndTime),
                         GenerateInParam("@InformGroup",SqlDbType.NVarChar,250,model.InformGroup),
                         GenerateInParam("@IsClassBrand",  SqlDbType.Int,4, model.IsClassBrand),
                         GenerateInParam("@PushStatus",  SqlDbType.Int,4, model.PushStatus),
                         GenerateInParam("@MicroVideo",  SqlDbType.NVarChar,250, model.MicroVideo),
                         GenerateInParam("@ApprovalStatus",  SqlDbType.Int,4, model.ApprovalStatus),
                         GenerateInParam("@Praise",  SqlDbType.Int,4, model.Praise),
                         GenerateInParam("@Auditor",  SqlDbType.NVarChar,250, model.Auditor)                        
              
            };

            string commandText = string.Format("update {0}Article set Body = @Body , AddTime = @AddTime , UpdateTime = @UpdateTime , Author = @Author , ComeForm = @ComeForm , ImgUrl = @ImgUrl , Url = @Url , Digest = @Digest , Keys = @Keys , UserID = @UserID , ArticleClassID = @ArticleClassID , AdminID = @AdminID , Hits = @Hits , SpecialID = @SpecialID , DisplayType = @DisplayType , IsShow = @IsShow , IsTop = @IsTop , IsHome = @IsHome , IsBest = @IsBest , Title = @Title,Keyword=@Keyword,Description=@Description,InformType=@InformType,EndTime=@EndTime,InformGroup=@InformGroup,IsClassBrand=@IsClassBrand,PushStatus=@PushStatus,MicroVideo=@MicroVideo,ApprovalStatus=@ApprovalStatus,Praise=@Praise,Auditor=@Auditor where ArticleID=@ArticleID ",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据ID获取文章内容
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IDataReader GetArticleById(int articleId)
        {

            DbParameter[] parms = {
                                    GenerateInParam("@ArticleID", SqlDbType.Int, 4, articleId)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}Article] WHERE [ArticleID]=@ArticleID",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ARTICLE);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获取文章列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public DataTable AdminGetArticleList(int pageSize, int pageNumber, string condition, string sort)
        {

            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Article] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.ARTICLE,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} {5} FROM [{2}Article] {6} WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.ARTICLE,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition,
                                                " , bs_User.UserName, bs_User.NickName ",
                                                " left join [bs_User] on bs_Article.AdminID = bs_User.UserID ");
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Article]) AS [temp] WHERE [bs_Article].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.ARTICLE,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} {6} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Article] WHERE {5}) AS [bs_Article] {7} WHERE [bs_Article].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.ARTICLE,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition
                                                , " , bs_User.UserName, bs_User.NickName "
                                                , " left join [bs_User] on [bs_Article].AdminID = bs_User.UserID ");
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得新闻列表搜索条件
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public string AdminGetArticleListCondition(int articleClassId, string title)
        {
            StringBuilder condition = new StringBuilder();

            if (articleClassId > 0)
                condition.AppendFormat(" AND [ArticleClassID] = {0} ", articleClassId);
            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [title] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }
        
        /// <summary>
        /// 后台获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string AdminGetArticleListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[AddTime]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获取文章数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int AdminGetArticleCount(string condition)
        {

            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ArticleID) FROM [{0}Article]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ArticleID) FROM [{0}Article] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }


        /// <summary>
        /// 前台获取文章搜索条件
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetArticleListCondition(int articleClassId, string title)
        {
            StringBuilder condition = new StringBuilder();

            if (articleClassId > 0)
                condition.AppendFormat(" And IsShow >0 AND [articleClassID] = {0}", articleClassId);
            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" And IsShow >0 AND [title] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }


        /// <summary>
        /// 前台获取文章搜索条件
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetArticleListConditionList(List<int> articleClassId, string title)
        {
            StringBuilder condition = new StringBuilder();

            if (articleClassId.Count > 0) {
                string temp = "";
                foreach (var item in articleClassId)
                {
                    temp += item+",";
                }
                condition.AppendFormat(" And IsShow >0 AND [articleClassID] in ({0})", temp.Substring(0,temp.Length-1));
            }
            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" And IsShow >0 AND [title] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }




        /// <summary>
        /// 获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string GetArticleListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[AddTime]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }
        //显示审核通过的文章
        public DataTable GetArticleList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Article] WHERE ApprovalStatus=1 ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.ARTICLEbefore,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Article] WHERE {4} and ApprovalStatus=1 ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.ARTICLEbefore,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Article]) AS [temp] WHERE ApprovalStatus=1 and [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.ARTICLEbefore,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Article] WHERE {5}) AS [temp] WHERE ApprovalStatus=1 and [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.ARTICLEbefore,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获取文章数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// 审核通过的文章数量
        public int GetArticleCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ArticleID) FROM [{0}Article] WHERE ApprovalStatus=1 ", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ArticleID) FROM [{0}Article] WHERE {1} and ApprovalStatus=1", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }



        #endregion

        #region 文章首页调用

        /// <summary>
        /// 获取首页文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        /// 审核通过的文章
        public IDataReader GetHomeArticleList(int articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (articleClassId == 0)
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where isShow = 1 and isHome = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article] where isShow = 1 and isHome = 1 and ApprovalStatus=1 and ArticleClassID = {2} Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获取首页文章列表
        /// </summary>
        /// <param name="articleClassId">分类ID，中间以,分割</param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        /// 审核通过的文章
        public IDataReader GetHomeArticleList(string articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (string.IsNullOrWhiteSpace(articleClassId))
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where isShow = 1 and isHome = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article]  where isShow = 1 and isHome = 1 and ApprovalStatus=1 and ArticleClassID in ({2}) Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }
        //显示审核通过的文章
        public IDataReader GetTopArticleList(int articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (articleClassId == 0)
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where isShow = 1 and isTop = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article] where isShow = 1 and isTop = 1 and ApprovalStatus=1 and ArticleClassID = {2} Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }
        //显示审核通过的文章
        public IDataReader GetTopArticleList(string articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (string.IsNullOrWhiteSpace(articleClassId))
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where  isShow = 1 and isTop = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article]  where isShow = 1 and isTop = 1 and ApprovalStatus=1 and ArticleClassID in ({2}) Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }
        //显示审核通过的文章
        public IDataReader GetBestArticleList(int articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (articleClassId == 0)
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where isShow = 1 and isBest = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article] where isShow = 1 and isBest = 1 and ApprovalStatus=1 and ArticleClassID = {2} Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }
        //推荐，通过审核的文章
        public IDataReader GetBestArticleList(string articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (string.IsNullOrWhiteSpace(articleClassId))
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where isShow = 1 and isBest = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article]  where isShow = 1 and isBest = 1 and ApprovalStatus=1 and ArticleClassID in ({2}) Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        //public int GetArticleIdByArticleClassId(int ArticleClassId)
        //{
        //    throw new NotImplementedException();
        //}

        //显示通过审核的文章
        public IDataReader GetArticleList(int articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (articleClassId == 0)
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where isShow = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article] where isShow = 1 and ApprovalStatus=1 and ArticleClassID = {2} Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }
        //显示通过审核的文章
        public IDataReader GetArticleList(string articleClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "AddTime Desc";

            string commandText;
            if (string.IsNullOrWhiteSpace(articleClassId))
                commandText = string.Format("SELECT top {2} {1} from [{0}Article] where isShow = 1 and ApprovalStatus=1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Article]  where isShow = 1 and ApprovalStatus=1 and ArticleClassID in ({2}) Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.ARTICLE,
                                            articleClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }


        public int GetArticleIdByArticleClassId(int ArticleClassId)
        {
            string commandText;
            commandText = string.Format("SELECT ArticleID from [{0}Article]  where ArticleClassID = {1} and ApprovalStatus=1 Order By AddTime desc",
                                             RDBSHelper.RDBSTablePre,
                                             ArticleClassId);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }
        //审核通过的文章
        public int GetTopArticleIdByArticleClassId(int ArticleClassId)
        {
            string commandText;
            commandText = string.Format("SELECT TOP 1 ArticleID from [{0}Article]  where ArticleClassID = {1} and ApprovalStatus=1 Order By AddTime desc",
                                             RDBSHelper.RDBSTablePre,
                                             ArticleClassId);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }
        public int GetArticleClassIdByIsClassBrand()
        {
            string commandText;
            commandText = string.Format("SELECT  ArticleClassId from [{0}Article]  where isClassBrand=1 and ApprovalStatus=1",
                                             RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        } 
        //通过父级ID查找下级菜单
        public DataTable GetArtClaIdByParentArticleClassID(int ParentArticleClassID)
        {
            string commandText;
            commandText = string.Format("SELECT * from [{0}ArticleClass]  where ParentArticleClassID = {1} Order By ArticleClassID desc",
                                             RDBSHelper.RDBSTablePre,
                                             ParentArticleClassID);
            //return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        //根据子菜单获取父菜单名字
        public DataTable GetNameByArticleClassID(int ArticleClassID)
        {
            string commandText;
            commandText = string.Format("SELECT * from [{0}ArticleClass]  where ArticleClassID = {1} Order By ArticleClassID desc",
                                             RDBSHelper.RDBSTablePre,
                                             ArticleClassID);
            //return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        //根据子菜单获取父菜单名字
        public int GetNameByArticleClassID2(int ArticleClassID)
        {
            int ParentArticleClassID = 0;
            string commandText;
            commandText = string.Format("SELECT * from [{0}ArticleClass]  where ArticleClassID = {1} Order By ArticleClassID desc",
                                             RDBSHelper.RDBSTablePre,
                                             ArticleClassID);
            //return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
            DataTable dd = RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
            if (dd.Rows.Count > 0)
            {
                if (dd.Rows[0]["ParentArticleClassID"] != null && !string.IsNullOrEmpty(dd.Rows[0]["ParentArticleClassID"].ToString()))
                {
                    ParentArticleClassID = Convert.ToInt32(dd.Rows[0]["ParentArticleClassID"].ToString());
                }
            }
            
            return ParentArticleClassID;
            
        }

       
        #endregion

        #region 专题
        /// <summary>
        /// 创建专题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int CreateSpecial(SpecialInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@Name",  SqlDbType.NVarChar,50, model.Name),                         
                         GenerateInParam("@Title",  SqlDbType.NVarChar,250, model.Title),                         
                         GenerateInParam("@ImgUrl",  SqlDbType.NVarChar,250, model.ImgUrl),                         
                         GenerateInParam("@LogoUrl",  SqlDbType.NVarChar,250, model.LogoUrl),                         
                         GenerateInParam("@IsOut",  SqlDbType.Int,4, model.IsOut),                         
                         GenerateInParam("@OutUrl",  SqlDbType.NVarChar,250, model.OutUrl),                         
                         GenerateInParam("@Body",  SqlDbType.NText,0, model.Body),                         
                         GenerateInParam("@DisplayOrder",  SqlDbType.Int,4, model.DisplayOrder) 
            };
            string commandText = string.Format("insert into {0}Special(Name,Title,ImgUrl,LogoUrl,IsOut,OutUrl,Body,DisplayOrder) values (@Name,@Title,@ImgUrl,@LogoUrl,@IsOut,@OutUrl,@Body,@DisplayOrder) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        public bool DeleteSpecialById(int specialID)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@SpecialID", SqlDbType.Int,4,specialID)
                                   };
            string commandText = string.Format("DELETE FROM [{0}Article] WHERE [SpecialID]=@SpecialID;DELETE FROM [{0}Special] WHERE [SpecialID]=@SpecialID;",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新专题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSpecial(SpecialInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@SpecialID",  SqlDbType.Int,4, model.SpecialID),                         
                         GenerateInParam("@Name",  SqlDbType.NVarChar,50, model.Name),                         
                         GenerateInParam("@Title",  SqlDbType.NVarChar,250, model.Title),                         
                         GenerateInParam("@ImgUrl",  SqlDbType.NVarChar,250, model.ImgUrl),                         
                         GenerateInParam("@LogoUrl",  SqlDbType.NVarChar,250, model.LogoUrl),                         
                         GenerateInParam("@IsOut",  SqlDbType.Int,4, model.IsOut),                         
                         GenerateInParam("@OutUrl",  SqlDbType.NVarChar,250, model.OutUrl),                         
                         GenerateInParam("@Body",  SqlDbType.NText,0, model.Body),                         
                         GenerateInParam("@DisplayOrder",  SqlDbType.Int,4, model.DisplayOrder)                         
              
            };

            string commandText = string.Format("update {0}Special set Name = @Name , Title = @Title , ImgUrl = @ImgUrl , LogoUrl = @LogoUrl , IsOut = @IsOut , OutUrl = @OutUrl , Body = @Body , DisplayOrder = @DisplayOrder   where SpecialID=@SpecialID ",
                                                RDBSHelper.RDBSTablePre);


            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据ID获取专题内容
        /// </summary>
        /// <param name="specialID"></param>
        /// <returns></returns>
        public IDataReader GetSpecialById(int specialID)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@Special", SqlDbType.Int, 4, specialID)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}Special] WHERE [SpecialID]=@specialID",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.SPECIAL);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取所有专题
        /// </summary>
        /// <returns></returns>
        public IDataReader GetSpecialList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}Special] order by DisplayOrder",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.SPECIAL);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获取前N条专题
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IDataReader GetTopSpecialList(int count)
        {
            string commandText = string.Format("SELECT top {2} {1} FROM [{0}Special] order by DisplayOrder",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.SPECIAL,
                                                count);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        
        }

        #endregion

        #region 班级管理
        public DataTable GetClassManageList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}classManage]",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.CLASSMANAGE);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 创建班级
        /// </summary>
        /// <param name="friendLinkInfo"></param>
        /// <returns></returns>
        public int CreateClassManage(ClassManageInfo classManageInfo)
        {

            DbParameter[] parms = {
                                         GenerateInParam("@ClassName",SqlDbType.NVarChar, 50, classManageInfo.ClassName)
                                       };
            string commandText = String.Format("INSERT INTO [{0}ClassManage] ([ClassName]) VALUES(@ClassName);select @@IDENTITY;",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }
        /// <summary>
        /// 更新班级
        /// </summary>
        /// <param name="classManageInfo"></param>
        /// <returns></returns>
        public bool UpdateClassManage(ClassManageInfo classManageInfo)
        {
            DbParameter[] parms = {
                                       GenerateInParam("@ClassID",SqlDbType.Int, 0, classManageInfo.ClassID),
                                         GenerateInParam("@ClassName",SqlDbType.NVarChar, 50, classManageInfo.ClassName)                                        
                                       };
            string commandText = String.Format("UPDATE [{0}ClassManage] SET [ClassName]=@ClassName WHERE [ClassID]=@ClassID",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteClassManageById(string idList)
        {
            string commandText = String.Format("DELETE FROM [{0}ClassManage] WHERE [ClassID] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                idList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        } 

        #endregion

        #region 友情连接
        /// <summary>
        /// 获取友情连接列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetFriendLinkList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}friendlinks] ORDER BY [displayorder] ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FRIENDLINKS);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 创建友情连接
        /// </summary>
        /// <param name="friendLinkInfo"></param>
        /// <returns></returns>
        public int CreateFriendLink(FriendLinkInfo friendLinkInfo)
        {

            DbParameter[] parms = {
                                         GenerateInParam("@name",SqlDbType.NVarChar, 50, friendLinkInfo.Name),
                                         GenerateInParam("@title",SqlDbType.NVarChar, 110, friendLinkInfo.Title),
                                         GenerateInParam("@logo",SqlDbType.NVarChar, 250, friendLinkInfo.Logo),
                                         GenerateInParam("@url",SqlDbType.NVarChar, 250, friendLinkInfo.Url),
                                         GenerateInParam("@target",SqlDbType.Int, 4, friendLinkInfo.Target),
                                         GenerateInParam("@displayorder",SqlDbType.Int,4,friendLinkInfo.DisplayOrder)
                                       };
            string commandText = String.Format("INSERT INTO [{0}friendlinks] ([name],[title],[logo],[url],[target],[displayorder]) VALUES(@name,@title,@logo,@url,@target,@displayorder);select @@IDENTITY;",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 更新友情连接
        /// </summary>
        /// <param name="friendLinkInfo"></param>
        /// <returns></returns>
        public bool UpdateFriendLink(FriendLinkInfo friendLinkInfo)
        {
            DbParameter[] parms = {
                                         GenerateInParam("@name",SqlDbType.NVarChar, 50, friendLinkInfo.Name),
                                         GenerateInParam("@title",SqlDbType.NVarChar, 110, friendLinkInfo.Title),
                                         GenerateInParam("@logo",SqlDbType.NVarChar, 250, friendLinkInfo.Logo),
                                         GenerateInParam("@url",SqlDbType.NVarChar, 250, friendLinkInfo.Url),
                                         GenerateInParam("@target",SqlDbType.Int, 4, friendLinkInfo.Target),
                                         GenerateInParam("@displayorder",SqlDbType.Int,4,friendLinkInfo.DisplayOrder),
                                         GenerateInParam("@id",SqlDbType.Int, 4, friendLinkInfo.Id)
                                       };
            string commandText = String.Format("UPDATE [{0}friendlinks] SET [name]=@name,[title]=@title,[logo]=@logo,[url]=@url,[target]=@target,[displayorder]=@displayorder WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除友情连接
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteFriendLinkById(string idList)
        {
            string commandText = String.Format("DELETE FROM [{0}friendlinks] WHERE [id] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                idList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        } 
        #endregion

        #region 角色管理

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserRoleList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}userRole]",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.USERROLE);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="friendLinkInfo"></param>
        /// <returns></returns>
        public int CreateUserRole(UserRoleInfo userRoleInfo)
        {

            DbParameter[] parms = {
                                         GenerateInParam("@RoleName",SqlDbType.NVarChar, 50, userRoleInfo.RoleName),
                                         GenerateInParam("@Remark",SqlDbType.NVarChar, 50, userRoleInfo.Remark)
                                         
                                       };
            string commandText = String.Format("INSERT INTO [{0}userRole] ([RoleName],[Remark]) VALUES(@RoleName,@Remark);select @@IDENTITY;",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);

        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="userRoleInfo"></param>
        /// <returns></returns>
        public bool UpdateUserRole(UserRoleInfo userRoleInfo)
        {
            DbParameter[] parms = {
                                       GenerateInParam("@RoleID",SqlDbType.Int, 0, userRoleInfo.RoleID),
                                         GenerateInParam("@RoleName",SqlDbType.NVarChar, 50, userRoleInfo.RoleName),
                                         GenerateInParam("@Remark",SqlDbType.NVarChar, 50, userRoleInfo.Remark) 
                                       };
            string commandText = String.Format("UPDATE [{0}userRole] SET [RoleName]=@RoleName,[Remark]=@Remark WHERE [RoleID]=@RoleID",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteUserRoleId(string RoleIDList)
        {
            string commandText = String.Format("DELETE FROM [{0}userRole] WHERE [RoleID] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                RoleIDList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 创建角色菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int CreateRoleMenu(RoleMenuInfo model)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassId", SqlDbType.Int, 4, model.ArticleClassID),
                                    GenerateInParam("@RoleID", SqlDbType.Int,4,model.RoleId)
                                    };

            string commandText = string.Format("INSERT INTO [{0}RoleMenu](ArticleClassId,RoleID) VALUES(@ArticleClassId,@RoleID);SELECT SCOPE_IDENTITY()",
                                                RDBSHelper.RDBSTablePre);
            //RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }


        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ArticleClassId"></param>
        /// <returns></returns>
        public bool DeleteRoleMenu(int RoleID, int ArticleClassId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassId", SqlDbType.Int, 4, ArticleClassId),
                                    GenerateInParam("@RoleID", SqlDbType.Int,4,RoleID)
                                    };
            string commandText = string.Format("DELETE FROM [{0}RoleMenu] WHERE [ArticleClassId]=@ArticleClassId and [RoleID]=@RoleID",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public bool DeleteRoleMenu(int RoleID)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@RoleID", SqlDbType.Int,4,RoleID)
                                    };
            string commandText = string.Format("DELETE FROM [{0}RoleMenu] WHERE [RoleID]=@RoleID",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }


        public bool ExistsRoleMenu(int RoleID, int ArticleClassId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ArticleClassId", SqlDbType.Int, 4, ArticleClassId),
                                    GenerateInParam("@RoleID", SqlDbType.Int,4,RoleID)
                                    };
            string commandText = string.Format("Select count(*) FROM [{0}RoleMenu] WHERE [ArticleClassId]=@ArticleClassId and [RoleID]=@RoleID",
                                                RDBSHelper.RDBSTablePre);


            if (TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), 0) > 0)
                return true;
            else
                return false;
        }


        #endregion

        #region 公众号管理
        /// 获取公众号列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetWeChatList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}WxOfficeAccount]",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.WXOffICIAlACCOUNTS);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 创建公众号
        /// </summary>
        /// <param name="friendLinkInfo"></param>
        /// <returns></returns>
        public int CreateWeChat(WeChatInfo weChatInfo)
        {

            DbParameter[] parms = {
                                         GenerateInParam("@name",SqlDbType.NVarChar, 50, weChatInfo.name),
                                         GenerateInParam("@appid",SqlDbType.NVarChar, 100, weChatInfo.appid),
                                         GenerateInParam("@secret",SqlDbType.NVarChar, 100, weChatInfo.secret)
                                         
                                       };
            string commandText = String.Format("INSERT INTO [{0}WxOfficeAccount] ([name],[appid],[secret]) VALUES(@name,@appid,@secret);select @@IDENTITY;",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);

        }
        /// <summary>
        /// 更新公众号
        /// </summary>
        /// <param name="userRoleInfo"></param>
        /// <returns></returns>
        public bool UpdateWeChat(WeChatInfo weChatInfo)
        {
            DbParameter[] parms = {         
                                       GenerateInParam("@id",SqlDbType.Int, 11, weChatInfo.id),
                                       GenerateInParam("@name",SqlDbType.NVarChar, 50, weChatInfo.name),
                                         GenerateInParam("@appid",SqlDbType.NVarChar, 100, weChatInfo.appid),
                                         GenerateInParam("@secret",SqlDbType.NVarChar, 100, weChatInfo.secret) 
                                       };
            string commandText = String.Format("UPDATE [{0}WxOfficeAccount] SET [name]=@name,[appid]=@appid,[secret]=@secret WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除公众号
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteWeChat(string idList)
        {
            string commandText = String.Format("DELETE FROM [{0}WxOfficeAccount] WHERE [id] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                idList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }
        

        #endregion

        #region Banner

        /// <summary>
        /// 前台根据位置，获取Banner列表
        /// </summary>
        /// <param name="nowTime"></param>
        /// <param name="BanPosId"></param>
        /// <returns></returns>
        public DataTable GetBannerList(DateTime nowTime, int banPosId)
        {
             DbParameter[] param = {
                                    GenerateInParam("@nowtime",SqlDbType.DateTime,8,nowTime)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}banners] WHERE [starttime]<=@nowtime AND [endtime]>@nowtime AND [isshow]=1 And BanPosId = {2} ORDER BY [displayorder] DESC,[id] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.BANNERS,
                                                banPosId
                                                );
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText, param).Tables[0];
        }

        /// <summary>
        /// 根据位置ID获取Banner列表
        /// </summary>
        /// <param name="banPosId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public DataTable AdminGetBannerList(int pageSize, int pageNumber,int banPosId)
        {
            string commandText;
            if (pageNumber == 1)
            {
                if (banPosId < 1)
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[banposid],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[IsShow],[temp1].[displayorder],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[banposid],[title],[starttime],[endtime],[isShow],[displayorder] FROM [{1}Banners] ORDER BY [banposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}Bannerspositions] AS [temp2] ON [temp1].[banposid]=[temp2].[banposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre);

                else
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[banposid],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[IsShow],[temp1].[displayorder],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[banposid],[title],[starttime],[endtime],[isShow],[displayorder] FROM [{1}Banners] WHERE [banposid]={2} ORDER BY [banposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}bannerspositions] AS [temp2] ON [temp1].[banposid]=[temp2].[banposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                banPosId);
            }
            else
            {
                if (banPosId < 1)
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[banposid],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[IsShow],[temp1].[displayorder],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[banposid],[title],[starttime],[endtime],[isShow],[displayorder] FROM [{1}banners] WHERE [id] NOT IN (SELECT TOP {2} [id] FROM [{1}banners] ORDER BY [banposid] DESC,[displayorder] DESC) ORDER BY [banposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}bannerspositions] AS [temp2] ON [temp1].[banposid]=[temp2].[banposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize);
                else
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[banposid],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[IsShow],[temp1].[displayorder],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[banposid],[type],[title],[starttime],[endtime],[isShow],[displayorder] FROM [{1}banners] WHERE [banposid]={3} AND [id] NOT IN (SELECT TOP {2} [id] FROM [{1}banners] WHERE [banposid]={3} ORDER BY [banposid] DESC,[displayorder] DESC) ORDER BY [banposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}bannerspositions] AS [temp2] ON [temp1].[banposid]=[temp2].[banposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                banPosId);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获取Banner数量
        /// </summary>
        /// <returns></returns>
        public int AdminGetBannerCount(int banPosId)
        {
            string commandText = string.Format("SELECT COUNT(id) FROM [{0}banners]", RDBSHelper.RDBSTablePre);
            if (banPosId > 0)
                commandText += string.Format(" where banPosId = {0}", banPosId);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 后台获得banner
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public IDataReader AdminGetBannerById(int id)
        {
            DbParameter[] param = {
                                    GenerateInParam("@id",SqlDbType.Int,4,id)
                                   };
            string commandText = String.Format("SELECT {1} FROM [{0}banners] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.BANNERS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 创建banner
        /// </summary>
        public int CreateBanner(BannerInfo bannerInfo)
        {
            DbParameter[] param = {
                                    GenerateInParam("@BanPosId",SqlDbType.Int,4,bannerInfo.BanPosId),
                                    GenerateInParam("@starttime",SqlDbType.DateTime, 8, bannerInfo.StartTime),
                                    GenerateInParam("@endtime",SqlDbType.DateTime, 8, bannerInfo.EndTime),
                                    GenerateInParam("@isshow",SqlDbType.TinyInt, 1, bannerInfo.IsShow),
                                    GenerateInParam("@title",SqlDbType.NVarChar, 100, bannerInfo.Title),
                                    GenerateInParam("@img",SqlDbType.NVarChar, 250, bannerInfo.Img),
                                    GenerateInParam("@url",SqlDbType.NVarChar, 250, bannerInfo.Url),
                                    GenerateInParam("@displayorder",SqlDbType.Int,4,bannerInfo.DisplayOrder)
                                   };
            string commandText = String.Format("INSERT INTO [{0}banners]([BanPosId],[starttime],[endtime],[isshow],[title],[img],[url],[displayorder]) VALUES(@BanPosId,@starttime,@endtime,@isshow,@title,@img,@url,@displayorder);select @@IDENTITY;",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, param), -1);
        }

        /// <summary>
        /// 更新Banner
        /// </summary>
        /// <param name="bannerInfo"></param>
        public void UpdateBanner(BannerInfo bannerInfo)
        {
            DbParameter[] param = {
                                    GenerateInParam("@BanPosId",SqlDbType.Int,4,bannerInfo.BanPosId),
                                    GenerateInParam("@starttime",SqlDbType.DateTime, 8, bannerInfo.StartTime),
                                    GenerateInParam("@endtime",SqlDbType.DateTime, 8, bannerInfo.EndTime),
                                    GenerateInParam("@isshow",SqlDbType.TinyInt, 1, bannerInfo.IsShow),
                                    GenerateInParam("@title",SqlDbType.NVarChar, 100, bannerInfo.Title),
                                    GenerateInParam("@img",SqlDbType.NVarChar, 250, bannerInfo.Img),
                                    GenerateInParam("@url",SqlDbType.NVarChar, 250, bannerInfo.Url),
                                    GenerateInParam("@displayorder",SqlDbType.Int,4,bannerInfo.DisplayOrder),
                                    GenerateInParam("@id",SqlDbType.Int, 4, bannerInfo.Id)
                                  };
            string commandText = String.Format("UPDATE [{0}banners] SET [BanPosId]=@BanPosId,[starttime]=@starttime,[endtime]=@endtime,[isshow]=@isshow,[title]=@title,[img]=@img,[url]=@url,[displayorder]=@displayorder WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 删除Banner
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBannerById(int id)
        {
            string commandText = String.Format("DELETE FROM [{0}banners] WHERE [id] ={1}",
                                                RDBSHelper.RDBSTablePre,
                                                id);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 管理员获取Banner位置列表
        /// </summary>
        /// <returns></returns>
        public IDataReader AdminGetBannerPositionList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}bannersPositions] ORDER BY [BanPosId] DESC",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.BANNERSPOSITIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader AdminGetBannerPositionByBanPosId(int banPosId)
        {
            DbParameter[] param = {
                                    GenerateInParam("@banPosId",SqlDbType.Int,4,banPosId)
                                   };
            string commandText = String.Format("SELECT {1} FROM [{0}bannersPositions] WHERE [BanPosId]=@banPosId",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.BANNERSPOSITIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 创建Banner位置
        /// </summary>
        /// <param name="bannerPositionInfo"></param>
        /// <returns></returns>
        public int CreateBannerPosition(BannerPositionInfo bannerPositionInfo)
        {
            DbParameter[] param = {
                                    GenerateInParam("@BanPosId",SqlDbType.Int,4,bannerPositionInfo.BanPosId),  
                                    GenerateInParam("@title",SqlDbType.NVarChar, 250, bannerPositionInfo.Title),
                                    GenerateInParam("@Description",SqlDbType.NVarChar, 250, bannerPositionInfo.Description)
                                   };
            string commandText = String.Format("INSERT INTO [{0}bannersPositions]([title],[Description]) VALUES(@title,@Description);select @@IDENTITY;",
                                                RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, param), -1);
        }

        /// <summary>
        /// 更新Banner位置
        /// </summary>
        /// <param name="bannerPositionInfo"></param>
        public void UpdateBannerPosition(BannerPositionInfo bannerPositionInfo)
        {
            DbParameter[] param = {
                                    GenerateInParam("@BanPosId",SqlDbType.Int,4,bannerPositionInfo.BanPosId),  
                                    GenerateInParam("@title",SqlDbType.NVarChar, 250, bannerPositionInfo.Title),
                                    GenerateInParam("@Description",SqlDbType.NVarChar, 250, bannerPositionInfo.Description)
                                  };
            string commandText = String.Format("UPDATE [{0}bannersPositions] SET [title]=@title,[Description]=@Description WHERE [BanPosId]=@BanPosId",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 删除Banner位置
        /// </summary>
        /// <param name="banPosId"></param>
        public void DeleteBannerPosition(int banPosId)
        {
            string commandText = String.Format("DELETE FROM [{0}bannersPositions] WHERE [banPosId] ={1}",
                                                RDBSHelper.RDBSTablePre,
                                                banPosId);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText);
        } 
        #endregion

        #region 广告

        /// <summary>
        /// 获得广告位置列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IDataReader GetAdvertPositionList(int pageSize, int pageNumber)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT TOP {2} {1} FROM [{0}advertpositions]",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ADVERT_POSITIONS,
                                                pageSize);
            }
            else
            {
                commandText = string.Format("SELECT TOP {2} {1} FROM [{0}advertpositions] WHERE [adposid] > (SELECT MAX([adposid]) FROM (SELECT TOP {3} [adposid] FROM [{0}advertpositions]) AS [temp])",
                                               RDBSHelper.RDBSTablePre,
                                               RDBSFields.ADVERT_POSITIONS,
                                               pageSize,
                                               (pageNumber - 1) * pageSize);
            }

            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得广告位置数量
        /// </summary>
        /// <returns></returns>
        public int GetAdvertPositionCount()
        {
            string commandText = string.Format("SELECT COUNT(adposid) FROM [{0}advertpositions]", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得全部广告位置
        /// </summary>
        /// <returns></returns>
        public IDataReader GetAllAdvertPosition()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}advertpositions]",
                                               RDBSHelper.RDBSTablePre,
                                               RDBSFields.ADVERT_POSITIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 创建广告位置
        /// </summary>
        /// <param name="advertPositionInfo"></param>
        public void CreateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            DbParameter[] param = {
                                     GenerateInParam("@title",SqlDbType.NChar, 50, advertPositionInfo.Title),
                                     GenerateInParam("@description",SqlDbType.NChar, 150, advertPositionInfo.Description)
                                   };
            string commandText = String.Format("INSERT INTO [{0}advertpositions]([title],[description]) VALUES(@title,@description)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 更新广告位置
        /// </summary>
        /// <param name="advertPositionInfo"></param>
        public void UpdateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            DbParameter[] param = {
                                     GenerateInParam("@title",SqlDbType.NChar, 50, advertPositionInfo.Title),
                                     GenerateInParam("@description",SqlDbType.NChar, 150, advertPositionInfo.Description),
                                     GenerateInParam("@adposid",SqlDbType.SmallInt, 2, advertPositionInfo.AdPosId)
                                   };
            string commandText = String.Format("UPDATE [{0}advertpositions] SET [title]=@title,[description]=@description WHERE [adposid]=@adposid",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId"></param>
        /// <returns></returns>
        public IDataReader GetAdvertPositionById(int adPosId)
        {
            DbParameter[] param = {
                                    GenerateInParam("@adposid",SqlDbType.SmallInt, 2, adPosId)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}advertpositions] WHERE [adposid]=@adposid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ADVERT_POSITIONS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 删除广告位置
        /// </summary>
        /// <param name="adPosId"></param>
        public void DeleteAdvertPositionById(int adPosId)
        {
            DbParameter[] param = {
                                    GenerateInParam("@adposid",SqlDbType.SmallInt, 2, adPosId)
                                   };
            string commandText = String.Format("DELETE FROM [{0}adverts] WHERE [adposid]=@adposid;DELETE FROM [{0}advertpositions] WHERE [adposid]=@adposid;",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 创建广告
        /// </summary>
        /// <param name="advertInfo"></param>
        public void CreateAdvert(AdvertInfo advertInfo)
        {
            DbParameter[] param = {
                                     GenerateInParam("@clickcount",SqlDbType.Int, 4, advertInfo.ClickCount),
                                     GenerateInParam("@adposid",SqlDbType.SmallInt, 2, advertInfo.AdPosId),
                                     GenerateInParam("@state",SqlDbType.TinyInt, 1, advertInfo.State),
                                     GenerateInParam("@starttime",SqlDbType.DateTime, 8, advertInfo.StartTime),
                                     GenerateInParam("@endtime",SqlDbType.DateTime, 8, advertInfo.EndTime),
                                     GenerateInParam("@displayorder",SqlDbType.Int, 4, advertInfo.DisplayOrder),
                                     GenerateInParam("@type",SqlDbType.TinyInt, 1, advertInfo.Type),
                                     GenerateInParam("@title",SqlDbType.NVarChar, 50, advertInfo.Title),
                                     GenerateInParam("@url",SqlDbType.NVarChar, 200, advertInfo.Url),
                                     GenerateInParam("@body",SqlDbType.NVarChar, 800, advertInfo.Body)
                                   };
            string commandText = String.Format("INSERT INTO [{0}adverts]([clickcount],[adposid],[state],[starttime],[endtime],[displayorder],[type],[title],[url],[body]) VALUES(@clickcount,@adposid,@state,@starttime,@endtime,@displayorder,@type,@title,@url,@body)",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="advertInfo"></param>
        public void UpdateAdvert(AdvertInfo advertInfo)
        {
            DbParameter[] param = {
                                     GenerateInParam("@clickcount",SqlDbType.Int, 4, advertInfo.ClickCount),
                                     GenerateInParam("@adposid",SqlDbType.SmallInt, 2, advertInfo.AdPosId),
                                     GenerateInParam("@state",SqlDbType.TinyInt, 1, advertInfo.State),
                                     GenerateInParam("@starttime",SqlDbType.DateTime, 8, advertInfo.StartTime),
                                     GenerateInParam("@endtime",SqlDbType.DateTime, 8, advertInfo.EndTime),
                                     GenerateInParam("@displayorder",SqlDbType.Int, 4, advertInfo.DisplayOrder),
                                     GenerateInParam("@type",SqlDbType.TinyInt, 1, advertInfo.Type),
                                     GenerateInParam("@title",SqlDbType.NVarChar, 50, advertInfo.Title),
                                     GenerateInParam("@url",SqlDbType.NVarChar, 200, advertInfo.Url),
                                     GenerateInParam("@body",SqlDbType.NVarChar, 800, advertInfo.Body),
                                     GenerateInParam("@id",SqlDbType.Int, 4, advertInfo.Id)
                                   };
            string commandText = String.Format("UPDATE [{0}adverts] SET [clickcount]=@clickcount,[adposid]=@adposid,[state]=@state,[starttime]=@starttime,[endtime]=@endtime,[displayorder]=@displayorder,[type]=@type,[title]=@title,[url]=@url,[body]=@body WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteAdvertById(int Id)
        {
            DbParameter[] param = {
                                     GenerateInParam("@id",SqlDbType.Int, 4, Id)
                                   };
            string commandText = String.Format("DELETE FROM [{0}adverts] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 后台获得广告
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IDataReader AdminGetAdvertById(int Id)
        {
            DbParameter[] param = {
                                     GenerateInParam("@id",SqlDbType.Int, 4, Id)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}adverts] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.ADVERTS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="adPosId"></param>
        /// <returns></returns>
        public DataTable AdminGetAdvertList(int pageSize, int pageNumber, int adPosId)
        {
            string commandText;
            if (pageNumber == 1)
            {
                if (adPosId < 1)
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[clickcount],[temp1].[adposid],[temp1].[type],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[state],[temp1].[displayorder],[temp1].[Body],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[clickcount],[adposid],[type],[title],[starttime],[endtime],[state],[displayorder],[Body] FROM [{1}adverts] ORDER BY [adposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}advertpositions] AS [temp2] ON [temp1].[adposid]=[temp2].[adposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre);

                else
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[clickcount],[temp1].[adposid],[temp1].[type],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[state],[temp1].[displayorder],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[clickcount],[adposid],[type],[title],[starttime],[endtime],[state],[displayorder] FROM [{1}adverts] WHERE [adposid]={2} ORDER BY [adposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}advertpositions] AS [temp2] ON [temp1].[adposid]=[temp2].[adposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                adPosId);
            }
            else
            {
                if (adPosId < 1)
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[clickcount],[temp1].[adposid],[temp1].[type],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[state],[temp1].[displayorder],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[clickcount],[adposid],[type],[title],[starttime],[endtime],[state],[displayorder] FROM [{1}adverts] WHERE [id] NOT IN (SELECT TOP {2} [id] FROM [{1}adverts] ORDER BY [adposid] DESC,[displayorder] DESC) ORDER BY [adposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}advertpositions] AS [temp2] ON [temp1].[adposid]=[temp2].[adposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize);
                else
                    commandText = string.Format("SELECT [temp1].[id],[temp1].[clickcount],[temp1].[adposid],[temp1].[type],[temp1].[title] AS [atitle],[temp1].[starttime],[temp1].[endtime],[temp1].[state],[temp1].[displayorder],[temp2].[title] AS [aptitle] FROM (SELECT TOP {0} [id],[clickcount],[adposid],[type],[title],[starttime],[endtime],[state],[displayorder] FROM [{1}adverts] WHERE [adposid]={3} AND [id] NOT IN (SELECT TOP {2} [id] FROM [{1}adverts] WHERE [adposid]={3} ORDER BY [adposid] DESC,[displayorder] DESC) ORDER BY [adposid] DESC,[displayorder] DESC) AS [temp1] LEFT JOIN [{1}advertpositions] AS [temp2] ON [temp1].[adposid]=[temp2].[adposid]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                adPosId);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得广告数量
        /// </summary>
        /// <param name="adPosId"></param>
        /// <returns></returns>
        public int AdminGetAdvertCount(int adPosId)
        {
            string commandText;
            if (adPosId < 1)
                commandText = string.Format("SELECT COUNT(id) FROM [{0}adverts]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(id) FROM [{0}adverts] WHERE [adposid]={1}", RDBSHelper.RDBSTablePre, adPosId);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得广告列表
        /// </summary>
        /// <param name="adPosId"></param>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public IDataReader GetAdvertList(int adPosId, DateTime nowTime)
        {
            DbParameter[] param = {
                                     GenerateInParam("@adposid",SqlDbType.Int, 4, adPosId),
                                     GenerateInParam("@nowtime",SqlDbType.DateTime, 8, nowTime)
                                   };
            string commandText = string.Format("SELECT {0} FROM [{1}adverts] WHERE [adposid]=@adposid AND [state]=1 AND [starttime]<=@nowtime AND [endtime]>@nowtime ORDER BY [displayorder] DESC",
                                                RDBSFields.ADVERTS,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        } 
        #endregion
        
        #region 信息反馈
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetFeedBackTypeList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}FeedBackType]",
                                               RDBSHelper.RDBSTablePre,
                                               RDBSFields.FEEDBACK_TYPE);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 创建反馈分类
        /// </summary>
        /// <param name="feedBackTypeInfo"></param>
        public void CreateFeedBackType(FeedBackTypeInfo feedBackTypeInfo)
        {
            DbParameter[] param = { 
                                    GenerateInParam("@TypeName",SqlDbType.NVarChar,250,feedBackTypeInfo.TypeName),
                                    GenerateInParam("@IsShowList",SqlDbType.Int,4,feedBackTypeInfo.IsShowList),
                                    GenerateInParam("@Body",SqlDbType.NText,0,feedBackTypeInfo.Body),
                                    GenerateInParam("@Tags",SqlDbType.NVarChar,250,feedBackTypeInfo.Tags)
                                  };
            string commandText = string.Format("INSERT INTO [{0}FeedBackType] ([TypeName],[IsShowList],[Body],[Tags]) values (@TypeName,@IsShowList,@Body,@Tags)", RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 更新反馈分类
        /// </summary>
        /// <param name="feedBackTypeInfo"></param>
        /// <returns></returns>
        public void UpdateFeedBackType(FeedBackTypeInfo feedBackTypeInfo)
        {
            DbParameter[] param = { 
                                    GenerateInParam("@TypeName",SqlDbType.NVarChar,250,feedBackTypeInfo.TypeName),
                                    GenerateInParam("@IsShowList",SqlDbType.Int,4,feedBackTypeInfo.IsShowList),
                                    GenerateInParam("@Body",SqlDbType.NText,0,feedBackTypeInfo.Body),
                                    GenerateInParam("@Tags",SqlDbType.NVarChar,250,feedBackTypeInfo.Tags),
                                    GenerateInParam("@FeedBackTypeId",SqlDbType.Int,4,feedBackTypeInfo.FeedBackTypeId)
                                  };
            string commandText = string.Format("UPDATE [{0}FeedBackType] SET [TypeName]=@TypeName,[IsShowList]=@IsShowList,[Body]=@Body,[Tags]=@Tags WHERE [FeedBackTypeId] = @FeedBackTypeId",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 获取信息反馈分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDataReader GetFeedBackType(int id)
        {
            DbParameter[] param = { 
                                  GenerateInParam("@FeedBackTypeId",SqlDbType.Int,4,id)
                                  };
            string commandText = string.Format("SELECT {1} FROM [{0}FeedBackType] WHERE [feedbackTypeid]=@feedbackTypeid",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FEEDBACK_TYPE);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);

        }

        /// <summary>
        /// 删除信息反馈分类
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFeedBackType(int id)
        {
            DbParameter[] param = {
                                    GenerateInParam("@feedbacktypeid",SqlDbType.Int, 4, id)
                                   };
            string commandText = String.Format("DELETE FROM [{0}FeedBackType] WHERE [feedbacktypeid]=@feedbacktypeid;",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }


        

        /// <summary>
        /// 前台获取信息反馈列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public DataTable GetFeedBackList( int pageSize, int pageNumber,int typeID)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT [temp1].[id],[temp1].[FeedBackTypeID],[temp1].[Tag],[temp1].[LinkMan],[temp1].[Tel],[temp1].[Mobile],[temp1].[Email],[temp1].[Title],[temp1].[AddTime],[temp1].[ReplyTime],[temp1].[state],[temp1].[IsOut],[temp1].[Ip],[temp1].[SearchKey],[temp2].[typeName],[temp1].[body],[temp1].[reply] FROM (SELECT TOP {0} [id],[FeedBackTypeID],[Tag],[LinkMan],[tel],[Mobile],[Email],[title],[AddTime],[ReplyTime],[state],[IsOut],[Ip],[SearchKey],[body],[reply] FROM [{1}FeedBacks] WHERE [FeedBackTypeId]={2} and Isout=1 and state = 1 ORDER BY [id] DESC) AS [temp1] LEFT JOIN [{1}FeedBackType] AS [temp2] ON [temp1].[FeedBackTypeId]=[temp2].[FeedBackTypeId]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                typeID);
            }
            else
            {
                commandText = string.Format("SELECT [temp1].[id],[temp1].[FeedBackTypeID],[temp1].[Tag],[temp1].[LinkMan],[temp1].[Tel],[temp1].[Mobile],[temp1].[Email],[temp1].[Title],[temp1].[AddTime],[temp1].[ReplyTime],[temp1].[state],[temp1].[IsOut],[temp1].[Ip],[temp1].[SearchKey],[temp2].[typeName],[temp1].[body],[temp1].[reply] FROM (SELECT TOP {0} [id],[FeedBackTypeID],[Tag],[LinkMan],[tel],[Mobile],[Email],[title],[AddTime],[ReplyTime],[state],[IsOut],[Ip],[SearchKey],[body],[reply] FROM [{1}FeedBacks] WHERE [FeedBackTypeId]={3} and Isout=1 and state = 1 AND [id] NOT IN (SELECT TOP {2} [id] FROM [{1}FeedBacks] WHERE [FeedBackTypeId]={3} and Isout=1 and state = 1 ORDER BY [id] DESC) ORDER BY [id] DESC) AS [temp1] LEFT JOIN [{1}FeedBackType] AS [temp2] ON [temp1].[FeedBackTypeID]=[temp2].[FeedBackTypeID]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                typeID);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获取信息反馈的数量
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public int GetFeedBackListCount(int typeId)
        {
            string commandText;
            if (typeId < 1)
                commandText = string.Format("SELECT COUNT(id) FROM [{0}FeedBacks] Where Isout=1 and State=1", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(id) FROM [{0}FeedBacks] WHERE [FeedBackTypeId]={1} and Isout=1 and state=1", RDBSHelper.RDBSTablePre, typeId);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));

        }

        /// <summary>
        /// 后台获取信息反馈的数量
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public int AdminGetFeedBackListCount(int typeId)
        {
            string commandText;
            if (typeId < 1)
                commandText = string.Format("SELECT COUNT(id) FROM [{0}FeedBacks]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(id) FROM [{0}FeedBacks] WHERE [FeedBackTypeId]={1}", RDBSHelper.RDBSTablePre, typeId);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        
        }

        public DataTable AdminGetFeedBackList(int pageSize, int pageNumber, int typeID)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT [temp1].[id],[temp1].[FeedBackTypeID],[temp1].[Tag],[temp1].[LinkMan],[temp1].[Tel],[temp1].[Mobile],[temp1].[Email],[temp1].[Title],[temp1].[AddTime],[temp1].[ReplyTime],[temp1].[state],[temp1].[IsOut],[temp1].[Ip],[temp1].[SearchKey],[temp2].[typeName] FROM (SELECT TOP {0} [id],[FeedBackTypeID],[Tag],[LinkMan],[tel],[Mobile],[Email],[title],[AddTime],[ReplyTime],[state],[IsOut],[Ip],[SearchKey] FROM [{1}FeedBacks] WHERE [FeedBackTypeId]={2} ORDER BY [id] DESC) AS [temp1] LEFT JOIN [{1}FeedBackType] AS [temp2] ON [temp1].[FeedBackTypeId]=[temp2].[FeedBackTypeId]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                typeID);
            }
            else
            {
                commandText = string.Format("SELECT [temp1].[id],[temp1].[FeedBackTypeID],[temp1].[Tag],[temp1].[LinkMan],[temp1].[Tel],[temp1].[Mobile],[temp1].[Email],[temp1].[Title],[temp1].[AddTime],[temp1].[ReplyTime],[temp1].[state],[temp1].[IsOut],[temp1].[Ip],[temp1].[SearchKey],[temp2].[typeName] FROM (SELECT TOP {0} [id],[FeedBackTypeID],[Tag],[LinkMan],[tel],[Mobile],[Email],[title],[AddTime],[ReplyTime],[state],[IsOut],[Ip],[SearchKey] FROM [{1}FeedBacks] WHERE [FeedBackTypeId]={3} AND [id] NOT IN (SELECT TOP {2} [id] FROM [{1}FeedBacks] WHERE [FeedBackTypeId]={3} ORDER BY [id] DESC) ORDER BY [id] DESC) AS [temp1] LEFT JOIN [{1}FeedBackType] AS [temp2] ON [temp1].[FeedBackTypeID]=[temp2].[FeedBackTypeID]",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                typeID);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 创建信息反馈
        /// </summary>
        /// <param name="feedBackInfo"></param>
        /// <returns></returns>
        public void CreateFeedBack(FeedBackInfo feedBackInfo)
        {
            DbParameter[] param = { 
                                  GenerateInParam("@FeedBackTypeId",SqlDbType.Int,4,feedBackInfo.FeedBackTypeId),
                                  GenerateInParam("@Tag",SqlDbType.NVarChar,50,feedBackInfo.Tag),
                                  GenerateInParam("@LinkMan",SqlDbType.NVarChar,50,feedBackInfo.LinkMan),
                                  GenerateInParam("@Tel",SqlDbType.NVarChar,50,feedBackInfo.Tel),
                                  GenerateInParam("@Mobile",SqlDbType.NVarChar,50,feedBackInfo.Mobile),
                                  GenerateInParam("@Email",SqlDbType.NVarChar,50,feedBackInfo.Email),
                                  GenerateInParam("@Title",SqlDbType.NVarChar,250,feedBackInfo.Title),
                                  GenerateInParam("@Body",SqlDbType.NText,0,feedBackInfo.Body),
                                  GenerateInParam("@AddTime",SqlDbType.DateTime,8,feedBackInfo.AddTime),
                                  GenerateInParam("@Reply",SqlDbType.NText,0,feedBackInfo.Reply),
                                  GenerateInParam("@ReplyTime",SqlDbType.DateTime,8,feedBackInfo.ReplyTime),
                                  GenerateInParam("@State",SqlDbType.Int,4,feedBackInfo.State),
                                  GenerateInParam("@IsOut",SqlDbType.Int,4,feedBackInfo.IsOut),
                                  GenerateInParam("@Ip",SqlDbType.NVarChar,32,feedBackInfo.Ip),
                                  GenerateInParam("@SearchKey",SqlDbType.NVarChar,50,feedBackInfo.SearchKey)
                                  };
            string commandText = string.Format("INSERT INTO [{0}FeedBacks] ([FeedBackTypeID],[Tag],[LinkMan],[Tel],[Mobile],[Email],[Title],[Body],[AddTime],[Reply],[ReplyTime],[State],[IsOut],[Ip],[SearchKey]) VALUES (@FeedBackTypeID,@Tag,@LinkMan,@Tel,@Mobile,@Email,@Title,@Body,@AddTime,@Reply,@ReplyTime,@State,@IsOut,@Ip,@SearchKey)", RDBSHelper.RDBSTablePre);

            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);

        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="feedBackInfo"></param>
        public void UpdateFeedBack(FeedBackInfo feedBackInfo)
        {
            DbParameter[] param = { 
                                  GenerateInParam("@Tag",SqlDbType.NVarChar,50,feedBackInfo.Tag),
                                  GenerateInParam("@LinkMan",SqlDbType.NVarChar,50,feedBackInfo.LinkMan),
                                  GenerateInParam("@Tel",SqlDbType.NVarChar,50,feedBackInfo.Tel),
                                  GenerateInParam("@Mobile",SqlDbType.NVarChar,50,feedBackInfo.Mobile),
                                  GenerateInParam("@Email",SqlDbType.NVarChar,50,feedBackInfo.Email),
                                  GenerateInParam("@Title",SqlDbType.NVarChar,250,feedBackInfo.Title),
                                  GenerateInParam("@Body",SqlDbType.NText,0,feedBackInfo.Body),                                  
                                  GenerateInParam("@Reply",SqlDbType.NText,0,feedBackInfo.Reply),
                                  GenerateInParam("@ReplyTime",SqlDbType.DateTime,8,feedBackInfo.ReplyTime),
                                  GenerateInParam("@State",SqlDbType.Int,4,feedBackInfo.State),
                                  GenerateInParam("@IsOut",SqlDbType.Int,4,feedBackInfo.IsOut),
                                  GenerateInParam("@Ip",SqlDbType.NVarChar,32,feedBackInfo.Ip),
                                  GenerateInParam("@SearchKey",SqlDbType.NVarChar,50,feedBackInfo.SearchKey),
                                  GenerateInParam("@ID",SqlDbType.Int,4,feedBackInfo.Id)
                                  };
            string commandText = string.Format("UPDATE [{0}FeedBacks] SET [Tag]=@Tag,[LinkMan]=@LinkMan,[Tel]=@tel,[Mobile]=@mobile,[Email]=@Email,[Title]=@title,[Body]=@body,[Reply]=@Reply,[ReplyTime]=@ReplyTime,[State]=@state,[IsOut]=@IsOut,[IP]=@IP,[SearchKey]=@SearchKey Where ID =@ID", RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);

        }
        /// <summary>
        /// 根据ID获取FeedBack
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IDataReader GetFeedBack(int Id)
        {
            DbParameter[] param = {
                                     GenerateInParam("@id",SqlDbType.Int, 4, Id)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}FeedBacks] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FEEDBACKS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 根据SearchKey获取Feedback
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IDataReader GetFeedBackBySearchKey(string searchKey)
        {
            DbParameter[] param = {
                                     GenerateInParam("@SearchKey",SqlDbType.NVarChar, 50, searchKey)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}FeedBacks] WHERE [SearchKey]=@SearchKey",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.FEEDBACKS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 删除信息反馈
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFeedBack(int id)
        {
            DbParameter[] param = {
                                    GenerateInParam("@id",SqlDbType.Int, 4, id)
                                   };
            string commandText = String.Format("DELETE FROM [{0}FeedBacks] WHERE [id]=@id;",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }


        #endregion


        #region 投票
        /// <summary>
        /// 获取首页投票列表
        /// </summary>
        /// <param name="count"></param>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public IDataReader GetHomeVoteList(int count,DateTime nowTime)
        {
            DbParameter[] param = {
                                     GenerateInParam("@nowtime",SqlDbType.DateTime, 8, nowTime)
                                   };
            string commandText = string.Format("SELECT {0} FROM [{1}votes] WHERE [state]=1 AND [starttime]<=@nowtime AND [endtime]>@nowtime ORDER BY [ID] DESC",
                                                RDBSFields.VOTES,
                                                RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        } 

        public IDataReader GetVoteList(int pageSize, int pageNumber, string condition, string sort)
        {
            throw new NotImplementedException();
        }

        public DataTable AdminGetVoteList(int pageSize, int pageNumber)
        {
            string commandText;
            if (pageNumber == 1)
            {
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}votes] ORDER BY [id] DESC",
                                                pageSize,
                                                RDBSFields.VOTES,
                                                RDBSHelper.RDBSTablePre
                                                );
            }
            else
            {
                commandText = string.Format("SELECT TOP {0} {3} FROM [{1}votes] Where [id] NOT IN (SELECT TOP {2} [id] FROM [{1}votes] ORDER BY [id] DESC) ORDER BY [id] DESC",
                                                pageSize,
                                                RDBSHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                RDBSFields.VOTES);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 管理员获取投票数量
        /// </summary>
        /// <returns></returns>
        public int AdminGetVoteCount()
        {
            string commandText;
            commandText = string.Format("SELECT COUNT(id) FROM [{0}votes]", RDBSHelper.RDBSTablePre);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText));
        
        }

        /// <summary>
        /// 创建投票
        /// </summary>
        /// <param name="voteInfo"></param>
        /// <returns></returns>
        public int CreateVote(VoteInfo voteInfo)
        {
            DbParameter[] param = { 
                                  GenerateInParam("@Title",SqlDbType.NVarChar,250,voteInfo.Title),
                                  GenerateInParam("@StartTime",SqlDbType.DateTime,8,voteInfo.StartTime),
                                  GenerateInParam("@EndTime",SqlDbType.DateTime,8,voteInfo.EndTime),
                                  GenerateInParam("@State",SqlDbType.Int,4,voteInfo.State),
                                  GenerateInParam("@Type",SqlDbType.Int,4,voteInfo.Type)
                                  };
            string commandText = string.Format("INSERT INTO [{0}Votes] ([Title],[StartTime],[EndTime],[State],[Type]) Values (@Title,@StartTime,@EndTime,@State,@Type);SELECT SCOPE_IDENTITY()", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, param), -1);
        }

        /// <summary>
        /// 更新投票信息
        /// </summary>
        /// <param name="voteInfo"></param>
        public void UpdateVote(VoteInfo voteInfo)
        {
            DbParameter[] param = { 
                                  GenerateInParam("@Title",SqlDbType.NVarChar,250,voteInfo.Title),
                                  GenerateInParam("@StartTime",SqlDbType.DateTime,8,voteInfo.StartTime),
                                  GenerateInParam("@EndTime",SqlDbType.DateTime,8,voteInfo.EndTime),
                                  GenerateInParam("@State",SqlDbType.Int,4,voteInfo.State),
                                  GenerateInParam("@Type",SqlDbType.Int,4,voteInfo.Type),
                                  GenerateInParam("@ID",SqlDbType.Int,8,voteInfo.Id)
                                  };
            string commandText = string.Format("UPDATE [{0}Votes] SET [Title]=@title,[startTime]=@StartTime,[EndTime]=@EndTime,[State]=@State,[Type]=@Type where [id]=@ID", RDBSHelper.RDBSTablePre);

            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);

        }

        /// <summary>
        /// 删除投票
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteVote(int Id)
        {
            DbParameter[] param = {
                                    GenerateInParam("@id",SqlDbType.Int, 4, Id)
                                   };
            string commandText = String.Format("DELETE FROM [{0}Votes] WHERE [id]=@id;DELETE FROM [{0}VoteResults] WHERE [VoteID]=@id;",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 获取投票
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IDataReader GetVote(int Id)
        {
            DbParameter[] param = {
                                     GenerateInParam("@id",SqlDbType.Int, 4, Id)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}Votes] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.VOTES);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 获取投票结果
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        public IDataReader GetVoteResultsList(int voteId)
        {
            DbParameter[] param = {
                                     GenerateInParam("@VoteId",SqlDbType.Int, 4, voteId)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}VoteResults] WHERE [VoteId]=@VoteId",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.VOTE_RESULTS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 获取单个投票结果
        /// </summary>
        /// <param name="voteResultId"></param>
        /// <returns></returns>
        public IDataReader GetVoteResult(int voteResultId)
        {
            DbParameter[] param = {
                                     GenerateInParam("@voteResultId",SqlDbType.Int, 4, voteResultId)
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}VoteResults] WHERE [Id]=@voteResultId",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.VOTE_RESULTS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, param);

        }

        /// <summary>
        /// 创建投票结果
        /// </summary>
        /// <param name="voteResultInfo"></param>
        public void CreateVoteResults(VoteResultInfo voteResultInfo)
        {
            DbParameter[] param = { 
                                  GenerateInParam("@VoteId",SqlDbType.Int,4,voteResultInfo.VoteId),
                                  GenerateInParam("@DisplayOrder",SqlDbType.Int,4,voteResultInfo.DisplayOrder),
                                  GenerateInParam("@Result",SqlDbType.NVarChar,250,voteResultInfo.Result),
                                  GenerateInParam("@Count",SqlDbType.Int,4,voteResultInfo.Count)
                                  };
            string commandText = string.Format("INSERT INTO [{0}VoteResults] ([VoteId],[DisplayOrder],[Result],[Count]) Values (@VoteId,@DisplayOrder,@Result,@Count)", RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 更新投票结果
        /// </summary>
        /// <param name="voteResultInfo"></param>
        public void UpdateVoteResults(VoteResultInfo voteResultInfo)
        {
            DbParameter[] param = { 
                                  GenerateInParam("@VoteId",SqlDbType.Int,4,voteResultInfo.VoteId),
                                  GenerateInParam("@DisplayOrder",SqlDbType.Int,4,voteResultInfo.DisplayOrder),
                                  GenerateInParam("@Result",SqlDbType.NVarChar,250,voteResultInfo.Result),
                                  GenerateInParam("@Count",SqlDbType.Int,4,voteResultInfo.Count),
                                  GenerateInParam("@Id",SqlDbType.Int,4,voteResultInfo.Id)
                                  };
            string commandText = string.Format("UPDATE [{0}VoteResults] SET [DisplayOrder]=@DisplayOrder,[Result]=@Result,[Count]=@Count Where [Id] = @Id", RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 删除投票结果
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteVoteResult(int Id)
        {
            DbParameter[] param = {
                                    GenerateInParam("@id",SqlDbType.Int, 4, Id)
                                   };
            string commandText = String.Format("DELETE FROM [{0}VoteResults] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="ResultId"></param>
        public void VoteResults(int ResultId)
        {
            DbParameter[] param = {
                                    GenerateInParam("@id",SqlDbType.Int, 4, ResultId)
                                   };
            string commandText = String.Format("UPDATE [{0}VoteResults] SET Count = Count+1 WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre);
            RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
            
        }
        #endregion


       
    }
}
