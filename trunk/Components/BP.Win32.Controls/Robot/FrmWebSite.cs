using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BP.RB.Frm;
using BP.RB;

namespace BP.RB.Frm
{
    public partial class FrmWebSite : Form
    {
        public FrmWebSite()
        {
            InitializeComponent();
        }

        private void FrmWebSite_Load(object sender, EventArgs e)
        {
            WebSites ens = new WebSites();
            ens.RetrieveAll();
            this.dg1.BindEnsThisOnly(ens, true, true);
        }

        private void Btn_Run_Click(object sender, EventArgs e)
        {
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {

        }
        //public void BindI
    }
}