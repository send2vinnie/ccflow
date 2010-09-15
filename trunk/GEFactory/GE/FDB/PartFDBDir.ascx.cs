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
using BP.GE;

public partial class GE_FDB_PartFDBDir : BP.Web.UC.UCBase3
{
    public string FK_Dir
    {
        get
        {
            return this.Request.QueryString["FK_Dir"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //引用css和js
        this.Page.Response.Write("<link href='" + this.Request.ApplicationPath + "/GE/FDB/dtree.css' rel='stylesheet' type='text/css' />");
        this.Response.Write("<script src='" + this.Request.ApplicationPath + "/GE/FDB/dtree.js' type='text/javascript'></script>");

        //Title样式
        string groupTitle = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "GroupTitleCSS");
        this.Pub1.AddTD("class='" + groupTitle + "'", "目录");
        
        //绑定目录
        this.BindDir();
    }

    //绑定目录结构
    public void BindDir()
    {
        //基路径
        string baseUrl = this.Request.ApplicationPath + "/GE/FDB/";

        this.PubDir.Add("<script type='text/javascript'>");
        this.PubDir.Add("d = new dTree('d','" + baseUrl + "','fm');");
        //
        FDBDirs roots = new FDBDirs();
        string title = "";
        string pNo = "10000";
        if (this.FK_Dir == null)
        {
            roots.ReRootDir(this.RefNo);
            title = BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName");
            pNo = "10000";
        }
        else
        {
            FDBDir rootDir = new FDBDir(this.FK_Dir);
            title = rootDir.Name;
            roots.ReHisChilds(rootDir.No,this.RefNo);
            pNo = rootDir.No;
        }

        //根目录标题
        this.PubDir.Add("d.add(" + pNo + ",-1,'" + title + "');");

        //添加目录
        AddChildDirs(roots, pNo);

        //FDBDirs dirs = new FDBDirs();
        //foreach (FDBDir root in roots)
        //{

        //    this.PubDir.Add("d.add(" + root.No + ",10000,'" + root.Name + "','FDBDtl.aspx?FK_Dir=" + root.No + "');");
        //    dirs.ReHisChilds(root.No);
        //    AddChildDirs(dirs, root.No);
        //}
        this.PubDir.Add("document.write(d);");
        this.PubDir.Add("</script>");
    }

    //添加目录子节点
    public void AddChildDirs(FDBDirs roots, string pNo)
    {
        FDBDirs dirs = new FDBDirs();
        foreach (FDBDir root in roots)
        {
            this.PubDir.Add("d.add(" + root.No + "," + pNo + ",'" + root.Name + "','FDBDtl.aspx?FK_Dir=" + root.No + "');");
            dirs.ReHisChilds(root.No,this.RefNo);
            AddChildDirs(dirs, root.No);

        }
    }
}
