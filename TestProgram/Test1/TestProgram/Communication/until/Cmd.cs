using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialUtil.until
{
    public enum Cmd:byte
    {
        Query=0x01,   //查询信息
        Idle=0x02,      //通用应答
        VerifyId=0x03,  //验证身份请求
        VerifyIdResult=0x04,    //身份验证结果
        CommitExam=0x05,        //考试结果
    }
}
