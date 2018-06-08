using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFactoryDAL
{
    public class ParameterConfig
    {
        private string server;  //要连接的数据库实例的名称或网络地址(可以在名称后指定端口号),指定本地实例可用(Local),如果是SqlExpress(名称\SqlExpress)。 

        private string dataBase;  //数据库的名称。

        private string userID;  //登录帐户。

        private string password;  //帐户登录的密码

        public string Server
        {
            get
            {
                return server;
            }

            set
            {
                server = value;
            }
        }

        public string DataBase
        {
            get
            {
                return dataBase;
            }

            set
            {
                dataBase = value;
            }
        }

        public string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
    }
}
