using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BonSite.Core;

namespace BonSite.Services
{
    public  class ProductClass
    {

        #region 读写删
        /// <summary>
        /// 创建新分类，并返回分类ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ProductClassInfo model)
        {
            return BonSite.Data.ProductClass.Create(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ProductClassInfo model)
        {
            return BonSite.Data.ProductClass.Update(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="articleclassid"></param>
        /// <returns></returns>
        public static bool Delete(int productclassid)
        {
            return BonSite.Data.ProductClass.Delete(productclassid);
        }
        #endregion


        #region 后台

        /// <summary>
        /// 判断该分类是否有直接下级
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        public static bool HaveChild(int productClassId)
        {
            List<ProductClassInfo> list = BonSite.Data.ProductClass.GetList();
            foreach (ProductClassInfo Model in list)
            {
                if (Model.ParentProductClassID == productClassId)
                {
                    return true;
                }
            }
            return false;

        }





        /// <summary>
        /// 管理员获取分类列表
        /// </summary>
        /// <returns></returns>
        public static List<ProductClassInfo> AdminGetProductClassTreeList()
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            List<ProductClassInfo> slist = BonSite.Data.ProductClass.GetList();
            AdminCreateProductTree(slist, list, 0, "");

            return list;
        }

        protected static void AdminCreateProductTree(List<ProductClassInfo> sourceList, List<ProductClassInfo> resultList, int parentId, string fontStr)
        {
            foreach (ProductClassInfo Model in sourceList)
            {
                if (Model.ParentProductClassID == parentId)
                {
                    Model.ProductClassName = fontStr + Model.ProductClassName;
                    resultList.Add(Model);
                    AdminCreateProductTree(sourceList, resultList, Model.ProductClassID, fontStr + "　　");
                }
            }
        }

        /// <summary>
        /// 管理员获取所有文章分类
        /// </summary>
        /// <returns></returns>
        public static List<ProductClassInfo> AdminGetList()
        {
            return BonSite.Data.ProductClass.GetList();
        }

        /// <summary>
        /// 管理员获取文章分类
        /// </summary>
        /// <param name="articleclassid"></param>
        /// <returns></returns>
        public static ProductClassInfo AdminGetModelById(int productclassid)
        {
            if (productclassid > 0)
            {
                foreach (ProductClassInfo model in BonSite.Data.ProductClass.GetList())
                {
                    if (model.ProductClassID == productclassid)
                        return model;
                }
            }
            return null;
        }
        #endregion


        #region 前台

        ///// <summary>
        ///// 前台获得分类列表(带缓存）
        ///// </summary>
        ///// <returns></returns>
        //public static List<NavInfo> GetList()
        //{
        //    List<NavInfo> list = BonSite.Core.BSCache.Get(CacheKeys.SITE_ARTICLECLASS_LIST) as List<NavInfo>;
        //    if (list == null)
        //    {
        //        list = GetListByIsWeb();
        //        BonSite.Core.BSCache.Insert(CacheKeys.SITE_ARTICLECLASS_LIST, list);
        //    }
        //    return list;
        //}




        /// <summary>
        /// 根据ID获得分类（带缓存）
        /// </summary>
        /// <param name="articleclassid">新闻类型id</param>
        /// <returns></returns>
        public static ProductClassInfo GetModelById(int productclassid)
        {
            if (productclassid > 0)
            {
                foreach (ProductClassInfo model in AdminGetList())
                {
                    if (model.ProductClassID == productclassid)
                        return model;
                }
            }
            return null;
        }


        /// <summary>
        /// 前台获取导航树
        /// </summary>
        /// <returns></returns>
        public static List<ProductClassInfo> GetProductClassList()
        {
            List<ProductClassInfo> list = BonSite.Core.BSCache.Get(CacheKeys.SITE_PRODUCTCLASS_LIST) as List<ProductClassInfo>;
            if (list == null)
            {
                list = new List<ProductClassInfo>();
                List<ProductClassInfo> slist = BonSite.Data.ProductClass.GetProductClassList();
                CreateProductClassTree(slist, list, 0);
                BonSite.Core.BSCache.Insert(CacheKeys.SITE_PRODUCTCLASS_LIST, list);
            }
            return list;
        }



        /// <summary>
        /// 递归创建导航树
        /// </summary>
        protected static void CreateProductClassTree(List<ProductClassInfo> sourceList, List<ProductClassInfo> resultList, int parentId)
        {
            foreach (ProductClassInfo Model in sourceList)
            {
                if (Model.ParentProductClassID == parentId)
                {
                    //ProductClassInfo info = new ProductClassInfo();
                    //info.Id = Model.ArticleClassID;
                    //info.Pid = Model.ParentArticleClassID;
                    //info.Name = Model.ClassName;
                    //info.Target = Model.Target;
                    //info.DisplayOrder = Model.DisplayOrder;
                    //info.WebUrl = Model.WebUrl;
                    //if (Model.ClassType > 0)
                    //    info.Url = string.Format("/List/{0}-1.html", Model.ArticleClassID);
                    //else
                    //    info.Url = Model.WebUrl;

                    resultList.Add(Model);
                    CreateProductClassTree(sourceList, resultList, Model.ProductClassID);
                }
            }
        }

        #endregion


        #region 通用
        /// <summary>
        /// 获取分类路径Path
        /// </summary>
        /// <param name="articleClassID"></param>
        /// <returns></returns>
        public static List<ProductClassInfo> GetProductClassPath(int productClassID)
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            CreateProductClassPath(productClassID, list);

            return list;
        }

        /// <summary>
        /// 递归创建分类路径Path
        /// </summary>
        protected static void CreateProductClassPath(int productClassID, List<ProductClassInfo> resultList)
        {
            ProductClassInfo model = GetModelById(productClassID);
            if (model != null)
            {
                resultList.Insert(0, model);

                if (model.ParentProductClassID != 0)
                    CreateProductClassPath(model.ParentProductClassID, resultList);
            }

        }


        #endregion
    }
}
