using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Example485
{
    /// <summary>
    /// udp通讯方式
    /// </summary>
    public class UdpCommunication : CommunicationBase
    {
        UdpClient client;//通讯对象

        IPEndPoint remoteEp;//远端终结点

        Boolean isConnected = false;//是否连接

        string ncIP;//网卡IP
        /// <summary>
        /// 网卡IP
        /// </summary>
        public string NCIp
        {
            get { return ncIP; }
            private set { ncIP = value; }
        }
        /// <summary>
        /// 是否已连接
        /// </summary>
        public Boolean IsConnected
        {
            get { return isConnected; }
            private set
            {
                isConnected = value;
            }
        }
        /// <summary>
        /// 本地终结点
        /// </summary>
        public EndPoint LocalEndPoint
        {
            get
            {
                if (client != null)
                {
                    try
                    {
                    	return client.Client.LocalEndPoint;
                    }
                    catch (SocketException)
                    {
                        return null;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 判断网段是否一致
        /// </summary>
        /// <returns></returns>
        //public Boolean IsInTheSameNetwork(out IPAddress address1, out IPAddress address2)
        //{
        //    Boolean result = true;

        //    address1 = null;
        //    address2 = null;

        //    var ipEndPoint = LocalEndPoint as IPEndPoint;
        //    if (ipEndPoint != null) address1 = ipEndPoint.Address;

        //    if (RemoteEP != null)
        //    {
        //        address2 = RemoteEP.Address;
        //    }

        //    if (address1 != null && address2 != null)
        //    {
        //        if (!BITools.IsInTheSameNetwork(address1, address2))
        //        {
        //            result = false;
        //        }
        //    }

        //    return result;
        //}

        /// <summary>
        /// 目标端口+目标IP
        /// </summary>
        public IPEndPoint RemoteEP
        {
            get { return remoteEp; }
            set { remoteEp = value; }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        public override void Send(byte[] data)
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("本地终结点:{0}");
                sb.AppendLine("远程终结点:{1}");
                sb.AppendLine("发送:{2}");

                //if (Log != null)
                //{
                //    Log.WriteLine(sb.ToString(), LocalEndPoint, RemoteEP, BITools.GetDataString(data, " "));
                //}
                
                client.Send(data, data.Length, RemoteEP);
            }
            catch
            {
                string error;
                Close(out error);
            }
        }

        /// <summary>
        /// UDP默认使用任何可用端口
        /// </summary>
        /// <param name="networkInterfacesIp"></param>
        /// <param name="error"></param>
        /// <param name="localPort"></param>
        /// <returns></returns>
        public bool Bind(string networkInterfacesIp, out string error, Int32 localPort = 0)
        {
            Boolean result = false;
            
            try
            {   
                error = null;

                NCIp = networkInterfacesIp;

                var ip = IPAddress.Any;

                if (!string.IsNullOrEmpty(networkInterfacesIp))
                {
                    ip = IPAddress.Parse(networkInterfacesIp);
                }

                client = new UdpClient(new IPEndPoint(ip, localPort));
                
                UdpState state = new UdpState(client, RemoteEP);

                client.BeginReceive(EndReceive, state);

                result = true;

                IsConnected = true;
            }
            catch (SocketException ex)
            {
                error = string.Format("初始化网络时发生了如下错误：\r\n\r\n\t{0}", ex.Message);
            }
            
            return result;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool Close(out string error)
        {
            Boolean result = false;
            
            try
            {
                error = null;

                if (client != null)
                {
                    client.Close();

                    (client as IDisposable).Dispose();
                }
                
                client = null;
                
                result = true;
            }
            catch (SocketException ex)
            {
                error = string.Format("关闭网络连接时发生了如下错误：\r\n\r\n\t{0}", ex.Message);
            }

            IsConnected = false;

            return result;
        }
        /// <summary>
        /// 接收数据回调处理程序
        /// </summary>
        /// <param name="ar"></param>
        void EndReceive(IAsyncResult ar)
        {
            UdpState s = ar.AsyncState as UdpState;

            if (s != null)
            {
                UdpClient udpClient = s.UdpClient;

                IPEndPoint ip = s.IP;

                Byte[] receiveBytes;

                string error;

                try
                {
                    receiveBytes = udpClient.EndReceive(ar, ref ip);
                }
                catch (ObjectDisposedException)
                {
                    //如果是手动Close就直接返回
                    return;
                }
                catch (System.Exception ex)
                {
                    //如果是网络环境异常，就释放掉当前连接
                    System.Diagnostics.Trace.WriteLine(ex.Message);

                    //Logger.Error(String.Format("UDP EndReceive报错：{0}", ex.Message));
                    
                    //CloseInternal(udpClient);
                    Close(out error);

                    return;
                }

                PrintDataSourceEndPoint(ip);

                OnDataReceived(receiveBytes);
                    
                try
                {
                	udpClient.BeginReceive(EndReceive, s);//在这里重新开始一个异步接收，用于处理下一个网络请求
                }
                catch (ObjectDisposedException)
                {
                    Close(out error);
                }
            }
        }
        /// <summary>
        /// 打印终结点信息
        /// </summary>
        /// <param name="ep"></param>
        void PrintDataSourceEndPoint(IPEndPoint ep)
        {
            //if (Log != null) Log.WriteLine("从{0}", ep);
        }
    }
    /// <summary>
    /// udp状态信息
    /// </summary>
    class UdpState
    {
        private UdpClient udpclient = null;
        /// <summary>
        /// 连接对象
        /// </summary>
        public UdpClient UdpClient
        {
            get { return udpclient; }
        }
        /// <summary>
        /// 终结点
        /// </summary>
        private IPEndPoint ip;

        public IPEndPoint IP
        {
            get { return ip; }
            set { ip = value; }
        }

        public UdpState(UdpClient udpclient, IPEndPoint ip)
        {
            this.udpclient = udpclient;
            this.ip = ip;
        }
    }
}
