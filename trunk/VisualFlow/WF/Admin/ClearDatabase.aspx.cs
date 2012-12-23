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
using BP.Sys;

namespace BP.Web.WF.WF
{
	/// <summary>
	/// DeleteZF ��ժҪ˵����
	/// </summary>
    partial class DeleteZF : WebPage
    {

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }
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
            //this.Button1.Click += new System.EventHandler(this.Button1_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        private void Button1_Click(object sender, System.EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }
            Button2.Attributes["onclick"] = "return window.confirm('��ȷ��Ҫɾ����');";
            Button3.Attributes["onclick"] = "return window.confirm('��ȷ��Ҫɾ����');";
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }

            this.WinCloseWithMsg("clear ok.");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }
        }
        protected void Button3_Click1(object sender, EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("�Ƿ��û���");
                return;
            }

            //ɾ�����ǵ����ݡ�
            this.Button2_Click(null, null);

            // ɾ������������ݡ�
            DA.DBAccess.RunSQL("DELETE FROM WF_Node");
            DA.DBAccess.RunSQL("DELETE FROM WF_Flow");
            DA.DBAccess.RunSQL("DELETE FROM WF_FlowSort WHERE No Not in ('01','02') ");
          //  DA.DBAccess.RunSQL("DELETE FROM WF_FAppSet");
            DA.DBAccess.RunSQL("DELETE FROM WF_FileManager");
            DA.DBAccess.RunSQL("DELETE FROM WF_RptStation");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction");

            DA.DBAccess.RunSQL("DELETE FROM WF_StandardCheck");
            DA.DBAccess.RunSQL("DELETE FROM WF_SelectAccper");
            DA.DBAccess.RunSQL("DELETE FROM WF_RptEmp");
            DA.DBAccess.RunSQL("DELETE FROM WF_RptAttr");
            DA.DBAccess.RunSQL("DELETE FROM WF_Rpt");
            DA.DBAccess.RunSQL("DELETE FROM WF_NodeStation");
            DA.DBAccess.RunSQL("DELETE FROM WF_NodeDept");
            DA.DBAccess.RunSQL("DELETE FROM WF_NodeCompleteCondition");
            DA.DBAccess.RunSQL("DELETE FROM WF_LabNote");
            DA.DBAccess.RunSQL("DELETE FROM WF_FlowStation");
            DA.DBAccess.RunSQL("DELETE FROM WF_FlowNode");

            //   DA.DBAccess.RunSQL("DELETE FROM WF_FlowCompleteCondition");

        //    DA.DBAccess.RunSQL("DELETE FROM WF_FAppSet");
            DA.DBAccess.RunSQL("DELETE FROM WF_ExpDtl");
            DA.DBAccess.RunSQL("DELETE FROM WF_Exp");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction");
            DA.DBAccess.RunSQL("DELETE FROM WF_DataApply");
            DA.DBAccess.RunSQL("DELETE FROM WF_Cond");
            DA.DBAccess.RunSQL("DELETE FROM WF_Bill");
            DA.DBAccess.RunSQL("DELETE FROM WF_BillTemplate");
            DA.DBAccess.RunSQL("DELETE FROM TA_Mail");
            DA.DBAccess.RunSQL("DELETE FROM TA_MailDtl");

            //DA.DBAccess.RunSQL("DELETE FROM WF_FlowNode");
            //DA.DBAccess.RunSQL("DELETE FROM WF_FlowNode");
            //DA.DBAccess.RunSQL("DELETE FROM WF_FlowNode");
            //DA.DBAccess.RunSQL("DELETE FROM WF_FlowNode");
            //DA.DBAccess.RunSQL("DELETE FROM WF_FlowNode");

            MapDatas mds = new MapDatas();
            mds.RetrieveAll();
            foreach (MapData md in mds)
            {
                try
                {
                    DA.DBAccess.RunSQL("DROP TABLE  " + md.No);
                }
                catch
                {
                }
            }
            MapDtls dtls = new MapDtls();
            dtls.RetrieveAll();
            foreach (MapDtl dtl in dtls)
            {
                try
                {
                    DA.DBAccess.RunSQL("DROP TABLE  " + dtl.No);
                }
                catch
                {
                }
            }
            DA.DBAccess.RunSQL("DELETE FROM Sys_MapDtl");
            DA.DBAccess.RunSQL("DELETE FROM Sys_MapData");
            DA.DBAccess.RunSQL("DELETE FROM Sys_MapAttr");
            DA.DBAccess.RunSQL("DELETE FROM Sys_GroupField");
            this.Alert("����Ҫ�ֹ���ִ�� iisreset ����!!!, ����������ݣ��˳�����������½��롣");
        }
    }
}
