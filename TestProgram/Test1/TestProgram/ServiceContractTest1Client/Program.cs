using ServiceContractTest1;
using ServiceContractTest1.implements;
using ServiceContractTest1Client.Service1;
using ServiceContractTest1Client.Service2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace ServiceContractTest1Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //  NewMethod();
            //引用另一个WCF
            //如果终结点 多于一个的话需要 指定终结点名称
          //  FirstServiceClient servicer = new Service1.FirstServiceClient("NetTcpBinding_IFirstService");

    //       string temp=  servicer.GetDataString("客户端");
        //    Console.WriteLine(temp+ ":NetTcpBinding_IFirstService");

        //    servicer = new Service1.FirstServiceClient("BasicHttpBinding_IFirstService");

          //  Console.WriteLine(servicer.HelloWorld("张三") + "BasicHttpBinding_IFirstService");

            IserviceClient service = new IserviceClient("BasicHttpBinding_Iservice");
            Console.WriteLine("我是服务端2 "+service.Greetings("客户端2")+ "BasicHttpBinding_Iservice");

            service = new IserviceClient("NetTcpBinding_Iservice");

            Console.WriteLine("我是服务端2 " + service.Greetings("客户端2")+ ":NetTcpBinding_Iservice");



            //获取服务端的事件
            Console.WriteLine("服务端的时间 为");


            Console.ReadKey();

        }

        /// <summary>
        /// 一个 WCF
        /// </summary>
        private static void NewMethod()
        {
            Hello hell = new Hello();
            string meg = hell.Greetings("你好");
            Console.WriteLine(meg);
            Console.Read();
        }
    }

    public class Hello : ClientBase<ServiceContractTest1.Iservice>, ServiceContractTest1.Iservice
    {

        public static Binding HelloWorldBingding = new NetNamedPipeBinding();


        public static EndpointAddress HelloWorldAddress = new EndpointAddress(new Uri("net.pipe://localhost/HelloWorld"));

        public Hello() : base(HelloWorldBingding, HelloWorldAddress)
        {

        }

        public string Greetings(string name)
        {
            return Channel.Greetings(name);

        }
    }
}
