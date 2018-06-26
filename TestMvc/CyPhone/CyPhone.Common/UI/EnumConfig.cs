using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI.WebControls;

/*****************
 * 枚举
 * create by shuai 2017年12月10日 19:28:39
 *****************/
namespace CyPhone.Common.UI
{
    /// <summary>
    /// 系统枚举
    /// </summary>
    public static class EnumConfig
    {
        /// <summary>
        /// 通用状态
        /// </summary>
        public enum CommonState
        {
            /// <summary>
            /// 禁用、否
            /// </summary>
            [Description("禁用、否")]
            Disable = 0,
            /// <summary>
            /// 启用、是
            /// </summary>
            [Description("启用、是")]
            Enable = 1
        }

        /// <summary>
        /// 订单种类
        /// </summary>
        /// create by shuai 2018年1月14日 01:49:30
        public enum OrderCate
        {
            /// <summary>
            /// 维修服务卡
            /// </summary>
            [Description("维修服务卡")]
            Product = 1,
            /// <summary>
            /// 积分商品
            /// </summary>
            [Description("积分商品")]
            Goods = 2
        }

        #region 用户

        /// <summary>
        /// 用户状态
        /// </summary>
        public enum UserState
        {
            /// <summary>
            /// 正常
            /// </summary>
            [Description("正常")]
            Normal = 1,
            /// <summary>
            /// 冻结
            /// </summary>
            [Description("冻结")]
            Frozen = 2,
            /// <summary>
            /// 删除
            /// </summary>
            [Description("删除")]
            Delete = 3,
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// 未知
            /// </summary>
            [Description("未知")]
            Unknown = 0,
            /// <summary>
            /// 男
            /// </summary>
            [Description("男")]
            Male = 1,
            /// <summary>
            /// 女
            /// </summary>
            [Description("女")]
            Female = 2
        }

        /// <summary>
        /// 关注状态
        /// </summary>
        public enum IsAttention
        {
            /// <summary>
            /// 未关注
            /// </summary>
            [Description("未关注")]
            No = 0,
            /// <summary>
            /// 已关注
            /// </summary>
            [Description("已关注")]
            Yes = 1,
            /// <summary>
            /// 取消关注
            /// </summary>
            [Description("取消关注")]
            Cancel = 2
        }

        public enum feedType
        {
            /// <summary>
            /// 意见与反馈
            /// </summary>
            [Description("意见与反馈")]
            Feedback = 1,
            /// <summary>
            /// 积分兑换留言
            /// </summary>
            [Description("积分兑换")]
            LeaveMsg = 2
        }
        /// <summary>
        /// 用户角色
        /// </summary>
        public enum UserRole
        {
            [Description("系统管理员")]
            Admin=101,
            [Description("客服")]
            Service =102,
            [Description("维修")]
            Repair =103,
            [Description("财务")]
            Finance =104,
            [Description("运营")]
            Business =105,
            [Description("普通用户")]
            User =120,
            [Description("代理")]
            Agent =130
        }
        #endregion

        #region 优惠券

        /// <summary>
        /// 优惠券类型
        /// </summary>
        public enum CouponType
        {
            /// <summary>
            /// 关注公众号
            /// </summary>
            [Description("关注公众号")]
            Attention = 0,
            /// <summary>
            /// 线下发送
            /// </summary>
            [Description("线下发送")]
            Offline = 1
        }

        /// <summary>
        /// 优惠券状态
        /// </summary>
        public enum CouponStatus
        {
            /// <summary>
            /// 未使用
            /// </summary>
            [Description("未使用")]
            NotUsed = 0,
            /// <summary>
            /// 冻结
            /// </summary>
            [Description("冻结")]
            Frozen = 1,
            /// <summary>
            /// 已使用
            /// </summary>
            [Description("已使用")]
            Used = 2,
            /// <summary>
            /// 已过期
            /// </summary>
            [Description("已过期")]
            Expired = 3
        }

        #endregion

        #region 积分

        /// <summary>
        /// 积分变动方式
        /// </summary>
        public enum PointsUpMethod
        {
            /// <summary>
            /// 增加
            /// </summary>
            [Description("增加")]
            Add = 1,
            /// <summary>
            /// 减少
            /// </summary>
            [Description("减少")]
            Reduce
        }

        /// <summary>
        /// 获取积分来源
        /// </summary>
        public enum PointsSource
        {
            /// <summary>
            /// 注册
            /// </summary>
            [Description("注册")]
            Register = 1,
            /// <summary>
            /// 邀请
            /// </summary>
            [Description("邀请")]
            Invitation =2,
            /// <summary>
            /// 下单奖励
            /// </summary>
            [Description("下单奖励")]
            BuyProduct=3,
            /// <summary>
            /// 邀请好友下单奖励
            /// </summary>
            [Description("邀请好友下单奖励")]
            InviteBuy=4,
            /// <summary>
            /// 兑换商品
            /// </summary>
            [Description("兑换商品")]
            Exchange = 5
        }

        #endregion

        #region 网站
        /// <summary>
        /// 广告位code
        /// </summary>
        public enum AdvCode
        {
            /// <summary>
            /// PC首页轮播图
            /// </summary>
            [Description("PC首页轮播图")]
            PcIndexCarousel = 101,
            /// <summary>
            /// Web首页轮播图
            /// </summary>
            [Description("Wap首页轮播图")]
            WapIndexCarousel = 102,
            /// <summary>
            /// Wap公众号首页底部广告位
            /// </summary>
            [Description("Wap公众号首页底部广告位")]
            WapIndexBottom = 103,
            /// <summary>
            /// 积分商城轮播图
            /// </summary>
            [Description("积分商城轮播图")]
            GoodsCarousel = 104
        }

        /// <summary>
        /// 广告位显示方式
        /// </summary>
        public enum AdvDisplayMode
        {
            /// <summary>
            /// 单个
            /// </summary>
            [Description("单个")]
            Single = 1,
            /// <summary>
            /// 轮播
            /// </summary>
            [Description("轮播")]
            Multiple = 2
        }

        /// <summary>
        /// 广告位类型
        /// </summary>
        public enum AdvType
        {
            /// <summary>
            /// 图片
            /// </summary>
            [Description("图片")]
            Image = 1,
            /// <summary>
            /// 其它
            /// </summary>
            [Description("其它")]
            Other = 2
        }

        /// <summary>
        /// 新闻类型code
        /// </summary>
        public enum ArticleTypeCode
        {
            /// <summary>
            /// 企业简介
            /// </summary>
            [Description("企业简介")]
            Intro = 1,
            /// <summary>
            /// 服务保障
            /// </summary>
            [Description("服务保障")]
            Service = 2,
            /// <summary>
            /// 售后服务
            /// </summary>
            [Description("售后服务")]
            AfterSale = 3,
            /// <summary>
            /// 常见问题
            /// </summary>
            [Description("常见问题")]
            Questions = 4,
            /// <summary>
            /// 维修协议
            /// </summary>
            [Description("维修协议")]
            Protocol = 5
        }

        #endregion

        #region 系统管理
        /// <summary>
        /// 主机Code
        /// </summary>
        public enum HostCode
        {
            /// <summary>
            /// Web
            /// </summary>
            [Description("Web")]
            Web = 1000,
            /// <summary>
            /// 管理平台
            /// </summary>
            [Description("管理平台")]
            Manager = 1001,
            /// <summary>
            /// 微信公众平台
            /// </summary>
            [Description("微信公众平台")]
            Wap = 1002
        }

        #endregion

        #region 维修产品

        /// <summary>
        /// 产品状态
        /// </summary>
        public enum ProductState
        {
            /// <summary>
            /// 未上架
            /// </summary>
            [Description("未上架")]
            NoShelf = 0,
            /// <summary>
            /// 上架
            /// </summary>
            [Description("上架")]
            OnShelf = 1,
            /// <summary>
            /// 已下架
            /// </summary>
            [Description("已下架")]
            OffShelf = 2
        }

        #endregion

        #region 积分商城

        /// <summary>
        /// 商品状态
        /// </summary>
        public enum GoodsState
        {
            /// <summary>
            /// 未上架
            /// </summary>
            [Description("未上架")]
            NoShelf = 0,
            /// <summary>
            /// 上架
            /// </summary>
            [Description("上架")]
            OnShelf = 1,
            /// <summary>
            /// 已下架
            /// </summary>
            [Description("已下架")]
            OffShelf = 2
        }

        #endregion

        #region 订单

        /// <summary>
        /// 订单类型
        /// </summary>
        public enum OrderType
        {
            /// <summary>
            /// 质保订单
            /// </summary>
            [Description("维修服务卡")]
            Warranty = 100
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public enum OrderPayType
        {
            /// <summary>
            /// 微信支付
            /// </summary>
            [Description("微信支付")]
            WeChat = 1
        }

        /// <summary>
        /// 订单来源
        /// </summary>
        public enum OrderSource
        {
            /// <summary>
            /// 微信
            /// </summary>
            [Description("微信")]
            WeChat = 1,
            /// <summary>
            /// PC
            /// </summary>
            [Description("Pc")]
            Pc = 2
        }

        /// <summary>
        /// 订单状态 0待付款; 1已付款,待审核; 2已确认,待发货; 3待收货; 4已完成; 5审核不通过 6 已退款 7已取消 8支付失败
        /// </summary>
        public enum OrderState
        {
            /// <summary>
            /// 等待买家付款
            /// </summary>
            [Description("待付款")]
            NotPay=0,
            /// <summary>
            /// 买家已付款,待确认
            /// </summary>
            [Description("待审核")]
            Paid =1,
            /// <summary>
            /// 平台已确认，待发货
            /// </summary>
            [Description("待发货")]
            Confirmed = 2,
            /// <summary>
            /// 已发货，待收
            /// </summary>
            [Description("待收货")]
            Send =3,
            /// <summary>
            /// 交易完成
            /// </summary>
            [Description("交易成功")]
            Success =4,
            /// <summary>
            /// 审核不通过
            /// </summary>
            [Description("审核不通过")]
            NotPass = 5,
            /// <summary>
            /// 审核不通过，已退款
            /// </summary>
            [Description("已退款")]
            Refunded = 6,
            /// <summary>
            /// 交易取消,买家可直接取消未付款订单
            /// </summary>
            [Description("已取消")]
            Cancel =7,
            /// <summary>
            /// 支付失败
            /// </summary>
            [Description("支付失败")]
            PayFail = 8,
            /// <summary>
            /// 退款中
            /// </summary>
            [Description("退款中")]
            Refunding = 9,
            /// <summary>
            /// 退款异常
            /// </summary>
            [Description("退款异常")]
            RefundChange = 10,
            /// <summary>
            /// 退款关闭
            /// </summary>
            [Description("退款关闭")]
            RefundClose = 11
        }
        /// <summary>
        /// 订单售后状态(0等待审核,1审核通过,2.客户手机邮寄.3维修中.4维修完成,5邮寄发还,6服务完成.7用户已取消,8平台已关闭)
        /// </summary>
        public enum OrderServiceState
        {
            /// <summary>
            /// 等待审核
            /// </summary>
            [Description("等待审核")]
            NotConfirmed = 0,
            /// <summary>
            /// 审核通过
            /// </summary>
            [Description("审核通过")]
            Confirmed = 1,
            /// <summary>
            /// 客户手机邮寄中
            /// </summary>
            [Description("客户邮寄")]
            CustomPost = 2,
            /// <summary>
            /// 维修中
            /// </summary>
            [Description("维修中")]
            InService = 3,
            /// <summary>
            /// 维修完成
            /// </summary>
            [Description("维修完成")]
            FinishService = 4,
            /// <summary>
            /// 邮寄发还
            /// </summary>
            [Description("邮寄发还")]
            BackPost = 5,
            /// <summary>
            /// 服务完成
            /// </summary>
            [Description("服务完成")]
            Success = 6,
            /// <summary>
            /// 交易取消,买家可直接取消未付款订单
            /// </summary>
            [Description("用户已取消")]
            Cancel = 7,
            /// <summary>
            /// 平台已关闭
            /// </summary>
            [Description("平台已关闭")]
            Close = 8,
            [Description("审核不通过")]
            NotPass=9,
            /// <summary>
            /// 无售后
            /// </summary>
            [Description("无售后")]
            NoService=10
        }
        /// <summary>
        /// 订单操作状态 1审核通过.2审核不通过.3退款.4发货
        /// </summary>
        public enum OrderActState {
            /// <summary>
            /// 审核通过
            /// </summary>
            Confirmed=1,
            /// <summary>
            /// 审核不通过
            /// </summary>
            NotPass=2,
            /// <summary>
            /// 退款
            /// </summary>
            Refunden=3,
            /// <summary>
            /// 发货
            /// </summary>
            Send=4,
        }
        #endregion

        #region 售后|在线报修
        public enum ServiceTypeState
        {
            [Description("订单售后")]
            Purchase = 1,
            [Description("在线报修")]
            Purchased = 2,
        }

        /// <summary>
        /// 维修类型
        /// </summary>
        public enum ServiceType
        {
            /// <summary>
            /// 上门维修
            /// </summary>
            [Description("上门维修")]
            TheDoor = 1,
            /// <summary>
            /// 邮寄
            /// </summary>
            [Description("邮寄快修")]
            Mail = 2,
            /// <summary>
            /// 到店维修
            /// </summary>
            [Description("到店维修")]
            ArriveStore = 3
        }

        /// <summary>
        /// 维修状态(0等待审核,1审核通过,2.客户手机邮寄.3维修中.4维修完成,5邮寄发还,6服务完成.7用户已取消,8平台已关闭)
        /// </summary>
        public enum ServiceState
        {
            /// <summary>
            /// 等待审核
            /// </summary>
            [Description("等待审核")]
            NotConfirmed=0,
            /// <summary>
            /// 审核通过
            /// </summary>
            [Description("审核通过")]
            Confirmed =1,
            /// <summary>
            /// 客户手机邮寄中
            /// </summary>
            [Description("客户手机邮寄中")]
            CustomPost =2,
            /// <summary>
            /// 维修中
            /// </summary>
            [Description("维修中")]
            InService =3,
            /// <summary>
            /// 维修完成
            /// </summary>
            [Description("维修完成")]
            FinishService =4,
            /// <summary>
            /// 邮寄发还
            /// </summary>
            [Description("邮寄发还")]
            BackPost =5,
            /// <summary>
            /// 服务完成
            /// </summary>
            [Description("服务完成")]
            Success =6,
            /// <summary>
            /// 交易取消,买家可直接取消未付款订单
            /// </summary>
            [Description("用户已取消")]
            Cancel = 7,
            /// <summary>
            /// 平台已关闭
            /// </summary>
            [Description("平台已关闭")]
            Close = 8,
            [Description("审核不通过")]
            NotPass = 9,
        }
        #endregion

        #region 积分兑换订单
        /// <summary>
        /// 订单状态 0 待付款;1待发货;2待收货;3待评价;4已完成;5交易取消
        /// </summary>
        public enum IntegraOrderState {
            /// <summary>
            /// 等待买家付款
            /// </summary>
            [Description("待付款")]
            NotPay = 0,
            /// <summary>
            /// 买家已付款,待发货
            /// </summary>
            [Description("待发货")]
            Paid = 1,
            /// <summary>
            /// 平台已发货，待收货
            /// </summary>
            [Description("待收货")]
            Send = 2,
            /// <summary>
            /// 已收货，待评价（备用）
            /// </summary>
            [Description("待评价")]
            NotEvaluate = 3,
            /// <summary>
            /// 交易完成
            /// </summary>
            [Description("已完成")]
            Success = 4,
            /// <summary>
            /// 交易取消
            /// </summary>
            [Description("交易取消")]
            Cancel = 5,
            /// <summary>
            /// 支付失败
            /// </summary>
            [Description("支付失败")]
            PayFail = 8

        }

        #endregion
    }

    #region 枚举Helper

    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举变量值的 Description 属性
        /// </summary>
        /// <param name="obj">枚举变量</param>
        /// <param name="isTop">是否改变为返回该类、枚举类型的头 Description 属性，而不是当前的属性或枚举变量值的 Description 属性</param>
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>
        public static string GetDescription(this object obj, bool isTop = false)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                Type enumType = obj.GetType();
                DescriptionAttribute dna;

                if (isTop)
                {
                    dna = (DescriptionAttribute)System.Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute));
                }
                else
                {
                    FieldInfo fi = enumType.GetField(Enum.GetName(enumType, obj));
                    dna = (DescriptionAttribute)System.Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                }

                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                {
                    return dna.Description;
                }
            }
            catch
            {
            }

            return obj.ToString();
        }

        /// <summary>
        /// 将枚举类转换成IList
        /// </summary>
        /// <param name="type">枚举的类型，参数格式是“typeof (EnumTool.EmailAgrmt)”</param>
        /// <returns>ArrayList</returns>
        /// 添加人：彭国帅
        public static List<Dictionary<string, string>> GetEnumList(Type type)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            foreach (int i in Enum.GetValues(type))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    {"Text", Enum.GetName(type, i)},
                    {"Value", i.ToString()}
                };

                list.Add(dic);
            }

            return list;
        }

        /// <summary>
        /// 获取枚举项的tText
        /// </summary>
        /// <param name="enumType">typeof(Enum)</param>
        /// <param name="enumValue">枚举的Value值</param>
        /// <returns></returns>
        /// 添加人：彭国帅 2015-01-29
        public static string GetEnumText(Type enumType, int enumValue)
        {
            return Enum.GetName(enumType, enumValue);
        }

        /// <summary>
        /// 枚举转化为List
        /// </summary>
        /// <param name="enumType">enum名称</param>
        /// <returns></returns>
        public static List<DictionaryEntry> ConvertEnumToList(Type enumType)
        {
            List<DictionaryEntry> list = new List<DictionaryEntry>();

            if (enumType.IsEnum)
            {
                foreach (int myCode in Enum.GetValues(enumType))
                {
                    //string strName = Enum.GetName(enumType, myCode);//获取名称
                    string strVaule = myCode.ToString();                                //获取值
                    string strDesc = GetDescription(Enum.ToObject(enumType, myCode));   // 获取描述

                    list.Add(new DictionaryEntry(strVaule, strDesc));
                }
            }

            return list;
        }

        /// <summary>
        /// 将枚举类转换成IList
        /// </summary>
        /// <param name="type">枚举的类型，参数格式是“typeof(枚举类)”</param>
        /// <returns>ArrayList</returns>
        public static ArrayList ConvertEnumToArray(Type type)
        {
            ArrayList list = new ArrayList();

            foreach (int i in Enum.GetValues(type))
            {
                ListItem listitem = new ListItem(GetDescription(Enum.ToObject(type, i)), i.ToString());
                list.Add(listitem);
            }

            return list;
        }
    }

    #endregion
}
