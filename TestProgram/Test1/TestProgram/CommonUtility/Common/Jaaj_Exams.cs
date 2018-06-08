using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyCommon.Common
{
    public class Jaaj_Exams
    {

        private int iExamId;

        private string nvcBatchNo;

        private string nvcExamName;

        private string datExamDate;

        private string datExamEnd;

        public int IExamId
        {
            get
            {
                return iExamId;
            }

            set
            {
                iExamId = value;
            }
        }

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

        public string NvcExamName
        {
            get
            {
                return nvcExamName;
            }

            set
            {
                nvcExamName = value;
            }
        }

        public string DatExamDate
        {
            get
            {
                return datExamDate;
            }

            set
            {
                datExamDate = value;
            }
        }

        public string DatExamEnd
        {
            get
            {
                return datExamEnd;
            }

            set
            {
                datExamEnd = value;
            }
        }
    }
}
