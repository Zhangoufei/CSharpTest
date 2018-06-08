using System;

namespace HardWare.CardReader
{
    class UHFArgs
    {
        public Int32 SerialPort { get; set; }
        public byte BaudRateCode { get; set; }
        public byte DeviceId { get; set; }
    }
}
