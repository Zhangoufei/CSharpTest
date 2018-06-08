using System;

namespace Example485
{
    /// <summary>
    /// 通讯方式枚举
    /// </summary>
    public enum CommunicationType
    {
        Serial,
        Udp,
        UDisk,
        Tcp,
    }
    /// <summary>
    /// 通讯方式基类
    /// </summary>
    public abstract class CommunicationBase
    {
        /// <summary>
        /// 界面日志
        /// </summary>
        //public CommunicationLog Log
        //{
        //    get;
        //    set;
        //}

        //public abstract Boolean Connect(out string error);
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public abstract Boolean Close(out string error);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        public virtual void Send(byte[] data)
        {
        }
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event Action<byte[]> DataReceived;
        /// <summary>
        /// 触发数据接收事件
        /// </summary>
        /// <param name="data"></param>
        protected void OnDataReceived(byte[] data)
        {
            //if (Log != null) Log.WriteLine("收到({0}):{1}\r\n", data == null ? null : data.Length.ToString(),BITools.GetDataString(data, " "));

            Action<byte[]> cache = DataReceived;
            
            if (cache != null)
            {
                cache(data);
            }
        }
    }
}
