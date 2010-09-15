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
using BP.Port;
using BP.DA;
using BP.En;
using BP.DA;

public partial class Comm_RefFunc_Left : BP.Web.UC.UCBase3
{
    public new string PK
    {
        get
        {
            if (ViewState["PK"] == null)
            {
                string pk = this.Request.QueryString["PK"];
                if (pk == null)
                    pk = this.Request.QueryString["No"];

                if (pk == null)
                    pk = this.Request.QueryString["RefNo"];

                if (pk == null)
                    pk = this.Request.QueryString["OID"];

                if (pk == null)
                    pk = this.Request.QueryString["MyPK"];


                if (pk != null)
                {
                    ViewState["PK"] = pk;
                }
                else
                {
                    Entity mainEn = BP.DA.ClassFactory.GetEn(this.EnName);
                    ViewState["PK"] = this.Request.QueryString[mainEn.PK];
                }
            }
            return ViewState["PK"] as string;
        }
    }
    public string AttrKey
    {
        get
        {
            return this.Request.QueryString["AttrKey"];
        }
    }
    public new string EnName
    {
        get
        {
            string enName = this.Request.QueryString["EnName"];
            string ensName = this.Request.QueryString["EnsName"];
            if (enName == null && ensName == null)
                throw new Exception("@缺少参数");

            if (enName == null)
                enName = this.ViewState["EnName"] as string;

            if (enName == null)
            {
                Entities ens = ClassFactory.GetEns(this.EnsName);
                this.ViewState["EnName"] = ens.GetNewEntity.ToString();
                enName = this.ViewState["EnName"].ToString();
            }
            return enName;
        }
    }
    public new string EnsName
    {
        get
        {
            string enName = this.Request.QueryString["EnName"];
            string ensName = this.Request.QueryString["EnsName"];
            if (enName == null && ensName == null)
                throw new Exception("@缺少参数");


            if (ensName == null)
                ensName = this.ViewState["EnsName"] as string;
            if (ensName == null)
            {
                Entity en = ClassFactory.GetEn(this.EnName);
                this.ViewState["EnsName"] = en.GetNewEntities.ToString();
                ensName = this.ViewState["EnsName"].ToString();
            }
            return ensName;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Entity en = ClassFactory.GetEn(this.EnName);
        if (this.PK == null)
            return;

        en.PKVal = this.PK;

        string keys = "&" + en.PK + "=" + this.PK + "&r=" + DateTime.Now.ToString("MMddhhmmss");

        this.AddFieldSet("<a href='UIEn.aspx?EnName=" + this.EnName + "&PK=" + this.PK + "' >" + en.EnDesc + "-主页</a>");

        this.AddUL();


        #region 加入一对多的实体编辑
        AttrsOfOneVSM oneVsM = en.EnMap.AttrsOfOneVSM;
        string sql = "";
        int i = 0;

        if (oneVsM.Count > 0)
        {
            foreach (AttrOfOneVSM vsM in oneVsM)
            {
                string url = "Dot2Dot.aspx?EnsName=" + en.GetNewEntities.ToString() + "&EnName="+this.EnName+"&AttrKey=" + vsM.EnsOfMM.ToString() + keys;

                try
                {
                    sql = "SELECT COUNT(*) FROM " + vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable + " WHERE " + vsM.AttrOfOneInMM + "='" + en.PKVal + "'";
                    i = DBAccess.RunSQLReturnValInt(sql);
                }
                catch
                {
                    sql = "SELECT COUNT(*) FROM " + vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable + " WHERE " + vsM.AttrOfOneInMM + "=" + en.PKVal;
                    i = DBAccess.RunSQLReturnValInt(sql);
                }


                if (i == 0)
                {
                    if (this.AttrKey == vsM.EnsOfMM.ToString())
                        this.AddLi("<b><a href='" + url + "'  >" + vsM.Desc + "</a></b>");
                    else
                        this.AddLi("<a href='" + url + "'  >" + vsM.Desc + "</a>");
                }
                else
                {
                    if (this.AttrKey == vsM.EnsOfMM.ToString())
                    this.AddLi("<b><a href='" + url + "'  >" + vsM.Desc + "-" + i + "</a></b>");
                    else
                        this.AddLi("<a href='" + url + "'  >" + vsM.Desc + "-" + i + "</a>");

                }
            }
        }
        #endregion

        #region 加入方法

        
        #endregion

        #region 加入他的明细
        EnDtls enDtls = en.EnMap.Dtls;
        if (enDtls.Count > 0)
        {
            foreach (EnDtl enDtl in enDtls)
            {
                string url = "UIEnDtl.aspx?EnsName=" + enDtl.EnsName + "&RefKey=" + enDtl.RefKey + "&RefVal=" + en.PKVal.ToString() + "&MainEnsName=" + en.ToString() + keys;
                try
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM " + enDtl.Ens.GetNewEntity.EnMap.PhysicsTable + " WHERE " + enDtl.RefKey + "='" + en.PKVal + "'");
                }
                catch
                {
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM " + enDtl.Ens.GetNewEntity.EnMap.PhysicsTable + " WHERE " + enDtl.RefKey + "=" + en.PKVal);
                }


                if (i == 0)
                    this.AddLi("<a href='" + url + "'  >" + enDtl.Desc + "</a>");
                else
                    this.AddLi("<a href='" + url + "'  >" + enDtl.Desc + "-" + i + "</a>");
            }
        }
        #endregion

        this.AddULEnd();

        this.AddFieldSetEnd();




        // this.GenerCaption(this.MainEnName + " 相关功能:" + refstrs);
    }

}
