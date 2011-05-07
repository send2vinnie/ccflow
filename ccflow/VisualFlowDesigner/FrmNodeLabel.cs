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
using BP.Win.WF;

namespace VisualFlowDesigner
{
    public partial class FrmNodeLabel : Form
    {
        public FrmNodeLabel()
        {
            InitializeComponent();
        }
        private void FrmNodeLabel_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = this.LabName;
            //    BP.WF.LabNote en = this.Tag as LabNote;
         //   this.textBox1.Text = this.HisWinWFLab.HisNode.Name;
        }

        public string  LabName = "";
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.LabName = this.textBox1.Text;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
