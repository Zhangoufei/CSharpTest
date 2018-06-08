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
    /// 不知道用户传人什么类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SomethingFactory<T>
    {
        public static T InitInstance(T inObj)
        {
            if (true)
            {
                return inObj;
            }
            return default(T);
        }
    }

    public static class SomeThingFactory2<T> where T : myTestT
    {
        public static T InitInstance(T inobj)
        {
            return inobj;
        }
       
    }

    
    public class myTestT
    {
        public string ID { set; get; }

       
    }

    public static class printStringT {
        public static void PrintString(this String val)
        {
            MessageBox.Show(val);
        }
    }
    /// <summary>
    /// TestView.xaml 的交互逻辑
    /// </summary>
    public partial class TestView2 : UserControl
    {
        public TestView2()
        {
            InitializeComponent();
        }
        
        delegate Boolean moreOrlessDelegate(int item);
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //基本的委托
            var arr = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            //var d1 = new moreOrlessDelegate(More);
            //Print(arr, d1);


            //var d2 = new moreOrlessDelegate(Less);
            //Print(arr, d2);

            //使用泛型
            //MessageBox.Show(SomethingFactory<string>.InitInstance("你好"));

            //限制泛型的类型
            //myTestT mm = new myTestT();
            //mm.ID = "232432";
            //MessageBox.Show(SomeThingFactory2<myTestT>.InitInstance(mm).ID);

            //使用Predicate委托  一个object对象 返回一个bool类型
            //var d3 = new Predicate<int>(More);
            //Print2(arr,d3);

            //使用action 委托
            //var d4 = new Action<int,string,int>(PrintAction);
            //d4.BeginInvoke(23,"3232",12,null,null);
            //使用func委托 多个参数 返回一个 对象
            //var d5 = new Func<int, string, int>(PrintAction2);
            //d5.BeginInvoke(434,"32432",null,null);


            //使用回调函数
            //Func<int, int, string> d6 = new Func<int, int, string>(PrintFunc);
            //d6.BeginInvoke(100,200,DoneCallBack, d6);

            //使用匿名方法
            //var d7 = new Predicate<int>(delegate (int item)
            //{
            //    if (item > 3)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //});
            //Print2(arr, d7);


            //使用Lambda表达式
            //   arr.ForEach(new Action<int>(delegate (int a) { MessageBox.Show(a.ToString()); }));

            //  arr.ForEach(a => MessageBox.Show(a.ToString()));

            //arr.ForEach(a =>
            //{
            //    if (a > 3) { MessageBox.Show(a.ToString()); }
            //});

            //扩展方法  添加一个静态类，写一个静态扩展方法
            //var a = "aaaa";
            //a.PrintString();

            ////使用迭代器
            //foreach (var item in GetIterator())
            //{
            //    if (item == 2)
            //    {
            //        break;
            //    }
            //}

            //使用linq 中的where 和 sum
            var result = arr.Where(a => { return a > 3; }).Sum();
            MessageBox.Show(result.ToString ());

            var mm = (from v in arr where v > 3 select v).Sum();
            MessageBox.Show(mm.ToString());
        }

        /// <summary>
        /// 定义一个迭代器
        /// </summary>
        /// <returns></returns>
        private  static IEnumerable<int> GetIterator() {

            MessageBox.Show("迭代器返回1");
            yield return 1;
            MessageBox.Show("迭代器返回2");
            yield return 2;
            MessageBox.Show("迭代器返回3");
            yield return 3;
        }
        /// <summary>
        /// 定义一个迭代器
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<int> GetIterator2()
        {

            MessageBox.Show("迭代器返回1");
            yield return 1;
            MessageBox.Show("迭代器返回2");
            yield break;
            MessageBox.Show("迭代器返回3");
            yield return 3;
        }

        void DoneCallBack(IAsyncResult asyncResult)
        {
            Func<int, int, string> method = (Func<int, int, string>)asyncResult.AsyncState;

            try
            {
                string temp = method.EndInvoke(asyncResult);
                MessageBox.Show(temp);
            }
            catch (Exception ee)
            {

            }

        }


        string PrintFunc(int a1, int a2)
        {
            for (int i = 0; i < 1000; i++)
            {
                a1 += i;
            }
            string temp = (a1 + a2) + "你好啊";
            return temp;
        }
        //使用func 
        int PrintAction2(int a1, string a2)
        {
            return a1 + 100;
            // MessageBox.Show((a1 + a3) + a2);
        }


        //使用action 多个参数 传入没有返回值
        void PrintAction(int a1, string a2, int a3)
        {
            MessageBox.Show((a1 + a3) + a2);
        }


        void Print2(List<int> arr, Predicate<int> d1)
        {
            foreach (var item in arr)
            {
                if (d1(item))
                {
                    MessageBox.Show(item.ToString());
                }
            }
        }

        void Print(List<int> arr, moreOrlessDelegate dl)
        {
            foreach (var item in arr)
            {
                if (dl(item))
                {
                    MessageBox.Show(item.ToString());
                }
            }
        }

        private bool More(int item)
        {
            if (item > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Less(int item)
        {
            if (item < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
