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
        private bool windowCreate = true;

        public WarnForm()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
            timer.Interval = 1000 * 10; // 单位:ms
            timer.Enabled = true;
        }

        /// <summary> 定时执行事件 </summary>
        private void TimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //业务逻辑代码
            //notifyIcon1_DoubleClick(null, null); // 会出现跨线程问题
            showEvent(true);
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
                this.Hide();
                return;
            }
            base.WndProc(ref m);
        }
    }
}
