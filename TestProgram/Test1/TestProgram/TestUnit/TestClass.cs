using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestUnit
{

    [TestFixture]
    class TestClass
    {

        [Test]
        public void TestListExcept()
        {
            Console.WriteLine("324");

            List<string> temp2 = new List<string>();
            temp2.Add("temp1");
            temp2.Add("temp2");
            temp2.Add("temp4");

            List<string> list = new List<string>();
            list.Add("temp1");
            list.Add("temp2");
            list.Add("temp3");
            var temp = list.Except(temp2);

            foreach (var VARIABLE in temp)
            {
                Console.WriteLine(VARIABLE);
            }

            var availalbeDevice = temp ?? temp.ToArray();

            foreach (var VARIABLE in availalbeDevice)
            {
                Console.WriteLine(VARIABLE);
            }
        }
        [Test]
        public void TestValue2()
        {
            List<int> list = new List<int>(Enumerable.Range(1, 5));

            foreach (var VARIABLE in list)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        [Test]
        public void TestDelegate()
        {

            DelegateTest(TestParaDelete);

        }


        public void DelegateTest(Action delete)
        {

        }

        public void TestParaDelete()
        {


        }

        [Test]
        public void TestValue()
        {

            List<byte> list = new List<byte>();
            list.Add(0xaa);
            list.Add(0xbb);
            list.Add(01);       //设备号
            list.Add(0x1);      //身份验证中
            list.Add(0x03);    //发送身份请求
            list.Add(18);

            list.Add(0xcc);
            list.Add(0xdd);
        }

        [Test]
        public void TestIEnumberable()
        {
            TestMM testmm = new TestMM();
            List<TestMM> list = new List<TestMM>();
            list.Add(testmm);

            testmm.Id = "32432";
            testmm.Name = "张三";

            foreach (var VARIABLE in list)
            {
                Console.WriteLine(VARIABLE.Name + VARIABLE.Id);
            }
        }

        [Test]
        public void testIsNullOrEmpty()
        {
            string temp = "";
            if (temp.Length > 0)
            {
                Console.WriteLine(temp);
            }
            temp = null;
            Console.WriteLine(temp);

            temp = "";
            if (temp != null && temp.Length > 0)
            {
                Console.WriteLine(temp);
            }

            if (string.IsNullOrEmpty(temp))
            {
                Console.WriteLine(temp);
            }

        }

        /// <summary>
        /// 测试单例模式   获取的对象都是同一个对象，内部的属性都是同样的
        /// </summary>
        [Test]
        public void TestObjectInstance()
        {
            TestObject test1 = TestObject.GetInstance();
            test1.temp = "22";

            TestObject test2 = TestObject.GetInstance();

            Console.WriteLine("test1="+ test1.GetHashCode() + ":test2="+ test2.GetHashCode() );

            Console.WriteLine(" test1=" + test1.temp+" temp2="+test2.temp);
        }

        class TestObject
        {

            public string temp = "";
            private TestObject()
            {
            }

            private static TestObject testObject;

            public static TestObject GetInstance()
            {
                if (testObject == null)
                {
                    testObject = new TestObject();
                }
                return testObject;
            }

        }


        class TestMM
        {
            private string id;
            private string name;
            private int age;
            public string Id
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
