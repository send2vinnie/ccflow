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

namespace BP.Web.WF.Port
{
	/// <summary>
	/// LeftTree 的摘要说明。
	/// </summary>
	public partial class LeftTree : WebPage
	{
        public void BindMenuV2(string file)
        {
            string openNo = this.Request.QueryString["No"];
            if (openNo == null || openNo == "")
                openNo = "a";

            DataSet ds = new DataSet();
            ds.ReadXml(SystemConfig.PathOfXML + file);
            DataTable dt = ds.Tables[0];
            DataTable dt2 = dt.Clone();

            this.TreeView1.Font.Name = "宋体";
            this.TreeView1.Font.Size = FontUnit.Larger;
            foreach (DataRow dr in dt.Rows)
            {
                string no = dr["No"].ToString();
                string name = dr["Name"].ToString();
                string img = dr["Img"].ToString();
                string url = dr["Url"].ToString();

             //   url = url.Replace("@FK_DeptNo", "&No="+ BP.Edu.EduUser.FK_Dept );

                string enable = dr["Enable"].ToString();
                string DFor = "," + dr["DFor"].ToString() + ",";

                if (enable == null || enable == "0")
                    continue;

                #region  检查权限。
                if (DFor != ",,")
                {
                    bool IsOk = false;
                    while (true)
                    {
                        string myNo = "," + WebUser.No + ",";
                        if (DFor.IndexOf(myNo) >= 0)
                        {
                            IsOk = true;
                            break;
                        }


                        BP.Port.Stations sts = WebUser.HisStations;
                        foreach (BP.Port.Station s in sts)
                        {
                            myNo = "," + s.No + ",";
                            if (DFor.IndexOf(myNo) >= 0)
                            {
                                IsOk = true;
                                break;
                            }

                            myNo = "," + s.Name + ",";
                            if (DFor.IndexOf(myNo) >= 0)
                            {
                                IsOk = true;
                                break;
                            }
                        }
                        break;
                    }

                    if (IsOk == false)
                        continue;
                }
                #endregion 检查权限。


                if (img.Length > 1)
                    name = "<img src='" + img + "' border=0 /><font size='2' >" + name + "</font>";

                Microsoft.Web.UI.WebControls.TreeNode tn = new Microsoft.Web.UI.WebControls.TreeNode();
                tn.Text = name;
                tn.Target = "mainfrm";
                tn.ID = "TN" + no;
                if (url.Length <= 0)
                {
                    url = "?No=" + no;
                    tn.Target = "left";
                }
                tn.NavigateUrl = url;
                if (openNo.IndexOf(no) == 0)
                {
                    tn.Expanded = true;
                }

                switch (no.Length)
                {
                    case 2:
                        this.TreeView1.Nodes.Add(tn);
                        break;
                    case 4:
                        foreach (Microsoft.Web.UI.WebControls.TreeNode mytn in this.TreeView1.Nodes)
                        {
                            if (mytn.ID == "TN" + no.Substring(0, 2))
                            {
                                mytn.Nodes.Add(tn);
                                break;
                            }
                        }
                        break;
                    case 6:
                        foreach (Microsoft.Web.UI.WebControls.TreeNode mytn in this.TreeView1.Nodes)
                        {
                            if (mytn.ID == "TN" + no.Substring(0, 2))
                            {
                                foreach (Microsoft.Web.UI.WebControls.TreeNode mytn1 in mytn.Nodes)
                                {
                                    if (mytn1.ID == "TN" + no.Substring(0, 4))
                                    {
                                        mytn1.Nodes.Add(tn);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("erroe no ");
                }
            }
        }
        /// <summary>
        /// 区县
        /// </summary>
        public void BindQuXian()
        {
            string openNo = this.Request.QueryString["No"];
            if (openNo == null || openNo == "")
                openNo = "a";


            DataSet ds = new DataSet();
            string file = this.Request.QueryString["File"];
            if (file == null)
                file = "Menu.xml";

            ds.ReadXml(SystemConfig.PathOfXML + file);
            DataTable dt = ds.Tables[0];
            DataTable dt2 = dt.Clone();

            this.TreeView1.Font.Name = "宋体";
            this.TreeView1.Font.Size = FontUnit.Larger;
            foreach (DataRow dr in dt.Rows)
            {
                string no = dr["No"].ToString();
                string name = dr["Name"].ToString();
                string img = dr["Img"].ToString();
                string url = dr["Url"].ToString();
                string enable = dr["Enable"].ToString();
                string DFor = "," + dr["DFor"].ToString() + ",";

                if (enable == null || enable == "0")
                    continue;

                #region  检查权限。
                if (DFor != ",,")
                {
                    bool IsOk = false;
                    while (true)
                    {
                        string myNo = "," + WebUser.No + ",";
                        if (DFor.IndexOf(myNo) >= 0)
                        {
                            IsOk = true;
                            break;
                        }


                        BP.Port.Stations sts = WebUser.HisStations;
                        foreach (BP.Port.Station s in sts)
                        {
                            myNo = "," + s.No + ",";
                            if (DFor.IndexOf(myNo) >= 0)
                            {
                                IsOk = true;
                                break;
                            }

                            myNo = "," + s.Name + ",";
                            if (DFor.IndexOf(myNo) >= 0)
                            {
                                IsOk = true;
                                break;
                            }
                        }
                        break;
                    }

                    if (IsOk == false)
                        continue;
                }
                #endregion 检查权限。


                if (img.Length > 1)
                    name = "<img src='" + img + "' border=0 /><font size='4' >" + name + "</font>";

                Microsoft.Web.UI.WebControls.TreeNode tn = new Microsoft.Web.UI.WebControls.TreeNode();
                tn.Text = name;
                tn.Target = "mainfrm";
                tn.ID = "TN" + no;
                if (url.Length <= 0)
                {
                    url = "LeftOutlookTJ.aspx?No=" + no;
                    tn.Target = "left";
                }
                tn.NavigateUrl = url;
                if (openNo.IndexOf(no) == 0)
                {
                    tn.Expanded = true;
                }

                switch (no.Length)
                {
                    case 2:
                        this.TreeView1.Nodes.Add(tn);
                        break;
                    case 4:
                        foreach (Microsoft.Web.UI.WebControls.TreeNode mytn in this.TreeView1.Nodes)
                        {
                            if (mytn.ID == "TN" + no.Substring(0, 2))
                            {
                                mytn.Nodes.Add(tn);
                                break;
                            }
                        }
                        break;
                    case 6:
                        foreach (Microsoft.Web.UI.WebControls.TreeNode mytn in this.TreeView1.Nodes)
                        {
                            if (mytn.ID == "TN" + no.Substring(0, 2))
                            {
                                foreach (Microsoft.Web.UI.WebControls.TreeNode mytn1 in mytn.Nodes)
                                {
                                    if (mytn1.ID == "TN" + no.Substring(0, 4))
                                    {
                                        mytn1.Nodes.Add(tn);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("erroe no ");
                }
            }
            
        }
        public void BindSchool()
        {
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.BindMenuV2("MenuDev.xml");
            return;
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

		}
		#endregion
	}
}
