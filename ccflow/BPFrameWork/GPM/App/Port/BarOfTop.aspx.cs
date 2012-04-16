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
using BP.Sys.Xml;


namespace BP.Web.KM
{
	/// <summary>
	/// Bar ��ժҪ˵����
	/// </summary>
	public partial class Bar : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            BP.Sys.Xml.ShortKeys sks = new ShortKeys();
            sks.RetrieveAll();

            this.UCSys1.AddTable();
            this.UCSys1.AddTR();
            foreach (ShortKey sk in sks)
            {
                this.UCSys1.AddTD("<a href='" + sk.URL + "' target='mainfrm' ><img src='" + sk.Img + "' border=0 />" + sk.Name + "</a>");
            }
            this.UCSys1.AddTREnd();
            this.UCSys1.AddTableEnd();
        }

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
