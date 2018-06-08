using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 单例模式
{
    public class Singleton1
    {
        private static Singleton1 singleton = new Singleton1();

        private Singleton1() { }

        public static Singleton1 getInstance() {

            return singleton;
        }

    }


    public class Singleton2 {

        private static Singleton2 singleton;

       private  static object obj = new object();
        private Singleton2() { }

        public static Singleton2 getInstance()
        {

            lock (obj)
            {
                if (singleton == null)
                {
                    singleton = new Singleton2();
                }
            }
            return singleton;
        }
    }

}
