using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BP.Win32
{
    public partial class FrmIE : Form
    {
        public FrmIE()
        {
            InitializeComponent();
        }

        private void FrmIE_Load(object sender, EventArgs e)
        {

        }
        public void ShowIE(string url)
        {
            this.Text = BP.SystemConfig.DeveloperName ;
            webBrowser1.WebBrowserShortcutsEnabled = true;
            this.webBrowser1.Url = new Uri(url);
            this.ShowDialog();
        }
    }
}