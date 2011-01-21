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

public partial class GE_Comm_FtpDirReader : BP.Web.UC.UCBase3
{
    #region 属性：FTP地址
    private string ftpRootPath = "";
    #endregion

    public string FtpIP = GloFTP.FtpIP;
    public string FtpUser = GloFTP.FtpUser;
    public string FtpPass = GloFTP.FtpPass;
    //根目录
    public string FtpRootPath
    {
        get
        {
            return this.ftpRootPath;
        }
        set
        {
            this.ftpRootPath = value;
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

            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //引用css和js
        this.Page.Response.Write("<link href='" + this.Request.ApplicationPath + "/GE/Comm/FtpReader/dtree.css' rel='stylesheet' type='text/css' />");
        this.Response.Write("<script src='" + this.Request.ApplicationPath + "/GE/Comm/FtpReader/dtree.js' type='text/javascript'></script>");

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
        string baseUrl = this.Request.ApplicationPath + "/GE/Comm/FtpReader/";
        this.Response.Write("<base target='_self'/>");
        this.PubDir.Add("<script type='text/javascript'>");
        this.PubDir.Add("d = new dTree('d','" + baseUrl + "','fm');");
        //根目录标题
        string rootTitle = "";
        if (this.FtpRootPath == "/" || this.FtpRootPath == "")
        {
            rootTitle = "根目录";
        }
        else if (this.FtpRootPath.EndsWith("/"))
        {
            rootTitle = this.FtpRootPath.Substring(0, this.FtpRootPath.Length - 2);
        }
        try
        {
            rootTitle = this.FtpRootPath.Substring(this.FtpRootPath.LastIndexOf("/") + 1);
        }
        catch
        {
            rootTitle = rootTitle;
        }
        this.PubDir.Add("d.add(01,-1,'" + rootTitle + "');");

        //添加子目录
        AddChildDirs(this.FtpRootPath, "01");

        this.PubDir.Add("document.write(d);");
        this.PubDir.Add("</script>");
    }

    //添加目录子节点
    public void AddChildDirs(string parentPath, string pNo)
    {
        try
        {
            // 设置在服务器上的文件的路径
            FtpSupport.FtpConnection conn = new FtpSupport.FtpConnection(FtpIP, FtpUser, FtpPass);
            conn.SetCurrentDirectory(parentPath);

            Win32FindData[] files = conn.FindFiles();
            int dirNum = 1;
            foreach (Win32FindData f in files)
            {
                //文件夹
                if (f.FileAttributes == System.IO.FileAttributes.Directory)
                {
                    string dirNo = pNo + dirNum.ToString().PadLeft(2, '0');
                    this.PubDir.Add("d.add(" + dirNo + "," + pNo + ",'" + f.FileName + "','" + this.Request.ApplicationPath
                        + "/GE/Comm/FtpReader/FtpReader.aspx?FtpCurrentPath=" + HttpUtility.UrlEncode(conn.GetCurrentDirectory() + "/" + f.FileName) + "');");

                    //递归添加子目录
                    dirNum++;
                    AddChildDirs(parentPath + "/" + f.FileName, dirNo);
                }
            }
        }
        catch (Exception ex)
        {
            this.Alert("读取目录失败，详细信息：" + ex.Message);
        }

    }
}
