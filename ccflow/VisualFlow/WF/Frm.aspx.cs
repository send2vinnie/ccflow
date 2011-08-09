using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Sys;
using BP.Web;

public partial class WF_Frm : WebPage
{
    #region 属性
    public int FK_Node
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
            }
            catch
            {
                return 101;
            }
        }
    }
    public int WorkID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public int FID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public string FK_MapData
    {
        get
        {
            string s = this.Request.QueryString["FK_MapData"];
            if (s == null)
                return "ND101";
            return s;
        }
    }
    public bool IsReadonly
    {
        get
        {
            if (this.Request.QueryString["IsReadonly"] == "1")
                return true;
            return false;
        }
    }
    public bool IsPrint
    {
        get
        {
            if (this.Request.QueryString["IsPrint"] == "1")
                return true;
            return false;
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
         

        if (this.Request.QueryString["IsTest"] == "1")
        {
            BP.SystemConfig.DoClearCash();
        }

        MapData md = new MapData();
        md.No = this.FK_MapData;
        if (md.RetrieveFromDBSources() == 0 && md.Name.Length > 3)
        {
            MapDtl dtl = new MapDtl(this.FK_MapData);
            GEDtl dtlEn = dtl.HisGEDtl;
            dtlEn.SetValByKey("OID", this.FID);
            int i = dtlEn.RetrieveFromDBSources();
            if (i == 0)
            {
            }
            this.UCEn1.BindFreeFrm(dtlEn, this.FK_MapData, this.IsReadonly);
        }
        else
        {
            GEEntity en = md.HisGEEn;
            en.SetValByKey("OID", this.FID);
            int i = en.RetrieveFromDBSources();
            if (i == 0 && this.FID != 0)
                en.DirectInsert();

            this.UCEn1.BindFreeFrm(en, this.FK_MapData, this.IsReadonly);
        }

        this.Btn_Save.Visible = !this.IsReadonly;
        this.Btn_Save.Enabled = !this.IsReadonly;
        this.Btn_Print.Visible = true;
        this.Btn_Print.Attributes["onclick"] = "window.showModalDialog('./FreeFrm/Print.aspx?FK_Node=" + this.FK_Node + "&FID=" + this.FID + "&FK_MapData=" + this.FK_MapData + "&WorkID="+this.WorkID+"', '', 'dialogHeight: 350px; dialogWidth:450px; center: yes; help: no'); return false;";
        //this.Btn_Print.Attributes["onclick"] = "window.showModalDialog('./FreeFrm/Print.aspx?FK_Node=" + this.FK_Node + "&FID=" + this.FID + "', '', 'dialogHeight: 550px; dialogWidth:950px; dialogTop: 100px; dialogLeft: 100px; center: no; help: no'); return false;";
        //this.IsPrint;
        //   this.Button1.Enabled = this.IsReadonly;
    }
    public void SaveNode()
    {
        Node nd = new Node(this.FK_Node);
        Work wk = nd.HisWork;
        wk.OID = this.FID;
        wk.RetrieveFromDBSources();
        wk = this.UCEn1.Copy(wk) as Work;
        try
        {
            wk.BeforeSave(); //调用业务逻辑检查。
        }
        catch (Exception ex)
        {
            if (BP.SystemConfig.IsDebug)
                wk.CheckPhysicsTable();

            throw new Exception("@在保存前执行逻辑检查错误。@技术信息:" + ex.Message);
        }

        wk.NodeState = NodeState.Init;
        wk.Rec = WebUser.No;
        wk.SetValByKey("FK_Dept", WebUser.FK_Dept);
        wk.SetValByKey("FK_NY", BP.DA.DataType.CurrentYearMonth);
        try
        {
            wk.Update();
        }
        catch (Exception ex)
        {
            try
            {
                wk.CheckPhysicsTable();
            }
            catch (Exception ex1)
            {
                throw new Exception("@保存错误:" + ex.Message + "@检查物理表错误：" + ex1.Message);
            }

            this.UCEn1.AlertMsg_Warning("错误", ex.Message + "@有可能此错误被系统自动修复,请您从新保存一次.");
            return;
        }
        wk.RetrieveFromDBSources();
        this.UCEn1.ResetEnVal(wk);
        return;
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        if (this.FK_MapData.Replace("ND", "") == this.FK_Node.ToString())
        {
            this.SaveNode();
            return;
        }

        MapData md = new MapData(this.FK_MapData);
        GEEntity en = md.HisGEEn;
        en.SetValByKey("OID", this.FID);
        int i = en.RetrieveFromDBSources();
        en = this.UCEn1.Copy(en) as GEEntity;
        if (i == 0)
            en.Insert();
        else
            en.Update();
    }
    
}