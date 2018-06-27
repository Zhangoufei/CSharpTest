using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BonSite.Web.Framework;

namespace BonSite.Web.Admin.Models
{
    public class JobModel
    {
        public  int JobID { get; set; }
        public string JobTitle { get; set; }
        public DateTime PubDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Number { get; set; }
        public int State { get; set; }
        public string Body { get; set; }
        public string City { get; set; }
    }

    public class JobListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable DataList { get; set; }
    }
}