using System;
using System.Data;
using System.Collections.Generic;

namespace BonSite.Core
{ 
    /// <summary>
    /// BonSite关系数据库策略之产品分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        /// <summary>
        /// 创建产品分类
        /// </summary>
        /// <param name="productClassInfo"></param>
        /// <returns></returns>
        int CreateProductClass(ProductClassInfo productClassInfo);

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="productClassId"></param>
        /// <returns></returns>
        bool DeleteProductClass(int productClassId);

        /// <summary>
        /// 更新产品分类
        /// </summary>
        /// <param name="productClassInfo"></param>
        /// <returns></returns>
        bool UpdateProductClass(ProductClassInfo productClassInfo);

        /// <summary>
        /// 获取产品分类列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetProductClassList();

        /// <summary>
        /// 新加产品
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        int CreateProduct(ProductInfo productInfo);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="ProductIdList"></param>
        /// <returns></returns>
        bool DeleteProductById(string ProductIdList);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        bool UpdateProduct(ProductInfo productInfo);

        /// <summary>
        /// 获取产品详情
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IDataReader GetProductById(int productId);

        /// <summary>
        /// 后台获取产品列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable AdminGetProductList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获取产品列表搜索条件
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        string AdminGetProductListCondition(int productClassId, string title);

        /// <summary>
        /// 后台获取产品列表排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        string AdminGetProductListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获取产品数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int AdminGetProductCount(string condition);

        /// <summary>
        /// 获取置顶产品
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortConlumn"></param>
        /// <returns></returns>
        IDataReader GetTopProductList(int productClassId, int count, string sortConlumn);

        /// <summary>
        /// 获取置顶产品
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortConlumn"></param>
        /// <returns></returns>
        IDataReader GetTopProductList(string productClassId, int count, string sortConlumn);

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortConlumn"></param>
        /// <returns></returns>
        IDataReader GetBestProductList(int productClassId, int count, string sortConlumn);

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortConlumn"></param>
        /// <returns></returns>
        IDataReader GetBestProductList(string productClassId, int count, string sortConlumn);

        /// <summary>
        /// 获取最新产品列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortConlumn"></param>
        /// <returns></returns>
        IDataReader GetProductList(int productClassId, int count, string sortConlumn);

        /// <summary>
        /// 获取最新产品列表
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortConlumn"></param>
        /// <returns></returns>
        IDataReader GetProductList(string productClassId, int count, string sortConlumn);

        /// <summary>
        /// 获取产品表表条件
        /// </summary>
        /// <param name="productClassId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        string GetProductListCondition(int productClassId, string title);

        /// <summary>
        /// 获取产品列表排序
        /// </summary>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        string GetProductListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 获取产品分页列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable GetProductList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 获取产品数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int GetProductCount(string condition);

    }
}
