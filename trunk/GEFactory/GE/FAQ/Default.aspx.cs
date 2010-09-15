using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;

public partial class FAQ_Default : BP.Web.WebPage
{
    public string ShowType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        string sql = null;
        KMs kms = new KMs();

        TreeNode t0 = new TreeNode("小学");
        t0.Selected = false;
        t0.SelectAction = TreeNodeSelectAction.Expand;

        tvkm.Nodes.Add(t0);
        QueryObject qo = new QueryObject(kms);
        sql = "select no from edu_km where no in(SELECT DISTINCT FK_KM AS fk_km FROM  Edu_KMNJ WHERE (FK_NJ LIKE 'a%'))";
        qo.AddWhereInSQL("No", sql);
        qo.DoQuery();

        foreach (KM km in kms)
        {
            TreeNode tn0 = new TreeNode();

            tn0.Text = km.Name;
            tn0.NavigateUrl = "ListNew.aspx?TopType=" + km.No + "&ShowType=" + this.ShowType;
            //tvkm.Target[0].c;
            t0.ChildNodes.Add(tn0);
        }

        TreeNode t1 = new TreeNode("初中");
        t1.Selected = false;
        t1.SelectAction = TreeNodeSelectAction.Expand;

        tvkm.Nodes.Add(t1);
        sql = "select no from edu_km where no in(SELECT DISTINCT FK_KM AS fk_km FROM  Edu_KMNJ WHERE (FK_NJ LIKE 'b%'))";
        QueryObject qo1 = new QueryObject(kms);
        qo1.AddWhereInSQL("No", sql);

        qo1.DoQuery();
        foreach (KM km in kms)
        {
            TreeNode tn1 = new TreeNode();
            tn1.Text = km.Name;
            tn1.NavigateUrl = "ListNew.aspx?TopType=" + km.No + "&ShowType=" + this.ShowType;
            t1.ChildNodes.Add(tn1);
        }
        TreeNode t2 = new TreeNode("高中");
        t1.Selected = false;
        t2.SelectAction = TreeNodeSelectAction.Expand;
        tvkm.Nodes.Add(t2);
        sql = "select no from edu_km where no in(SELECT DISTINCT FK_KM AS fk_km FROM  Edu_KMNJ WHERE (FK_NJ LIKE 'c%'))";
        QueryObject qo2 = new QueryObject(kms);
        qo2.AddWhereInSQL("No", sql);
        qo2.DoQuery();
        foreach (KM km in kms)
        {
            TreeNode tn2 = new TreeNode();
            tn2.Text = km.Name;
            tn2.NavigateUrl = "ListNew.aspx?TopType=" + km.No + "&ShowType=" + this.ShowType;
            t2.ChildNodes.Add(tn2);
        }
        
        if (t0.Expanded == true)
        {
            t1.Collapse();
            t2.Collapse();
        }
        else if (t1.Expanded == true)
        {
            t0.Collapse();
            t2.Collapse();
        }
        else if (t2.Expanded == true)
        {
            t0.Collapse();
            t1.Collapse();
        }
        else
        {
            tvkm.CollapseAll();
        }
    }

}
