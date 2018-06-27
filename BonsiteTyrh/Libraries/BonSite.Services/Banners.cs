using System;
using System.Collections.Generic;
using System.Data;

using BonSite.Core;

namespace BonSite.Services
{
    public class Banners
    {
        /// <summary>
        /// 获得Banner位置列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static List<BannerPositionInfo> GetBannerPositionList(int pageSize, int pageNumber)
        {
            return BonSite.Data.Banners.GetBannerPositionList();
        }

        ///// <summary>
        ///// 获取广告位置数量
        ///// </summary>
        ///// <returns></returns>
        //public static int GetBannerPositionCount()
        //{
        //    return BonSite.Data.Banners.GetBannerPositionListnCount();
        //}

        /// <summary>
        /// 获取全部广告位置
        /// </summary>
        /// <returns></returns>
        public static List<BannerPositionInfo> GetAllBannerPosition()
        {
            return BonSite.Data.Banners.GetBannerPositionList();
        }

        /// <summary>
        /// 获得banner位置
        /// </summary>
        /// <param name="adPosId"></param>
        /// <returns></returns>
        public static BannerPositionInfo GetBannerPositionById(int banPosId)
        {
            if (banPosId > 0)
                return BonSite.Data.Banners.GetBannerPositionById(banPosId);
            return null;
        }

        /// <summary>
        /// 获得banner列表
        /// </summary>
        /// <param name="adPosId"></param>
        /// <returns></returns>
        public static List<BannerInfo> GetBannerList(int banPosId)
        {
            List<BannerInfo> bannerList = BonSite.Core.BSCache.Get(CacheKeys.SITE_BANNER_LIST + banPosId) as List<BannerInfo>;
            if (bannerList == null)
            {
                bannerList = BonSite.Data.Banners.GetBannerList(banPosId, DateTime.Now);
                BonSite.Core.BSCache.Insert(CacheKeys.SITE_BANNER_LIST + banPosId, bannerList);
            }
            return bannerList;
        }



        /// <summary>
        /// 创建banner位置
        /// </summary>
        public static void CreateBannerPosition(BannerPositionInfo bannerPositionInfo)
        {
            BonSite.Data.Banners.CreateBannerPosition(bannerPositionInfo);
        }

        /// <summary>
        /// 更新banner位置
        /// </summary>
        public static void UpdateBannerPosition(BannerPositionInfo bannerPositionInfo)
        {
            BonSite.Data.Banners.UpdateBannerPosition(bannerPositionInfo);
        }

        /// <summary>
        /// 删除banner位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        public static void DeleteBannerPositionById(int banPosId)
        {
            BonSite.Data.Banners.DeleteBannerPositionById(banPosId);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_BANNER_LIST + banPosId);

        }




        /// <summary>
        /// 创建Banner
        /// </summary>
        public static void CreateBanner(BannerInfo bannerInfo)
        {
            BonSite.Data.Banners.CreateBanner(bannerInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_BANNER_LIST + bannerInfo.Id);
        }

        /// <summary>
        /// 更新Banner
        /// </summary>
        public static void UpdateBanner(int oldBanPosId, BannerInfo bannerInfo)
        {
            BonSite.Data.Banners.UpdateBanner(bannerInfo);
            if (oldBanPosId == bannerInfo.BanPosId)
            {
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_BANNER_LIST + bannerInfo.BanPosId);
            }
            else
            {
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_BANNER_LIST + oldBanPosId);
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_BANNER_LIST + bannerInfo.BanPosId);
            }
        }

        /// <summary>
        /// 删除Banner
        /// </summary>
        /// <param name="banId">Bannerid</param>
        public static void DeleteBannerById(int Id)
        {
            BannerInfo bannerInfo = AdminGetBannerById(Id);
            if (bannerInfo != null)
            {
                BonSite.Data.Banners.DeleteBannerById(Id);
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_BANNER_LIST + bannerInfo.BanPosId);
            }
        }

        /// <summary>
        /// 后台Banner广告
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns></returns>
        public static BannerInfo AdminGetBannerById(int Id)
        {
            return BonSite.Data.Banners.AdminGetBannerById(Id);
        }

        /// <summary>
        /// 后台获得banner列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static DataTable AdminGetBannerList(int pageSize, int pageNumber, int banPosId)
        {
            return BonSite.Data.Banners.AdminGetBannerList(pageSize, pageNumber, banPosId);
        }

        /// <summary>
        /// 后台获得banner数量
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns></returns>
        public static int AdminGetBannerCount(int banPosId)
        {
            return BonSite.Data.Banners.AdminGetBannerCount(banPosId);
        }
    }
}
