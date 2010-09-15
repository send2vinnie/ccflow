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
        switch (this.DoType)
        {
            case "DownLoad":
                if (this.FileOID == null)
                    break;

                SysFileManager file = new SysFileManager();
                int i = file.Retrieve(SysFileManagerAttr.OID, FileOID);

                if (i == 0)
                {
                    throw new Exception("该视频不存在或已被删除!");
                }
                try
                {
                    //更新下载次数
                    string userNo = "";
                    if (WebUser.No != null && WebUser.No != "")
                    {
                        userNo = WebUser.No;
                    }
                    HttpCookie hc = System.Web.HttpContext.Current.Request.Cookies[userNo + "Video1" + file.OID.ToString()];
                    if (hc != null)
                    {
                        //下载
                        HttpDownload(file.WebPath, file.MyFileName + "." + file.MyFileExt);
                        break;
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie(userNo + "Video1" + file.OID.ToString());
                        cookie.Expires.AddDays(1);
                        cookie.Value = file.OID.ToString();
                        System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                        //下载
                        if (HttpDownload(file.WebPath, file.MyFileName + "." + file.MyFileExt))
                        {
                            //更新下载次数
                            Video1 en = new Video1();
                            en.Retrieve(Video1Attr.No, file.RefVal.ToString());
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

                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="Uri">"http://localhost/AXT/Data/BP.GE.ZTDtl/" + fullFileName 资源路径</param>
    /// <param name="FullFileName">文件名</param>
    /// <returns></returns>
    public bool HttpDownload(string Uri, string FullFileName)
    {
        try
        {
            const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力 
            byte[] buffer = new byte[ChunkSize];

            Response.Clear();
            System.IO.FileStream iStream = System.IO.File.OpenRead(Server.MapPath(Uri));
            long dataLengthToRead = iStream.Length;//获取下载的文件总大小 
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FullFileName));
            while (dataLengthToRead > 0 && Response.IsClientConnected)
            {
                int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小 
                Response.OutputStream.Write(buffer, 0, lengthRead);
                dataLengthToRead = dataLengthToRead - lengthRead;
                Response.Flush();
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            return false;
        }
    }
}