﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class FileUpload : BP.Web.WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Files.Count > 0)
        {
            HttpPostedFile file = Request.Files[0];
            string msg = "";
            string error = "";
            if (file.ContentLength == 0)
                error = "文件长度为0";
            else
            {
                BaseFileUpload fileUpload = new BaseFileUpload();
                fileUpload.Sizes = 2048;
                fileUpload.FileType = "bmp|BMP|jpg|JPG|jpeg|JPEG|png|PNG|gif|GIF";
                //fileUpload.Path = "~/UploadFiles";
                fileUpload.Path = "../../DataUser/ImgAth/Upload";
                fileUpload.PostedFile = file;
                string newName = this.Request.QueryString["ImgAth"] + "_" + this.MyPK + ".png";
                string picName = fileUpload.Upload(newName);
                msg = "../../DataUser/ImgAth/Upload/" + picName;
            }
            //    ToLog("上传了一个名称为" + msg + "的图片!");
            string result = "{ error:'" + error + "', msg:'" + msg + "',msgWidth:'" + GetImage( msg , 120) + "'}";
            Response.Write(result);
            Response.End();
        }
    }
    public int GetImage(string url, int widthSize)
    {
        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(url)))
        {
            System.Drawing.Image imgOutput = System.Drawing.Bitmap.FromFile(System.Web.HttpContext.Current.Server.MapPath(url));
            if (imgOutput.Width > widthSize)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 1;
        }
    }
}