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
using BP.GE;
using BP.DA;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;

public partial class Peng_Table : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    /*
    * 编写规范：
    * 1，表格是呈现数据的基本方式。
    * 2，表格的头单元格是由多种样式组成。TDTitle 是默认的。TDTitleRed红色的。TDTitleGreen 等。
    * 3，为什么不提倡用DataGrade, 有开发规范说明。
    */
        Infos ens = new Infos();
        ens.RetrieveAll(5);

        bool is1 = false;
        #region 默认风格
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft("默认表格风格");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("编号");
        this.Pub1.AddTDTitle("名称");
        this.Pub1.AddTREnd();
        foreach (Info en in ens)
        {
           is1= this.Pub1.AddTR(is1);
            this.Pub1.AddTD(en.No);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithBR();
        #endregion 默认风格


        #region Red风格
        this.Pub1.AddTableRed();
        this.Pub1.AddCaptionLeft("Red 表格风格");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitleRed("编号");
        this.Pub1.AddTDTitleRed("名称");
        this.Pub1.AddTREnd();
        foreach (Info en in ens)
        {
            is1=this.Pub1.AddTR(is1);
            this.Pub1.AddTD(en.No);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithBR();
        #endregion Red风格


        #region AddTableGreen 风格
        this.Pub1.AddTableGreen();
        this.Pub1.AddCaptionLeft("TableGreen 表格风格");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitleGreen("编号");
        this.Pub1.AddTDTitleGreen("名称");
        this.Pub1.AddTREnd();
        foreach (Info en in ens)
        {
          is1=  this.Pub1.AddTR(is1);
            this.Pub1.AddTD(en.No);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithBR();
        #endregion AddTableGreen风格


        #region AddTableBlue 风格
        this.Pub1.AddTableBlue();
        this.Pub1.AddCaptionLeft("AddTableBlue 表格风格");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitleBlue("编号");
        this.Pub1.AddTDTitleBlue("名称");
        this.Pub1.AddTREnd();
        foreach (Info en in ens)
        {
           is1= this.Pub1.AddTR(is1);
            this.Pub1.AddTD(en.No);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithBR();
        #endregion AddTableBlue风格


        #region AddTableBlue 风格
        this.Pub1.AddTableWin();
        this.Pub1.AddCaptionLeft("TableWin 表格风格");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitleWin("编号");
        this.Pub1.AddTDTitleWin("名称");
        this.Pub1.AddTREnd();
        foreach (Info en in ens)
        {
          is1=  this.Pub1.AddTR(is1);
            this.Pub1.AddTD(en.No);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithBR();
        #endregion AddTableBlue风格




    }
}
