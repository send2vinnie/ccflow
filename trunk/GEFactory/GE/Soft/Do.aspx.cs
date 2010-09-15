using System;
using System.Web;
using BP.Web;
using BP.Sys;
using BP.GE;
using System.IO;

public partial class GE_Soft_Do : BP.Web.WebPage
{
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }

    public string FileOID
    {
        get
        {
            return this.Request.QueryString["FileOID"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "DownLoad":
                if (this.FileOID == null)
                    break;
                try
                {
                    SysFileManager file = new SysFileManager();
                    int i = file.Retrieve(SysFileManagerAttr.OID, FileOID);

                    if (i == 0)
                    {
                        this.Alert("该文件不存在或已被删除!");
                    }

                    //更新下载次数
                    string userNo = "";
                    if (WebUser.No != null && WebUser.No != "")
                    {
                        userNo = WebUser.No;
                    }
                    HttpCookie hc = System.Web.HttpContext.Current.Request.Cookies[userNo + "Soft" + file.OID.ToString()];
                    if (hc != null)
                    {
                        //下载
                        WuXiaoyun.HttpDownload(file.WebPath, file.MyFileName + "." + file.MyFileExt);
                        break;
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie(userNo + "Soft" + file.OID.ToString());
                        cookie.Expires.AddDays(1);
                        cookie.Value = file.OID.ToString();
                        System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                        //下载
                        if (WuXiaoyun.HttpDownload(file.WebPath, file.MyFileName + "." + file.MyFileExt))
                        {
                            //更新下载次数
                            Soft en = new Soft();
                            en.Retrieve(SoftAttr.No, file.RefVal.ToString());
                            en.DownTimes = (Convert.ToInt16(en.DownTimes) + 1).ToString();
                            en.DirectUpdate();
                        }
                    }
                    WinClose();
                }
                catch (Exception ex)
                {
                    this.Response.Write("<script>alert('下载失败，详细信息：" + ex.Message + "');window.opener=null;window.close();</script>");
                }

                break;
            default:
                break;
        }
    }
}