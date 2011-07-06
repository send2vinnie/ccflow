using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        this.Pub1.Clear();
        this.Pub1.Add("<style type='text/css'>");
        this.Pub1.Add("\t\n #container");
        this.Pub1.Add("\t\n {");
        this.Pub1.Add("\t\n width:500px;");
        this.Pub1.Add("\t\n height:500px;");
        this.Pub1.Add("\t\n margin:50px auto;");
        this.Pub1.Add("\t\n border:solid 1px #7d9edb;");
        this.Pub1.Add("\t\n padding:5px;");
        this.Pub1.Add("\t\n }");
        this.Pub1.Add("\t\n </style>");

        this.Pub1.Add("\t\n <script type='text/javascript'>");
        this.Pub1.Add("\t\n $().ready(function() {");
        this.Pub1.Add("\t\n $.fn.bitmapCutter({");
        this.Pub1.Add("\t\n src: './../DataUser/ImgAth/Def.jpg',");

        this.Pub1.Add("\t\n renderTo: '#container',");
        this.Pub1.Add("\t\n cutterSize: { width: "+this.W+", height: "+this.H+" },");
        this.Pub1.Add("\t\n onGenerated: function(src) {");
        this.Pub1.Add("\t\n },");
        this.Pub1.Add("\t\n rotateAngle: 90,");
        this.Pub1.Add("\t\n lang: {clockwise:'顺时针旋转{0}度.'}");
        this.Pub1.Add("\t\n });");
        this.Pub1.Add("\t\n })");
        this.Pub1.Add("\t\n </script>");

    }
    public void v()
    {

        this.WinClose("ss/sdsd");
    }

}