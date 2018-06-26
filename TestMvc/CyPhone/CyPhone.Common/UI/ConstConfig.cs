using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyPhone.Common.UI
{
    public static class ConstConfig
    {
        /// <summary>
        /// 优惠券默认价格
        /// </summary>
        public static int CouponPrice = 20;
        /// <summary>
        /// 优惠券过期时间
        /// </summary>
        public static DateTime CouponOvertime = DateTime.Now.AddDays(30);
        /// <summary>
        /// 默认积分
        /// </summary>
        public static int Integral = 1000;
        /// <summary>
        /// 邀请赠送积分,调整为188
        /// </summary>
        /// create by shuai 2018年1月7日 22:48:59
        public static int InviteIntegral = 188;
        /// <summary>
        /// 维修卡服务延长生效day数（订单审核通过处理）
        /// </summary>
        public static int ServiceEffect = 21;
        /// <summary>
        /// 默认列表每页显示条数
        /// </summary>
        public static int PageSize = 20;
        /// <summary>
        /// 默认登录密码
        /// </summary>
        public static string DefaultPwd = "123456";
    }
}
