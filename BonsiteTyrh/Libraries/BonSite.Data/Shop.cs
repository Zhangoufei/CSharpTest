using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.Data
{
    public class Shop
    {

        #region 辅助方法
        /// <summary>
        /// 通过IDataReader创建ShopInfo
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static ShopInfo BuildFromReader(IDataReader reader)
        {
            ShopInfo model = new ShopInfo();

            model.ShopID = TypeHelper.ObjectToInt(reader["ShopID"]);
            model.ShopName = reader["ShopName"].ToString();
            model.Address = reader["Address"].ToString();
            model.Tel = reader["Tel"].ToString();
            model.Fax = reader["Fax"].ToString();
            model.ShopImg = reader["ShopImg"].ToString();
            model.Position = reader["Position"].ToString();
            model.Body = reader["Body"].ToString();
            model.Area = reader["Area"].ToString();
            model.Type = reader["Type"].ToString();
            model.OrderID = TypeHelper.ObjectToInt(reader["OrderID"]);
            model.Remark = reader["Remark"].ToString();

            return model;
        }

        #endregion



        /// <summary>
        /// 创建门店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ShopInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateShop(model);
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="shopidlist"></param>
        /// <returns></returns>
        public static bool Delete(string shopidlist)
        {
            return BonSite.Core.BSData.RDBS.DeleteShop(shopidlist);
        }

        /// <summary>
        /// 更新门店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ShopInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateShop(model);

        }

        /// <summary>
        /// 获得产品
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public static ShopInfo GetModelById(int shopid)
        {
            ShopInfo model = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetShopById(shopid);
            if (reader.Read())
            {
                model = BuildFromReader(reader);
            }

            reader.Close();

            return model;
        }

        /// <summary>
        /// 管理员获取门店列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static DataTable AdminGetShopList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.AdminGetShopList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 管理员获取门店列表搜索条件
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string AdminGetShopListCondition(string title)
        {
            return BonSite.Core.BSData.RDBS.AdminGetShopListCondition(title);
        }

        /// <summary>
        /// 管理员获取门店搜索排序
        /// </summary>
        /// <param name="sortcolumn"></param>
        /// <param name="sortdirection"></param>
        /// <returns></returns>
        public static string AdminGetShopListSort(string sortcolumn,string sortdirection)
        {
            return BonSite.Core.BSData.RDBS.AdminGetShopListSort(sortcolumn, sortdirection);

        }

        /// <summary>
        /// 管理员获取门店数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int AdminGetShopCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.AdminGetShopCount(condition);
        }

        /// <summary>
        /// 前台获取门店列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static DataTable GetShopList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Core.BSData.RDBS.GetShopList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 前台获取门店搜索条件
        /// </summary>
        /// <param name="area"></param>
        /// <param name="type"></param>
        /// <param name="shopname"></param>
        /// <returns></returns>
        public static string GetShopListCondition(string area,string type,string shopname)
        {
            return BonSite.Core.BSData.RDBS.GetShopListCondition(area,type,shopname);
        }

        /// <summary>
        /// 前台获取门店排序
        /// </summary>
        /// <param name="sortcolumn"></param>
        /// <param name="sortdirection"></param>
        /// <returns></returns>
        public static string GetShopListSort(string sortcolumn, string sortdirection)
        {
            return BonSite.Core.BSData.RDBS.GetShopListSort(sortcolumn, sortdirection);

        }

        /// <summary>
        /// 前台获取门店数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int GetShopCount(string condition)
        {
            return BonSite.Core.BSData.RDBS.GetShopCount(condition);
        }

        /// <summary>
        /// 获取门店地区列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetShopAreaList()
        {
            return BonSite.Core.BSData.RDBS.GetShopAreaList();
        }
    }
}
