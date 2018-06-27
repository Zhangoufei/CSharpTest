using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public partial interface IRDBSStrategy
    {
        /// <summary>
        /// 创建门店
        /// </summary>
        /// <param name="shopInfo"></param>
        /// <returns></returns>
        int CreateShop(ShopInfo shopInfo);

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="ShopIdList"></param>
        /// <returns></returns>
        bool DeleteShop(string shopIdList);

        /// <summary>
        /// 更新门店
        /// </summary>
        /// <param name="shopInfo"></param>
        /// <returns></returns>
        bool UpdateShop(ShopInfo shopInfo);

        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        IDataReader GetShopById(int shopId);

        /// <summary>
        /// 前台获取门店列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable GetShopList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 前台获取门店搜索条件
        /// </summary>
        /// <param name="area"></param>
        /// <param name="type"></param>
        /// <param name="shopname"></param>
        /// <returns></returns>
        string GetShopListCondition(string area, string type, string shopname);

        /// <summary>
        /// 前台获取门店列表排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        string GetShopListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 前台获取门店数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int GetShopCount(string condition);




        /// <summary>
        /// 后台获取门店列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable AdminGetShopList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获取门店列表搜索条件
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        string AdminGetShopListCondition(string title);

        /// <summary>
        /// 后台获取门店列表排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        string AdminGetShopListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获取门店数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int AdminGetShopCount(string condition);

        /// <summary>
        /// 获取门店地区列表
        /// </summary>
        /// <returns></returns>
        DataTable GetShopAreaList();


    }
}
