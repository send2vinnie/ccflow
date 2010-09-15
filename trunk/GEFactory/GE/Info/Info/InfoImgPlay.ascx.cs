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

public partial class GE_Info_InfoImgPlay : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Infos ens = new Infos();
        ens.Retrieve(InfoAttr.InfoSta, "3");

        //图片的宽度
        int imgWidth = ens.GetEnsAppCfgByKeyInt("FlashWidth");
        //图片的高度
        int imgHeight = ens.GetEnsAppCfgByKeyInt("FlashHeight");
        bool IsShowTitle = ens.GetEnsAppCfgByKeyBoolen("IsShowTitle");
        int text_height = 20;
        if (!IsShowTitle)
        {
            this.PubCSS.Add("<style type='text/css'>#play_info {display:none;}</style>");
            text_height = 0;
        }
        imgHeight = imgHeight - text_height;

        //css控制按钮区域和title区域的宽度
        this.PubCSS.Add("<style type='text/css'>#play_info,#play_list img,#play_bg,#play,#play_text {width:" + imgWidth + "px;}</style>");
        this.PubCSS.Add("<style type='text/css'>#play_list img,#play {height:" + imgHeight + "px;}</style>");
        this.PubCSS.Add("<style type='text/css'>#play_info {margin-top:" + imgHeight + "px;}</style>");
        this.PubCSS.Add("<style type='text/css'>#play_bg,#play_text {margin-top:" + (imgHeight - 20) + "px;}</style>");
        string basePath = this.Request.ApplicationPath;
        int i = 1;
        foreach (Info en in ens)
        {
            if (BP.DA.DataType.IsImgExt(en.MyFileExt))
            {
                this.PubImgA.Add("<a href=\"" + "InfoDtl.aspx?No=" + en.No + "\" target=\"_blank\"><img src=\"" + en.WebPath + "\" title=\"\" alt=\""
                                + en.Name + "\" onerror=\"this.src='" + basePath + "/GE/Info/Images/Default.jpg'\"/></a>");
                this.PubImgBtn.AddLi(i.ToString());
                i++;
            }
        }
    }
}
