using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speak
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechToPrompt("这是一条语音调试信息。");

            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }
        public static void SpeechToPrompt(string strText)
        {
            var tts = SpeekTTS.GetInstance();
            
            tts.SpeechToPrompt(strText);
        }
    }
    
}
