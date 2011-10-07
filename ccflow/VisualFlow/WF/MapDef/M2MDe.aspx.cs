using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class WF_MapDef_M2MDe : WebPage
{
    public string FK_MapData
    {
        get
        {
            return this.Request.QueryString["FK_MapData"];
        }
    }
    public string FK_MapM2M
    {
        get
        {
            return this.Request.QueryString["FK_MapM2M"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        MapM2M M2M = new MapM2M(this.FK_MapM2M);

        DataTable dtGroup = new DataTable();

        if (M2M.DBOfGroups.Length > 3)
            dtGroup = BP.DA.DBAccess.RunSQLReturnTable(M2M.DBOfGroupsRun);

        DataTable dtObj = BP.DA.DBAccess.RunSQLReturnTable(M2M.DBOfObjsRun);

        this.Pub1.AddTable("border=0");
        foreach (DataRow drGroup in dtGroup.Rows)
        {
            string groupNo = drGroup[0].ToString();
            string ctlIDs = "";
            this.Pub1.AddTR();
            CheckBox cbx = new CheckBox();
            cbx.ID = "CBs_" + drGroup[0].ToString();
            cbx.Text = drGroup[1].ToString();
            this.Pub1.AddTDTitle("align=left", cbx);
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.AddTDBigDocBegain(); // ("nowarp=true");
            foreach (DataRow drObj in dtObj.Rows)
            {
                string no = drObj[0].ToString();
                string name = drObj[1].ToString();
                string group = drObj[2].ToString();
                if (group != groupNo)
                    continue;

                CheckBox cb = new CheckBox();
                cb.ID = "CB_" + no;
                ctlIDs += cb.ID + ",";
                cb.Text = name;
                this.Pub1.Add(cb);
            }
            cbx.Attributes["onclick"] = "SetSelected(this,'" + ctlIDs + "')";
            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }

        #region 处理未分组的情况.
        bool isHaveUnGroup = false;
        if (dtObj.Columns.Count > 2)
        {
            foreach (DataRow drObj in dtObj.Rows)
            {
                string group = drObj[2].ToString();
                isHaveUnGroup = true;
                foreach (DataRow drGroup in dtGroup.Rows)
                {
                    string groupNo = drGroup[0].ToString();
                    if (group == groupNo)
                    {
                        isHaveUnGroup = false;
                        break;
                    }
                }
                if (isHaveUnGroup == false)
                    continue;
            }
        }

        if (isHaveUnGroup == true)
        {
            this.Pub1.AddTR();
            //CheckBox cbx = new CheckBox();
            //cbx.ID = "CBs_UnGroup";
            //cbx.Text = "未分组";
            //this.Pub1.AddTDTitle("align=left", cbx);
            //this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.AddTDBigDocBegain(); // ("nowarp=true");

            string ctlIDs = "";
            foreach (DataRow drObj in dtObj.Rows)
            {
                string group = drObj[2].ToString();
                isHaveUnGroup = true;
                foreach (DataRow drGroup in dtGroup.Rows)
                {
                    string groupNo = drGroup[0].ToString();
                    if (group != groupNo)
                    {
                        isHaveUnGroup = true;
                        break;
                    }
                }
                if (isHaveUnGroup == false)
                    continue;


                string no = drObj[0].ToString();
                string name = drObj[1].ToString();

                CheckBox cb = new CheckBox();
                cb.ID = "CB_" + no;
                ctlIDs += cb.ID + ",";
                cb.Text = name;
                this.Pub1.Add(cb);
            }
          //  cbx.Attributes["onclick"] = "SetSelected(this,'" + ctlIDs + "')";
            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }
        #endregion 处理未分组的情况.

        this.Pub1.AddTableEnd();
    }
}