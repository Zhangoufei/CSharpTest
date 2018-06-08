using System;
using System.Collections.Generic;

using System.Text;

namespace JAAJ.Common
{
    #region 枚举的显示名称
    /// <summary>
    /// 枚举的显示名称
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumShowNameAttribute : Attribute
    {
        private string showName;

        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName
        {
            get
            {
                return this.showName;
            }
        }

        /// <summary>
        /// 构造枚举的显示名称
        /// </summary>
        /// <param name="showName">显示名称</param>
        public EnumShowNameAttribute(string showName)
        {
            this.showName = showName;
        }
    }
    #endregion

    public class CommonEnum
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogCategory
        {
            SystemError = 1,//系统错误
            Operate = 2     //操作日志
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public enum OperateCategory
        {
            Add = 1,        //新增
            Edit = 2,       //编辑
            Delete = 3,     //删除
            Search = 4,     //查询
            Preview = 5,    //预览
            Other = 6       //其他
        }
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public enum OperResult
        {
            Success = 1,  //成功
            Failed = 2    //失败

        }
        /// <summary>
        /// 功能模块大类
        /// </summary>
        public enum PermissionCategpry
        {
            BasePermission = 1,    //基础资料
            BusinessPermission = 2,//功能模块
            SystemPermission = 3,  //系统管理
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public enum OrderStatus
        {
            Normal = 0,           //正常
            Audit = 1,            //已审核
            Red = 2,              //红冲
            Delete = 3            //软删除
        }
        /// <summary>
        /// 状态
        /// </summary>
        public enum Subject_Status
        {
            /// <summary>
            /// 正常
            /// </summary>
            [EnumShowName("正常")]
            Normal = 1,
            /// <summary>
            /// 禁用
            /// </summary>
            [EnumShowName("禁用")]
            Disable = 2
        }

        /// <summary>
        /// 设备状态
        /// </summary>
        public enum Device_status
        {
            /// <summary>
            /// 空闲
            /// </summary>
            [EnumShowName("空闲")]
            Normal = 0,
            /// <summary>
            /// 忙碌
            /// </summary>
            [EnumShowName("忙碌")]
            Busy = 1
        }

        /// <summary>
        /// 互斥项
        /// </summary>
        public enum Type_status
        {
            /// <summary>
            /// 不是否决项
            /// </summary>
            [EnumShowName("否")]
            Yes = 0,
            /// <summary>
            /// 是否决项
            /// </summary>
            [EnumShowName("是")]
            No = 1
        }


        /// <summary>
        /// 系统用户角色
        /// </summary>
        public enum User_Role
        {
            /// <summary>
            /// 普通用户
            /// </summary>
            [EnumShowName("普通用户")]
            Normal = 1,
            /// <summary>
            /// 普通管理员
            /// </summary>
            [EnumShowName("普通管理员")]
            Admin = 2,
            /// <summary>
            /// 超级管理员
            /// </summary>
            [EnumShowName("超级管理员")]
            SuperAdmin = 3
        }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public enum PoliticalStatus
        {
            PartyMember = 1,          //中共党员
            ProbationaryMember = 2,   //中共预备党员
            YouthLeagueMember = 3,    //共青团员
            Masses = 4,               //群众
            Other = 5                 //其他
        }

        /// <summary>
        /// 系统中用户可能具有的操作权限
        /// </summary>
        public enum OperationPermission
        {
            //使用
            Use = 1,
            //保存
            Save = 2,
            //查询
            Search = 4,
            //添加
            Add = 8,
            //删除
            Delete = 16,
            //修改
            Modify = 32,
            //导出
            Export = 64,
            //打印
            Print = 128,
            //导入
            Input = 256,
            //其它
            Other = 512
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            [EnumShowName("男")]
            Man = 0,     //男
            [EnumShowName("女")]
            Woman = 1    //女
        }

        /// <summary>
        /// 考试安排状态
        /// </summary>
        public enum Exam_Status
        {
            [EnumShowName("进行中")]
            Proceed = 0,       //进行中
            [EnumShowName("已完结")]
            Complete = 1       //已完结
        }
        /// <summary>
        /// 测量值的单位
        /// </summary>
        public enum MeasureUnit
        {
            [EnumShowName("单位")]
            UNSelect = 0,
            [EnumShowName("V")]
            V=1,
            [EnumShowName("kV")]
            kV=2,
            [EnumShowName("A")]
            A=3,
            [EnumShowName("mA")]
            mA=4,
            [EnumShowName("Ω")]
            Ω=5,
            [EnumShowName("MΩ")]
            MΩ=6
        }
    }
}
