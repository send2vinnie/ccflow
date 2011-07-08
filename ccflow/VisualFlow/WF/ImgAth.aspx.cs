using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class WF_ImgAth : BP.Web.WebPage
{
    #region 属性.
    public int H
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["H"]);
            }
            catch
            {
                return 120;
            }
        }
    }
    public int W
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["W"]);
            }
            catch
            {
                return 100;
            }
        }
    }
    public string ImgAth
    {
        get
        {
            return  this.Request.QueryString["ImgAth"];
        }
    }
    public string MyPK
    {
        get
        {
            return this.Request.QueryString["MyPK"];
        }
    }
    #endregion 属性.

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string url = "../DataUser/ImgAth/Def.jpg";

            string newName = ImgAth + MyPK;
            if (!string.IsNullOrEmpty(newName))
            {
                string sourceFile = Server.MapPath("../DataUser/ImgAth/Data/" + newName);
                if (System.IO.File.Exists(sourceFile))
                {
                    url = newName;
                }
            }
           

            txtPhotoUrl.Text = url;
            Page.ClientScript.RegisterStartupScript(this.GetType(),
            "js", "<script>ImageCut('" + url + "','"+W+"','"+H+"' )</script>");
        }
        else
        {
 
        }
        //this.Pub1.Clear();
        //this.Pub1.Add("<style type='text/css'>");
        //this.Pub1.Add("\t\n #container");
        //this.Pub1.Add("\t\n {");
        //this.Pub1.Add("\t\n width:500px;");
        //this.Pub1.Add("\t\n height:350px;");
        //this.Pub1.Add("\t\n margin:0px auto;");
        //this.Pub1.Add("\t\n border:solid 1px #7d9edb;");
        //this.Pub1.Add("\t\n padding:5px;");
        //this.Pub1.Add("\t\n }");
        //this.Pub1.Add("\t\n </style>");

        //this.Pub1.Add("\t\n <script type='text/javascript'>");
        //this.Pub1.Add("\t\n $().ready(function() {");
        //this.Pub1.Add("\t\n $.fn.bitmapCutter({");
        //this.Pub1.Add("\t\n src: 'ImgAth/Def.jpg',");

        //this.Pub1.Add("\t\n renderTo: '#container',");
        //this.Pub1.Add("\t\n holderSize: { width: 320, height: 240 },");
        //this.Pub1.Add("\t\n cutterSize: { width: "+this.W+", height: "+this.H+" },");
        //this.Pub1.Add("\t\n onGenerated: function(src) {");

        //this.Pub1.Add("\t\n },");
        //this.Pub1.Add("\t\n rotateAngle: 90,");
        //this.Pub1.Add("\t\n lang: {clockwise:'顺时针旋转{0}度.'}");
        //this.Pub1.Add("\t\n });");
        //this.Pub1.Add("\t\n })");
        //this.Pub1.Add("\t\n </script>");

    }
    public void v()
    {
        this.WinClose("ss/sdsd");
    }

    //确定按钮
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string str = txtPhotoUrl.Text.ToString();

        string newName = ImgAth + MyPK;
        newName = "11";
        if (string.IsNullOrEmpty(newName))
        {
            newName = System.Guid.NewGuid().ToString();
        }

        string type = str.Substring(str.LastIndexOf(".") + 1); //得到文件后缀名 

        CopyFile(str, "../DataUser/ImgAth/Data/" + newName + "." + type);

        this.WinClose("/DataUser/ImgAth/Data/" + newName + "." + type);
    }

    //复制文件
    public void CopyFile(string SourceFile, string ObjectFile)
    {
        string sourceFile = Server.MapPath(SourceFile);
        string objectFile = Server.MapPath(ObjectFile);
        if (System.IO.File.Exists(sourceFile))
        {
            System.IO.File.Copy(sourceFile, objectFile, true);
        }
    }

    //取消按钮
    protected void btnCancle_Click(object sender, EventArgs e)
    {

    }
}