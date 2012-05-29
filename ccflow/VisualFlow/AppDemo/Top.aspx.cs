﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;

public partial class AppDemo_Top : System.Web.UI.Page
{
    public int tempcount;
    public string webloginurl
    {
        get
        {
            try
            {
                return BP.SystemConfig.AppSettings["WebLogin"].ToString();
            }
            catch
            {
                return "Login.aspx?DoType=Logout";
            }
        }
    }
    public int sname
    {
        get
        {
            if (Session["sname"] != null)
            {
                return int.Parse(Session["sname"].ToString());
            }
            return 1;

        }
        set
        {
            Session["sname"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.imgBtn.Attributes.Add("onclick", "return relogin();");
        }
        getnews();
    }
    void getnews()
    {
        DataTable dt = new DataTable();
        dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable();
        foreach (DataRow dr in dt.Rows)
        {
            //if (dr["SFCK"].ToString().Trim() == "0")
            //{
            //    DateTime myjssj = DataType.ParseSysDate2DateTime(dr["ADT"].ToString());
            //    if (myjssj.AddMinutes(1) > System.DateTime.Now)
            //    {
            //        tempcount++;
            //    }
            //}
        }
        Session["abc"] = tempcount;
    }
    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        this.Session.Clear();
    }
}