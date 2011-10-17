using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BP.DA;
using BP.Port;
using BP.WF;
using ICSharpCode.SharpZipLib.Zip;

namespace BP.Port
{
    public class WebUser
    {
        #region 个人的属性
        /// <summary>
        /// 执行什么？
        /// </summary>
        public static string DoWhat = null;
        public static bool isLogin = false;
        /// <summary>
        /// 人员编号
        /// </summary>
        public static string No = null;
        public static string Name = null;
        public static string Pass = null;
        public static Dept HisDept = null;
        public static string FK_DeptOfShiJu
        {
            get
            {
                return FK_Dept.Substring(0, 2);
            }
        }
        public static string FK_Dept = null;
        public static string FK_DeptName = null;
        public static int WorkID = 0;

        public static string _FK_Flow = null;



        public static string FK_Flow
        {
            get
            {
                return _FK_Flow;
            }
            set
            {
                _FK_Flow = value;
            }
        }

        private static int _FK_Node = 0;
        public static int FK_Node
        {
            get
            {
                if (FK_Flow == null)
                    return 0;

                if (_FK_Node == 0 || _FK_Node == 1)
                {
                    if (FK_Flow != null)
                        _FK_Node = int.Parse(FK_Flow + "01");
                }

                return _FK_Node;
            }
            set
            {
                _FK_Node = value;
            }
        }
        public static bool IsStartNode
        {
            get
            {
                if (int.Parse(WebUser.FK_Flow + "01") == WebUser.FK_Node)
                    return true;

                return false;
            }
        }
        public static bool IsSavePass=false;
        public static bool IsSaveInfo = false; 

        public static string DoType = null;
        public static string SID = null;
        private static Work _HisWork = null;
        public static Work HisWork
        {
            get
            {
                if (WebUser.WorkID == 0)
                    return null;

                if (WebUser._HisWork == null)
                    _HisWork = new Work(WebUser.FK_Node, WebUser.WorkID);

                return _HisWork;
                     
            }
            set
            {

                _HisWork = value;
                WebUser.WorkID = _HisWork.OID;
            }
        }
        public static WFWord2007.Ribbon1 HisRib = null;
        #endregion

        #region 公共的属性
        public static string AppServWorkID
        {
            get
            {
                return DBAccess.GetWebConfigByKey("WorkID");
            }
        }
        public static string AppServFK_Flow
        {
            get
            {
                return  DBAccess.GetWebConfigByKey("FK_Flow");
            }
        }
        public static string AppServFtpUser
        {
            get
            {
                return DBAccess.GetWebConfigByKey("FtpUser");
            }
        }
        public static string AppServFtpPass
        {
            get
            {
                return DBAccess.GetWebConfigByKey("FtpPass");
            }
        }
        #endregion

        public static bool LoadProfile()
        {
            if (Profile.IsExitProfile)
            {
                Profile.ProfileDoc = null;
                try
                {
                    if (System.IO.Directory.Exists( BP.WF.Glo.PathOfTInstall ) == false)
                        System.IO.Directory.CreateDirectory(BP.WF.Glo.PathOfTInstall );

                    WebUser.No = Profile.GetValByKey("No");
                    WebUser.Name = Profile.GetValByKey("Name");
                    WebUser.FK_Dept = Profile.GetValByKey("FK_Dept");

                    //WebUser.FK_Node = Profile.GetValIntByKey("FK_Node");
                    //WebUser.WorkID = Profile.GetValIntByKey("WorkID");
                    //WebUser.FK_Flow = Profile.GetValByKey("FK_Flow");

                    WebUser.FK_Node = 0; // Profile.GetValIntByKey("FK_Node");
                    WebUser.WorkID = 0; // Profile.GetValIntByKey("WorkID");
                    WebUser.FK_Flow = null; // nuProfile.GetValByKey("FK_Flow");

                    switch (WebUser.DoType)
                    {
                        case BP.WF.DoType.DoStartFlowByTemple: //我要打开ppt.
                        case BP.WF.DoType.DoStartFlow: //我要打开ppt.
                            break;
                        default:
                            break;
                    }


                    if (WebUser.DoType != null)
                    {
                        WebUser.DoType = "";  // 清除标记。
                        WebUser.WriterIt();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("@加载个人信心时间出现错误，有可能个人信息文件遭到破坏：" + ex.Message);
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="emp"></param>
        public static void Sigin(Emp emp)
        {
            WebUser.isLogin = true;
            WebUser.No = emp.No;
            WebUser.Name = emp.Name;
            WebUser.FK_Dept = emp.FK_Dept;
            WebUser.DoType = "";
            Dept dept = WebUser.HisDept;
            WebUser.WriterIt();

        }
        /// <summary>
        /// 写入文件
        /// </summary>
        public static void WriterIt()
        {
            WriterIt(WebUser.DoType, WebUser.FK_Flow, WebUser.FK_Node, WebUser.WorkID);
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="dotype"></param>
        /// <param name="fk_flow"></param>
        /// <param name="fk_node"></param>
        /// <param name="workid"></param>
        public static void WriterIt(string dotype, string fk_flow, int fk_node, int workid)
        {
            if (WebUser.IsSavePass == false)
                return;

            string strLocalPath = BP.WF.Glo.PathOfTInstall;
            if (Directory.Exists(strLocalPath) == false)
                Directory.CreateDirectory(strLocalPath);

            string strProFileName = BP.WF.Glo.PathOfTInstall + "\\Profile.txt";
            if (File.Exists(strProFileName))
                File.Delete(strProFileName);

            FileStream fs1 = File.Create(strProFileName);
            fs1.Close();

            string strContent = "@No=" + WebUser.No + "@Name=" + WebUser.Name + "@FK_Dept=" +
                WebUser.FK_Dept + "@FK_DeptName=" + WebUser.FK_DeptName
                + "@WorkID=" + workid + "@FK_Flow=" +
             fk_flow + "@FK_Node=" + fk_node + "@DoType=" + dotype;
            File.WriteAllText(strProFileName, strContent);
        }
        public static void WriterCookes()
        {
            if (WebUser.IsSaveInfo == false)
                return;

            string strLocalPath = BP.WF.Glo.PathOfTInstall;
            if (Directory.Exists(strLocalPath) == false)
                Directory.CreateDirectory(strLocalPath);

            string strProFileName = BP.WF.Glo.PathOfTInstall + "\\Login.txt";
            if (File.Exists(strProFileName))
                File.Delete(strProFileName);

            FileStream fs1 = File.Create(strProFileName);
            fs1.Close();

            string strContent = "@No=" + WebUser.No + "@Name=" + WebUser.Name;
            File.WriteAllText(strProFileName, strContent);
        }
    }
}
