using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProtectEye
{
    public partial class WarnForm : Form
    {
        // 自定义
        public delegate void showDelegate();
        public event showDelegate showEvent;
        private bool windowCreate = true;

        /// <summary> 开始休息的时间 </summary>
        private DateTime restStartTime;
        /// <summary> 结束休息的时间 </summary>
        private DateTime restEndTime;
        /// <summary> 休息时间 </summary>
        private int restTime = 5; // 单位：分钟

        private DateTime now;
        private TimeSpan timeSpan;


        public WarnForm()
        {
            InitializeComponent();
            this.showEvent += showThis;
            timer1.Start();

        }

        private void showThis()
        {
            restStartTime = DateTime.Now;
            restEndTime = restStartTime.AddMinutes(restTime);
            timerUpateTime.Start();

            this.Show();
        }

        private void hideThis()
        {
            timerUpateTime.Stop();
            this.Hide();
        }

        protected override void OnActivated(EventArgs e)
        {
            if (windowCreate)
            {
                base.Visible = false;
                windowCreate = false;
            }

            base.OnActivated(e);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                //this.Show();
                this.BringToFront();
            }

        }

        /// <summary>
        /// 重写 WndProc 函数,禁用关闭,改为隐藏
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            // 当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息
            const int WM_SYSCOMMAND = 0x0112;
            // 禁止关闭按钮(值就为0xF060),参考:https://www.cnblogs.com/licongzhuo/p/8057474.html
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                hideThis();
                return;
            }
            base.WndProc(ref m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (showEvent != null)
            {
                showEvent();
            }
        }

        private void timerUpateTime_Tick(object sender, EventArgs e)
        {
            now = DateTime.Now;
            timeSpan = restEndTime - now;
            labelTimeLeft.Text += timeSpan.ToString();
            if(timeSpan.TotalSeconds <= 0)
            {
                btnStartWork.Visible = true;
            }
        }
    }
}
