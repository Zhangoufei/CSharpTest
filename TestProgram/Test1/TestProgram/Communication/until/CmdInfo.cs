using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialUtil.until
{
    public class CmdInfo
    {
        public Int32 DeviceId { get; set; }
        public DeviceState State { get; set; }
        public Cmd Cmd { get; set; }
        public byte[] Data { get; set; }

        public ValidateInfo ToValidateInfo()
        {
            var validateInfo = new ValidateInfo { DeviceId = DeviceId };
            
            if (Data != null)
            {
                var userInputId = BitConverter.ToString(Data).ToUpperInvariant().Replace("-", "");
                userInputId = new string(userInputId.Where(((c, i) => (i % 2) == 1)).ToArray());
                userInputId = userInputId.Replace("C", "X");
                validateInfo.UserInputId = userInputId;
            }

            return validateInfo;
        }
      
    }
}
