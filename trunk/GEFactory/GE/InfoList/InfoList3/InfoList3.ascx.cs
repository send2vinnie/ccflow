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
using BP.DA;
using BP.GE;
using System.Text;


public partial class GE_Info_InfoList3 : BP.Web.UC.UCBase3
{
    //DoType不为空时，当前为更多页面
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string groupTitle = BP.Sys.EnsAppCfgs.GetValString("BP.GE.InfoList3s", "GroupTitleCSS");

        //输出列表(首页或更多页面)
        if (this.RefNo == null)
        {
            PanelList.Visible = true;
            InfoList3s ens = new InfoList3s();

            int cols = 1;
            //GroupTitle的样式
            string title = ens.GetEnsAppCfgByKeyString("Title");
            this.PubList.AddTable();
            this.PubList.AddTR();
            int colspan = 3;

            //更多页面
            if (this.DoType != null)
            {
                cols = ens.GetEnsAppCfgByKeyInt("M_NumOfCol");
                int rows = ens.GetEnsAppCfgByKeyInt("M_NumOfRow");
                colspan = 3 * cols;
                this.PubList.AddTD("colspan='" + colspan + "' class='" + groupTitle + "'", title);

                //翻页控件
                int pageSize = cols * rows;
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(InfoList3Attr.InfoListSta, "!=", "0");
                qo.addOrderByDesc(InfoList3Attr.RDT);
                this.PubPage.Add("<div class='divPage' style='text-align:right;'>");
                this.PubPage.BindPageIdx(qo.GetCount(), pageSize, this.PageIdx, "InfoList3.aspx?DoType=" + this.DoType);
                qo.DoQuery("No", pageSize, this.PageIdx);
                this.PubPage.Add("</div>");
            }
            else
            {
                this.PubPage.Clear();
                cols = ens.GetEnsAppCfgByKeyInt("NumOfCol");
                colspan = 3 * cols;
                //更多页面的打开方式
                int target = ens.GetEnsAppCfgByKeyInt("M_Target");
                string strTarget = "_self";
                if (target == 1)
                {
                    strTarget = "_blank";
                }

                this.PubList.AddTD("colspan='" + colspan + "' class='" + groupTitle + "'", "<a target='" + strTarget + "' href='InfoList3.aspx?DoType=ShowMore' class='gengduo' style='float:right'>更 多...</a>" + title);
                ens.RetrieveAll();
            }

            #region 开始 [  InfoList3 ] 的矩阵输出

            //控制字符超长样式
            this.PubList.Add("<style type='text/css'>");
            this.PubList.Add("tr td {overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;}");
            this.PubList.Add("</style>");

            //定义显示列数 从0开始。
            this.PubList.AddTREnd();
            int idx = -1;
            foreach (InfoList3 en in ens)
            {
                idx++;

                //一条记录的详细信息
                this.PubList.AddTDBegin();

                this.PubList.Add("<table width='100%' cellspacing='0' style='table-layout:fixed'>");
                this.PubList.AddTR();
                this.PubList.AddTD("style='border:0'", "<a style='width:98%;' href='InfoList3.aspx?RefNo=" + en.No + "' target='_blank'>" + en.Name + "</a>");
                this.PubList.AddTD("style='width:80px;border:0'", "阅读次数：" + en.NumRead);
                this.PubList.AddTD("style='width:62px;border:0'", en.RDT);
                this.PubList.AddTREnd();
                this.PubList.AddTableEnd();

                this.PubList.AddTDEnd();

                if (idx == cols - 1)
                {
                    idx = -1;
                    this.PubList.AddTREnd();
                }
            }

            while (idx != -1)
            {
                idx++;
                if (idx == cols - 1)
                {
                    idx = -1;
                    this.PubList.AddTD();
                    this.PubList.AddTREnd();
                }
                else
                {
                    this.PubList.AddTD();
                }
            }
            this.PubList.AddTableEnd();

            #endregion 结束  [  InfoList3 ]  矩阵输出
        }
        //详细信息
        else
        {
            PanelDtl.Visible = true;
            InfoList3 en = new InfoList3(this.RefNo);
            if (en == null)
            {
                return;
            }

            //页面Title
            try
            {
                this.Page.Title = en.Name;
            }
            catch
            {
            }

            //标题
            this.PubTitle.AddTR();
            this.PubTitle.AddTDTitle(en.Name);
            this.PubTitle.AddTREnd();
            this.PubTitle.AddTR();
            this.PubTitle.AddTDCenter("更新日期：" + en.RDT + "&nbsp;&nbsp;&nbsp;&nbsp;浏览次数：" + en.NumRead);
            this.PubTitle.AddTREnd();

            //更新浏览次数
            en.NumRead += 1;
            en.Update();

            //详细信息
            int imgWidth = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.InfoList3s", "ImgWidth");
            int imgHeight = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.InfoList3s", "ImgHeight");

            this.PubContent.AddTR();
            this.PubContent.AddTDBegin();
            this.PubContent.Add("<div class='info_main'>");
            //img图片
            string imgDiv = "<div style='text-align:center;width:100%;margin-bottom:5px'>"
                            + "<img width='" + imgWidth + "px' height='" + imgHeight + "px' src='{0}' />"
                            + "<br/><span>{1}</span></div>";
            //可下载附件
            StringBuilder sbFile = new StringBuilder();
            string imgSrc = this.Request.ApplicationPath + "/Images/FileType/";

            if (en.WebPath != "" && en.WebPath != null)
            {
                //图片
                if (BP.DA.DataType.IsImgExt(en.MyFileExt))
                {
                    this.PubContent.Add(string.Format(imgDiv, en.WebPath, en.Name));
                }
                //Flash
                else if (BP.GE.WuXiaoyun.IsVideoExt(en.MyFileExt))
                {
                    //Flash播放器
                    BP.GE.Ctrl.GEWebPlayer player = new BP.GE.Ctrl.GEWebPlayer();
                    player.Width = imgWidth;
                    player.Height = imgHeight;
                    player.VideoSrc = en.WebPath;

                    this.PubContent.Add("<div style='text-align:center;width:100%;margin-bottom:5px'>");
                    this.PubContent.Add(player);
                    this.PubContent.Add("<br/><span>视频：" + en.Name + "</span></div>");
                }
                else
                {
                    //可下载附件
                    sbFile.Append("<li style=\"list-style:none\"><img style=\"margin-right:10px;border:0\" src=\"" + imgSrc + en.MyFileExt + ".gif\" onerror=\"this.src='"
                        + imgSrc + "Undefined.gif'\"/><a href=\"" + this.Request.ApplicationPath + "/GE/Info/InfoList3/Do.aspx?FileOID=" + en.No
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
                    sbFile.Append("<li style=\"list-style:none\"><img style=\"margin-right:10px;border:0\" src=\"" + imgSrc + sf.MyFileExt + ".gif\" onerror=\"this.src='"
                        + imgSrc + "Undefined.gif'\"/><a href=\"" + this.Request.ApplicationPath + "/GE/Info/InfoList3/Do.aspx?FileOID=" + sf.OID
                        + "&DoType=DownLoad\" target=\"_blank\">" + sf.MyFileName + "." + sf.MyFileExt + "</a></li>");
                }
            }

            this.PubContent.Add("<p class='BigDoc'>" + en.DocHtml + "</p>");
            this.PubContent.Add("</div>");
            this.PubContent.AddTDEnd();
            this.PubContent.AddTREnd();

            ////下载信息
            if (sbFile.ToString() != "")
            {
                //    sbFile.Insert(0, "<div class='fjxz'><div class='fjxz_title'>下载</div><ul>");
                //    sbFile.Append("</ul></div>");
                this.PubContent.AddTR();
                //this.PubContent.AddTDBegin("class='" + groupTitle + "'");
                this.PubContent.AddTD("class='" + groupTitle + "'", "下载");
                this.PubContent.AddTREnd();

                this.PubContent.AddTR();
                this.PubContent.AddTDBegin();
                this.PubContent.Add(sbFile.ToString());
                this.PubContent.AddTDEnd();
                this.PubContent.AddTREnd();
            }


            //上一篇、下一篇
            string strPreviousInfo = "上一篇：无";
            string strNextInfo = "下一篇：无";
            InfoList3 previousInfo = new InfoList3();
            previousInfo = en.PreviousInfo;
            if (previousInfo != null)
            {
                strPreviousInfo = "<li style='list-style:none'>上一篇：<a href='InfoList3.aspx?RefNo=" + previousInfo.No + "' target='_self'>" + previousInfo.Name + "</a></li>";
            }
            InfoList3 nextInfo = new InfoList3();
            nextInfo = en.NextInfo;
            if (nextInfo != null)
            {
                strNextInfo = "<li style='list-style:none'>下一篇：<a href='InfoList3.aspx?RefNo=" + nextInfo.No + "' target='_self'>" + nextInfo.Name + "</a></li>";
            }
            this.PubContent.AddTR();
            //this.PubContent.AddTDBegin("class='" + groupTitle + "'");
            this.PubContent.AddTD("class='" + groupTitle + "'", "相关链接");
            this.PubContent.AddTREnd();

            this.PubContent.AddTR();
            this.PubContent.AddTDBegin();
            this.PubContent.Add(strPreviousInfo);
            this.PubContent.Add(strNextInfo);
            this.PubContent.AddTDEnd();
            this.PubContent.AddTREnd();

            //是否启用评论和评价
            bool isEnableComment = BP.Sys.EnsAppCfgs.GetValBoolen("BP.GE.InfoList3s", "IsEnableComment");
            bool isEnablePJ = BP.Sys.EnsAppCfgs.GetValBoolen("BP.GE.InfoList3s", "IsEnablePJ");

            //是否启用历史浏览
            bool isEnableViewHistory = BP.Sys.EnsAppCfgs.GetValBoolen("BP.GE.InfoList3s", "IsEnableViewHistory");
            if (isEnableViewHistory && WebUser.No != null && WebUser.No != "")
            {
                PanelHisView.Visible = true;
                GeMyView1.MyHistoryNum = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.InfoList3s", "NumOfHistoryView");
                BP.GE.GeViewEntity entity = new GeViewEntity(WebUser.No, WebUser.Name, this.RefNo, "BP.GE.InfoList3", en.Name);
                GeMyView1.MyView = entity;
            }

            //评论初始化
            if (isEnableComment)
            {
                PanelComment.Visible = true;
                GeComment1.RefOID = this.RefNo;
                //评论采用的分组
                GeComment1.GroupKey = BP.Sys.EnsAppCfgs.GetValInt("BP.GE.InfoList3s", "PJGroupKey").ToString();
                this.PubComTitle.AddTD("class='" + groupTitle + "'", "相关评论");
            }
            //评价初始化
            if (isEnablePJ)
            {
                this.PubPJTitle.AddTD("class='" + groupTitle + "'", "评价");
                PanelPJ.Visible = true;
                GePJ1.RefOID = this.RefNo;
            }
        }
    }
}
