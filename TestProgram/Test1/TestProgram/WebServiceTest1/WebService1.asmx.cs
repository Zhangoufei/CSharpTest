using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceTest1
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {

            return "Hello World";
        }

        [WebMethod]
        public List<Student> GetStudent()
        {
            List<Student> list = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                Student stu = new Student();
                stu.Name = "张三" + i;
                stu.Age = 20 + i;
                stu.Address = "郑州" + i;
                list.Add(stu);
            }
          
            return list;
        }

    }

    [Serializable]
    public class Student
    {

        private string name;
        private string address;

        private int age;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }
    }

}
