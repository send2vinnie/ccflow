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
using BP.DA;
using BP.En;
using BP.GE;
using FtpSupport;
using Microsoft.Win32;
using BP.Web.Controls;
using System.Text;
using BP.Edu;

public partial class GE_NetDisk_NetDiskDtl : BP.Web.UC.UCBase3
{
    // 获取DKDir的OID
    public string FK_Dir
    {
        get
        {
            string str = this.Request.QueryString["FK_Dir"];
            return str;
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        string upload = this.Request.ApplicationPath + "/Images/Btn/Upload.GIF";
        int pageSize = new DKFiles().GetEnsAppCfgByKeyInt("ShowFileNUM");

        DKFiles dkfiles = new DKFiles();
        QueryObject qo = new QueryObject(dkfiles);
        qo.AddWhere(DKFileAttr.FK_Emp, BP.Web.WebUser.No);
        qo.DoQuery();

        int totalRL = 0;
        foreach (DKFile dkfile in dkfiles)
        {
            totalRL += dkfile.MyFileSize;
        }
        int RL = 52428800 - totalRL;
        

        this.Pub1.AddTableDef("width='98%'");
        this.Pub1.AddTR();
        this.Pub1.Add("<td align =right colspan=1 >" +"网络硬盘总容量：50M"+ " </td>");
        this.Pub1.Add("<td align =right colspan=1 >" + "剩余容量：" +GloDK.ConvertFileSize (RL.ToString ())+ " </td>");
        this.Pub1.Add("<td align =right colspan=1 >" + " <a href=\"javascript:OrdinaryUpload('" + this.FK_Dir + "');\">普通上传</a>" + " </td>");

        this.Pub1.Add("<td align =right colspan=1 >"  + " <a href=\"javascript:Upload('" + this.FK_Dir + "');\"><img src='" + upload + "' border=0/></a>" + " </td>");
        //this.Pub1.Add("<td align =right  colspan=4>" + " <a href=\"javascript:Upload('" + this.FK_Dir + "');\"><img src='" + upload + "' border=0/></a>" + " </td>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR("class='disk_title'");
        this.Pub1.AddTD("width:30%", "名称");
        this.Pub1.AddTD("width:10%", "大小");
        this.Pub1.AddTD("width:10%", "日期");
        this.Pub1.AddTD("width:15%", "操作");

        this.Pub1.AddTREnd();

        this.BindDtl();
        this.Pub1.AddTableEnd();
    }

    // 目录DKDir为空时， 绑定根目录
    public void BindGrade1()
    {
        int i = 0;
        this.Pub1.AddTableDef();

        DKDirs tps = new DKDirs();
        QueryObject qo = new QueryObject(tps);
        qo.AddWhere(DKDirAttr.FK_Emp, BP.Web.WebUser.No);
        qo.addAnd();
        qo.AddWhereLen(DKDirAttr.GradeNo, "=", 2, DBType.SQL2000);
        qo.DoQuery();

        foreach (DKDir tp in tps)
        {
            i++;
            this.Pub1.AddTR();
            string tpHref = "<a href='NetDiskDtl.aspx?FK_Dir=" + tp.OID + "' ><img src='" + this.Request.ApplicationPath + "/GE/NetDisk/img/Dir.gif' border=0>" + tp.Name + "</a>";
            this.Pub1.Add("<td colspan=4>" + tpHref + "</td>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }

    public void BindDtl()
    {
        string dirOID = this.FK_Dir;
        if (dirOID == null || dirOID == "" || dirOID == "0")
        {
            this.BindGrade1();
            return;
        }


        DKDir dir = new DKDir(int.Parse(dirOID));

        // 设置返回上层目录链接
        string pGradeNo = dir.GradeNoOfParent;
        if (pGradeNo == "")
        {
            string href = "<a href='NetDiskDtl.aspx'><img src='" + this.Request.ApplicationPath + "/GE/NetDisk/img/Root.gif' border=0>根目录</a>";
            href += "-<a href='NetDiskDtl.aspx?FK_Dir=" + dir.OID + "' ><img src='" + this.Request.ApplicationPath + "/GE/NetDisk/img/folderup.png' border=0>" + dir.Name + "</a>";

            this.Pub1.Add("<td colspan=4>" + href + "</td>");

            this.Pub1.AddTREnd();

        }
        else
        {
            // 获取所有父节点链接信息
            DKDir pDir = new DKDir();
            int pcount = pGradeNo.Length / 2;
            string href = "<a href='NetDiskDtl.aspx'><img src='" + this.Request.ApplicationPath + "/GE/NetDisk/img/Root.gif' border=0>根目录</a>";
            for (int i = 1; i <= pcount; i++)
            {
                pDir.RetrieveByAttrAnd(DKDirAttr.GradeNo, pGradeNo.Substring(0, i * 2), DKDirAttr.FK_Emp, BP.Web.WebUser.No);
                href += "-<a href='NetDiskDtl.aspx?FK_Dir=" + pDir.OID + "' ><img src='" + this.Request.ApplicationPath + "/GE/NetDisk/img/folderup.png' border=0>" + pDir.Name + "</a>";
            }

            // 添加本节点的导航节点
            href += "-<a href='NetDiskDtl.aspx?FK_Dir=" + dir.OID + "' ><img src='" + this.Request.ApplicationPath + "/GE/NetDisk/img/folderup.png' border=0>" + dir.Name + "</a>";
            this.Pub1.AddTR();
            this.Pub1.Add("<td colspan=4>" + href + "</td>");
            this.Pub1.AddTREnd();
        }


        #region  attrs
        // 获取本目录下的所有目录
        DKDirs dirs = new DKDirs();
        int dirCount = dirs.ReNextChild(BP.Web.WebUser.No, dir.GradeNo);

        foreach (DKDir mydir in dirs)
        {
            this.Pub1.AddTR();

            this.Pub1.AddTDA("NetDiskDtl.aspx?FK_Dir=" + mydir.OID,
                "<img src='" + this.Request.ApplicationPath + "/GE/NetDisk/img/Dir.gif' border=0>" + mydir.Name);
            this.Pub1.AddTD();
            this.Pub1.AddTD();
            this.Pub1.AddTD();
            this.Pub1.AddTREnd();
        }
        #endregion

        // 获取本目录下的文件信息
        DKFiles ens = new DKFiles();
        ens.Retrieve(DKFileAttr.FK_Emp, BP.Web.WebUser.No, DKFileAttr.FK_DKDir, dirOID);
        string appPath = this.Request.ApplicationPath;
        foreach (DKFile en in ens)
        {
            this.Pub1.AddTR();
            string path = this.Request.ApplicationPath;
            if (en.MyFileName.Length < 16)
            {
                this.Pub1.AddTD("<img src='" + this.Request.ApplicationPath + "/Images/FileType/" + en.MyFileExt + ".gif' border=0/>" + en.MyFileName);
            }
            else
            {
                this.Pub1.AddTD("<img src='" + this.Request.ApplicationPath + "/Images/FileType/" + en.MyFileExt + ".gif' border=0/>" + en.MyFileName.Substring(0, 16) + "..");
            }
            this.Pub1.AddTD(GloDK.ConvertFileSize(en.MyFileSize));

            this.Pub1.AddTD("align=center", en.RDT.Substring(0, 10));
            this.Pub1.Add("<td align =center>");

            ImageButton btn1 = new ImageButton();
            btn1.ID = "DL_" + en.OID.ToString();
            btn1.ImageUrl = this.Request.ApplicationPath + "/GE/NetDisk/img/down.gif";
            btn1.Click += new ImageClickEventHandler(btnDownLoad_Click);
            this.Pub1.Add(btn1);

            ImageButton btn = new ImageButton();
            btn.ID = en.OID.ToString();
            btn.ImageUrl = this.Request.ApplicationPath + "/GE/NetDisk/img/delete.gif";
            btn.Attributes["onclick"] = "return window.confirm('您确定要删除吗?');";
            btn.Click += new ImageClickEventHandler(btnDelete_Click);
            this.Pub1.Add(btn);

            this.Pub1.Add("</td>");
            this.Pub1.AddTREnd();
        }
    }

    // 从FTP服务器上下载文件
    public void btnDownLoad_Click(object sender, EventArgs e)
    {
        string fileOID = ((ImageButton)sender).ID.Substring(3, ((ImageButton)sender).ID.Length - 3);
        DKFile dkFile = new DKFile(int.Parse(fileOID));
        string fileName = fileOID + "." + dkFile.MyFileExt;

        string fk_Emp = "";
        if (BP.Web.WebUser.FK_Dept == null)
        {
            EduEmp emp = new EduEmp(BP.Web.WebUser.No);
            fk_Emp = emp.FK_Dept;

        }
        else
        {
            fk_Emp = BP.Web.WebUser.FK_Dept;
        }

        // 文件的存放目录
        string basePath = GloDK.NetDiskFtpPath + "/" + fk_Emp + "/" + BP.Web.WebUser.No + "/";
        string filePath = basePath + fileName;

        // 获取FTP的链接

        try
        {
            FtpConnection conn = GloDK.FileFtpConn;

            if (conn.DirectoryExist(basePath))
            {
                conn.SetCurrentDirectory("/"+basePath);
                if (conn.FileExist(fileName) == false)
                {
                    this.Alert("服务器上已经不存在此文件，请检查！");
                    return;
                }
                else
                {
                    string fileExt = dkFile.MyFileExt;
                    string DEFAULT_CONTENT_TYPE = "application/unknown";
                    RegistryKey regKey, fileExtKey;
                    string fileContentType;
                    try
                    {
                        regKey = Registry.ClassesRoot;
                        fileExtKey = regKey.OpenSubKey(fileExt);
                        fileContentType = fileExtKey.GetValue("Content   Type", DEFAULT_CONTENT_TYPE).ToString();
                    }
                    catch
                    {
                        fileContentType = DEFAULT_CONTENT_TYPE;
                    }
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;   fileName=" + HttpUtility.UrlEncode(dkFile.MyFileName));
                    Response.ContentType = fileContentType;

                    FtpSupport.FtpStream ftpFS = conn.OpenFile(fileName, FtpSupport.GenericRights.Read);
                    byte[] buffer = new byte[10240];
                    int n = ftpFS.Read(buffer, 0, buffer.Length);
                    while (n > 0)
                    {
                        Response.BinaryWrite(buffer);
                        n = ftpFS.Read(buffer, 0, buffer.Length);
                    }
                    ftpFS.Close();

                    Response.End();
                }
            }
            else
            {
                this.Alert("文件不存在路径不存在，请检查！");
            }
        }
        catch
        {
            this.Alert("请检查FTP服务器连接，连接失败！");
            return;
        }
    }

    // 删除文件和文件信息
    public void btnDelete_Click(object sender, EventArgs e)
    {

        string fileOID = ((ImageButton)sender).ID;
        DKFile dkFile = new DKFile(int.Parse(fileOID));
        string fileName = fileOID + "." + dkFile.MyFileExt;

        string fk_Emp = "";
        if (BP.Web.WebUser.FK_Dept == null)
        {
            EduEmp emp = new EduEmp(BP.Web.WebUser.No);
            fk_Emp = emp.FK_Dept;

        }
        else
        {
            fk_Emp = BP.Web.WebUser.FK_Dept;
        }

        // 文件的存放目录 
        string basePath = GloDK.NetDiskFtpPath + "/" + fk_Emp + "/" + BP.Web.WebUser.No + "/";
        string filePath = basePath + fileName;

        try
        {
            // 删除FTP服务器上的文件
            FtpConnection ftpConn = GloDK.FileFtpConn;
            ftpConn.SetCurrentDirectory(basePath);
            if (ftpConn.FileExist(fileName))
            {
                ftpConn.DeleteFile(fileName);
            }
        }
        catch
        {
            this.Alert("不能正确连接FTP服务器，请检查服务器链接！");
        }
        // 删除数据库的信息
        dkFile.DirectDelete();

        // 刷新页面
        Response.Redirect(this.PageID + ".aspx?a=b&FK_Dir=" + this.FK_Dir + "&PageIdx=" + this.PageIdx);
    }
}
