using System;
using System.Data;
using System.Collections.Generic;

using BonSite.Core;

namespace BonSite.Data
{
    public class Banners
    {
        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建BannerInfo
        /// </summary>
        public static BannerInfo BuildBannerFromReader(IDataReader reader)
        {
            BannerInfo bannerInfo = new BannerInfo();

            bannerInfo.Id = TypeHelper.ObjectToInt(reader["id"]);
            bannerInfo.BanPosId = TypeHelper.ObjectToInt(reader["banposId"]);
            bannerInfo.StartTime = TypeHelper.ObjectToDateTime(reader["starttime"]);
            bannerInfo.EndTime = TypeHelper.ObjectToDateTime(reader["endtime"]);
            bannerInfo.IsShow = TypeHelper.ObjectToInt(reader["isshow"]);
            bannerInfo.Title = reader["title"].ToString();
            bannerInfo.Img = reader["img"].ToString();
            bannerInfo.Url = reader["url"].ToString();
            bannerInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);

            return bannerInfo;
        }

        /// <summary>
        /// 从DataRow创建BannerInfo
        /// </summary>
        public static BannerInfo BuildBannerFromRow(DataRow row)
        {
            BannerInfo bannerInfo = new BannerInfo();

            bannerInfo.Id = TypeHelper.ObjectToInt(row["id"]);
            bannerInfo.StartTime = TypeHelper.ObjectToDateTime(row["starttime"]);
            bannerInfo.EndTime = TypeHelper.ObjectToDateTime(row["endtime"]);
            bannerInfo.IsShow = TypeHelper.ObjectToInt(row["isshow"]);
            bannerInfo.Title = row["title"].ToString();
            bannerInfo.Img = row["img"].ToString();
            bannerInfo.Url = row["url"].ToString();
            bannerInfo.DisplayOrder = TypeHelper.ObjectToInt(row["displayorder"]);

            return bannerInfo;
        }

        /// <summary>
        /// 从IDataReader创建AdvertPositionInfo
        /// </summary>
        public static BannerPositionInfo BuildBannerPositionFromReader(IDataReader reader)
        {
            BannerPositionInfo bannerPositionInfo = new BannerPositionInfo();

            bannerPositionInfo.BanPosId = TypeHelper.ObjectToInt(reader["banposId"]);
            bannerPositionInfo.Title = reader["title"].ToString();
            bannerPositionInfo.Description = reader["description"].ToString();

            return bannerPositionInfo;
        }

        #endregion


        /// <summary>
        /// 获得Banner位置列表  
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static List<BannerPositionInfo> GetBannerPositionList()
        {
            List<BannerPositionInfo> bannerPositionList = new List<BannerPositionInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.AdminGetBannerPositionList();
            while (reader.Read())  
            {
                BannerPositionInfo bannerPositionInfo = BuildBannerPositionFromReader(reader);
                bannerPositionList.Add(bannerPositionInfo);
            }
            reader.Close();
            return bannerPositionList;
        }

        ///// <summary>
        ///// 获得广告位置数量
        ///// </summary>
        ///// <returns></returns>
        //public static int GetBannerPositionCount()
        //{
        //    return BonSite.Core.BSData.RDBS.GetBannerPositionCount();
        //}

        ///// <summary>
        ///// 获得全部广告位置
        ///// </summary>
        ///// <returns></returns>
        //public static List<BannerPositionInfo> GetAllBannerPosition()
        //{
        //    List<BannerPositionInfo> bannerPositionList = new List<BannerPositionInfo>();
        //    IDataReader reader = BonSite.Core.BSData.RDBS.AdminGetBannerPositionList();
        //    while (reader.Read())
        //    {
        //        BannerPositionInfo bannerPositionInfo = BuildBannerPositionFromReader(reader);
        //        advertPositionList.Add(advertPositionInfo);
        //    }
        //    reader.Close();
        //    return advertPositionList;
        //}

        /// <summary>
        /// 创建广告位置
        /// </summary>
        public static void CreateBannerPosition(BannerPositionInfo bannerPositionInfo)
        {
            BonSite.Core.BSData.RDBS.CreateBannerPosition(bannerPositionInfo);
        }

        /// <summary>
        /// 更新广告位置
        /// </summary>
        public static void UpdateBannerPosition(BannerPositionInfo bannerPositionInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateBannerPosition(bannerPositionInfo);
        }

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static BannerPositionInfo GetBannerPositionById(int banPosId)
        {
            BannerPositionInfo bannerPositionInfo = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.AdminGetBannerPositionByBanPosId(banPosId);
            if (reader.Read())
            {
                bannerPositionInfo = BuildBannerPositionFromReader(reader);
            }
            reader.Close();
            return bannerPositionInfo;
        }

        /// <summary>
        /// 删除广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        public static void DeleteBannerPositionById(int banPosId)
        {
            BonSite.Core.BSData.RDBS.DeleteBannerPosition(banPosId);
        }




        /// <summary>
        /// 获得首页banner列表
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public static List<BannerInfo> GetBannerList(int banPosId, DateTime nowTime)
        {
            List<BannerInfo> bannerList = new List<BannerInfo>();
            DataTable dt = BonSite.Core.BSData.RDBS.GetBannerList(nowTime, banPosId);
            //BannerInfo[] bannerList = new BannerInfo[dt.Rows.Count];

            //int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                BannerInfo bannerInfo = BuildBannerFromRow(row);
                bannerList.Add(bannerInfo);
            }
            return bannerList;
        }

        /// <summary>
        /// 后台获得banner列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static DataTable AdminGetBannerList(int banPosId, int pageSize, int pageNumber)
        {
            return BonSite.Core.BSData.RDBS.AdminGetBannerList(banPosId, pageSize, pageNumber);            
        }

        /// <summary>
        /// 后台获得banner数量
        /// </summary>
        /// <returns></returns>
        public static int AdminGetBannerCount(int banPosId)
        {
            return BonSite.Core.BSData.RDBS.AdminGetBannerCount(banPosId);
        }

        /// <summary>
        /// 后台获得banner
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static BannerInfo AdminGetBannerById(int id)
        {
            BannerInfo bannerInfo = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.AdminGetBannerById(id);
            if (reader.Read())
            {
                bannerInfo = BuildBannerFromReader(reader);
            }
            reader.Close();
            return bannerInfo;
        }

        /// <summary>
        /// 创建banner
        /// </summary>
        public static void CreateBanner(BannerInfo bannerInfo)
        {
            BonSite.Core.BSData.RDBS.CreateBanner(bannerInfo);
        }

        /// <summary>
        /// 更新banner
        /// </summary>
        public static void UpdateBanner(BannerInfo bannerInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateBanner(bannerInfo);
        }

        /// <summary>
        /// 删除banner
        /// </summary>
        /// <param name="id">id列表</param>
        public static void DeleteBannerById(int id)
        {
            BonSite.Core.BSData.RDBS.DeleteBannerById(id);
        }

        
    }
}
