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
        #region 参数设置区

        /// <summary>
        /// 休息时间
        /// </summary>
        private int restTime = 10; // 单位：分钟

        /// <summary>
        /// 允许将休息时间推迟几次
        /// </summary>
        private const int DELAY_TIMES = 3;

        /// <summary>
        /// 连续工作多久开始提示休息
        /// </summary>
        private const int RestWarnInterval = 60; // 单位:分钟
        #endregion

        // 自定义
        public delegate void showDelegate();
        /// <summary>
        /// 休息事件
        /// </summary>
        public event showDelegate startRest;
        private bool windowCreate = true;

        /// <summary> 开始休息的时间 </summary>
        private DateTime restStartTime;
        /// <summary> 结束休息的时间 </summary>
        private DateTime restEndTime;

        /// <summary>
        /// 这是第几次推迟
        /// </summary>
        private int delayTime = 0;

        private DateTime now;
        private TimeSpan timeSpan;


        public WarnForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.startRest += WarnRest;
            timerRestWarn.Interval = RestWarnInterval * 1000 * 60;
            timerRestWarn.Start();
            btnWorkAgainForAWhile.Visible = false;
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
            // 显示面板
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

        /// <summary>
        /// 用于提醒休息的计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRestWarn_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                if (startRest != null)
                {
                    startRest();
                }
            }
        }

        /// <summary>
        /// 用于更新时间的计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            Work();
        }

        private void 提前休息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerRestWarn_Tick(null,null);
        }

        private void WarnForm_Load(object sender, EventArgs e)
        {

        }

        private void btnWorkAgainForAWhile_Click(object sender, EventArgs e)
        {
            this.delayTime += 1;
            Work();
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        private void Work()
        {
            this.Hide();
            timerRestWarn.Start();
        }

        /// <summary>
        /// 提醒休息
        /// </summary>
        private void WarnRest()
        {
            Rest();
            if (this.delayTime < DELAY_TIMES)
            {
                btnWorkAgainForAWhile.Visible = true;
            }
            else
            {
                btnWorkAgainForAWhile.Visible = false;
                // 推迟次数置零
                this.delayTime = 0;
            }
        }

        /// <summary>
        /// 开始休息
        /// </summary>
        private void Rest()
        {
            restStartTime = DateTime.Now;
            restEndTime = restStartTime.AddMinutes(restTime);   // 计算休息结束时间

            // 弹窗遮挡，并禁用“开始工作”
            this.WindowState = FormWindowState.Normal;
            btnStartWork.Visible = false;
            this.Show();

            btnWorkAgainForAWhile.Visible = false;
            // 开始休息倒计时
            timerUpateTime.Start();
            timerRestWarn.Stop();
        }
    }
}
