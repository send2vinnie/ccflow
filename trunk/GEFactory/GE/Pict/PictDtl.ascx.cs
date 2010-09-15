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
using BP.Web;
using BP.En;
using BP.DA;
using System.Text;
using BP.GE;
using BP.Sys;
using System.Net;
using System.IO;

public partial class GE_Pict_PictDtl : BP.Web.UC.UCBase3
{
    public string RefNo
    {
        get
        {
            return Request.QueryString["RefNo"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindDtl();
    }
    public void BindDtl()
    {
        Pict pic = new Pict(this.RefNo);

        // 对浏览次数进行设置
        string hcName = "PictRefNo" + this.RefNo;
        //if (Request.Cookies[hcName] == null)
        //{
        //    // 创建cookie
        //    HttpCookie hcPictDtl = new HttpCookie(hcName);
        //    hcPictDtl.Value = this.RefNo.ToString();
        //    hcPictDtl.Expires = DateTime.Now.AddHours(8);
        //    Response.Cookies.Add(hcPictDtl);

        // 更新访问次数
        pic.ReadTimes += 1;
        pic.Update();
        //}
        //string hc_value = Request.Cookies[hcName].Value;

        PictSort psor = new PictSort(pic.FK_Sort);

        this.AddTable();
        this.AddTR();
        this.Page.Title = pic.Name;
        // 显示左边的专题详细信息
        this.Add("<TD>");
        this.Add("<div align='center'>");
        this.Add("<H2>" + pic.Name + "</H2>");
        this.Add("</div>");
        // 添加作者和日期
        this.Add("<div class='author' align='center'>");
        this.Add("类别：" + psor.Name + "   日期：" + pic.RDT + "    阅读：" + pic.ReadTimes);
        this.Add("</div>");
        this.Add("<hr/>");
        //添加详细信息
        this.Add("<div align='center'>");
        //找不到图片时的默认图片
        string DefImgSrc = this.Request.ApplicationPath + "/GE/Pict/img/default.jpg";
        this.Add("<img width='500px' src='" + pic.WebPath + "' onerror=\"this.src='" + DefImgSrc + "'\"/>");
        this.Add("</div>");

        this.Add("<div>");
        this.Add(pic.Doc);
        this.Add("</div>");
        this.AddTDEnd();

        // 显示右边的相关专题列表
        this.Add("<TD class=BigDoc valign=top style=\"width:30%\" >");


        Picts pics = new Picts();
        QueryObject qo = new QueryObject(pics);
        qo.Top = 6;
        qo.AddWhere(PictAttr.FK_Sort, pic.FK_Sort);
        //qo.addAnd();
        //qo.addOrderBy(PictAttr.ReadTimes);
        qo.DoQuery();
        this.AddB(pic.Name + "&nbsp;&nbsp;相关员工");

        this.AddUL();
        foreach (Pict pi in pics)
        {
            if (pi.No == this.RefNo)
            {
                continue;
            }
            this.AddLi("<a href='PictDtl.aspx?RefNo=" + pi.No + "' target=_blank>" + pi.Name + "</a>  " + " 关注度（" + pi.ReadTimes + "）");
        }
        this.AddULEnd();
        this.AddTDEnd();

        this.AddTREnd();

        this.AddTR();
        this.Add("<TD class=BigDoc>");
        this.Add("<p align=center><a href='javascript:window.close()'>【关闭】</a> <a href='javascript:print();'>【打印】</a></p>");
        this.AddTDEnd();

        this.Add("<TD class=BigDoc valign=top style=\"width:30%\" ></TD>");
        this.AddTREnd();
        this.AddTableEnd();
    }
}
