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
using BP.Web.Controls;

public partial class WF_Admin_Exp : System.Web.UI.Page
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.WF.Exp exp = new BP.WF.Exp(this.FK_Flow);
        BindBase(exp);
        switch (this.DoType)
        {
            case "Base":
                
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void BindBase(BP.WF.Exp exp)
    {
       
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeft("<img src='../../Images/Btn/Do.gif' />流程数据导出 " + BP.WF.Glo.GenerHelp("Exp"));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("项目");
        this.Ucsys1.AddTDTitle("输入");
        this.Ucsys1.AddTDTitle("描述");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD("调度发生时间");
        DDL ddl = new DDL();
        ddl.ID = "DDL_DTSWhen";
        ddl.BindSysEnum("DTSWhen");
        ddl.SetSelectItem(exp.DTSWhen);
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);

        this.Ucsys1.AddTD(ddl);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD("数据源");
        ddl = new DDL();
        ddl.ID = "DDL_DLink";
        ddl.BindSysEnum("DLink");
        ddl.SetSelectItem( (int)exp.DLink);
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);

        this.Ucsys1.AddTD(ddl);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD("数据表");
        ddl = new DDL();
        ddl.ID = "DDL_RefTable";
        DataTable dt = new DataTable();
        string sql="";
        switch (exp.DLink)
        {
            case BP.WF.DLink.AppCenterDSN:
                sql = "select tname from tab WHERE tname not like 'WF%' AND tname not like 'SYS_%' AND tname not like 'ND%' AND tname not like 'CN%' AND tname not like 'PORT_%' AND tname not like 'PUB_%' AND tname not like 'V_%'";
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                break;
            case BP.WF.DLink.ODBC:
                sql = "select tname from tab WHERE tname not like 'WF%' AND tname not like 'Sys_%' AND tname not like 'ND%' AND tname not like 'CN%' AND tname not like 'Port_%'";
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                break;
            default:
                sql = "select tname from tab WHERE tname not like 'WF%' AND tname not like 'Sys_%' AND tname not like 'ND%' AND tname not like 'CN%' AND tname not like 'Port_%'";
                dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                break;
        }

        foreach (DataRow dr in dt.Rows)
        {
            ddl.Items.Add(new ListItem(dr[0].ToString() ,dr[0].ToString() ));
        }

     // ddl.BindAppCheckType

        
        // ddl.BindSysEnum(FAppSetAttr.ShowTime);
        ddl.SetSelectItem(exp.RefTable);


        this.Ucsys1.AddTD(ddl);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD("是否起用");
        CheckBox cb = new CheckBox();
        cb.ID = "CB_IsEnable";
        cb.Text = "是否起用";
        this.Ucsys1.AddTD(cb);
        this.Ucsys1.AddTD("点启用后，此定义才能生效。");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD("说明");
        TB tb = new TB();
        tb.ID = "TB_ExpDesc";
        tb.Text = exp.ExpDesc;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD class=TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " 保存 ";
        this.Ucsys1.Add(btn);
        btn.Click += new EventHandler(btn_Click);

        btn = new Button();
        btn.ID = "Btn_SaveMap";
        btn.Text = "保存并设置字段映射";
        this.Ucsys1.Add(btn);
        btn.Click += new EventHandler(btn_Click);

        this.Ucsys1.Add("</TD>");
        this.Ucsys1.AddTREnd();


        this.Ucsys1.AddTRSum();
        this.Ucsys1.AddTDBigDoc("colspan=3 class=BigDoc", "提示：关于如何传递参数请打开帮助。");
        this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTable();
    }

    void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
         
    }

    void btn_Click(object sender, EventArgs e)
    {
         
    }
}
