using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BP.Port;

namespace BP.Comm
{
    public partial class FrmLogin : Form
    {
        public int TT = 0;
        public FrmLogin()
        {
            InitializeComponent();
            this.TB_User.Text = BP.WF.Profile.GetLoginNo();
            if (this.TB_User.Text == "" || this.TB_User.Text == null)
            {
                this.CB_SaveInfo.Checked = false;
            }
            else
            {
                this.CB_SaveInfo.Checked = true;
                this.TB_Pass.Text = "pub";
            }
        }
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                TT++;
                if (TT > 5)
                    throw new Exception("@您频繁的输入错误的密码，系统拒绝您重试，请联系管理员解决此问题。");

                Emp ue = new Emp(this.TB_User.Text);
                if (ue.Pass.Equals(this.TB_Pass.Text) == false)
                    throw new Exception("@您输入的密码不正确。 \t\n@注意密码区分大小写。");


                WebUser.IsSaveInfo = this.CB_SaveInfo.Checked;
                WebUser.IsSavePass = this.CB_SavePass.Checked;

                WebUser.Sigin(ue);
                WebUser.FK_Flow = null;
                WebUser.FK_Node = 0;
                WebUser.WorkID = 0;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "登陆错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private void Btn_C_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void CB_SavePass_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CB_SavePass.Checked)
            {
                this.CB_SaveInfo.Checked = true;
                this.CB_SaveInfo.Enabled = false;
            }
            else
            {
            //    this.CB_SaveInfo.Checked = true;
                this.CB_SaveInfo.Enabled = true;
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
