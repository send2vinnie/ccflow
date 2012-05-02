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
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;

namespace BP.Web.Port
{
	/// <summary>
	/// Port ��ժҪ˵����
	/// </summary>
    public partial class Port : System.Web.UI.Page
    {
        #region ���봫�ݲ���
        public string DoWhat
        {
            get
            {
                return this.Request.QueryString["DoWhat"];
            }
        }
        public string UserNo
        {
            get
            {
                return this.Request.QueryString["UserNo"];
            }
        }
        public string SID
        {
            get
            {
                return this.Request.QueryString["SID"];
            }
        }
        #endregion

        #region  ��ѡ��Ĳ���
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        public string FK_Node
        {
            get
            {
                return this.Request.QueryString["FK_Node"];
            }
        }
        public string WorkID
        {
            get
            {
                return this.Request.QueryString["WorkID"];
            }
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.AddHeader("P3P", "CP=CAO PSA OUR");
            //if (this.UserNo != null && this.SID != null)
            //{
            //    string sql = "SELECT SID  FROM Port_Emp WHERE No='" + this.UserNo + "'";
            //    string sid = BP.DA.DBAccess.RunSQLReturnVal(sql) as string;
            //    if (sid != this.SID)
            //    {
            //        this.Response.Write("�Ƿ��ķ��ʣ��������Ա��ϵ��sid=" + sid);
            //        //this.UCSys1.AddMsgOfWarning("����", "�Ƿ��ķ��ʣ��������Ա��ϵ��");
            //        return;
            //    }
            //    else
            //    {
            //        Emp em = new Emp(this.UserNo);
            //        WebUser.Token = this.Session.SessionID;
            //        WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);
            //    }
            //    //  this.Response.Redirect("EmpWorks.aspx", true);
            //    //  return;
            //}

           
            
            BP.SystemConfig.DoClearCash();

            Emp em = new Emp(this.UserNo);
            WebUser.Token = this.Session.SessionID;
            WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);

            string paras = "";
            foreach (string str in this.Request.QueryString)
            {
                string val = this.Request.QueryString[str];
                if (val.IndexOf('@') != -1)
                    throw new Exception("��û���ܲ���: [ " + str + " ," + val + " ] ��ֵ ��URL �����ܱ�ִ�С�");

                switch (str)
                {
                    case DoWhatList.DoNode:
                    case DoWhatList.Emps:
                    case DoWhatList.EmpWorks:
                    case DoWhatList.EmpWorksSmall:
                    case DoWhatList.EmpWorksSmallSingle:
                    case DoWhatList.FlowSearch:
                    case DoWhatList.Login:
                    case DoWhatList.MyFlow:
                    case DoWhatList.MyWork:
                    case DoWhatList.Start:
                    case DoWhatList.StartSmall:
                    case DoWhatList.FlowFX:
                    case DoWhatList.DealWork:
                    case DoWhatList.DealWorkInSmall:
                 //   case DoWhatList.CallMyFlow:
                    case "FK_Flow":
                    case "WorkID":
                    case "FK_Node":
                    case "SID":
                        break;
                    default:
                        paras += "&" + str + "=" + val;
                        break;
                }
            }

            if (this.IsPostBack == false)
            {
                if (this.IsCanLogin() == false)
                {
                    this.ShowMsg("<fieldset><legend>��ȫ��֤����</legend> ϵͳ�޷�ִ���������󣬿��������ĵ�½ʱ��̫���������µ�½��<br>�����Ҫȡ����ȫ��֤���޸�web.config ��IsDebug �е�ֵ���ó�1��</fieldset>");
                    return;
                }

                BP.Port.Emp emp = new BP.Port.Emp(this.UserNo);
                BP.Web.WebUser.SignInOfGener(emp); //��ʼִ�е�½��


                string nodeID = int.Parse(this.FK_Flow + "01").ToString();
                switch (this.DoWhat)
                {
                    //case DoWhatList.CallMyFlow: // ����ָ���Ĺ���
                    //     this.Response.Redirect("MyFlow.aspx?WorkID="+this.WorkID+"&FK_Flow=" + this.FK_Flow + paras + "&FK_Node="+this.FK_Node, true);
                    //    break;
                    //case DoWhatList.CallMyFlowSmall: // ����ָ���Ĺ���
                    //    this.Response.Redirect("MyFlowSmall.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + this.FK_Node, true);
                    //    break;
                    case DoWhatList.Start: // ������
                        if (this.FK_Flow == null)
                            this.Response.Redirect("Start.aspx", true);
                        else
                            this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.StartSmall: // ��������С����
                        if (this.FK_Flow == null)
                            this.Response.Redirect("StartSmall.aspx?FK_Flow=" + this.FK_Flow + paras, true);
                        else
                            this.Response.Redirect("MyFlowSmall.aspx?FK_Flow=" + this.FK_Flow + paras, true);
                        break;
                    case DoWhatList.StartSmallSingle: // ����������С����
                        if (this.FK_Flow == null)
                            this.Response.Redirect("StartSmall.aspx?FK_Flow=" + this.FK_Flow + paras + "&IsSingle=1" + "&FK_Node=" + nodeID, true);
                        else
                            this.Response.Redirect("MyFlowSmallSingle.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.RuningSmall: // ��;�й���
                        this.Response.Redirect("RuningSmall.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case "RuningSmallSingle": // ��;�й���
                        this.Response.Redirect("RuningSmallSingle.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.Tools: // ������Ŀ��
                        this.Response.Redirect("Tools.aspx", true);
                        break;
                    case DoWhatList.ToolsSmall: // С������Ŀ��
                        this.Response.Redirect("ToolsSmall.aspx?RefNo=" + this.Request["RefNo"], true);
                        break;
                    case DoWhatList.EmpWorksSmall: // �ҵĹ���С����
                        if (this.FK_Flow == null || this.FK_Flow == "")
                            this.Response.Redirect("EmpWorksSmall.aspx", true);
                        else
                            this.Response.Redirect("EmpWorksSmall.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.EmpWorksSmallSingle: // �ҵĹ���С���ڶ�����
                        if (this.FK_Flow == null || this.FK_Flow == "")
                            this.Response.Redirect("EmpWorksSmallSingle.aspx", true);
                        else
                            this.Response.Redirect("EmpWorksSmallSingle.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.EmpWorks: // �ҵĹ�����
                    case DoWhatList.Login:
                        if (this.FK_Flow == null)
                            this.Response.Redirect("EmpWorks.aspx", true);
                        else
                            this.Response.Redirect("EmpWorks.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.Emps: // ͨѶ¼��
                        this.Response.Redirect("Emps.aspx", true);
                        break;
                    case DoWhatList.FlowSearch: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearch.aspx", true);
                        else
                            this.Response.Redirect("/Rpt/Search.aspx?Endse=s&FK_Flow=001&EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowSearchSmall: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearchSmall.aspx", true);
                        else
                            this.Response.Redirect("../Comm/PanelEns.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowSearchSmallSingle: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearchSmallSingle.aspx", true);
                        else
                            this.Response.Redirect("../Comm/PanelEns.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowFX: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            throw new Exception("@û�в������̱�š�");

                        this.Response.Redirect("../Comm/GroupEnsMNum.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.DealWork:
                        if (this.FK_Flow == null || this.WorkID == null)
                            throw new Exception("@���� FK_Flow ���� WorkID ΪNull ��");
                        this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&o2=1" + paras, true);
                        break;
                    case DoWhatList.DealWorkInSmall:
                        if (this.FK_Flow == null || this.WorkID == null)
                            throw new Exception("@���� FK_Flow ���� WorkID ΪNull ��");
                        this.Response.Redirect("MyFlowSmall.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&o2=1" + paras, true);
                        break;
                    default:
                        this.ToErrorPage("û��Լ���ı��:DoWhat=" + this.DoWhat);
                        break;
                }
            }
        }
        public void ShowMsg(string msg)
        {
            this.Response.Write(msg);
        }
        /// <summary>
        /// ��֤��½�û��Ƿ�Ϸ�
        /// </summary>
        /// <returns></returns>
        public bool IsCanLogin()
        {
            if (BP.SystemConfig.AppSettings["IsAuth"] == "1")
            {
                if (this.SID != this.GetKey())
                {
                    if (SystemConfig.IsDebug)
                        return true;
                    else
                        return false;
                }
            }
            return true;
        }
        public string GetKey()
        {
            return BP.DA.DBAccess.RunSQLReturnString("SELECT SID From Port_Emp WHERE no='" + this.UserNo + "'");
        }

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion

        public void ToMsgPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + "/Port/InfoPage.aspx", true);
            return;
        }
        /// <summary>
        /// �л�����ϢҲ�档
        /// </summary>
        /// <param name="mess"></param>
        public void ToErrorPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + "/Comm/Port/InfoPage.aspx");
            return;
        }
    }
}
