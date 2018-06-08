using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceContractTest1.implements
{
    /// <summary>
    /// 实现契约
    /// </summary>
    public class Service : Iservice
    {
        public string Greetings(string name)
        {
            return "Hello World :"+ name;
        }
    }
}
