using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test001.Common
{
    public class XmlProcess
    {

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obiBaseInfo"></param>
        public static void SerializeScoreFieldsSetting(Score score)
        {
            using (FileStream fs = new FileStream(@AppDomain.CurrentDomain.BaseDirectory + "\\Score.xml", FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Score));
                formatter.Serialize(fs, score);
            }
        }
    }
}
