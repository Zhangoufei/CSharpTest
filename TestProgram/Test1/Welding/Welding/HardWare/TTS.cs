using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using Microsoft.Speech.Synthesis;

namespace HardWare
{
    public static class TTS
    {
        private static SpeechSynthesizer Synthesizer { get; set; }
        
        private static SoundPlayer Player { get; set; }

        private static List<string> MsgQueue { get; set; }

        private static Boolean Speaking { get; set; }
        /// <summary>
        /// 要说的内容
        /// </summary>
        private static string SpeakingText { get; set; }
        /// <summary>
        /// 内容即将被播放
        /// </summary>
        public static event Action<string> TextSpeaking;

        static TTS()
        {
            Synthesizer = new SpeechSynthesizer();
            Player = new SoundPlayer();

            MsgQueue = new List<String>();

            Synthesizer.SpeakCompleted += SynthesizerSpeakCompleted;

            ThreadPool.QueueUserWorkItem(SpeakLoop);
        }
        /// <summary>
        /// 加入语音播放队列
        /// </summary>
        /// <param name="text"></param>
        public static void Speak(string text)
        {
            lock (MsgQueue)
            {
                MsgQueue.Add(text);
            }
        }

        private static void SpeakLoop(object state)
        {
            while (true)
            {
                lock (MsgQueue)
                {
                    if (MsgQueue.Count == 0 || Speaking)//如果队列为空或正在播放语音
                    {
                        Thread.Sleep(300);
                        continue;
                    }

                    SpeakingText = MsgQueue[0];//播放队列中的第一条消息

                    SpeakInternal(SpeakingText);
                }
            }
        }

        private static void SpeakInternal(string msg)
        {
            Speaking = true;

            Synthesizer.SpeakAsyncCancelAll();
            Player.Stream = new System.IO.MemoryStream();
            Synthesizer.SetOutputToWaveStream(Player.Stream);
            Synthesizer.SpeakAsync(msg);
        }

        private static void SynthesizerSpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            //Console.WriteLine($"speak thread id:{Thread.CurrentThread.ManagedThreadId.ToString()}");
            
            OnTextSpeaking(SpeakingText);

            //语音播放用线程池中的线程来操作
            ThreadPool.QueueUserWorkItem(state =>
            {
                Player.Stream.Position = 0;
                Player.PlaySync();//同步播放

                //删除第一条语音，并设置读状态为假
                lock (MsgQueue)
                {
                    MsgQueue.RemoveAt(0);
                }

                Speaking = false;
            });

            
        }

        private static void OnTextSpeaking(string text)
        {
            var cache = TextSpeaking;

            if (cache != null)
            {
                cache.Invoke(text);
            }
        }
    }
}
