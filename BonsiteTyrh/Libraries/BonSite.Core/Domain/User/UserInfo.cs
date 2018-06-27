using System;

namespace BonSite.Core
{
    /// <summary>
    /// 部分用户信息类
    /// </summary>
    public class PartUserInfo
    {
        private int _userid;//用户id
        private string _username = "";//用户名称
        private string _email = "";//用户邮箱
        private string _mobile = "";//用户手机
        private string _password = "";//用户密码
        private string _nickname = "";//用户昵称
        private int _userrankid;//用户等级id
        private int _admingroupid;//角色id
        private string _rolecode;//角色id
        private string _avatar;//用户头像
        private int _rankcredits;//等级积分
        private int _verifyemail;//是否验证邮箱
        private int _verifymobile;//是否验证手机
        private int _state;//状态
        private string _salt="";//盐值
        /// <summary>
        ///用户id
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        ///用户名称
        /// </summary>
        public string UserName
        {
            set { _username = value.TrimEnd(); }
            get { return _username; }
        }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            set { _password = value.TrimEnd(); }
            get { return _password; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        ///<summary>
        ///用户等级id
        ///</summary>
        public int UserRankID
        {
            get { return _userrankid; }
            set { _userrankid = value; }
        }
        ///<summary>
        ///用户管理员组id
        ///</summary>
        public int AdminGroupID
        {
            get { return _admingroupid; }
            set { _admingroupid = value; }
        }
             ///<summary>
        ///角色编码
        ///</summary>
        public string RoleCode
        {
            get { return _rolecode; }
            set { _rolecode = value; }
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        
        /// <summary>
        /// 等级积分
        /// </summary>
        public int RankCredits
        {
            get { return _rankcredits; }
            set { _rankcredits = value; }
        }
        /// <summary>
        /// 是否验证邮箱
        /// </summary>
        public int VerifyEmail
        {
            get { return _verifyemail; }
            set { _verifyemail = value; }
        }
        /// <summary>
        /// 是否验证手机
        /// </summary>
        public int VerifyMobile
        {
            get { return _verifymobile; }
            set { _verifymobile = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        
        ///<summary>
        ///盐值
        ///</summary>
        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }
    }

    /// <summary>
    /// 完整用户信息
    /// </summary>
    public class UserInfo : PartUserInfo
    {
        private string _realname = "";//用户真实名称
        private DateTime _birthday = DateTime.Parse("1900-1-1");//出生日期
        private int _gender = 0;//用户性别(0代表未知，1代表男，2代表女)
        private string _idcard = "";//身份证号
        private int _regionid = 0;//区域id
        private string _address = "";//地址
        private DateTime _regtime = DateTime.Now;//用户注册时间
        private string _regip = "";//注册ip
        private DateTime _lastime = DateTime.Now;//最后访问时间
        private string _lastip = "";//最后访问ip
        private string _body = "";//简介

        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        ///<summary>
        ///用户出生日期
        ///</summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        ///<summary>
        ///用户性别(0代表未知，1代表男，2代表女)
        ///</summary>
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }///<summary>
        ///区域id
        ///</summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        ///<summary>
        ///地址
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        /// <summary>
        /// 用户注册ip
        /// </summary>
        public string RegIP
        {
            set { _regip = value; }
            get { return _regip; }
        }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastTime
        {
            set { _lastime = value; }
            get { return _lastime; }
        }
        /// <summary>
        /// 最后访问ip
        /// </summary>
        public string LastIP
        {
            set { _lastip = value; }
            get { return _lastip; }
        }
        
        ///<summary>
        ///简介
        ///</summary>
        public string Body
        {
            get { return _body.TrimEnd(); }
            set { _body = value; }
        }
    }

    /// <summary>
    /// 用户细节信息类
    /// </summary>
    public class UserDetailInfo
    {
        private int _userid;//用户id
        private string _realname = "";//用户真实名称
        private DateTime _birthday = DateTime.Parse("1900-1-1");//出生日期
        private int _gender = 0;//用户性别(0代表未知，1代表男，2代表女)
        private string _idcard = "";//身份证号
        private int _regionid = 0;//区域id
        private string _address = "";//地址
        private DateTime _regtime = DateTime.Now;//用户注册时间
        private string _regip = "";//注册ip
        private DateTime _lastime = DateTime.Now;//最后访问时间
        private string _lastip = "";//最后访问ip
        private string _body = "";//简介

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        ///<summary>
        ///用户出生日期
        ///</summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        ///<summary>
        ///用户性别(0代表未知，1代表男，2代表女)
        ///</summary>
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }///<summary>
        ///区域id
        ///</summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        ///<summary>
        ///地址
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        /// <summary>
        /// 用户注册ip
        /// </summary>
        public string RegIP
        {
            set { _regip = value; }
            get { return _regip; }
        }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastTime
        {
            set { _lastime = value; }
            get { return _lastime; }
        }
        /// <summary>
        /// 最后访问ip
        /// </summary>
        public string LastIP
        {
            set { _lastip = value; }
            get { return _lastip; }
        }

        ///<summary>
        ///简介
        ///</summary>
        public string Body
        {
            get { return _body.TrimEnd(); }
            set { _body = value; }
        }
    }
}
