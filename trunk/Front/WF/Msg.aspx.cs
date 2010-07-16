﻿using System;
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


public partial class WF_Msg : WebPage
{
    public int MsgSta
    {
        get
        {
            string msg = this.Request.QueryString["Sta"];
            if (msg == null || msg == "1")
                return 1;
            else
                return 0;
        }
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.Title = "系统消息";
        switch (this.DoType)
        {
            case "Del":
                BP.Sys.Msg msg = new BP.Sys.Msg();
                msg.OID = this.RefOID;
                msg.Retrieve();
                if (msg.Accepter == WebUser.No)
                {
                    msg.Delete();
                }
                break;
            default:
                break;
        }
        this.Bind();
    }
    public void Bind()
    {
        int colspan = 5;
        BP.Sys.Msgs ens = new BP.Sys.Msgs();
        if (this.MsgSta == 9)
            ens.Retrieve(BP.Sys.MsgAttr.Sender, WebUser.No);
        else
            ens.Retrieve(BP.Sys.MsgAttr.Accepter, WebUser.No, BP.Sys.MsgAttr.MsgSta, this.MsgSta);


        this.Pub1.AddTable("width='90%'");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.Add("<TD  class=TitleMsg colspan=" + colspan + "><div align=right><a href='Msg.aspx?Sta=0'>未读</a>|<a href='Msg.aspx?Sta=1'>已读</a>|<a href='Msg.aspx?Sta=9'>已发送</a>|<a href=\"javascript:WinOpen('./Msg/Write.aspx')\" >编写</a></div></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("");
        this.Pub1.AddTDTitle("");
        this.Pub1.AddTDTitle("标题");
        this.Pub1.AddTDTitle("发送人");
        this.Pub1.AddTDTitle("发送日期");
        this.Pub1.AddTREnd();
        int i = 0;
        bool is1 = false;
        foreach (BP.Sys.Msg en in ens)
        {
            i++;
            is1 = this.Pub1.AddTR(is1);
            CheckBox cb = new CheckBox();
            cb.ID = "CB_" + en.OID;
            this.Pub1.AddTDIdx(i);
            this.Pub1.AddTD(cb);
            this.Pub1.AddTDA("javascript:WinOpen('./Msg/Read.aspx?RefOID=" + en.OID + "','sd');", en.Title);
            this.Pub1.AddTD(en.SenderText);
            this.Pub1.AddTD(en.RDT);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
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



