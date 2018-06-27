using BonSite.Core;
using BonSite.Core.Domain.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Services
{
    public class ClassManages
    {
        /// <summary>
        /// 获得班级管理列表
        /// </summary>
        public static ClassManageInfo[] GetClassManageList()
        {
            //ClassManageInfo[] classManageList = BonSite.Core.BSCache.Get(BonSite.Core.CacheKeys.SITE_CLASSMANAGE_LIST) as ClassManageInfo[];
            //if (classManageList == null)
            //{
            ClassManageInfo[] classManageList = BonSite.Data.ClassManages.GetClassManageList();
            //    //BonSite.Core.BSCache.Insert(CacheKeys.SITE_CLASSMANAGE_LIST,classManageList);
            //}
            return classManageList;
        }
        /// <summary>
        /// 创建班级
        /// </summary>
        public static void CreateClassManage(ClassManageInfo classManageInfo)
        {
            BonSite.Data.ClassManages.CreateClassManage(classManageInfo);
           // BonSite.Core.BSCache.Remove(CacheKeys.SITE_FRIENDLINK_LIST);
        }

        /// <summary>
        /// 获得班级
        /// </summary>
        /// <param name="ClassID">班级ClassID</param>
        /// <returns></returns>
        public static ClassManageInfo GetClassManageById(int id)
        {
            foreach (ClassManageInfo classManageInfo in GetClassManageList())
            {
                if (classManageInfo.ClassID ==id)
                    return classManageInfo;
            }

            return null;
        }

        /// <summary>
        /// 更新班级
        /// </summary>
        public static void UpdateClassManage(ClassManageInfo classManageInfo)
        {
            BonSite.Data.ClassManages.UpdateClassManage(classManageInfo);
            //BonSite.Core.BSCache.Remove(CacheKeys.SITE_FRIENDLINK_LIST);
        }
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="idList">班级id</param>
        public static void DeleteClassManageById(int[] idList)
        {
            if (idList != null && idList.Length > 0)
            {
                 BonSite.Data.ClassManages.DeleteClassManageById(CommonHelper.IntArrayToString(idList));
               // BonSite.Core.BSCache.Remove(CacheKeys.SITE_FRIENDLINK_LIST);
            }
        }
    }
}
