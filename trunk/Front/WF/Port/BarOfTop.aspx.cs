using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.Web.Comm;
using BP.Web.Comm.UC;


namespace BP.Web.CT.GTS.Port
{
	/// <summary>
	/// Bar 的摘要说明。
	/// </summary>
	public partial class Bar : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
            ens.RetrieveAll();

            foreach (BP.WF.XML.ToolBar en in ens)
            {
                this.UCSys1.Add("<a href='" + en.Url + "'  title='" + en.Title + "' ><img src='" + en.Img + "' border='0' >" + en.Name + "</a>&nbsp;");
            }
            return;

            this.UCSys1.Add("<a href='../Home.aspx' target='mainfrm' title='税收业务流程主页面' ><img src='images/home.gif' border='0' width='15' height='15'>主页&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../Start.aspx' target='mainfrm' title='流程列表主页面'><img src='images/file_folder.gif' border='0' width='15' height='15'>流程发起&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");
            this.UCSys1.Add("<a href='../MyWork.aspx' target='mainfrm' title='待办工作汇总'><img src='images/MyRptOn.gif' border='0' width='15' height='15'>待办工作&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../Runing.aspx' target='mainfrm' title='在途工作'><img src='images/MyRptOn.gif' border='0' width='15' height='15'>在途工作&nbsp;</a>");

            

            this.UCSys1.Add("<a href='../Warning.aspx' target='mainfrm' title='待办工作汇总'><img src='images/alert.gif' border='0' width='15' height='15'>工作预警&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../Book/Home.aspx' target='mainfrm' title='文书统计、管理、分类'><img src='images/index.gif' border='0' width='15' height='15'>文书管理&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;&nbsp;</font>");

      //      this.UCSys1.Add("<a href='../../NSR/shenbaotaizhang.aspx' target='mainfrm' title='纳税台帐'><img src='images/311.gif' border='0' width='15' height='15'>纳税台帐&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='Help.htm' target='mainfrm' title='帮助'><img src='images/Help.gif' border='0'>帮助&nbsp;</a>");
            //this.UCSys1.Add("<font>&nbsp;</font>");

            this.UCSys1.Add("<a href='../../Comm/Port/ChangeSystem.aspx' target='mainfrm' title='这是本系统特有的功能模块，多系统之间无缝耦合：单击进入系统切换页面'><img src='images/quit.gif' border='0' width='15' height='15'>切换系统</a>");
        }

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
