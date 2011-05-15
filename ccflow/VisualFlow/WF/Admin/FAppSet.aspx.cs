using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_FApp : WebPage
{
    public int NodeID
    {
        get
        {
            return int.Parse(this.Request.QueryString["NodeID"]);
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public void DoNew(FAppSet Bill)
    {
        this.Ucsys1.Clear();

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaption("<a href='FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "' >" + nd.Name + "</a> - <img src='../../Images/Btn/New.gif' />" + this.ToE("New", "新建") + " - " + BP.WF.Glo.GenerHelp("FAppSet"));

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle(this.ToE("Item", "项目"));
        this.Ucsys1.AddTDTitle( this.ToE("Input","采集") );
        this.Ucsys1.AddTDTitle(this.ToE("Desc","描述"));
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("InvoType", "调用类型"));

        DDL ddl = new DDL();
        ddl.ID = "DDL_AppType";
        ddl.BindSysEnum("AppType");
        ddl.SetSelectItem(Bill.AppType);

        this.Ucsys1.AddTD(ddl);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        //this.Ucsys1.AddTR();
        //this.Ucsys1.AddTD(this.ToE("InvoWhen", "调用时间"));
        //ddl = new DDL();
        //ddl.ID = "DDL_ShowTime";
        //ddl.BindSysEnum(FAppSetAttr.ShowTime);
        //ddl.SetSelectItem(Bill.ShowTime);
        //this.Ucsys1.AddTD(ddl);
        //this.Ucsys1.AddTD("");
        //this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("Label","标签"));
        TB tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = Bill.Name;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD(this.ToE("ShowName","显示名称") ); // 显示名称
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("DoWhat","执行内容")); //"执行内容"
        tb = new TB();
        tb.ID = "TB_DoWhat";
        tb.Text = Bill.DoWhat;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD class=TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " "+this.ToE("Save","保存")+" ";
        this.Ucsys1.Add(btn);
        btn.Click += new EventHandler(btn_Click);
        this.Ucsys1.Add(btn);
        if (Bill.OID > 1)
        {
            btn = new Button();
            btn.ID = "Btn_Del";
            btn.Text = " " + this.ToE("Del", "删除") + " ";
            this.Ucsys1.Add(btn);
            btn.Attributes["onclick"] += " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";

            btn.Click += new EventHandler(btn_Del_Click);
        }

        this.Ucsys1.Add("</TD>");
        this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTR1();
        //string msg = "设置帮助：关于如何传递参数请打开帮助。<hr>";
        //msg += "<li>如果您选择<b>“外部Url连接”</b>，执行内容请按照http:// 格式填写，比如：http://ccFlow.org/demo.aspx  <br> 参数格式为：http://ccFlow.org/demo.aspx?WorkID=1234&UserNo=zp,安全问题，用户接受参数与工作ID后自己处理。</li>";
        //msg += "<li>如果您选择<b>“本地的可执行文件”</b>，执行内容请按照windows路径格式填写，比如：C:\\\\AppPath\\\\AppName.exe.<br>参数格式为：C:\\\\AppPath\\\\AppName.exe @WorkID=123@UserNo=zp</li>";
        this.Ucsys1.AddTDBigDoc("colspan=3 class=BigDoc", "<a href='./Help/index.htm' target=_b><img src='../../Images/Btn/Help.gif' border=0/>" + this.ToE("Help", "帮助") + "</a>");
        this.Ucsys1.AddTREnd();

        //this.Ucsys1.AddTRSum();
        //this.Ucsys1.AddTD("colspan=3", "在当前节点上要执行的外部程序。比如：在此节点点调用sina.");
        //this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTable();
    }

    void btn_Click(object sender, EventArgs e)
    {
        FAppSet bt = new FAppSet();
        if (this.RefOID != 0)
        {
            bt.OID = this.RefOID;
            bt.Retrieve();
        }

        bt = this.Ucsys1.Copy(bt) as FAppSet;
        bt.NodeID = this.NodeID;
        if (this.RefOID == 0)
            bt.Insert();
        else
            bt.Update();
        this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        FAppSet t = new FAppSet();
        t.OID = this.RefOID;
        t.Delete();
        this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "New":
                FAppSet bk = new FAppSet();
                bk.NodeID = this.RefOID;
                this.DoNew(bk);
                return;
            case "Edit":
                FAppSet bk1 = new FAppSet(this.RefOID);
                bk1.NodeID = this.NodeID;
                this.DoNew(bk1);
                return;
            default:
                break;
        }

        FAppSets Bills = new FAppSets(this.NodeID);
        if (Bills.Count == 0)
        {
            this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New", true);
            return;
        }

        if (Bills.Count ==1)
        {
            //this.Response.Redirect("FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefOID="+Bills[0].GetValByKey("OID"), true);
           // return;
        }

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        this.Title = nd.Name + " - " + this.ToE("FAppSet", "调用外部程序接口");
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaption(nd.Name + " - <a href='FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New'><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("New", "新建") + "</a>  - " + BP.WF.Glo.GenerHelp("FAppSet" ));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("ID");
        this.Ucsys1.AddTDTitle( this.ToE("Node","节点") );
        this.Ucsys1.AddTDTitle(this.ToE("ShowName","显示名称"));
        this.Ucsys1.AddTDTitle(this.ToE("InvoType", "调用类型"));
       // this.Ucsys1.AddTDTitle(this.ToE("InvoWhen", "调用时间"));
        this.Ucsys1.AddTDTitle(this.ToE("DoWhat", "执行内容"));
        this.Ucsys1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Ucsys1.AddTREnd();
        int i = 0;
        foreach (FAppSet Bill in Bills)
        {
            i++;
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(i);
            this.Ucsys1.AddTD(nd.Name );
            this.Ucsys1.AddTD(Bill.Name);

            this.Ucsys1.AddTD(Bill.AppTypeT);
            this.Ucsys1.AddTD(Bill.DoWhat);

            this.Ucsys1.AddTD("<a href='FAppSet.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefOID=" + Bill.OID + "'><img src='../../Images/Btn/Edit.gif' border=0/>"+this.ToE("Edit","编辑")+"</a>");
            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEnd();
    }


}
