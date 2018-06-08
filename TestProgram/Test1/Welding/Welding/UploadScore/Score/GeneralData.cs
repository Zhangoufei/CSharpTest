using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UploadScore
{
    public class GeneralData
    {
        private const int DefalutIntNullValue = 0;
        private const string DefalutStringNullValue = "";
        public const int UnReferenceInt = NullInt;
        public const string UnReferenceString = DefalutStringNullValue;


        public const int NullInt = DefalutIntNullValue;
        public const int NullFloat = DefalutIntNullValue;
        public const int NullDouble = DefalutIntNullValue;
        public const int NullDecimal = DefalutIntNullValue;
        public const int NullSqlMoney = DefalutIntNullValue;
        public const string NullString = DefalutStringNullValue;

        static public DateTime NullDateTime
        {
            get { return new DateTime(1900, 1, 1); }
            set { ;}
        }

        static public Guid NullGuid
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000000");
            }
            set { ;}
        }
        static public Guid NewGuid
        {
            get
            {
                return Guid.NewGuid();
            }
            set { ;}
        }
    }
}
