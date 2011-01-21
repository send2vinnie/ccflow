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
using FtpSupport;
using BP.GE;

public partial class GE_Comm_FtpReader : BP.Web.UC.UCBase3
{
    public string FtpIP = GloFTP.FtpIP;
    public string FtpUser = GloFTP.FtpUser;
    public string FtpPass = GloFTP.FtpPass;
    public static string root = "";
    //根目录
    public string FtpRootPath
    {
        get
        {
            if (root == "")
            {
                root = this.Request.QueryString["FtpRootPath"];
                if (root == null)
                    root = GloFTP.FtpPath;
            }

            return HttpUtility.UrlDecode(root);
        }
    }
    //当前目录
    public string FtpCurrentPath
    {
        get
        {
            string s = this.Request.QueryString["FtpCurrentPath"];
            if (s == null)
                s = this.FtpRootPath;

            return HttpUtility.UrlDecode(s);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FtpSupport.FtpConnection conn = new FtpSupport.FtpConnection(FtpIP, FtpUser, FtpPass);
            conn.SetCurrentDirectory(this.FtpCurrentPath);
            //网站根目录
            string appPath = this.Request.ApplicationPath;

            string currentDir = conn.GetCurrentDirectory();
            if (currentDir.EndsWith("/"))
            {
                currentDir = currentDir.Remove(currentDir.LastIndexOf("/"));
            }
            string[] dirNames = currentDir.Split('/');
            string dirHrefs = "";
            string tempDir = "";

            //获取基路径长度
            string rootPath = FtpRootPath;
            if (!rootPath.StartsWith("/") && rootPath != "")
            {
                rootPath = "/" + rootPath;
            }
            int baseCount = rootPath.Split('/').Length - 1;

            //添加目录导航
            for (int count = dirNames.Length - 1; count >= baseCount; count--)
            {
                if (dirNames[count] == "")
                {
                    continue;
                }
                string imgSrc = appPath + "/GE/FDB/img/dir.gif";
                //根目录
                if (count == baseCount)
                {
                    imgSrc = appPath + "/GE/FDB/img/Root.gif";
                }
                dirHrefs = "<a href='?FtpCurrentPath=" + HttpUtility.UrlEncode(currentDir) + "&FtpRootPath=" + HttpUtility.UrlEncode(this.FtpRootPath) + "'><img src='" + imgSrc + "' border='0' />" + dirNames[count] + "</a> \\" + dirHrefs;
                currentDir = currentDir.Remove(currentDir.LastIndexOf("/"));
            }
            this.AddTable("width='100%'");
            // this.AddTableBarDef(null, "<span><Img src='" + appPath + "/GE/FDB/img/Root.gif' border=0/>根目录\\</span>" + dirHrefs, 5, null);
            //this.AddTableBarDef(null, "<a href='?FtpCurrentPath=" + FtpRootPath + "'><Img src='" + appPath + "/GE/FDB/img/Root.gif' border=0/>当前目录：</a>" + dirHrefs, 5, null);
            this.AddTableBarDef(null, dirHrefs, 6, null);
            this.AddTR("class='th'");
            this.AddTDTitle("style='width:2%'", "序");
            this.AddTDTitle("style='width:35%'", "名称");
            this.AddTDTitle("style='width:10%'", "大小");
            this.AddTDTitle("style='width:10%'", "类型");
            this.AddTDTitle("style='width:10%'", "修改日期");
            this.AddTDTitle("style='width:10%'", "下载");
            this.AddTREnd();

            //返回上一级目录
            //if (dirNames.Length > baseCount)
            if (conn.GetCurrentDirectory() != this.FtpRootPath)
            {
                this.AddTR();
                this.AddTD();
                this.AddTD("<a href='?FtpCurrentPath=" + HttpUtility.UrlEncode(this.FtpCurrentPath.Remove(FtpCurrentPath.LastIndexOf("/"))) + "&FtpRootPath="
                    + HttpUtility.UrlEncode(this.FtpRootPath) + "'><img src='" + appPath + "/GE/FDB/img/folderup.png' border='0' />......</a>");
                this.AddTD();
                this.AddTD();
                this.AddTD();
                this.AddTD();
                this.AddTREnd();
            }

            //资源列表
            Win32FindData[] files = conn.FindFiles();
            int i = 0;
            foreach (Win32FindData f in files)
            {
                //文件夹
                if (f.FileAttributes == System.IO.FileAttributes.Directory)
                {
                    i++;
                    this.AddTR();
                    this.AddTD(i);

                    this.AddTDA("?FtpCurrentPath=" + HttpUtility.UrlEncode(conn.GetCurrentDirectory()) + "/" + f.FileName + "&FtpRootPath=" + this.Server.UrlEncode(this.FtpRootPath), "<img src='" + appPath + "/GE/FDB/img/Dir.gif' border='0' />" + f.FileName);
                    this.AddTD();
                    this.AddTD("文件夹");
                    this.AddTD(f.LastWriteTime.ToString("yyyy-MM-dd"));
                    this.AddTD();
                    this.AddTREnd();
                }
                //文件
                else
                {
                    i++;
                    this.AddTR();
                    this.AddTD(i);
                    string ext = f.FileName.Substring(f.FileName.LastIndexOf(".") + 1);
                    this.AddTDA(appPath + "/GE/Comm/FtpReader/FtpResDtl.aspx?FileDir=" + HttpUtility.UrlEncode(conn.GetCurrentDirectory() + "/" + f.FileName),
                                "<img src=\"" + appPath + "/Images/FileType/" + ext + ".gif\" border=\"0\" onerror=\"this.src='" + appPath + "/Images/FileType/Undefined.gif'\"/>" + f.FileName, "_blank");
                    this.AddTD(WuXiaoyun.ConvertFileSize(f.FileSize));
                    this.AddTD(ext);
                    this.AddTD(f.LastWriteTime.ToString("yyyy-MM-dd"));
                    this.AddTDA(appPath + "/GE/Comm/FtpReader/Do.aspx?DoType=DownLoad&FileDir=" + HttpUtility.UrlEncode(conn.GetCurrentDirectory() + "/" + f.FileName),
                                "<img style='margin-left:38%' src=\"" + appPath + "/GE/Comm/FtpReader/img/icon_down.gif\" border=\"0\" />", "_blank");
                    this.AddTREnd();
                }
            }

        }
        catch (Exception ex)
        {
            this.Response.Write("<script>alert('目录读取失败，详细信息：" + ex.Message + "');history.go(-1);</script>");
        }
        this.AddTableEnd();
    }
}
