using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyCommon.Common
{
    /// <summary>
    /// 设备授权信息
    /// </summary>
    public class AccreditInfo
    {

        private string nvcBatchNo;

        private string nvcCardNum;

        public string NvcBatchNo
        {
            get
            {
                return nvcBatchNo;
            }

            set
            {
                nvcBatchNo = value;
            }
        }

        public string NvcCardNum
        {
            get
            {
                return nvcCardNum;
            }

            set
            {
                nvcCardNum = value;
            }
        }
    }
}
