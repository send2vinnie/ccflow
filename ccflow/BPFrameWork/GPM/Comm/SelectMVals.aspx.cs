﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;
using BP.Sys;
using BP.DA;
using BP.En;

public partial class Comm_SelectMVals : WebPage
{
    public string MyPK
    {
        get
        {
            return WebUser.No + this.EnsName + "_SearchAttrs";
            //return this.Request.QueryString["MyPK"];
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
        this.Title = "选择范围";


        UserRegedit ur = new UserRegedit();
        ur.MyPK = this.MyPK;
        if (ur.RetrieveFromDBSources() == 0)
        {
            ur.MyPK = this.MyPK;
            ur.FK_Emp = WebUser.No;
            ur.CfgKey = this.EnsName + "_SearchAttrs";
            ur.Insert();
        }

        Entity en = BP.DA.ClassFactory.GetEns(this.EnsName).GetNewEntity;
        Attr attr = en.EnMap.GetAttrByKey(this.AttrKey);
        string cfgVal = ur.MVals;
        if (string.IsNullOrEmpty(cfgVal))
            cfgVal = "";

        AtPara ap = new AtPara(cfgVal);
        cfgVal = ap.GetValStrByKey(this.AttrKey);
        if (string.IsNullOrEmpty(cfgVal))
            cfgVal = "";

        if (attr.IsEnum)
            this.BindEnum(ur, attr, cfgVal);
        else
            this.BindEns(ur, attr, cfgVal);
    }
    public void BindEnum(UserRegedit ur, Attr attr, string cfgVal)
    {
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("<input type=checkbox text='选择全部' name=checkedAll onclick='SelectAll()' >选择全部");
        this.Pub1.AddTREnd();

        SysEnums ses = new SysEnums(attr.UIBindKey);
        int idx = 0;
        bool is1 = false;
        foreach (SysEnum item in ses)
        {
            idx++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(idx);

            CheckBox cb = new CheckBox();
            cb.Text = item.Lab;
            cb.ID = "CB_" + item.IntKey;
            cb.Checked = cfgVal.Contains("." + item.IntKey  + ".");

            this.Pub1.AddTD(cb);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " OK ";
        btn.CssClass = "Btn";

        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
    }
    public void BindEns(UserRegedit ur, Attr attr, string cfgVal)
    {
        Entities ens = BP.DA.ClassFactory.GetEns(attr.UIBindKey);
        ens.RetrieveAll();

        this.Pub1.AddTable("width='90%'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("<input type=checkbox   text='选择全部' name=checkedAll onclick='SelectAll()' >选择全部");
        this.Pub1.AddTREnd();

        int idx = 0;
        bool is1 = false;
        foreach (Entity item in ens)
        {
            idx++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(idx);
            CheckBox cb = new CheckBox();
            cb.Text = item.GetValStrByKey(attr.UIRefKeyText);
            cb.ID = "CB_" + item.GetValByKey(attr.UIRefKeyValue);
            cb.Checked = cfgVal.Contains("." + item.GetValStrByKey(attr.UIRefKeyValue) + ".");

            this.Pub1.AddTD(cb);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithHR();
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " O K ";
        btn.CssClass = "Btn";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add("&nbsp;&nbsp;&nbsp;");
        this.Pub1.Add(btn);
    }
    void btn_Click(object sender, EventArgs e)
    {
        UserRegedit ur = new UserRegedit();
        ur.MyPK = this.MyPK;
        ur.RetrieveFromDBSources();
        ur.FK_Emp = WebUser.No;
        ur.CfgKey = this.EnsName + "_SearchAttrs";

        Entity en = BP.DA.ClassFactory.GetEns(this.EnsName).GetNewEntity;
        Attr attr = en.EnMap.GetAttrByKey(this.AttrKey);

        string cfgVal = ur.MVals;
        AtPara ap = new AtPara(cfgVal);
        string old_Val = ap.GetValStrByKey(this.AttrKey);

        string keys = "@" + this.AttrKey + "=";
        if (attr.IsEnum)
        {
            SysEnums ses = new SysEnums(attr.UIBindKey);
            foreach (SysEnum item in ses)
            {
                if (this.Pub1.GetCBByID("CB_" + item.IntKey ).Checked == false)
                    continue;
                keys += "." + item.IntKey  + ".";
            }

            if (ur.MVals.Contains("@" + this.AttrKey))
                ur.MVals = ur.MVals.Replace("@" + this.AttrKey + "=" + old_Val, keys);
            else
                ur.MVals = ur.MVals + keys;

            ur.DirectUpdate();
        }
        else
        {
            Entities ens = BP.DA.ClassFactory.GetEns(attr.UIBindKey);
            ens.RetrieveAll();
            foreach (Entity item in ens)
            {
                if (this.Pub1.GetCBByID("CB_" + item.GetValStrByKey(attr.UIRefKeyValue)).Checked == false)
                    continue;
                keys += "." + item.GetValStrByKey(attr.UIRefKeyValue) + ".";
            }

            if (ur.MVals.Contains("@" + this.AttrKey))
                ur.MVals = ur.MVals.Replace("@" + this.AttrKey + "=" + old_Val, keys);
            else
                ur.MVals = ur.MVals + keys;

            ur.DirectUpdate();
        }
        keys = keys.Replace("@" + this.AttrKey + "=", "");
        this.WinClose(keys);
    }
}