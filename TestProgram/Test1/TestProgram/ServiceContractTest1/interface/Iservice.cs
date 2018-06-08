using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ServiceContractTest1
{
    
    /// <summary>
    /// 建立契约  公开接口的一部分告诉外部这个服务是干什么的
    /// </summary>
    [ServiceContract]
    public interface Iservice
    {

        [OperationContract]
        string Greetings(string name);

    }
}
