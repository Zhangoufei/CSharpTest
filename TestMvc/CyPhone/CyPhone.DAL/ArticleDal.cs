using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CyPhone.Model;
using CyPhone.DbUtility;
using CyPhone.Common;
namespace CyPhone.DAL
{
    public class ArticleDal 
    {
        /// <summary>
        /// 根据关键词获取新闻列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public  List<bs_Article> GetArticleListByKeyWords(string keywords, int pageNum, int pageSize)
        {
            //列名
            string columnsStr = "ArticleID,ArticleClassID,SpecialID,DisplayType,IsShow,IsTop,IsHome,IsBest,Title,Body,AddTime,UpdateTime,Author,ComeForm,ImgUrl,Url,Digest,Keys,UserID,AdminID,Hits,Keyword,Description,InformType,EndTime,InformGroup,IsClassBrand,PushStatus,MicroVideo,ApprovalStatus,Praise,Auditor";
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select {0} from(", columnsStr);
            strSql.AppendFormat(" SELECT ROW_NUMBER() OVER(ORDER BY ArticleID desc) AS rowid,{0} ", columnsStr);
            strSql.Append(" FROM bs_Article");
            strSql.Append(" where ApprovalStatus=1 ");
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                strSql.AppendFormat(" and Title + CAST(Body as varchar) like '%{0}%' ", keywords);
            }
            strSql.Append(")t ");
            strSql.AppendFormat(" where t.rowid >= {0} AND t.rowid <= {1} ", (pageNum - 1) * pageSize + 1,
                                                pageNum * pageSize);
            strSql.Append(" order by UpdateTime desc");
            List<bs_Article> list = new List<bs_Article>();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    bs_Article model = new bs_Article();
                    DataHelper.TableRowToModel<bs_Article>(model,dr);
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public  int GetArticleRecordCount(string keywords)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(ArticleID) from bs_Article ");
            strSql.Append(" where ApprovalStatus=1 ");
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                strSql.AppendFormat(" and Title + CAST(Body as varchar) like '%{0}%' ", keywords);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
    }
}
