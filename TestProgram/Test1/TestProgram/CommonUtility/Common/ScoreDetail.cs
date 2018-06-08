using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyCommon.Common
{
    public class ScoreDetail
    {

        private string _nvcTitleName;

        private string _nvcDescription;

        public string NvcTitleName
        {
            get
            {
                return _nvcTitleName;
            }

            set
            {
                _nvcTitleName = value;
            }
        }

        public string NvcDescription
        {
            get
            {
                return _nvcDescription;
            }

            set
            {
                _nvcDescription = value;
            }
        }
    }
}
