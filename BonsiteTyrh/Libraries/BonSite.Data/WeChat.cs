using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BonSite.Core;
using BonSite.Core.Domain.Site;
using System.Data;

namespace BonSite.Data
{
    public class WeChat
    {
        //获取列表
        public static WeChatInfo[] GetWeChatList()
        {
            DataTable dt = BonSite.Core.BSData.RDBS.GetWeChatList();
            WeChatInfo[] weChatList = new WeChatInfo[dt.Rows.Count];

            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                WeChatInfo weChatInfo = new WeChatInfo();
                weChatInfo.id = TypeHelper.ObjectToInt(row["id"]);
                weChatInfo.name = row["name"].ToString();
                weChatInfo.appid = row["appid"].ToString();
                weChatInfo.secret = row["secret"].ToString();
                weChatList[index] = weChatInfo;
                index++;
            }
            return weChatList;
        }
        //增加
        public static int CreateWeChat(WeChatInfo weChatInfo)
        {
            return BonSite.Core.BSData.RDBS.CreateWeChat(weChatInfo);
        }
        //删除
        public static void DeleteWeChat(string idList)
        {
            BonSite.Core.BSData.RDBS.DeleteWeChat(idList);
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
            BonSite.Core.BSData.RDBS.UpdateWeChat(weChatInfo);
        }
    }
}
