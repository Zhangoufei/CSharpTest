using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public class JobInfo
    {
        private int _jobid;
        private string _jobtitle;
        private DateTime _pubdate;
        private DateTime _enddate;
        private int _number;
        private int _state;
        private string _body;
        private string _city;

        public int JobID
        {
            set { _jobid = value; }
            get { return _jobid; }
        }

        public string JobTitle
        {
            set { _jobtitle = value; }
            get { return _jobtitle; }
        }

        public DateTime PubDate
        {
            set { _pubdate = value; }
            get { return _pubdate; }
        }

        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }

        public int Number
        {
            set { _number = value; }
            get { return _number; }
        }

        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }

        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
    }
}
