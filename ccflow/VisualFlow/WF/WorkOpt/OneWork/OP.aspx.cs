using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_WorkOpt_OneWork_OP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub2.AddFieldSet("恢复启用流程数据到结束节点");
        this.Pub2.AddFieldSetEnd();

        this.Pub2.AddFieldSet("删除流程");
        this.Pub2.AddFieldSetEnd();

        this.Pub2.AddFieldSet("撤销挂起");
        this.Pub2.AddFieldSetEnd();

        this.Pub2.AddFieldSet("强制工作移交");
        this.Pub2.AddFieldSetEnd();

        this.Pub2.AddFieldSet("取回审批");
        this.Pub2.AddFieldSetEnd();
    }
}