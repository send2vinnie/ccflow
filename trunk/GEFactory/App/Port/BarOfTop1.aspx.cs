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
using BP.XML;

namespace BP.Web.CT.GTS.Port
{
	/// <summary>
	/// Bar 的摘要说明。
	/// </summary>
	public partial class Bar : System.Web.UI.Page
	{
        public void Bind()
        {
            BP.Sys.Xml.ShortKeys sks = new BP.Sys.Xml.ShortKeys();
            sks.RetrieveAll();

            this.UCSys1.Add("<div class='top_nav'>");
            foreach (BP.Sys.Xml.ShortKey sk in sks)
            {
                //this.UCSys1.Add("<a href='" + sk.URL + "' target='left' ><img src='" + sk.Img + "' border=0 />" + sk.Name + "</a>");
                this.UCSys1.Add("<a href='" + sk.URL + "' target='" + sk.Target + "' ><img src='./Style/img/"+sk.No+".gif' border=0 />" + sk.Name + "</a>&nbsp;");
            }
            this.UCSys1.AddDivEnd(); // ("<div id='top_nav'>");


        }
		protected void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                this.Bind();
            }
            catch
            {
                this.Bind();
            }
			return ;
			
			this.UCSys1.Add("<a href='../Home.aspx' target='mainfrm' ><img src='../Images/Home.gif' border=0 />主页</a>");
			this.UCSys1.Add("<a href='../List.aspx?StateOfHD=1' target='mainfrm' ><img src='../Images/Home.gif' border=0 />定税审核</a>");
			this.UCSys1.Add("<a href='../../Comm/Port/ChangeSystem.aspx' target='mainfrm' ><img src='../../Images/System/Power.gif' border=0 />切换系统</a>");
			return;
			 
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
