using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Web;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.Web.Controls;

public partial class Comm_M2M : WebPage
{
    public int WorkID
    {
        get
        {
            return int.Parse( this.Request.QueryString["WorkID"] );
        }
    }
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
        MapM2M mapM2M = new MapM2M(this.FK_MapM2M);
        BP.WF.M2M m2m = new BP.WF.M2M();
        m2m.MyPK = mapM2M.FK_Node+"_"+this.WorkID+"_"+this.FK_MapM2M;
        m2m.RetrieveFromDBSources();

        
        DataTable dtGroup = BP.DA.DBAccess.RunSQLReturnTable(mapM2M.DBOfGroups);
        DataTable dtObj = BP.DA.DBAccess.RunSQLReturnTable(mapM2M.DBOfObjs);

        bool isInsert = mapM2M.IsInsert;
        bool isDelete = mapM2M.IsDelete;

        if (isDelete == false && isInsert == false)
            this.Button1.Enabled = false;

        this.Pub1.AddTable();
        foreach (DataRow drGroup in dtGroup.Rows)
        {
            string ctlIDs = "";
            string groupNo = drGroup[0].ToString();
            this.Pub1.AddTR();
          
            CheckBox cbx = new CheckBox();
            cbx.ID = "CBs_" + drGroup[0].ToString();
            cbx.Text = drGroup[1].ToString();
            this.Pub1.AddTDTitle("align=left", cbx);
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.AddTDBegin("nowarp=false");
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
                cb.Attributes["onclick"] = "isChange=true;";
                cb.Text = name;
                cb.Checked = m2m.Vals.Contains("," + no + ",");

                //if (cb.Checked)
                //{
                //    cb.Enabled = isDelete;
                //}
                //else
                //{
                //    cb.Enabled = isInsert;
                //}
                this.Pub1.Add(cb);
            }
            cbx.Attributes["onclick"] = "SetSelected(this,'" + ctlIDs + "')";

            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }

        #region 处理未分组的情况.
        bool isHaveUnGroup = true;
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

        if (isHaveUnGroup == true)
        {
            this.Pub1.AddTR();
            CheckBox cbx = new CheckBox();
            cbx.ID = "CBs_UnGroup";
            cbx.Text = "未分组";
            this.Pub1.AddTDTitle("align=left", cbx);
            this.Pub1.AddTREnd();

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
                cb.Checked = m2m.Vals.Contains("," + no + ",");
                this.Pub1.Add(cb);
            }
            cbx.Attributes["onclick"] = "SetSelected(this,'" + ctlIDs + "')";
            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }
        #endregion 处理未分组的情况.


        this.Pub1.AddTableEnd();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MapM2M mapM2M = new MapM2M(this.FK_MapM2M);

        BP.WF.M2M m2m = new BP.WF.M2M();
        m2m.MyPK = mapM2M.FK_Node + "_" + this.WorkID+"_"+this.FK_MapM2M;
        m2m.FK_Node = mapM2M.FK_Node;
        m2m.WorkID = this.WorkID;
        DataTable dtObj = BP.DA.DBAccess.RunSQLReturnTable(mapM2M.DBOfObjs);

        string str = ",";
        string strT = "";
        foreach (DataRow dr in dtObj.Rows)
        {
            string id = dr[0].ToString();
            CheckBox cb = this.Pub1.GetCBByID("CB_" + id);
            if (cb == null)
                continue;

            if (cb.Checked == false)
                continue;

            str += id + ",";
            strT += "@" + id + cb.Text;
        }
        m2m.Vals = str;
        m2m.ValNames = strT;
        m2m.MapM2M = this.FK_MapM2M;
        m2m.Save();
    }
}
