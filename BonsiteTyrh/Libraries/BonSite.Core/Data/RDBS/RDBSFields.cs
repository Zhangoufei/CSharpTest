using System;

namespace BonSite.Core
{
    /// <summary>
    /// 关系数据库表
    /// </summary>
    public class RDBSFields
    {
        /// <summary>
        /// 后台操作表
        /// </summary>
        public const string ADMIN_ACTIONS = "[adminaid],[title],[action],[parentid],[displayorder]";

        /// <summary>
        /// 管理员组表
        /// </summary>
        public const string ADMIN_GROUPS = "[admingid],[title],[actionlist]";

        /// <summary>
        /// 管理员操作日志表
        /// </summary>
        public const string ADMIN_OPERATELOGS = "[logid],[uid],[nickname],[admingid],[admingtitle],[operation],[description],[ip],[operatetime]";

        /// <summary>
        /// 广告位置表
        /// </summary>
        public const string ADVERT_POSITIONS = "[adposid],[title],[description]";

        /// <summary>
        /// 广告表
        /// </summary>
        public const string ADVERTS = "[Id],[clickcount],[adposid],[state],[starttime],[endtime],[displayorder],[type],[title],[url],[body]";

        /// <summary>
        /// 属性分组表
        /// </summary>
        public const string ATTRIBUTE_GROUPS = "[attrgroupid],[cateid],[name],[displayorder]";

        /// <summary>
        /// 属性表
        /// </summary>
        public const string ATTRIBUTES = "[attrid],[name],[cateid],[attrgroupid],[showtype],[isfilter],[displayorder]";

        /// <summary>
        /// 属性值表
        /// </summary>
        public const string ATTRIBUTE_VALUES = "[attrvalueid],[attrgroupid],[attrgroupname],[attrgroupdisplayorder],[attrid],[attrname],[attrdisplayorder],[attrvalue],[isinput],[attrvaluedisplayorder],[attrshowtype]";

        /// <summary>
        /// 被禁止的ip表
        /// </summary>
        public const string BANNEDIPS = "[id],[ip],[liftbantime]";

        /// <summary>
        /// banner表
        /// </summary>
        public const string BANNERS = "[id],[BanPosId],[starttime],[endtime],[isshow],[title],[img],[url],[displayorder]";

        /// <summary>
        /// banner分类表
        /// </summary>
        public const string BANNERSPOSITIONS = "[BanPosId],[Title],[Description]";

        /// <summary>
        /// 品牌表
        /// </summary>
        public const string BRANDS = "[brandid],[displayorder],[name],[logo]";

        /// <summary>
        /// 浏览历史表
        /// </summary>
        public const string BROWSEHISTORIES = "[recordid],[uid],[pid],[times],[updatetime]";

        /// <summary>
        /// 买送促销商品表
        /// </summary>
        public const string BUYSEND_PRODUCTS = "[recordid],[pmid],[pid]";

        /// <summary>
        /// 买送促销表
        /// </summary>
        public const string BUYSEND_PROMOTIONS = "[pmid],[starttime],[endtime],[userranklower],[state],[name],[type],[buycount],[sendcount]";

        /// <summary>
        /// 分类表
        /// </summary>
        public const string CATEGORIES = "[cateid],[displayorder],[name],[pricerange],[parentid],[layer],[haschild],[path]";

        /// <summary>
        /// 优惠劵商品表
        /// </summary>
        public const string COUPON_PRODUCTS = "[recordid],[coupontypeid],[pid]";

        /// <summary>
        /// 优惠劵表
        /// </summary>
        public const string COUPONS = "[couponid],[couponsn],[uid],[coupontypeid],[oid],[usetime],[useip],[money],[activatetime],[activateip],[createuid],[createoid],[createtime],[createip]";

        /// <summary>
        /// 优惠劵类型表
        /// </summary>
        public const string COUPON_TYPES = "[coupontypeid],[state],[name],[money],[count],[sendmode],[getmode],[usemode],[userranklower],[orderamountlower],[limitcateid],[limitbrandid],[limitproduct],[sendstarttime],[sendendtime],[useexpiretime],[usestarttime],[useendtime]";

        /// <summary>
        /// 积分日志表
        /// </summary>
        public const string CREDITLOGS = "[logid],[uid],[paycredits],[rankcredits],[action],[actioncode],[actiontime],[actiondes],[operator]";

        /// <summary>
        /// 事件日志表
        /// </summary>
        public const string EVENTLOGS = "[id],[key],[title],[server],[executetime]";

        /// <summary>
        /// 收藏夹表
        /// </summary>
        public const string FAVORITES = "[recordid],[uid],[pid],[state],[addtime]";

        /// <summary>
        /// 筛选词表
        /// </summary>
        public const string FILTERWORDS = "[id],[match],[replace]";

        /// <summary>
        /// 友情链接表
        /// </summary>
        public const string FRIENDLINKS = "[id],[name],[title],[logo],[url],[target],[displayorder]";

        /// <summary>
        /// 班级管理表
        /// </summary>
        public const string CLASSMANAGE = "[ClassID],[ClassName]";

        /// <summary>
        /// 角色表
        /// </summary>
        public const string USERROLE = "[RoleID],[RoleName],[Remark]";

        /// <summary>
        /// 公众号表
        /// </summary>
        public const string WXOffICIAlACCOUNTS = "[id],[name],[appid],[secret]";

        /// <summary>
        /// 满减促销商品表
        /// </summary>
        public const string FULLCUT_PRODUCTS = "[recordid],[pmid],[pid]";

        /// <summary>
        /// 满减促销表
        /// </summary>
        public const string FULLCUT_PROMOTIONS = "[pmid],[type],[starttime],[endtime],[userranklower],[state],[name],[limitmoney1],[cutmoney1],[limitmoney2],[cutmoney2],[limitmoney3],[cutmoney3]";

        /// <summary>
        /// 满赠促销商品表
        /// </summary>
        public const string FULLSEND_PRODUCTS = "[recordid],[pmid],[pid],[type]";

        /// <summary>
        /// 满赠促销表
        /// </summary>
        public const string FULLSEND_PROMOTIONS = "[pmid],[starttime],[endtime],[userranklower],[state],[name],[limitmoney],[addmoney]";

        /// <summary>
        /// 赠品表
        /// </summary>
        public const string GIFTS = "[recordid],[pmid],[giftid],[number]";

        /// <summary>
        /// 赠品促销表
        /// </summary>
        public const string GIFT_PROMOTIONS = "[pmid],[pid],[starttime1],[endtime1],[starttime2],[endtime2],[starttime3],[endtime3],[userranklower],[state],[name],[quotaupper]";

        /// <summary>
        /// 商城帮助表
        /// </summary>
        public const string HELPS = "[id],[pid],[title],[url],[description],[displayorder]";

        /// <summary>
        /// 登陆失败日志表
        /// </summary>
        public const string LOGINFAILLOGS = "[id],[loginip],[failtimes],[lastlogintime]";

        /// <summary>
        /// 导航栏表
        /// </summary>
        public const string NAVS = "[id],[pid],[layer],[name],[title],[url],[target],[displayorder]";

        /// <summary>
        /// 新闻表/后台
        /// </summary>
        public const string ARTICLE = "ArticleID,ArticleClassID,SpecialID,DisplayType,IsShow,IsTop,IsHome,IsBest,Title,Body,AddTime,UpdateTime,Author,ComeForm,ImgUrl,Url,Digest,Keys,[bs_Article].UserID,AdminID,Hits,Keyword,Description,InformType,EndTime,InformGroup,IsClassBrand,PushStatus,MicroVideo,ApprovalStatus,Praise,Auditor";

        /// <summary>
        /// 新闻表/前台
        /// </summary>
        public const string ARTICLEbefore = "ArticleID,ArticleClassID,SpecialID,DisplayType,IsShow,IsTop,IsHome,IsBest,Title,Body,AddTime,UpdateTime,Author,ComeForm,ImgUrl,Url,Digest,Keys,UserID,AdminID,Hits,Keyword,Description,InformType,EndTime,InformGroup,IsClassBrand,PushStatus,MicroVideo,ApprovalStatus,Praise,Auditor";

        /// <summary>
        /// 文章类型表
        /// </summary>
        public const string ARTICLE_CLASS = "[ArticleClassID],[ClassName],[ParentArticleClassID],[ClassType],[Target],[IsNav],[IsWeb],[WebUrl],[IsAdmin],[AdminUrl],[DisplayOrder],[ListView],[ContentView],[Code],[ImgUrl],[Keyword],[Description],[IsClassBrand],[Subhead],[Auditor]";

        /// <summary>
        /// 专题表
        /// </summary>
        public const string SPECIAL = "SpecialID,Name,Title,ImgUrl,LogoUrl,IsOut,OutUrl,Body,DisplayOrder";

        /// <summary>
        /// 开放授权表
        /// </summary>
        public const string OAUTH = "[id],[uid],[openid],[server]";

        /// <summary>
        /// 用户在线时间表
        /// </summary>
        public const string ONLINE_TIME = "[uid],[total],[year],[month],[week],[day],[updatetime]";

        /// <summary>
        /// 在线用户表
        /// </summary>
        public const string ONLINE_USERS = "[olid],[uid],[sid],[nickname],[ip],[regionid],[updatetime]";

        /// <summary>
        /// 订单处理表
        /// </summary>
        public const string ORDER_ACTIONS = "[aid],[oid],[uid],[realname],[admingid],[admingtitle],[actiontype],[actiontime],[actiondes]";

        /// <summary>
        /// 订单商品表
        /// </summary>
        public const string ORDER_PRODUCTS = "[recordid],[oid],[uid],[sid],[pid],[psn],[cateid],[brandid],[name],[showimg],[discountprice],[shopprice],[costprice],[marketprice],[weight],[isreview],[realcount],[buycount],[sendcount],[type],[paycredits],[coupontypeid],[extcode1],[extcode2],[extcode3],[extcode4],[extcode5],[addtime]";

        /// <summary>
        /// 订单退款表
        /// </summary>
        public const string ORDER_REFUNDS = "[refundid],[oid],[osn],[uid],[state],[applytime],[paymoney],[refundmoney],[refundsn],[refundsystemname],[refundfriendname],[refundtime],[paysn],[paysystemname],[payfriendname]";

        /// <summary>
        /// 订单表
        /// </summary>
        public const string ORDERS = "[oid],[osn],[uid],[orderstate],[productamount],[orderamount],[surplusmoney],[parentid],[addtime],[shipsn],[shipsystemname],[shipfriendname],[shiptime],[paysn],[paysystemname],[payfriendname],[paymode],[paytime],[regionid],[consignee],[mobile],[phone],[email],[zipcode],[address],[besttime],[shipfee],[payfee],[fullcut],[discount],[paycreditcount],[paycreditmoney],[couponmoney],[weight],[buyerremark],[ip]";

       

        /// <summary>
        /// 定时商品表
        /// </summary>
        public const string TIMEPRODUCTS = "[recordid],[pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime]";

        /// <summary>
        /// 活动专题表
        /// </summary>
        public const string TOPICS = "[topicid],[starttime],[endtime],[isshow],[sn],[title],[headhtml],[bodyhtml]";

        /// <summary>
        /// 部分用户表
        /// </summary>
        public const string PARTUSERS = "[UserID],[UserName],[Email],[Mobile],[Password],[NickName],[UserRankID],[AdminGroupID],[Avatar],[RankCredits],[VerifyEmail],[VerifyMobile],[State],[Salt]";

        /// <summary>
        /// 用户细节表
        /// </summary>
        public const string USERDETAILS = "UserID,RealName,BirthDay,Gender,IDCard,RegionID,Address,RegTime,RegIp,LastTime,LastIp,Body";

        /// <summary>
        /// 用户等级表
        /// </summary>
        public const string USER_RANKS = "[userrid],[system],[title],[avatar],[creditslower],[creditsupper],[limitdays]";

        /// <summary>
        /// 信息反馈分类表
        /// </summary>
        public const string FEEDBACK_TYPE = "FeedBackTypeId,TypeName,IsShowList,Body,Tags";

        /// <summary>
        ///  信息反馈表
        /// </summary>
        public const string FEEDBACKS = "id,feedbacktypeid,tag,linkman,tel,mobile,email,title,body,addtime,reply,replytime,state,isout,ip,searchkey";

        /// <summary>
        /// 投票表
        /// </summary>
        public const string VOTES = "id,title,startTime,EndTime,State,Type";

        /// <summary>
        /// 投票结果表
        /// </summary>
        public const string VOTE_RESULTS = "id,VoteID,DisplayOrder,Result,Count";

        //public const string ARTICLE = "ArticleID,ArticleClassID,SpecialID,DisplayType,IsShow,IsTop,IsHome,IsBest,Title,AddTime,UpdateTime,Author,Comeform,ImgUrl,Url,Digest,Keys,UserID,AdminID,Hits";
        /// <summary>
        /// 产品分类表
        /// </summary>
        public const string PRODUCT_CLASS = "ProductClassId,ProductClassName,ParentProductClassID,DisplayOrder";

        /// <summary>
        /// 产品表
        /// </summary>
        public const string PRODUCT = "[ProductID],[ProductClassID],[Code],[Type],[Provider],[IsShow],[IsTop],[IsBest],[Title],[Body],[AddTime],[UpdateTime],[ImgUrl],[BigImgUrl],[Digest],[Keys],[AdminID],[Hits],[DisplayOrder],[Keyword],[Description]";

        /// <summary>
        /// 门店表
        /// </summary>
        public const string SHOPS = "[ShopID],[ShopName],[Address],[Tel],[Fax],[ShopImg],[Position],[Body],[Area],[Type],[OrderID],[Remark]";

        /// <summary>
        /// 招聘信息表
        /// </summary>
        public const string JOBS = "[JobID],[JobTitle],[PubDate],[EndDate],[Number],[State],[Body],[City]";


        public const string PRODUCTFEEDBACKS = "[Id],[CityName],[ProductName],[ProductModel],[CustomerName],[Contact],[Address],[Body],[State],[CreateTime],[WeChatName],[WeChatOpenId],[Imgs]";

        public const string SERVICEEVAL = "[Id],[Name],[Contact],[Province],[City],[Courier],[EvalProduct],[EvalLogistics],[EvalService],[Body],[CreateTime],[State],[WeChatName],[WeChatOpenId]";

        public const string LOGSERVER = "id,title,content,userName,ip,createTime";
    }
}
