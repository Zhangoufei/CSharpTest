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

        public int CreateShop(ShopInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@ShopName",  SqlDbType.NVarChar, 50,model.ShopName),                         
                         GenerateInParam("@Address",  SqlDbType.NVarChar,200, model.Address),                       
                         GenerateInParam("@Tel",  SqlDbType.NVarChar,200, model.Tel),      
                         GenerateInParam("@Fax",  SqlDbType.NVarChar,200, model.Fax),      
                         GenerateInParam("@ShopImg",SqlDbType.NVarChar,200,model.ShopImg),
                         GenerateInParam("@Position",  SqlDbType.NVarChar,50, model.Position),                         
                         GenerateInParam("@Body",  SqlDbType.NText,0, model.Body),                          
                         GenerateInParam("@Area",  SqlDbType.NVarChar,50, model.Area),
                         GenerateInParam("@Type",  SqlDbType.NVarChar,50, model.Type),                         
                         GenerateInParam("@OrderID",  SqlDbType.Int,4, model.OrderID),                         
                         GenerateInParam("@Remark",  SqlDbType.NVarChar,200, model.Remark)
              
            };

            string commandText = string.Format("insert into {0}Shops(ShopName,Address,Tel,Fax,ShopImg,Position,Body,Area,Type,OrderID,Remark) values (@ShopName,@Address,@Tel,@Fax,@ShopImg,@Position,@Body,@Area,@Type,@OrderID,@Remark) ;select @@IDENTITY;", RDBSHelper.RDBSTablePre);
            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        public bool DeleteShop(string shopIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}Shops] WHERE [ShopID] IN ({1})",
                                                RDBSHelper.RDBSTablePre,
                                                shopIdList);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText) > 0)
                return true;
            else
                return false;
        }

        public bool UpdateShop(ShopInfo model)
        {
            DbParameter[] parms = {
			             GenerateInParam("@ShopName",  SqlDbType.NVarChar, 50,model.ShopName),                         
                         GenerateInParam("@Address",  SqlDbType.NVarChar,200, model.Address),                       
                         GenerateInParam("@Tel",  SqlDbType.NVarChar,200, model.Tel),      
                         GenerateInParam("@Fax",  SqlDbType.NVarChar,200, model.Fax),      
                         GenerateInParam("@ShopImg",SqlDbType.NVarChar,200,model.ShopImg),
                         GenerateInParam("@Position",  SqlDbType.NVarChar,50, model.Position),                         
                         GenerateInParam("@Body",  SqlDbType.NText,0, model.Body),                          
                         GenerateInParam("@Area",  SqlDbType.NVarChar,50, model.Area),
                         GenerateInParam("@Type",  SqlDbType.NVarChar,50, model.Type),                         
                         GenerateInParam("@OrderID",  SqlDbType.Int,4, model.OrderID),                         
                         GenerateInParam("@Remark",  SqlDbType.NVarChar,200, model.Remark),                  
                         GenerateInParam("@ShopID",  SqlDbType.Int,4, model.ShopID)
              
            };

            string commandText = string.Format("update {0}Shops set ShopName=@ShopName,Address=@Address,Tel=@Tel,Fax=@Fax,ShopImg=@ShopImg,Position=@Position,Body=@Body,Area=@Area,Type=@Type,OrderID=@OrderID,Remark=@Remark where ShopID=@ShopID ",
                                                RDBSHelper.RDBSTablePre);
            if (RDBSHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0)
                return true;
            else
                return false;
        }

        public System.Data.IDataReader GetShopById(int shopId)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@shopId", SqlDbType.Int, 4, shopId)    
                                   };
            string commandText = string.Format("SELECT {1} FROM [{0}Shops] WHERE [shopId]=@shopId",
                                                RDBSHelper.RDBSTablePre,
                                                RDBSFields.SHOPS);
            return RDBSHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        public System.Data.DataTable GetShopList(int pageSize, int pageNumber, string condition, string sort)
        {
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Shops] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Shops] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Shops]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Shops] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public string GetShopListCondition(string area, string type, string shopname)
        {
            StringBuilder condition = new StringBuilder();

            if (!area.Equals("全部"))
                condition.AppendFormat(" AND [area] = '{0}' ", area);
            if (!type.Equals("全部"))
                condition.AppendFormat(" AND [type] = '{0}' ", type);
            if (!string.IsNullOrWhiteSpace(shopname))
                condition.AppendFormat(" AND [shopname] like '{0}%' ", shopname);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string GetShopListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[ShopID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        public int GetShopCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ShopID) FROM [{0}Shops]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ShopID) FROM [{0}Shops] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }

        public System.Data.DataTable AdminGetShopList(int pageSize, int pageNumber, string condition, string sort)
        {

            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Shops] ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {1} FROM [{2}Shops] WHERE {4} ORDER BY {3}",
                                                pageSize,
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                condition);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Shops]) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1);
                else
                    commandText = string.Format("SELECT {0} FROM (SELECT TOP {3} ROW_NUMBER() OVER (ORDER BY {2}) AS [rowid],{0} FROM [{1}Shops] WHERE {5}) AS [temp] WHERE [temp].[rowid] BETWEEN {4} AND {3}",
                                                RDBSFields.SHOPS,
                                                RDBSHelper.RDBSTablePre,
                                                sort,
                                                pageNumber * pageSize,
                                                (pageNumber - 1) * pageSize + 1,
                                                condition);
            }

            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public string AdminGetShopListCondition(string title)
        {
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(title))
                condition.AppendFormat(" AND [shopname] like '{0}%' ", title);

            return condition.Length > 0 ? condition.Remove(0, 4).ToString() : "";
        }

        public string AdminGetShopListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[ShopID]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        public int AdminGetShopCount(string condition)
        {
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(ShopID) FROM [{0}Shops]", RDBSHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(ShopID) FROM [{0}Shops] WHERE {1}", RDBSHelper.RDBSTablePre, condition);

            return TypeHelper.ObjectToInt(RDBSHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }


        public DataTable GetShopAreaList()
        {
            string commandText = string.Format("SELECT DISTINCT Area FROM {0}shops", RDBSHelper.RDBSTablePre);
            return RDBSHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
    }
}
