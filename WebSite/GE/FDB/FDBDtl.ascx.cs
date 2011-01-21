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

public partial class GE_FDB_FDBDtl : BP.Web.UC.UCBase3
{
    public string FK_Dir
    {
        get
        {
            return this.Request.QueryString["FK_Dir"];
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.BindDtl();
    }

    public void BindGrade1()
    {
        int i = 0;
        this.AddTableDef("width='98%'");
        this.AddTableBarDef(null, "<a href='FDBDtl.aspx'><Img src='" + this.Request.ApplicationPath + "/GE/FDB/img/Root.gif' border=0/>根目录\\</a>", 5, null);
        this.AddTR("class='th'");
        this.AddTD("width:10%", "");
        this.AddTD("width:30%", "名称");
        this.AddTD("width:10%", "大小");
        this.AddTD("width:10%", "类型");
        this.AddTD("width:15%", "修改日期");
        this.AddTREnd();

        //添加根目录
        FDBDirs tps = new FDBDirs();
        tps.ReRootDir(this.RefNo);
        foreach (FDBDir tp in tps)
        {
            i++;
            this.AddTR();
            this.AddTD(i);
            this.AddTD("<a href='FDBDtl.aspx?FK_Dir=" + tp.No + "' ><img src='" + this.Request.ApplicationPath + "/GE/FDB/img/Dir.gif' border=0>" + tp.Name + "</a>");
            this.AddTD();
            this.AddTD();
            this.AddTD();
            this.AddTREnd();
        }

        //添加根目录文件
        FDBs rootFiles = new FDBs();
        rootFiles = tps.ReRootFiles(this.RefNo);
        string appPath = this.Request.ApplicationPath;
        foreach (FDB file in rootFiles)
        {
            i++;
            this.AddTR();
            this.AddTD(i);
            this.AddTDA(appPath + "/GE/FDB/Do.aspx?MyPK=" + file.MyPK, file.NameShort, "_blank");
            this.AddTD(WuXiaoyun.ConvertFileSize(file.FSize));
            this.AddTD(file.Ext);
            this.AddTD(file.CDT);
            this.AddTREnd();
        }

        this.AddTableEnd();
    }
    public void BindDtl()
    {
        this.Clear();
        if (this.FK_Dir == null || this.FK_Dir == "")
        {
            this.BindGrade1();
            return;
        }

        int i = 0;
        FDBs ens = new FDBs(this.FK_Dir);
        FDBDir dir = new FDBDir(this.FK_Dir);

        //获取右侧目录导航链接
        char splitChar = '\\';
        if (dir.NameFull.ToLower().StartsWith("ftp://"))
        {
            splitChar = '/';
        }
        string[] dirNames = dir.ParentDir.Substring(1).Split(splitChar);
        string dirHrefs = "";
        int count = 1;
        foreach (string name in dirNames)
        {
            if (name == "")
            {
                continue;
            }
            dirHrefs += "\\<a href='FDBDtl.aspx?FK_Dir=" + this.FK_Dir.Substring(0, 2 * count) + "'>" + name + "</a>";
            count++;
        }
        //if (dirHrefs != "")
        //{
            dirHrefs += "\\";
        //}
        //当前目录
        dirHrefs += dir.Name;

        this.AddTableDef("width='98%'");
        this.AddTableBarDef(null, "<a href='FDBDtl.aspx'><Img src='" + this.Request.ApplicationPath + "/GE/FDB/img/Root.gif' border=0/>根目录\\</a>" + dirHrefs, 5, null);
        //this.AddTableBarDef(null, dirHrefs, 5, null);
        this.AddTR("class='th'");
        this.AddTD();
        this.AddTD("width:30%", "名称");
        this.AddTD("width:10%", "大小");
        this.AddTD("width:10%", "类型");
        this.AddTD("width:15%", "修改日期");
        this.AddTREnd();

        #region  attrs
        if (this.FK_Dir.Length >= 1)
        {
            i++;
            this.AddTR();
            this.AddTD(i);
            this.AddTD("<a href='FDBDtl.aspx?FK_Dir=" + this.FK_Dir.Substring(0, this.FK_Dir.Length - 2) + "' ><img src='" + this.Request.ApplicationPath + "/GE/FDB/img/folderup.png' border=0>......</a>");
            this.AddTD();
            this.AddTD();
            this.AddTD();
            this.AddTREnd();
        }
        FDBDirs dirs = new FDBDirs();
        dirs.ReHisChilds(this.FK_Dir,this.RefNo);
        foreach (FDBDir mydir in dirs)
        {
            i++;
            this.AddTR();
            this.AddTD(i);
            this.AddTDA("FDBDtl.aspx?FK_Dir=" + mydir.No, "<img src='" + this.Request.ApplicationPath + "/GE/FDB/img/Dir.gif' border=0>" + mydir.Name);
            this.AddTD();
            this.AddTD();
            this.AddTD();
            this.AddTREnd();
        }
        #endregion

        string appPath = this.Request.ApplicationPath;
        foreach (FDB en in ens)
        {
            i++;
            this.AddTR();
            this.AddTD(i);
            this.AddTDA(appPath + "/GE/FDB/Do.aspx?MyPK=" + en.MyPK, en.NameShort, "_blank");
            this.AddTD(WuXiaoyun.ConvertFileSize(en.FSize));
            this.AddTD(en.Ext);
            this.AddTD(en.CDT);
            this.AddTREnd();
        }
        this.AddTableEnd();
    }
}
