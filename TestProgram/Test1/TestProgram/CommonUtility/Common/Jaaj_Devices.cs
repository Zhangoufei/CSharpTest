using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyCommon.Common
{
    public class Jaaj_Devices
    {
        private int iDeviceID;

        private string nvcDeviceName;

        public int IDeviceID
        {
            get
            {
                return iDeviceID;
            }

            set
            {
                iDeviceID = value;
            }
        }

        public string NvcDeviceName
        {
            get
            {
                return nvcDeviceName;
            }

            set
            {
                nvcDeviceName = value;
            }
        }
    }
}
