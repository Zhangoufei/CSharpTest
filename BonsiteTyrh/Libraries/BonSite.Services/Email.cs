using System;
using System.Text;

using BonSite.Core;

namespace BonSite.Services
{
    public class Emails
    {
        private static object _locker = new object();
        private static IEmailStrategy _iemailstrategy = null;
        private static EmailConfigInfo _emailconfiginfo = null;

        static Emails() {
            _iemailstrategy = BSEmail.Instance;
            _iemailstrategy.Host = BSConfig.EmailConfig.Host;
            _iemailstrategy.Port = BSConfig.EmailConfig.Port;
            _iemailstrategy.UserName = BSConfig.EmailConfig.UserName;
            _iemailstrategy.Password = BSConfig.EmailConfig.Password;
            _iemailstrategy.From = BSConfig.EmailConfig.From;
            _iemailstrategy.FromName = BSConfig.EmailConfig.FromName;
        }


        public static bool SendFeedbackMsg(FeedBackInfo info)
        {
            if (BSConfig.EmailConfig.SendFeedBackMail.Equals(1))
            {
                string subject = BSConfig.EmailConfig.FeedBackTitle.Replace("{linkman}", info.LinkMan);

                string body = BSConfig.EmailConfig.FeedBackBody.Replace("{linkman}", info.LinkMan)
                    .Replace("{addtime}", info.AddTime.ToString())
                    .Replace("{tel}", info.Tel)
                    .Replace("{email}", info.Email)
                    .Replace("{tag}", info.Tag)
                    .Replace("{body}", info.Body);


                return _iemailstrategy.Send(BSConfig.EmailConfig.FeedBackMail, subject, body);
                
            }
            else
            {
                return true;
            }
        }




    }
}
