using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BonSite.Core;
using BonSite.Core.Domain.Site;

namespace BonSite.Services
{
    public class WeChats
    {
        //获得列表
        public static WeChatInfo[] GetWeChatList()
        {
            WeChatInfo[] weChatList = BonSite.Data.WeChat.GetWeChatList();
            return weChatList;
        }
        //创建
        public static void CreateWeChat(WeChatInfo weChatInfo)
        {
            BonSite.Data.WeChat.CreateWeChat(weChatInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_WECHAT_LIST);
        }
        //删除
        public static void DeleteWeChat(int[] idList)
        {
            if (idList != null && idList.Length > 0)
            {
                BonSite.Data.WeChat.DeleteWeChat(CommonHelper.IntArrayToString(idList));
                BonSite.Core.BSCache.Remove(CacheKeys.SITE_WECHAT_LIST);
            }
        }
        //根据ID获取
        public static WeChatInfo GetWeChatById(int id)
        {
            foreach (WeChatInfo weChatInfo in GetWeChatList())
            {
                if (weChatInfo.id == id)
                    return weChatInfo;
            }
            return null;
        }
        //更新
        public static void UpdateWeChat(WeChatInfo weChatInfo)
        {
            BonSite.Data.WeChat.UpdateWeChat(weChatInfo);
            BonSite.Core.BSCache.Remove(CacheKeys.SITE_WECHAT_LIST);
        }
    }
}
