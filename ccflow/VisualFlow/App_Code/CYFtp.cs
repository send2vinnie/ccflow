using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Xml.Linq;
using System.Data;
using System.Configuration;

using Silverlight.DataSetConnector;
using BP.CY;
using BP.CY.Net;
using Discuz.Toolkit;

/// <summary>
///CYFtp 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class CYFtp : System.Web.Services.WebService
{
    private Dictionary<string, string> dicFtpConfig;
    private FTP ftp = null;

    public CYFtp()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        dicFtpConfig = Config.ParseDic(ConfigurationManager.AppSettings["TemplateFtpConfig"]);
        if (dicFtpConfig != null && dicFtpConfig.ContainsKey("server") && dicFtpConfig.ContainsKey("username") && dicFtpConfig.ContainsKey("pwd"))
        {
            ftp = new IISFtp(dicFtpConfig["server"], dicFtpConfig["username"], dicFtpConfig["pwd"]);
        }
        else
        {
            throw new Exception("ftp配置不正确");
        }
    }

    [WebMethod]
    public string CheckUser(string userName, string pass)
    {
        DiscuzSession session = DiscuzSessionHelper.GetSession();
        try
        {
            if (session.GetUserInfo((long)session.GetUserID(userName.Trim())).Password == Encrypt.MD5(pass.Trim()))
                return "success";
            else
                return "error:用户或者密码错误";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [WebMethod]
    public string RegUser(string userName, string password, string email)
    {
        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email))
        {
            DiscuzSession session = DiscuzSessionHelper.GetSession();
            if (session.GetUserID(userName.Trim()) > 0)
                return "error:用户名已经存在";
            else if (session.GetUserInfo(email) != null)
                return "error:Email已经存在";
            else if (session.Register(userName, password, email, false) > 0)
                return "success";
            else
                return "error:注册失败";
        }
        else
            return "error:输入有误";
    }

    /// <summary>
    /// 获取FTP服务器地址
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetServer()
    {
        return ftp.Server;
    }

    /// <summary>
    /// 获取ftp默认流程模板要目录
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetDefaultFlowRoot()
    {
        return "ftp://" + ftp.Server + "/" + dicFtpConfig["flowroot"];
    }

    /// <summary>
    /// 获取流程共享文件夹
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetFlowShareRoot()
    {
        return "ftp://" + ftp.Server + "/" + dicFtpConfig["shareflowroot"];
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="srcFilePath"></param>
    /// <param name="targetFilePath"></param>
    /// <returns></returns>
    [WebMethod]
    public string DownloadFile(string srcFilePath)
    {
        try
        {
            string savePath = Server.MapPath("~/temp/") + srcFilePath.Substring(srcFilePath.LastIndexOf("/") + 1);
            ftp.DownloadFile(srcFilePath, savePath);

            RemoveXMLBitmap(savePath);

            return savePath;
        }
        catch (Exception ex)
        {
            return "error:" + ex.Message;
        }
    }

    /// <summary>
    /// 删除预览图
    /// </summary>
    private void RemoveXMLBitmap(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                XDocument doc = XDocument.Load(path);
                XElement ele = doc.Root.Elements().Where(e => e.Name.LocalName.ToLower() == "bitmap").FirstOrDefault();

                if (ele != null)
                {
                    ele.Remove();
                }

                ele = doc.Root.Elements().Where(e => e.Name.LocalName == "Description").FirstOrDefault();
                if (ele != null)
                {
                    ele.Remove();
                }

                doc.Save(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    /// <summary>
    /// 导入工作流
    /// </summary>
    /// <param name="path"></param>
    /// <param name="typeCode"></param>
    /// <returns></returns>
    [WebMethod]
    public string ImportFlow(string path)
    {
        path = this.DownloadFile(path);

        if (!path.StartsWith("error") && File.Exists(path))
        {
            RemoveXMLBitmap(path);

            return path;
        }

        return "error:文件不存在";
    }

    #region 流程模板

    /// <summary>
    /// 读取ftp服务器xml文件内容
    /// </summary>
    /// <param name="serverPath"></param>
    /// <returns></returns>
    [WebMethod]
    public List<FsFileItem> GetFlowFiles(string serverDirectoryPath)
    {
        if (string.IsNullOrEmpty(serverDirectoryPath))
        {
            serverDirectoryPath = GetDefaultFlowRoot();
        }

        List<FsFileItem> lstFileItem = new List<FsFileItem>();
        FsFileItem fileItem = null;

        List<FsItem> lstItem = ftp.GetDirectoryList(serverDirectoryPath);
        string downloadDir = Server.MapPath("~/temp/");

        foreach (FsItem item in lstItem)
        {
            if (!string.IsNullOrEmpty(item.Path))
            {
                if (item.IsFolder)
                {
                    fileItem = new FsFileItem()
                    {
                        Name = item.Name,
                        Path = item.Path,
                        IsFolder = true
                    };
                }
                else
                {
                    ftp.DownloadFile(item.Path, downloadDir + item.Name);
                    while (true)
                    {
                        try
                        {
                            FileStream stream = File.Open(downloadDir + item.Name, FileMode.Append);
                            stream.Close();
                            break;
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }

                    fileItem = ParseFlowXmlFile(downloadDir + item.Name);

                    if (fileItem != null)
                    {
                        fileItem.Name = item.Name;
                        fileItem.Path = item.Path;
                        fileItem.IsFolder = false;
                    }

                    File.Delete(downloadDir + item.Name);
                }

                if (fileItem != null)
                {
                    lstFileItem.Add(fileItem);
                }
            }
        }

        return lstFileItem;
    }

    /// <summary>
    /// 获取所有文件夹
    /// </summary>
    /// <param name="serverDirectoryPath"></param>
    /// <returns></returns>
    [WebMethod]
    public List<FsItem> GetFolders(string serverDirectoryPath)
    {
        if (string.IsNullOrEmpty(serverDirectoryPath))
        {
            serverDirectoryPath = GetDefaultFlowRoot();
        }

        List<FsItem> items = ftp.GetDirectoryList(serverDirectoryPath);

        if (items != null && items.Count > 0)
        {
            items = items.Where(i => i.IsFolder).ToList();
        }

        return items;
    }

    /// <summary>
    /// 解析xml
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private FsFileItem ParseFlowXmlFile(string path)
    {
        FsFileItem item = null;

        if (!string.IsNullOrEmpty(path) && File.Exists(path))
        {
            try
            {
                XDocument doc = XDocument.Load(path);

                if (doc != null)
                {
                    item = new FsFileItem();
                    XElement ele = doc.Root.Elements().Where(e => e.Name.LocalName.ToLower() == "bitmap").FirstOrDefault();

                    if (ele != null)
                    {
                        item.Bitmap = ele.Value;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        return item;
    }

    /// <summary>
    /// 保存工作流的预览图
    /// </summary>
    /// <param name="fkFlow"></param>
    /// <param name="bitmapData"></param>
    /// <returns></returns>
    [WebMethod]
    public string SaveFlowBitmap(string fkFlow, string bitmapData, bool append)
    {
        BP.WF.Flow flT = new BP.WF.Flow(fkFlow);
        string dir = BP.SystemConfig.PathOfDataUser + @"\FlowDesc\" + flT.No + "." + flT.Name;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string path = dir + "\\" + flT.Name + "_Bitmap.xml";
        XDocument doc;

        if (!append)
        {
            doc = new XDocument();
            XElement ele = new XElement("Bitmap");
            ele.Value = bitmapData;

            doc.Add(ele);
        }
        else
        {
            doc = XDocument.Load(path);
            if (doc != null)
            {
                doc.Document.Root.Value += bitmapData;
            }
            else
            {
                doc = new XDocument();
                XElement ele = new XElement("Bitmap");
                ele.Value = bitmapData;

                doc.Add(ele);
            }
        }

        doc.Save(path);

        return path;
    }

    /// <summary>
    /// 将指定工作流上传到指定路径
    /// </summary>
    /// <param name="fkFlow"></param>
    /// <param name="serverDirPath"></param>
    /// <returns></returns>
    [WebMethod]
    public string UploadFlow(string fkFlow, string serverDirPath, string description)
    {
        BP.WF.Flow flT = new BP.WF.Flow(fkFlow);
        string fileXml = flT.GenerFlowXmlTemplete();

        string bitmapPath = fileXml + "\\" + flT.Name + "_Bitmap.xml";
        if (File.Exists(bitmapPath))
        {
            XDocument doc = XDocument.Load(fileXml + "\\" + flT.Name + ".xml");
            XDocument docBitmap = XDocument.Load(bitmapPath);

            if (doc != null && docBitmap != null)
            {
                doc.Document.Root.Add(docBitmap.Document.Root);
                doc.Document.Root.Add(new XElement("Description", description));

                doc.Save(fileXml + "\\" + flT.Name + ".xml");
            }

            File.Delete(bitmapPath);
        }

        try
        {
            ftp.UploadFile(fileXml + "\\" + flT.Name + ".xml", serverDirPath + "\\" + flT.Name + ".xml");
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "success";
    }

    #endregion
}

[Serializable]
public class FsFileItem
{
    public string Name { get; set; }

    public string Path { get; set; }

    public string Bitmap { get; set; }

    public bool IsFolder { get; set; }
}
