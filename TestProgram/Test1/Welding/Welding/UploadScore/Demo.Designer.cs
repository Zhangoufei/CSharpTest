namespace UploadScore
{
    partial class Demo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtIDNum = new System.Windows.Forms.TextBox();
            this.IDNUM = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始考试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(63, 238);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "手动编写成绩保存成绩";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(305, 238);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(211, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "解析XML文件保存成绩";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtIDNum
            // 
            this.txtIDNum.Location = new System.Drawing.Point(124, 90);
            this.txtIDNum.Name = "txtIDNum";
            this.txtIDNum.Size = new System.Drawing.Size(379, 21);
            this.txtIDNum.TabIndex = 1;
            // 
            // IDNUM
            // 
            this.IDNUM.AutoSize = true;
            this.IDNUM.Location = new System.Drawing.Point(61, 94);
            this.IDNUM.Name = "IDNUM";
            this.IDNUM.Size = new System.Drawing.Size(65, 12);
            this.IDNUM.TabIndex = 2;
            this.IDNUM.Text = "身份证号：";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(63, 333);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(248, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "部分终端刷完卡未开始考试更新设备状态";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(48, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(509, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "注意事项：其它终端引用这个dll的时候，其本身的项目的连接字符串要定义Name为“strConn”";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(328, 333);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(211, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "开始考试";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button1_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(424, 142);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 458);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtIDNum);
            this.Controls.Add(this.IDNUM);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Name = "Demo";
            this.Text = "Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtIDNum;
        private System.Windows.Forms.Label IDNUM;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}