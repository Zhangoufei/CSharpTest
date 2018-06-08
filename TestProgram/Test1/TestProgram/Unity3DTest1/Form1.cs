using ControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Unity3DTest1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         //   SetStyle(ControlStyles.Opaque, true);
          //  CreateControl();
           // this.Visible = false;
            string paht = Application.StartupPath + "\\unity\\保温层.unity3d";
            // InitUnity(paht);

            u3DPlayer1.InitUnity(paht);

        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020; //0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
        //        return cp;
        //    }
        //}
        protected override void OnPaint(PaintEventArgs e)
        {
            float vlblControlWidth;
            float vlblControlHeight;

            Pen labelBorderPen;
            SolidBrush labelBackColorBrush;


            Color drawColor = Color.FromArgb(0, this.BackColor);
            labelBorderPen = new Pen(drawColor, 0);
            labelBackColorBrush = new SolidBrush(drawColor);


            base.OnPaint(e);
            vlblControlWidth = this.Size.Width;
            vlblControlHeight = this.Size.Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        bool init;
        string Src;
        #region InitUnity
        private AxUnityWebPlayerAXLib.AxUnityWebPlayer _axUnityWebPlayer = null;
        /// <summary>
        /// 初始化UnityWebPlayer
        /// </summary>
        /// <param name="src">Unity3D文件的路径</param>
        public void InitUnity(String src)
        {
            Src = src;
            if (!File.Exists(Src))
            {
                return;
            }
            init = false;
            var unity = new AxUnityWebPlayerAXLib.AxUnityWebPlayer();
            ((System.ComponentModel.ISupportInitialize)(unity)).BeginInit();
            base.Controls.Add(unity);
            ((System.ComponentModel.ISupportInitialize)(unity)).EndInit();
            unity.src = Src;//Application.StartupPath + "\\u.unity3d";  //改成自己想要的路径
            AxHost.State state = unity.OcxState;
            base.Controls.Remove(unity);
            unity.Dispose();
            unity = new AxUnityWebPlayerAXLib.AxUnityWebPlayer();
            ((System.ComponentModel.ISupportInitialize)(unity)).BeginInit();
            this.SuspendLayout();
            unity.Dock = DockStyle.Fill;

            unity.Name = "Unity";
            unity.OcxState = state;
            unity.TabIndex = 0;
            this.Controls.Add(unity); //panel1是我用的一个容器，改成this.Controls也可以
            ((System.ComponentModel.ISupportInitialize)(unity)).EndInit();
            this.ResumeLayout(false);
            _axUnityWebPlayer = unity;
            if (_axUnityWebPlayer == null)
            {
                throw new Exception("_axUnityWebPlayer init fail");
            }
            else
            {
                _axUnityWebPlayer.OnExternalCall += _axUnityWebPlayer_OnExternalCall;
                _axUnityWebPlayer.Hide();
            }
        }

        void _axUnityWebPlayer_OnExternalCall(object sender, AxUnityWebPlayerAXLib._DUnityWebPlayerAXEvents_OnExternalCallEvent e)
        {
            ///接受到LOAD_COMPLETE说明WEBPLAYER初始化完毕
            if (e.value.StartsWith("LOAD_COMPLETE"))
            {
                this.Visible = true;
                init = true;
                if (!_axUnityWebPlayer.Visible)
                {

                    _axUnityWebPlayer.Width = this.Width;
                    _axUnityWebPlayer.Height = this.Height;
                    _axUnityWebPlayer.Show();
                }
            }
            OnUnityCall(sender, e);
        }
        public delegate void ExternalCallHandler(object sender, AxUnityWebPlayerAXLib._DUnityWebPlayerAXEvents_OnExternalCallEvent e);
        [Browsable(true), Description("接收Unity调用宿主(如WinForm)函数的消息")]
        public event ExternalCallHandler UnityCall;
        //方法
        public void OnUnityCall(object sender, AxUnityWebPlayerAXLib._DUnityWebPlayerAXEvents_OnExternalCallEvent e)
        {
            if (UnityCall != null)
            {
                UnityCall(sender, e);
            }
        }
        #endregion

    }
}
