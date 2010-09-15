using System;
using System.Web;
using BP.Web;
using BP.Sys;
using BP.GE;
using System.IO;

public partial class GE_Video_Do : BP.Web.WebPage
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
        try
        {
            SysFileManager file = new SysFileManager();
            int i = file.Retrieve(SysFileManagerAttr.OID, FileOID);

            if (i == 0)
            {
                throw new Exception("该视频不存在或已被删除!");
            }

            if (BP.Edu.EduUser.No == null || BP.Edu.EduUser.No == "" || BP.Edu.EduUser.No.Contains("admin"))
            {
                Response.Redirect(this.Request.ApplicationPath + "/Port/SignIn.aspx?Jurl=" + Request.RawUrl);
                Response.End();
            }

            //更新下载次数
            string userNo = "";
            if (WebUser.No != null && WebUser.No != "")
            {
                userNo = WebUser.No;
            }
            HttpCookie hc = System.Web.HttpContext.Current.Request.Cookies[userNo + "Video" + file.OID.ToString()];
            if (hc != null)
            {
                //下载
                WuXiaoyun.HttpDownload(file.WebPath, file.MyFileName + "." + file.MyFileExt);
            }
            else
            {
                HttpCookie cookie = new HttpCookie(userNo + "Video" + file.OID.ToString());
                cookie.Expires.AddDays(1);
                cookie.Value = file.OID.ToString();
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                //下载
                if (WuXiaoyun.HttpDownload(file.WebPath, file.MyFileName + "." + file.MyFileExt))
                {
                    //更新下载次数
                    Video en = new Video();
                    en.Retrieve(VideoAttr.No, file.RefVal.ToString());
                    en.DownTimes = en.DownTimes + 1;
                    en.DirectUpdate();
                }
            }
            this.Response.Write("<script>window.opener=null;window.close();</script>");
        }
        catch (Exception ex)
        {
            this.Response.Write("<script>alert('下载失败，详细信息：" + ex.Message + "');window.opener=null;window.close();</script>");
        }
    }
}