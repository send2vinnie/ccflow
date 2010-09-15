using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;
using System.IO;
using System.Collections.Generic;

public partial class FAQ_OKDesc : BP.Web.WebPage
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(BP.Edu.EduUser.No) || BP.Edu.EduUser.No.Contains("admin"))
            Response.Write("<script> window.parent.location.href='../Port/SignIn.aspx?Jurl="+Request.RawUrl+"';</script>");
        if (!IsPostBack)
        {
            Question myquestion2 = new Question();
            myquestion2.Retrieve(QuestionAttr.OID, this.RefOID);
            myquestion2.NumOfRead += 1;
            myquestion2.Update();
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
            setQuestion();
            setIsOK();
            setAnswer();
        }
        catch
        { }
    }

    public void setQuestion()
    {
        #region 绑定问题
        Question q = new Question(this.RefOID);
        //if (EduUser.IsAdmin)
        //{
        //    if (q.FK_Dept.Contains(EduUser.FK_Dept))
        //        isAdmin = true;
        //}
        //person(q.FK_Emp);//绑定个人信息

        //-----------------------------------Beging-----------------------------
        this.PubQuestion.Add("<table width='98%' cellpadding='0' cellspacing='0'>");
        this.PubQuestion.Add("<tr><td width='19'><img src='Img/round_1.gif' alt='' /></td>");
        this.PubQuestion.Add("<td style='background:url(Img/table_top_bg.gif) repeat-x left top;text-align:left;font-size:14px;font-weight:bolder'>状态:<img src='Img/1.gif' /></td>");
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
            this.PubQuestion.Add("<td style='text-align:left;font-size:14px;font-weight:bolder;padding-top:20px;padding-left:10px;padding-right:20px;'>适用于：" + q.FK_WorkName + "/" + zj.Name + "&nbsp&nbsp资源类型:" + q.FK_BTypeName + "</td>");
            this.PubQuestion.Add("<td  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
            this.PubQuestion.Add("</tr>");
        }
        catch 
        {

            this.PubQuestion.Add("<tr>");
            this.PubQuestion.Add("<td style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");
            this.PubQuestion.Add("<td style='text-align:left;font-size:14px;font-weight:bolder;padding-top:20px;padding-left:10px;padding-right:20px;'>适用于：获取章节信息失败 &nbsp&nbsp资源类型:" + q.FK_BTypeName + "</td>");
            this.PubQuestion.Add("<td  style='background:url(Img/table_right_bg.gif) repeat-y left top'></td>");
            this.PubQuestion.Add("</tr>");
        }
        



        this.PubQuestion.Add("<tr>");
        this.PubQuestion.Add("<td style='background:url(Img/table_left_bg.gif) repeat-y left top'></td>");
        this.PubQuestion.Add("<td style='text-align:right;padding:30px 0 20px'>");

        string s = null;
        if (q.RDT.Length == 0)
            s = q.RDT;
        else
            s = q.RDT.Substring(0,10);

        this.PubQuestion.Add("提问者：<a href=\"javascript:DoLook('" + q.FK_Emp + "')\">" + q.FK_EmpName + "</a>&nbsp;&nbsp;||&nbsp;&nbsp;" + s);

        if (EduUser.No == q.FK_Emp)
        {
            //this.PubQuestion.Add("<a href=\"javascript:DoDel('" + this.RefOID + "')\">删除</a>");
        }
        //if (isAdmin)
        //{
        //    //是管理员，删除自己的帖子
        //    if (EduUser.No == q.FK_Emp)
        //    {
        //        this.PubQuestion.Add("&nbsp;&nbsp;||&nbsp;&nbsp;<a href=\"javascript:DoDel('" + this.RefOID + "')\">删除</a>");
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
        this.PubQuestion2.Add("</br>");

        //---------------------------------------------------------------END
        #endregion
    }
    public void setIsOK()
    {

        QDtl qdtl = new QDtl();
        qdtl.Retrieve(QDtlAttr.FK_Question, this.RefOID, QDtlAttr.IsOK, "1");

        this.PubIsOK.Add("<table width='98%' cellpadding='0' cellspacing='0' style='margin-bottom:10px;margin-top:5px'>");
        this.PubIsOK.Add("<tr><td width='19'><img src='Img/round_11.gif' alt='' /></td>");
        this.PubIsOK.Add("<td  style='background:url(Img/table_top_bg1.gif) repeat-x left top;text-align:left'>" +
        "<p style='padding:3px 0 0 23px;background:url(Img/icon_right.gif) left 0.1em no-repeat;font-size:14px;font-weight:bolder'>最佳答案</p></td>");
        this.PubIsOK.Add("<td width='19'><img src='Img/round_21.gif' alt='' /></td>");
        this.PubIsOK.Add("</tr>");
        this.PubIsOK.Add("<tr>");
        this.PubIsOK.Add("<td style='background:url(Img/table_left_bg1.gif) repeat-y left top'></td>");
        this.PubIsOK.Add("<td style='text-align:left;padding-top:15px;padding-left:25px'>");
        this.PubIsOK.Add(qdtl.DocHtml);
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
            //this.PubIsOK.Add("<p style='margin-top:15px'><a href=\"javascript:DownLoad('" + qdtl.MyPK + "," + this.RefOID + "')\"><img src= '../Images/FileType/" + EXT + ".gif'  />" + qdtl.FileName + "</a></p>");
            //this.PubIsOK.Add("<p style='margin-top:15px'><a href=\"javascript:Down('" + qdtl.MyPK + "," + Glo.R2IPServ + "')\"><img src= '../Images/FileType/" + EXT + ".gif'  />" + qdtl.FileName + "</a></p>");
            //this.PubIsOK.Add("<p style='margin-top:15px'><a href='Http://192.168.0.154/r2/PSV_R2_Res.aspx?RefOID=FAQ"+qdtl.MyPK + "'><img src= '../Images/FileType/" + EXT + ".gif'  />" + qdtl.FileName + "</a></p>");
            this.PubIsOK.Add("<p style='margin-top:15px'><a target='_blank' href=\"/edu/sharefile/sfDtl.aspx?RefOID=FAQ" + qdtl.MyPK + "\"><img src= '../Images/FileType/" + EXT + ".gif'  />" + qdtl.FileName + "</a></p>");

        }

        this.PubIsOK.Add("</td>");
        this.PubIsOK.Add("<td style='background:url(Img/table_right_bg1.gif) repeat-y left top'></td>");
        this.PubIsOK.Add("</tr>");
        this.PubIsOK.Add("<tr>");
        this.PubIsOK.Add("<td style='background:url(Img/table_left_bg1.gif) repeat-y left top'></td>");
        this.PubIsOK.Add("<td style='text-align:right;padding-right:10px;padding-top:10px;padding-bottom:20px'>");

        string qrdt = null;
        if (qdtl.RDT.Length == 0)
            qrdt = qdtl.RDT;
        else
            qrdt = qdtl.RDT.Substring(0,10);
        this.PubIsOK.Add("回答者：<a href=\"javascript:DoLook('" + qdtl.FK_Emp + "')\">" + qdtl.FK_EmpName + "</a>&nbsp;&nbsp;||&nbsp;&nbsp;" + qrdt);

        this.PubIsOK.Add("</td>");
        this.PubIsOK.Add("<td style='background:url(Img/table_right_bg1.gif) repeat-y left top'></td>");
        this.PubIsOK.Add("</tr>");
        this.PubIsOK.Add("<tr><td valign='top'><img src='Img/round_31.gif' alt='' /></td>" +
            "<td  style='background:url(Img/table_bottom_bg1.gif) repeat-x left top'>&nbsp;</td>" +
        "<td valign='top'><img src='Img/round_41.gif' alt='' /></td></tr>");
        this.PubIsOK.Add("</table>");
    }
    public void setAnswer()
    {
        QDtls qdtls = new QDtls();
        qdtls.Retrieve(QDtlAttr.FK_Question, this.RefOID, QDtlAttr.IsOK, "0");

        if (qdtls.Count > 0)
            this.PubAnswer.Add("<h2 style='width:98%;margin:0 aotu;text-align:left;font-size:14px;font-weight:bolder;border-bottom:2px solid #A5DA94'>其他回答：(共" + qdtls.Count + "条)</h2>");
        foreach (QDtl qdtl in qdtls)
        {
            this.PubAnswer.Add("<table cellpadding='0' cellspacing='0'  style='width:100%;height:120px;margin-top:10px; vertical-align:top; text-align:left;border:1px solid #A5DA94;border-collapse:collapse'>");
            this.PubAnswer.Add("<tr> ");
            this.PubAnswer.Add("<td style='padding:20px 35px 5px;vertical-align:top'>");
            this.PubAnswer.Add(qdtl.DocHtml);
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
                this.PubAnswer.Add("<a target='_blank' href=\"/edu/sharefile/sfdtl.aspx?RefOID=FAQ" + qdtl.MyPK + "\"><img src= '../Images/FileType/" + EXT + ".gif'  />" + qdtl.FileName + "</a>");
            }
            this.PubAnswer.Add("</td>");
            this.PubAnswer.Add("</tr>");

            this.PubAnswer.Add("<tr align=right>");
            this.PubAnswer.Add("<td style='padding-right:30px;padding-bottom:20px;padding-top:15px'>");

            string rdt = null;

            if (qdtl.RDT.Length == 0)
                rdt = qdtl.RDT;
            else
                rdt = qdtl.RDT.Substring(0, 10);

            this.PubAnswer.Add("回答者：<a href=\"javascript:DoLook('" + qdtl.FK_Emp + "')\">" + qdtl.FK_EmpName + "</a>&nbsp;&nbsp;||&nbsp;&nbsp;" + rdt);

            this.PubAnswer.Add("</td>");
            this.PubAnswer.Add("</tr>");
            this.PubAnswer.Add("</table>");

        }
    }
}
