using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ServiceContractTest1.implements
{
    public class FirstService : IFirstService
    {

        public string GetDataString(string name)
        {
            string temp= name + ":你好 WCF编程";
            //注册这个回调方法
            callBack = OperationContext.Current.GetCallbackChannel<IFirstCallBack>();
            return temp;
        }

        public string HelloWorld(string str)
        {
            return "HelloWorld :" + str;
        }


        //重写回调方法
        public static IFirstCallBack callBack;

    }
}
