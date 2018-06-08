using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WPF布局1.bLL
{
    class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;

        public string Name {
            get { return name; }
            set {
                name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this,new PropertyChangedEventArgs("Name"));
                }

            }
        }

    }

    public class Student2
    {
        private int id;

        private string name2;

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

        public string Name2
        {
            get
            {
                return name2;
            }

            set
            {
                name2 = value;
            }
        }
    }

}
