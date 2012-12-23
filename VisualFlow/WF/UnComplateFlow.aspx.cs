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
using BP.WF;
using BP.DA;
using BP.En;

namespace BP.Web.WFQH.WF
{
	/// <summary>
	/// UnComplateFlow ��ժҪ˵����
	/// </summary>
	public partial class UnComplateFlow : WebPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Page.RegisterClientScriptBlock("s",
          "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");


			string fk_flow = this.Request.QueryString["FK_Flow"] ; 

			//��ѯδ��ɵĹ�����Ϣ
			string sql="SELECT FK_FLOW, FK_Node AS FK_Node, COUNT(*) AS NUM "
				+"FROM WF_GENERWORKFLOW WHERE WORKID IN (SELECT WORKID FROM WF_GenerWorkerlist WHERE FK_EMP='"+WebUser.No+"' )"
				+"GROUP BY FK_FLOW, FK_Node";

			DataTable dt = DBAccess.RunSQLReturnTable(sql);
			Nodes nds = new Nodes(fk_flow);
			BP.WF.Flow fl = new BP.WF.Flow(fk_flow);

			this.UCSys1.Clear();
			this.UCSys1.AddTable();
            this.UCSys1.AddCaptionLeft(fl.Name);
			this.UCSys1.AddTR();
			this.UCSys1.AddTDTitle("��������");
			this.UCSys1.AddTDTitle("ͣ�������ڵ�����");
			this.UCSys1.AddTDTitle("δ��ɸ���");
			this.UCSys1.AddTREnd();

			foreach(Node nd in nds)
			{
				this.UCSys1.AddTR();
				this.UCSys1.AddTDIdx(nd.Step);
				this.UCSys1.AddTD(nd.Name);
				string wwc="0"; //δ��ɡ�
				foreach(DataRow dr in dt.Rows)
				{
					if (dr["FK_Node"].ToString() !=nd.NodeID.ToString())
						continue;
				 
					wwc= dr["Num"].ToString() ;
				}
				//string str="<a title='��"+nd.FlowName+"�����С�"+wwc+"���������Ѿ���ת����"+nd.Name+"���ڵ���' href=\"javascript:alert('��"+wwc+"������ͣ���ڴ˹�������ϣ�');window.open('UnCompFlowDetails.aspx?FK_Flow="+nd.FK_Flow+"&FK_Node="+nd.NodeID+"&FK_Emp="+Web.WebUser.No+"&IsClose=1','newwin','width=750,top=300,left=300,height=300,scrollbars=yes,resizable=no,toolbar=false,location=false');newWindow.focus();\" >"+wwc+"</a>";
//				if(int.Parse(wwc)==0)
//				{
////					PubClass.Alert("Ŀǰû�й���ͣ���ڴ˹����ڵ��ϣ�");
//					this.UCSys1.AddTD("<a href=\"javascript:alert('��"+wwc+"������ͣ���ڴ˹�������ϣ�');\" >"+wwc+"</a>");
//				}
				this.UCSys1.AddTD("<a title='��"+nd.FlowName+"�����С�"+wwc+"���������Ѿ���ת����"+nd.Name+"���ڵ���' href=\"javascript:alert('��"+wwc+"������ͣ���ڴ˹�������ϣ�');window.open('UnCompFlowDetails.aspx?FK_Flow="+nd.FK_Flow+"&FK_Node="+nd.NodeID+"&FK_Emp="+Web.WebUser.No+"&IsClose=1','newwin','width=750,top=300,left=300,height=300,scrollbars=yes,resizable=no,toolbar=false,location=false');newWindow.focus();\" >"+wwc+"</a>");
				this.UCSys1.AddTREnd();
			}
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
