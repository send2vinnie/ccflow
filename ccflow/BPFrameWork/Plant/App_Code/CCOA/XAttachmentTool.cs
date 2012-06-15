using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web;

/// <summary>
///XAttachmentTool 的摘要说明
/// </summary>
public class XAttachmentTool
{
    /// <summary>
    /// 增加附件
    /// </summary>
    /// <param name="fileFullName">文件名</param>
    /// <param name="attachmentName">附件名</param>
    /// <param name="uploadFilePath">上传路径</param>
    /// <param name="currentUser">操作人</param>
    /// <returns></returns>
    public static void InsertAttachment(string fileFullName, string attachmentName, string uploadFilePath, string currentUser, out string No)
    {
        BP.CCOA.OA_Attachment attachment = new BP.CCOA.OA_Attachment();
        No = Guid.NewGuid().ToString();
        attachment.No = No;
        attachment.AttachmentName = fileFullName;
        attachment.FileNeme = attachmentName;
        attachment.FilePath = uploadFilePath;
        string extention = Path.GetExtension(fileFullName);
        attachment.CreateTime = DateTime.Now;
        attachment.Suffix = extention;
        attachment.Uploader = BP.Web.WebUser.No;
        attachment.Remarks = "";
        attachment.Insert();
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="fileUpload"></param>
    /// <param name="physicalApplicationPath"></param>
    /// <param name="page"></param>
    /// <param name="encodeSaveFile"></param>
    /// <returns></returns>
    public static string UploadFile(System.Web.UI.WebControls.FileUpload fileUpload, string physicalApplicationPath, System.Web.UI.Page page, string encodeSaveFile)
    {
        string[] extension = new string[] { ".doc", ".docx", ".txt", ".png", ".jpg", ".bmp", ".pdf", ".xml", ".dwg", ".ppt" };
        string fileName = fileUpload.FileName;
        string ext = Path.GetExtension(fileName);
        if (!extension.Contains(ext))
        {
            Lizard.Common.MessageBox.Show(page, "文件类型错误");
            //this.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + "文件类型错误" + "');</script>");
            return "";
        }
        if (fileUpload.HasFile)
        {
            string saveDir = @"CCOA\Upload\uploadfiles\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string appPath = physicalApplicationPath;
            string path = appPath + saveDir;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string savefile = DateTime.Now.ToFileTimeUtc().ToString() + ext;
            string savePath = path + encodeSaveFile;

            fileUpload.SaveAs(savePath);

            return savePath;
        }
        else
        {
            return "";
        }
    }
}