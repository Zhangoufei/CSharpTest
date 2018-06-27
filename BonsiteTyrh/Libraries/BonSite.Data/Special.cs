using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using BonSite.Core;

namespace BonSite.Data
{
    public class Special
    {

        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ArticleInfo
        /// </summary>
        public static SpecialInfo BuildFromReader(IDataReader reader)
        {
            SpecialInfo model = new SpecialInfo();

            model.SpecialID = TypeHelper.ObjectToInt(reader["SpecialID"]);
            model.Name = reader["Name"].ToString();
            model.Title = reader["Title"].ToString();
            model.Body = reader["Body"].ToString();
            model.ImgUrl = reader["ImgUrl"].ToString();
            model.LogoUrl = reader["LogoUrl"].ToString();
            model.OutUrl = reader["OutUrl"].ToString();
            model.IsOut = TypeHelper.ObjectToInt(reader["IsOut"]);
            model.DisplayOrder = TypeHelper.ObjectToInt(reader["DisplayOrder"]);

            return model;
        }

        #endregion


        /// <summary>
        /// 创建专题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(SpecialInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateSpecial(model);
        }

        /// <summary>
        /// 删除专题
        /// </summary>
        /// <param name="specialId"></param>
        /// <returns></returns>
        public static bool Delete(int specialId)
        {
            return BonSite.Core.BSData.RDBS.DeleteSpecialById(specialId);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(SpecialInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateSpecial(model);
        }

        /// <summary>
        /// 获取专题列表
        /// </summary>
        /// <returns></returns>
        public static List<SpecialInfo> GetList()
        {

            List<SpecialInfo> list = new List<SpecialInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetSpecialList();
            while (reader.Read())
            {
                SpecialInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;

        }


        /// <summary>
        /// 获取专题列表
        /// </summary>
        /// <returns></returns>
        public static List<SpecialInfo> GetTopSpecialList(int count)
        {

            List<SpecialInfo> list = new List<SpecialInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetTopSpecialList(count);
            while (reader.Read())
            {
                SpecialInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;

        }
    }
}
