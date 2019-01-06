using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProtectEye
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
            timer.Interval = 1000 * 10; // 单位:ms
            timer.Enabled = true;
        }

        /// <summary> 定时执行事件 </summary>
        private static void TimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //业务逻辑代码
            WarnForm _form = new WarnForm();
            _form.Show();
        }
    }
}
