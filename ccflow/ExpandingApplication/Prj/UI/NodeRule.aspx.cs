using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.DA;
using BP.En;
using BP.PRJ;
using BP.Web;

public partial class ExpandingApplication_PRJ_NodeRuleUI :WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string FK_Node
    {
        get
        {
            return this.Request.QueryString["FK_Node"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Dir dir = new Dir();
        //dir.CheckPhysicsTable();
        //NodeRule nr = new NodeRule();
        //nr.CheckPhysicsTable();

        if (this.FK_Node != null)
        {
            this.BindNodeRule();
            return;
        }

        if (this.FK_Flow == null)
        {
            Flows fls = new Flows();
            fls.RetrieveAll();
            Flow fl = fls[0] as Flow;
            this.Response.Redirect("NodeRule.aspx?FK_Flow=" + fl.No, true);
            return;
        }
        this.BindLeft();
        this.BindRight();
    }
    public void BindLeft()
    {
        FlowSorts sorts = new FlowSorts();
        sorts.RetrieveAll();
        Flows fls = new Flows();
        fls.RetrieveAll();
        foreach (FlowSort fs in sorts)
        {
            this.Pub1.AddB(fs.Name);
            this.Pub1.AddBR();
            this.Pub1.AddUL();
            foreach (Flow fl in fls)
            {
                if (fl.FK_FlowSort != fs.No)
                    continue;

                this.Pub1.AddLi("<a href='NodeRule.aspx?FK_Flow="+fl.No+"'>" + fl.Name + "</a>");
            }
            this.Pub1.AddULEnd();
        }
    }
    public void BindRight()
    {
        Nodes nds = new Nodes(this.FK_Flow);
        foreach (Node nd in nds)
        {
            this.Pub2.AddLi("<a href=\"javascript:window.showModalDialog('NodeRule.aspx?FK_Node=" + nd.NodeID + "', 'ds', 'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');\" >" + nd.Name + "</a>");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void BindNodeRule()
    {
        Dirs dirs = new Dirs();
        dirs.RetrieveAll();

        Rules rls = new Rules();
        rls.RetrieveAll();

        NodeRules nrs = new NodeRules();
        nrs.Retrieve(NodeRuleAttr.FK_Node, this.FK_Node);

        foreach (Dir dir in dirs)
        {
            this.Pub2.AddB(dir.Name);
            this.Pub2.AddBR();
            
            foreach (Rule rl in rls)
            {

                CheckBox cb = new CheckBox();
                cb.ID = "CB_" + rl.No;
                cb.Text = rl.Name;
                if (nrs.Contains(NodeRuleAttr.FK_Rule, rl.No) == true)
                    cb.Checked = true;
                else
                    cb.Checked = false;

                this.Pub2.Add(cb);
            }
        }
        this.Pub2.AddHR();
        Button btn = new Button();
        btn.Text = " Save  ";
        btn.ID = "Btn_Save";
        btn.Click += new EventHandler(btn_Click);
    }

    void btn_Click(object sender, EventArgs e)
    {
        NodeRules nrs = new NodeRules();
        nrs.Delete(NodeRuleAttr.FK_Node, this.FK_Node);

        Rules rls = new Rules();
        rls.RetrieveAll();
        foreach (Rule rl in rls)
        {
            CheckBox cb = this.Pub2.GetCBByID("CB_" + rl.No);
            if (cb.Checked == false)
                continue;

            NodeRule nr = new NodeRule();
            nr.FK_Node = this.FK_Node;
            nr.FK_Rule = rl.No;
            nr.Insert();
        }

        this.WinCloseWithMsg("Save OK.");
    }
}