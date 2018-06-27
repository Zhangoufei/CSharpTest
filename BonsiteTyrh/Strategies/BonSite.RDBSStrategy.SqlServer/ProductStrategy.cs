using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

using BonSite.Core;

namespace BonSite.RDBSStrategy.SqlServer
{
    public partial class RDBSStrategy : IRDBSStrategy
    {

        #region 产品分类
        /// <summary>
        /// 创建产品分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int CreateProductClass(ProductClassInfo model)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ProductClassName", SqlDbType.NVarChar, 50, model.ProductClassName),
                                    GenerateInParam("@ParentProductClassID", SqlDbType.Int,4,model.ParentProductClassID),
                                    GenerateInParam("@DisplayOrder",SqlDbType.Int,4,model.DisplayOrder)
                                    };

            string commandText = string.Format("INSERT INTO [{0}ProductClass](ProductClassName,ParentProductClassID,DisplayOrder) VALUES(@ProductClassName,@ParentProductClassID,@DisplayOrder);SELECT SCOPE_IDENTITY()",
                                                RDBSHelper.RDBSTablePre);
            //RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="productClassId"></param>
        /// <returns></returns>
        public bool DeleteProductClass(int productClassId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@productClassId", SqlDbType.Int,4,productClassId)
                                   };
            string commandText = string.Format("DELETE FROM [{0}ProductClass] WHERE [productClassId]=@productClassId;DELETE FROM [{0}product] WHERE [productClassId]=@productClassId;",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新产品分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProductClass(ProductClassInfo model)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@ProductClassID", SqlDbType.Int, 4, model.ProductClassID),
                                    GenerateInParam("@ProductClassName", SqlDbType.NVarChar, 50, model.ProductClassName),
                                    GenerateInParam("@ParentProductClassID", SqlDbType.Int,4,model.ParentProductClassID),
                                    GenerateInParam("@DisplayOrder", SqlDbType.NVarChar,250,model.DisplayOrder)
                                    };
            string commandText = string.Format("UPDATE [{0}ProductClass] SET ProductClassName=@ProductClassName, ParentProductClassID=@ParentProductClassID, DisplayOrder=@DisplayOrder WHERE [ProductClassID]=@ProductClassID",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取产品分类
        /// </summary>
        /// <returns></returns>
        public IDataReader GetProductClassList()
        {
            string commandText = string.Format("SELECT {1} FROM [{0}ProductClass] ORDER BY [displayorder],ProductClassID ",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT_CLASS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        #endregion


        #region 产品

        /// <summary>
        /// 新加产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int CreateProduct(ProductInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@Body",  SqlDbType.NText, 0,model.Body),                         
                         GenerateInParam("@AddTime",  SqlDbType.DateTime,8, model.AddTime),                       
                         GenerateInParam("@UpdateTime",  SqlDbType.DateTime,8, model.UpdateTime),      
                         GenerateInParam("@ImgUrl",  SqlDbType.NVarChar,250, model.ImgUrl),      
                         GenerateInParam("@BigImgUrl",SqlDbType.NVarChar,250,model.BigImgUrl),
                         GenerateInParam("@Digest",  SqlDbType.NText,0, model.Digest),                         
                         GenerateInParam("@Keys",  SqlDbType.VarChar,50, model.Keys),                          
                         GenerateInParam("@ProductClassID",  SqlDbType.Int,4, model.ProductClassID),
                         GenerateInParam("@AdminID",  SqlDbType.Int,4, model.AdminID),                         
                         GenerateInParam("@Hits",  SqlDbType.Int,4, model.Hits),                         
                         GenerateInParam("@IsShow",  SqlDbType.Int,4, model.IsShow),                         
                         GenerateInParam("@IsTop",  SqlDbType.Int,4, model.IsTop),                                  
                         GenerateInParam("@IsBest",  SqlDbType.Int,4, model.IsBest),                         
                         GenerateInParam("@Title",  SqlDbType.NVarChar,250, model.Title),
                         GenerateInParam("@Code",SqlDbType.NVarChar,50,model.Code),
                         GenerateInParam("@Type",SqlDbType.NVarChar,50,model.Type),
                         GenerateInParam("@Provider",SqlDbType.NVarChar,50,model.Provider),
                         GenerateInParam("@DisplayOrder",SqlDbType.Int,4,model.DisplayOrder),
                         GenerateInParam("@Keyword",SqlDbType.NVarChar,250,model.Keyword),
                         GenerateInParam("@Description",SqlDbType.NVarChar,250,model.Description)
              
            };

            string commandText = string.Format("insert into {0}Product(Body,AddTime,UpdateTime,ImgUrl,BigImgUrl,Digest,Keys,ProductClassID,AdminID,Hits,IsShow,IsTop,IsBest,Title,Code,Type,Provider,DisplayOrder,Keyword,Description) values (@Body,@AddTime,@UpdateTime,@ImgUrl,@BigImgUrl,@Digest,@Keys,@ProductClassID,@AdminID,@Hits,@IsShow,@IsTop,@IsBest,@Title,@Code,@Type,@Provider,@DisplayOrder,@Keyword,@Description) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="ProductIdList"></param>
        /// <returns></returns>
        public bool DeleteProductById(string ProductIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}Product] WHERE [ProductID] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                ProductIdList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        public bool UpdateProduct(ProductInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@Body",  SqlDbType.NText, 0,model.Body),                         
                         GenerateInParam("@AddTime",  SqlDbType.DateTime,8, model.AddTime),                       
                         GenerateInParam("@UpdateTime",  SqlDbType.DateTime,8, model.UpdateTime),      
                         GenerateInParam("@ImgUrl",  SqlDbType.NVarChar,250, model.ImgUrl),      
                         GenerateInParam("@BigImgUrl",SqlDbType.NVarChar,250,model.BigImgUrl),
                         GenerateInParam("@Digest",  SqlDbType.NText,0, model.Digest),                         
                         GenerateInParam("@Keys",  SqlDbType.VarChar,50, model.Keys),                          
                         GenerateInParam("@ProductClassID",  SqlDbType.Int,4, model.ProductClassID),
                         GenerateInParam("@AdminID",  SqlDbType.Int,4, model.AdminID),                         
                         GenerateInParam("@Hits",  SqlDbType.Int,4, model.Hits),                         
                         GenerateInParam("@IsShow",  SqlDbType.Int,4, model.IsShow),                         
                         GenerateInParam("@IsTop",  SqlDbType.Int,4, model.IsTop),                                  
                         GenerateInParam("@IsBest",  SqlDbType.Int,4, model.IsBest),                         
                         GenerateInParam("@Title",  SqlDbType.NVarChar,250, model.Title),
                         GenerateInParam("@Code",SqlDbType.NVarChar,50,model.Code),
                         GenerateInParam("@Type",SqlDbType.NVarChar,50,model.Type),
                         GenerateInParam("@Provider",SqlDbType.NVarChar,50,model.Provider),
                         GenerateInParam("@DisplayOrder",SqlDbType.Int,4,model.DisplayOrder),
                         GenerateInParam("@Keyword",SqlDbType.NVarChar,250,model.Keyword),
                         GenerateInParam("@Description",SqlDbType.NVarChar,250,model.Description),
                         GenerateInParam("@ProductID",SqlDbType.Int,4,model.ProductID)
              
            };

            string commandText = string.Format("update {0}Product set  Body=@Body,UpdateTime=@UpdateTime,ImgUrl=@ImgUrl,BigImgUrl=@BigImgUrl,Digest=@Digest,Keys=@Keys,ProductClassID=@ProductClassID,AdminID=@AdminID, Hits=@Hits,IsShow=@IsShow,IsTop=@IsTop, IsBest=@IsBest,Title=@Title,Code=@Code, Type=@Type,Provider=@Provider,DisplayOrder=@DisplayOrder,Keyword=@Keyword,Description=@Description  where ProductID=@ProductID ",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据ID获取产品信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IDataReader GetProductById(int productId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@productId", SqlDbType.Int, 4, productId)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}Product] WHERE [productId]=@productId",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.PRODUCT);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 后台获取产品列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public DataTable AdminGetProductList(int pageSize, int pageNumber, string condition, string sort)
        {

            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Product] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Product] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Product]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Product] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 后台获得产品列表搜索条件
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string AdminGetProductListCondition(int productClassId, string title)
        {
            StringBuilder condition = new StringBuilder();

            if (productClassId > 0)
                condition.AppendFormat(" AND [ProductClassID] = {0} ", productClassId);
            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [title] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        /// <summary>
        /// 后台获取产品列表排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public string AdminGetProductListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[ProductID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获取产品数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int AdminGetProductCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ProductID) FROM [{0}Product]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ProductID) FROM [{0}Product] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public IDataReader GetTopProductList(int productClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "ProductID Desc";

            string commandText;
            if (productClassId == 0)
                commandText = string.Format("SELECT top {2} {1} from [{0}Product] where isShow = 1 and isTop = 1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Product] where isShow = 1 and isTop = 1 and ProductClassID = {2} Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            productClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetTopProductList(string productClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "ProductID Desc";

            string commandText;
            if (string.IsNullOrWhiteSpace(productClassId))
                commandText = string.Format("SELECT top {2} {1} from [{0}Product] where  isShow = 1 and isTop = 1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Product]  where isShow = 1 and isTop = 1 and ProductClassID in ({2}) Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            productClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetBestProductList(int productClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "ProductID Desc";

            string commandText;
            if (productClassId == 0)
                commandText = string.Format("SELECT top {2} {1} from [{0}Product] where isShow = 1 and isBest = 1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Product] where isShow = 1 and isBest = 1 and ProductClassID = {2} Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            productClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetBestProductList(string productClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "ProductID Desc";

            string commandText;
            if (string.IsNullOrWhiteSpace(productClassId))
                commandText = string.Format("SELECT top {2} {1} from [{0}Product] where isShow = 1 and isBest = 1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Product]  where isShow = 1 and isBest = 1 and ProductClassID in ({2}) Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            productClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetProductList(int productClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "ProductID Desc";

            string commandText;
            if (productClassId == 0)
                commandText = string.Format("SELECT top {2} {1} from [{0}Product] where isShow = 1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Product] where isShow = 1 and ProductClassID = {2} Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            productClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetProductList(string productClassId, int count, string sortColumn)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "ProductID Desc";

            string commandText;
            if (string.IsNullOrWhiteSpace(productClassId))
                commandText = string.Format("SELECT top {2} {1} from [{0}Product] where isShow = 1 Order by {3}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            count,
                                            sortColumn);
            else
                commandText = string.Format("SELECT top {3} {1} from [{0}Product]  where isShow = 1 and ProductClassID in ({2}) Order by {4}",
                                            RDBSHelper.RDBSTablePre,
                                            RDBSFields.PRODUCT,
                                            productClassId,
                                            count,
                                            sortColumn);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public string GetProductListCondition(int productClassId, string title)
        {
            StringBuilder condition = new StringBuilder();

            if (productClassId > 0)
                condition.AppendFormat(" AND [productClassID] = {0} ", productClassId);
            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [title] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string GetProductListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[ProductID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        public DataTable GetProductList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Product] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Product] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Product]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Product] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.PRODUCT,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public int GetProductCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ProductID) FROM [{0}Product]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ProductID) FROM [{0}Product] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }
        #endregion
    }
}
