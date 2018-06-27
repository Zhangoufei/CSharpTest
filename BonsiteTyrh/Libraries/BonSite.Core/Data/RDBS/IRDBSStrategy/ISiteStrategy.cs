using System;
using System.Data;
using System.Collections.Generic;
using BonSite.Core.Domain.Site;

namespace BonSite.Core
{
    /// <summary>
    /// BonSite关系数据库策略之系统分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        #region PV统计

        /// <summary>
        /// 更新pv统计
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="value">值</param>
        /// <param name="count">数量</param>
        //void UpdatePVStat(string category, string value, int count);

        /// <summary>
        /// 获得省级区域统计
        /// </summary>
        /// <returns></returns>
        //DataTable GetProvinceRegionStat();

        /// <summary>
        /// 获得市级区域统计
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <returns></returns>
        //DataTable GetCityRegionStat(int provinceId);

        /// <summary>
        /// 获得区/县级区域统计
        /// </summary>
        /// <param name="cityId">市id</param>
        /// <returns></returns>
        //DataTable GetCountyRegionStat(int cityId);

        /// <summary>
        /// 获得PV统计列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        //IDataReader GetPVStatList(string condition);

        /// <summary>
        /// 获得PV统计
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        //IDataReader GetPVStatByCategoryAndValue(string category, string value);

        #endregion

        #region 禁止IP

        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        //IDataReader GetBannedIPList();

        /// <summary>
        /// 获得禁止的ip
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        //IDataReader GetBannedIPById(int id);

        /// <summary>
        /// 获得禁止IP的id
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        //int GetBannedIPIdByIP(string ip);

        /// <summary>
        /// 添加禁止的ip
        /// </summary>
        //void AddBannedIP(BannedIPInfo bannedIPInfo);

        /// <summary>
        /// 更新禁止的ip
        /// </summary>
        //void UpdateBannedIP(BannedIPInfo bannedIPInfo);

        /// <summary>
        /// 删除禁止的ip
        /// </summary>
        /// <param name="idList">id列表</param>
        //void DeleteBannedIPById(string idList);

        /// <summary>
        /// 后台获得禁止的ip列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="ip">ip</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        //DataTable AdminGetBannedIPList(int pageSize, int pageNumber, string ip, string sort);

        /// <summary>
        /// 后台获得禁止的ip列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        //string AdminGetBannedIPListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得禁止的ip数量
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        //int AdminGetBannedIPCount(string ip);

        #endregion

        #region 筛选词

        /// <summary>
        /// 获得筛选词列表
        /// </summary>
        /// <returns></returns>
        //IDataReader GetFilterWordList();

        /// <summary>
        /// 添加筛选词
        /// </summary>
        //void AddFilterWord(FilterWordInfo filterWordInfo);

        /// <summary>
        /// 更新筛选词
        /// </summary>
        //void UpdateFilterWord(FilterWordInfo filterWordInfo);

        /// <summary>
        /// 删除筛选词
        /// </summary>
        /// <param name="idList">id列表</param>
        //void DeleteFilterWordById(string idList);

        #endregion

        #region 登陆失败日志

        /// <summary>
        /// 获得登陆失败日志
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <returns></returns>
        //IDataReader GetLoginFailLogByIP(Int64 loginIP);

        /// <summary>
        /// 增加登陆失败次数
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <param name="loginTime">登陆时间</param>
        //void AddLoginFailTimes(Int64 loginIP, DateTime loginTime);

        /// <summary>
        /// 删除登陆失败日志
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        //void DeleteLoginFailLogByIP(Int64 loginIP);

        #endregion

        #region 管理员操作日志

        /// <summary>
        /// 创建管理员操作日志
        /// </summary>
        //void CreateAdminOperateLog(AdminOperateLogInfo adminOperateLogInfo);

        /// <summary>
        /// 获得管理员操作日志列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        //IDataReader GetAdminOperateLogList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 获得管理员操作日志列表条件
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="operation">操作行为</param>
        /// <param name="startTime">操作开始时间</param>
        /// <param name="endTime">操作结束时间</param>
        /// <returns></returns>
        //string GetAdminOperateLogListCondition(int uid, string operation, string startTime, string endTime);

        /// <summary>
        /// 获得管理员操作日志数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        //int GetAdminOperateLogCount(string condition);

        /// <summary>
        /// 删除管理员操作日志
        /// </summary>
        /// <param name="idList">日志id</param>
        //void DeleteAdminOperateLogById(string idList);

        #endregion

        #region 导航栏-Old

        /// <summary>
        /// 获得导航栏列表
        /// </summary>
        /// <returns></returns>
        //IDataReader GetNavList();

        /// <summary>
        /// 创建导航栏
        /// </summary>
        //void CreateNav(NavInfo navInfo);

        /// <summary>
        /// 删除导航栏
        /// </summary>
        /// <param name="id">导航栏id</param>
        //void DeleteNavById(int id);

        /// <summary>
        /// 更新导航栏
        /// </summary>
        //void UpdateNav(NavInfo navInfo);

        #endregion

        #region banner

        /// <summary>
        /// 获得首页banner列表
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        DataTable GetBannerList(DateTime nowTime, int banPosId);

        /// <summary>
        /// 后台获得banner列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        DataTable AdminGetBannerList(int banPosId, int pageSize, int pageNumber);

        /// <summary>
        /// 后台获得banner数量
        /// </summary>
        /// <returns></returns>
        int AdminGetBannerCount(int banPosId);

        /// <summary>
        /// 后台获得banner
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        IDataReader AdminGetBannerById(int id);

        /// <summary>
        /// 创建banner
        /// </summary>
        int CreateBanner(BannerInfo bannerInfo);

        /// <summary>
        /// 更新banner
        /// </summary>
        void UpdateBanner(BannerInfo bannerInfo);

        /// <summary>
        /// 删除banner
        /// </summary>
        /// <param name="idList">id列表</param>
        void DeleteBannerById(int id);

        /// <summary>
        /// 管理员获取Banner位置列表
        /// </summary>
        /// <returns></returns>
        IDataReader AdminGetBannerPositionList();

        /// <summary>
        /// 管理员获取Banner位置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataReader AdminGetBannerPositionByBanPosId(int banPosId);

        /// <summary>
        /// 创建Banner位置列表
        /// </summary>
        /// <param name="bannerPositionInfo"></param>
        /// <returns></returns>
        int CreateBannerPosition(BannerPositionInfo bannerPositionInfo);

        /// <summary>
        /// 更新Banner位置列表
        /// </summary>
        /// <param name="bannerPositionInfo"></param>
        void UpdateBannerPosition(BannerPositionInfo bannerPositionInfo);

        /// <summary>
        /// 删除Banner位置列表
        /// </summary>
        /// <param name="idList"></param>
        void DeleteBannerPosition(int BanPosId);
        

        #endregion

        #region 广告

        /// <summary>
        /// 获得广告位置列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        IDataReader GetAdvertPositionList(int pageSize, int pageNumber);

        /// <summary>
        /// 获得广告位置数量
        /// </summary>
        /// <returns></returns>
        int GetAdvertPositionCount();

        /// <summary>
        /// 获得全部广告位置
        /// </summary>
        /// <returns></returns>
        IDataReader GetAllAdvertPosition();

        /// <summary>
        /// 创建广告位置
        /// </summary>
        void CreateAdvertPosition(AdvertPositionInfo advertPositionInfo);

        /// <summary>
        /// 更新广告位置
        /// </summary>
        void UpdateAdvertPosition(AdvertPositionInfo advertPositionInfo);

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        IDataReader GetAdvertPositionById(int adPosId);

        /// <summary>
        /// 删除广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        void DeleteAdvertPositionById(int adPosId);

        /// <summary>
        /// 创建广告
        /// </summary>
        void CreateAdvert(AdvertInfo advertInfo);

        /// <summary>
        /// 更新广告
        /// </summary>
        void UpdateAdvert(AdvertInfo advertInfo);

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="adId">广告id</param>
        void DeleteAdvertById(int adId);

        /// <summary>
        /// 后台获得广告
        /// </summary>
        /// <param name="adId">广告id</param>
        /// <returns></returns>
        IDataReader AdminGetAdvertById(int adId);

        /// <summary>
        /// 后台获得广告列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        DataTable AdminGetAdvertList(int pageSize, int pageNumber, int adPosId);

        /// <summary>
        /// 后台获得广告数量
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        int AdminGetAdvertCount(int adPosId);

        /// <summary>
        /// 获得广告列表
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        IDataReader GetAdvertList(int adPosId, DateTime nowTime);

        #endregion

        #region 管理员权限
        /// <summary>
        /// 创建管理员菜单
        /// </summary>
        /// <param name="adminMenuInfo"></param>
        /// <returns></returns>
        /// 
        int CreateAdminMenu(AdminMenuInfo adminMenuInfo);
        /// <summary>
        /// 删除管理员菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        bool DeleteAdminMenu(int userId, int articleClassId);

        /// <summary>
        /// 删除管理员菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteAdminMenu(int userId);
        
        /// <summary>
        /// 判断管理员菜单是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleClassId"></param>
        /// <returns></returns>
        bool ExistsAdminMenu(int userId, int articleClassId);

        #endregion

        #region 文章类型

        /// <summary>
        /// 创建分类
        /// </summary>
        int CreateArticleClass(ArticleClassInfo articleClassInfo);

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="articleClassId">新闻分类id</param>
        bool DeleteArticleClassById(int articleClassId);

        /// <summary>
        /// 更新分类
        /// </summary>
        bool UpdateArticleClass(ArticleClassInfo articleClassInfo);

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetArticleClassList();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataReader GetArticleClassList(int ArticleClassID, bool isHaveChild);
        
        /// <summary>
        /// 获取导航列表()
        /// </summary>
        /// <returns></returns>
        IDataReader GetNavList();

        /// <summary>
        /// 获取可投稿栏目列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetOpenArticleClassList();

        /// <summary>
        /// 获取管理员菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDataReader GetAdminMenu(int adminGroupId);


        ///// <summary>
        ///// 获取管理员菜单
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //IDataReader GetAdminMenu(int userId);

        #endregion


        #region 文章

        /// <summary>
        /// 创建文章
        /// </summary>
        int CreateArticle(ArticleInfo articleInfo);

        

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="ArticleIdList">新闻id列表</param>
        bool DeleteArticleById(string ArticleIdList);

        /// <summary>
        /// 更新文章
        /// </summary>
        bool UpdateArticle(ArticleInfo articleInfo);

        /// <summary>
        /// 获得文章
        /// </summary>
        /// <param name="newsId">文章id</param>
        /// <returns></returns>
        IDataReader GetArticleById(int articleId);

        /// <summary>
        /// 后台获得文章
        /// </summary>
        /// <param name="newsId">文章id</param>
        /// <returns></returns>
        //IDataReader AdminGetArticleById(int articleId);

        /// <summary>
        /// 后台获得文章列表(获取所有)
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetArticleList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得文章列表搜索条件
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        string AdminGetArticleListCondition(int articleClassId, string title);

        /// <summary>
        /// 后台获得文章列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetArticleListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得文章数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetArticleCount(string condition);

        /// <summary>
        /// 后台根据文章标题得到新闻id
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        //int AdminGetArticleIdByTitle(string title);

        /// <summary>
        /// 获得置首的文章列表
        /// </summary>
        /// <param name="articleClassId">新闻类型id(0代表全部类型)</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        IDataReader GetHomeArticleList(int articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获得置首的文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IDataReader GetHomeArticleList(string articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获得置顶的文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        IDataReader GetTopArticleList(int articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获得置顶的文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        IDataReader GetTopArticleList(string articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获取推荐的文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        IDataReader GetBestArticleList(int articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获取推荐的文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        IDataReader GetBestArticleList(string articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获取最新的文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        IDataReader GetArticleList(int articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获取最新的文章列表
        /// </summary>
        /// <param name="articleClassId"></param>
        /// <param name="count"></param>
        /// <param name="sortColumn"></param>
        /// <returns></returns>
        IDataReader GetArticleList(string articleClassId, int count, string sortColumn);

        /// <summary>
        /// 获得文章列表条件
        /// </summary>
        /// <param name="articleClassId">新闻类型id(0代表全部类型)</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        string GetArticleListCondition(int articleClassId, string title);


        /// <summary>
        /// 获得文章列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string GetArticleListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 获得文章列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable GetArticleList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 获得文章数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int GetArticleCount(string condition);

        /// <summary>
        /// 根据分类ID，获取分类中文章的ID
        /// </summary>
        /// <param name="ArticleClassId"></param>
        /// <returns></returns>
        int GetArticleIdByArticleClassId(int ArticleClassId);
        //根据分类ID，获取分类中最新文章的ID
        int GetTopArticleIdByArticleClassId(int ArticleClassId);
        //查询是班牌专栏的信息
        int GetArticleClassIdByIsClassBrand();
        //获取子栏目
        DataTable GetArtClaIdByParentArticleClassID(int ParentArticleClassID);
        //根据子菜单获取父菜单名字
        DataTable GetNameByArticleClassID(int ArticleClassID);
        //根据子菜单获取父菜单名字
        int GetNameByArticleClassID2(int ArticleClassID);


        #endregion

        #region 专题
        /// <summary>
        /// 创建专题
        /// </summary>
        /// <param name="specialInfo"></param>
        /// <returns></returns>
        int CreateSpecial(SpecialInfo specialInfo);

        /// <summary>
        /// 删除专题
        /// </summary>
        /// <param name="specialID"></param>
        /// <returns></returns>
        bool DeleteSpecialById(int specialID);

        /// <summary>
        /// 更新专题
        /// </summary>
        /// <param name="specialInfo"></param>
        /// <returns></returns>
        bool UpdateSpecial(SpecialInfo specialInfo);

        /// <summary>
        /// 获取专题
        /// </summary>
        /// <param name="specialID"></param>
        /// <returns></returns>
        IDataReader GetSpecialById(int specialID);

        /// <summary>
        /// 获取所有专题
        /// </summary>
        /// <returns></returns>
        IDataReader GetSpecialList();

        /// <summary>
        /// 获取前N条专题
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IDataReader GetTopSpecialList(int count);

        #endregion

        #region 帮助

        ///// <summary>
        ///// 创建帮助
        ///// </summary>
        //void CreateHelp(HelpInfo helpInfo);

        ///// <summary>
        ///// 删除帮助
        ///// </summary>
        ///// <param name="id">帮助id</param>
        //void DeleteHelpById(int id);

        ///// <summary>
        ///// 更新帮助
        ///// </summary>
        //void UpdateHelp(HelpInfo helpInfo);

        ///// <summary>
        ///// 获得帮助列表
        ///// </summary>
        ///// <returns></returns>
        //IDataReader GetHelpList();

        ///// <summary>
        ///// 更新帮助排序
        ///// </summary>
        ///// <param name="id">帮助id</param>
        ///// <param name="displayOrder">排序</param>
        ///// <returns></returns>
        //bool UpdateHelpDisplayOrder(int id, int displayOrder);

        #endregion

        #region 班级管理
        /// <summary>
        /// 获得班级列表
        /// </summary>
        DataTable GetClassManageList();
        /// <summary>
        /// 创建班级
        /// </summary>
        int CreateClassManage(ClassManageInfo classManageInfo);
        /// <summary>
        /// 更新班级
        /// </summary>
        bool UpdateClassManage(ClassManageInfo classManageInfo);
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="idList">班级id</param>
        bool DeleteClassManageById(string idList);
        #endregion

        #region 友情链接

        /// <summary>
        /// 获得友情链接列表
        /// </summary>
        DataTable GetFriendLinkList();

        /// <summary>
        /// 创建友情链接
        /// </summary>
        int CreateFriendLink(FriendLinkInfo friendLinkInfo);

        /// <summary>
        /// 更新友情链接
        /// </summary>
        bool UpdateFriendLink(FriendLinkInfo friendLinkInfo);

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="idList">友情链接id</param>
        bool DeleteFriendLinkById(string idList);

        #endregion

        #region 角色管理

        /// <summary>
        /// 获得角色列表
        /// </summary>
        DataTable GetUserRoleList();

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <returns></returns>
        int CreateUserRole(UserRoleInfo userRoleInfo);

        

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleIDList">友情链接id</param>
        bool DeleteUserRoleId(string RoleIDList);

        /// <summary>
        /// 更新角色
        /// </summary>
        bool UpdateUserRole(UserRoleInfo userRoleInfo);



        /// <summary>
        /// 创建角色菜单
        /// </summary>
        /// <param name="roleMenuInfo"></param>
        /// <returns></returns>
        /// 
        int CreateRoleMenu(RoleMenuInfo roleMenuInfo);

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ArticleClassId"></param>
        /// <returns></returns>
        bool DeleteRoleMenu(int RoleID, int ArticleClassId);

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        bool DeleteRoleMenu(int RoleID);

        /// <summary>
        /// 判断角色菜单是否存在
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ArticleClassId"></param>
        /// <returns></returns>
        bool ExistsRoleMenu(int RoleID, int ArticleClassId);
        


        #endregion

        #region 公众号管理

        /// <summary>
        /// 获得公众号列表
        /// </summary>
        DataTable GetWeChatList();

        /// <summary>
        /// 创建公众号
        /// </summary>
        /// <returns></returns>
        int CreateWeChat(WeChatInfo weChatInfo);



        /// <summary>
        /// 删除公众号
        /// </summary>
        /// <param name="RoleIDList">友情链接id</param>
        bool DeleteWeChat(string idList);

        /// <summary>
        /// 更新公众号
        /// </summary>
        bool UpdateWeChat(WeChatInfo weChatInfo);


        #endregion

        #region 信息反馈
        /// <summary>
        /// 获取信息反馈分类
        /// </summary>
        /// <returns></returns>
        IDataReader GetFeedBackTypeList();

        /// <summary>
        /// 创建信息反馈分类
        /// </summary>
        /// <param name="feedBackTypeInfo"></param>
        /// <returns></returns>
        void CreateFeedBackType(FeedBackTypeInfo feedBackTypeInfo);

        /// <summary>
        /// 更新信息反馈分类
        /// </summary>
        /// <param name="feedBackTypeInfo"></param>
        /// <returns></returns>
        void UpdateFeedBackType(FeedBackTypeInfo feedBackTypeInfo);

        /// <summary>
        /// 获取信息反馈分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataReader GetFeedBackType(int id);       

        /// <summary>
        /// 删除信息反馈分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteFeedBackType(int id);

        /// <summary>
        /// 前台获取反馈信息列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable GetFeedBackList(int pageSize, int pageNumber, int typeID);

        /// <summary>
        /// 前台获取信息反馈的数量
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        int GetFeedBackListCount(int typeId);

        /// <summary>
        /// 管理员获取信息反馈的数量
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        int AdminGetFeedBackListCount(int typeId);

        /// <summary>
        /// 管理员获取信息反馈列表
        /// </summary>
        /// <param name="TypeID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable AdminGetFeedBackList(int pageSize, int pageNumber, int typeID);

        /// <summary>
        /// 创建信息反馈
        /// </summary>
        /// <param name="feedBackInfo"></param>
        /// <returns></returns>
        void CreateFeedBack(FeedBackInfo feedBackInfo);

        /// <summary>
        /// 更新信息反馈
        /// </summary>
        /// <param name="feedBackInfo"></param>
        /// <returns></returns>
        void UpdateFeedBack(FeedBackInfo feedBackInfo);

        /// <summary>
        /// 获取信息反馈
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataReader GetFeedBack(int id);

        /// <summary>
        /// 根据查询码获取信息反馈内容
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IDataReader GetFeedBackBySearchKey(string searchKey);

        /// <summary>
        /// 删除信息反馈
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteFeedBack(int id);

        #endregion

        #region 投票
        /// <summary>
        /// 获取首页投票列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IDataReader GetHomeVoteList(int count,DateTime nowTime);

        /// <summary>
        /// 前台获取投票列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IDataReader GetVoteList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获取投票列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="condition"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        DataTable AdminGetVoteList(int pageSize, int pageNumber);

        /// <summary>
        /// 后台获取投票数量
        /// </summary>
        /// <returns></returns>
        int AdminGetVoteCount();

        /// <summary>
        /// 创建投票
        /// </summary>
        /// <param name="voteInfo"></param>
        /// <returns></returns>
        int CreateVote(VoteInfo voteInfo);

        /// <summary>
        /// 更新投票
        /// </summary>
        /// <param name="voteInfo"></param>
        void UpdateVote(VoteInfo voteInfo);

        /// <summary>
        /// 删除投票
        /// </summary>
        /// <param name="Id"></param>
        void DeleteVote(int Id);

        /// <summary>
        /// 获取投票信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IDataReader GetVote(int Id);

        /// <summary>
        /// 获取投票结果
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        IDataReader GetVoteResultsList(int voteId);

        /// <summary>
        /// 获取投票结果
        /// </summary>
        /// <param name="voteResultId"></param>
        /// <returns></returns>
        IDataReader GetVoteResult(int voteResultId);

        /// <summary>
        /// 创建投票结果
        /// </summary>
        /// <param name="voteResultInfo"></param>
        void CreateVoteResults(VoteResultInfo voteResultInfo);

        /// <summary>
        /// 更新投票结果
        /// </summary>
        /// <param name="voteResultInfo"></param>
        void UpdateVoteResults(VoteResultInfo voteResultInfo);

        /// <summary>
        /// 删除投票结果
        /// </summary>
        /// <param name="Id"></param>
        void DeleteVoteResult(int Id);

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="Result"></param>
        void VoteResults(int Result);        

        #endregion


        #region 全国行政区域

        /// <summary>
        /// 获得全部区域
        /// </summary>
        /// <returns></returns>
        //DataTable GetAllRegion();

        /// <summary>
        /// 获得区域列表
        /// </summary>
        /// <param name="parentId">父id</param>
        /// <returns></returns>
        //IDataReader GetRegionList(int parentId);

        /// <summary>
        /// 获得区域
        /// </summary>
        /// <param name="regionId">区域id</param>
        /// <returns></returns>
        //IDataReader GetRegionById(int regionId);

        /// <summary>
        /// 获得区域
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="layer">级别</param>
        /// <returns></returns>
        //IDataReader GetRegionByNameAndLayer(string name, int layer);

        #endregion

        #region 积分日志

        ///// <summary>
        ///// 后台获得积分日志列表
        ///// </summary>
        ///// <param name="pageSize">每页数</param>
        ///// <param name="pageNumber">当前页数</param>
        ///// <param name="condition">条件</param>
        ///// <returns></returns>
        //DataTable AdminGetCreditLogList(int pageSize, int pageNumber, string condition);

        ///// <summary>
        ///// 后台获得积分日志列表条件
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="startTime">开始时间</param>
        ///// <param name="endTime">结束时间</param>
        ///// <returns></returns>
        //string AdminGetCreditLogListCondition(int uid, string startTime, string endTime);

        ///// <summary>
        ///// 后台获得积分日志数量
        ///// </summary>
        ///// <param name="condition">条件</param>
        ///// <returns></returns>
        //int AdminGetCreditLogCount(string condition);

        ///// <summary>
        ///// 发放积分
        ///// </summary>
        ///// <param name="userRid">用户等级id</param>
        ///// <param name="creditLogInfo">积分日志信息</param>
        //void SendCredits(int userRid, CreditLogInfo creditLogInfo);

        ///// <summary>
        ///// 获得今天发放的支付积分
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="today">今天日期</param>
        ///// <returns></returns>
        //int GetTodaySendPayCredits(int uid, DateTime today);

        ///// <summary>
        ///// 获得今天发放的等级积分
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="today">今天日期</param>
        ///// <returns></returns>
        //int GetTodaySendRankCredits(int uid, DateTime today);

        ///// <summary>
        ///// 是否发放了今天的登陆积分
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="today">今天日期</param>
        ///// <returns></returns>
        //bool IsSendTodayLoginCredit(int uid, DateTime today);

        ///// <summary>
        ///// 是否发放了完成用户信息积分
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <returns></returns>
        //bool IsSendCompleteUserInfoCredit(int uid);

        ///// <summary>
        ///// 获得支付积分日志列表
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="type">类型(2代表全部类型，0代表收入，1代表支出)</param>
        ///// <param name="pageSize">每页数</param>
        ///// <param name="pageNumber">当前页数</param>
        ///// <returns></returns>
        //IDataReader GetPayCreditLogList(int uid, int type, int pageSize, int pageNumber);

        ///// <summary>
        ///// 获得支付积分日志数量
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="type">类型(2代表全部类型，0代表收入，1代表支出)</param>
        ///// <returns></returns>
        //int GetPayCreditLogCount(int uid, int type);

        ///// <summary>
        ///// 获得用户订单发放的积分
        ///// </summary>
        ///// <param name="oid">订单id</param>
        ///// <returns></returns>
        //DataTable GetUserOrderSendCredits(int oid);

        #endregion

        #region 事件日志

        /// <summary>
        /// 创建事件日志
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="title">标题</param>
        /// <param name="server">服务器名称</param>
        /// <param name="executeTime">执行时间</param>
        //void CreateEventLog(string key, string title, string server, DateTime executeTime);

        /// <summary>
        /// 获得事件的最后执行时间
        /// </summary>
        /// <param name="key">事件key</param>
        /// <returns></returns>
        //DateTime GetEventLastExecuteTimeByKey(string key);

        #endregion
    }
}
