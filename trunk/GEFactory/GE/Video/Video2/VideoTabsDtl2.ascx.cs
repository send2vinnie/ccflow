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
using BP.GE;
using BP.Web;
using BP.Sys;
using System.Text;

public partial class GE_Video_VideoTabsDtl2 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Video2 en = new Video2();
        int i = en.Retrieve(Video2Attr.No, this.RefNo);
        if (i == 0)
        {
            this.Response.Write("<script>alert('该视频不存在或已被删除!');go.history(-1);</script>");
        }

        //设置页面标题
        try
        {
            this.Page.Title = en.Name;
        }
        catch
        {
        }

        //引用脚本
        this.Page.Response.Write("<link href='" + this.Request.ApplicationPath + "/GE/Video/CSS/VideoDtl.css' rel='stylesheet' type='text/css' />");
        //GroupTitle的样式
        string groupTitle = BP.Sys.EnsAppCfgs.GetValString("BP.GE.Video2s", "GroupTitleCSS");
        //是否启用评论和评价
        bool isEnableComment = EnsAppCfgs.GetValBoolen("BP.GE.Video2s", "IsEnableComment");
        bool isEnablePJ = EnsAppCfgs.GetValBoolen("BP.GE.Video2s", "IsEnablePJ");
        bool isEnableViewHistory = EnsAppCfgs.GetValBoolen("BP.GE.Video2s", "IsEnableViewHistory");

        //评论初始化
        if (isEnableComment)
        {
            PanelComment.Visible = true;
            GeComment1.RefOID = this.RefNo;
            //评论采用的分组
            GeComment1.GroupKey = EnsAppCfgs.GetValInt("BP.GE.Video2s", "PJGroupKey").ToString();
            this.Pub8.AddTD("class='" + groupTitle + "'", "相关评论");
        }
        //评价初始化
        if (isEnablePJ)
        {
            this.Pub7.AddTD("class='" + groupTitle + "'", "评价");
            PanelPJ.Visible = true;
            GePJ1.RefOID = this.RefNo;
        }

        //是否启用评论和评价
        bool isEnableSC = EnsAppCfgs.GetValBoolen("BP.GE.Video2s", "IsEnableSC");
        if (isEnableSC)
        {
            PanelSC.Visible = true;
        }

        //最近浏览初始化
        if (isEnableViewHistory == true && WebUser.No != null)
        {
            PanelViewHistory.Visible = true;
            BP.GE.GeViewEntity entity = new GeViewEntity(WebUser.No, WebUser.Name, this.RefNo, "BP.GE.Video2", en.Name);
            GeMyView1.MyView = entity;
        }

        //更新浏览次数
        en.ViewTimes += 1;
        en.DirectUpdate();

        VideoSort2s sorts = new VideoSort2s();
        sorts.RetrieveAll();
        foreach (VideoSort2 sort in sorts)
        {
            this.PubSort.Add("<a href='VideoTabsMore2.aspx?RefNo=" + sort.No + "'>" + sort.Name + "</a>&nbsp;");
        }

        //左侧视频播放区域
        this.PubVideo.Add("<div class='video_name'><h2>" + en.Name + "</h2></div>");
        this.Pub5.AddTD("class ='" + groupTitle + "'", "视频信息");
        this.Pub6.AddTD("class ='" + groupTitle + "'", "相关视频");

        SysFileManagers fs = en.HisSysFileManagers;
        SysFileManager f_vedio = fs.GetSysFileByAttrFileNo("Video2");

        int videoOID = 0;
        string videoExt = "";
        float videoSize = 0;
        string videoPath = "";
        if (f_vedio != null)
        {
            videoOID = f_vedio.OID;
            videoExt = f_vedio.MyFileExt;
            videoSize = f_vedio.MyFileSize;
            videoPath = f_vedio.WebPath;
        }
        Video2s vedios = new Video2s();
        int vedioWidth = vedios.GetEnsAppCfgByKeyInt("VideoWidth");
        int vedioHeight = vedios.GetEnsAppCfgByKeyInt("VideoHeight");

        //Flash播放器
        BP.GE.Ctrl.GEWebPlayer player = new BP.GE.Ctrl.GEWebPlayer();
        player.Width = vedioWidth;
        player.Height = vedioHeight;
        player.VideoSrc = videoPath;

        this.PubVideo.Add("<div class='video' style='text-align:center'>");
        this.PubVideo.Add(player);
        this.PubVideo.Add("</div>");

        //下载和加入收藏
        if (WebUser.No != null && WebUser.No != "")
        {
            this.PubDownLoad.Add("<span style='padding-right:10px;'><a title='' alt='下载到本地' href=\"" + this.Request.ApplicationPath + "/GE/Video/Video2/Do.aspx?FileOID=" + videoOID
                            + "&DoType=DownLoad\" target='_blank'><img src='" + this.Request.ApplicationPath + "/GE/Video/Images/Down.gif' alt='下载到本地' class='FavImgBtn' /></a></span>");
        }

        //视频详细信息说明
        this.PubVideoDoc.AddTable();
        this.PubVideoDoc.AddTR();
        this.PubVideoDoc.AddTD("class ='" + groupTitle + "'", "简介");
        this.PubVideoDoc.AddTREnd();

        this.PubVideoDoc.AddTR();
        this.PubVideoDoc.AddTDBigDoc(en.Doc);
        this.PubVideoDoc.AddTREnd();
        this.PubVideoDoc.AddTableEnd();

        this.Pub3.AddLi("视频编号：" + videoOID);
        this.Pub3.AddLi("分类：" + en.FK_TypeT);
        this.Pub3.AddLi("格式：" + videoExt);
        this.Pub3.AddLi("大小：" + WuXiaoyun.ConvertFileSize(videoSize));
        this.Pub3.AddLi("浏览次数：" + en.ViewTimes);
        this.Pub3.AddLi("更新日期：" + en.RDT);
        //}
        //else
        //{
        //    this.PubVideo.Add("该视频不存在或已被删除。");
        //    this.Pub3.Add("该视频不存在或已被删除。");
        //}
        //this.PubVideo.AddDivEnd();

        DataTable dt = new DataTable();
        dt.Columns.Add("ImgUrl");
        dt.Columns.Add("Title");
        dt.Columns.Add("No");
        dt.Columns.Add("ImgWidth");
        dt.Columns.Add("ImgHeight");
        dt.Columns.Add("ViewTimes");
        dt.Columns.Add("DefImgSrc");
        string DefImgSrc = HttpContext.Current.Request.ApplicationPath + "/GE/Video/Images/Default.jpg";

        //添加相关信息
        Video2s ens = new Video2s();
        int imgWidth = ens.GetEnsAppCfgByKeyInt("ImgWidth");
        int imgHeight = ens.GetEnsAppCfgByKeyInt("ImgHeight");
        int cols = ens.GetEnsAppCfgByKeyInt("XG_NumOfCol");
        int rows = ens.GetEnsAppCfgByKeyInt("XG_NumOfRow");
        int pageSize = rows * cols;
        this.GeImage1.GloRepeatColumns = cols;
        this.GeImage1.PageSize = pageSize;

        ens.RetrieveRecomByType(en.FK_Sort,-1);
        int count = 1;
        foreach (Video2 vedio in ens)
        {
            if(count > pageSize )
            {
                break;
            }
            if (vedio.No == this.RefNo)
            {
                continue;
            }

            SysFileManagers sfs = vedio.HisSysFileManagers;
            if (sfs == null)
            {
                continue;
            }
            SysFileManager sf_vedio = (SysFileManager)sfs.GetEntityByKey(SysFileManagerAttr.AttrFileNo, "Video2");
            if (sf_vedio == null)
            {
                continue;
            }
            dt.Rows.Add(vedio.WebPath, vedio.Name, vedio.No, imgWidth, imgHeight, vedio.ViewTimes, DefImgSrc);
            count++;
        }
        GeImage1.GloDBSource = dt;
        if (dt.Rows.Count == 0)
        {
            this.Pub4.Add("无");
        }

    }
}
