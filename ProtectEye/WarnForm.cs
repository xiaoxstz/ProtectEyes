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
        private int restTime = 10; // 单位：分钟

        private DateTime now;
        private TimeSpan timeSpan;


        public WarnForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.showEvent += showThis;
            timerWarn.Start();
        }

        private void showThis()
        {
            restStartTime = DateTime.Now;
            restEndTime = restStartTime.AddMinutes(restTime);
            timerUpateTime.Start();

            this.WindowState = FormWindowState.Normal;
            btnStartWork.Visible = false;
            this.Show();
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
        /// 重写 WndProc 函数,禁用关闭
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
                return;
            }
            base.WndProc(ref m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                if (showEvent != null)
                {
                    showEvent();
                }
            }

        }

        private void timerUpateTime_Tick(object sender, EventArgs e)
        {
            now = DateTime.Now;
            timeSpan = restEndTime - now;
            labelTimeLeft.Text = String.Format("{0:00}:{1:00}:{2:00}",timeSpan.Hours, 
                                timeSpan.Minutes, timeSpan.Seconds);
            if(timeSpan.TotalSeconds <= 0)
            {
                btnStartWork.Visible = true;
                timerUpateTime.Stop();
            }
        }

        private void btnStartWork_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
