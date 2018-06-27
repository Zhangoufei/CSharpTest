using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    /// <summary>
    /// 邮件相关配置信息类
    /// </summary>
    [Serializable]
    public class EmailConfigInfo:IConfigInfo
    {
        private string _host = "";
        private int _port = 25;
        private string _username = "";
        private string _password = "";
        private string _from = "";
        private string _fromname = "";


        /// <summary>
        /// 主机
        /// </summary>
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 发送人邮箱
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// 发送人名称
        /// </summary>
        public string FromName
        {
            get { return _fromname; }
            set { _fromname = value; }
        }





        private int _sendfeedbackmail = 0;
        private string _feedbackmail = "";
        private string _feedbacktitle = "";
        private string _feedbackbody = "";

        /// <summary>
        /// 是否发送反馈提醒邮件
        /// </summary>
        public int SendFeedBackMail
        {
            get { return _sendfeedbackmail; }
            set { _sendfeedbackmail = value; }
        }

        /// <summary>
        /// 反馈信息接收人
        /// </summary>
        public string FeedBackMail
        {
            get { return _feedbackmail; }
            set { _feedbackmail = value; }
        }

        /// <summary>
        /// 信息反馈标题
        /// </summary>
        public string FeedBackTitle
        {
            get { return _feedbacktitle; }
            set { _feedbacktitle = value; }
        }
        /// <summary>
        /// 信息反馈正文
        /// </summary>
        public string FeedBackBody
        {
            get { return _feedbackbody; }
            set { _feedbackbody = value; }
        }
    }
}
