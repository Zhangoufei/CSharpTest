using JAAJ.HighVoltageSwitchCabinet.Speek;
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
    /// SpeakTest.xaml 的交互逻辑
    /// </summary>
    public partial class SpeakTest : UserControl
    {
        public SpeakTest()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string temp = textBox.Text.Trim();
            SpeekTTS tts = SpeekTTS.GetInstance();
            tts.SpeechToPrompt(temp);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            string temp = textBox1.Text.Trim();
            SpeekTTS tts = SpeekTTS.GetInstance();
            tts.SpeechToPrompt(temp);
        }
    }
}
