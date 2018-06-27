using System;
using System.Collections.Generic;
using System.Data;

using BonSite.Core;

namespace BonSite.Services
{
    public class FeedBack
    {

        /// <summary>
        /// 创建反馈分类
        /// </summary>
        /// <param name="feedbackTypeInfo"></param>
        public static void CreateFeedBackType(FeedBackTypeInfo feedbackTypeInfo)
        {
            BonSite.Data.FeedBack.CreateFeedBackType(feedbackTypeInfo);
        }

        /// <summary>
        /// 更新反馈分类
        /// </summary>
        /// <param name="feedbackTypeInfo"></param>
        public static void UpdateFeedBackType(FeedBackTypeInfo feedbackTypeInfo)
        {
            BonSite.Data.FeedBack.UpdateFeedBackType(feedbackTypeInfo);
        }

        /// <summary>
        /// 删除反馈分类
        /// </summary>
        /// <param name="Id"></param>
        public static void DeleteFeedBackType(int Id)
        {
            BonSite.Data.FeedBack.DeleteFeedBackType(Id);
        }

        /// <summary>
        /// 创建反馈
        /// </summary>
        /// <param name="feedbackInfo"></param>
        public static void CreateFeedBackInfo(FeedBackInfo feedbackInfo)
        {
            BonSite.Data.FeedBack.CreateFeedBackInfo(feedbackInfo);
        }

        /// <summary>
        /// 更新反馈
        /// </summary>
        /// <param name="feedbackInfo"></param>
        public static void UpdateFeedBackInfo(FeedBackInfo feedbackInfo)
        {
            BonSite.Data.FeedBack.UpdateFeedBackInfo(feedbackInfo);
        }
        /// <summary>
        /// 删除反馈
        /// </summary>
        /// <param name="Id"></param>
        public static void DeleteFeedBackInfo(int Id)
        {
            BonSite.Data.FeedBack.DeleteFeedBackInfo(Id);
        }

        public static List<FeedBackTypeInfo> GetFeedBackTypeList()
        {
            return BonSite.Data.FeedBack.GetFeedBackTypeList();
        }

        public static int AdminGetFeedBackListCount(int typeId)
        {
            return BonSite.Data.FeedBack.AdminGetFeedBackListCount(typeId);
        }

        /// <summary>
        /// 获取反馈信息列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public static DataTable AdminGetFeedBackList(int pageSize, int pageNumber, int TypeID)
        {
            return BonSite.Data.FeedBack.AdminGetFeedBackList(pageSize, pageNumber, TypeID);
        }

        public static FeedBackTypeInfo GetFeedBackTypeById(int TypeId)
        {
            return BonSite.Data.FeedBack.GetFeedBackTypeById(TypeId);
        }

        public static FeedBackInfo GetFeedBackById(int Id)
        {
            return BonSite.Data.FeedBack.GetFeedBackById(Id);
        }

        public static FeedBackInfo GetFeedBackBySearchKey(string key)
        {
            return BonSite.Data.FeedBack.GetFeedBackBySearchKey(key);
        }

        public static int GetFeedBackListCount(int typeId)
        {
            return BonSite.Data.FeedBack.GetFeedBackListCount(typeId);
        }

        public static DataTable GetFeedBackList(int PageSize, int pageNumber, int typeId)
        {
            return BonSite.Data.FeedBack.GetFeedBackList(PageSize, pageNumber, typeId);
        }


    }
}
