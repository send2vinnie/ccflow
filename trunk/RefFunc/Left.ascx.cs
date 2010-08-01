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



                if (this.Request.QueryString["PK"] != null)
                {
                    ViewState["PK"] = pk;
                }
                else
                {
                    Entity mainEn = BP.DA.ClassFactory.GetEn(this.EnName);
                    ViewState["PK"] = this.Request.QueryString[mainEn.PK];
                }
            }
            return (string)ViewState["PK"];
        }
    }
    public string AttrKey
    {
        get
        {
            return this.Request.QueryString["AttrKey"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Entity en = ClassFactory.GetEn(this.EnName);
        en.PKVal = this.PK;

        string keys = "&" + en.PK + "=" + this.PK + "&r=" + DateTime.Now.ToString("MMddhhmmss");

        this.AddFieldSet("<a href='UIEn.aspx?EnName=" + this.EnName + "&EnsName=" + this.EnsName + "&PK=" + this.PK + "' >" + en.EnDesc + "-主页</a>");

        this.AddUL();

        #region 加入一对多的实体编辑
        AttrsOfOneVSM oneVsM = en.EnMap.AttrsOfOneVSM;
        if (oneVsM.Count > 0)
        {
            foreach (AttrOfOneVSM vsM in oneVsM)
            {
                string url = "Dot2Dot.aspx?EnName=" + en.ToString() + "&AttrKey=" + vsM.EnsOfMM.ToString() + keys;
                string sql = "SELECT COUNT(*) FROM " + vsM.EnsOfMM.GetNewEntity.EnMap.PhysicsTable + " WHERE " + vsM.AttrOfOneInMM + "='" + en.PKVal + "'";
                int i = DBAccess.RunSQLReturnValInt(sql);
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

                int i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM " + enDtl.Ens.GetNewEntity.EnMap.PhysicsTable + " WHERE " + enDtl.RefKey + "='" + en.PKVal + "'");
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
