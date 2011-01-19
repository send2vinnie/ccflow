using System;
using BP.YG;
using BP.Web;


namespace BP.YG
{
	
	/// <summary>
	/// THPage 的摘要说明。
	/// </summary>
	public class YGPage:System.Web.UI.Page
	{
        public string PRI
        {
            get
            {
                string s = this.Request.QueryString["PRI"];
                if (s == "")
                    return null;
                return s;
            }
        } 
        public string FK_SF
        {
            get
            {
                string s = this.Request.QueryString["FK_SF"];
                if (s == null || s == "")
                {
                    if (this.FK_City == null || this.FK_City == "")
                        return null;

                    return this.FK_City.Substring(0, 2);
                }
                return s;
            }
        }
        public string FK_Sort
        {
            get
            {
                return this.Request.QueryString["FK_Sort"];
            }
        }
        public string FK_Type
        {
            get
            {
                return this.Request.QueryString["FK_Type"];
            }
        }
        public string FK_City
        {
            get
            {
                return this.Request.QueryString["FK_City"];
            }
        }
        public string DoType
        {
            get
            {
                string s = this.Request.QueryString["DoType"];
                if (s == null)
                    return "About";
                return s;
            }
        }
        public string RefNo
        {
            get
            {
                return this.Request.QueryString["RefNo"];
            }
        }
        public int RefOID
        {
            get
            {
                try
                {
                    return int.Parse(this.Request.QueryString["RefOID"]);
                }
                catch
                {
                    try
                    {
                        return int.Parse(this.Request.QueryString["OID"]);
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
        }
        public string BureauNo
        {
            get
            {
                string s = ViewState["B"] as string;
                if (s == null)
                {
                    s = this.Request.QueryString["B"];
                    if (s == null)
                    {
                        s = Global.BureauNo;
                        ViewState["B"] = s;
                    }

                    if (s.IndexOf(",") != -1)
                    {
                        string[] strs = s.Split(',');
                        s = strs[0];
                        ViewState["B"] = s;
                    }
                }
                return s;
            }
        }

		public int PageIdx
		{
			get
			{
				try
				{
					return int.Parse(this.Request.QueryString["PageIdx"]) ; 
				}
				catch
				{
					return 1;
				}
			}
		}

		public void ToUrl(string url)
		{
			string msg1="<script language=javascript>window.location.href='"+url+"';</script>";
			this.Response.Write(msg1);
			return;
		}
		public void Alert(string msg)
		{
			string msg1="<script language=javascript>alert('"+msg+"');</script>";
			this.Response.Write(msg1);
			return;
			 
		}
		public void Close()
		{
			string msg1="<script language=javascript>window.close();</script>";
			this.Response.Write(msg1);
			return;
		}
		public void WinOpen(string url)
		{
			string msg1="<script language=javascript>window.open("+url+");</script>";
			this.Response.Write(msg1);
			return;
		}
        public void WinClose()
        {
            string msg1 = "<script language=javascript>window.close();</script>";
            this.Response.Write(msg1);
            return;
        }
        ///// <summary>
        ///// CheckCustomrSession
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public bool CheckCustomrSession(string msg, string B)
        //{
        //    if (Global.CustomerNo == null)
        //    {
        //        //string msg = "您登陆时间太长或者您没有登陆。登陆成功后系统会自动转入上一页面上去。";
        //        Global.MsgOfReLogin = msg;
        //        Global.GoWhere = this.Request.RawUrl;
        //        //this.ToUrl("/Login.aspx?B=" + B);
        //        return false;
        //    }
        //    return true;
        //}
        //public bool CheckCustomrSession(string msg)
        //{
        //    if (Global.CustomerNo == null)
        //    {
        //        //string msg1 = " var url='/Port.aspx';";
        //        //msg1 += " var b=window.showModalDialog(url, 'ass' ,'dialogHeight: 450px; dialogWidth: 500px;center: yes; help: no');";
        //        //msg1 += "  window.location.reload();";
        //        //System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> " + msg1 + "</script> ");
        //        return false;
        //    }
        //    else
        //        return true;
        //    //  return this.CheckCustomrSession(msg, this.BureauNo);
        //}
        public string CheckCustomrSession()
        {
            return this.CheckCustomrSession("您的登陆时间太长或者您没有登录。");
        }
        public string CheckCustomrSession(string msg)
        {
            if (Global.CustomerNo != null)
            {
                return null;
            }
            return "<a>" + msg + "，您需要<a href='javascript:DoPort();'>重新登陆或者注册</a>，注册华夏财税人有如下好处。</a><hr><li>1、您可以发表文章获取积分。</li> <li>2、您可以贡献文件获取积分。</li> <li>3，您可以利用积分购买纪念器。</li> ";
        }
        public bool CheckCustomrSessionAlert()
        {
            if (Global.CustomerNo == null)
            {
                BP.PubClass.Alert("重新登陆或者注册");
                return false;
            }
            return true;
        }
		public void ToMsgPage(string msg)
		{
			try
			{
				Global.Msg = msg;
				this.ToUrl("/Msg.aspx?B="+this.BureauNo);
			}
			catch
			{
                this.ToUrl("/Msg.aspx?B=" + this.BureauNo);
			}
			return;
		}
		public void ToLoginPage_(string msg)
		{
			Global.MsgOfReLogin = msg;
			string url ="/Login.aspx?GoWhere="+this.Request.RawUrl;
			this.Response.Redirect(url,true);
		}
	}
}
