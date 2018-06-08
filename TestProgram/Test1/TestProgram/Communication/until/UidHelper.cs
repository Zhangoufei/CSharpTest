using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialUtil.until
{
    public class UidHelper
    {
        public static string ToString(byte[] uid)
        {
            //去连字符
            var userInputId = BitConverter.ToString(uid).ToUpperInvariant().Replace("-", "");
            //取每字节的未位字符
            userInputId = new string(userInputId.Where(((c, i) => (i % 2) == 1)).ToArray());
            //替换X
            return userInputId.Replace("C", "X");
            
        }

        public static byte[] ToBytes(String uid)
        {
            return uid.ToUpperInvariant().Replace("X", "C").Select(c => Convert.ToByte(c.ToString(), 16)).ToArray();
        }
    }
}
