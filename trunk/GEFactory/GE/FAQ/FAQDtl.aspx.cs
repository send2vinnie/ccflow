using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.En;
using BP.Edu;
using BP.Edu.Res;
using System.Data;
using System.IO;
using BP.Edu.TH;
using BP.Port;
using BP.Web;
using BP.Web.Controls;


public partial class R2_PSV_R2_Res : WebPage
{
    public string refOPK = string.Empty;

    // private string refOID=string.Empty;

    public string GeRefOID
    {
        get
        {
            return Convert.ToString(Request.QueryString["RefOID"]);
        }
    }

    private string _gropKey;

    public string GeGroupKey
    {
        get { return "1"; }
        set { _gropKey = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        GePJ1.RefOID = GeRefOID;
        GePJ1.PJGroup = 1;
        GeComment1.RefOID = GeRefOID;
        GeComment1.GroupKey = GeGroupKey;

        //if (EduUser.No != null && EduUser.No != "")
         //   this.UC_SC1.Visible = true;
       // else
        //    this.UC_SC1.Visible = false;

        if (!IsPostBack)
        {
            if (Request["RefOID"] == null)
                return;

        }

        #region 绑定最近浏览记录

        //BoundLastestBrowse();
        #endregion

        BoundListAll();

        #region 加入审核[zcj]

        if (this.GeRefOID.Substring(0, 3) == "RSF")
        {
            string oid = this.GeRefOID.Substring(3);


            //this.Alert(oid.ToString());
            try
            {

                ShareFile sf = new ShareFile(Convert.ToInt32(oid));
                if (sf.FK_Emp == EduUser.No)
                {
                    this.DivComment.Visible = false;
                    this.DivPJ.Visible = false;
                }
                else
                {
                    this.DivComment.Visible = true;
                    this.DivPJ.Visible = true;
                }
                if (EduUser.IsCHOfTech || EduUser.IsCHOfContext)
                {
                    if (EduUser.HisKMs.Contains(KMAttr.No, sf.FK_KM))
                        this.BindSH(sf);
                }
            }
            catch { }
        }
        #endregion

    }

    #region 根据不同类型的资源从不同的表中抽取数据

    public void BoundListAll()
    {
        string str_RefType = GeRefOID.Substring(0, 3);
        int str_OID = 0;
        if (str_RefType != "FAQ")
        {
            str_OID = int.Parse(GeRefOID.Substring(3));
        }
        switch (str_RefType)
        {
            #region 共享资源

            case "RSF"://

                try
                {
                    ShareFile resRSF = new ShareFile(str_OID);
                    ZhangJie zjsf = new ZhangJie(resRSF.FK_ZJ);

                    //StateCounts.AddBrows(resRSF.FK_RESType);
                    //ResViews.AddBrows(str_RefType, str_OID);//添加浏览次数

                    if (EduUser.No != null && EduUser.No != "")
                    {
                        //插入到数据表中
                        Browse brosf = new Browse();
                        //插入当前登录人的编号

                        brosf.FK_ExtType = resRSF.FK_Type;
                        brosf.FK_Emp = EduUser.No;
                        brosf.Title = resRSF.Title;
                        brosf.Ext = resRSF.Ext;
                        brosf.FSize = Convert.ToInt32(resRSF.FSize);
                        brosf.RefOBJ = this.GeRefOID.ToString();
                        brosf.RDT = System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss：ffff");

                        brosf.FK_ZJ = resRSF.FK_ZJ;

                        brosf.FK_ZJName = zjsf.RealName;


                        try
                        {
                            brosf.Insert();
                        }
                        catch
                        {
                            try
                            {
                                Browse upbrosesf = new Browse(EduUser.No + "_" + this.GeRefOID.ToString());
                                upbrosesf.Update(BrowseAttr.RDT, System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss：ffff"));
                            }
                            catch { }
                        }
                    }

                    resRSF.Update(ShareFileAttr.NumBrowse, ++resRSF.NumBrowse);
                    this.labBrowser.Text = resRSF.NumBrowse.ToString();//浏览次数
                    try
                    {
                        this.labBType.Text = new ResBType(resRSF.FK_BType).Name;
                    }
                    catch { }
                    this.labOID.Text = resRSF.OID.ToString();
                    this.labDown.Text = resRSF.NumDown.ToString();//下载次数
                    this.labRDT.Text = resRSF.RDT;//上传时间
                    float sfsize = Convert.ToSingle((float)(resRSF.FSize) / (float)(1024 * 1024));

                    if (sfsize == 0)
                        this.labSize.Text = "0.01";
                    else
                        this.labSize.Text = sfsize.ToString();
                    this.labTitle.Text = resRSF.Title;
                    try
                    {
                        this.labType.Text = new ResType(resRSF.FK_Type).Name;
                    }
                    catch { }
                    /*当前资源不存在*/
                    this.labIntegral.Text = resRSF.Cent.ToString();

                    this.labVer.Text = resRSF.FK_NJText + "-" + resRSF.FK_VerText + "-" + resRSF.FK_KMText + "-" + zjsf.RealName;//适用版本关联章节
                    try
                    {
                        this.labWorker.Text = resRSF.FK_EmpText;
                    }
                    catch
                    {
                        this.labWorker.Text = "资源网提供";
                    }

                    //相关资源
                    //List<ShareFile> ressflist = new List<ShareFile>();
                    //ShareFiles resRSFs = new ShareFiles();
                    //QueryObject qoqsf = new QueryObject(resRSFs);
                    //qoqsf.AddWhere(ShareFileAttr.FK_ZJ, resRSF.FK_ZJ);
                    //qoqsf.Top = 15;
                    //qoqsf.DoQuery();

                    //foreach (ShareFile re in resRSFs)
                    //{
                    //    re.MyPK = "RSF" + re.OID.ToString();
                    //    ressflist.Add(re);
                    //}
                    //this.DTL_Res_Static.DataSource = ressflist;

                    //this.DTL_Res_Static.DataBind();
                    Preview();
                }
                catch
                {
                    Img.ImageUrl = "/edu/sharefile/image/nopic.jpg";
                    this.Alert("该资源已不存在!");
                }
                break;
            #endregion

            #region 请求资源

            case "FAQ"://
                try
                {
                    string str_RefOID = GeRefOID.Substring(3);
                    QDtl qd = new QDtl();
                    qd.Retrieve(QDtlAttr.MyPK, str_RefOID);
                    Question Q = new Question();
                    Q.Retrieve(QuestionAttr.OID, qd.FK_Question);
                    ZhangJie zjfaq = new ZhangJie(qd.FK_ZJ);

                    //StateCounts.AddBrows(resRSF.FK_RESType);
                    //ResViews.AddBrows(str_RefType, str_RefOID);//添加浏览次数

                    //插入到数据表中

                    if (EduUser.No != null && EduUser.No != "")
                    {
                        Browse brofaq = new Browse();
                        //插入当前登录人的编号

                        brofaq.FK_ExtType = qd.FK_Type;
                        brofaq.FK_Emp = EduUser.No;
                        brofaq.Title = qd.FileName;
                        brofaq.Ext = qd.FileExt;
                        float faqsize = Convert.ToSingle((float)(qd.FSize) / (float)(1024 * 1024));

                        if (faqsize == 0)
                            this.labSize.Text = "0.01";
                        else
                            this.labSize.Text = faqsize.ToString();
                        brofaq.RefOBJ = this.GeRefOID.ToString();
                        brofaq.RDT = System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss：ffff");
                        try
                        {
                            //bro.FK_CB = resRSF.FK_CB;
                        }
                        catch { }
                        brofaq.FK_ZJ = qd.FK_ZJ;

                        brofaq.FK_ZJName = zjfaq.RealName;
                        //  brofaq.FK_NJ = resRSF.FK_NJ;
                        // brofaq.FK_KM = resRSF.FK_KM;

                        try
                        {
                            brofaq.Insert();
                        }
                        catch
                        {
                            try
                            {
                                Browse upbrosefaq = new Browse(EduUser.No + "_" + this.GeRefOID.ToString());
                                upbrosefaq.Update(BrowseAttr.RDT, System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss：ffff"));
                            }
                            catch { }
                        }
                    }

                    qd.Update(QDtlAttr.NumBrowse, qd.NumBrowse + 1);
                    this.labBrowser.Text = qd.NumBrowse.ToString();//浏览次数
                    try
                    {
                        this.labBType.Text = new ResBType(qd.FK_BType).Name;
                    }
                    catch { }
                    this.labOID.Text = qd.MyPK.ToString();
                    this.labDown.Text = qd.NumDown.ToString();//下载次数
                    this.labRDT.Text = qd.RDT;//上传时间
                    this.labSize.Text = System.Convert.ToString(qd.FSize);
                    this.labTitle.Text = qd.FileName;
                    try
                    {
                        this.labType.Text = new ResType(qd.FK_Type).Name;
                    }
                    catch { }
                    /*当前资源不存在*/
                    this.labIntegral.Text = "0";

                    this.labVer.Text = zjfaq.FK_KMText + "-" + zjfaq.FK_NJText + "-" + zjfaq.FK_JCText + "-" + zjfaq.FK_CBText + "-" + zjfaq.HisParent.Name + "_" + zjfaq.Name;//适用版本关联章节
                    try
                    {
                        this.labWorker.Text = qd.FK_EmpName;
                    }
                    catch
                    {
                        this.labWorker.Text = "资源网提供";
                    }

                    ////相关资源
                    //List<QDtl> resfaqlist = new List<QDtl>();
                    //QDtls qds = new QDtls();
                    //QueryObject qoqd = new QueryObject(qds);
                    //qoqd.AddWhere(QDtlAttr.FK_ZJ, qd.FK_ZJ);
                    //qoqd.Top = 15;
                    //qoqd.DoQuery();

                    //foreach (QDtl re in qds)
                    //{
                    //    re.MyPK = "FAQ" + re.MyPK;
                    //    resfaqlist.Add(re);

                    //}
                    //this.DTL_Res_Static.DataSource = resfaqlist;

                    //this.DTL_Res_Static.DataBind();
                    Preview();
                }
                catch
                {
                    Img.ImageUrl = "/edu/sharefile/image/nopic.jpg";
                    this.Alert("该资源已不存在或加载过程中出现异常!");
                }
                break;
            #endregion

            default:
                break;
        }
    }
    #endregion
    //-----------------------------------------------------------------------------------------------------------//
    #region  资源审核
    /// <summary>
    ///
    /// </summary>
    /// <param name="sf">sharefile类的对象</param>
    public void BindSH(ShareFile sf)
    {
        SFSHDtl mydtl = new SFSHDtl();
        mydtl.MyPK = EduUser.FK_Dept + "_" + sf.OID;
        //mydtl.RetrieveFromDBSources();

        int mydtlcount = mydtl.RetrieveFromDBSources();

        //this.Alert(mydtl.MyPK);

        if (mydtlcount != 0)
        {
            //如果审核中或审核通过

            string dbsql = "select mypk from Edu_SFSHDtl where mypk='" + EduUser.FK_Dept + "_" + sf.OID + "' and  StaResCHZT=0";
            int count = DBAccess.RunSQLReturnCount(dbsql);


            if (count == 0)
            {


                if (EduUser.IsCHOfTech && mydtl.StaOfJS != 0 || EduUser.IsCHOfContext && mydtl.StaOfNR != 0)
                {

                    this.Pub1.Add("<h2 style='margin-bottom:10px;padding-bottom:5px;font-size:12px;font-weight:bolder;color:#333;border-bottom:3px solid #0B80DF'>此资源审核情况如下</h2>");

                    this.Pub1.Add("<table style='width:100%;margin-top:10px' border='0'>");


                    this.Pub1.AddTR();
                    this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
                    this.Pub1.Add("属性");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
                    this.Pub1.Add("审核情况");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTREnd();

                    this.Pub1.AddTR();
                    this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
                    this.Pub1.Add("审核状态");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
                    this.Pub1.Add(mydtl.StaResCHZT_Text);
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTREnd();


                    this.Pub1.AddTR();
                    this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
                    this.Pub1.Add("资源标题");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
                    this.Pub1.Add(mydtl.Title);
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTREnd();

                    this.Pub1.AddTR();
                    this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
                    this.Pub1.Add("资源描述");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
                    this.Pub1.Add(mydtl.SubTitle);
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTREnd();
                    this.Pub1.AddTR();
                    this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
                    this.Pub1.Add("关键字");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
                    this.Pub1.Add(mydtl.KeyWord);
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTREnd();
                    this.Pub1.AddTR();
                    this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
                    this.Pub1.Add("技术审核意见");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
                    this.Pub1.Add(mydtl.DocOfJS.Replace("\r\n", "<br/>"));
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTREnd();
                    this.Pub1.AddTR();
                    this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
                    this.Pub1.Add("内容审核意见");
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
                    this.Pub1.Add(mydtl.DocOfNR.Replace("\r\n", "<br/>"));
                    this.Pub1.AddTDEnd();

                    this.Pub1.AddTREnd();

                    this.Pub1.AddTableEnd();
                    return;
                }
            }
        }


        // 判断是否市局审核。
        SFSHDtl dtl = new SFSHDtl();
        QueryObject qo = new QueryObject(dtl);
        qo.AddWhere(SFSHDtlAttr.FK_Dept, EduUser.FK_Dept);
        qo.addAnd();
        qo.AddWhere(SFSHDtlAttr.FK_ShareFile, this.GeRefOID.Substring(3));

        int numzt = qo.DoQuery();
        bool isCanSH = false;

        if (numzt == 0)
        {
            isCanSH = true;

        }
        else
        {

            if (EduUser.IsCHOfContext && EduUser.IsCHOfTech)
            {
                if (dtl.StaOfNR != 0 && dtl.StaOfJS != 0)
                    isCanSH = false;
                else
                    isCanSH = true;
            }

            else if (EduUser.IsCHOfTech)
            {
                if (dtl.StaOfJS != 0)
                    isCanSH = false;
                else

                    isCanSH = true;
            }
            else if (EduUser.IsCHOfContext)
            {
                if (dtl.StaOfNR != 0)
                    isCanSH = false;
                else
                    isCanSH = true;
            }
            //    switch (EduUser.HisWorkGrade)
            //    {
            //        case WorkGrade.ShiJi:
            //            if (EduUser.IsCHOfContext && EduUser.IsCHOfTech)
            //            {
            //                if (dtl.StaOfNR != 0 && dtl.StaOfJS != 0)
            //                    isCanSH = false;
            //                else
            //                    isCanSH = true;
            //                break;
            //            }

            //            else if (EduUser.IsCHOfTech)
            //            {
            //                if (dtl.StaOfJS != 0)
            //                    isCanSH = false;
            //                else

            //                    isCanSH = true;
            //            }
            //            else if (EduUser.IsCHOfContext)
            //            {
            //                if (dtl.StaOfNR != 0)
            //                    isCanSH = false;
            //                else
            //                    isCanSH = true;
            //            }
            //            break;
            //        case WorkGrade.QuXian:
            //            // 判断是否有区县审核的记录。

            //            SFSHDtl qxdtl = new SFSHDtl();
            //            QueryObject qxqo = new QueryObject(qxdtl);
            //            qxqo.AddWhere(SFSHDtlAttr.FK_ShareFile, sf.OID);
            //            qxqo.addAnd();
            //            qxqo.AddWhere(SFSHDtlAttr.FK_Dept, 06);
            //            int num = qxqo.DoQuery();
            //            if (num != 0)
            //                isCanSH = false;
            //            else
            //            {
            //                if (EduUser.IsCHOfContext && EduUser.IsCHOfTech)
            //                {
            //                    if (dtl.StaOfNR != 0 && dtl.StaOfJS != 0)
            //                        isCanSH = false;
            //                    else
            //                        isCanSH = true;
            //                    break;
            //                }
            //                else if (EduUser.IsCHOfTech)
            //                {
            //                    if (dtl.StaOfJS != 0)
            //                        isCanSH = false;
            //                    else
            //                        isCanSH = true;
            //                }
            //                else if (EduUser.IsCHOfContext)
            //                {
            //                    if (dtl.StaOfNR != 0)
            //                        isCanSH = false;
            //                    else
            //                        isCanSH = true;
            //                }
            //            }

            //            break;
            //        case WorkGrade.School: // 判断是否有上级审核。
            //            string sql = null;
            //            SFSHDtl xxdtl = new SFSHDtl();
            //            QueryObject xxqo = new QueryObject(xxdtl);

            //            if (EduUser.IsCHOfTech && EduUser.IsCHOfContext)
            //            {
            //                sql = "select MyPk from Edu_SFSHDtl where fk_dept='" + EduUser.FK_Dept + "' and StaOfJS=0 or StaOfNR=0 and mypk not in (select mypk from Edu_SFSHDtl where fk_sharefile="
            //                    + sf.OID
            //                    + "and fk_dept = '" + Glo.CityNo + "'or fk_dept = '"
            //                    + EduUser.FK_Dept.Substring(0, 6) + "' )";

            //                xxqo.AddWhereInSQL("MyPk", sql);

            //                int numxx = xxqo.DoQuery();
            //                if (numxx == 0)
            //                    isCanSH = false;
            //                else
            //                    isCanSH = true;
            //            }



            //            else if (EduUser.IsCHOfTech)
            //            {

            //                sql = "select MyPk from Edu_SFSHDtl where fk_dept='" + EduUser.FK_Dept + "' and StaOfJS=0 and mypk not in (select mypk from Edu_SFSHDtl where fk_sharefile=" + sf.OID
            //                    + "and fk_dept = '" + Glo.CityNo + "'or fk_dept = '"
            //                    + EduUser.FK_Dept.Substring(0, 6) + "')";
            //                xxqo.AddWhereInSQL("MyPk", sql);
            //                int numxx = xxqo.DoQuery();
            //                if (numxx == 0)
            //                    isCanSH = false;
            //                else
            //                    isCanSH = true;

            //            }

            //            else if (EduUser.IsCHOfContext)
            //            {
            //                sql = "select MyPk from Edu_SFSHDtl where fk_dept=" + EduUser.FK_Dept + " and StaOfNR=0 and mypk not in (select mypk from Edu_SFSHDtl where fk_sharefile=" + sf.OID
            //                    + "and fk_dept = '" + Glo.CityNo + "'or fk_dept = '"
            //                    + EduUser.FK_Dept.Substring(0, 6) + "')";
            //                xxqo.AddWhereInSQL("MyPk", sql);
            //                int numxx = xxqo.DoQuery();
            //                if (numxx == 0)
            //                    isCanSH = false;
            //                else
            //                    isCanSH = true;
            //            }

            //            break;
            //    }

        }

        if (isCanSH == false)
            return;



        mydtl.FK_Dept = EduUser.FK_Dept;

        this.Pub1.Add("<h2 style='margin-bottom:10px;padding-bottom:5px;font-size:12px;font-weight:bolder;color:#333;border-bottom:3px solid #0B80DF'>资源审核</h2>");
        this.Pub1.Add("<table style='width:100%;margin-top:10px' border='0'>");

        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
        this.Pub1.Add("审核状态");
        this.Pub1.AddTDEnd();
        DDL ddl = new DDL();
        ddl.BindSysEnum("StaResCH");
        ddl.ID = "DDL_CheckState";
        ddl.Width = 200;

        if (EduUser.IsCHOfTech)
            ddl.SetSelectItem((int)mydtl.StaOfJS);
        else
            ddl.SetSelectItem((int)mydtl.StaOfNR);
        this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
        this.Pub1.Add(ddl);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
        this.Pub1.Add("资源标题");
        this.Pub1.AddTDEnd();
        TextBox tb = new TextBox();
        tb.ID = "TB_Title";
        tb.Columns = 65;
        tb.Text = sf.Title;
        //mydtl.Title  =  tb.Text;

        this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
        this.Pub1.Add(tb);

        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
        this.Pub1.Add("资源描述");
        this.Pub1.AddTDEnd();
        tb = new TextBox();
        tb.ID = "TB_SubTitle";
        tb.Columns = 65;
        tb.Rows = 8;
        tb.Text = sf.HtmlDoc;
        //tb.Text = mydtl.SubTitle;


        this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
        this.Pub1.Add(tb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
        this.Pub1.Add("关键字");
        this.Pub1.AddTDEnd();
        tb = new TextBox();
        tb.Columns = 65;
        tb.ID = "TB_Key";
        //tb.Text = mydtl.KeyWord;
        this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
        this.Pub1.Add(tb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDBegin("style='font-weight:bolder;font-size:12px;width:15%;'");
        this.Pub1.Add("意见");
        this.Pub1.AddTDEnd();
        tb = new TextBox();
        tb.ID = "TB_DocSH";
        tb.TextMode = TextBoxMode.MultiLine;

        tb.Rows = 8;
        tb.Columns = 50;

        //if (EduUser.IsCHOfTech)
        //    tb.Text = mydtl.DocOfNR;
        //else
        //    tb.Text = mydtl.DocOfJS;

        this.Pub1.AddTDBegin("style='padding:5px 15px;border:2px solid #FFF;width:70%'");
        this.Pub1.Add(tb);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        Button btn = new Button();
        btn.ID = "Btn_SH";
        btn.Text = "执行审核";
        btn.Click += new EventHandler(btn_SH_Click);
        btn.CssClass = "Btn2";
        this.Pub1.AddTD("colspan=\"2\" align=\"right\" style='padding-right:140px;padding-bottom:15px;'", btn);
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();



    }

    /// <summary>
    /// 执行审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btn_SH_Click(object sender, EventArgs e)
    {

        SFSHDtl dtl = new SFSHDtl();
        dtl.MyPK = EduUser.FK_Dept + "_" + this.GeRefOID.Substring(3);
        dtl.RetrieveFromDBSources();
        dtl.FK_ShareFile = this.GeRefOID.Substring(3);
        dtl.FK_Dept = EduUser.FK_Dept;


        ShareFile sf = new ShareFile(Convert.ToInt32(this.GeRefOID.Substring(3)));
        dtl.FK_ResType = sf.FK_Type;
        dtl.FK_Work = sf.FK_Work;
        dtl.Title = this.Pub1.GetTextBoxByID("TB_Title").Text;
        dtl.SubTitle = this.Pub1.GetTextBoxByID("TB_SubTitle").Text;
        dtl.KeyWord = this.Pub1.GetTextBoxByID("TB_Key").Text;
        dtl.FK_KM = sf.FK_KM;


        if (EduUser.IsCHOfContext && EduUser.IsCHOfTech)
        {
            dtl.StaOfJS = (ResCheckState)this.Pub1.GetDDLByID("DDL_CheckState").SelectedItemIntVal;
            dtl.DocOfJS = this.Pub1.GetTextBoxByID("TB_DocSH").Text;
            dtl.RDTOfJS = DataType.CurrentDataTime;
            dtl.DocOfNR = this.Pub1.GetTextBoxByID("TB_DocSH").Text;
            dtl.StaOfNR = (ResCheckState)this.Pub1.GetDDLByID("DDL_CheckState").SelectedItemIntVal;
            dtl.RDTOfNR = DataType.CurrentDataTime;
        }
        else
        {
            if (EduUser.IsCHOfTech)
            {
                dtl.StaOfJS = (ResCheckState)this.Pub1.GetDDLByID("DDL_CheckState").SelectedItemIntVal;
                dtl.DocOfJS = this.Pub1.GetTextBoxByID("TB_DocSH").Text;
                dtl.RDTOfJS = DataType.CurrentDataTime;
            }

            if (EduUser.IsCHOfContext)
            {
                dtl.DocOfNR = this.Pub1.GetTextBoxByID("TB_DocSH").Text;
                dtl.StaOfNR = (ResCheckState)this.Pub1.GetDDLByID("DDL_CheckState").SelectedItemIntVal;
                dtl.RDTOfNR = DataType.CurrentDataTime;
            }
        }

        ZhangJie zj = new ZhangJie(sf.FK_ZJ);

        if (dtl.StaOfJS == ResCheckState.OK && dtl.StaOfNR == ResCheckState.OK)
        {
            dtl.StaResCHZT = ResCheckState.OK;//通过
            // 执行copy In.
            try
            {
                ResBase.CopyInFtp("/SF/" + sf.FK_KM + "/" + System.Convert.ToDateTime(sf.RDT).ToString("yyMM") + "/", sf.OID + "." + sf.Ext, zj, dtl.Title, dtl.SubTitle, dtl.KeyWord, sf.FK_Emp, sf.FK_BType);
            }
            catch (Exception ex)
            {
                this.Alert("此资源已不存在或文件服务器出现异常，请与管理员联系或稍后再试");
                return;
            }
        }

        else if (dtl.StaOfJS == ResCheckState.UnOK || dtl.StaOfNR == ResCheckState.UnOK)
        {
            dtl.StaResCHZT = ResCheckState.UnOK;//未通过
        }
        else if (dtl.StaOfJS == ResCheckState.Init && dtl.StaOfNR == ResCheckState.Init)
        {
            dtl.StaResCHZT = ResCheckState.Init;
        }
        else
        {
            dtl.StaResCHZT = ResCheckState.Chcking; //审核中
        }

        dtl.Save();

        //if (dtl.StaResCHZT == ResCheckState.OK)
        //{
        this.Response.Redirect("PSV_R2_Res.aspx?RefOID=" + this.GeRefOID);
        //}
        //else
        //    this.Alert("审核成功！");

        //Response.Write("<Script>alert('恭喜您，审核成功！')</Script>");

        //WinClose();

        // this.Response.Redirect("DoMsg.aspx?DoType=ResCheckOK",true);
    }

    #endregion

    //-------------------------------------------------------------------------------------------------------------//

    //#region 最近浏览资源
    //public void BoundLastestBrowse()
    //{
    //    if (EduUser.No != "" && EduUser.No != null)
    //    {
    //        //根据人员编号取最近浏览的记录
    //        this.D1.Visible = true;
    //        this.D2.Visible = true;

    //        Browses bros = new Browses();
    //        QueryObject qo = new QueryObject(bros);
    //        qo.AddWhere(BrowseAttr.FK_Emp, EduUser.No);

    //        qo.Top = 15;
    //        qo.addOrderByDesc(BrowseAttr.RDT);
    //        qo.DoQuery();
    //        List<Browse> brolist = new List<Browse>();
    //        foreach (Browse bro in bros)
    //        {
    //            if (bro.Title.Length >= 12)
    //            {
    //                bro.Title = bro.Title.Substring(0, 12) + "...";
    //            }
    //            brolist.Add(bro);
    //        }
    //        this.DataList1.DataSource = brolist;
    //        this.DataList1.DataBind();
    //    }
    //    else
    //    {
    //        //不显示最近浏览记录
    //        this.D1.Visible = false;
    //        this.D2.Visible = false;
    //    }
    //}
    //#endregion



    #region 预览功能

    public string ttitle = string.Empty;
    public string uurl = string.Empty;
    /// <summary>
    ///     预览
    /// </summary>
    protected void Preview()
    {
        FtpSupport.FtpConnection conn = Glo.FileFtpConn;
        string str_RefType = GeRefOID.Substring(0, 3);

        if (str_RefType == "RSF")
        {
            ShareFile en = new ShareFile(int.Parse(GeRefOID.Substring(3)));
            switch (en.Ext.Trim())
            {
                case "jpg":
                case "gif":
                    this.Div1.Visible = true;
                    this.Div2.Visible = false;

                    ttitle = en.Title;

                    if (conn.FileExist("/SF/" + en.FK_KM + "/" + Convert.ToDateTime(en.RDT).ToString("yyMM") + "/"
                        + en.OID + "." + en.Ext))
                        Img.ImageUrl = "http://" + Glo.FileFtpIP + "/FDB/SF/" + en.FK_KM + "/" + Convert.ToDateTime(en.RDT).ToString("yyMM") + "/"
                        + en.OID + "." + en.Ext;
                    else
                        Img.ImageUrl = "/edu/sharefile/image/nopic.jpg";

                    break;
                case "wmv":
                case "wma":
                case "mp3":
                case "avi":
                case "asf":
                case "mpg":
                case "rm":
                case "swf":
                    this.Div2.Visible = true;
                    this.Div1.Visible = false;
                    ConfigTools.PlayClass pc = new ConfigTools.PlayClass();
                    uurl = pc.Play("http://" + Glo.FileFtpIP + "/FDB/SF/" + en.FK_KM + "/" + Convert.ToDateTime(en.RDT).ToString("yyMM") + "/"
                        + en.OID + "." + en.Ext, 400, 300);
                    break;
                default:
                    this.Div1.Visible = false;
                    this.Div2.Visible = false;
                    break;
            }
        }
        else if (str_RefType == "FAQ")
        {
            string str_RefOID = GeRefOID.Substring(3);
            QDtl en = new QDtl();
            en.Retrieve(QDtlAttr.MyPK, str_RefOID);
            Question Q = new Question();
            Q.Retrieve(QuestionAttr.OID, en.FK_Question);
            switch (en.FileExt.Trim())
            {
                case "jpg":
                case "gif":
                    this.Div1.Visible = true;
                    this.Div2.Visible = false;
                    ttitle = en.Title;
                    //uurl = "192.168.0.12/R2/ResAll/zoomPicture.aspx?imageUrl=" + en.NoUrl.Substring(en.NoUrl.IndexOf('/')) + "&size=300";
                    string strfaq = "http://" + Glo.FileFtpIP + "/FDB/FAQ/" + Q.FK_KM + "/" + Convert.ToDateTime(en.RDT).ToString("yyMM") + "/" + en.MyPK + "." + en.FileExt;
                    if (conn.FileExist("/FAQ/" + Q.FK_KM + "/" + Convert.ToDateTime(en.RDT).ToString("yyMM") + "/"
                       + en.MyPK + "." + en.FileExt))
                        Img.ImageUrl = strfaq;
                    else
                        Img.ImageUrl = "/edu/sharefile/image/nopic.jpg";
                    break;
                case "wmv":
                case "wma":
                case "mp3":
                case "avi":
                case "asf":
                case "mpg":
                case "rm":
                case "swf":
                    this.Div2.Visible = true;
                    this.Div1.Visible = false;
                    ConfigTools.PlayClass pc = new ConfigTools.PlayClass();
                    uurl = pc.Play("http://" + Glo.FileFtpIP + "/FDB/FAQ/" + Q.FK_KM + "/" + Convert.ToDateTime(en.RDT).ToString("yyMM") + "/" + en.MyPK + "." + en.FileExt, 400, 300);
                    break;
                default:
                    this.Div1.Visible = false;
                    this.Div2.Visible = false;
                    break;
            }
        }

    }






    #endregion
}
