using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using BonSite.Core;

namespace BonSite.RDBSStrategy.SqlServer
{
    public partial class RDBSStrategy : IRDBSStrategy
    {

        public int CreateServiceEval(ServiceEvalInfo model)
        {
            DbParameter[] parms = { 
                         GenerateInParam("@Name",  SqlDbType.NVarChar,50, model.Name),  
                         GenerateInParam("@Contact",  SqlDbType.NVarChar,50, model.Contact),  
                         GenerateInParam("@Province",  SqlDbType.NVarChar,50, model.Province),  
                         GenerateInParam("@City",  SqlDbType.NVarChar,50, model.City), 
                         GenerateInParam("@Courier",  SqlDbType.NVarChar,250, model.Courier), 
                         GenerateInParam("@EvalProduct",  SqlDbType.Int,4, model.EvalProduct),      
                         GenerateInParam("@EvalLogistics",  SqlDbType.Int,4, model.EvalLogistics),      
                         GenerateInParam("@EvalService",  SqlDbType.Int,4, model.EvalService), 
			             GenerateInParam("@Body",  SqlDbType.NText, 0,model.Body),                         
                         GenerateInParam("@CreateTime",  SqlDbType.DateTime,8, model.CreateTime),          
                         GenerateInParam("@State",  SqlDbType.Int,4, model.State),              
                         GenerateInParam("@WeChatName",  SqlDbType.NVarChar,250, model.WeChatName),
                         GenerateInParam("@WeChatOpenId",SqlDbType.NVarChar,50,model.WeChatOpenId)
            };

            string commandText = string.Format("insert into {0}ServiceEval(Name,Contact,Province,City,Courier,EvalProduct,EvalLogistics,EvalService,Body,CreateTime,State,WeChatName,weChatOpenId) values (@Name,@Contact,@Province,@City,@Courier,@EvalProduct,@EvalLogistics,@EvalService,@Body,@CreateTime,@State,@WeChatName,@weChatOpenId) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        public bool DeleteServiceEvalById(string idlist)
        {
            string commandText = string.Format("DELETE FROM [{0}ServiceEval] WHERE [id] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                idlist);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }

        public bool UpdateServiceEval(ServiceEvalInfo model)
        {

            DbParameter[] parms = {
			              GenerateInParam("@Name",  SqlDbType.NVarChar,50, model.Name),  
                         GenerateInParam("@Contact",  SqlDbType.NVarChar,50, model.Contact),  
                         GenerateInParam("@Province",  SqlDbType.NVarChar,50, model.Province),  
                         GenerateInParam("@City",  SqlDbType.NVarChar,50, model.City), 
                         GenerateInParam("@Courier",  SqlDbType.NVarChar,250, model.Courier), 
                         GenerateInParam("@EvalProduct",  SqlDbType.Int,4, model.EvalProduct),      
                         GenerateInParam("@EvalLogistics",  SqlDbType.Int,4, model.EvalLogistics),      
                         GenerateInParam("@EvalService",  SqlDbType.Int,4, model.EvalService), 
			             GenerateInParam("@Body",  SqlDbType.NText, 0,model.Body),                         
                         GenerateInParam("@CreateTime",  SqlDbType.DateTime,8, model.CreateTime),          
                         GenerateInParam("@State",  SqlDbType.Int,4, model.State),              
                         GenerateInParam("@WeChatName",  SqlDbType.NVarChar,250, model.WeChatName),
                         GenerateInParam("@WeChatOpenId",SqlDbType.NVarChar,50,model.WeChatOpenId),
                         GenerateInParam("@id",SqlDbType.Int,4,model.Id)
              
            };

            string commandText = string.Format("update {0}ServiceEval set Name=@Name,Contact=@Contact,Province=@Province,Courier=@Courier,City=@City,EvalProduct=@EvalProduct,EvalLogistics=@EvalLogistics,EvalService=@EvalService,Body=@Body,CreateTime=@CreateTime,State=@State,WeChatName=@WeChatName,weChatOpenId=@weChatOpenId  where id=@id ",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        public System.Data.IDataReader GetServiceEvalInfoById(int id)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@id", SqlDbType.Int, 4, id)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}ServiceEval] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.SERVICEEVAL);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public int CreateProductFeedbacks(ProductFeedbacksInfo model)
        {
            DbParameter[] parms = { 
                         GenerateInParam("@CityName",  SqlDbType.NVarChar,50, model.CityName),  
                         GenerateInParam("@ProductName",  SqlDbType.NVarChar,50, model.ProductName),  
                         GenerateInParam("@ProductModel",  SqlDbType.NVarChar,50, model.ProductModel),  
                         GenerateInParam("@CustomerName",  SqlDbType.NVarChar,50, model.CustomerName),  
                         GenerateInParam("@Contact",  SqlDbType.NVarChar,50, model.Contact),  
                         GenerateInParam("@Address",  SqlDbType.NVarChar,250, model.Address),  
			             GenerateInParam("@Body",  SqlDbType.NText, 0,model.Body),        
                         GenerateInParam("@State",  SqlDbType.Int,4, model.State),                           
                         GenerateInParam("@CreateTime",  SqlDbType.DateTime,8, model.CreateTime),              
                         GenerateInParam("@WeChatName",  SqlDbType.NVarChar,250, model.WeChatName),
                         GenerateInParam("@WeChatOpenId",SqlDbType.NVarChar,50,model.WeChatOpenId),
                         GenerateInParam("@Imgs",SqlDbType.NVarChar,1000,model.Imgs)
            };

            string commandText = string.Format("insert into {0}ProductFeedbacks(CityName,ProductName,ProductModel,CustomerName,Contact,Address,Body,CreateTime,State,WeChatName,weChatOpenId,imgs) values (@CityName,@ProductName,@ProductModel,@CustomerName,@Contact,@Address,@Body,@CreateTime,@State,@WeChatName,@weChatOpenId,@imgs) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        public bool DeleteProductFeedbacksById(string idlist)
        {
            string commandText = string.Format("DELETE FROM [{0}ProductFeedbacks] WHERE [id] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                idlist);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }

        public bool UpdateProductFeedbacks(ProductFeedbacksInfo model)
        {

            DbParameter[] parms = {
                         GenerateInParam("@CityName",  SqlDbType.NVarChar,50, model.CityName),  
                         GenerateInParam("@ProductName",  SqlDbType.NVarChar,50, model.ProductName),  
                         GenerateInParam("@ProductModel",  SqlDbType.NVarChar,50, model.ProductModel),  
                         GenerateInParam("@CustomerName",  SqlDbType.NVarChar,50, model.CustomerName),  
                         GenerateInParam("@Contact",  SqlDbType.NVarChar,50, model.Contact),  
                         GenerateInParam("@Address",  SqlDbType.NVarChar,250, model.Address),  
			             GenerateInParam("@Body",  SqlDbType.NText, 0,model.Body),        
                         GenerateInParam("@State",  SqlDbType.Int,4, model.State),                           
                         GenerateInParam("@CreateTime",  SqlDbType.DateTime,8, model.CreateTime),              
                         GenerateInParam("@WeChatName",  SqlDbType.NVarChar,250, model.WeChatName),
                         GenerateInParam("@WeChatOpenId",SqlDbType.NVarChar,50,model.WeChatOpenId),
                         GenerateInParam("@Imgs",SqlDbType.NVarChar,1000,model.Imgs),
                         GenerateInParam("@id",SqlDbType.Int,4,model.Id)
              
            };

            string commandText = string.Format("update {0}ProductFeedbacks set CityName=@CityName,ProductName=@ProductName,ProductModel=@ProductModel,CustomerName=@CustomerName,Contact=@Contact,Address=@Address,sBody=@Body,CreateTime=@CreateTime,State=@State,WeChatName=@WeChatName,weChatOpenId=@weChatOpenId,Imgs=@Imgs  where id=@id ",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        public System.Data.IDataReader GetProductFeedbacksInfoById(int id)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@id", SqlDbType.Int, 4, id)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}ProductFeedbacks] WHERE [id]=@id",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCTFEEDBACKS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }


        public DataTable AdminGetServiceEvalList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}ServiceEval] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.SERVICEEVAL,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}ServiceEval] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.SERVICEEVAL,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}ServiceEval]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.SERVICEEVAL,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}ServiceEval] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.SERVICEEVAL,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public string AdminGetServiceEvalListCondition(string title)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [Name] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string AdminGetServiceEvalListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[ID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        public DataTable AdminGetProductFeedbacksList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}ProductFeedbacks] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCTFEEDBACKS,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}ProductFeedbacks] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCTFEEDBACKS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}ProductFeedbacks]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCTFEEDBACKS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}ProductFeedbacks] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCTFEEDBACKS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public string AdminGetProductFeedbacksListCondition(string title)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [ProductName] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string AdminGetProductFeedbacksListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[ID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }


        public int AdminGetServiceEvalCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ID) FROM [{0}ServiceEval]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ID) FROM [{0}ServiceEval] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public int AdminGetProductFeedbacksCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ID) FROM [{0}ProductFeedbacks]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ID) FROM [{0}ProductFeedbacks] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }
    }
}
