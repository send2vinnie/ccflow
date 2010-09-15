using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
using BP.Edu.Res;

public partial class FAQ_Delete : BP.Web.WebPage
{
    public string DelType
    {
        get
        {
            string deltype = this.Request.QueryString["DelType"];
            return deltype;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (DelType == "Dtl")
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
        }
        else if (DelType == "Question")
        {
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }
        else
        {
            Alert("异常操作");
            return;
        }
    }
    protected void BtnDelAdmin_Click(object sender, EventArgs e)
    {
        string btype = null;
        int cent = 0;

        if (RbtnCon.Checked == false && RbtnFalse.Checked == false && RbtnHave.Checked == false)
        {
            this.Alert("请选择删除原因");
            return;
        }
        if (RbtnCon.Checked == true)
        {
            btype = "FAQ_AskDelCon";
            cent = 8;
        }
        else if (RbtnFalse.Checked == true)
        {
            btype = "FAQ_AskDelFalse";
            cent = 5;
        }
        else
        {
            btype = "FAQ_AskDelHave";
            cent = 1;
        }
        Question Q = new Question(this.RefOID);
        QDtls qds = new QDtls();
        qds.Retrieve(QDtlAttr.FK_Question, this.RefOID);
        foreach (QDtl qd in qds)
        {
            string aa = "d:\\ShiDai\\FDB\\FAQ\\" + Q.FK_KM + "\\" +Convert.ToDateTime(qd.RDT).ToString("yyMM") +"\\"+ qd.MyPK + "." + qd.FileExt;
            if (File.Exists(aa))
            {
                File.Delete(aa);
            }

            //删除文件服务器文件
            //string ny = Convert.ToDateTime(qd.RDT).ToString("yyMM");
            //string localFile = "D:\\ShiDai\\FDB\\FAQ\\" + Q.FK_KM + "\\" + ny + "\\" + qd.MyPK + "." + qd.FileExt;
            //FtpSupport.FtpConnection conn = Glo.FileFtpConn;
            //string ftpPath = "/FAQ/" + Q.FK_KM + "/" + ny + "/" + qd.MyPK + "." + qd.FileExt;

            //if (conn.FileExist(ftpPath) == true)
            //{
            //    conn.DeleteFile(ftpPath);
            //}
        }
        qds.Delete();

        string RefEmp = Q.FK_Emp;
        string RefOID = this.RefOID.ToString();
        string url = "../FAQ/InitDesc.aspx?RefOID=" + this.RefOID;
        Glo.TradeDelete(btype, RefEmp, RefOID, cent, Q.Title, url);
        Actives.DelActive(this.RefOID.ToString());//个人中心信息删除
        Q.Delete();

        SC s = new SC();
        s.Retrieve(SCAttr.RefOBJ, this.RefOID);
        s.Delete();

        //删除审核信息表里面的信息
        //市级审核表信息删除
        OKRes ok = new OKRes();
        ok.Retrieve(OKResAttr.OID,this.RefOID);
        ok.Delete();
        //区县与校级审核表信息删除
        SHDtl sh = new SHDtl();
        sh.Retrieve(SHDtlAttr.FK_SRC, this.RefOID);
        sh.Delete();

        this.Alert("删除成功……");
        this.WinClose();
    }
    protected void BtnDelAdmin1_Click(object sender, EventArgs e)
    {
        string btype = null;
        int cent = 0;

        if (RbtnCon1.Checked == false && RbtnFalse1.Checked == false && RbtnHave1.Checked == false)
        {
            this.Alert("请选择删除原因");
            return;
        }
        if (RbtnCon1.Checked == true)
        {
            btype = "FAQ_ANDelCon";
            cent = 8;
        }
        else if (RbtnFalse1.Checked == true)
        {
            btype = "FAQ_ANDelFalse";
            cent = 5;
        }
        else
        {
            btype = "FAQ_ANDelHave";
            cent = 1;
        }
        Question Q = new Question(this.RefOID);
        QDtl qd = new QDtl();
        qd.Retrieve(QDtlAttr.MyPK, this.RefNo);

        string aa = "d:\\ShiDai\\FDB\\FAQ\\" + Q.FK_KM + "\\"+Convert.ToDateTime(qd.RDT).ToString("yyMM") +"\\"+ qd.MyPK + "." + qd.FileExt;
        if (File.Exists(aa))
        {
            File.Delete(aa);
        }
        qd.Delete();
        string RefEmp = Q.FK_Emp;
        string RefOID = this.RefOID.ToString();
        string url = "../FAQ/InitDesc.aspx?RefOID=" + this.RefOID;
        Glo.TradeDelete(btype, RefEmp, RefOID, cent, Q.Title, url);
        //删除文件服务器文件
        //string ny = Convert.ToDateTime(qd.RDT).ToString("yyMM");
        //string localFile = "D:\\ShiDai\\FDB\\FAQ\\" + Q.FK_KM + "\\" + ny + "\\" + qd.MyPK + "." + qd.FileExt;
        //FtpSupport.FtpConnection conn = Glo.FileFtpConn;
        //string ftpPath = "/FAQ/" + Q.FK_KM + "/" + ny + "/" + qd.MyPK + "." + qd.FileExt;

        //if (conn.FileExist(ftpPath) == true)
        //{
        //    conn.DeleteFile(ftpPath);
        //}

        this.Alert("删除成功……");
        //this.Response.Redirect("ShowMsg.aspx?DoType=Del", true);
        this.WinClose();
    }
}
