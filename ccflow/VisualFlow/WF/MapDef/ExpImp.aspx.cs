using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.Web;
using BP.DA;
using BP.En;

public partial class WF_MapDef_ExpImp : WebPage
{
    public string RefNo
    {
        get
        {
            return this.Request.QueryString["RefNo"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.Sys.MapData md = new BP.Sys.MapData();
        md.No = this.RefNo;
        md.RetrieveFromDBSources();
        switch (this.DoType)
        {
            case "Exp":
                DataSet ds = md.GenerHisDataSet();
                string file=this.Request.PhysicalApplicationPath+"\\Temp\\"+this.RefNo+".xml";
                ds.WriteXml(file);
                BP.PubClass.DownloadFile(file, md.Name + ".xml");
                this.WinClose();
                break;
            case "Imp":
                break;
            default:
                this.BindHome();
                break;
        }
    }
    public void BindHome()
    {
        this.Pub1.AddFieldSet("导出表单模板");
        this.Pub1.Add("&nbsp;&nbsp;&nbsp;<a href='ExpImp.aspx?DoType=Exp&RefNo=" + this.RefNo + "' >执行导出</a>");
        this.Pub1.AddFieldSetEnd();

        this.Pub1.AddFieldSet("导入");
        this.Pub1.AddBR("特别说明:执行导入系统将会清除当前表单信息。");
        this.Pub1.AddBR("表单模板(*.xml)");
        HtmlInputFile fu = new HtmlInputFile();
        fu.ID = "F";
        this.Pub1.Add(fu);
        
        Button btn = new Button();
        btn.Text = "执行导入";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
        this.Pub1.AddFieldSetEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        string file = this.Request.PhysicalApplicationPath + "\\Temp\\" + this.RefNo + ".xml";
        HtmlInputFile myfu = this.Pub1.FindControl("F") as HtmlInputFile;
        myfu.PostedFile.SaveAs(file);

    //    myfu.HttpPostedFile.SaveAs(file);

        DataSet ds = new DataSet();
        ds.ReadXml(file);
        BP.Sys.MapData.ImpMapData(this.RefNo,  ds);
        this.WinClose();
    }
}