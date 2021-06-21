﻿using System;
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
        /// 允许将休息时间推迟几次
        /// </summary>
        private const int DELAY_COUNT = 3;

        /// <summary>
        /// 推迟休息的时间（分钟）
        /// </summary>
        private const double DELAY_MINUTE = 5;

        /// <summary>
        /// 连续工作多久开始提示休息
        /// </summary>
        private const double RestWarnInterval = 60; // 单位:分钟

        /// <summary>
        /// 休息时间
        /// </summary>
        private const double restTime = 10; // 单位：分钟
        #endregion

        // 自定义
        public delegate void showDelegate();
        /// <summary>
        /// 休息事件
        /// </summary>
        public event showDelegate startRest;

        private bool windowCreate = true;

        /// <summary> 结束休息的时间 </summary>
        private DateTime restEndTime;

        /// <summary>
        /// 这是第几次推迟
        /// </summary>
        private int delayCount = 0;

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
            Work(RestWarnInterval);
        }

        private void 提前休息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerRestWarn_Tick(null,null);
            btnWorkAgainForAWhile.Visible = false;
        }

        private void WarnForm_Load(object sender, EventArgs e)
        {
            this.Location = GetCenter(Screen.PrimaryScreen, this);
        }

        private void btnWorkAgainForAWhile_Click(object sender, EventArgs e)
        {
            this.delayCount += 1;
            Work(DELAY_MINUTE);
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        private void Work(double minute)
        {
            this.Hide();
            timerRestWarn.Interval = (int)(minute * 1000 * 60);
            timerRestWarn.Start();
        }

        /// <summary>
        /// 提醒休息
        /// </summary>
        private void WarnRest()
        {
            Rest();
            if (this.delayCount < DELAY_COUNT)
            {
                btnWorkAgainForAWhile.Visible = true;
            }
            else
            {
                btnWorkAgainForAWhile.Visible = false;
                // 推迟次数置零
                this.delayCount = 0;
            }
        }

        /// <summary>
        /// 开始休息
        /// </summary>
        private void Rest()
        {
            restEndTime = DateTime.Now.AddMinutes(restTime);   // 计算休息结束时间

            // 弹窗遮挡，并禁用“开始工作”
            this.WindowState = FormWindowState.Normal;
            btnStartWork.Visible = false;
            this.Show();
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            btnWorkAgainForAWhile.Visible = false;
            // 开始休息倒计时
            timerUpateTime.Start();
            timerRestWarn.Stop();
        }

        private void WarnForm_Shown(object sender, EventArgs e)
        {
            foreach(Screen screen in Screen.AllScreens)
            {
                // block window
                if (!screen.Primary)
                {
                    BlockForm form = new BlockForm();
                    form.Show();

                    // 居中
                    form.Location = GetCenter(screen, form);
                }
            }
        }

        
        /// <summary>
        /// 获取，让窗口在屏幕上居中，需要的location点(左上角)
        /// </summary>
        /// <param name="screen">屏幕</param>
        /// <param name="form">窗体</param>
        /// <returns>点</returns>
        private Point GetCenter(Screen screen, Form form)
        {
            int shift_x = (screen.Bounds.Width - form.Width) / 2;
            int shift_y = (screen.Bounds.Height - form.Height) / 2;
            int x = screen.Bounds.X + shift_x;
            int y = screen.Bounds.Y + shift_y;
            return new Point(x, y);
        }
    }
}
