using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Welding
{
    [TestFixture]
    class Test1
    {
        [Test]
        public void T1()
        {
            Console.WriteLine(Enum.Parse(typeof(Utensil), "WeldingWorkClothes"));
        }
        [Test]
        public void T2()
        {
            var l1 = new List<string>() {"111","222"};
            var l2 =new List<string>() {"222","333"};
            var l3 = l1.Union(l2);
            foreach (var item in l3)
            {
                Console.WriteLine(item);
            }
        }
        [Test]
        public void T3()
        {
            var state = (DeviceState)0xc0;

            var state2 = (DeviceState) 7;
            Console.WriteLine(state.ToString());

            Console.WriteLine(state2 == state);
        }
        [Test]
        public void T4()
        {
            var l1 = new List<Int32>() {1,};
            var l2 = new List<Int32>() { 1, 2, 3 };

            Console.WriteLine(l1.Except(l2).Count());
        }
        [Test]
        public void T5()
        {
            Console.WriteLine(Path.Combine(ConfigurationManager.AppSettings["sceneDir"], @"weld\config\Score.ini"));
        }
    }
}
