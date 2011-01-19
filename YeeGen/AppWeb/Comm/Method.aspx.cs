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
using BP.Sys;
using BP.En;
using BP.Web.Controls;

namespace BP.Web.Comm
{
	/// <summary>
	/// RefFuncLink 的摘要说明。
	/// </summary>
	public partial class UIRefMethod1 : BP.Web.PageBase 
	{
        public void Init(Method rm)
        {
            /*
            Button btn = new Button();
            btn.Text = "执行:" + rm.Title;
            btn.Click += new EventHandler(btn_Do_Click);
            this.Ucsys2.Add(btn);
            this.Ucsys2.Add("<input type=button onclick='javascript:window.close()' value=' 关闭 ' />");
        //    this.Ucsys2.Add("<input type=button onclick='javascript:window.close()' value=' 帮助 ' />");
             */ 
        }
        public void btn_Do_Click(object sender, EventArgs e)
        {
            string ensName = this.Request.QueryString["M"];
            Method rm = BP.DA.ClassFactory.GetMethod(ensName);
           // rm.Init();
            int mynum = 0;
            foreach (Attr attr in rm.HisAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                mynum++;
            }

            //object[] objs =new object[rm.HisAttrs.Count];
            //object[] objs = new object[mynum];
            int idx = 0;
            foreach (Attr attr in rm.HisAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                if (attr.UIVisible == false)
                    continue;
                try
                {
                    switch (attr.UIContralType)
                    {
                        case UIContralType.TB:
                            switch (attr.MyDataType)
                            {
                                case BP.DA.DataType.AppString:
                                case BP.DA.DataType.AppDate:
                                case BP.DA.DataType.AppDateTime:
                                    string str1 = this.UCEn1.GetTBByID("TB_" + attr.Key).Text;
                                    rm.SetValByKey(attr.Key, str1);
                                    break;
                                case BP.DA.DataType.AppInt:
                                    int myInt = int.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
                                    rm.Row[idx] = myInt;
                                    rm.SetValByKey(attr.Key, myInt);
                                    break;
                                case BP.DA.DataType.AppFloat:
                                    float myFloat = float.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
                                    rm.SetValByKey(attr.Key, myFloat);
                                    break;
                                case BP.DA.DataType.AppDouble:
                                case BP.DA.DataType.AppMoney:
                                case BP.DA.DataType.AppRate:
                                    decimal myDoub = decimal.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
                                    rm.SetValByKey(attr.Key, myDoub);
                                    break;
                                case BP.DA.DataType.AppBoolean:
                                    int myBool = int.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
                                    rm.SetValByKey(attr.Key, myBool);
                                    break;
                                default:
                                    throw new Exception("没有判断的数据类型．");
                            }
                            break;
                        case UIContralType.DDL:
                            try
                            {
                                string str = this.UCEn1.GetDDLByKey("DDL_" + attr.Key).SelectedItemStringVal;
                                rm.SetValByKey(attr.Key, str);
                            }
                            catch (Exception ex)
                            {
                                rm.SetValByKey(attr.Key, "");
                            }
                            break;
                        case UIContralType.CheckBok:
                            if (this.UCEn1.GetCBByKey("CB_" + attr.Key).Checked)
                                rm.SetValByKey(attr.Key, 1);
                            else
                                rm.SetValByKey(attr.Key, 0);
                            break;
                        default:
                            break;
                    }
                    idx++;
                }
                catch (Exception ex)
                {
                    throw new Exception("attr=" + attr.Key +" attr = "+attr.Key + ex.Message);
                }
            }
            try
            {
                object obj = rm.Do();
                if (obj != null)
                {
                    switch (rm.HisMsgShowType)
                    {
                        case MsgShowType.SelfAlert:
                            PubClass.Alert(obj.ToString());
                            return;
                        case MsgShowType.SelfMsgWindows:
                            PubClass.Alert(obj.ToString());
                            return;
                        case MsgShowType.Blank:
                            this.ToMsgPage(obj.ToString());
                            //this.ToMsgPage_Do(obj.ToString());
                            return;
                        default:
                            return;
                    }
                }
                this.WinClose();
            }
            catch (Exception ex)
            {
                this.UcMsg.AddMsgOfWarning("@执行[" + ensName + "]期间出现错误：", ex.Message);
                //    string msg = "";
                //  throw new Exception( + ex.Message);
            }
            return;
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            
        }
        public void Bind(Method rm)
        {
            this.UCEn1.BindAttrs(rm.HisAttrs);
            //this.UCEn1.AddHR();
            //Button btn = new Button();
            //btn.Text = rm.Title;
            //if (rm.Warning == null || rm.Warning == "")
            //{
            //}
            //else
            //{
            //    btn.OnClientClick = "";
            //}
            //btn.Click += new EventHandler(BPToolBar1_ButtonClick);
            //btn.ID = "Btn_Do";
            //this.UCEn1.Add(btn);

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

        private void BPToolBar1_ButtonClick(object sender, EventArgs e)
        {
            #region
            //ToolbarBtn btn = (ToolbarBtn)sender;
            //switch( btn.ID)
            //{
            //    case NamesOfBtn.Do:
            //        string ensName = this.Request.QueryString["M"];
            //        Method rm =BP.DA.ClassFactory.GetMethod(ensName);
            //        rm.Init();
            //        int mynum=0;
            //        foreach (Attr attr in rm.HisAttrs)
            //        {
            //            if (attr.MyFieldType == FieldType.RefText)
            //                continue;
            //            mynum++;
            //        }

            //    //	object[] objs =new object[rm.HisAttrs.Count];
            //    //  object[] objs = new object[mynum];

            //        int idx=0;
            //        foreach (Attr attr in rm.HisAttrs)
            //        {
            //            if (attr.MyFieldType == FieldType.RefText)
            //                continue;

            //            switch (attr.UIContralType)
            //            {
            //                case UIContralType.TB:
            //                    switch (attr.MyDataType)
            //                    {
            //                        case BP.DA.DataType.AppString:
            //                        case BP.DA.DataType.AppDate:
            //                        case BP.DA.DataType.AppDateTime:
            //                            string str1 = this.UCEn1.GetTBByID("TB_" + attr.Key).Text;

            //                            rm.SetValByKey(attr.Key, str1);
            //                            //rm.Row[idx]=str1;
            //                            //attr.DefaultVal=str1;
            //                            break;
            //                        case BP.DA.DataType.AppInt:
            //                            int myInt = int.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
            //                            rm.Row[idx] = myInt;
            //                            rm.SetValByKey(attr.Key, myInt);

            //                            //attr.DefaultVal=myInt;
            //                            break;
            //                        case BP.DA.DataType.AppFloat:
            //                            float myFloat = float.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
            //                            rm.SetValByKey(attr.Key, myFloat);
            //                            break;
            //                        case BP.DA.DataType.AppDouble:
            //                        case BP.DA.DataType.AppMoney:
            //                        case BP.DA.DataType.AppRate:
            //                            decimal myDoub = decimal.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
            //                            rm.SetValByKey(attr.Key, myDoub);

            //                            //attr.DefaultVal=myDoub;
            //                            break;
            //                        case BP.DA.DataType.AppBoolean:
            //                            int myBool = int.Parse(this.UCEn1.GetTBByID("TB_" + attr.Key).Text);
            //                            rm.SetValByKey(attr.Key, myBool);
            //                            break;
            //                        default:
            //                            throw new Exception("没有判断的数据类型．");

            //                    }
            //                    break;
            //                case UIContralType.DDL:
            //                    try
            //                    {
            //                        string str = this.UCEn1.GetDDLByKey("DDL_" + attr.Key).SelectedItemStringVal;
            //                        rm.SetValByKey(attr.Key, str);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        rm.SetValByKey(attr.Key, "");
            //                        // this.ToErrorPage("获取：[" + attr.Desc + "] 期间出现错误，可能是该下拉框中没有选择项目，错误技术信息：" + ex.Message);
            //                        //  rm.Row[idx] = null;
            //                        // attr.DefaultVal = "";
            //                    }
            //                    break;
            //                case UIContralType.CheckBok:
            //                    if (this.UCEn1.GetCBByKey("CB_" + attr.Key).Checked)
            //                        rm.SetValByKey(attr.Key, 1);
            //                    else
            //                        rm.SetValByKey(attr.Key, 0);
            //                    break;
            //                default:
            //                    break;
            //            }
            //            idx++;
            //        }

            //        try
            //        {
            //            object obj = rm.Do();
            //            if (obj != null)
            //            {
            //                this.ToMsgPage(obj.ToString());
            //            }
            //            this.WinClose();
            //        }
            //        catch (Exception ex)
            //        {
            //            string msg = "";
            //            throw new Exception("@执行[" + ensName + "]期间出现错误：" + ex.Message);
            //        }
            //        return;
            //    case NamesOfBtn.Help:
            //        this.Helper();
            //        break;
            //    case NamesOfBtn.Close:
            //        this.WinClose();
            //        break;
            //    case NamesOfBtn.Cancel:
            //        this.WinClose();
            //        break;
            //    default:
            //        throw new Exception("error id"+btn.ID);
            //}
            #endregion
        }

        private void DoMethod()
        {
            //this.BPToolBar1.ButtonClick += new EventHandler(BPToolBar1_ButtonClick);
            string ensName = this.Request.QueryString["M"];

            Method rm = DA.ClassFactory.GetMethod(ensName);
            if (rm == null)
                throw new Exception("请确认类名是否输入错误：" + ensName);

            this.Init(rm);
            rm.Init();

            if (rm.HisAttrs == null || rm.HisAttrs.Count == 0)
            {
                object obj = rm.Do();
                if (obj == null)
                {
                    this.WinClose();
                    return;
                }
                this.Ucsys1.Clear();
                this.Ucsys2.Clear();

                this.Ucsys1.AddMsgOfInfo("执行信息", null);
                this.UCEn1.AddMsgOfInfo("", obj.ToString());

                //    this.AlertHtmlMsg(obj.ToString());
                // this.WinClose();
                // this.ToMsgPage(obj.ToString().Replace("@", "<BR>@"));
                return;
            }
            if (this.IsPostBack == false)
            {
                //this.BPToolBar1.AddBtn(NamesOfBtn.Do, rm.Title);
                //this.BPToolBar1.AddSpt("sd");
                //this.BPToolBar1.AddBtn(NamesOfBtn.Close);
                //this.BPToolBar1.AddBtn(NamesOfBtn.Help);
            }

            this.Bind(rm);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            panConfirm.Visible = false;
            DoMethod();
        }
    }
}
