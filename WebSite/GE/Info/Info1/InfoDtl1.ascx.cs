using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Web;
using BP.En;
using BP.Sys;
using BP.DA;
using BP.GE;
using System.Text;
using System.IO;

public partial class Comm_GE_Info_InfoDtl1 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Info1s ens = new Info1s();
        int imgWidth = ens.GetEnsAppCfgByKeyInt("ImgWidth");
        int imgHeight = ens.GetEnsAppCfgByKeyInt("ImgHeight");
        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");

        //页面Title
        Info1 en = new Info1(this.RefNo);
        try
        {
            this.Page.Title = en.Name;
        }
        catch
        {
        }

        //更新浏览次数
        if (!IsPostBack)
        {
            en.NumRead += 1;
            en.DirectUpdate();
        }

        //是否启用评论和评价
        bool isEnableComment = ens.GetEnsAppCfgByKeyBoolen("IsEnableComment");
        bool isEnablePJ = ens.GetEnsAppCfgByKeyBoolen("IsEnablePJ");
        bool isEnableViewHistory = ens.GetEnsAppCfgByKeyBoolen("IsEnableViewHistory");

        //评论初始化
        if (isEnableComment)
        {
            PanelComment.Visible = true;
            GeComment1.RefOID = this.RefNo;
            //评价采用的分组
            GeComment1.GroupKey = ens.GetEnsAppCfgByKeyInt("PJGroupKey").ToString();
            this.Pub5.AddTD("class='" + groupTitle + "'", "相关评论");
        }

        //评价初始化
        if (isEnablePJ)
        {
            this.Pub4.AddTD("class='" + groupTitle + "'", "评价");
            PanelPJ.Visible = true;
            GePJ1.RefOID = this.RefNo;
        }

        //最近浏览初始化
        if (isEnableViewHistory == true && WebUser.No != null)
        {
            PanelViewHistory.Visible = true;
            BP.GE.GeViewEntity entity = new GeViewEntity(WebUser.No, WebUser.Name, this.RefNo, "BP.GE.Info1", en.Name);
            GeMyView1.MyView = entity;
            GeMyView1.MyHistoryNum = ens.GetEnsAppCfgByKeyInt("NumOfHistoryView");
        }

        //控制字符超长样式
        this.Add("<style type='text/css'>");
        this.Add("tr td li{overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;}");
        this.Add("</style>");

        //新闻标题
        this.PubH.Add("<div class='info_title' style='text-align:center'>");
        this.PubH.Add("<h2 style='font-size:20px;'>" + en.Name + "</h2><hr style='border:0px;border-top:1px dashed #ccc;'>");
        this.PubH.Add("<span>发布日期：" + en.RDT + "&nbsp;&nbsp;&nbsp;&nbsp;阅读次数：" + en.NumRead + "</span>");
        this.PubH.Add("</div>");

        //新闻内容
        this.PubContent.AddTR();
        this.PubContent.Add("<td >");
        this.PubContent.Add("<div class='info_main'>");
        //img图片
        string imgDiv = "<div style=\"text-align:center;width:100%;\">"
                        + "<img width=\"" + imgWidth + "px\" src=\"{0}\" onerror=\"this.src='" + this.Request.ApplicationPath + "/GE/Info/Images/Default.jpg'\" />"
                        + "<br/><span>{1}</span></div>";

        //可下载附件
        StringBuilder sbFile = new StringBuilder();
        string imgSrc = this.Request.ApplicationPath + "/Images/FileType/";

        if (en.WebPath != "" && en.WebPath != null)
        {
            //图片
            if (BP.DA.DataType.IsImgExt(en.MyFileExt))
            {
                if (en.InfoSta != "3")
                {
                    this.PubContent.Add(string.Format(imgDiv, en.WebPath, en.Name));
                }
            }
            //Flash
            else if (BP.GE.WuXiaoyun.IsVideoExt(en.MyFileExt))
            {
                //Flash播放器
                BP.GE.Ctrl.GEWebPlayer player = new BP.GE.Ctrl.GEWebPlayer();
                player.Width = imgWidth;
                player.Height = imgHeight;
                player.VideoSrc = en.WebPath;

                this.PubContent.Add("<div style='text-align:center;width:100%;'>");
                this.PubContent.Add(player);
                this.PubContent.Add("<br/>视频：" + en.Name + "</div>");
            }
            else
            {
                //可下载附件
                sbFile.Append("<li style=\"list-style:none\"><img style=\"margin-right:6px;border:0\" src=\"" + imgSrc + en.MyFileExt + ".gif\" onerror=\"this.src='"
                    + imgSrc + "Undefined.gif'\"/><a href=\"" + this.Request.ApplicationPath + "/GE/Info/Info1/Do.aspx?FileOID=" + en.No
                    + "&DoType=DownLoad\" target=\"_blank\">" + en.MyFileName + "." + en.MyFileExt + "</a></li>");
            }
        }

        BP.Sys.SysFileManagers sfs = en.HisSysFileManagers;
        foreach (BP.Sys.SysFileManager sf in sfs)
        {
            //图片
            if (BP.DA.DataType.IsImgExt(sf.MyFileExt))
            {
                this.PubContent.Add(string.Format(imgDiv, sf.WebPath, sf.MyFileName));
            }
            //Flash
            else if (BP.GE.WuXiaoyun.IsVideoExt(sf.MyFileExt))
            {
                //Flash播放器
                BP.GE.Ctrl.GEWebPlayer player = new BP.GE.Ctrl.GEWebPlayer();
                player.Width = imgWidth;
                player.Height = imgHeight;
                player.VideoSrc = sf.WebPath;

                this.PubContent.Add("<div style='text-align:center;width:100%;'>");
                this.PubContent.Add(player);
                this.PubContent.Add("<br/>视频：" + sf.MyFileName + "</div>");
            }
            else
            {
                //可下载附件
                sbFile.Append("<li style=\"list-style:none\"><img style=\"margin-right:6px;border:0\" src=\"" + imgSrc + sf.MyFileExt + ".gif\" onerror=\"this.src='"
                    + imgSrc + "Undefined.gif'\"/><a href=\"" + this.Request.ApplicationPath + "/GE/Info/Info1/Do.aspx?FileOID=" + sf.OID
                    + "&DoType=DownLoadSF\" target=\"_blank\">" + sf.MyFileName + "." + sf.MyFileExt + "</a></li>");
            }
        }

        this.PubContent.Add("<p class='BigDoc'>" + en.DocHtml + "</p>");
        this.PubContent.Add("<br/>");
        this.PubContent.Add("</div>");
        this.PubContent.AddTDEnd();
        this.PubContent.AddTREnd();

        //下载信息
        if (sbFile.ToString() != "")
        {
            this.PubContent.AddTR();
            this.PubContent.AddTD("style='background:#edf2fc;height:25px;padding:5px;'", "相关附件下载");
            this.PubContent.AddTREnd();

            this.PubContent.AddTR();
            this.PubContent.Add("<td style='padding:5px;'>");
            this.PubContent.AddUL("style='list-style-type:none;margin:0px;padding:0px'");
            this.PubContent.Add(sbFile.ToString());
            this.PubContent.AddULEnd();
            this.PubContent.AddTDEnd();
            this.PubContent.AddTREnd();
        }

        //this.PubContent.Add("<div id='back_to_top' style='display:none'><a href='#' >【回到顶部↑】</a></div>");

        //上一篇、下一篇
        string strPreviousInfo = "上一篇：无";
        string strNextInfo = "下一篇：无";
        Info1 previousInfo = new Info1();
        Info1 nextInfo = new Info1();

        //是否显示了类别
        int numOfTab = ens.GetEnsAppCfgByKeyInt("NumOfTab");
        //所有类别中排序
        if (numOfTab == 0)
        {
            previousInfo = en.PreviousInfoOfAll;
            nextInfo = en.NextInfoOfAll;
        }
        //当前类别中排序
        else
        {
            previousInfo = en.PreviousInfo;
            nextInfo = en.NextInfo;
        }

        if (previousInfo != null)
        {
            strPreviousInfo = "上一篇：<a href='InfoDtl1.aspx?No=" + previousInfo.No + "' target='_self'>" + previousInfo.Name + "</a>";
        }
        if (nextInfo != null)
        {
            strNextInfo = "下一篇：<a href='InfoDtl1.aspx?No=" + nextInfo.No + "' target='_self'>" + nextInfo.Name + "</a>";
        }

        this.Pub2.AddTR();
        this.Pub2.AddTD("class='" + groupTitle + "'", "相关链接");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<td class='TD'>");
        this.Pub2.Add("<ul class='last_and_nex'>");
        this.Pub2.Add("<li style='list-style-type:none'>" + strPreviousInfo + "</li>");
        this.Pub2.Add("<li style='list-style-type:none'>" + strNextInfo + "</li>");
        this.Pub2.Add("</ul>");
        this.Pub2.AddTDEnd();
        this.Pub2.AddTREnd();

        //相关信息
        this.PubXG.Add("<table class='Table' style='border-collapse: collapse;table-layout:fixed;margin-top:5px'>");
        this.PubXG.AddTR();
        this.PubXG.AddTD("class ='" + groupTitle + "'", "相关信息");
        this.PubXG.AddTREnd();

        this.PubXG.AddTR();
        this.PubXG.AddTDBegin();
        this.PubXG.AddUL("style='list-style-type:none;margin:0px;padding:0px'");
        Info1s infos = new Info1s();
        int relatedNum = infos.GetEnsAppCfgByKeyInt("NumOfRelatedInfo");
        infos.RetrieveByType(en.FK_Sort, relatedNum);

        foreach (Info1 info in infos)
        {
            if (info.No == en.No)
            {
                continue;
            }
            this.PubXG.Add("<li style='list-style-type:none'><a href='InfoDtl1.aspx?No=" + info.No + "' target='_self'>" + info.Name + "</a></li>");
        }
        this.PubXG.AddULEnd();
        if (infos == null)
        {
            this.PubXG.Add("无");
        }
        this.PubXG.AddTDEnd();
        this.PubXG.AddTREnd();
        this.PubXG.AddTableEnd();
    }
}
