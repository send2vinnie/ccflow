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

public partial class Comm_GE_Soft_SoftDtl : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GroupTitle的样式
        string groupTitle = BP.Sys.EnsAppCfgs.GetValString("BP.GE.Softs", "GroupTitleCSS");
        //是否启用评论和评价
        bool isEnableComment = EnsAppCfgs.GetValBoolen("BP.GE.Softs", "IsEnableComment");
        bool isEnablePJ = EnsAppCfgs.GetValBoolen("BP.GE.Softs", "IsEnablePJ");
        if (isEnableComment)
        {
            PanelComment.Visible = true;
            GeComment1.RefOID = this.RefNo;
            //评价采用的分组
            GeComment1.GroupKey = EnsAppCfgs.GetValInt("BP.GE.Softs", "PJGroupKey").ToString();
            this.Pub3.AddTD("class='" + groupTitle + "'", "相关评论");
        }
        if (isEnablePJ)
        {
            this.Pub4.AddTD("class='" + groupTitle + "'", "评价");
            PanelPJ.Visible = true;
            GePJ1.RefOID = this.RefNo;
        }
        Soft en = new Soft();
        en.No = this.RefNo;
        en.RetrieveFromDBSources();

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTD("class ='" + groupTitle + "'", "分类");
        this.Pub1.AddTREnd();

        //软件类别
        this.Pub1.AddTR();
        this.Pub1.AddTDBegin();
        this.Pub1.Add("<div class='enSort'>");
        this.Pub1.AddUL("style='list-style-type:none;margin:0px;padding:0px'");

        SoftSorts softSorts = new SoftSorts();
        softSorts.RetrieveAll();
        foreach (SoftSort sort in softSorts)
        {
            this.Pub1.AddLi("<a href='SoftMore.aspx?RefNo=" + sort.No + "' target='_blank'>" + sort.Name + "</a>");
            //strTypes += "<li><a href='SoftMore.aspx?RefNo=" + sort.No + "' target='_blank'>" + sort.Name + "</a></li>";
        }
        if (softSorts == null)
        {
            this.Pub1.Add("无");
        }
        this.Pub1.AddULEnd();
        this.Pub1.Add("</div>");
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        //相关信息
        this.Pub1.AddTR();
        this.Pub1.AddTD("class ='" + groupTitle + "'", "相关信息");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDBegin();
        this.Pub1.Add("<div>");
        this.Pub1.AddUL("style='list-style-type:none;margin:0px;padding:0px'");

        Softs softTJs = new Softs();
        int tjNum = EnsAppCfgs.GetValInt("BP.GE.Softs", "NumOfRecom");
        softTJs.RetrieveRecomByType(en.FK_Sort, tjNum);
        bool hasTJ = false;
        foreach (Soft soft in softTJs)
        {
            SysFileManagers s = en.HisSysFileManagers;
            if (s.Count == 0)
                continue;
            hasTJ = true;
            this.Pub1.AddLi("<a href='SoftDtl.aspx?RefNo=" + soft.No + "' target='_self'>" + soft.Name + "</a>");
        }
        if (hasTJ == false)
        {
            this.Pub1.Add("无");
        }
        this.Pub1.AddULEnd();
        this.Pub1.Add("</div>");
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();

        this.Pub2.AddTable();
        this.Pub2.AddTR();
        this.Pub2.AddTD("class ='" + groupTitle + "' colspan='3'", en.Name);
        this.Pub2.AddTREnd();

        //软件详细信息
        this.Pub2.AddTR();
        SysFileManagers sfs = en.HisSysFileManagers;
        if (sfs.Count == 0)
        {
            this.Pub2.AddTD("class='TD' rowspan='11'", "<span>该软件不存在或已被删除。</span>");
            this.Pub2.AddTREnd();
            this.Pub2.AddTableEnd();
            return;
        }
        SysFileManager sf_soft = (SysFileManager)sfs.GetEntityByKey(SysFileManagerAttr.AttrFileNo, "Soft");
        if (sf_soft == null)
        {
            this.Pub2.AddTD("class='TD' rowspan='11'", "<span>该软件不存在或已被删除。</span>");
            this.Pub2.AddTREnd();
            this.Pub2.AddTableEnd();
            return;
        }
        this.Pub2.AddTD("class='TD' rowspan='8' width='40%' align='center'", "<img alt='暂无图片' src='" + en.WebPath + "' border='1px' width='98%' onerror=\"this.src='" +
            this.Request.ApplicationPath + "/GE/Soft/Images/Default.jpg" + "'\"/>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTH("软件类别：");
        this.Pub2.AddTD("class='TD' style='border-top: #89c6fd 1px solid;	' width='35%'", en.FK_TypeT);
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTH("下载次数：");
        this.Pub2.AddTD(en.DownTimes);
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTH("软件授权：");
        this.Pub2.AddTD(en.SoftRoleT);
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTH("软件大小：");
        this.Pub2.AddTD(WuXiaoyun.ConvertFileSize(sf_soft.MyFileSize));
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTH("软件语言：");
        this.Pub2.AddTD(en.SoftLanguageT);
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTH("应用平台：");
        this.Pub2.AddTD(en.AppPlaT);
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTH("推荐指数：");
        string img = "";
        int starnum = Convert.ToInt16(en.RecomIdx);
        for (int i = 1; i <= starnum; i++)
        {
            img += "<img border='0' alt='' src='" + this.Request.ApplicationPath + "/GE/Soft/Images/SavedStar.png' />";
        }
        for (int j = 1; j <= 5 - starnum; j++)
        {
            img += "<img border='0' alt='' src='" + this.Request.ApplicationPath + "/GE/Soft/Images/EmptyStar.png' />";
        }
        this.Pub2.AddTD(img);
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTD("class ='" + groupTitle + "' colspan='2' style='border-right:0'", "软件描述");
        if (sf_soft != null)
        {
            this.Pub2.AddTD("class ='" + groupTitle + "' style='border-left:0';", "<span style='border-left:0;float:right;padding-right:10%'><a href=\"" + this.Request.ApplicationPath
    + "/GE/Soft/Do.aspx?FileOID=" + sf_soft.OID + "&DoType=DownLoad\" target='_blank'><img border='0' alt='立即下载' title='立即下载' src='" + this.Request.ApplicationPath + "/GE/Soft/Images/DownLoad.gif' /></a></span>");
        }
        else
        {
            this.Pub2.AddTD("<span>该软件不存在或已被删除。</span>");
        }

        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDBigDoc("colspan='4'", en.Doc);
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();
    }
}