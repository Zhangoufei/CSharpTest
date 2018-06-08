using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace JAAJExamManagementSys
{
    public class Common
    {
        #region
        /// <summary>
        /// 获取序列化实例
        /// </summary>
        public static BaseInfo CurrentScoreFieldsSetting
        {
            get
            {
                BaseInfo obiBaseInfo = new BaseInfo();
                try
                {
                    obiBaseInfo = DeSerializeScoreFieldsSetting();
                }
                catch
                { obiBaseInfo = new BaseInfo(); }

                return obiBaseInfo;
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obiBaseInfo"></param>
        private static void SerializeScoreFieldsSetting(BaseInfo obiBaseInfo)
        {
            using (FileStream fs = new FileStream(@AppDomain.CurrentDomain.BaseDirectory + "\\Score.xml", FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(BaseInfo));
                formatter.Serialize(fs, obiBaseInfo);
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <returns></returns>
        public static BaseInfo DeSerializeScoreFieldsSetting()
        {
            BaseInfo obiBaseInfo;
            using (FileStream fs = new FileStream(@AppDomain.CurrentDomain.BaseDirectory + "\\Score.xml", FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(BaseInfo));
                obiBaseInfo = (BaseInfo)formatter.Deserialize(fs);
            }
            return obiBaseInfo;
        }
        /// <summary>
        /// 保存或更新
        /// </summary>
        /// <param name="obiBaseInfo"></param>
        /// <returns></returns>
        public static bool SaveBillFieldsSetting(BaseInfo obiBaseInfo)
        {
            try
            {
                SerializeScoreFieldsSetting(obiBaseInfo);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
