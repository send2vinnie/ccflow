using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Windows.Forms;
using BP.WF;
using BP.Sys;
using BP;
using BP.En;

namespace SMSServices
{
    public class Glo
    {
        public static bool IsExitProcess(string name)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                if (pro.ProcessName + ".exe" == name)
                    return true;
            }
            return false;
        }
        public static bool KillProcess(string name)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                if (pro.ProcessName == name)
                {
                    pro.Kill();
                    return true;
                }
            }
            return false;
        }

        public static string PathOfVisualFlow
        {
            get
            {
                return @"D:\VisualWorkFlow\VisualFlow";
                //return Application.StartupPath + @"\.\..\..\..\VisualFlow\";
            }
        }
        public static void LoadConfigByFile()
        {
          //  BP.WF.Glo.IntallPath = PathOfVisualFlow;

            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;


            string path = PathOfVisualFlow + "\\web.config"; //如果有这个文件就装载它。
            if (System.IO.File.Exists(path) == false)
                throw new Exception("配置文件没有找到:" + path);


            BP.DA.ClassFactory.LoadConfig(path);
            try
            {
                BP.Port.Emp em = new BP.Port.Emp("admin");
            }
            catch
            {
                BP.Port.Emp em = new BP.Port.Emp("admin");
            }

            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
         //   BP.Win.WF.Global.FlowImagePath = BP.WF.Global.PathOfVisualFlow + "\\Data\\FlowDesc\\";

            BP.Web.WebUser.SysLang ="CH";
            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
        }
    }
}
