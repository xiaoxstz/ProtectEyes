namespace ProtectEye
{
    partial class WarnForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarnForm));
            this.label1 = new System.Windows.Forms.Label();
            this.labelTimeLeft = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStartWork = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.提前休息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRestWarn = new System.Windows.Forms.Timer(this.components);
            this.timerUpateTime = new System.Windows.Forms.Timer(this.components);
            this.btnWorkAgainForAWhile = new System.Windows.Forms.Button();
            this.contextMenuStripNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(424, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "保护眼睛，小心猝死！";
            // 
            // labelTimeLeft
            // 
            this.labelTimeLeft.AutoSize = true;
            this.labelTimeLeft.Location = new System.Drawing.Point(679, 243);
            this.labelTimeLeft.Name = "labelTimeLeft";
            this.labelTimeLeft.Size = new System.Drawing.Size(124, 27);
            this.labelTimeLeft.TabIndex = 1;
            this.labelTimeLeft.Text = "00:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "强制休息倒计时:";
            // 
            // btnStartWork
            // 
            this.btnStartWork.Location = new System.Drawing.Point(530, 306);
            this.btnStartWork.Name = "btnStartWork";
            this.btnStartWork.Size = new System.Drawing.Size(174, 56);
            this.btnStartWork.TabIndex = 2;
            this.btnStartWork.Text = "开始工作";
            this.btnStartWork.UseVisualStyleBackColor = true;
            this.btnStartWork.Click += new System.EventHandler(this.btnStartWork_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripNotify;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "强制休息";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStripNotify
            // 
            this.contextMenuStripNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.提前休息ToolStripMenuItem});
            this.contextMenuStripNotify.Name = "contextMenuStripNotify";
            this.contextMenuStripNotify.Size = new System.Drawing.Size(125, 26);
            // 
            // 提前休息ToolStripMenuItem
            // 
            this.提前休息ToolStripMenuItem.Name = "提前休息ToolStripMenuItem";
            this.提前休息ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.提前休息ToolStripMenuItem.Text = "提前休息";
            this.提前休息ToolStripMenuItem.Click += new System.EventHandler(this.提前休息ToolStripMenuItem_Click);
            // 
            // timerRestWarn
            // 
            this.timerRestWarn.Interval = 6000;
            this.timerRestWarn.Tick += new System.EventHandler(this.timerRestWarn_Tick);
            // 
            // timerUpateTime
            // 
            this.timerUpateTime.Interval = 1000;
            this.timerUpateTime.Tick += new System.EventHandler(this.timerUpateTime_Tick);
            // 
            // btnWorkAgainForAWhile
            // 
            this.btnWorkAgainForAWhile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWorkAgainForAWhile.Font = new System.Drawing.Font("宋体", 10F);
            this.btnWorkAgainForAWhile.Location = new System.Drawing.Point(1164, 567);
            this.btnWorkAgainForAWhile.Name = "btnWorkAgainForAWhile";
            this.btnWorkAgainForAWhile.Size = new System.Drawing.Size(108, 29);
            this.btnWorkAgainForAWhile.TabIndex = 2;
            this.btnWorkAgainForAWhile.Text = "过会儿再休息";
            this.btnWorkAgainForAWhile.UseVisualStyleBackColor = true;
            this.btnWorkAgainForAWhile.Click += new System.EventHandler(this.btnWorkAgainForAWhile_Click);
            // 
            // WarnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1284, 608);
            this.Controls.Add(this.btnWorkAgainForAWhile);
            this.Controls.Add(this.btnStartWork);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTimeLeft);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "WarnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "强制休息";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.WarnForm_Load);
            this.contextMenuStripNotify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTimeLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartWork;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timerRestWarn;
        private System.Windows.Forms.Timer timerUpateTime;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotify;
        private System.Windows.Forms.ToolStripMenuItem 提前休息ToolStripMenuItem;
        private System.Windows.Forms.Button btnWorkAgainForAWhile;
    }
}