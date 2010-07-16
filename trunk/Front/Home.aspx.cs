using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Web;
using BP.En;
using BP.DA;
using BP.WF;
using BP.Sys;
using BP.Port;
using BP;

public partial class Face_Home : WebPage
{
    public string SID
    {
        get
        {
            return this.Request.QueryString["SID"];
        }
    }
    public string UserNo
    {
        get
        {
            return this.Request.QueryString["UserNo"];
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (this.UserNo != null && this.UserNo != BP.Web.WebUser.No)
        {
            string sql = "select sid  from port_emp where no='" + this.UserNo + "'";
            string sid = BP.DA.DBAccess.RunSQLReturnVal(sql).ToString();
            if (sid != this.SID)
            {
                this.Pub1.AddMsgOfWarning("错误：", "非法的访问，请与管理员联系。");
                return;
            }
            else
            {
                Emp em = new Emp(this.UserNo);
                HttpCookie cookie = new HttpCookie("CCS");
                cookie.Expires = DateTime.Now.AddMonths(10);
                cookie.Values.Add("UserNo", em.No);
                cookie.Values.Add("UserName", em.Name);
                cookie.Values.Add("Lang", BP.SystemConfig.SysLanguage);
                cookie.Values.Add("Token", this.Page.Session.SessionID);
                //cookie.Values.Add( "FontSize",  em.FontSize);
                Response.AppendCookie(cookie);
                WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);
                WebUser.Token = this.Session.SessionID;
            }
        }


        string fk_flow = this.Session["FK_Flow"] as string;
        if (fk_flow != null)
        {
            this.Session["FK_Flow"] = null;
            this.WinOpen("MyFlow.aspx?FK_Flow=" + fk_flow);
        }

        try
        {
            this.Today();
        }
        catch (Exception ex)
        {
            if (BP.SystemConfig.IsDebug)
            {
                BP.WF.Glo.ClearDBData();
            }
            throw ex;
        }
    }
    /// <summary>
    /// 我的今天
    /// </summary>
    public void Today()
    {
        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='100%' >");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='100%' height='20' colspan='2'><b>&nbsp;&nbsp;</b></td>");
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='50%'  height='100%' rowspan='3' valign='top' align='right'>");

        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='98%' height='100%' bgcolor='#F3FFE3'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/11.gif'></td>");
        /////////王思修改,增加管理员工具
        if (WebUser.No == "admin" || WebUser.No == "028888")
        {
            this.Pub1.Add("<td background='images/12.gif'><b><font color='#ff0000'>" + this.ToE("AdminTools", "管理员工作列表") + "</font></b></td>");
        }
        else
        {
            this.Pub1.Add("<td background='images/12.gif'><b><font color='#ff0000'>" + this.ToE("PendingWork", "待办工作") + "</font></b></td>");
        }
        //////////////////////////////////////////////////
        this.Pub1.Add("<td width='15' height='30' background='images/13.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' background='images/14.gif'></td>");
        this.Pub1.Add("<td>");

        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='100%' height='100%'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='100%' valign='top'>");

        this.DBGZ();

        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");


        this.Pub1.Add("</td>");
        this.Pub1.Add("<td width='15' background='images/15.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/16.gif'></td>");
        this.Pub1.Add("<td background='images/17.gif'></td>");
        this.Pub1.Add("<td width='15' height='30' background='images/18.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");
        //待办工作尾

        this.Pub1.Add("</td>");
        this.Pub1.Add("<td width='50%' valign='top' align='right'>");


        if (this.UserNo != null)
        {
            /*说明是外部调用接口 ， 就显示菜单。 */
            this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='98%' bgcolor='#F3FFE3'>");
            this.Pub1.Add("<tr>");
            this.Pub1.Add("<td width='15' height='30' background='images/11.gif'></td>");
            this.Pub1.Add("<td background='images/12.gif' ><b><font color='#ff0000'>" + this.ToE("FlowMenu", "流程菜单") + "</font></b></td>");
            this.Pub1.Add("<td width='15' height='30' background='images/13.gif'></td>");
            this.Pub1.Add("</tr>");
            this.Pub1.Add("<tr>");
            this.Pub1.Add("<td width='15' background='images/14.gif'></td>");
            this.Pub1.Add("<td>");

            this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");
            this.Pub1.Add("<tr>");
            this.Pub1.Add("<td width='100%'>");

            BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
            ens.RetrieveAll();
            foreach (BP.WF.XML.ToolBar en in ens)
            {
                if (en.No == "Home" || en.No == "CS")
                    continue;
                this.Pub1.Add("<a href='./WF/" + en.Url + "' title='" + en.Title + "' ><img src='./Port/" + en.Img + "' border='0' >" + en.Name + "</a>&nbsp;");
            }
            return;


            this.Pub1.Add("</td>");
            this.Pub1.Add("</tr>");
            this.Pub1.Add("</table>");

            this.Pub1.Add("</td>");
            this.Pub1.Add("<td width='15' background='images/15.gif'></td>");
            this.Pub1.Add("</tr>");
            this.Pub1.Add("<tr>");
            this.Pub1.Add("<td width='15' height='30' background='images/16.gif'></td>");
            this.Pub1.Add("<td background='images/17.gif'></td>");
            this.Pub1.Add("<td width='15' height='30' background='images/18.gif'></td>");
            this.Pub1.Add("</tr>");
            this.Pub1.Add("</table>");
        }

        //工作预警头
        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='98%' bgcolor='#F3FFE3'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/11.gif'></td>");
        this.Pub1.Add("<td  background='images/12.gif' onclick=\"javascript:To('Warning.aspx')\"><b><font color='#ff0000'>" + this.ToE("WorkEarlyWarning", "工作预警") + "</font></b></td>");
        this.Pub1.Add("<td width='15' height='30' background='images/13.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' background='images/14.gif'></td>");
        this.Pub1.Add("<td>");

        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='100%'>");

        this.Flow();

        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");

        this.Pub1.Add("</td>");
        this.Pub1.Add("<td width='15' background='images/15.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/16.gif'></td>");
        this.Pub1.Add("<td background='images/17.gif'></td>");
        this.Pub1.Add("<td width='15' height='30' background='images/18.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");
        //工作预警尾

        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='50%' valign='top' align='right'>");

        //我的文书头
        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='98%' bgcolor='#F3FFE3'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/11.gif'></td>");
        this.Pub1.Add("<td background='images/12.gif'><b><font color='#ff0000'>" + this.ToE("MyBook", "我的文书") + "</font></b></td>");
        this.Pub1.Add("<td width='15' height='30' background='images/13.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' background='images/14.gif'></td>");
        this.Pub1.Add("<td>");

        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='100%'>");

        this.MyBook();

        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");

        this.Pub1.Add("</td>");
        this.Pub1.Add("<td width='15' background='images/15.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/16.gif'></td>");
        this.Pub1.Add("<td background='images/17.gif'></td>");
        this.Pub1.Add("<td width='15' height='30' background='images/18.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");
        //我的文书尾

        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='50%' valign='top' align='right'>");

        //我的岗位头
        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='98%' bgcolor='#F3FFE3'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/11.gif'></td>");
        this.Pub1.Add("<td background='images/12.gif'><b><font color='#FF0000'>" + this.ToE("MySts", "我的岗位") + "</font></b></td>");
        this.Pub1.Add("<td width='15' height='30' background='images/13.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' background='images/14.gif'></td>");
        this.Pub1.Add("<td>");

        this.Pub1.Add("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='100%'>");
        this.My();
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");
        //我的岗位尾

        this.Pub1.Add("</td>");
        this.Pub1.Add("<td width='15' background='images/15.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='15' height='30' background='images/16.gif'></td>");
        this.Pub1.Add("<td background='images/17.gif'></td>");
        this.Pub1.Add("<td width='15' height='30' background='images/18.gif'></td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");

        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<td width='50%'>");
        this.Pub1.Add("</td>");
        this.Pub1.Add("<td width='50%'>");
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");
    }
    private void MyBook()
    {
        return;

        //我的文书
        this.Pub1.Add("<Table width='100%' class='TableWork'>");
        this.Pub1.Add("<TR class='TableFlowTR'>");
        this.Pub1.Add("<TD class='TableFlowTD' title='有" + BP.WF.Book.NumOfUnSend + "个文书未送达' onclick=\"javascript:To('../Comm/PanelEns.aspx?EnsName=BP.WF.Books&FK_NY=all&BookState=" + (int)BookState.UnSend + "')\" ><img src='./Images/UnRead_s.ico' border=0 />&nbsp;未送达</TD>");
        this.Pub1.Add("<TD class='TableFlowTD'>" + BP.WF.Book.NumOfUnSend + "</TD>");
        this.Pub1.Add("</TR>");

        this.Pub1.Add("<TR class='TableFlowTR'>");
        this.Pub1.Add("<TD class='TableFlowTD' title='有" + BP.WF.Book.NumOfUnSendTimeout + "个文书逾期未送达' onclick=\"javascript:To('../Comm/PanelEns.aspx?EnsName=BP.WF.Books&FK_NY=all&BookState=" + (int)BookState.UnSendTimeout + "')\" ><img src='./Images/Bell.gif' border=0 />&nbsp;逾期未送达</TD>");
        this.Pub1.Add("<TD class='TableFlowTD'>" + BP.WF.Book.NumOfUnSendTimeout + "</TD>");
        this.Pub1.Add("</TR>");

        this.Pub1.Add("<TR class='TableFlowTR'>");
        this.Pub1.Add("<TD class='TableFlowTD' title='有" + BP.WF.Book.NumOfSend + "个文书已送达' onclick=\"javascript:To('../Comm/PanelEns.aspx?EnsName=BP.WF.Books&FK_NY=all&BookState=" + (int)BookState.Send + "')\"  ><img src='./Images/SendOver.ico' border=0 />&nbsp;已送达</TD>");
        this.Pub1.Add("<TD class='TableFlowTD'>" + BP.WF.Book.NumOfSend + "</TD>");
        this.Pub1.Add("</TR>");

        this.Pub1.Add("<TR class='TableFlowTR'>");
        this.Pub1.Add("<TD class='TableFlowTD' title='有" + BP.WF.Book.NumOfNotfind + "个文书送达时未找到人' onclick=\"javascript:To('../Comm/PanelEns.aspx?EnsName=BP.WF.Books&FK_NY=all&BookState=" + (int)BookState.Notfind + "')\"  ><img src='./Images/UnSend.ico' border=0 />&nbsp;未找到人</TD>");
        this.Pub1.Add("<TD class='TableFlowTD'>" + BP.WF.Book.NumOfNotfind + "</TD>");
        this.Pub1.Add("</TR>");

        this.Pub1.Add("<TR class='TableFlowTR'>");
        this.Pub1.Add("<TD class='TableFlowTD' title='有" + BP.WF.Book.NumOfPigeonhole + "个文书已归档' onclick=\"javascript:To('../Comm/PanelEns.aspx?EnsName=BP.WF.Books&FK_NY=all&BookState=" + (int)BookState.Pigeonhole + "')\"  ><img src='./Images/Book_s.ico' border=0 />&nbsp;已归档</TD>");
        this.Pub1.Add("<TD class='TableFlowTD'>" + BP.WF.Book.NumOfPigeonhole + "</TD>");
        this.Pub1.Add("</TR>");

        this.Pub1.Add("</Table>");
    }

    private void My()
    {
        // 我的岗位
        this.Pub1.Add("<Table width='100%' class='TableWork' bgcolor='#F3FFE3'>");
        //this.Pub1.Add("<TR class='TableFlowTR' >");
        //this.Pub1.Add("<TD class='TableFlowTD'colspan=2 ><b>岗责体系</b></TD>");
        //this.Pub1.Add("</TR>");

        BP.Port.Stations sts = WebUser.HisStations;
        foreach (BP.Port.Station st in sts)
        {
            this.Pub1.Add("<TR class='TableFlowTR' >");
            this.Pub1.Add("<TD class='TableFlowTD' width='30%' >" + st.No + "</TD>");
            this.Pub1.Add("<TD class='TableFlowTD' width='70%' >" + st.Name + "</TD>");
            this.Pub1.Add("</TR>");
        }

        BP.Port.Depts depts = WebUser.HisDepts;
        foreach (BP.Port.Dept dept in depts)
        {
            this.Pub1.Add("<TR class='TableFlowTR' >");
            this.Pub1.Add("<TD class='TableFlowTD' width='30%' >" + dept.No + "</TD>");
            this.Pub1.Add("<TD class='TableFlowTD' width='70%' >" + dept.Name + "</TD>");
            this.Pub1.Add("</TR>");
        }

        this.Pub1.Add("</Table>");

    }

    public void Flow()
    {
        // 工作预警
        this.Pub1.Add("<Table width='100%' class='TableWork' bgcolor='#F3FFE3'>");

        //this.Pub1.Add("<TR class='TableFlowTR' >");
        //this.Pub1.Add("<TD title='单击打开"+Web.WebUser.Name+"的工作预警' class='TableFlowTD'colspan=2 onclick=\"javascript:To('Warning.aspx')\"  ><b>工作预警</b></TD>");
        //this.Pub1.Add("</TR>");

        this.Pub1.Add("<TR class='TableFlowTR' >");
        this.Pub1.Add("<TD  class='TableFlowTD' width='70%' onclick=\"javascript:To('Warning.aspx?WorkProgress=" + (int)BP.WF.WorkProgress.Runing + "')\"  ><img src='./Images/W_Runing.ico' border=0 />&nbsp;" + this.ToE("Ordinary", "正常") + "</TD>");
        this.Pub1.Add("<TD class='TableFlowTD' >" + WorkFlow.NumOfRuning(WebUser.No) + "</TD>");
        this.Pub1.Add("</TR>");
        this.Pub1.Add("<TR class='TableFlowTR' >");
        this.Pub1.Add("<TD  class='TableFlowTD' onclick=\"javascript:To('Warning.aspx?WorkProgress=" + (int)BP.WF.WorkProgress.Alert + "')\"  ><img src='./Images/W_Alert.ico' border=0 />&nbsp;" + this.ToE("Warning", "预警") + "</TD>");
        this.Pub1.Add("<TD class='TableFlowTD' >" + WorkFlow.NumOfAlert(WebUser.No) + "</TD>");
        this.Pub1.Add("</TR>");
        this.Pub1.Add("<TR class='TableFlowTR' >");
        this.Pub1.Add("<TD   class='TableFlowTD' onclick=\"javascript:To('Warning.aspx?WorkProgress=" + (int)BP.WF.WorkProgress.Timeout + "')\"  ><img src='./Images/W_Timeout.ico' border=0 />&nbsp;" + this.ToE("OutTime", "逾期") + "</TD>");
        this.Pub1.Add("<TD class='TableFlowTD' >" + WorkFlow.NumOfTimeout(WebUser.No) + "</TD>");
        this.Pub1.Add("</TR>");
        this.Pub1.Add("</Table>");
        //this.Pub1.Add("<Table width='100%' class='TableWork' >");
        //this.Pub1.AddTR();

        //this.Pub1.Add("<TD background='./Images/TdHome_background.bmp' align='center'>序号</TD>");
        //this.Pub1.Add("<TD background='./Images/TdHome_background.bmp' align='center'>工作名称</TD>");
        //this.Pub1.Add("<TD background='./Images/TdHome_background.bmp' align='center'>待办工作个数</TD>");
        //this.Pub1.AddTREnd();
        //获取待办工作个数
        //string sql="SELECT FK_FLOW,  COUNT(*) AS NUM  FROM  wf_generworkerlist WHERE isenable=1 and FK_NODE IN (SELECT FK_Node FROM wf_generworkflow WHERE wf_generworkflow.Workid=wf_generworkerlist.Workid   AND FK_Node<>20206 and fk_flow!='220' ) AND FK_EMP='"+WebUser.No+"' GROUP BY FK_FLOW ";
        //DataTable dt=DBAccess.RunSQLReturnTable(sql);
        //int i=0;
        //foreach(DataRow dr in dt.Rows)
        //{
        //    i++;
        //    this.Pub1.Add("<TR title='单击打开您的待办工作列表' class='TableFlowTR' onclick=\"javascript:window.location.href='WorkList.aspx?FK_Flow="+dr["FK_Flow"].ToString()+"'\" >");
        //    Flow fl = new Flow(dr["FK_Flow"].ToString());
        //    this.Pub1.Add("<TD background='./Images/TdIdx_background.bmp' align='center'>"+i+"</TD> ");
        //    this.Pub1.AddTD(fl.Name );
        //    this.Pub1.AddTDNum( int.Parse(dr["NUM"].ToString())  );
        //    this.Pub1.AddTREnd();
        //}
        //this.Pub1.AddTableEnd();  
    }

    public void DBGZ()
    {
        ////////////////////王思修改,增加管理员工具
        if (WebUser.No == "admin" || WebUser.No == "028888")
        {
            this.Pub1.Add("<Table width='100%' class='TableWork' cellspacing='1' cellpadding='10' bgcolor='#C0DE98'>");

            this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            this.Pub1.Add("<TD align='left'><a href='../Comm/PanelEns.aspx?EnsName=BP.Port.Emps'>" + this.ToE("EmpEdit", "用户维护") + "</a></TD>");
            this.Pub1.AddTREnd();

            //this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            //this.Pub1.Add("<TD align='left'><a href='../Comm/UIEns.aspx?EnsName=BP.Port.Depts'>部 门 维 护</a></TD>");
            //this.Pub1.AddTREnd();

            this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            this.Pub1.Add("<TD align='left'><a href='../Comm/Sys/AdminTools.aspx'>" + this.ToE("AdminTools", "管理员工作列表") + "</a></TD>");
            this.Pub1.AddTREnd();

            //this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            //this.Pub1.Add("<TD align='left'><a href='../Comm/PanelEns.aspx?EnsName=BP.WF.BookTemplates'>文 书 维 护</a></TD>");
            //this.Pub1.AddTREnd();
            //this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            //this.Pub1.Add("<TD align='left'><a href='../Comm/PanelEns.aspx?EnsName=BP.WF.CHOfQLs'>流 程 质 量 考 核 查 询</a></TD>");
            //this.Pub1.AddTREnd();

            //this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            //this.Pub1.Add("<TD align='left'><a href='../Comm/GroupEnsMNum.aspx?EnsName=BP.WF.CHOfQLs'>流 程 质 量 考 核 分 析</a></TD>");
            //this.Pub1.AddTREnd();

            //this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            //this.Pub1.Add("<TD align='left'><a href='/bosi/data/qianshuigonggao.asp'>欠 税 公 告</a></TD>");
            //this.Pub1.AddTREnd();
        }
        else
        {

            // 待办工作
            this.Pub1.Add("<Table width='100%' class='TableWork' cellspacing='1' cellpadding='0' bgcolor='#C0DE98'>");
            //this.Pub1.AddTR();
            this.Pub1.Add("<TR bgcolor='#ffffff' height='20'>");
            this.Pub1.Add("<TD align='center'>ID</TD>");
            this.Pub1.Add("<TD align='center'>" + this.ToE("Name", "名称") + "</TD>");
            this.Pub1.Add("<TD align='center'>" + this.ToE("Num", "个数") + "</TD>");
            this.Pub1.AddTREnd();
            //获取待办工作个数
            string sql = "SELECT FK_FLOW,  COUNT(*) AS NUM  FROM  wf_generworkerlist WHERE isenable=1 and FK_NODE IN (SELECT FK_Node FROM wf_generworkflow WHERE wf_generworkflow.Workid=wf_generworkerlist.Workid   AND FK_Node<>20206 and fk_flow!='220' ) AND FK_EMP='" + WebUser.No + "' GROUP BY FK_FLOW ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                i++;
                this.Pub1.Add("<TR class='TableFlowTR' height='20' bgcolor='#ffffff' onclick=\"javascript:window.location.href='WorkList.aspx?FK_Flow=" + dr["FK_Flow"].ToString() + "'\" >");
                Flow fl = new Flow(dr["FK_Flow"].ToString());
                this.Pub1.Add("<TD align='center' bgcolor='#ffffff'>" + i + "</TD> ");
                this.Pub1.AddTD(fl.Name);
                this.Pub1.AddTD(int.Parse(dr["NUM"].ToString()));
                this.Pub1.AddTREnd();
            }
        }
        this.Pub1.AddTableEnd();
    }

    public void Dot()
    {
        //			this.Pub1.Add("<Table width='80%' class='TableWork' >");
        //			this.Pub1.Add("<TR class='TableFlowTR' >");
        //			this.Pub1.Add("<TD class='TableFlowTD' onclick=\"javascript:To('../ZF/Home.aspx?FK_SBState=1')\" ><img src='../ZF/Images/UnSB.gif' border=0 />&nbsp;未申辩</TD>");
        //			this.Pub1.Add("<TD class='TableFlowTD' >"+BP.ZF1.DDE.NumOfUnSB+"</TD>");
        //			this.Pub1.Add("</TR>");
        //
        //			this.Pub1.Add("<TR class='TableFlowTR' >");
        //			this.Pub1.Add("<TD class='TableFlowTD' onclick=\"javascript:To('../ZF/Home.aspx?FK_SBState=2')\"  ><img src='../ZF/Images/SBList.gif' border=0 />&nbsp;申请中</TD>");
        //			this.Pub1.Add("<TD class='TableFlowTD' >"+BP.ZF1.DDE.NumOfSBList+"</TD>");
        //			this.Pub1.Add("</TR>");
        //
        //
        //			this.Pub1.Add("<TR class='TableFlowTR' >");
        //			this.Pub1.Add("<TD class='TableFlowTD' onclick=\"javascript:To('../ZF/Home.aspx?FK_SBState=3')\"  ><img src='../ZF/Images/SBing.gif' border=0 />&nbsp;申辩中</TD>");
        //			this.Pub1.Add("<TD class='TableFlowTD' >"+BP.ZF1.DDE.NumOfSBing+"</TD>");
        //			this.Pub1.Add("</TR>");
        //
        //			this.Pub1.Add("<TR class='TableFlowTR' >");
        //			this.Pub1.Add("<TD class='TableFlowTD' onclick=\"javascript:To('../ZF/Home.aspx?FK_SBState=4')\"  ><img src='../ZF/Images/SBOver.ico' border=0 />&nbsp;申辩完毕</TD>");
        //			this.Pub1.Add("<TD class='TableFlowTD' >"+BP.ZF1.DDE.NumOfSBOver+"</TD>");
        //			this.Pub1.Add("</TR>");
        //
        //			this.Pub1.Add("<TR class='TableFlowTR' >");
        //			this.Pub1.Add("<TD class='TableFlowTD' onclick=\"javascript:To('../ZF/Home.aspx?FK_SBState=5')\"  ><img src='../ZF/Images/SBUnEnable.gif' border=0 />&nbsp;不允许申辩</TD>");
        //			this.Pub1.Add("<TD class='TableFlowTD' >"+BP.ZF1.DDE.NumOfSBUnEnable+"</TD>");
        //			this.Pub1.Add("</TR>");
        //
        //			this.Pub1.Add("<TR class='TableFlowTR' >");
        //			this.Pub1.Add("<TD class='TableFlowTD' class='ToDayMailTD' onclick=\"javascript:To('../ZF/Home.aspx?FK_SBState=all')\"  >&nbsp;本月合计</TD>");
        //			this.Pub1.Add("<TD class='TableFlowTD' >"+BP.ZF1.DDE.NumOfSum+"</TD>");
        //			this.Pub1.Add("</TR>");
        //
        //			this.Pub1.Add("</Table>");

    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion
}
