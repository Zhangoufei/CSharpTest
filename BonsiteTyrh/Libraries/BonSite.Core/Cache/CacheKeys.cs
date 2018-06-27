using System;

namespace BonSite.Core
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public class CacheKeys
    {
        /// <summary>
        /// 在线游客数量缓存键
        /// </summary>
        public const string SHOP_ONLINEUSER_GUESTCOUNT = "/Site/OnlineGuestCount";
        /// <summary>
        /// 全部在线人数缓存键
        /// </summary>
        public const string SHOP_ONLINEUSER_ALLUSERCOUNT = "/Site/OnlineAllUserCount";

        /// <summary>
        /// 用户等级列表缓存键
        /// </summary>
        public const string SHOP_USERRANK_LIST = "/Site/UserRankList";

        /// <summary>
        /// 后台操作HashSet缓存键
        /// </summary>
        public const string SHOP_ADMINACTION_HASHSET = "/Site/AdminActionHashSet";
        /// <summary>
        /// 管理员组列表缓存键
        /// </summary>
        public const string SHOP_ADMINGROUP_LIST = "/Site/AdminGroupList";
        /// <summary>
        /// 管理员组操作HashSet缓存键
        /// </summary>
        public const string SHOP_ADMINGROUP_ACTIONHASHSET = "/Site/AdminGroupActionHashSet/";

        /// <summary>
        /// 被禁止的IPHashSet缓存键
        /// </summary>
        public const string SHOP_BANNEDIP_HASHSET = "/Site/BannedIPHashSet";

        /// <summary>
        /// 筛选词正则列表缓存键
        /// </summary>
        public const string SHOP_FILTERWORD_REGEXLIST = "/Site/FilterWordRegexList";

        ///// <summary>
        ///// 品牌信息缓存键
        ///// </summary>
        //public const string SHOP_BRAND_INFO = "/Site/BrandInfo/";

        ///// <summary>
        ///// 品牌分类缓存键
        ///// </summary>
        //public const string SHOP_BRAND_CATEGORYLIST = "/Site/BrandCategoryList/";
        /// <summary>
        /// 分类列表缓存键
        /// </summary>
        public const string SHOP_CATEGORY_LIST = "/Site/CategoryList";
        /// <summary>
        /// 分类品牌列表缓存键
        /// </summary>
        public const string SHOP_CATEGORY_BRANDLIST = "/Site/CategoryBrandList/";
        /// <summary>
        /// 属性值信息缓存键
        /// </summary>
        public const string SHOP_ATTRIBUTEVALUE_INFO = "/Site/AttributeValueInfo/";
        /// <summary>
        /// 分类筛选属性及其值列表缓存键
        /// </summary>
        public const string SHOP_CATEGORY_FILTERAANDVLIST = "/Site/CategoryFilterAANDVList/";
        /// <summary>
        /// 分类属性及其值列表JSON缓存键
        /// </summary>
        public const string SHOP_CATEGORY_AANDVLISTJSONCACHE = "/Site/CategoryAANDVListJSONCache/";

        /// <summary>
        /// 签名商品列表
        /// </summary>
        public const string SHOP_SIGNPRODUCT_LIST = "/Site/SignProductList/";

        /// <summary>
        /// 商品咨询类型列表缓存键
        /// </summary>
        public const string SHOP_PRODUCTCONSULTTYPE_LIST = "/Site/ProductConsultTypeList";

        /// <summary>
        /// 活动专题信息缓存键
        /// </summary>
        public const string SHOP_TOPIC_INFO = "/Site/TopicInfo/";

        /// <summary>
        /// 发放中的优惠劵类型列表缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_SENDINGLIST = "/Site/SendingCouponTypeList";
        /// <summary>
        /// 使用中的优惠劵类型列表缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_USINGLIST = "/Site/UsingCouponTypeList";
        /// <summary>
        /// 优惠劵类型信息缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_INFO = "/Site/CouponTypeInfo/";
        /// <summary>
        /// 优惠劵类型发放数量缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_SENDCOUNT = "/Site/CouponTypeSendCount/";

        /// <summary>
        /// 新闻类型列表缓存键
        /// </summary>
        public const string SHOP_NEWSTYPE_LIST = "/Site/NewsTypeList";
        /// <summary>
        /// 置首新闻列表缓存键
        /// </summary>
        public const string SHOP_NEWS_HOMELIST = "/Site/HomeNewsList/";

        /// <summary>
        /// 首页banner列表缓存键
        /// </summary>
        public const string SHOP_BANNER_HOMELIST = "/Site/HomeBannerList";

        /// <summary>
        /// 帮助列表缓存键
        /// </summary>
        public const string SHOP_HELP_LIST = "/Site/HelpList";

        /// <summary>
        /// 友情链接列表缓存键
        /// </summary>
        public const string SITE_FRIENDLINK_LIST = "/Site/FriendLinkList";

        /// <summary>
        /// 角色列表缓存键
        /// </summary>
        public const string SITE_USERROLE_LIST = "/Site/UserRoleList";

        /// <summary>
        /// 公众号缓存键
        /// </summary>
        public const string SITE_WECHAT_LIST = "/Site/WeChatList";

        /// <summary>
        /// 导航列表缓存键
        /// </summary>
        public const string SITE_NAV_LIST = "/Site/NavList";

        /// <summary>
        /// 产品分类列表缓存键
        /// </summary>
        public const string SITE_PRODUCTCLASS_LIST = "/Site/ProductClassList";

        /// <summary>
        /// 主导航列表缓存键
        /// </summary>
        public const string SITE_NAV_MAINLIST = "/Site/MainNavList";

        /// <summary>
        /// 所有文章分类缓存键
        /// </summary>
        public const string SITE_ARTICLECLASS_LIST = "/Site/ArticleClassList";

        /// <summary>
        /// 后台管理菜单缓存
        /// </summary>
        public const string SITE_ADMINMENU_LIST = "/Site/AdminMenu/list/{0}/{1}";

        /// <summary>
        /// 专题列表
        /// </summary>
        public const string SITE_SPECIAL_LIST = "/Site/SpecialList";

        ///// <summary>
        ///// 子区域列表缓存键
        ///// </summary>
        //public const string SHOP_REGION_CHILDLIST = "/Site/ChildRegionList/";
        ///// <summary>
        ///// 区域缓存键
        ///// </summary>
        //public const string SHOP_REGION_INFOBYID = "/Site/RegionInfo/";
        ///// <summary>
        ///// 区域缓存键
        ///// </summary>
        //public const string SHOP_REGION_INFOBYNAMEANDLAYER = "/Site/RegionInfo/{0}/{1}";

        /// <summary>
        /// 广告列表缓存键
        /// </summary>
        public const string SITE_ADVERT_LIST = "/Site/AdvertList/";

        /// <summary>
        /// Banner列表缓存键
        /// </summary>
        public const string SITE_BANNER_LIST = "/Site/BannerList/";
    }
}
