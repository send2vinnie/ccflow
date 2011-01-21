using System;
using System.Web;
using BP.Web;
using BP.Sys;
using BP.GE;
using System.IO;

public partial class GE_Info2_Do : BP.Web.WebPage
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
            switch (this.DoType)
            {
                case "DownLoad":
                    if (this.FileOID == null)
                        break;

                    //下载
                    Info2 en = new Info2(FileOID);
                    HttpDownload(en.WebPath, en.MyFileName + "." + en.MyFileExt);
                    this.Response.Write("<script>window.opener=null;window.close();</script>");
                    break;
                case "DownLoadSF":
                    if (this.FileOID == null)
                        break;

                    //下载
                    SysFileManager file = new SysFileManager();
                    file.Retrieve(SysFileManagerAttr.OID, FileOID);
                    HttpDownload(file.WebPath, file.MyFileName + "." + file.MyFileExt);
                    this.Response.Write("<script>window.opener=null;window.close();</script>");
                    break;
                default:
                    break;
            }
        }
        catch
        {
            this.Response.Write("<script>alert('文件不存在或已被删除!');window.opener=null;window.close();</script>");
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
                Response.Flush();
                dataLengthToRead = dataLengthToRead - lengthRead;
            }
            Response.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
}