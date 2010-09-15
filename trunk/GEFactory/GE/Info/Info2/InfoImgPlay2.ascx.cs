using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using BP.GE;
using BP.Sys;

public partial class GE_Info_InfoImgPlay2 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Info2s ens = new Info2s();
        ens.Retrieve(Info2Attr.InfoSta, "3");

        //图片的宽度
        int imgWidth = ens.GetEnsAppCfgByKeyInt("FlashWidth");
        //图片的高度
        int imgHeight = ens.GetEnsAppCfgByKeyInt("FlashHeight");
        bool IsShowTitle = ens.GetEnsAppCfgByKeyBoolen("IsShowTitle");
        int text_height = 20;
        if (!IsShowTitle)
        {
            this.PubCSS.Add("<style type='text/css'>#play2_info {display:none;}</style>");
            text_height = 0;
        }
        imgHeight = imgHeight - text_height;

        //css控制按钮区域和title区域的宽度
        this.PubCSS.Add("<style type='text/css'>#play2_info,#play2_list img,#play2_bg,#play2,#play2_text {width:" + imgWidth + "px;}</style>");
        this.PubCSS.Add("<style type='text/css'>#play2_list img,#play2 {height:" + imgHeight + "px;}</style>");
        this.PubCSS.Add("<style type='text/css'>#play2_info {margin-top:" + imgHeight + "px;}</style>");
        this.PubCSS.Add("<style type='text/css'>#play2_bg,#play2_text {margin-top:" + (imgHeight - 20) + "px;}</style>");
        string basePath = this.Request.ApplicationPath;
        int i = 1;
        foreach (Info2 en in ens)
        {
            if (BP.DA.DataType.IsImgExt(en.MyFileExt))
            {
                this.PubImgA.Add("<a href=\"" + "InfoDtl2.aspx?No=" + en.No + "\" target=\"_blank\"><img src=\"" + en.WebPath + "\" title=\"\" alt=\""
                                + en.Name + "\" onerror=\"this.src='" + basePath + "/GE/Info/Images/Default.jpg'\"/></a>");
                this.PubImgBtn.AddLi(i.ToString());
                i++;
            }
        }
    }
}
