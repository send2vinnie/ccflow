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
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;
using BP.Web.Controls;
using System.IO;
using BP.GE;

public partial class OrdinaryUpload : WebPage
{
    public string DirOID
    {
        get
        {
            return Request.QueryString["DirOID"];
        }
    }
    private int RL = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        // 
        DKFiles dkfiles = new DKFiles();
        QueryObject qo = new QueryObject(dkfiles);
        qo.AddWhere(DKFileAttr.FK_Emp, BP.Web.WebUser.No);
        qo.DoQuery();

        int totalRL = 0;

        foreach (DKFile dkfile in dkfiles)
        {
            totalRL += dkfile.MyFileSize;
        }
        RL = 52428800 - totalRL;

        this.Title = "文件上传";
        this.Pub1.AddTable("width='120px'");
        this.Pub1.AddCaptionLeftTX("文件上传");
        this.Pub1.AddTR();
        this.Pub1.AddTDGroupTitle("序");
        this.Pub1.AddTDGroupTitle("文件名称");
        this.Pub1.AddTDGroupTitle("文件");
        this.Pub1.AddTDGroupTitle("清除");

        this.Pub1.AddTREnd();
        for (int i = 1; i < 11; i++)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTD("width=6px align=center", i);
            TextBox tb = new TextBox();
            tb.ID = "TB_" + i;
            tb.Attributes["width"] = "100%";
            //tb.Columns = 20;

            this.Pub1.AddTD("width=40px", tb);

            HtmlInputFile file = new HtmlInputFile();
            file.ID = "F" + i;
            file.Attributes["width"] = "100%";
            file.Attributes["onkeydown"] = "return false;";
            file.Size = 40;
            this.Pub1.AddTD("width=80px", file);

            this.Pub1.AddTD("<input id='BtnClear_" + i + "' type='button' value='删除' onclick='ClearFile(\"" + i + "\");' />");
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEndWithHR();

        Btn btn = new Btn();
        btn.ID = "Btn_Save";
        btn.Text = "文件上传";
        btn.Click += new EventHandler(btn_Click);

        this.Pub1.Add("<div style='text-align:center;width:550px'>");
        this.Pub1.Add(btn);
        this.Pub1.AddTD("<input id='ClearAll' type='button' value='删除所有' onclick='ClearForm();' />");
        this.Pub1.Add("</div>");
    }


    void btn_Click(object sender, EventArgs e)
    {
        int n = 0;
        int size = 0;

        for (int i = 1; i < 11; i++)
        {
            HtmlInputFile file = (HtmlInputFile)this.Pub1.FindControl("F" + i);
            if (file.Value.Length == 0)
            {
                ((TextBox)this.Pub1.FindControl("TB_" + i)).Text = "";
                continue;
            }

            size = file.PostedFile.ContentLength;
            ////////////////////////
            bool hasFile = file.HasControls();

            ////////////////////////

            if (size > RL)
            {
                this.Alert("对不起，超过了网络硬盘的容量！");
                return;
            }

            n = n + 1;
            string fileName = file.PostedFile.FileName;
            fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            string ext = fileName.Substring(fileName.LastIndexOf(".") + 1);
            string fileShortName = fileName.Substring(0, fileName.Substring(0, fileName.LastIndexOf(".")).Length);
            string txtName = ((TextBox)this.Pub1.FindControl("TB_" + i)).Text.Trim();
            if (txtName.Length > 0)
                fileName = txtName + "." + ext;
            ((TextBox)this.Pub1.FindControl("TB_" + i)).Text = "";
            DKFile dkFile = new DKFile();

            try
            {
                dkFile.FK_DKDir = this.DirOID;
                dkFile.FK_Emp = BP.Web.WebUser.No;
                dkFile.MyFileName = fileName;
                dkFile.MyFileSize = size;
                dkFile.MyFileExt = ext;
                dkFile.RDT = System.DateTime.Now.ToString();
                dkFile.Insert();
            }
            catch
            {
                this.Alert("文件信息写入数据库出错，请检查数据库连接！");
            }

            try
            {
                string localFile = "/FDB/Edu/Upload/" + fileName;
                file.PostedFile.SaveAs(Server.MapPath(localFile));

                string userDept = "";
                if (BP.Web.WebUser.FK_Dept == null)
                {
                    Emp emp = new Emp(BP.Web.WebUser.No);
                    userDept = emp.FK_Dept;
                }
                else
                {
                    userDept = BP.Web.WebUser.FK_Dept;
                }

                FtpSupport.FtpConnection conn = GloDK.FileFtpConn;

                conn.SetCurrentDirectory("/FDB");
                if (!conn.DirectoryExist("NetDisk"))
                    conn.CreateDirectory("NetDisk");
                conn.SetCurrentDirectory("/FDB/NetDisk");
                if (!conn.DirectoryExist(userDept))
                    conn.CreateDirectory(userDept);
                conn.SetCurrentDirectory("/FDB/NetDisk/" + userDept);
                if (!conn.DirectoryExist(BP.Web.WebUser.No))
                    conn.CreateDirectory(BP.Web.WebUser.No);
                conn.SetCurrentDirectory("/FDB/NetDisk/" + userDept + "/" + BP.Web.WebUser.No);

                if (conn.FileExist(dkFile.OID + "." + ext) == false)
                {
                    // 向服务器上存放文件
                    conn.PutFile(Server.MapPath(localFile), dkFile.OID + "." + ext);
                }
            }
            catch
            {
                dkFile.DirectDelete();
                this.Alert("请检查FTP服务器配置，文件上传失败！");
                return;
            }
        }
        if (n == 0)
        {
            this.Alert("您还没有选择资源");

            return;
        }

        this.Alert("恭喜您，上传成功!");
    }
}
