using System.Web.UI;
using System.ComponentModel;
using System.Data;
using BP.En;
using System;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Configuration;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEPJ runat='server'></{0}:GEPJ>")]
    [PersistChildren(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public class GEPJ : Control
    {
        private int RowsCount = 1;

        [Bindable(true),
        Category("自定义设置"),
        Description("评论的内容所属的组别"),
        Browsable(true)]
        public string NewsGroup
        {
            get;
            set;
        }
        [Bindable(true),
        Category("自定义设置"),
        Description("专题"),
        Browsable(true)]
        public string RefOID
        {
            get;
            set;
        }
        [Bindable(true),
        Category("自定义设置"),
        Description("评论组"),
        Browsable(true)]
        public int PJGroup
        {
            get;
            set;
        }

        [Bindable(true),
        DefaultValue(eDisplayMode.Horizontal),
        Category("自定义设置"),
        Description("显示样式"),
        Browsable(true)]
        public eDisplayMode DisplayMode
        {
            get;
            set;
        }

        [Bindable(true),
        DefaultValue("true"),
        Category("自定义设置"),
        Description("是否显示图片"),
        Browsable(true)]
        public bool IsShowPic
        {
            get;
            set;
        }

        [Bindable(true),
        DefaultValue("true"),
        Category("自定义设置"),
        Description("是否显示标题"),
        Browsable(true)]
        public bool IsShowTitle
        {
            get;
            set;
        }
        [Browsable(false)]
        public string FK_Emp
        {
            get
            {
                return BP.Web.WebUser.No;
            }
        }

        [Bindable(false),
        Category("自定义设置"),
        DefaultValue(""),
        Description("用户名"),
        Browsable(false)]
        public string FK_EmpT
        {
            get
            {
                return BP.Web.WebUser.Name;
            }
        }
        [Browsable(false)]
        public string ResUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ResUrl"] == null ? string.Empty : ConfigurationManager.AppSettings["ResUrl"].ToString();
            }
        }
        private bool _ISAllowAnony = false;
        [Bindable(true),
        Category("自定义设置"),
        Description("是否允许匿名用户发表评论")]
        public bool ISAllowAnony
        {
            get
            {
                return _ISAllowAnony;
            }
            set
            {
                _ISAllowAnony = Convert.ToBoolean(value);
            }
        }
        private Button btnSubmit;
        private HiddenField hidden;
        public GEPJ()
        {
            this.Controls.Clear();
            btnSubmit = new Button();
            btnSubmit.OnClientClick = "return btnSubmit_Click()";
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnSubmit.Text = "提交";
            this.Controls.Add(btnSubmit);
            hidden = new HiddenField();
            hidden.Value = string.Empty;
            this.Controls.Add(hidden);
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FK_Emp))
            {
                if (ISAllowAnony == true)
                {
                    //为匿名用户保存会话ID必须先建立会话否则每次会话ID都是不一样的
                    System.Web.HttpContext.Current.Session["tempNo"] = System.Web.HttpContext.Current.Session.SessionID;
                    string NewsGroup = this.NewsGroup;
                    string RefOID = this.RefOID;
                    int PJGroup = this.PJGroup;
                    string Emp = System.Web.HttpContext.Current.Session["tempNo"].ToString();
                    string EmpT = "匿名";
                    string Num = hidden.Value;
                    string strIP = GeFun.getIp();
                    PJEmpInfo pjEmp = new PJEmpInfo();
                    pjEmp.FK_Emp = Emp;
                    pjEmp.FK_EmpT = EmpT;
                    PJSubject pjsubject = new PJSubject();
                    pjsubject.NewsGroup = NewsGroup;
                    pjsubject.RefOID = RefOID;
                    pjsubject.PJGroup = PJGroup;
                    pjsubject.ID = MyOID;
                    string strResult = saveAll(pjEmp, pjsubject, Num, strIP);
                    GeFun.ShowMessage(this.Page, "Result", strResult);
                }
                else
                {
                    BP.GE.GeFun.ShowMessage(this.Page, "strJS1", "对不起先请登录!");
                }
            }
            else
            {
                string NewsGroup = this.NewsGroup;
                string RefOID = this.RefOID;
                int PJGroup = this.PJGroup;
                string Emp = this.FK_Emp;
                string EmpT = this.FK_EmpT;
                string Num = hidden.Value;
                string strIP = GeFun.getIp();
                PJEmpInfo pjEmp = new PJEmpInfo();
                pjEmp.FK_Emp = Emp;
                pjEmp.FK_EmpT = EmpT;
                PJSubject pjsubject = new PJSubject();
                pjsubject.NewsGroup = NewsGroup;
                pjsubject.RefOID = RefOID;
                pjsubject.PJGroup = PJGroup;
                pjsubject.ID = MyOID;
                string strResult = saveAll(pjEmp, pjsubject, Num, strIP);
                GeFun.ShowMessage(this.Page, "Result", strResult);
            }
        }
        /// <summary>
        /// 保存评价信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pjEmp"></param>
        private string saveAll(PJEmpInfo pjEmp, PJSubject pjsubject, string key, string IP)
        {
            //查询该用户是否已经参加过此投票
            string strSql = "Select Count(*) from GE_PJEmpInfo where FK_Emp='" + pjEmp.FK_Emp + "' and FK_Subject='" + pjsubject.ID + "'";
            int j = BP.DA.DBAccess.RunSQLReturnValInt(strSql);
            if (j <= 0)
            {
                //是否已经添加过该组投票
                strSql = "select count(*) from GE_PJSubject where ID='" + pjsubject.ID + "'";
                int i = BP.DA.DBAccess.RunSQLReturnValInt(strSql);
                if (i <= 0)
                {
                    //保存评价主体
                    pjsubject.Save();
                    //初始化总票数
                    strSql = "insert into GE_PJTotal select '" + pjsubject.ID + "', PJNum,0 from GE_PJType where PJGroup=" + pjsubject.PJGroup;
                    i = BP.DA.DBAccess.RunSQL(strSql);
                }
                strSql = "update GE_PJTotal set Total=Total+1" + " where Fk_Subject='" + pjsubject.ID + "' and FK_Num='" + key + "'";
                int result = BP.DA.DBAccess.RunSQL(strSql);
                //保存评价人信息
                pjEmp.FK_Subject = pjsubject.ID;
                pjEmp.IP = IP;
                pjEmp.RDT = DateTime.Now;
                pjEmp.Save();
                return "评价成功!";
            }
            else
            {
                //已经评价过
                return "只能评价一次!";
            }
        }

        /// <summary>
        /// 重载呈现
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<link rel='stylesheet'type='text/css' href='" + ResUrl + "GE/PJ/Stylesheet.css'/> ");
            writer.Write("<script type='text/javascript' src='" + ResUrl + "GE/PJ/JScript.js'></script> ");
            if (this.DesignMode)
            {
                DesignView(writer);
            }
            else
            {
                RunningView(writer);
            }
        }

        #region 设计视图
        /// <summary>
        /// 设计视图
        /// </summary>
        /// <param name="writer"></param>
        private void DesignView(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "divpjhead");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("(目前评价人数:[数据绑定],");
            writer.Write("资源分为:[数据绑定]分。)");
            writer.Write("&nbsp;您的评价是:");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tabpj");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            if (this.DisplayMode == eDisplayMode.Horizontal)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "trpjh");
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                for (int i = 0; i <= 4; i++)
                {
                    RowsCount++;
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tdpjh");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    //img begin
                    if (IsShowPic == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, "#");
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "picpjh");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);
                        writer.RenderEndTag();
                        writer.WriteBreak();
                    }
                    //img end
                    if (IsShowTitle == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "spanpjh");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write("[数据绑定]");
                        writer.RenderEndTag();
                        writer.WriteBreak();
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();  //</input>
                    writer.RenderEndTag();  //</td>
                }
                writer.RenderEndTag();      //</tr>               
            }
            else if (this.DisplayMode == eDisplayMode.Vertical)
            {
                for (int i = 0; i <= 4; i++)
                {
                    RowsCount++;
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "trpjv");
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tdpjv");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    //img begin
                    if (IsShowPic == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, "#");
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "picpjv");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);
                        writer.RenderEndTag();
                    }
                    //img end
                    if (IsShowTitle == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "spanpjv");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write("[数据绑定]");
                        writer.RenderEndTag();
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "GetVal()");
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();  //</input>
                    writer.RenderEndTag();  //</td>
                    writer.RenderEndTag();  //</tr> 
                }
            }
            writer.RenderEndTag();          //</table>
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "divfoot");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            btnSubmit.RenderControl(writer);
            writer.RenderEndTag();          //</div>
        }
        #endregion

        #region 运行视图
        /// <summary>
        /// 运行视图
        /// </summary>
        /// <param name="writer"></param>
        private void RunningView(HtmlTextWriter writer)
        {
            int level = 0;
            int PJEmpCount = 0;
            int PJScoreCount = 0;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" SELECT GE_PJTotal.Total,GE_PJType.Score FROM GE_PJTotal,GE_PJType");
            sbSql.Append(" WHERE GE_PJTotal.FK_Subject='" + MyOID + "'");
            sbSql.Append(" AND GE_PJTotal.FK_Num=GE_PJType.PJNum");
            DataTable dtTotal = BP.DA.DBAccess.RunSQLReturnTable(sbSql.ToString());

            if (dtTotal.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTotal.Rows)
                {
                    PJEmpCount += Convert.ToInt32(dr["Total"]);
                    PJScoreCount += Convert.ToInt32(dr["Total"]) * Convert.ToInt32(dr["Score"]);
                }
                level = PJScoreCount / PJEmpCount;
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "divpjhead");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "divPJ");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (PJEmpCount > 0)
            {
                writer.Write("(目前评价人数:" + PJEmpCount.ToString() + ",");
                writer.Write("资源分为:" + level.ToString() + "分。)");
            }
            else
            {
                writer.Write("暂时没有人评价该信息。");
            }
            writer.Write("&nbsp;您的评价是:");
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tabpj");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            PJTypes PJS = new PJTypes();
            QueryObject qo = new QueryObject(PJS);
            qo.AddWhere(PJTypeAttr.PJGroup, "=", PJGroup);
            qo.DoQuery();

            if (this.DisplayMode == eDisplayMode.Horizontal)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "trpjh");
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                foreach (PJType PJ in PJS)
                {
                    RowsCount++;
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tdpjh");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    //img begin
                    if (IsShowPic == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + PJ.Pic);
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "picpjh");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);
                        writer.RenderEndTag();
                        writer.WriteBreak();
                    }
                    //img end
                    if (IsShowTitle == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "spanpjh");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(PJ.Title);
                        writer.RenderEndTag();
                        writer.WriteBreak();
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, "RdPJ");
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, PJ.PJNum);
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "GetVal('" + hidden.ClientID + "')");
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();      //</input>
                    writer.RenderEndTag();      //</td>
                }
                writer.RenderEndTag();          //</tr>               
            }
            else if (this.DisplayMode == eDisplayMode.Vertical)
            {
                foreach (PJType PJ in PJS)
                {
                    RowsCount++;
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "trpjv");
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tdpjv");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    //img begin
                    if (IsShowPic == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, ResUrl + PJ.Pic);
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "picpjv");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);
                        writer.RenderEndTag();
                    }
                    //img end
                    if (IsShowTitle == true)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "spanpjv");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(PJ.Title);
                        writer.RenderEndTag();
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, "RdPJ");
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, PJ.PJNum);
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "GetVal('" + hidden.ClientID + "')");
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();      //</input>
                    writer.RenderEndTag();      //</td>
                    writer.RenderEndTag();      //</tr> 
                }
            }
            writer.RenderEndTag();              //</table>

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "divResult");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "divResult");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            AddResult(writer);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "divfoot");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            btnSubmit.RenderControl(writer);
            hidden.RenderControl(writer);
            writer.RenderEndTag();              //</div>
        }

        private void AddResult(HtmlTextWriter writer)
        {
            int sum = 0;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" select * from GE_PJSubject,GE_PJTotal,GE_PJType ");
            sbSql.Append(" where GE_PJSubject.PJGroup=GE_PJType.PJGroup ");
            sbSql.Append(" and GE_PJSubject.ID=GE_PJTotal.FK_Subject ");
            sbSql.Append(" and GE_PJTotal.FK_Num=GE_PJType.PJNum ");
            sbSql.Append(" and GE_PJSubject.ID='" + MyOID + "' ");
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sbSql.ToString());
            if (dt.Rows.Count > 0)
            {
                sum = Convert.ToInt32(dt.Compute("sum(Total)", "1=1"));
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTable");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTDTitle1");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("投票项");
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTDTitle2");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("比例");
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTDTitle3");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("图示");
                writer.RenderEndTag();
                writer.RenderEndTag();

                foreach (DataRow dr in dt.Rows)
                {
                    int width = Convert.ToInt32(dr["Total"]) * 100 / sum;
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTD1");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(dr["Title"].ToString());
                    writer.RenderEndTag();
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTD2");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write((Convert.ToInt32(dr["Total"]) * 100 / sum).ToString() + "%");
                    writer.RenderEndTag();
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTD3");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, "background-color:blue; width:" + width + "%");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.Write("&nbsp;");
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
        }
        #endregion

        /// <summary>
        /// 生成主键OID
        /// </summary>
        [Browsable(false)]
        private string MyOID
        {
            get
            {
                return this.NewsGroup + this.RefOID + PJGroup.ToString();
            }
        }

        public static void CreateTables()
        {
            PJSubjects pjsubjects = new PJSubjects();
            PJTotals pjtotals = new PJTotals();
            PJEmpInfos pjempinfos = new PJEmpInfos();
            PJTypes pjtypes = new PJTypes();
            pjsubjects.RetrieveAll();
            pjtotals.RetrieveAll();
            pjempinfos.RetrieveAll();
            pjtypes.RetrieveAll();
        }
    }
}