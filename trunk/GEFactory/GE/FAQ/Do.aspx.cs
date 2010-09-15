using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.DA;
using BP.Edu;
using BP.Port;
using BP.En;
using System.IO;
using System.Collections.Generic;
public partial class Do : BP.Web.WebPage
{
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string FK_Type
    {
        get
        {
            return this.Request["FK_Type"].ToString();
        }
    }
    public string SCName
    {
        get
        {
            return this.Request.QueryString["SCName"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "CaiNa": // 采纳。
                Question q = new Question(this.RefOID);
                q.Sta = 1;
                q.Update();

                QDtl qdtl = new QDtl(this.RefNo);
                qdtl.IsOK = true;
                qdtl.Update();

                if (qdtl.FileExt.Length >= 2)
                {
                    try
                    {
                        OKRes okres = new OKRes();
                        okres.CheckPhysicsTable();

                        okres.Copy(qdtl);
                        okres.Title = q.Title;
                        okres.Descs = q.Descs;
                        okres.FileName = qdtl.FileName;
                        okres.FileExt = qdtl.FileExt;
                        okres.FK_ZJ = qdtl.FK_ZJ;
                        okres.FK_Dept = qdtl.FK_Dept;
                        okres.FK_Type = qdtl.FK_Type;
                        okres.RDT = qdtl.RDT;
                        okres.FK_KM = q.FK_KM;
                        okres.FK_Work = q.FK_Work;

                        //okres.FK_Work = q.FK_Work;
                        //okres.FK_ZJ = q.FK_ZJ;
                        okres.StaResCHZT = "3";
                        okres.SaveAsOID(this.RefOID);
                    }
                    catch
                    {
                        this.Alert("采纳失败!");
                    }
                    

                }
                string url = "../FAQ/OKDesc.aspx?RefOID=" + this.RefOID;
                Glo.Trade("FAQ_CN", qdtl.FK_Emp, this.RefOID.ToString(), q.Cent, q.Title, url);

                this.WinClose();
                break;
            case "NumOfRead": // 刷新阅读次数。
                string OID2 = this.Request.QueryString["OID"].ToString();
                Question myquestion2 = new Question();
                myquestion2.Retrieve(QuestionAttr.OID, OID2);
                myquestion2.NumOfRead += 1;
                myquestion2.Update();
                this.WinClose();
                break;
            case "FAQ": // 收藏。
               
                ////      Glo.Fav("FAQ", qq.FK_Emp, this.RefOID.ToString(),"07");
                //this.WinClose();
                //break;
                try
                {
                    int oid = 0;
                    if (this.RefOID.ToString().Contains("FAQ"))
                    {
                         oid = Convert.ToInt32(this.RefOID.ToString().Substring(3));
                    }
                    else
                    {
                        oid = this.RefOID;
                    }

                    Question qq = new Question(oid);
                    SC sctype = new SC();
                    sctype.Title = qq.Title;
                    sctype.Ext = "FAQ";
                    //sctype.Url=Url;

                    sctype.FK_Emp = BP.Edu.EduUser.No;
                    sctype.FK_ExtType = "07";

                    sctype.FK_KM = qq.FK_KM;
                    sctype.FK_NJ = qq.FK_NJ;
                    sctype.FK_Ver = qq.FK_Ver;
                    sctype.FK_ZJ = qq.FK_ZJ;
                    sctype.FSize = 0;
                    sctype.RefEmp = qq.FK_Emp;
                    sctype.RefOBJ = RefOID.ToString();
                    sctype.RDT = BP.DA.DataType.CurrentDataTime;
                    sctype.FK_Type = FK_Type;
                    sctype.FK_RESType = "FAQ";
                    sctype.Insert();
                }
                catch { }
                this.WinClose();
                break;

         
            case "DelFAQ": // 删除问题。
                Question qo = new Question();
                qo.OID = this.RefOID;
                qo.Delete();
                string url2 = "../FAQ/InitDesc.aspx?RefOID=" + this.RefOID;

                BLog log1 = new BLog();
                QueryObject qoe = new QueryObject(log1);
                qoe.AddWhere(BLogAttr.FK_BType, "FAQ_Ask");
                qoe.addAnd();
                qoe.AddWhere(BLogAttr.RefOBJ, qo.OID);
                qoe.DoQuery();

                log1.Delete();
                Actives.DelActive(qo.OID.ToString());
                //BType btype = new BType("FAQ_AskDel");
                //Glo.TradeDelete("FAQ_AskDel", qo.FK_Emp, this.RefOID.ToString(), btype.Cent, qo.Title, url2);
                //Actives.DelActive(this.RefOID.ToString());//个人中心信息删除

                //回复内容删除、附件删除
                QDtls qds = new QDtls();
                qds.Retrieve(QDtlAttr.FK_Question, this.RefOID);

                foreach (QDtl qd in qds)
                {
                    string aa = "d:\\ShiDai\\FDB\\FAQ\\" + qo.FK_KM + "\\" + qd.MyPK + "." + qd.FileExt;
                    if (File.Exists(aa))
                    {
                        File.Delete(aa);
                    }

                }
                qds.Delete();

                this.Alert("删除成功");
                this.WinClose();
                break;
            case "DoDelMyRe": // 删除回复。
                Question Q = new Question(this.RefOID);
                QDtl dtl = new QDtl();
                dtl.Retrieve(QDtlAttr.MyPK, this.RefNo);

                string aaa = "d:\\ShiDai\\FDB\\FAQ\\" + Q.FK_KM + "\\" + dtl.MyPK + "." + dtl.FileExt;
                if (File.Exists(aaa))
                {
                    File.Delete(aaa);
                }
                //删除服务器文件
                //string ny2 = Convert.ToDateTime(dtl.RDT).ToString("yyMM");
                //string local = "D:\\ShiDai\\FDB\\FAQ\\" + Q.FK_KM + "\\" + ny2 + "\\" + dtl.MyPK + "." + dtl.FileExt;
                //FtpSupport.FtpConnection conn2 = Glo.FileFtpConn;
                //string ftpPath2 = "/FAQ/" + Q.FK_KM + "/" + ny2 + "/" + dtl.MyPK + "." + dtl.FileExt;

                //if (conn2.FileExist(ftpPath2) == true)
                //{
                //    conn2.DeleteFile(ftpPath2);
                //}
                BLog log = new BLog();
                QueryObject qoe1 = new QueryObject(log);
                qoe1.AddWhere(BLogAttr.FK_BType, "FAQ_AN");
                qoe1.addAnd();
                qoe1.AddWhere(BLogAttr.RefOBJ, dtl.MyPK);
                qoe1.DoQuery();

                log.Delete();
                //Actives.DelActive(dtl.MyPK);
                //string url3 = "../FAQ/InitDesc.aspx?RefOID=" + this.RefOID;
                //Glo.TradeDelete("FAQ_ANDel", dtl.FK_Emp, this.RefNo, 2, Q.Title, url3);
                Q.NumOfRe -= 1;
                if (Q.NumOfRe < 0)
                {
                    Q.NumOfRe = 0;
                }

                Q.Update();
                dtl.Delete();
                this.Alert("删除成功");
                this.WinClose();
                break;
            case "DelFav":
                SC ss = new SC();
                ss.Retrieve(SCAttr.RefOBJ, this.RefOID);
                try
                {
                    ss.Delete();
                }
                catch (Exception ex)
                {
                    this.Alert("数据异常");
                    return;
                }
                this.WinClose();
                break;
            case "UpScanTimes":

                QDtl qdd = new QDtl(this.RefNo);
                qdd.NumBrowse = qdd.NumBrowse + 1;
                qdd.Update();
                this.WinClose();
                break;
            case "PJ": // 评价

                DoPJ();
                this.WinClose();
                break;
            default:
                throw new Exception("error " + this.DoType);
        }
    }
    //评价
    /// <summary>
    /// //refno为mypk 
    /// </summary>
    private void DoPJ()
    {
        QDtl QD = new QDtl(this.RefNo);
        string pjemps = QD.PJEmp;
        string[] pjemp = pjemps.Split(',');
        List<string> list = new List<string>();

        for (int i = 0; i < pjemp.Length; i++)
        {
            list.Add(pjemp[i]);
        }

        if (list.Contains(EduUser.No))
        {
            this.Alert("您已经对该资源进行过评价！");
            this.WinClose();
        }
        else
        {
            pjemps = pjemps + "," + EduUser.No;
            QD.PJEmp = pjemps;
            string name=Request.QueryString["pjtype"].ToString();
            switch(name)
            {
                case "PJA":
                    QD.NumA = QD.NumA + 1;
                    break;
                case "PJB":
                    QD.NumB = QD.NumB + 1;
                    break;
                case "PJC":
                    QD.NumC = QD.NumC + 1;
                    break;
                case "PJD":
                    QD.NumD = QD.NumD + 1;
                    break;
                default:
                    break;
            }
            QD.Update();
            int sum = QD.NumA * 5 + QD.NumB * 3 + QD.NumC * 2 - QD.NumD * 1;
            QD.Update(QDtlAttr.PRI, sum);
        }
    }
}
