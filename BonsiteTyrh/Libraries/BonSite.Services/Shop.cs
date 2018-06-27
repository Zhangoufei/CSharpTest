using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using BonSite.Core;

namespace BonSite.Services
{
    public class Shop
    {

        #region 增删改查
        public static int Create(ShopInfo model)
        {
            return BonSite.Data.Shop.Create(model);
        }

        public static ShopInfo GetModelByShopID(int shopid)
        {
            if (shopid > 0)
                return BonSite.Data.Shop.GetModelById(shopid);
            return null;
        }

        public static bool Update(ShopInfo model)
        {
            return BonSite.Data.Shop.Update(model);
        }

        public static bool Del(int[] idlist)
        {
            if (idlist != null && idlist.Length > 0)
            {
                bool isSucces = BonSite.Data.Shop.Delete(CommonHelper.IntArrayToString(idlist));
                //删除缓存
                //BonSite.Core.BSCache.Remove(CacheKeys.SITE_ARTICLECLASS_LIST + "\\d+");
                return isSucces;
            }
            else
            {
                return false;
            }
        }
#endregion

        #region 后台

        public static DataTable AdminGetShopList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Shop.AdminGetShopList(pageSize, pageNumber, condition, sort);
        }

        public static int AdminGetShopCount(string condition)
        {
            return BonSite.Data.Shop.AdminGetShopCount(condition);
        }

        public static string AdminGetShopListCondition(string title)
        {
            return BonSite.Data.Shop.AdminGetShopListCondition(title);
        }

        public static string AdminGetShopListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Shop.AdminGetShopListSort(sortColumn, sortDirection);
        }

        #endregion


        public static DataTable GetShopList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.Shop.GetShopList(pageSize, pageNumber, condition, sort);
        }

        public static int GetShopCount(string condition)
        {
            return BonSite.Data.Shop.GetShopCount(condition);
        }

        public static string GetShopListCondition(string area,string type,string shopname)
        {
            return BonSite.Data.Shop.GetShopListCondition(area, type, shopname);
        }

        public static string GetShopListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.Shop.GetShopListSort(sortColumn, sortDirection);
        }

        public static DataTable GetShopAreaList()
        {
            return BonSite.Data.Shop.GetShopAreaList();
        }

    }
}
