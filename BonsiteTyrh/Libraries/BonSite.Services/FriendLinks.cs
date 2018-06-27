using System;
using System.Collections.Generic;
using System.Text;

using BonSite.Core;

namespace BonSite.Services
{
    public class FriendLinks
    {
        /// <summary>
        /// 创建友情链接
        /// </summary>
        public static void CreateFriendLink(FriendLinkInfo friendLinkInfo)
        {
            BonSite.Data.FriendLinks.CreateFriendLink(friendLinkInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_FRIENDLINK_LIST);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="idList">友情链接id</param>
        public static void DeleteFriendLinkById(int[] idList)
        {
            if (idList != null && idList.Length > 0)
            {
                BonSite.Data.FriendLinks.DeleteFriendLinkById(CommonHelper.IntArrayToString(idList));
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_FRIENDLINK_LIST);
            }
        }

        /// <summary>
        /// 更新友情链接
        /// </summary>
        public static void UpdateFriendLink(FriendLinkInfo friendLinkInfo)
        {
            BonSite.Data.FriendLinks.UpdateFriendLink(friendLinkInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_FRIENDLINK_LIST);
        }

        /// <summary>
        /// 获得友情链接列表
        /// </summary>
        public static FriendLinkInfo[] GetFriendLinkList()
        {
            FriendLinkInfo[] friendLinkList = BonSite.Core.BSCache.Get(CacheKeys.SITE_FRIENDLINK_LIST) as FriendLinkInfo[];
            if (friendLinkList == null)
            {
                friendLinkList = BonSite.Data.FriendLinks.GetFriendLinkList();
                BonSite.Core.BSCache.Insert(CacheKeys.SITE_FRIENDLINK_LIST, friendLinkList);
            }
            return friendLinkList;
        }

        /// <summary>
        /// 获得友情链接
        /// </summary>
        /// <param name="id">友情链接id</param>
        /// <returns></returns>
        public static FriendLinkInfo GetFriendLinkById(int id)
        {
            foreach (FriendLinkInfo friendLinkInfo in GetFriendLinkList())
            {
                if (friendLinkInfo.Id == id)
                    return friendLinkInfo;
            }

            return null;
        }
    }
}
