using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_FreeFrm_Print : WebPage
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
                return 107;
            }
        }
    }
    public Int64 WorkID
    {
        get
        {
            try
            {
                return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 39;
            }
        }
    }
    public string BillIdx
    {
        get
        {
            return this.Request.QueryString["BillIdx"];
        }
    }
    #endregion 属性

    public void PrintBill()
    {
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        string path = @"D:\ccflow\VisualFlow\DataUser\CyclostyleFile\FlowFrm\" + nd.FK_Flow + "\\" + nd.NodeID + "\\";
        string[] fls = System.IO.Directory.GetFiles(path);
        string file = fls[int.Parse(this.BillIdx)];
        file = file.Replace(@"D:\ccflow\VisualFlow\DataUser\CyclostyleFile", "");

        FileInfo finfo = new FileInfo(file);
        string tempName = finfo.Name.Split('.')[0];
        string tempNameChinese = finfo.Name.Split('.')[1];


        string toPath = @"D:\ccflow\VisualFlow\DataUser\Bill\FlowFrm\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
        if (System.IO.Directory.Exists(toPath) == false)
            System.IO.Directory.CreateDirectory(toPath);

        string billFile = toPath + "\\" + tempName + "." + this.WorkID + ".doc";

        BP.Rpt.RTF.RTFEngine engine = new BP.Rpt.RTF.RTFEngine();
        if (tempName.ToLower() == "all" )
        {
            FrmNodes fns = new FrmNodes(this.FK_Node);
            foreach (FrmNode fn in fns)
            {
                GEEntity ge = new GEEntity(fn.FK_Frm, this.WorkID);
                engine.AddEn(ge);
            }
            engine.MakeDoc(file, toPath, tempName + "." + this.WorkID + ".doc", null, false);
        }
        else
        {
            GEEntity ge = new GEEntity(tempName, this.WorkID);
            engine.HisGEEntity = ge;
            engine.AddEn(ge);
            engine.MakeDoc(file, toPath, tempName + "." + this.WorkID + ".doc", null, false);
        }

        BP.PubClass.OpenWordDocV2(billFile, tempNameChinese + ".doc");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "单据打印";

        if (this.BillIdx != null)
        {
            this.PrintBill();
            return;
        }

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle("表单编号");
        this.Pub1.AddTDTitle("表单名");
        this.Pub1.AddTDTitle("下载");
        this.Pub1.AddTREnd();

        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        string path = @"D:\ccflow\VisualFlow\DataUser\CyclostyleFile\FlowFrm\" + nd.FK_Flow + "\\" + nd.NodeID + "\\";

        string[] fls = null;
        try
        {
            fls = System.IO.Directory.GetFiles(path);
        }
        catch
        {
            this.Pub1.AddTableEnd();
            this.Pub1.AddMsgOfWarning("获取模版错误", "模版文件没有找到。"+path);
            return;
        }

        int idx = 0;
        int fileIdx = -1;
        foreach (string f in fls)
        {
            fileIdx++;
            string myfile = f.Replace(path, "");
            if (myfile.ToLower().Contains(".rtf") == false)
                continue;

            string[] strs = myfile.Split('.');
            idx++;

            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(idx);
            this.Pub1.AddTD(strs[0]);
            this.Pub1.AddTD(strs[1]);
            this.Pub1.AddTD("<a href='Print.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&BillIdx=" + fileIdx + "' target=_blank >打印</a>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }
}