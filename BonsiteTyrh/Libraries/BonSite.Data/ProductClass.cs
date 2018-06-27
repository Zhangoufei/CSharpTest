using System.Collections.Generic;
using System.Data;

using BonSite.Core;


namespace BonSite.Data
{
   public  class ProductClass
    {

        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建ProductClassInfo
        /// </summary>
       public static ProductClassInfo BuildFromReader(IDataReader reader)
        {
            ProductClassInfo model = new ProductClassInfo();
            model.ProductClassID = TypeHelper.ObjectToInt(reader["ProductClassID"]);
            model.ProductClassName = reader["ProductClassName"].ToString();
            model.ParentProductClassID = TypeHelper.ObjectToInt(reader["ParentProductClassID"]);         
            model.DisplayOrder = TypeHelper.ObjectToInt(reader["DisplayOrder"]);

            return model;
        }



        #endregion

        #region 读写删
        /// <summary>
        /// 创建产品类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static int Create(ProductClassInfo model)
        {
            return BonSite.Core.BSData.RDBS.CreateProductClass(model);
        }

        /// <summary>
        /// 删除产品类型
        /// </summary>
        /// <param name="productclassid"></param>
        /// <returns></returns>
        public static bool Delete(int productclassid)
        {
            return BonSite.Core.BSData.RDBS.DeleteProductClass(productclassid);
        }

        /// <summary>
        /// 更新产品类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ProductClassInfo model)
        {
            return BonSite.Core.BSData.RDBS.UpdateProductClass(model);
        }
        #endregion

        #region 后台
        /// <summary>
        /// 获取产品类型列表
        /// </summary>
        /// <returns></returns>
        public static List<ProductClassInfo> GetList()
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetProductClassList();
            while (reader.Read())
            {
                ProductClassInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;
        }



        #endregion

        #region 前台
        /// <summary>
        /// 获取导航列表
        /// </summary>
        /// <returns></returns>
        public static List<ProductClassInfo> GetProductClassList()
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetProductClassList();
            while (reader.Read())
            {
                ProductClassInfo model = BuildFromReader(reader);
                list.Add(model);
            }
            reader.Close();
            return list;

        }


        #endregion

    }
}
