using System;

namespace HardWare.CardReader
{
    public class IDCard
    {
        #region field
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }     

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }      

        /// <summary>
        /// 民族，护照识别时此项为空
        /// </summary>
        public string Nation { get; set; }    

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }   

        /// <summary>
        /// 地址，在识别护照时导出的是国籍简码
        /// </summary>
        public string Address { get; set; }  

        /// <summary>
        /// 身份证件号
        /// </summary>
        public string IDCode { get; set; }  

        /// <summary>
        /// 签发日期，在识别护照时导出的是有效期至 
        /// </summary>
        public string SignDepartment { get; set; }   

        /// <summary>
        /// 有效起始日期，在识别护照时为空
        /// </summary>
        public string ValidtermOfStart { get; set; } 

        /// <summary>
        /// 有效截止日期，在识别护照时为空
        /// </summary>
        public string ValidtermOfEnd { get; set; }  

        /// <summary>
        /// 安全模块号
        /// </summary>
        public string SAMID { get; set; }

        /// <summary>
        /// 初始化状态
        /// </summary>
        private bool InitState = false;
        #endregion

        #region constructor
        public IDCard()
        {
        }
        #endregion

        #region private method

        /// <summary>
        /// 设置字段值
        /// </summary>
        private void SetFieldValue()
        {
            try
            {
                byte[] name = new byte[30];
                int length = 30;
                CVRSDK.GetPeopleName(ref name[0], ref length);

                byte[] idCode = new byte[30];
                length = 36;
                CVRSDK.GetPeopleIDCode(ref idCode[0], ref length);

                byte[] nation = new byte[30];
                length = 3;
                CVRSDK.GetPeopleNation(ref nation[0], ref length);
             
                byte[] validtermOfStart = new byte[30];
                length = 16;
                CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);
             
                byte[] birthday = new byte[30];
                length = 16;
                CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);
               
                byte[] address = new byte[30];
                length = 70;
                CVRSDK.GetPeopleAddress(ref address[0], ref length);
                
                byte[] validtermOfEnd = new byte[30];
                length = 16;
                CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);
                
                byte[] signDep = new byte[30];
                length = 30;
                CVRSDK.GetDepartment(ref signDep[0], ref length);
                
                byte[] sex = new byte[30];
                length = 3;
                CVRSDK.GetPeopleSex(ref sex[0], ref length);

                byte[] samid = new byte[32];
                CVRSDK.CVR_GetSAMID(ref samid[0]);

                Name = ByteArrayToString(name);
                Sex = ByteArrayToString(sex);
                Nation = ByteArrayToString(nation);
                Birthday = ByteArrayToString(birthday);
                Address = ByteArrayToString(address);
                IDCode = ByteArrayToString(idCode);
                SignDepartment = ByteArrayToString(signDep);
                ValidtermOfStart = ByteArrayToString(validtermOfStart);
                ValidtermOfEnd = ByteArrayToString(validtermOfEnd);
                SAMID = ByteArrayToString(samid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Byte数组转换字符串
        /// </summary>
        /// <param name="bytData"></param>
        /// <returns></returns>
        private string ByteArrayToString(byte[] bytData)
        {
            return System.Text.Encoding.GetEncoding("GB2312").GetString(bytData).Replace("\0", "").Trim();
        }
        #endregion

        #region public method
        /// <summary>
        /// 初始化连接
        /// 0:初始化失败;1:初始化成功;
        /// </summary>
        /// <returns></returns>
        public int InitComm()
        {
            try
            {
                int iRetUSB = 0;
                int iRetCOM = 0;
                int iPort;
                for (iPort = 1001; iPort <= 1016; iPort++)
                {
                    iRetUSB = CVRSDK.CVR_InitComm(iPort);
                    if (iRetUSB == 1)
                    {
                        break;
                    }
                }
                if (iRetUSB != 1)
                {
                    for (iPort = 1; iPort <= 4; iPort++)
                    {
                        iRetCOM = CVRSDK.CVR_InitComm(iPort);
                        if (iRetCOM == 1)
                        {
                            break;
                        }
                    }
                }

                if ((iRetCOM == 1) || (iRetUSB == 1))
                {
                    InitState = true;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public int CloseComm()
        {
            try
            {
                return CVRSDK.CVR_CloseComm();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取身份证件信息
        /// 0:未放卡或卡片放置不正确;1:读卡失败;2:读卡成功;3:初始化失败;
        /// </summary>
        /// <returns></returns>
        public int ReadCardContent()
        {
            try
            {
                if (InitState)
                {
                    int authenticate = CVRSDK.CVR_Authenticate();
                    if (authenticate == 1)
                    {
                        int readContent = CVRSDK.CVR_Read_Content(4);
                        if (readContent == 1)
                        {
                            SetFieldValue();
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 3;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
