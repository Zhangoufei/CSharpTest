using ServiceContractTest1;
using ServiceContractTest1.implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace ServiceContractTest1Host
{
    /// <summary>
    /// 建立宿主
    /// </summary>
    class Program
    {

        //   其中
        static Type typeInterfaceService = typeof(Iservice); //服务契约定义类型
        static Type typeClassService = typeof(Service); //服务契约实现类型
        static string baseAddress = "net.pipe://localhost";//基地址（注意写法）

        static string helloWorldServiceAddress = "HelloWorld";//可选地址
        static Binding HelloWorldBinding = new NetNamedPipeBinding();//服务只定义了一个绑定

        static void Main(string[] args)
        {
            try
            {
                //初始化 servicehost对象 用实现服务的类型和基地址初始化
                //ServiceHost host = new ServiceHost(typeClassService, new Uri[] { new Uri(baseAddress) });

                ////可以添加多个终节点
                ////添加一个终节点 ，包括服务契约类型，服务定义的绑定，服务地址
                //host.AddServiceEndpoint(typeInterfaceService, HelloWorldBinding, helloWorldServiceAddress);

                //host.Open();

                //Console.WriteLine("Service is started! 服务1 启用");
                //Console.WriteLine("Press andy key to stop the service");


                ServiceHost host = new ServiceHost(typeof(Service));
                host.Open();
                Console.WriteLine("服务1 已启动 ");

                //启用另一个服务
                ServiceHost host2 = new ServiceHost(typeof(FirstService));
                host2.Open();

                Console.WriteLine("服务2 已启动 ");


                Console.Read();


            }
            catch (Exception e)
            {

            }

        }
    }
}
