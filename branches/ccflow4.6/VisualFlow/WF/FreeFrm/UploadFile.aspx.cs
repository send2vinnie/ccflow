﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;

public partial class WF_FreeFrm_UploadFile : WebPage
{
    /// <summary>
    /// ath.
    /// </summary>
    public string Ath
    {
        get
        {
            return this.Request.QueryString["Ath"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub1.AddFieldSet("文件上传");
        FileUpload fu = new FileUpload();
        fu.ID = "s";
       // fu.Width = 300;
        this.Pub1.Add(fu);
        this.Pub1.AddFieldSetEnd();
    }
}