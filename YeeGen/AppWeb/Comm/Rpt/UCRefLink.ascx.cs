namespace BP.Web.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using BP.DA;
	using BP.En;
	using BP.Rpt;
	

	/// <summary>
	///		UCRefLink 的摘要说明。
	///		用来处理，相关连接的Html。
	/// </summary>
	public partial class UCRefLink : UCBase
	{
		public void BindDB(string className,string val)
		{

			RefLinks ens= new RefLinks( className );

			this.Text+="<p align=center>"+ens.Title+"</p>";
			this.Text+="<br>";
			this.Text+="<div align='center'>";
			this.Text+="<table class='D1'>";

			this.Text+="<TR>";
			this.Text+=" <TD>编号 </TD>";
			this.Text+=" <TD>名称 </TD>";
			this.Text+=" <TD>备注 </TD>";
			this.Text+="</TR>";


			#region 输出
			foreach(RefLink en2 in ens ) 
			{
				this.Text+="<tr>" ;
				this.Text+=en2.No ;
				this.Text+= en2.HtmlName(className, val) ;
				this.Text+= en2.Note ;
				this.Text+="</tr>" ;
			}
			#endregion

			this.Text+="</table>" ;
			this.Text+="</div>" ; 

		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
