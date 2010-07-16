using System;
using System.Collections.Generic;
using System.Text;
using BP.Sys;
using BP;

namespace BP.WF
{
    public class Glo
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsShowFlowNum
        {
            get
            {
                switch (SystemConfig.AppSettings["IsShowFlowNum"])
                {
                    case "1":
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 仅仅显示用户编号？
        /// 考虑到中文的操作系统，与英文的操作系统不同。
        /// 
        /// </summary>
        public static bool IsShowUserNoOnly
        {
            get
            {
                switch (SystemConfig.AppSettings["IsShowUserNoOnly"])
                {
                    case "1":
                        return true;
                    default:
                        return false;
                }
            }
        }
        public static string GenerHelp(string helpId)
        {
            switch (helpId)
            {
                case "Book":
                    return "<a href=\"http://ccflow.cn\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo", "操作录像") + "</a>";
                case "FAppSet":
                    return "<a href=\"http://ccflow.cn\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo", "操作录像") + "</a>";
                default:
                    return "<a href=\"http://ccflow.cn\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo","操作录像") + "</a>";
                    break;
            }
        }
        public static string NodeImagePath
        {
            get {
                return Glo.IntallPath + "\\FlowDesign\\Data\\Node\\";
            }
        }
        public static void ClearDBData()
        {

            string sql = "delete wf_generworkflow where fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);

            sql = "delete wf_generworkerlist where fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);
        }
        public static string OEM_Flag = "CCS";
        public static string FlowFile
        {
            get { return  Glo.IntallPath+"\\FlowFile\\"; }
        }
        public static string FlowFileBook
        {
            get { return  Glo.IntallPath+"\\FlowFile\\Book\\"; }
        }
        public static string IntallPath="D:\\WorkFlow";
        /// <summary>
        /// 语言
        /// </summary>
        public static string Language = "CH";
        public static string CfgFilePath
        {
            get
            {
                return "D:\\WorkFlow\\web.config";
            }
        }
        public static bool IsQL
        {
            get
            {
                string s = BP.SystemConfig.AppSettings["IsQL"];
                if (s == null || s == "0")
                    return false;
                return true;
            }
        }
    }
}
