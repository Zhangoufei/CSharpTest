namespace theoryExam
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbScore = new System.Windows.Forms.TextBox();
            this.dt1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.answer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dt1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 14F);
            this.label1.Location = new System.Drawing.Point(52, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(132, 24);
            this.tbTitle.Multiline = true;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(455, 47);
            this.tbTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 14F);
            this.label2.Location = new System.Drawing.Point(641, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "理论分值";
            // 
            // tbScore
            // 
            this.tbScore.Location = new System.Drawing.Point(740, 24);
            this.tbScore.Multiline = true;
            this.tbScore.Name = "tbScore";
            this.tbScore.Size = new System.Drawing.Size(54, 47);
            this.tbScore.TabIndex = 3;
            // 
            // dt1
            // 
            this.dt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dt1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dt1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dt1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dt1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.A,
            this.B,
            this.C,
            this.D,
            this.answer,
            this.Type});
            this.dt1.Location = new System.Drawing.Point(28, 86);
            this.dt1.Name = "dt1";
            this.dt1.RowTemplate.Height = 23;
            this.dt1.Size = new System.Drawing.Size(1231, 597);
            this.dt1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1116, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 45);
            this.button1.TabIndex = 5;
            this.button1.Text = "生成文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Title
            // 
            this.Title.HeaderText = "试题";
            this.Title.Name = "Title";
            this.Title.Width = 54;
            // 
            // A
            // 
            this.A.HeaderText = "A";
            this.A.Name = "A";
            this.A.Width = 36;
            // 
            // B
            // 
            this.B.HeaderText = "B";
            this.B.Name = "B";
            this.B.Width = 36;
            // 
            // C
            // 
            this.C.HeaderText = "C";
            this.C.Name = "C";
            this.C.Width = 36;
            // 
            // D
            // 
            this.D.HeaderText = "D";
            this.D.Name = "D";
            this.D.Width = 36;
            // 
            // answer
            // 
            this.answer.HeaderText = "答案";
            this.answer.Name = "answer";
            this.answer.Width = 54;
            // 
            // Type
            // 
            this.Type.HeaderText = "类别";
            this.Type.Name = "Type";
            this.Type.Width = 54;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 720);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dt1);
            this.Controls.Add(this.tbScore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbScore;
        private System.Windows.Forms.DataGridView dt1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn A;
        private System.Windows.Forms.DataGridViewTextBoxColumn B;
        private System.Windows.Forms.DataGridViewTextBoxColumn C;
        private System.Windows.Forms.DataGridViewTextBoxColumn D;
        private System.Windows.Forms.DataGridViewTextBoxColumn answer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
    }
}

