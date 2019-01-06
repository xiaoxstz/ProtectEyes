using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ProtectEyeService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
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
            this.WriteLog("执行事件");            
        }

        protected override void OnStart(string[] args)
        {
            this.WriteLog("护眼服务：【服务启动】");
        }

        protected override void OnStop()
        {
            this.WriteLog("护眼服务：【服务停止】");
        }
        protected override void OnShutdown()
        {
            this.WriteLog("护眼服务：【计算机关闭】");
        }

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {

            //string path = @"C:\log.txt";

            //该日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                FileStream fs;
                fs = File.Create(path);
                fs.Close();
            }

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "   " + msg);
                }
            }
        }
        #endregion
    }
}
