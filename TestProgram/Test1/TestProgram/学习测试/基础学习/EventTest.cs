using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 学习测试.基础学习
{
    public partial class EventTest : Form
    {
        public EventTest()
        {
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            EventTestClass.Start = true;
            EventTestClass eventTestClass = new EventTestClass();
            eventTestClass.RegisterEvent += EventTestClass_RegisterEvent;
            eventTestClass.StartEvent("你好啊");

          
        }

        private void EventTestClass_RegisterEvent(string obj)
        {
            if (obj != null)
            {
                MessageBox.Show(obj);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }


    class EventTestClass
    {

        public  event Action<string> RegisterEvent;

        void RegistSerialQuery(string temp)
        {
            var inoke = RegisterEvent;
            if (inoke != null)
            {
                inoke(temp);
            }
        }

        public static bool Start;
        public void StartEvent(string temp)
        {
            int i = 0;
            while (Start)
            {
                i++;
                RegistSerialQuery(temp+i);
            }
        }

    }

}
