using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProgram.Model
{
    /// <summary>
    /// LinqTest2.xaml 的交互逻辑
    /// </summary>
    public partial class LinqTest2 : UserControl
    {
        public LinqTest2()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // 1，获取数据源
            List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 2，创建查询
            var numQuery = from num in numbers
                           where num % 2 == 0
                           select num;

            // 3,执行查询
            foreach (var num in numQuery)
            {
                MessageBox.Show(num.ToString());
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            List<Test1> list = new List<Test1>();
            list.Add(new Test1 { Name = "张三", Age = 20, Id = 1 });
            list.Add(new Test1 { Name="李四",Age=23,Id=5});
            list.Add(new Test1 { Name = "王五", Age = 23, Id = 2 });
            list.Add(new Test1 { Name = "王五1", Age = 23, Id = 3 });
            list.Add(new Test1 { Name = "王五2", Age = 23, Id = 4 });
            list.Add(new Test1 { Name = "王五3", Age = 23, Id = 6 });
            list.Add(new Test1 { Name = "王五4", Age = 23, Id = 7 });
            list.Add(new Test1 { Name = "王五5", Age = 23, Id = 8 });


            //Test1
            //var QueryTest1 = from Test1 t in list where t.Id > 5 select t;

            //foreach (var item in QueryTest1)
            //{
            //    MessageBox.Show(item.Name+"  item.age:"+item.Age+"  item.id:"+item.Id);
            //}

            //Test2
            //List<string> listStr = new List<string>();
            //for (int i = 0; i < 10; i++)
            //{
            //    listStr.Add("王五"+i);
            //}
           
            //var select= listStr.Select(GetSelect);


        }
        private string ID = "王五0";
        private   string GetSelect(string id)
        {
            id = ID;
            return id;
        }


        class Test1 {

            private int id;
            private string name;
            private int age;

            public int Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value;
                }
            }

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
}
