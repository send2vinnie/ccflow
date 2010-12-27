using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.En;
using BP.WF;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.RawUrl.ToLower().Contains("wap"))
        {
            this.Response.Redirect("./WAP/", true);
            return;
        }

        this.Response.Redirect("./WF/Admin/TestFlow.aspx", true);
        return;


        //Flows fls = new Flows();
        //fls.RetrieveAll();

        //foreach (Flow fl in fls)
        //{
        //   // this.Response.Write(fl.Name+"<br>");
        //}

        //Flow fl = new Flow();
        //fl.No = "006";
        //fl.Retrieve();

        //this.Response.Write(fl.Name);
        //fl.Name = "办公用车申请流程123";
        //fl.Update();
        //fl.Delete();
        //this.Response.Write(fl.Name);
        
        return;

      //  //Flow fl = new Flow();
      //  //fl.No = "0123";
      //  //fl.Name = "abc";
      //  //fl.Insert();

      //  //Flow fl = new Flow();
      //  //fl.No = "001";
      //  //fl.Retrieve();

      //  //this.Response.Write(fl.Name);
      //  //fl.Name = "sssss";
      //  //fl.Update();
      //  //this.Response.Write(fl.Name);

      //  //fl.Delete();
         

      //  if (this.Request.RawUrl.ToLower().Contains("wap"))
      //  {
      //      this.Response.Redirect("./WAP/", true);
      //      return;
      //  }

      //  this.Response.Redirect("./WF/Admin/TestFlow.aspx", true);
      //  return;

      //  //BP.WF.Flow fl = new BP.WF.Flow();
      //  //fl.No = "002";
      //  //fl.Name = "asdfasdf";
      //  //fl.Insert();

      //  //fl.FK_FlowSort = "sds";
      //  //fl.Update();

      //  //fl.Delete();

      //  //BP.WF.Flows ens = new BP.WF.Flows();
      //  //QueryObject qo = new QueryObject(ens);
      //  //qo.addLeftBracket();
      //  //qo.AddWhere("No", "001");
      //  //qo.addAnd();
      //  //qo.AddWhere("No", "001");
      //  //qo.addRightBracket();


      // // ens.RetrieveAll();
      ////  ens.Retrieve("FK_FlowSort","01");

      //  //foreach (BP.WF.Flow en in ens)
      //  //{
      //  //    this.Response.Write(en.Name+"<br>");
      //  //}
      //  return;

      //  this.Title = "驰骋工作流演示版本 - 工作流引擎前台";

      //  BP.WF.Flows fls = new BP.WF.Flows();
      //  fls.RetrieveAll();

      //  // fls.get

      //  this.Ucsys1.AddTable();
      //  this.Ucsys1.AddCaptionLeftTX("驰骋流程演示版本 - 选择要演示的流程");

      //  this.Ucsys1.AddTR();
      //  this.Ucsys1.AddTDTitle();
      //  this.Ucsys1.AddTDTitle("新版本");
      //  this.Ucsys1.AddTDTitle("");
      //  this.Ucsys1.AddTREnd();

      //  foreach (BP.WF.Flow fl in fls)
      //  {
      //      this.Ucsys1.AddTR();
      //      this.Ucsys1.AddTD(fl.FK_FlowSortText);
      //      // this.Ucsys1.AddTD("<a href='./WF/Admin/TestFlow.aspx?FK_Flow=" + fl.No + "'>" + fl.Name + "</a>");
      //      this.Ucsys1.AddTD("<a href='./WF/Admin/TestFlow.aspx?FK_Flow=" + fl.No + "&Type=New'>" + fl.Name + "</a>");
      //      this.Ucsys1.AddTD("<a href='./DataUser/FlowDesc/" + fl.No + ".gif' target=_blank>流程图</a>");
      //      this.Ucsys1.AddTREnd();
      //  }

      //  this.Ucsys1.AddTableEndWithHR();
      //  this.Ucsys1.Add("<a href=http://flow.ccflow.cn/ > http://flow.ccflow.cn/ 相关下载</a>");
      //  this.Ucsys1.AddBR("<a href=./WF/Login.aspx > 直接登陆 (嵌入方式)</a>");
      //  this.Ucsys1.AddBR("<a href=./WF/Port/Signin.aspx > 直接登陆 (系统方式)</a>");

        //BP.Port.Emps ems = new BP.Port.Emps();
        //ems.RetrieveAll();
        //DataTable dt = ems.ToDataTableField();
    }
}
