using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.Edu;
using BP.En;
using System.IO;
using System.Collections.Generic;

public partial class FAQ_InitDesc : BP.Web.WebPage
{
    private string emp = "";
    private bool QuestionRight = false;
    SCTypes sctypes = new SCTypes();
    SCType sct = new SCType();
    public List<SCType> sctypelist = new List<SCType>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
          System.Web.HttpContext.Current.Response.Buffer = true;
          System.Web.HttpContext.Current.Response.Expires = 0;
          System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
          System.Web.HttpContext.Current.Response.AddHeader("pragma", "no-cache");
          System.Web.HttpContext.Current.Response.AddHeader("cache-control", "private");
          System.Web.HttpContext.Current.Response.CacheControl = "no-cache";

          if (string.IsNullOrEmpty(BP.Edu.EduUser.No) || BP.Edu.EduUser.No.Contains("admin"))
            Response.Write("<script> window.parent.location.href='../Port/SignIn.aspx?Jurl="+Request.RawUrl+"';</script>");

        if (EduUser.No != null && EduUser.No != "")
            this.GEFavorite1.Visible = true;
        else
            this.GEFavorite1.Visible = false;
        if (!IsPostBack)
        {
            try
            {
                Question myquestion2 = new Question();
                myquestion2.Retrieve(QuestionAttr.OID, this.RefOID);
                myquestion2.NumOfRead += 1;
                myquestion2.Update();
            }
            catch { }
        }
        try
        {
            Question question = new Question();
            QueryObject qo = new QueryObject(question);
            qo.AddWhere(QuestionAttr.OID, this.RefOID);
            int num = qo.DoQuery();
            if (num == 0)
            {
                this.Alert("此信息不存在");
                return;
            }
            else if (question.Sta == 1)
            {
                this.Response.Redirect("OKDesc.aspx?RefOID=" + this.RefOID);
            }
            if (question.FK_Emp != BP.Edu.EduUser.No)//本人不能回复
            {
                setData();
                QuestionRight = false;
            }
            else
            {
                QuestionRight = true;
            }

            string yf = DateTime.Now.ToString("yyMM");
        }
        catch
        { }

        try
        {

            setHuiDaList();
        }
        catch { }
    }

    /// <summary>
    /// 回复
    /// </summary>
    public void setData()
    {
        try
        {
            QDtls qdtl1 = new QDtls();
            QueryObject qos = new QueryObject(qdtl1);
            qos.AddWhere(QDtlAttr.MyPK, this.RefOID + "_" + EduUser.No);
            qos.addAnd();
            qos.AddWhere(QDtlAttr.FK_Emp, BP.Edu.EduUser.No);

            int num = qos.DoQuery(); //说明已经回复,不能回复，可编辑、删除
            if (num > 0)
            {
                this.PanelIng.Visible = false;
            }
            else
            {
                this.PanelIng.Visible = true;
                BindRe();
            }
        }
        catch
        { }

    }
    /// <summary>
    /// 回复窗口
    /// </summary>
    public void BindRe()
    {
        this.PubIng.Add("<table width='98%' cellpadding='0' cellspacing='0' style='font-size:13px;margin-top:40px' >");
        this.PubIng.Add("<tr><td width='19'><img src='Img/round_1.gif' alt='' /></td>");
        this.PubIng.Add("<td colspan='2' style='background:url(Img/table_top_bg.gif) repeat-x left top;text-align:left;font-size:14px;font-weight:bolder'>我来回答</td>");
        this.PubIng.Add("<td width='19'><img src='Img/round_2.gif' alt='' /></td>");

        this.PubIng.Add("</tr>");

        this.PubIng.Add("<tr><td  style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");

        this.PubIng.Add("<td style='width:70px;padding:20px 0px 20px;text-align:left;vertical-align:center;font-weight:bolder'>内容</td>");
        TextBox tb = new TextBox();
        tb.ID = "TB_Doc";
        tb.Rows = 10;
        tb.Columns = 55;
        tb.TextMode = TextBoxMode.MultiLine;

        this.PubIng.Add("<td style='padding-top:20px;padding-bottom:20px;text-align:left;'>");
        this.PubIng.Add(tb);
        this.PubIng.Add("</td>");

        this.PubIng.Add("<td align='left'  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
        this.PubIng.Add("</tr>");




        this.PubIng.Add("<tr><td  style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");

        this.PubIng.Add("<td style='width:70px;padding:20px 0px 20px;text-align:left;vertical-align:top;font-weight:bolder'>作者</td>");

        this.PubIng.Add("<td style='padding-bottom:0px;text-align:left;'>");
        tb = new TextBox();
        tb.ID = "TB_Doc2";
        tb.Columns = 10;
        tb.Text = EduUser.Name;
        this.PubIng.Add(tb);
        this.PubIng.Add("</td>");
        this.PubIng.Add("<td align='left'  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
        this.PubIng.Add("</tr>");



        this.PubIng.Add("<tr><td  style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");

        this.PubIng.Add("<td style='width:70px;padding:20px 0px 20px;text-align:left;vertical-align:top;font-weight:bolder'>文件上传</td>");

        HtmlInputFile file = new HtmlInputFile();
        file.ID = "file";
        file.Size = 60;
        this.PubIng.Add("<td style='padding-bottom:0px;text-align:left;'>");
        this.PubIng.Add(file);
        this.PubIng.Add("</td>");
        this.PubIng.Add("<td align='left'  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
        this.PubIng.Add("</tr>");


        this.PubIng.Add("<tr><td  style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");

        Button btnSave = new Button();
        btnSave.ID = "Btn_Save";
        btnSave.Text = "提交回答";
        btnSave.OnClientClick = "return Click();";
        btnSave.Click += new EventHandler(btnSave_Click);

        this.PubIng.Add("<td colspan='2' style='padding-bottom:20px;text-align:left;padding-left:110px'>");
        this.PubIng.Add(btnSave);
        this.PubIng.Add("</td>");

        this.PubIng.Add("<td align='left'  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
        this.PubIng.Add("</tr>");

        this.PubIng.Add("<tr><td valign='top'><img src='Img/round_3.gif' alt='' /></td>" +
           "<td  colspan='2' style='background:url(Img/table_bottom_bg.gif) repeat-x left top'>&nbsp;</td>" +
       "<td valign='top'><img src='Img/round_4.gif' alt='' /></td></tr>");
        this.PubIng.Add("</table>");


    }

    void btnSave_Click(object sender, EventArgs e)
    {
        Question q = new Question(this.RefOID);
        string fileName = "";
        string ext = "";
        QDtl qdtl = new QDtl();
        HtmlInputFile file = (HtmlInputFile)this.PubIng.FindControl("file");

        qdtl.Doc = this.PubIng.GetTextBoxByID("TB_Doc").Text;
        //if (qdtl.Doc != null && qdtl.Doc != "")
        //{
        //    if (qdtl.Doc.Length < 10)
        //    {
        //        this.Alert("内容过少，应不少于10字符");
        //        return;
        //    }
        //    if (qdtl.Doc.Length > 499)
        //    {
        //        this.Alert("内容过多，应不多于500字符");
        //        return;
        //    }
        //}
        //else
        //{
        //    this.Alert("内容为空，请认真填写！");
        //    return;
        //}
        qdtl.RDT = DataType.CurrentDataTime;
        qdtl.FK_Dept = BP.Edu.EduUser.FK_Dept;
        qdtl.FK_Emp = BP.Edu.EduUser.No;
        qdtl.FK_Question = this.RefOID.ToString();
        qdtl.MyPK = this.RefOID + "_" + EduUser.No;
        qdtl.FK_ZJ = q.FK_ZJ;//获取章节
        qdtl.Author = this.PubIng.GetTextBoxByID("TB_Doc2").Text;
        if (file.Value.Contains(":"))
        {
            try
            {
                string ny = DateTime.Now.ToString("yyMM");
                String filePath = file.PostedFile.FileName;
                fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                ext = fileName.Substring(fileName.LastIndexOf(".") + 1);
                fileName = fileName.Substring(0, fileName.LastIndexOf("."));
                if (!Directory.Exists("D:\\ShiDai\\FDB\\FAQ\\" + q.FK_KM + "\\" + ny + "\\"))
                {
                    Directory.CreateDirectory("D:\\ShiDai\\FDB\\FAQ\\" + q.FK_KM + "\\" + ny + "\\");
                }
                file.PostedFile.SaveAs("D:\\ShiDai\\FDB\\FAQ\\" + q.FK_KM + "\\" + ny + "\\" + qdtl.MyPK + "." + ext);
                //注释
                #region

                try
                {
                    string localFile = "D:\\ShiDai\\FDB\\FAQ\\" + q.FK_KM + "\\" + ny + "\\" + qdtl.MyPK + "." + ext;
                    FtpSupport.FtpConnection conn = Glo.FileFtpConn;
                    string ftpPath = "/FAQ/" + q.FK_KM + "/" + ny;

                    conn.SetCurrentDirectory("/FAQ/");


                    if (conn.DirectoryExist(q.FK_KM) == false)
                        conn.CreateDirectory(q.FK_KM);

                    conn.SetCurrentDirectory("/FAQ/" + q.FK_KM);

                    if (conn.DirectoryExist(ny) == false)
                        conn.CreateDirectory(ny);


                    conn.SetCurrentDirectory("/FAQ/" + q.FK_KM + "/" + ny);


                    if (conn.FileExist("/FAQ/" + q.FK_KM + "/" + DateTime.Now.ToString("yyMM") + "/" + qdtl.MyPK + "." + ext) == false)
                        conn.PutFile(localFile, qdtl.MyPK + "." + ext);
                }
                catch
                {
                    File.Delete("D:\\ShiDai\\FDB\\FAQ\\" + q.FK_KM + "\\" + ny + "\\" + qdtl.MyPK + "." + ext);
                }
            }

            catch
            {
                this.Alert("上传文件过程中出现异常！");
                return;
            }

            #endregion

            qdtl.FK_Work = q.FK_Work;
            qdtl.FileName = fileName;
            qdtl.FileExt = ext;
            qdtl.FSize = file.PostedFile.ContentLength;
            qdtl.FK_BType = q.FK_BType;
            ResExt re = new ResExt();
            int num = re.Retrieve(ResExtAttr.Name, ext);
            if (num > 0)
            {
                qdtl.FK_Type = re.FK_Type;
            }
            else
            {
                qdtl.FK_Type = "07";
            }

        }
        qdtl.Insert();

        q.NumOfRe += 1;
        q.Update();
        //次数累加
        try
        {
            PerEmp.FAQDtlCounts();
        }
        catch (Exception ex)
        {
            //ex.Message;

        }
        string url = "../FAQ/InitDesc.aspx?RefOID=" + this.RefOID;
        Glo.Trade("FAQ_AN", "", qdtl.MyPK.ToString(), q.Title, url);

        this.Response.Redirect("InitDesc.aspx?RefOID=" + this.RefOID);
    }


    /// <summary>
    /// 回答列表
    /// </summary>
    public void setHuiDaList()
    {
        bool isSH = false;
        if (EduUser.IsCHOfContext || EduUser.IsCHOfTech)
        {
            isSH = true;
        }


        #region 绑定问题。
        Question q = new Question(this.RefOID);
        if (EduUser.IsCHOfContext||EduUser.IsCHOfTech)
        {
            if (q.FK_Dept.Contains(EduUser.FK_Dept))
                isSH = true;
        }

        //---------------------------------------------
        this.PubQuestion.Add("<table width='100%' cellpadding='0' cellspacing='0' style='margin-top:0px'>");

        this.PubQuestion.Add("<tr><td width='19'><img src='Img/round_1.gif' alt='' /></td>");
        this.PubQuestion.Add("<td style='background:url(Img/table_top_bg.gif) repeat-x left top;text-align:left;font-size:14px;font-weight:bolder'>状态:<img src='Img/0.gif' /></td>");
        this.PubQuestion.Add("<td width='19'><img src='Img/round_2.gif' alt='' /></td>");
        this.PubQuestion.Add("</tr>");

        this.PubQuestion.Add("<tr><td  style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");

        this.PubQuestion.Add("<td style='padding-top:15px;text-align:left;font-size:14px;font-weight:bolder'><img src='Img/Cent.gif' />" + q.Cent + "&nbsp;&nbsp;" + q.Title + "</td>");

        this.PubQuestion.Add("<td align='left'  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
        this.PubQuestion.Add("</tr>");

        this.PubQuestion.Add("<tr>");
        this.PubQuestion.Add("<td style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");
        this.PubQuestion.Add("<td style='text-align:left;padding-top:15px;padding-left:35px;padding-right:20px;'>" + q.DescsHtml + "</td>");
        this.PubQuestion.Add("<td  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
        this.PubQuestion.Add("</tr>");

        try
        {
            ZhangJie zj = new ZhangJie(q.FK_ZJ);

            this.PubQuestion.Add("<tr>");
            this.PubQuestion.Add("<td style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");
            this.PubQuestion.Add("<td style='text-align:left;font-size:14px;font-weight:bolder;padding-top:20px; font-color:#009999;padding-left:10px;padding-right:20px;'>适用于：" + zj.FK_KMText + "." + zj.FK_NJText + "." + zj.FK_JCText + "." + zj.FK_CBText + "." + zj.HisParent.Name + "." + zj.Name + "&nbsp&nbsp资源类型:" + q.FK_BTypeName + "</td>");
            this.PubQuestion.Add("<td  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
            this.PubQuestion.Add("</tr>");

        }
        catch
        {
            this.PubQuestion.Add("<tr>");
            this.PubQuestion.Add("<td style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");
            this.PubQuestion.Add("<td style='text-align:left;font-size:14px;font-weight:bolder;padding-top:20px; font-color:#009999;padding-left:10px;padding-right:20px;'>适用于：  获取章节信息失败&nbsp&nbsp资源类型:" + q.FK_BTypeName + "</td>");
            this.PubQuestion.Add("<td  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
            this.PubQuestion.Add("</tr>");
        }

        this.PubQuestion.Add("<tr>");
        this.PubQuestion.Add("<td style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");
        this.PubQuestion.Add("<td style='text-align:right;padding:30px 0 20px'>");

        string qrdt = null;
        if (q.RDT.Length == 0)
            qrdt = q.RDT;
        else
            qrdt = q.RDT.Substring(0,10);

        this.PubQuestion.Add("提问者：<a href=\"javascript:DoLook('" + q.FK_Emp + "')\">" + q.FK_EmpName + "</a>&nbsp;&nbsp;||&nbsp;&nbsp;" + qrdt);

        if (EduUser.No == q.FK_Emp)
        {
            this.PubQuestion.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoEdit('" + this.RefOID + "')\">编辑</a>");

            this.PubQuestion.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoDel('" + this.RefOID + "')\">删除</a>");

        }
        //if (isSH)
        //{
        //    //是管理员，删除自己的帖子
        //    if (EduUser.No == q.FK_Emp)
        //    {

        //    }
        //    else
        //    {
        //        this.PubQuestion.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoDelPostByAdmin('" + this.RefOID + "')\" >删除</a>");
        //    }
        //}
        //this.PubQuestion.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoSC('" + this.RefOID + "')\" >收藏</a>&nbsp;&nbsp;&nbsp;&nbsp;");

        this.PubQuestion2.Add("</td>");
        this.PubQuestion2.Add("<td  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
        this.PubQuestion2.Add("</tr>");
        this.PubQuestion2.Add("<tr><td valign='top'><img src='Img/round_3.gif' alt='' /></td>" +
            "<td  style='background:url(Img/table_bottom_bg.gif) repeat-x left top'>&nbsp;</td>" +
        "<td valign='top'><img src='Img/round_4.gif' alt='' /></td></tr>");
        this.PubQuestion2.Add("</table>");

        //---------------------------------------------

        #endregion


        QDtls qdtls = new QDtls();
        qdtls.Retrieve(QDtlAttr.FK_Question, this.RefOID,QDtlAttr.RDT);
        if (qdtls.Count > 0)
            this.PubAnswer.Add("<h2 style='width:98%;margin:0 aotu;text-align:left;font-size:14px;font-weight:bolder;border-bottom:2px  solid #A5DA94'>回答：（共" + qdtls.Count + "条）</h2>");

        foreach (QDtl qdtl in qdtls)
        {
            //-----------------------------------------

            this.PubAnswer.Add("<table cellpadding='0' cellspacing='0'  style='width:98%;height:120px; vertical-align:top; text-align:left;border:1px solid #A5DA94;border-collapse:collapse;margin-bottom:10px;margin-top:5px'>");
            this.PubAnswer.Add("<tr> ");
            this.PubAnswer.Add("<td style='padding:20px 35px 5px;vertical-align:top; word-wrap: break-word; word-break: break-all;'>");
            Label label = new Label();
            label.ID = "Lab_Doc";
            label.Width = 500;
            label.Text = qdtl.DocHtml;
            this.PubAnswer.Add(label);
            if (qdtl.FileName != null && qdtl.FileName != "")
            {
                string EXT = "";
                ResExt re = new ResExt();
                int num = re.Retrieve(ResExtAttr.Name, qdtl.FileExt);
                if (num > 0)
                {
                    EXT = re.Name;
                }
                else
                {
                    EXT = "txt";
                }
                this.PubAnswer.Add("<p sytle='padding-bottom:10px'></p>");
                //this.PubAnswer.Add("<a href=\"javascript:Down('" + qdtl.MyPK + "," + this.RefOID + "')\"><img src= '../Images/FileType/" + EXT + ".gif'  />" + qdtl.FileName + "</a>");
                this.PubAnswer.Add("<p style='margin-top:15px'><a href=\"javascript:Down('" + qdtl.MyPK + "," + Glo.R2IPServ + "')\"><img src= '../Images/FileType/" + EXT + ".gif'  />" + qdtl.FileName + "</a></p>");
            }

            this.PubAnswer.Add("</td>");
            this.PubAnswer.Add("</tr>");

            this.PubAnswer.Add("<tr align=right>");
            this.PubAnswer.Add("<td style='padding-right:30px;padding-bottom:20px;'>");

            string rdt = null;

            if (qdtl.RDT.Length == 0)
                rdt = qdtl.RDT;
            else
                rdt = qdtl.RDT.Substring(0,10);

            this.PubAnswer.Add("回答者：<a href=\"javascript:DoLook('" + qdtl.FK_Emp + "')\">" + qdtl.FK_EmpName + "</a>&nbsp;&nbsp;||&nbsp;&nbsp;" +rdt);


            if (qdtl.FK_Emp == EduUser.No)
            {
                this.PubAnswer.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoDelMyRe('" + qdtl.MyPK + "," + this.RefOID + "')\" >删除</a>");
                //编辑 弹出 窗口
                this.PubAnswer.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoUpDate('" + qdtl.MyPK + "," + this.RefOID + "')\">编辑</a>");
            }
            //if (isSH)
            //{
            //    if (qdtl.FK_Emp == EduUser.No)
            //    {

            //    }
            //    else
            //    {
            //        this.PubAnswer.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoDelReByAdmin('" + qdtl.MyPK + "," + this.RefOID + "')\" >删除</a>");
            //    }
            //}
            if (q.FK_Emp == EduUser.No)
            {
                this.PubAnswer.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoCN('" + this.RefOID + ',' + qdtl.MyPK + "')\">采纳</a>");
            }

            this.PubAnswer.Add("</td>");
            this.PubAnswer.Add("</tr>");
            this.PubAnswer.Add("</table>");
            //-----------------------------------------
        }
    }
    //protected void Btn_SC_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    Question q = new Question(this.RefOID);

    //    string pk = "FAQ_" + EduUser.No + "_" + q.OID;

    //    SC sc = new SC();
    //    sc.Retrieve(SCAttr.MyPK, pk);

    //    if (sc.MyPK != null && sc.MyPK != "")
    //    {
    //        this.Alert("您已经收藏过该资源");
    //        return;
    //    }


    //    string FK_Type = null;
    //    FK_Type = "07";
    //    //  Glo.Fav("SF", sf.FK_Emp, this.RefOID.ToString(), FK_Type);
    //    this.Alert("收藏成功");
    //}
}
