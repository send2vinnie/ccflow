﻿using System;
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
    public int OID
    {
        get
        {
            string oid = this.Request.QueryString["WorkID"];
            if (oid == null || oid == "")
                oid = this.Request.QueryString["OID"];
            if (oid == null || oid == "")
                oid = "0";
            return int.Parse(oid);
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
    public bool IsEdit
    {
        get
        {
            if (this.Request.QueryString["IsEdit"] == "0")
                return false;
            return true;
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
            BP.SystemConfig.DoClearCash();

        if (this.Request.QueryString["IsLoadData"] == "1")
            this.UCEn1.IsLoadData = true;

        MapData md = new MapData();
        md.No = this.FK_MapData;
        if (md.RetrieveFromDBSources() == 0 && md.Name.Length > 3)
        {
            MapDtl dtl = new MapDtl(this.FK_MapData);
            GEDtl dtlEn = dtl.HisGEDtl;
            dtlEn.SetValByKey("OID", this.FID);

            if (dtlEn.EnMap.Attrs.Count < 2)
            {
                md.RepairMap();
                this.Response.Redirect(this.Request.RawUrl, true);
                return;
            }
            int i = dtlEn.RetrieveFromDBSources();

            string[] paras = this.RequestParas.Split('&');
            foreach (string str in paras)
            {
                if (string.IsNullOrEmpty(str) || str.Contains("=") == false)
                    continue;

                string[] kvs = str.Split('=');
                dtlEn.SetValByKey(kvs[0], kvs[1]);
            }

            this.UCEn1.BindFreeFrm(dtlEn, this.FK_MapData, !this.IsEdit);
            this.AddJSEvent(dtlEn);
        }
        else
        {
            GEEntity en = md.HisGEEn;
            if (this.FID != 0)
                en.SetValByKey("OID", this.FID);

            if (this.OID != 0)
                en.SetValByKey("OID", this.OID);

            if (en.EnMap.Attrs.Count < 2)
            {
                md.RepairMap();
                this.Response.Redirect(this.Request.RawUrl, true);
                //   this.UCEn1.AddMsgOfWarning("提示", "<h2>主表单没有字段无法预览。FK_MapData=" + this.FK_MapData + "</h2>");
                return;
            }

            int i = en.RetrieveFromDBSources();
            if (i == 0)
                en.DirectInsert();

            string[] paras = this.RequestParas.Split('&');
            foreach (string str in paras)
            {
                if (string.IsNullOrEmpty(str) || str.Contains("=") == false)
                    continue;

                string[] kvs = str.Split('=');
                en.SetValByKey(kvs[0], kvs[1]);
            }
            en.ResetDefaultVal();

            if (en.ToString() == "0")
            {
                en.SetValByKey("OID", this.OID);
            }

            this.UCEn1.BindFreeFrm(en, this.FK_MapData, !this.IsEdit);
            this.AddJSEvent(en);
        }

        Session["Count"] = null;
        this.Btn_Save.Click += new EventHandler(Btn_Save_Click);
        this.Btn_Save.Visible = this.IsEdit;
        this.Btn_Save.Enabled = this.IsEdit;
        this.Btn_Print.Visible = this.IsPrint;
        this.Btn_Print.Enabled = this.IsPrint;
        this.Btn_Print.Attributes["onclick"] = "window.showModalDialog('./FreeFrm/Print.aspx?FK_Node=" + this.FK_Node + "&FID=" + this.FID + "&FK_MapData=" + this.FK_MapData + "&WorkID=" + this.OID + "', '', 'dialogHeight: 350px; dialogWidth:450px; center: yes; help: no'); return false;";
    }
    public void AddJSEvent(Entity en)
    {
        Attrs attrs = en.EnMap.Attrs;
        foreach (Attr attr in attrs)
        {
            if (attr.UIIsReadonly || attr.UIVisible == false)
                continue;
            if (attr.IsFKorEnum)
            {
                var ddl = this.UCEn1.GetDDLByID("DDL_" + attr.Key);
            }
        }
    }
    /// <summary>
    /// 保存点
    /// </summary>
    public void SaveNode()
    {
        Node nd = new Node(this.FK_Node);
        Work wk = nd.HisWork;
        wk.OID = this.FID;
        if (wk.OID == 0)
            wk.OID = this.OID;
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
        FrmEvents fes = new FrmEvents(this.FK_MapData);
        fes.DoEventNode(FrmEventList.SaveBefore, wk);
        try
        {
            wk.Update();
            fes.DoEventNode(FrmEventList.SaveAfter, wk);
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
        this.Response.Redirect("Frm.aspx?OID=" + wk.GetValStringByKey("OID") + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.OID + "&FID=" + this.FID + "&FK_MapData=" + this.FK_MapData, true);
        return;
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.FK_MapData.Replace("ND", "") == this.FK_Node.ToString())
            {
                this.SaveNode();
                return;
            }

            MapData md = new MapData(this.FK_MapData);
            GEEntity en = md.HisGEEn;
            en.SetValByKey("OID", this.OID);
            int i = en.RetrieveFromDBSources();
            en = this.UCEn1.Copy(en) as GEEntity;
            FrmEvents fes = new FrmEvents(this.FK_MapData);
            fes.DoEventNode(FrmEventList.SaveBefore, en);
            if (i == 0)
                en.Insert();
            else
                en.Update();
            fes.DoEventNode(FrmEventList.SaveAfter, en);
            this.Response.Redirect("Frm.aspx?OID=" + en.GetValStringByKey("OID") + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.OID + "&FID=" + this.FID + "&FK_MapData=" + this.FK_MapData, true);
        }
        catch (Exception ex)
        {
            this.UCEn1.AddMsgOfWarning("error:", ex.Message);
        }
    }
}