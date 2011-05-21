using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace BP.WF.Frm
{
    public partial class FrmAction : Form
    {
        public FrmAction()
        {
            InitializeComponent();
          

        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            string s = this.TB_WhenSave.Text;

        }
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <param name="nd"></param>
        public void ShowNode(BP.WF.Node nd)
        {
            this.HisNode = nd;
            this.Text = nd.Name + " 节点事件";
            this.TB_WhenSave.Text = this.ReadPro("ND" + nd.NodeID + "_WhenSave");
            this.TB_WhenSend.Text = this.ReadPro("ND" + nd.NodeID + "_WhenSend");
            this.TB_WhenSendError.Text = this.ReadPro("ND" + nd.NodeID + "_WhenSendError");
            this.TB_WhenSendOK.Text = this.ReadPro("ND" + nd.NodeID + "_WhenSendOK");

            this.webBrowser1.Url = new Uri(Glo.IntallPath + "\\VisualFlowDesigner\\Help\\EditAction.htm");
             
            this.ShowDialog();
        }
        public string ReadPro(string proName)
        {
            DataTable dt = BP.DA.DBAccess.ReadProText(proName);
            if (dt.Rows.Count == 0)
                return null;
            string script = "";
            foreach (DataRow dr in dt.Rows)
                script += dr[0].ToString() + " \n\n";

            return script;
        }

        public BP.WF.Node HisNode = null;

        private void Btn_Help_Click(object sender, EventArgs e)
        {
            string msg = "帮助";
        }

        private void FrmAction_Load(object sender, EventArgs e)
        {

        }
    }
}