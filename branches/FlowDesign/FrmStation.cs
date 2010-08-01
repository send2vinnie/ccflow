using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BP.WF;
using BP.En;
using BP.Port;

namespace FlowDesign
{
    public partial class FrmStation : Form
    {
        public FrmStation()
        {
            InitializeComponent();
        }

        private void FrmStation_Load(object sender, EventArgs e)
        {

        }

       public void ShowIt(Node nd, Stations sts)
       {
          // this.tree1.BindEns(ens, true);
           this.ShowDialog();
       }
    }
}