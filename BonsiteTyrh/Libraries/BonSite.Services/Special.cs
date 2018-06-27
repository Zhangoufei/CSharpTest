using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BonSite.Core;
using BonSite.Data;

namespace BonSite.Services
{
    public class Special
    {
        /// <summary>
        /// 创建新分类，并返回分类ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(SpecialInfo model)
        {
            return BonSite.Data.Special.Create(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(SpecialInfo model)
        {
            return BonSite.Data.Special.Update(model);
        }


        /// <summary>
        /// 删除专题
        /// </summary>
        /// <param name="specialId"></param>
        /// <returns></returns>
        public static bool Delete(int specialId)
        {
            return BonSite.Data.Special.Delete(specialId);
        }


        /// <summary>
        /// 获得专题列表
        /// </summary>
        /// <returns></returns>
        public static List<SpecialInfo> GetList()
        {
            List<SpecialInfo> list = BonSite.Core.BSCache.Get(CacheKeys.SITE_SPECIAL_LIST) as List<SpecialInfo>;
            if (list == null)
            {
                list = BonSite.Data.Special.GetList();
                BonSite.Core.BSCache.Insert(CacheKeys.SITE_SPECIAL_LIST, list);
            }
            return list;
        }



        public static List<SpecialInfo> GetTopSpecialList(int count)
        {
            return BonSite.Data.Special.GetTopSpecialList(count);
        }

        /// <summary>
        /// 管理员获取专题列表（无缓存）
        /// </summary>
        /// <returns></returns>
        public static List<SpecialInfo> AdminGetList()
        {
            return BonSite.Data.Special.GetList();
        }

        ///// <summary>
        ///// 删除专题
        ///// </summary>
        ///// <param name="spedicalId"></param>
        ///// <returns></returns>
        //public static bool Delete(int spedicalId)
        //{
        //    return BonSite.Data.Special.Delete(spedicalId);
        //}

        public static SpecialInfo AdminGetModelById(int specialid)
        {
            if (specialid > 0)
            {
                foreach (SpecialInfo model in BonSite.Data.Special.GetList())
                {
                    if (model.SpecialID == specialid)
                        return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据ID获得文章分类
        /// </summary>
        /// <param name="articleclassid">新闻类型id</param>
        /// <returns></returns>
        public static SpecialInfo GetModelById(int specialid)
        {
            if (specialid > 0)
            {
                foreach (SpecialInfo model in GetList())
                {
                    if (model.SpecialID == specialid)
                        return model;
                }
            }
            return null;
        }

      

    }
}
