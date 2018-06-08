using Bonsit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bonsit.RDBSStrategy.Sqlserver
{
    public partial class RDBSStrategy : Srategy
    {
        public int Dev(int a, int b)
        {
            return a - b;
        }

        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
