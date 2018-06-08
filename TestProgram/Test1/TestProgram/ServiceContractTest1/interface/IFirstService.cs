using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ServiceContractTest1
{


    /// <summary>
    /// 声明为一个服务   定义一个回调方法
    /// </summary>
    [ServiceContract(CallbackContract =typeof(IFirstCallBack))]
    public interface IFirstService
    {
        /// <summary>
        /// 声明为服务的方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        string GetDataString(string name);

        [OperationContract]
        string HelloWorld(string str);


    }

    public interface IFirstCallBack {

        [OperationContract(IsOneWay = true)]
        void NotifyClientMsg(string devStateInfo);
    }

}

