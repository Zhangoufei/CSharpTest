using System;
using System.Collections.Generic;
using System.Data;

using BonSite.Core;

namespace BonSite.Services
{
    public class Adverts
    {
        /// <summary>
        /// 获得广告位置列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static List<AdvertPositionInfo> GetAdvertPositionList(int pageSize, int pageNumber)
        {
            return BonSite.Data.Adverts.GetAdvertPositionList(pageSize, pageNumber);
        }

        /// <summary>
        /// 获取广告位置数量
        /// </summary>
        /// <returns></returns>
        public static int GetAdvertPositionCount()
        {
            return BonSite.Data.Adverts.GetAdvertPositionCount();
        }

        /// <summary>
        /// 获取全部广告位置
        /// </summary>
        /// <returns></returns>
        public static List<AdvertPositionInfo> GetAllAdvertPosition()
        {
            return BonSite.Data.Adverts.GetAllAdvertPosition();
        }

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId"></param>
        /// <returns></returns>
        public static AdvertPositionInfo GetAdvertPositionById(int adPosId)
        {
            if (adPosId > 0)
                return BonSite.Data.Adverts.GetAdvertPositionById(adPosId);
            return null;
        }

        /// <summary>
        /// 获得广告列表
        /// </summary>
        /// <param name="adPosId"></param>
        /// <returns></returns>
        public static List<AdvertInfo> GetAdvertList(int adPosId)
        {
            List<AdvertInfo> advertList = BonSite.Core.BSCache.Get(CacheKeys.SITE_ADVERT_LIST + adPosId) as List<AdvertInfo>;
            if (advertList == null)
            {
                advertList = BonSite.Data.Adverts.GetAdvertList(adPosId, DateTime.Now);
                BonSite.Core.BSCache.Insert(CacheKeys.SITE_ADVERT_LIST + adPosId, advertList);
            }
            return advertList;
        }


        /// <summary>
        /// 创建广告位置
        /// </summary>
        public static void CreateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            BonSite.Data.Adverts.CreateAdvertPosition(advertPositionInfo);
        }

        /// <summary>
        /// 更新广告位置
        /// </summary>
        public static void UpdateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            BonSite.Data.Adverts.UpdateAdvertPosition(advertPositionInfo);
        }

        /// <summary>
        /// 删除广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        public static void DeleteAdvertPositionById(int adPosId)
        {
            BonSite.Data.Adverts.DeleteAdvertPositionById(adPosId);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_ADVERT_LIST + adPosId);

        }




        /// <summary>
        /// 创建广告
        /// </summary>
        public static void CreateAdvert(AdvertInfo advertInfo)
        {
            BonSite.Data.Adverts.CreateAdvert(advertInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_ADVERT_LIST + advertInfo.AdPosId);
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        public static void UpdateAdvert(int oldAdPosId, AdvertInfo advertInfo)
        {
            BonSite.Data.Adverts.UpdateAdvert(advertInfo);
            if (oldAdPosId == advertInfo.AdPosId)
            {
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_ADVERT_LIST + advertInfo.AdPosId);
            }
            else
            {
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_ADVERT_LIST + oldAdPosId);
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_ADVERT_LIST + advertInfo.AdPosId);
            }
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="adId">广告id</param>
        public static void DeleteAdvertById(int adId)
        {
            AdvertInfo advertInfo = AdminGetAdvertById(adId);
            if (advertInfo != null)
            {
                BonSite.Data.Adverts.DeleteAdvertById(adId);
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_ADVERT_LIST + advertInfo.AdPosId);
            }
        }

        /// <summary>
        /// 后台获得广告
        /// </summary>
        /// <param name="adId">广告id</param>
        /// <returns></returns>
        public static AdvertInfo AdminGetAdvertById(int adId)
        {
            return BonSite.Data.Adverts.AdminGetAdvertById(adId);
        }

        /// <summary>
        /// 后台获得广告列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static DataTable AdminGetAdvertList(int pageSize, int pageNumber, int adPosId)
        {
            return BonSite.Data.Adverts.AdminGetAdvertList(pageSize, pageNumber, adPosId);
        }

        /// <summary>
        /// 后台获得广告数量
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static int AdminGetAdvertCount(int adPosId)
        {
            return BonSite.Data.Adverts.AdminGetAdvertCount(adPosId);
        }

    }
}
