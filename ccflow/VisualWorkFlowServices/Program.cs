using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMSServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Glo.IsExitProcess("SMSServices.exe"))
            {
                MessageBox.Show("驰骋工作流程设计器应用程序已经启动，您不能同时启动两个操作窗口。", "操作提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

            Glo.LoadConfigByFile();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
