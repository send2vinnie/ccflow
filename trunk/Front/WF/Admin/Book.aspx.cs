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
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_BookSet : WebPage
{
    public int NodeID
    {
        get
        {
            return int.Parse( this.Request.QueryString["NodeID"] );
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }

    public void DoNew(BookTemplate book)
    {
        this.Ucsys1.Clear();

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);

        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX("<a href='Book.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "' >" + this.ToE("Back","返回") + "</a> - <img src='../../Images/Btn/New.gif' />" + this.ToE("New", "新建") + "-" + BP.WF.Glo.GenerHelp("Book" ));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle(ToE("Item", "项目"));
        this.Ucsys1.AddTDTitle(ToE("Input","输入"));
        this.Ucsys1.AddTDTitle(ToE("Note","备注"));
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(ToE("No","编号"));
        TB tb = new TB();
        tb.ID = "TB_No";
        tb.Text = book.No;
        tb.Enabled = false;
        if (tb.Text == "")
        {
            tb.Text = this.ToE("AutoGener","系统自动生成");
        }

        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("比如：NoteBill");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("Name","名称")); // 文书/单据名称
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = book.Name;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("ReplaceVal", "要替换<br>特殊字段")); // 文书/单据名称
        tb = new TB();
        tb.ID = "TB_ReplaceVal";
        tb.Text = book.ReplaceVal;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 3;
        this.Ucsys1.AddTD(tb);
        this.Ucsys1.AddTD("格式为:@表名称.字段名称=要替换的值 比如：@NodeName.ID='' ");
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTR();
        this.Ucsys1.AddTD(this.ToE("BookTemplete", "文书模板") );
        HtmlInputFile file = new HtmlInputFile();
        file.ID = "f";
        this.Ucsys1.AddTD("colspan=2", file);
       // this.Ucsys1.AddTD("colspan=2",this.ToE("BookTempleteD","选择本地模板上传"));
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD class=TD colspan=3 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = this.ToE("Save","保存");
        this.Ucsys1.Add(btn);
        btn.Click += new EventHandler(btn_Click);
        this.Ucsys1.Add(btn);
        if (book.No.Length > 1)
        {
            btn = new Button();
            btn.ID = "Btn_Gener";
            btn.Text = this.ToE("Test", "测试生成");   //"测试生成";
            this.Ucsys1.Add(btn);
            btn.Click += new EventHandler(btn_Gener_Click);
            this.Ucsys1.Add(btn);

            btn = new Button();
            btn.ID = "Btn_Del";
            btn.Text = this.ToE("Del", "删除"); // "删除文书";
            this.Ucsys1.Add(btn);
            btn.Attributes["onclick"] += " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
            btn.Click += new EventHandler(btn_Del_Click);
        }
        string url = "";
        if (this.RefNo != null)
            url = "<a href='../../Data/CyclostyleFile/" + book.No + ".rtf'><img src='../../Images/Btn/save.gif' border=0/>" + this.ToE("DownTemplete", "模板下载") + "</a>";

        this.Ucsys1.Add(url + "</TD>");
        this.Ucsys1.AddTREnd();

        //this.Ucsys1.AddTRSum();
        //this.Ucsys1.AddTDBigDoc("colspan=3 class=BigDoc", "提示：关于如何制作文书请打开帮助。");
        //this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTable();
    }
    void btn_Gener_Click(object sender, EventArgs e)
    {
        this.Alert("测试成功");
        string url = "Book.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefNo=" + this.RefNo;
        this.Response.Redirect(url, true);
    }
    void btn_Click(object sender, EventArgs e)
    {
        BookTemplate bt = new BookTemplate();
        if (this.RefNo != null && this.RefNo != "")
        {
            bt.No = this.RefNo;
            bt.Retrieve();
        }

        bt = this.Ucsys1.Copy(bt) as BookTemplate;
        if (this.RefNo == null)
        {
            try
            {
                bt.No = BP.DA.chs2py.convert(bt.Name);
            }
            catch
            {
                bt.No = BP.DA.DBAccess.GenerOID().ToString();
            }

            if (bt.IsExits)
            {
                //this.Alert(this.ToE("NoExits", "不存在") + " [" + bt.No + "]");
                bt.No = BP.DA.DBAccess.GenerOID().ToString();
                return;
            }
        }

        HtmlInputFile file = this.Ucsys1.FindControl("f") as HtmlInputFile;
        if (file != null && file.Value.IndexOf(":") != -1)
        {
            /* 如果包含这二个字段。*/
            string fileName = file.PostedFile.FileName;
            fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);

            if (bt.Name == "")
                bt.Name = fileName.Replace(".rtf", "");

            string ext = "";
            if (fileName.IndexOf(".") != -1)
                ext = fileName.Substring(fileName.LastIndexOf(".") + 1);

            if (ext.ToLower().Contains("rtf") == false)
            {
                this.Alert(this.ToE("WaringRtf", "必须是 rtf 格式的 才能被识别。"));
                return;
            }
            string fullFile = BP.SystemConfig.PathOfCyclostyleFile + "\\" + bt.No + ".rtf";
            file.PostedFile.SaveAs(fullFile);
        }

        bt.NodeID = this.NodeID;
        if (this.RefNo == null)
        {
            try
            {
                bt.No = BP.DA.chs2py.convert(bt.Name);
            }
            catch
            {
                bt.No = BP.DA.DBAccess.GenerOID().ToString();
            }
            bt.Insert();
        }
        else
        {
            bt.Update();
        }

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        string bookids = "";
        BookTemplates tmps = new BookTemplates(nd);
        foreach (BookTemplate tmp in tmps)
        {
            bookids += "@"+tmp.No;
        }
        nd.HisBookIDs = bookids;
        nd.Update();

        this.Response.Redirect("Book.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        BookTemplate t = new BookTemplate();
        t.No = this.RefNo;
        t.Delete();
        this.Response.Redirect("Book.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID, true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = this.ToE("NodeBookDesign", "节点文书设计"); //"节点文书设计";

        switch (this.DoType)
        {
            case "New":
                BookTemplate bk = new BookTemplate();
                bk.NodeID = this.RefOID;
                this.DoNew(bk);
                return;
            case "Edit":
                BookTemplate bk1 = new BookTemplate(this.RefNo);
                bk1.NodeID = this.NodeID;
                this.DoNew(bk1);
                return;
            default:
                break;
        }

        BookTemplates books = new BookTemplates(this.NodeID);
        if (books.Count == 0)
        {
            this.Response.Redirect("Book.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New", true);
            return;
        }

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        this.Title = nd.Name + " - " + this.ToE("BookMang", "文书管理");  //文书管理
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(nd.Name + " - <a href='Book.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=New'><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("New", "新建") + "</a>");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("IDX");
        this.Ucsys1.AddTDTitle(this.ToE("Node","节点"));
        this.Ucsys1.AddTDTitle(this.ToE("No","编号"));
        this.Ucsys1.AddTDTitle(this.ToE("Name","名称"));
        this.Ucsys1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Ucsys1.AddTREnd();
        int i = 0;
        foreach (BookTemplate book in books)
        {
            i++;
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDIdx(i);
            this.Ucsys1.AddTD(book.NodeID);
            this.Ucsys1.AddTD(book.No);
            this.Ucsys1.AddTD("<img src='../../Images/Btn/Word.gif' >" + book.Name);
            this.Ucsys1.AddTD("<a href='Book.aspx?FK_Flow=" + this.FK_Flow + "&NodeID=" + this.NodeID + "&DoType=Edit&RefNo=" + book.No + "'><img src='../../Images/Btn/Edit.gif' border=0/>" + this.ToE("Edit", "编辑") + "</a>|<a href='../../Data/CyclostyleFile/" + book.No + ".rtf'><img src='../../Images/Btn/save.gif' border=0/>" + this.ToE("DownTemplete", "模板下载") + "</a>");
            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTableEnd();
    }


}
