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
using BP.WF;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;
using BP.Web;
using BP;

public partial class GovDoc_DoClient : WebPage
{
    public int WorkID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string msg = null;
        switch (this.DoType)
        {
            //删除流程中第一个节点的数据，包括待办工作
            case "DelFlow":
                //调用DoDeleteWorkFlowByReal方法
                WorkFlow wf = new WorkFlow( this.FK_Flow, this.WorkID);
                wf.DoDeleteWorkFlowByReal();
                this.Pub1.AddH4("执行信息。");
                this.Pub1.AddHR();
                this.Pub1.Add("流程删除成功。。。");
                break;
            case "UnSend":
                this.Pub1.AddH4("执行撤消发送信息");
                msg = this.Session["Msg"].ToString().Replace("@@", "@");
                msg = msg.Replace("@", "<br>@");
                this.Pub1.AddHR();
                this.Pub1.Add(msg);
                return;
            case "Send":
                this.Pub1.AddH4("发送信息");
                msg = this.Session["Msg"].ToString().Replace("@@", "@");
                msg = msg.Replace("@", "<br>@");
                this.Pub1.AddHR();
                this.Pub1.Add(msg);
                return;
            case "Msg":
                msg = this.Session["Msg"].ToString().Replace("@@", "@");
                msg = msg.Replace("@", "<br>@");
                this.Pub1.AddHR();
                this.Pub1.Add(msg);
                return;
            case "StartFlow":
                //判断是否有草稿.
                int fk_node = int.Parse(this.FK_Flow + "01");
                string sql = "SELECT OID , Title, RDT FROM ND" + fk_node + " WHERE Rec='" + WebUser.No + "' AND NodeState=0";
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    this.Response.Redirect("DoClient.aspx?DoType=DoStartFlowByTemple&FK_Flow=" + this.FK_Flow, true);
                }
                else
                {
                    Flow fl = new Flow(this.FK_Flow);
                    this.Pub1.AddTable("width='100%'");
                    this.Pub1.AddCaptionLeftTX(fl.Name + "：<a href='DoClient.aspx?DoType=DoStartFlowByTemple&FK_Flow=" + fl.No + "'><img src='./../Images/Btn/Word.gif' border=0/>以模板创建新流程</a>");
                    this.Pub1.AddTR();
                    this.Pub1.AddTDTitle("建立日期");
                    this.Pub1.AddTDTitle("标题");
                    this.Pub1.AddTDTitle("colspan=2", "操作");
                    this.Pub1.AddTREnd();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string workid = dr["OID"].ToString();
                        this.Pub1.AddTR();
                        this.Pub1.AddTD(dr["RDT"].ToString());
                        this.Pub1.AddTD(dr["Title"].ToString());
                        this.Pub1.AddTDA("DoClient.aspx?DoType=DoStartFlow&FK_Flow=" + this.FK_Flow + "&WorkID=" + workid, "编辑");
                        this.Pub1.AddTDA("javascript:DoDelCaoGao('" + workid + "','" + this.FK_Flow + "')", "删除");
                        this.Pub1.AddTREnd();
                    }
                    this.Pub1.AddTableEndWithHR();
                }
                break;
            case "DoStartFlowByTemple":
                this.Pub1.AddMsgGreen("提示：", "按照模板生成公文 。。。");
                break;
            case "DoStartFlow":
                this.Pub1.AddMsgGreen("提示：", "正在生成 。。。编辑草稿");
                break;
            case "OpenFlow":
                this.Pub1.AddMsgGreen("提示：", "正在打开流程请稍后。。。。");
                break;
            case "OpenDoc":
                this.Pub1.AddMsgGreen("提示：", "正在打开文件请稍后。。");
                break;
            default:
                throw new Exception("@标记错误。" + this.DoType);
        }
    }
}
