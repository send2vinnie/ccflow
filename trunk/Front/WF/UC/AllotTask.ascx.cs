﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_AllotTask : BP.Web.UC.UCBase3
{
    /// <summary>
    /// 工作ID
    /// </summary>
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    /// <summary>
    /// NodeID
    /// </summary>
    public int NodeID
    {
        get
        {
            return int.Parse(this.Request.QueryString["NodeID"]);
        }
    }
    /// <summary>
    /// 流程编号
    /// </summary>
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    /// <summary>
    /// dd
    /// </summary>
    public void BindLB()
    {
        // 当前用的员工权限。

        this.Clear();

        WorkerLists wls = new WorkerLists(this.WorkID, this.NodeID, true);
        string sql = "SELECT * FROM WF_RememberMe WHERE FK_Emp='" + WebUser.No + "' AND FK_Node=" + this.NodeID;
        DataTable dt = DBAccess.RunSQLReturnTable(sql);

        this.AddFieldSet("工作指定");

        if (dt.Rows.Count == 0)
            throw new Exception("@系统错误....."+sql);

        string[] objs = dt.Rows[0]["Objs"].ToString().Split('@');
        string[] emps = dt.Rows[0]["Emps"].ToString().Split('@');

        foreach (string fk_emp in emps)
        {
            if (fk_emp == null || fk_emp == "")
                continue;

            Emp emp = new Emp(fk_emp);
            CheckBox cb = new CheckBox();
            cb.ID = "CB_" + fk_emp;

            if (Glo.IsShowUserNoOnly)
                cb.Text = emp.No;
            else
                cb.Text = emp.No + "  , " + emp.Name;

            WorkerList wl = wls.GetEntityByKey(WorkerListAttr.FK_Emp, fk_emp) as WorkerList;
            if (wl == null)
                cb.Checked = false;
            else
            {
                cb.Checked = wl.IsEnable;
            }

            this.Add(cb);
            this.AddBR();
        }
        this.AddHR();
        Btn btn = new Btn();
        btn.ID = "Btn_Do";
        btn.Text = "执行";
        btn.Click += new EventHandler(BPToolBar1_ButtonClick);
        this.Add(btn);
        this.Add("<input type=button value='取消' onclick='window.close();' class=Btn />");

        this.Add("<br><br>帮助:系统会记住本次的工作指定，下次您在发送时间它会自动把工作投递给您本次指定的人。");
        this.AddFieldSetEnd();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "指定同事处理";
        if (this.IsPostBack == false)
        {
            string fk_emp = this.Request.QueryString["FK_Emp"];
            if (fk_emp != null)
            {
                Emp emp = new Emp(fk_emp);
                BP.Web.WebUser.SignInOfGenerLang(emp, null);
            }
        }
        this.BindLB();
        this.GetBtnByID("Btn_Do").Click += new EventHandler(BPToolBar1_ButtonClick);

    }
    private void BPToolBar1_ButtonClick(object sender, System.EventArgs e)
    {
        this.Confirm();
        return;
    }
    public void Confirm()
    {
        WorkerLists wls = new WorkerLists(this.WorkID, this.NodeID,true);
        string sql = "SELECT * FROM WF_RememberMe WHERE FK_Emp='"+WebUser.No+"' AND FK_Node="+this.NodeID;
        DataTable dt = DBAccess.RunSQLReturnTable(sql);

        if (dt.Rows.Count == 0)
            throw new Exception("@系统错误.....");
        try
        {
            string[] objs = dt.Rows[0]["Objs"].ToString().Split('@');
            string[] empStrs = dt.Rows[0]["Emps"].ToString().Split('@');

            ArrayList al = new ArrayList();
            WorkerLists wlSeles = new WorkerLists();

            foreach (string fk_emp in empStrs)
            {
                if (fk_emp == null || fk_emp == "")
                    continue;

                CheckBox cb = this.GetCBByID("CB_" + fk_emp);
                if (cb == null)
                {
                    BP.DA.Log.DebugWriteWarning("不应该查询不到:" + fk_emp);
                    continue;
                }

                if (cb.Checked == false)
                    continue;

                WorkerList wl = wls.GetEntityByKey(WorkerListAttr.FK_Emp, fk_emp) as WorkerList;

                al.Add(cb.ID.Substring(3));
                wlSeles.AddEntity(wl);
            }


            if (al.Count == 0)
            {
                this.Alert("当前工作中你没有分配给任何人，此工作将不能被其他人所执行！");
                return;
            }

            // 删除当前非配的工作。
            // 已经非配或者自动分配的任务。
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID, this.FK_Flow);
            int NodeID = gwf.FK_Node;
            int workId = this.WorkID;
            //WorkerLists wls = new WorkerLists(this.WorkID,NodeID);
            DBAccess.RunSQL("update  WF_GenerWorkerList set IsEnable=0  WHERE WorkID=" + this.WorkID + " AND FK_Node=" + NodeID);
            string vals = "";
            string emps = "";
            string myemp = "";
            foreach (Object obj in al)
            {
                emps += obj.ToString() + ",";
                myemp = obj.ToString();
                DBAccess.RunSQL("update  WF_GenerWorkerList set IsEnable=1  WHERE WorkID=" + this.WorkID + " AND FK_Node=" + NodeID + " and fk_emp='" + obj + "'");
            }


            BP.WF.Node nd = new BP.WF.Node(NodeID);
            Work wk = nd.HisWork;
            if (wk.IsGECheckStand)
            {
                wk.SetValByKey(GECheckStandAttr.NodeID, NodeID);
                wk.OID = this.WorkID;
                wk.Retrieve();
            }
            else
            {
                wk.OID = this.WorkID;
                wk.Retrieve();
            }
            wk.Emps = emps;
            wk.Update();
            DBAccess.RunSQL("UPDATE WF_CHOfNode SET Emps='" + emps + "' WHERE FK_NODE='" + NodeID + "' AND WorkID='" + this.WorkID + "'");


            // 保存记忆功能。
            RememberMe rm = new RememberMe();
            rm.FK_Emp = BP.Web.WebUser.No;
            rm.FK_Node = NodeID;

            rm.Objs = "@";
            rm.ObjsExt = "";

            foreach (WorkerList mywl in wlSeles)
            {
                rm.Objs += mywl.FK_Emp + "@";
                rm.ObjsExt += mywl.FK_EmpText + "、";
            }

            rm.Emps = "@";
            rm.EmpsExt = "";

            foreach (WorkerList wl in wls)
            {
                rm.Emps += wl.FK_Emp + "@";

                if (rm.Objs.IndexOf(wl.FK_Emp) != -1)
                    rm.EmpsExt += wl.FK_EmpText + "、";
                else
                    rm.EmpsExt += "<strike>" + wl.FK_EmpText + "</strike>、";
            }
            rm.FK_Emp = BP.Web.WebUser.No;
            rm.Update();
            this.WinCloseWithMsg("任务分配成功。");
        }
        catch (Exception ex)
        {
            this.Response.Write(ex.Message);
            Log.DebugWriteWarning(ex.Message);
            this.Alert("任务分配出错：" + ex.Message);
        }
    }
}
