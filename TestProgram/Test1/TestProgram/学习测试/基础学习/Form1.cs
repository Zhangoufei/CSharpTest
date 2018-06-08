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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// AddRange(IEnumerable<T> collection)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string[] input = { "Brachiosaurus",
                           "Amargasaurus",
                           "Mamenchisaurus" };

            List<string> dinosaurs = new List<string>(input);

            Console.WriteLine("\nCapacity: {0}", dinosaurs.Capacity);

            foreach (var item in dinosaurs)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nAddRange(dinosaurs)");

            dinosaurs.AddRange(dinosaurs);

            Console.WriteLine();

            foreach (var item in dinosaurs)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n 去除数组");
            dinosaurs.RemoveRange(2, 2);

            Console.WriteLine();

            foreach (var item in dinosaurs)
            {
                Console.WriteLine(item);
            }


            input = new string[] { "Tyrannosaurus",
                               "Deinonychus",
                               "Velociraptor"};

            Console.WriteLine("\nInsertRange(3, input)");
            dinosaurs.InsertRange(3, input);

            Console.WriteLine();
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine(dinosaur);
            }

            Console.WriteLine("\noutput = dinosaurs.GetRange(2, 3).ToArray()");
            string[] output = dinosaurs.GetRange(2, 3).ToArray();

            Console.WriteLine();
            foreach (string dinosaur in output)
            {
                Console.WriteLine(dinosaur);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] input = { "Brachiosaurus",
                           "Amargasaurus",
                           "Mamenchisaurus",
                            "Mamenchisaurus2",
                             "Mamenchisaurus3",
                              "Mamenchisaurus4",
                               "Mamenchisaurus5",
            };

            List<string> list = new List<string>();
            list.AddRange(input);

            var result = list.Find(n =>
            {
                if (n.Contains("Mamenchisaurus5"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });



        }
    }
}
