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
using System.Text;
using BP.GE.Ctrl;
using BP.En;
using BP.Web;
using BP.GE;

public partial class GE_Message_Message : WebPage
{
    protected override void OnInit(EventArgs e)
    {

    }
    string strWidth = "719px";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(BP.Web.WebUser.No) || string.IsNullOrEmpty(BP.Web.WebUser.Name))
        {
            BP.GE.GeFun.ShowMessage(this.Page, "JS1", "对不起请登录!");
            return;
        }

        if (Request.QueryString["op"] != null)
        {
            CUser.Value = BP.Web.WebUser.Name + "<" + BP.Web.WebUser.No + ">";
            switch (Request.QueryString["op"].ToString())
            {

                case "Write":
                    AddEditor();
                    break;
                case "f":
                    NewFriend();
                    break;
                case "s":
                    AddEditor();
                    break;
                case "l":
                    AddLinkman();
                    break;
                case "m":
                    ModifyLinkman();
                    break;
                case "Receive":
                    ReceiveMsg();
                    break;
                case "Draft":
                    ShowDraft();
                    break;
                case "Recycle":
                    ShowRecycle();
                    break;
                case "Outbox":
                    ShowOutbox();
                    break;
                case "DetailReceive":
                    ShowMailDetail("Receive");
                    break;
                case "InboxDetailReceive":
                    ShowMailDetail("InboxReceive");
                    break;
                case "DetailDraft":
                    ShowMailDetail("Draft");
                    break;
                case "DetailSend":
                    ShowMailDetail("Send");
                    break;
                case "DetailRecycle":
                    ShowMailDetail("Recycle");
                    break;
                case "UnRead":
                    ShowInBoxWithUnRead();
                    break;
            }
        }
        else
        {
            ShowInBoxWithUnRead();
        }
    }

    private void ShowInBoxWithUnRead()
    {
        BP.GE.GeMessages ens = new GeMessages();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(GeMessageAttr.Receiver, BP.Web.WebUser.No);
        qo.addAnd();
        qo.AddWhere(GeMessageAttr.ReadSta, 0);
        qo.addAnd();
        qo.AddWhere(GeMessageAttr.StaR, 0);
        qo.addOrderByDesc(GeMessageAttr.SendDT);
        this.Pub2.BindPageIdx(qo.GetCount(), BP.SystemConfig.PageSize, this.PageIdx, "Message.aspx?op=UnRead");
        qo.DoQuery("OID", BP.SystemConfig.PageSize, this.PageIdx);
        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width=2%", "<input type='checkbox' onclick='selectAll(this)' />");
        this.Pub1.AddTDTitle("width=3%", "状态");
        this.Pub1.AddTDTitle("width=5%", "发件人");
        this.Pub1.AddTDTitle("标题");
        this.Pub1.AddTDTitle("width=5%", "发件日期");
        this.Pub1.AddTR();

        foreach (GeMessage en in ens)
        {
            this.Pub1.AddTR();
            string strID = en.OID.ToString();
            this.Pub1.AddTR("ondblclick='mydblClick(\"" + strID + "\",\"InboxDetailReceive\")' onmouseover='myOver(this)' onmouseout='myOut(this)'");
            string strHTML = "<input type='checkbox' id='" + strID + ";' name='chklinkman' onclick='Chk_Click(this)' />";
            this.Pub1.AddTD(strHTML);
            if (en.ReadSta == 0)
            {
                this.Pub1.AddTD("<img src='Img\\unRd2.gif' alt='未读' />");
            }
            else if (en.ReadSta == 1)
            {
                this.Pub1.AddTD("<img src='Img\\email_open.gif' alt='已读' />");
            }
            else if (en.ReadSta == 2)
            {
                this.Pub1.AddTD("<img src='Img\\sms_send.gif' alt='已回复' />");
            }
            this.Pub1.AddTD(en.SenderT);
            this.Pub1.AddTD(string.Format("<a href='#'onclick='mydblClick(\"" + strID + "\",\"InboxDetailReceive\")'>{0}</a>", en.Title));
            this.Pub1.AddTD(en.SendDT);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();
        this.Pub2.Add("<input type='button' value='返回' id='btnCancel' onclick='history.go(-1)' />");
        Button btnOP = new Button();
        btnOP.Text = "删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "MailDel";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "彻底删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "MailDirDel";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }

    private void ShowMailDetail(string op)
    {
        BP.GE.GeMessage message = new GeMessage();
        message.OID = Convert.ToInt32(Request.QueryString["OID"]);
        message.Retrieve();
        if (op == "InboxReceive")
        {
            message.ReadSta = 1;
            message.Update();
        }
        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        this.Pub1.AddTD("colspan='2' class='TDMailTitle'", "<B>" + message.Title + "</B>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("width=10%", "发件人");
        this.Pub1.AddTD(message.SenderT + "<" + message.Sender + ">");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("时间");
        this.Pub1.AddTD(message.SendDT);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("收件人");
        this.Pub1.AddTD(message.ReceiverT + "<" + message.Receiver + ">");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("colspan='2' class='TDMailDoc'", message.Doc);
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();

        if (op == "Send")
        {
            AddButtonSend(message);
        }
        else if (op == "Draft")
        {
            AddButtonDraft(message);
        }
        else
        {
            AddButton1(message);
        }

    }

    private void AddButtonDraft(BP.GE.GeMessage message)
    {
        this.Pub2.Add("<input type='button' value='返回' onclick='history.back(1);'>");
        Button btnOP = new Button();
        btnOP.Text = "发送";
        btnOP.CommandName = "Transpond";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "DraftDetailDel";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }

    private void AddButton1(BP.GE.GeMessage message)
    {
        this.Pub2.Add("<input type='button' value='返回' onclick='history.back(1);'>");
        Button btnOP = new Button();
        btnOP.Text = "回复";
        btnOP.CommandName = "Replay";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "转发";
        btnOP.CommandName = "Transpond";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "DetailDel";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "彻底删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "DetailDirDel";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }
    private void AddButtonSend(BP.GE.GeMessage message)
    {
        this.Pub2.Add("<input type='button' value='返回' onclick='history.back(1);'>");
        Button btnOP = new Button();
        btnOP.Text = "转发";
        btnOP.CommandName = "Transpond";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "OutBoxDetaiDel";
        btnOP.CommandArgument = message.OID.ToString();
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }
    /// <summary>
    /// 查看发件箱
    /// </summary>
    private void ShowOutbox()
    {
        string appPath = this.Request.ApplicationPath;
        BP.GE.GeMessages ens = new GeMessages();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(GeMessageAttr.Sender, BP.Web.WebUser.No);
        qo.addAnd();
        qo.AddWhere(GeMessageAttr.StaS, 0);
        qo.addOrderByDesc(GeMessageAttr.SendDT);
        int num = qo.GetCount();
        this.Pub2.BindPageIdx(num, BP.SystemConfig.PageSize, this.PageIdx, "Message.aspx?op=Outbox");
        qo.DoQuery("OID", BP.SystemConfig.PageSize, this.PageIdx);
        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width=2%", "<input type='checkbox' onclick='selectAll(this)' />");
        this.Pub1.AddTDTitle("width=3%", "状态");
        this.Pub1.AddTDTitle("width=20%", "收件人");
        this.Pub1.AddTDTitle("标题");
        this.Pub1.AddTDTitle("width=5%", "发件日期");
        this.Pub1.AddTR();
        foreach (GeMessage en in ens)
        {
            this.Pub1.AddTR();
            string strID = en.OID.ToString();
            this.Pub1.AddTR("ondblclick='mydblClick(\"" + strID + "\",\"DetailSend\")' onmouseover='myOver(this)' onmouseout='myOut(this)'");
            string strHTML = "<input type='checkbox' id='" + strID + ";' name='chklinkman' onclick='Chk_Click(this)' />";
            this.Pub1.AddTD(strHTML);
            if (en.ReadSta == 0)
            {
                this.Pub1.AddTD("<img src='Img\\unRd2.gif' alt='未读' />");
            }
            else if (en.ReadSta == 1)
            {
                this.Pub1.AddTD("<img src='Img\\email_open.gif' alt='已读' />");
            }
            else if (en.ReadSta == 2)
            {
                this.Pub1.AddTD("<img src='Img\\sms_send.gif' alt='已回复' />");
            }
            this.Pub1.AddTD(en.Receiver);
            this.Pub1.AddTD(string.Format("<a href='#'onclick='mydblClick(\"" + strID + "\",\"DetailSend\")'>{0}</a>", en.Title));
            this.Pub1.AddTD(en.SendDT);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();
        this.Pub2.AddBR();
        Button btnOP = new Button();
        btnOP.Text = "删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "OutBoxDel";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }
    /// <summary>
    /// 查看垃圾箱
    /// </summary>
    private void ShowRecycle()
    {
        string appPath = this.Request.ApplicationPath;

        BP.GE.GeMessages ens = new GeMessages();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(GeMessageAttr.Receiver, BP.Web.WebUser.No);
        qo.addAnd();
        qo.AddWhere(GeMessageAttr.StaR, 2);
        qo.addOrderByDesc(GeMessageAttr.SendDT);
        int num = qo.GetCount();
        this.Pub2.BindPageIdx(num, BP.SystemConfig.PageSize, this.PageIdx, "Message.aspx?op=Receive");
        qo.DoQuery("OID", BP.SystemConfig.PageSize, this.PageIdx);
        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width=2%", "<input type='checkbox' onclick='selectAll(this)' />");
        this.Pub1.AddTDTitle("width=3%", "状态");
        this.Pub1.AddTDTitle("width=20%", "收件人");
        this.Pub1.AddTDTitle("标题");
        this.Pub1.AddTDTitle("width=5%", "发件日期");
        this.Pub1.AddTR();

        foreach (GeMessage en in ens)
        {
            this.Pub1.AddTR();

            string strID = en.OID.ToString();
            this.Pub1.AddTR("ondblclick='mydblClick(\"" + strID + "\",\"DetailRecycle\")' onmouseover='myOver(this)' onmouseout='myOut(this)'");
            string strHTML = "<input type='checkbox' id='" + strID + ";' name='chklinkman' onclick='Chk_Click(this)' />";

            this.Pub1.AddTD(strHTML);
            if (en.ReadSta == 0)
            {
                this.Pub1.AddTD("<img src='Img\\unRd2.gif' alt='未读' />");
            }
            else if (en.ReadSta == 1)
            {
                this.Pub1.AddTD("<img src='Img\\email_open.gif' alt='已读' />");
            }
            else if (en.ReadSta == 2)
            {
                this.Pub1.AddTD("<img src='Img\\sms_send.gif' alt='已回复' />");
            }
            this.Pub1.AddTD(en.Receiver);
            this.Pub1.AddTD(string.Format("<a href='#'onclick='mydblClick(\"" + strID + "\",\"DetailRecycle\")'>{0}</a>", en.Title));
            this.Pub1.AddTD(en.SendDT);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();

        Button btnOP = new Button();
        btnOP.Text = "还原";
        btnOP.CommandName = "RecycleUndo";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "RecycleDel";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.Text = "清空";
        btnOP.CommandName = "RecycleDelAll";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }

    /// <summary>
    /// 草稿箱
    /// </summary>
    private void ShowDraft()
    {
        string appPath = this.Request.ApplicationPath;

        BP.GE.GeMessages ens = new GeMessages();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(GeMessageAttr.Sender, BP.Web.WebUser.No);
        qo.addAnd();
        qo.AddWhere(GeMessageAttr.StaS, 1);
        qo.addOrderByDesc(GeMessageAttr.SendDT);

        int num = qo.GetCount();
        this.Pub2.BindPageIdx(num, 10, this.PageIdx, "Message.aspx?op=Receive");

        qo.DoQuery("OID", BP.SystemConfig.PageSize, this.PageIdx);

        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width=2%", "<input type='checkbox' onclick='selectAll(this)' />");
        this.Pub1.AddTDTitle("width=3%", "状态");
        this.Pub1.AddTDTitle("width=20%", "收件人");
        this.Pub1.AddTDTitle("标题");
        this.Pub1.AddTDTitle("width=5%", "发件日期");
        this.Pub1.AddTR();

        foreach (GeMessage en in ens)
        {
            this.Pub1.AddTR();
            string strID = en.OID.ToString();
            this.Pub1.AddTR("ondblclick='mydblClick(\"" + strID + "\",\"DetailDraft\")' onmouseover='myOver(this)' onmouseout='myOut(this)'");
            string strHTML = "<input type='checkbox' id='" + strID + ";' name='chklinkman' onclick='Chk_Click(this)' />";
            this.Pub1.AddTD(strHTML);
            this.Pub1.AddTD("<img src='Img/mDf2.gif' border=0 />");
            this.Pub1.AddTD(en.Receiver);
            this.Pub1.AddTD(string.Format("<a href='#'onclick='mydblClick(\"" + strID + "\",\"DetailDraft\")'>{0}</a>", en.Title));
            this.Pub1.AddTD(en.SendDT);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();

        Button btnOP = new Button();
        btnOP.Text = "删除草稿";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "DraftDel";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }
    /// <summary>
    /// 我的收件箱
    /// </summary>
    private void ReceiveMsg()
    {
        string appPath = this.Request.ApplicationPath;

        BP.GE.GeMessages ens = new GeMessages();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(GeMessageAttr.Receiver, BP.Web.WebUser.No);
        qo.addAnd();
        qo.AddWhere(GeMessageAttr.StaR, 0);
        qo.addOrderByDesc(GeMessageAttr.SendDT);
        this.Pub2.BindPageIdx(qo.GetCount(), BP.SystemConfig.PageSize, this.PageIdx, "Message.aspx?op=Receive");
        qo.DoQuery("OID", BP.SystemConfig.PageSize, this.PageIdx);
        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width=2%", "<input type='checkbox' onclick='selectAll(this)' />");
        this.Pub1.AddTDTitle("width=3%", "状态");
        this.Pub1.AddTDTitle("width=5%", "发件人");
        this.Pub1.AddTDTitle("标题");
        this.Pub1.AddTDTitle("width=5%", "发件日期");
        this.Pub1.AddTR();

        foreach (GeMessage en in ens)
        {
            this.Pub1.AddTR();

            string strID = en.OID.ToString();
            this.Pub1.AddTR("ondblclick='mydblClick(\"" + strID + "\",\"InboxDetailReceive\")' onmouseover='myOver(this)' onmouseout='myOut(this)'");
            string strHTML = "<input type='checkbox' id='" + strID + ";' name='chklinkman' onclick='Chk_Click(this)' />";
            this.Pub1.AddTD(strHTML);
            if (en.ReadSta == 0)
            {
                this.Pub1.AddTD("<img src='Img\\unRd2.gif' alt='未读' />");
            }
            else if (en.ReadSta == 1)
            {
                this.Pub1.AddTD("<img src='Img\\email_open.gif' alt='已读' />");
            }
            else if (en.ReadSta == 2)
            {
                this.Pub1.AddTD("<img src='Img\\sms_send.gif' alt='已回复' />");
            }
            this.Pub1.AddTD(en.SenderT);
            this.Pub1.AddTD(string.Format("<a href='#'onclick='mydblClick(\"" + strID + "\",\"InboxDetailReceive\")'>{0}</a>", en.Title));
            this.Pub1.AddTD(en.SendDT);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();

        this.Pub2.AddBR();
        this.Pub2.Add("<input type='button' value='返回' id='btnCancel' onclick='history.go(-1)' />");
        Button btnOP = new Button();
        btnOP.Text = "删除";
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.CommandName = "MailDel";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
        btnOP = new Button();
        btnOP.OnClientClick = "return DelConfirm()";
        btnOP.Text = "彻底删除";
        btnOP.CommandName = "MailDirDel";
        btnOP.Command += new CommandEventHandler(btnOP_Command);
        this.Pub2.Add(btnOP);
    }

    void btnOP_Command(object sender, CommandEventArgs e)
    {
        string[] strOIDs = linkman.Value.Split(';');
        string strSql = string.Empty;
        switch (e.CommandName)
        {
            //从我的草稿箱中删除邮件
            case "DraftDel":
                for (int i = 0; i < strOIDs.Length - 1; i++)
                {
                    strSql = "DELETE FROM GE_Message WHERE OID=" + strOIDs[i];
                    BP.DA.DBAccess.RunSQL(strSql);
                }
                Response.Redirect("Message.aspx?op=Draft");
                break;
            //从我的草稿箱的详细页中删除邮件
            case "DraftDetailDel":
                strSql = "DELETE FROM GE_Message WHERE OID=" + e.CommandArgument.ToString();
                BP.DA.DBAccess.RunSQL(strSql);
                Response.Redirect("Message.aspx?op=Draft");
                break;
            //从我的收件箱中删除邮件
            case "MailDel":
                for (int i = 0; i < strOIDs.Length - 1; i++)
                {
                    strSql = "UPDATE GE_Message SET StaR = 2 WHERE OID=" + strOIDs[i];
                    BP.DA.DBAccess.RunSQL(strSql);
                }
                Response.Redirect("Message.aspx?op=UnRead");
                break;
            //从我的发件箱中删除邮件
            case "OutBoxDel":
                for (int i = 0; i < strOIDs.Length - 1; i++)
                {
                    strSql = "SELECT StaR FROM GE_Message WHERE OID=" + strOIDs[i];
                    int j = BP.DA.DBAccess.RunSQLReturnValInt(strSql);
                    if (j == 3)
                    {
                        strSql = "DELETE FROM GE_Message WHERE OID=" + strOIDs[i];
                    }
                    else
                    {
                        strSql = "UPDATE GE_Message SET StaS = 3 WHERE OID=" + strOIDs[i];
                    }
                    BP.DA.DBAccess.RunSQL(strSql);
                }
                Response.Redirect("Message.aspx?op=Outbox");
                break;
            //从我的收件箱中彻底删除邮件
            case "MailDirDel":
                for (int i = 0; i < strOIDs.Length - 1; i++)
                {
                    strSql = "SELECT StaS FROM GE_Message WHERE OID=" + strOIDs[i];
                    int j = BP.DA.DBAccess.RunSQLReturnValInt(strSql);
                    if (j == 3)
                    {
                        strSql = "DELETE FROM GE_Message WHERE OID=" + strOIDs[i];
                    }
                    else
                    {
                        strSql = "UPDATE GE_Message SET StaR = 3 WHERE OID=" + strOIDs[i];
                    }
                    BP.DA.DBAccess.RunSQL(strSql);
                }
                Response.Redirect("Message.aspx?op=Receive");
                break;
            //从我的发件箱中删除全部邮件
            case "OutBoxDelAll":
                break;
            //从我的垃圾箱中删除邮件
            case "RecycleDel":
                for (int i = 0; i < strOIDs.Length - 1; i++)
                {
                    strSql = "SELECT StaS FROM GE_Message WHERE OID=" + strOIDs[i];
                    int j = BP.DA.DBAccess.RunSQLReturnValInt(strSql);
                    if (j == 3)
                    {
                        strSql = "DELETE FROM GE_Message WHERE OID=" + strOIDs[i];
                    }
                    else
                    {
                        strSql = "UPDATE GE_Message SET StaR = 3 WHERE OID=" + strOIDs[i];
                    }
                    BP.DA.DBAccess.RunSQL(strSql);
                }
                Response.Redirect("Message.aspx?op=Recycle");
                break;
            //从邮件详细页删除邮件到回收站.
            case "DetailDel":
                strSql = "UPDATE GE_Message SET StaR = 2 WHERE OID=" + e.CommandArgument;
                BP.DA.DBAccess.RunSQL(strSql);
                Response.Redirect("Message.aspx?op=Receive");
                break;
            //清空回收站.
            case "RecycleDelAll":
                strSql = "DELETE FROM GE_Message WHERE Receiver='" + BP.Web.WebUser.No + "'" + " and StaR=2";
                BP.DA.DBAccess.RunSQL(strSql);
                Response.Redirect("Message.aspx?op=Recycle");
                break;
            //从回收站中还原
            case "RecycleUndo":
                for (int i = 0; i < strOIDs.Length - 1; i++)
                {
                    strSql = "UPDATE GE_Message SET StaR = 0 WHERE OID=" + strOIDs[i];
                    BP.DA.DBAccess.RunSQL(strSql);
                }
                Response.Redirect("Message.aspx?op=Recycle");
                break;
            //回复
            case "Replay":
                GeMessage message = new GeMessage();
                message.OID = Convert.ToInt32(e.CommandArgument);
                message.Retrieve();
                AddEditor(message, e.CommandName);
                break;
            //转发
            case "Transpond":
                message = new GeMessage();
                message.OID = Convert.ToInt32(e.CommandArgument);
                message.Retrieve();
                AddEditor(message, e.CommandName);
                break;
            case "OutBoxDetaiDel":
                strSql = "SELECT StaR FROM GE_Message WHERE OID=" + e.CommandArgument.ToString();
                if (BP.DA.DBAccess.RunSQLReturnValInt(strSql) == 3)
                {
                    strSql = "DELETE FROM GE_Message WHERE OID=" + e.CommandArgument.ToString();
                }
                else
                {
                    strSql = "UPDATE GE_Message SET StaS = 3 WHERE OID=" + e.CommandArgument.ToString();
                }
                BP.DA.DBAccess.RunSQL(strSql);
                Response.Redirect("Message.aspx?op=Outbox");
                break;
        }
    }
    void btn_Click(object sender, EventArgs e)
    {
        string[] strOIDs = linkman.Value.Split(';');
        string strResult = string.Empty;
        for (int i = 0; i < strOIDs.Length - 1; i++)
        {
            string strSql = "UPDATE GE_Inbox SET OPSta = 2 WHERE oid= " + Convert.ToString(strOIDs[i]);
            BP.DA.DBAccess.RunSQL(strSql);
        }
        ReceiveMsg();
    }

    /// <summary>
    /// 生成发送邮件界面
    /// </summary>
    private void AddEditor()
    {
        string strStyle = "width:622px";
        this.Pub1.Controls.Clear();
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTD("width='8%'", "收件人");
        string strHTML = "<input type='text' id='txtReceiver' name='txtReceiver' readonly=readonly style='" + strStyle +
                           "' value='" + linkman.Value + "'check='^\\S+$' warning='请选择用户' onclick='openDiv(this)'/>";
        this.Pub1.AddTD("width='649px'", strHTML);
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();
        this.Pub1.AddTD("主题");
        strHTML = "<input type='text' id='txtTitle' name='txtTitle' style='" + strStyle + "' check='^\\S+$' warning='主题不能为空,并且不能有空格!' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();
        this.Pub1.AddTD("正文");
        strHTML = "<iframe ID='eWebEditor1' src='Edit/editor.htm?id=txtContent&style=coolblue' ";
        strHTML += "frameborder='0' name='eWebEditor1' scrolling='no' width='600' HEIGHT='350'></iframe>";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("colspan='2'");
        this.Pub1.Add("<input type='button' value='发送' id='btnSend' onclick='doAsyncPost(this)' />");
        this.Pub1.Add("<input type='button' value='存草稿' id='btnDraft' onclick='doAsyncPost(this)' />");
        this.Pub1.Add("<input type='button' value='返回' id='btnCancel' onclick='history.go(-1)' />");
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        addEmpToDiv();
    }
    private void addEmpToDiv()
    {
        StringBuilder sbHTML = new StringBuilder();
        sbHTML.Append("<div id='divEmps' style='display:none'>" + "\n");
        sbHTML.Append("<div id='divTitle'><span id='spanTitle'>我的联系人</span>");
        sbHTML.Append("<span id='spanClose' onclick='closeDiv()' onmouseover='myOver(this)' onmouseout='myOut(this)'>[关闭]</span></div>");
        GeFriends ens = new GeFriends();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(GeFriendAttr.FK_Emp1, BP.Web.WebUser.No);
        qo.DoQuery();
        sbHTML.Append("<table style='width:100%' class='tblEmp'>");
        foreach (GeFriend en in ens)
        {
            string strEmp1 = en.Name + "&lt;" + en.FK_Emp2 + "&gt;";
            string strEmp2 = en.Name + "<" + en.FK_Emp2 + ">;";
            sbHTML.Append("<tr style='width:100%;cursor:hand;' onmouseover='myOver(this)' onmouseout='myOut(this)' onclick='SelectEmp(this,\"" + strEmp2 + "\")'>");
            sbHTML.Append("<td><span>●&nbsp;</span>" + strEmp1 + "\n");
            sbHTML.Append("</td>");
            sbHTML.Append("</tr>");
        }
        sbHTML.Append("</table>");
        sbHTML.Append("</div>" + "\n");
        this.Pub1.Add(sbHTML.ToString());
    }
    /// <summary>
    /// 生成发送邮件界面
    /// </summary>
    private void AddEditor(BP.GE.GeMessage message, string strOP)
    {
        this.Pub1.Controls.Clear();
        this.Pub2.Controls.Clear();
        string strEmps = string.Empty;
        if (strOP == "Replay")
        {
            strEmps = message.SenderT + "<" + message.Sender + ">;";
        }
        string strStyle = "width:622px";
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTD("width='8%'", "收件人");
        linkman.Value = strEmps;
        string strHTML = "<input type='text' id='txtReceiver' name='txtReceiver' readonly=readonly style='" + strStyle +
                           "' value='" + linkman.Value + "'check='^\\S+$' warning='请选择用户' onclick='openDiv(this)'/>";
        this.Pub1.AddTD("width='65%'", strHTML);
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();
        this.Pub1.AddTD("主题");
        strHTML = "<input type='text' id='txtTitle' name='txtTitle' style='" +
                    strStyle + "' check='^\\S+$' warning='请输入主题!' value='回复:" + message.Title + "'/>";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("正文");
        strHTML = "<iframe ID='eWebEditor1' src='Edit/editor.htm?id=txtContent&style=coolblue' ";
        strHTML += "frameborder='0' name='eWebEditor1' scrolling='no' width='600' HEIGHT='350'></iframe>";
        this.Pub1.AddTD(strHTML);
        this.txtContent.Value = message.Doc;
        this.ClientScript.RegisterStartupScript(this.GetType(), "setValue", "EditorSetValue('" + message.Doc + "')", true);
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("colspan='2'");
        this.Pub1.Add("<input type='button' value='发送' id='btnReply' name='" + message.OID + "' onclick='doAsyncPost(this)' />");
        this.Pub1.Add("<input type='button' value='存草稿' id='btnDraft' onclick='doAsyncPost(this)' />");
        this.Pub1.Add("<input type='button' value='返回' id='btnCancel' onclick='history.go(-1)' />");
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        addEmpToDiv();
    }
    /// <summary>
    /// 生成添加联系人界面
    /// </summary>
    private void AddLinkman()
    {
        this.Pub1.Controls.Clear();

        int myWidth = 200;

        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        this.Pub1.AddTD("用户：");

        string strHTML = "<input type='text' id='txtUser' name='" + BP.Web.WebUser.No + "' disabled='disabled' " +
                         "value='" + BP.Web.WebUser.Name + "' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("编号：");
        strHTML = "<input type='text' id='txtNo' onblur='doAsyncPost(this)' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("姓名：");
        strHTML = "<input type='text' id='txtName' onblur='doAsyncPost(this)' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("邮箱：");
        strHTML = "<input type='text' id='txtEmail' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("手机：");
        strHTML = "<input type='text' id='txtMobile' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("电话：");
        strHTML = "<input type='text' id='txtPhone' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("生日：");
        strHTML = "<input type='text' id='txtBirthday' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("地址：");
        strHTML = "<input type='text' id='txtAddr' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("部门：");
        strHTML = "<input type='text' id='txtDept' width='" + myWidth + "px' />";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("备注：");
        strHTML = "<textarea id='txtNote' cols='17' rows='3'></textarea>";
        this.Pub1.AddTD(strHTML);
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();

        strHTML = "<input type='button' id='btnSave' value='保存信息' onclick='doAsyncPost(this)' />";
        this.Pub1.Add(strHTML);
        strHTML = "<input type='button' id='btnCancel' value='取消' onclick='history.go(-1)' />";
        this.Pub1.Add(strHTML);
    }
    /// <summary>
    /// 修改联系人信息
    /// </summary>
    private void ModifyLinkman()
    {
        string strNo = string.Empty;
        string strName = string.Empty;

        if (Request.QueryString["OID"] != null)
        {
            //string strEmp = Convert.ToString(Request.QueryString["Emp"]);
            //strName = strEmp.Substring(0, strEmp.IndexOf('<'));
            //strNo = strEmp.Substring(strEmp.IndexOf('<') + 1, strEmp.IndexOf('>') - strEmp.IndexOf('<') - 1);
            //string strSql = "select * from GE_Friend where FK_Emp2='@FK_Emp2' and Name='@Name'";
            ////SqlParameter[] paras = new SqlParameter[2];
            ////paras[0] = new SqlParameter("@FK_Emp2", strNo);
            ////paras[1] = new SqlParameter("@Name", strName);

            //string strSql = "select * from GE_Friend where FK_Emp2=@FK_Emp2 and Name=@Name";
            //BP.DA.Paras paras = new BP.DA.Paras();
            //paras.Add("@FK_Emp2", strNo);
            //paras.Add("@Name", strName);
            //DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(strSql, paras);

            GeFriend friend = new GeFriend();
            friend.OID = Convert.ToInt32(this.Request.QueryString["OID"]);
            friend.Retrieve();
            {
                int myWidth = 200;
                this.Pub1.AddTable("width='" + strWidth + "'");
                this.Pub1.AddTR();
                this.Pub1.AddTD("用户：");
                this.Pub1.AddTDEnd();
                string strHTML = "<input type='text' id='txtUser' name='" + BP.Web.WebUser.No + "' disabled='disabled' " +
                                 "value='" + BP.Web.WebUser.Name + "' width='" + myWidth + "px' OID='" + friend.OID + "'/>";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
                this.Pub1.AddTR();
                this.Pub1.AddTD("编号：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtNo' disabled='disabled' width='" + myWidth + "px'  value='" + friend.FK_Emp2 + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("姓名：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtName' disabled='disabled' width='" + myWidth + "px'  value='" + friend.Name + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("邮箱：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtEmail' width='" + myWidth + "px' value='" + friend.Email + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("手机：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtMobile' width='" + myWidth + "px'  value='" + friend.Mobile + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("电话：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtPhone' width='" + myWidth + "px'  value='" + friend.Phone + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("生日：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtBirthday' width='" + myWidth + "px'  value='" + friend.Birthday + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("地址：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtAddr' width='" + myWidth + "px'  value='" + friend.Address + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("部门：");
                this.Pub1.AddTDEnd();
                strHTML = "<input type='text' id='txtDept' width='" + myWidth + "px'  value='" + friend.Company + "' />";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTR();
                this.Pub1.AddTD("备注：");
                this.Pub1.AddTDEnd();
                strHTML = "<textarea id='txtNote' cols='17' rows='3'>" + friend.Note + "</textarea>";
                this.Pub1.AddTD(strHTML);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();

                this.Pub1.AddTableEnd();

                strHTML = "<input type='button' id='btnUpdate' value='保存信息' onclick='doAsyncPost(this)' />";
                this.Pub1.Add(strHTML);
                strHTML = "<input type='button' id='btnCancel' value='取消' onclick='history.go(-1)' />";
                this.Pub1.Add(strHTML);
            }
        }
    }
    /// <summary>
    /// 联系人列表
    /// </summary>
    private void NewFriend()
    {
        this.Pub1.Controls.Clear();

        GeFriends ens = new GeFriends();
        QueryObject qo = new QueryObject(ens);
        qo.AddWhere(GeFriendAttr.FK_Emp1, BP.Web.WebUser.No);
        int num = qo.GetCount();
        this.Pub2.BindPageIdx(num, BP.SystemConfig.PageSize, this.PageIdx, "Message.aspx?op=f");
        qo.DoQuery("OID", BP.SystemConfig.PageSize, this.PageIdx);

        this.Pub1.AddTable("width='" + strWidth + "'");
        this.Pub1.AddTR();
        string strHTML = "<input type='checkbox' onclick='selectAll(this)' />";
        this.Pub1.AddTDTitle("width=2%", strHTML);
        this.Pub1.AddTDTitle("width=10%", "姓名");
        this.Pub1.AddTDTitle("width=10%", "手机");
        this.Pub1.AddTDTitle("width=20%", "Email");

        this.Pub1.AddTDTitle("width=58%", "备注");
        this.Pub1.AddTREnd();
        foreach (GeFriend en in ens)
        {
            string strID = en.Name + "<" + en.FK_Emp2 + ">;";
            this.Pub1.AddTR("ondblclick='myClick(this,\"" + strID + "\",\"" + en.OID + "\")' onmouseover='myOver(this)' onmouseout='myOut(this)'");
            strHTML = "<input type='checkbox' id='" + strID + "' name='chklinkman' onclick='Chk_Click(this)' />";
            this.Pub1.AddTD(strHTML);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTD(en.Mobile);
            this.Pub1.AddTD(en.Email);
            this.Pub1.AddTD(en.Note);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("colspan='5'");
        Button button = new Button();
        button.Text = "添加联系人";
        button.CommandName = "Add";
        button.Command += new CommandEventHandler(button_Command);
        this.Pub1.Add(button);
        button = new Button();
        button.Text = "写信";
        button.CommandName = "Create";
        button.Command += new CommandEventHandler(button_Command);
        this.Pub1.Add(button);
        button = new Button();
        button.Text = "删除";
        button.OnClientClick = "return DelConfirm()";
        button.CommandName = "Delete";
        button.Command += new CommandEventHandler(button_Command);
        button.Attributes.Add("onclick", "return confirm('你确定要删除吗?')");
        this.Pub1.Add(button);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

    /// <summary>
    /// 按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button_Command(object sender, CommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Add":
                AddLinkman();
                break;
            case "Create":
                AddEditor();
                break;
            case "Delete":
                DeleteLinkman();
                NewFriend();
                break;
            case "SendMsg":
                //SendMsg();
                Response.Redirect("GEMsg.aspx?op=w");
                break;
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    private void SendMsg()
    {
        string strEmps = Convert.ToString(this.Request.Form["txtReceiver"]);
        string strTitle = Convert.ToString(this.Request.Form["txtTitle"]);
        string strContent = txtContent.Value;

        string[] strs = strEmps.Split(';');
        for (int i = 0; i < strs.Length - 1; i++)
        {
            //string str = strs[i].Substring(strs[i].IndexOf('<') + 1, strs[i].Length - strs[i].IndexOf('<') - 2);
            string str = strs[i].Substring(strs[i].IndexOf('<') + 1, strs[i].IndexOf('<') - strs[i].IndexOf('>'));
            GeInbox inbox = new GeInbox();
            GeOutbox outbox = new GeOutbox();
            Response.Write(str + "," + strTitle + "," + strContent + "<br/>");

            //BP.DA.DBAccess.RunSQL(strSql);
        }
        linkman.Value = string.Empty;
    }
    /// <summary>
    /// 删除联系人信息
    /// </summary>
    private void DeleteLinkman()
    {
        string[] strs = linkman.Value.Split(';');
        for (int i = 0; i < strs.Length - 1; i++)
        {
            string str = strs[i].Substring(strs[i].IndexOf('<') + 1, strs[i].Length - strs[i].IndexOf('<') - 2);
            string strSql = "delete from ge_friend where fk_emp2='" + str + "'";
            BP.DA.DBAccess.RunSQL(strSql);
        }
        linkman.Value = string.Empty;
    }
}