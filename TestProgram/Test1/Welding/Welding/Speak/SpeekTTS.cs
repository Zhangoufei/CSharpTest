using System;
using System.Media;
using Microsoft.Speech.Synthesis;

namespace Speak
{
    /// <summary>
    /// 语音提示类
    /// </summary>
    public class SpeekTTS
    {
        private static SpeekTTS _speekTTS;
        public static SpeekTTS GetInstance()
        {
            _speekTTS = new SpeekTTS();
            return _speekTTS;
        }

        private SpeechSynthesizer tts = new SpeechSynthesizer();
        /// <summary>
        /// 播放读取的文字
        /// </summary>
        private SoundPlayer player = new SoundPlayer();

        public SpeekTTS()
        {
            tts.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(tts_SpeakCompleted);
        }
        void tts_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            player.Stream.Position = 0;
            player.Play();//.Play();
        }

        /// <summary>
        /// 语音提示方法
        /// </summary>
        /// <param name="text"></param>
        public void SpeechToPrompt(string text)
        {
            Console.WriteLine("TtsVolume:{tts.TtsVolume.ToString()}");
            Console.WriteLine("Volume:{tts.Volume.ToString()}");

            tts.SpeakAsyncCancelAll();
            player.Stream = new System.IO.MemoryStream();
            tts.SetOutputToWaveStream(player.Stream);
            tts.SpeakAsync(text);
        }

        /// <summary>
        /// 语音提示方法
        /// </summary>
        /// <param name="text"></param>
        public void SyncSpeechToPrompt(string text)
        {
            player.Stream = new System.IO.MemoryStream();
            tts.SetOutputToWaveStream(player.Stream);
            tts.Speak(text);
        }
    }
}
