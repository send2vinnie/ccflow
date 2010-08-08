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
	/// DeleteZF 的摘要说明。
	/// </summary>
    partial class DeleteZF : WebPage
    {

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("非法用户。");
                return;
            }
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
            //this.Button1.Click += new System.EventHandler(this.Button1_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        private void Button1_Click(object sender, System.EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("非法用户。");
                return;
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (WebUser.No != "admin")
            {
                this.ToErrorPage("非法用户。");
                return;
            }

            DA.DBAccess.RunSQL("DELETE FROM WF_CHOfNode");
            DA.DBAccess.RunSQL("DELETE FROM WF_CHOfFlow");
            DA.DBAccess.RunSQL("DELETE FROM WF_Book");
            DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist");
            DA.DBAccess.RunSQL("DELETE FROM WF_GENERWORKFLOW");
         //   DA.DBAccess.RunSQL("DELETE FROM WF_WORKLIST");
            DA.DBAccess.RunSQL("DELETE FROM WF_ReturnWork");
            DA.DBAccess.RunSQL("DELETE FROM WF_ReturnWork");
            DA.DBAccess.RunSQL("DELETE FROM WF_SelectAccper");

            Nodes nds = new Nodes();
            foreach (Node nd in nds)
            {
                try
                {
                    Work wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE FROM " + wk.EnMap.PhysicsTable);
                }
                catch
                {
                }
            }

            MapDatas mds = new MapDatas();
            mds.RetrieveAll();
            foreach (MapData nd in mds)
            {
                try
                {
                    DA.DBAccess.RunSQL("DELETE FROM " + nd.PTable);
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
                    DA.DBAccess.RunSQL("DELETE FROM " + dtl.PTable);
                }
                catch
                {
                }
            }

            this.Alert("clear ok.");

        }
        protected void Button3_Click(object sender, EventArgs e)
        {

            if (WebUser.No != "admin")
            {
                this.ToErrorPage("非法用户。");
                return;
            }



        }
}
}
