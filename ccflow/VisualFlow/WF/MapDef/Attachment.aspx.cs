using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Sys;
using BP.En;
using BP.Web;

public partial class WF_MapDef_FrmAttachment : WebPage
{
    public string UploadType
    {
        get
        {
            string s= this.Request.QueryString["UploadType"];
            if (s == null)
                s = "1";
            return s;
        }
    }
    public string FK_MapData
    {
        get
        {
            string s= this.Request.QueryString["FK_MapData"];
            if (s == null)
                s = "test";

            return s;
        }
    }
    public string Ath
    {
        get
        {
            return this.Request.QueryString["Ath"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        FrmAttachment ath = new FrmAttachment();
        ath.CheckPhysicsTable();

        ath.MyPK = this.FK_MapData + "_" + this.Ath;
        if (this.Ath != null)
            ath.RetrieveFromDBSources();

        ath.MyPK = this.FK_MapData + "_" + this.Ath;

        //this.Response.Write(this.Ath);
        //this.Response.Write("  -- "+this.FK_MapData);

        this.Title = "附件属性设置";

        this.Pub1.AddTable();
        //this.Pub1.AddCaptionLeft("附件");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("项目");
        this.Pub1.AddTDTitle("采集");
        this.Pub1.AddTDTitle("说明");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("编号");
        TextBox tb = new TextBox();
        tb.ID = "TB_" + FrmAttachmentAttr.NoOfAth;
        tb.Text = ath.NoOfAth;
        if (this.Ath != null)
            tb.Enabled = false;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("标示号只能英文字母数字或下滑线.");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("名称");
        tb = new TextBox();
        tb.ID = "TB_" + FrmAttachmentAttr.Name;
        tb.Text = ath.Name;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("附件名称.");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("保存到");
        tb = new TextBox();
        tb.ID = "TB_" + FrmAttachmentAttr.SaveTo;
        tb.Text = ath.SaveTo;
        tb.Columns = 60;
        this.Pub1.AddTD("colspan=2",tb);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("类别");
        tb = new TextBox();
        tb.ID = "TB_" + FrmAttachmentAttr.Sort;
        tb.Text = ath.Sort;
        tb.Columns = 60;
        this.Pub1.AddTD("colspan=2", tb);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("colspan=3", "帮助:类别可以为空,设置的格式为:@类别名1@类别名2@类别名3");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("");
        CheckBox cb = new CheckBox();
        cb.ID = "CB_" + FrmAttachmentAttr.IsDownload;
        cb.Text = "是否可下载";
        cb.Checked = ath.IsDownload;
        this.Pub1.AddTD(cb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("");
        cb = new CheckBox();
        cb.ID = "CB_" + FrmAttachmentAttr.IsDelete;
        cb.Text = "是否可删除";
        cb.Checked = ath.IsDelete;
        this.Pub1.AddTD(cb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("");
        cb = new CheckBox();
        cb.ID = "CB_" + FrmAttachmentAttr.IsUpload;
        cb.Text = "是否可上传";
        cb.Checked = ath.IsUpload;
        this.Pub1.AddTD(cb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("");
        Button btn = new Button();
        btn.ID = "Btn";
        btn.Text = "  Save  ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.AddTD(btn);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        FrmAttachment ath = new FrmAttachment();
        ath.MyPK = this.FK_MapData + "_" + this.Ath;
        if (this.Ath != null)
            ath.RetrieveFromDBSources();
        ath = this.Pub1.Copy(ath) as FrmAttachment;
        ath.FK_MapData = this.FK_MapData;


        ath.MyPK = this.FK_MapData + "_" + this.Ath;
        if (this.Ath == null)
        {
            ath.UploadType = (AttachmentUploadType)int.Parse(this.UploadType);
            ath.Insert();
        }
        else
        {
            ath.NoOfAth = this.Ath;
            ath.Update();
        }
        this.WinCloseWithMsg("保存成功");
    }
}