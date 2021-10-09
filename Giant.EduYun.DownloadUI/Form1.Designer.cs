
namespace Giant.EduYun.DownloadUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BaseDirDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaseDir = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbXueDuan = new System.Windows.Forms.ComboBox();
            this.cbNainJi = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbXueKe = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDanYuan = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "下载目录";
            // 
            // txtBaseDir
            // 
            this.txtBaseDir.Enabled = false;
            this.txtBaseDir.Location = new System.Drawing.Point(74, 16);
            this.txtBaseDir.Name = "txtBaseDir";
            this.txtBaseDir.Size = new System.Drawing.Size(174, 23);
            this.txtBaseDir.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(254, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "学段";
            // 
            // cbXueDuan
            // 
            this.cbXueDuan.DisplayMember = "Name";
            this.cbXueDuan.FormattingEnabled = true;
            this.cbXueDuan.Location = new System.Drawing.Point(74, 45);
            this.cbXueDuan.Name = "cbXueDuan";
            this.cbXueDuan.Size = new System.Drawing.Size(207, 25);
            this.cbXueDuan.TabIndex = 4;
            this.cbXueDuan.ValueMember = "Code";
            this.cbXueDuan.SelectedIndexChanged += new System.EventHandler(this.cbXueDuan_SelectedIndexChanged);
            // 
            // cbNainJi
            // 
            this.cbNainJi.DisplayMember = "Name";
            this.cbNainJi.FormattingEnabled = true;
            this.cbNainJi.Location = new System.Drawing.Point(73, 76);
            this.cbNainJi.Name = "cbNainJi";
            this.cbNainJi.Size = new System.Drawing.Size(207, 25);
            this.cbNainJi.TabIndex = 6;
            this.cbNainJi.ValueMember = "Code";
            this.cbNainJi.SelectedIndexChanged += new System.EventHandler(this.cbNainJi_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "年级";
            // 
            // cbXueKe
            // 
            this.cbXueKe.DisplayMember = "Name";
            this.cbXueKe.FormattingEnabled = true;
            this.cbXueKe.Location = new System.Drawing.Point(73, 107);
            this.cbXueKe.Name = "cbXueKe";
            this.cbXueKe.Size = new System.Drawing.Size(207, 25);
            this.cbXueKe.TabIndex = 8;
            this.cbXueKe.ValueMember = "Code";
            this.cbXueKe.SelectedIndexChanged += new System.EventHandler(this.cbXueKe_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "学科";
            // 
            // cbDanYuan
            // 
            this.cbDanYuan.DisplayMember = "Name";
            this.cbDanYuan.FormattingEnabled = true;
            this.cbDanYuan.Location = new System.Drawing.Point(73, 138);
            this.cbDanYuan.Name = "cbDanYuan";
            this.cbDanYuan.Size = new System.Drawing.Size(207, 25);
            this.cbDanYuan.TabIndex = 10;
            this.cbDanYuan.ValueMember = "Code";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "单元";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(73, 169);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(207, 51);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "开始下载";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(293, 230);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cbDanYuan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbXueKe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbNainJi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbXueDuan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBaseDir);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "云课堂视频下载器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog BaseDirDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaseDir;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbXueDuan;
        private System.Windows.Forms.ComboBox cbNainJi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbXueKe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDanYuan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStart;
    }
}

