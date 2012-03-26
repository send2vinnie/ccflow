using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.DA;
using BP.En;
using BP.Sys;
using BP;

public partial class Comm_RefFunc_Dot2Dot : BP.Web.UC.UCBase3
{
    #region 属性.
    public AttrOfOneVSM AttrOfOneVSM
    {
        get
        {
            Entity en = ClassFactory.GetEn(this.EnName);
            foreach (AttrOfOneVSM attr in en.EnMap.AttrsOfOneVSM)
            {
                if (attr.EnsOfMM.ToString() == this.AttrKey)
                {
                    return attr;
                }
            }
            throw new Exception("错误没有找到属性． ");
        }
    }
    /// <summary>
    /// 一的工作类
    /// </summary>
    public new  string EnsName
    {
        get
        {
            return this.Request.QueryString["EnsName"];
        }
    }
    public string AttrKey
    {
        get
        {
            return this.Request.QueryString["AttrKey"];
        }
    }
    public   string PK
    {
        get
        {
            if (ViewState["PK"] == null)
            {
                string pk = this.Request.QueryString["PK"];
                if (pk == null)
                    pk = this.Request.QueryString["No"];

                if (pk == null)
                    pk = this.Request.QueryString["RefNo"];

                if (pk == null)
                    pk = this.Request.QueryString["OID"];

                if (pk == null)
                    pk = this.Request.QueryString["MyPK"];


                if (pk != null)
                {
                    ViewState["PK"] = pk;
                }
                else
                {
                    Entity mainEn = BP.DA.ClassFactory.GetEn(this.EnName);
                    ViewState["PK"] = this.Request.QueryString[mainEn.PK];
                }
            }

            return ViewState["PK"].ToString();
        }
    }
    public DropDownList DDL_Group
    {
        get
        {
            return this.ToolBar1.GetDropDownListByID("DDL_Group");
        }
    }

    public bool IsLine
    {
        get
        {
            try
            {
                return (bool)ViewState["IsLine"];
            }
            catch
            {
                return false;
            }
        }
        set
        {
            ViewState["IsLine"] = value;
        }
    }
    public string MainEnName
    {
        get
        {
            return ViewState["MainEnName"] as string;
        }
        set
        {
            this.ViewState["MainEnName"] = value;
        }
    }
    public string MainEnPKVal
    {
        get
        {
            return ViewState["MainEnPKVal"] as string;
        }
        set
        {
            this.ViewState["MainEnPKVal"] = value;
        }
    }
    public bool IsTreeShowWay
    {
        get
        {
            if (this.Request.QueryString["IsTreeShowWay"] != null)
                return true;
            return false;
        }
    }
    /// <summary>
    /// 显示方式
    /// </summary>
    public string ShowWay
    {
        get
        {
            string str= this.Request.QueryString["ShowWay"];
            if (str == null)
                str = this.DDL_Group.SelectedValue;
            return str;
        }
    }
    public string MainEnPK
    {
        get
        {
            return ViewState["MainEnPK"] as string;
        }
        set
        {
            this.ViewState["MainEnPK"] = value;
        }
    }
    private Entity _MainEn = null;
    public Entity MainEn
    {
        get
        {
            if (_MainEn == null)
                _MainEn = ClassFactory.GetEn(this.Request.QueryString["EnsName"]);
            return _MainEn;
        }
        set
        {
            _MainEn = value;
        }
    }

    public int ErrMyNum = 0;
    public Button Btn_Save
    {
        get
        {
            return this.ToolBar1.GetBtnByID("Btn_Save");
        }
    }
    public Button Btn_SaveAndClose
    {
        get
        {
            return this.ToolBar1.GetBtnByID("Btn_SaveAndClose");
        }
    }
    #endregion 属性.

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            #region 处理可能来自于 父实体 的业务逻辑。
            Entity enP = ClassFactory.GetEn(this.EnName);
            this.Page.Title = enP.EnDesc;

            this.MainEnName = enP.EnDesc;
            this.MainEnPKVal = this.PK;
            this.MainEnPK = enP.PK;

            if (enP.EnMap.EnType != EnType.View)
            {
                enP.SetValByKey(enP.PK, this.PK);// =this.PK;
                enP.Retrieve(); //查询。
                enP.Update(); // 执行更新，处理写在 父实体 的业务逻辑。
            }
            MainEn = enP;
            #endregion
        }
        catch (Exception ex)
        {
            this.ToErrorPage(ex.Message);
            //this.WinClose();
            return;
        }

        AttrOfOneVSM ensattr = this.AttrOfOneVSM;
        //this.Label1.Text = ensattr.Desc ;
        this.ToolBar1.AddLab("lab_desc", this.ToE("Group", "分组") + ":");
        DropDownList ddl = new DropDownList();
        ddl.ID = "DDL_Group";
        ddl.AutoPostBack = true;
        this.ToolBar1.Add(ddl);

        ddl.Items.Clear();
        ddl.SelectedIndexChanged += new EventHandler(DDL_Group_SelectedIndexChanged);

        Entity open = ensattr.EnsOfM.GetNewEntity;
        Map map = open.EnMap;
        int len = 19;
        //switch (map.EnDBUrl.DBType)
        //{
        //    case DBType.Oracle9i:
        //        len = DBAccess.RunSQLReturnValInt("SELECT MAX( Length( " + ensattr.AttrOfMText + " )) FROM " + map.PhysicsTable + "");
        //        break;
        //    default:
        //        len = DBAccess.RunSQLReturnValInt("SELECT MAX( Len( " + ensattr.AttrOfMText + " )) FROM " + map.PhysicsTable + "");
        //        break;
        //}

        // 如果最长的 标题  〉 15 长度。就用一行显示。
        if (len > 20)
            this.IsLine = true;
        else
            this.IsLine = false;

        // 先加入enum 类型。
        foreach (Attr attr in map.Attrs)
        {
            /* map */
            if (attr.IsFKorEnum == false)
                continue;
            this.DDL_Group.Items.Add(new ListItem(attr.Desc, attr.Key));
        }

        //// 先加入enum 类型。
        //foreach (Attr attr in map.Attrs)
        //{
        //    /* map */
        //    if (attr.MyFieldType != FieldType.FK)
        //        continue;
        //    this.DDL_Group.Items.Add(new ListItem(attr.Desc, attr.Key));
        //}

        this.DDL_Group.Items.Add(new ListItem("无", "None"));
        foreach (ListItem li in ddl.Items)
        {
            if (li.Value == this.ShowWay)
                li.Selected = true;
        }

        this.ToolBar1.AddSpt("spt");
        UAC uac = ensattr.EnsOfMM.GetNewEntity.HisUAC;
        if (uac.IsInsert == true)
        {
            this.ToolBar1.AddBtn("Btn_Save", this.ToE("Save", "保存"));
            this.Btn_Save.UseSubmitBehavior = false;
            this.Btn_Save.OnClientClick = "this.disabled=true;"; 

          //  this.ToolBar1.AddBtn("Btn_SaveAndClose", this.ToE("SaveAndClose", "保存并关闭"));
            //this.Btn_SaveAndClose.UseSubmitBehavior = false;
            //this.Btn_SaveAndClose.OnClientClick = "this.disabled=true;"; 
        }
        else
        {
            #region 解决Access 不刷新的问题。
            if (uac.IsUpdate == false)
            {
                string rowUrl = this.Request.RawUrl;
                if (rowUrl.IndexOf("rowUrl") > 1)
                {
                }
                else
                {
                    this.Response.Redirect(rowUrl + "&rowUrl=1", true);
                    return;
                }
            }
            #endregion

        }

        CheckBox cb = new CheckBox();
        cb.ID = "checkedAll";
        cb.Attributes["onclick"] = "SelectAll(this);";

        cb.Text = this.ToE("SelectAll", "选择全部");

        this.ToolBar1.Add(cb); // ("<input type=checkbox values='选择全部' text='选择全部'  name=checkedAll onclick='SelectAll()' >选择全部");
       // this.ToolBar1.AddBtn("Btn_Close", this.ToE("Close", "关闭"));
        this.DDL_Group.SelectedIndexChanged += new EventHandler(DDL_Group_SelectedIndexChanged);

        #region 增加按钮事件
        try
        {
            this.ToolBar1.GetBtnByID("Btn_Save").Click += new EventHandler(BPToolBar1_ButtonClick);
        }
        catch
        {
        }

        try
        {
            this.ToolBar1.GetBtnByID("Btn_SaveAndClose").Click += new EventHandler(BPToolBar1_ButtonClick);
        }
        catch
        {

        }
      //  this.ToolBar1.GetBtnByID("Btn_Close").Click += new EventHandler(BPToolBar1_ButtonClick);
        //this.ToolBar1.GetBtnByID("Btn_Help").Click += new EventHandler(BPToolBar1_ButtonClick);
        #endregion

        this.SetDataV2();

        if (this.IsTreeShowWay == false)
            this.SetDataV2();
    }
    #endregion Page_Load

    #region 方法
    public void SetDataV2()
    {
        //this.Tree1.Nodes.Clear();
        this.UCSys1.ClearViewState();

        AttrOfOneVSM attrOM = this.AttrOfOneVSM;
        Entities ensOfM = attrOM.EnsOfM;
        if (ensOfM.Count == 0)
            ensOfM.RetrieveAll();

        try
        {
            Entities ensOfMM = attrOM.EnsOfMM;
            QueryObject qo = new QueryObject(ensOfMM);
            qo.AddWhere(attrOM.AttrOfOneInMM, this.PK);
            qo.DoQuery();

            if (this.DDL_Group.SelectedValue == "None")
            {
                if (this.IsLine)
                    this.UCSys1.UIEn1ToM_OneLine(ensOfM, attrOM.AttrOfMValue, attrOM.AttrOfMText, ensOfMM, attrOM.AttrOfMInMM);
                else
                    this.UCSys1.UIEn1ToM(ensOfM, attrOM.AttrOfMValue, attrOM.AttrOfMText, ensOfMM, attrOM.AttrOfMInMM);
            }
            else
            {
                if (this.IsLine)
                    this.UCSys1.UIEn1ToMGroupKey_Line(ensOfM, attrOM.AttrOfMValue, attrOM.AttrOfMText, ensOfMM, attrOM.AttrOfMInMM, this.DDL_Group.SelectedValue);
                else
                    this.UCSys1.UIEn1ToMGroupKey(ensOfM, attrOM.AttrOfMValue, attrOM.AttrOfMText, ensOfMM, attrOM.AttrOfMInMM, this.DDL_Group.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            ensOfM.GetNewEntity.CheckPhysicsTable();
            this.UCSys1.ClearViewState();
            ErrMyNum++;
            if (ErrMyNum > 3)
            {
                this.UCSys1.AddMsgOfWarning("error", ex.Message);
                return;
            }
            this.SetDataV2();
        }
    }
    public void BindTree()
    {
        if (this.DDL_Group.SelectedValue == "None")
        {
            BindTreeWithNone();
        }
        else
        {
            BindTreeWithGroup();
        }
    }
    public void BindTreeWithGroup()
    {
        //AttrOfOneVSM attrOM = this.AttrOfOneVSM;
        //Entities ensOfM = attrOM.EnsOfM;
        //ensOfM.RetrieveAll();

        //Entities ensOfMM = attrOM.EnsOfMM;
        //QueryObject qo = new QueryObject(ensOfMM);
        //qo.AddWhere(attrOM.AttrOfOneInMM, this.PK);
        //qo.DoQuery();

        ////this.Tree1.Visible = true;
        //this.UCSys1.Clear();
        //this.UCSys1.ClearViewState();

        //Entity en1 = ClassFactory.GetEn(this.Request.QueryString["EnsName"]);
        ////string keys = "&" + en1.PK + "=" + this.PK + "&r=" + DateTime.Now.ToString("MMddhhmmss");
        ////string refstrs = BP.Web.Comm.UC.UCEn.GetRefstrs1(keys, en1, en1.GetNewEntities);
        ////if (refstrs.Length > 0)
        ////    this.Label1.Text = this.GenerCaption(attrOM.Desc + " =>相关功能:" + refstrs);
        ////else
        ////    this.Label1.Text = this.GenerCaption(attrOM.Desc);

        //#region 绑定分组
        //string gropkey = this.DDL_Group.SelectedValue;
        //Attr attr = attrOM.EnsOfM.GetNewEntity.EnMap.GetAttrByKey(gropkey);

        //if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum) // 检查是否是 enum 类型。
        //{
        //    SysEnums ses = new SysEnums(gropkey);
        //    foreach (SysEnum se in ses)
        //    {
        //       // Node tn = this.Tree1.GetNodeByID("TN_T" + se.IntKey) as Node;
        //        TreeNode tn = new TreeNode();
        //        //   Microsoft.Web.UI.WebControls.TreeNode tn = this.Tree1.GetNodeByID("TN_T" + se.IntKey);

        //        if (tn != null)
        //            continue;

        //        tn = new TreeNode();
        //        tn.Text = se.Lab;
        //        tn.Value = "TN_T" + se.IntKey;
        //        this.Tree1.Nodes.Add(tn);
        //    }
        //}
        //else
        //{
        //    Entities ensGroup = attr.HisFKEns;
        //    ensGroup.RetrieveAll();

        //    BindIt(ensGroup, 2, false, "T");
        //    BindIt(ensGroup, 4, false, "T");
        //    BindIt(ensGroup, 6, false, "T");
        //    BindIt(ensGroup, 8, false, "T");
        //    BindIt(ensGroup, 10, false, "T");
        //}
        //#endregion 绑定分组


        //#region 绑定分组 元素
        //foreach (Entity en in ensOfM)
        //{
        //    //  #warning error
        //    // string val = en.GetValStringByKey(gropkey);
        //    //Microsoft.Web.UI.WebControls.TreeNode tn = this.Tree1.GetNodeByID("TN_T" + val);

        //    // //TreeNode tn = new TreeNode(); // this.Tree1.GetNodeByID("TN_T" + val);
        //    // if (tn == null)
        //    //     continue;

        //    // if (this.Tree1.GetNodeByID("TN_" + en.GetValStringByKey("No"), tn) != null)
        //    //     continue;

        //    // Node nd = new Node();
        //    // nd.Text = en.GetValStringByKey("Name");
        //    // nd.ID = "TN_" + en.GetValStringByKey("No");
        //    // nd.CheckBox = true;
        //    // tn.Nodes.Add(nd);

        //}

        //#endregion 绑定分组 元素


        //#region 设置分组 元素 的选择项目

        //foreach (Entity en in ensOfMM)
        //{
            
        //    //string val = en.GetValStringByKey(attrOM.AttrOfMInMM);
        //    //Microsoft.Web.UI.WebControls.TreeNode tn = this.Tree1.GetNodeByID("TN_" + val);
        //    //if (tn == null)
        //    //    continue;
        //    //tn.Checked = true;
        //}

        //#endregion 设置分组 元素 的选择项目

    }
    public void BindTreeWithNone()
    {
        //AttrOfOneVSM attrOM = this.AttrOfOneVSM;
        //Entities ensOfM = attrOM.EnsOfM;
        //ensOfM.RetrieveAll();

        //Entities ensOfMM = attrOM.EnsOfMM;
        //QueryObject qo = new QueryObject(ensOfMM);
        //qo.AddWhere(attrOM.AttrOfOneInMM, this.PK);
        //qo.DoQuery();

        //this.Tree1.Visible = true;
        //this.UCSys1.Clear();
        //this.UCSys1.ClearViewState();

        ////Entity en1 = ClassFactory.GetEn(this.Request.QueryString["EnsName"]);
        ////string keys = "&" + en1.PK + "=" + this.PK + "&r=" + DateTime.Now.ToString("MMddhhmmss");
        ////string refstrs = BP.Web.Comm.UC.UCEn.GetRefstrs1(keys, en1, en1.GetNewEntities);
        ////if (refstrs.Length > 0)
        ////    this.Label1.Text = this.GenerCaption(attrOM.Desc + " =>相关功能:" + refstrs);
        ////else
        ////    this.Label1.Text = this.GenerCaption(attrOM.Desc);


        //BindIt(ensOfM, 2, true, "");
        //BindIt(ensOfM, 4, true, "");
        //BindIt(ensOfM, 6, true, "");
        //BindIt(ensOfM, 8, true, "");
        //BindIt(ensOfM, 10, true, "");

        //foreach (Entity en in ensOfMM)
        //{
        //    //string val = en.GetValStringByKey(attrOM.AttrOfMInMM);
        //    //Microsoft.Web.UI.WebControls.TreeNode tn = this.Tree1.GetNodeByID("TN_" + val);
        //    //if (tn == null)
        //    //    continue;
        //    //tn.Checked = true;
        //}
    }
    public void BindIt(Entities ens, int len, bool isCheckBok, string idPro)
    {
        foreach (Entity en in ens)
        {
            //string no = en.GetValStringByKey("No");
            //if (no.Length != len)
            //    continue;

            //Microsoft.Web.UI.WebControls.TreeNode myyy = this.Tree1.GetNodeByID("TN_" + idPro + no);
            //if (myyy != null)
            //    continue;

            //Node tn = new Node();
            //tn.Text = en.GetValStringByKey("Name");
            //tn.ID = "TN_" + idPro + en.GetValStringByKey("No");
            //tn.CheckBox = isCheckBok;
            //if (len == 2)
            //{
            //    this.Tree1.Nodes.Add(tn);
            //    continue;
            //}

            //no = no.Substring(0, no.Length - 2);
            //Microsoft.Web.UI.WebControls.TreeNode mtn = this.Tree1.GetNodeByID("TN_" + idPro + no);
            //if (mtn == null)
            //{
            //    this.Tree1.Nodes.Add(tn);
            //}
            //else
            //{
            //    mtn.Nodes.Add(tn);
            //}
        }
    }
    private void BPToolBar1_ButtonClick(object sender, System.EventArgs e)
    {
        Btn btn = (Btn)sender;
        switch (btn.ID)
        {
            case NamesOfBtn.SelectNone:
                //this.CBL1.SelectNone();
                break;
            case NamesOfBtn.SelectAll:
                //this.CBL1.SelectAll();
                break;
            case NamesOfBtn.Save:
                if (this.IsTreeShowWay)
                    SaveTree();
                else
                    Save();

                string str = this.Request.RawUrl;
                if (str.Contains("ShowWay="))
                    str =str.Replace("&ShowWay=", "&1=");
                this.Response.Redirect(str + "&ShowWay=" + this.DDL_Group.SelectedItem.Value, true);
                return;
            case "Btn_SaveAndClose":
                if (this.IsTreeShowWay)
                    SaveTree();
                else
                    Save();

                this.WinClose();
                break;
            case "Btn_Close":
                this.WinClose();
                break;
            case "Btn_EditMEns":
                this.EditMEns();
                break;
            default:
                throw new Exception("@没有找到" + btn.ID);
        }
    }
    #endregion 方法


    #region 操作
    public void EditMEns()
    {
        this.WinOpen(this.Request.ApplicationPath + "/Comm/UIEns.aspx?EnsName=" + this.AttrOfOneVSM.EnsOfM.ToString());
    }
    public void Save()
    {
        AttrOfOneVSM attr = this.AttrOfOneVSM;
        Entities ensOfMM = attr.EnsOfMM;
        ensOfMM.Delete(attr.AttrOfOneInMM, this.PK);

        string msg = "";

        AttrOfOneVSM attrOM = this.AttrOfOneVSM;
        Entities ensOfM = attrOM.EnsOfM;
        ensOfM.RetrieveAll();
        foreach (Entity en in ensOfM)
        {
            string pk = en.GetValStringByKey(attr.AttrOfMValue);
            CheckBox cb = (CheckBox)this.UCSys1.FindControl("CB_" + pk);
            if (cb == null)
                continue;

            if (cb.Checked == false)
                continue;

            Entity en1 = ensOfMM.GetNewEntity;
            en1.SetValByKey(attr.AttrOfOneInMM, this.PK);
            en1.SetValByKey(attr.AttrOfMInMM, pk);
            en1.Insert();

            //   msg += "执行插入错误：" + en1.EnDesc + " " + ex.Message;
        }

        Entity enP = ClassFactory.GetEn(this.EnName);
        if (enP.EnMap.EnType != EnType.View)
        {
            enP.SetValByKey(enP.PK, this.PK);// =this.PK;
            enP.Retrieve(); //查询。
            try
            {
                enP.Update(); // 执行更新，处理写在 父实体 的业务逻辑。
            }
            catch (Exception ex)
            {
                msg += "执行更新错误：" + enP.EnDesc + " " + ex.Message;
            }
        }
        if (msg != "")
            this.ResponseWriteBlueMsg(msg);
    }
    public void SaveTree()
    {
        AttrOfOneVSM attr = this.AttrOfOneVSM;
        Entities ensOfMM = attr.EnsOfMM;
        ensOfMM.Delete(attr.AttrOfOneInMM, this.PK); //删除已经保存的数据。

        AttrOfOneVSM attrOM = this.AttrOfOneVSM;
        Entities ensOfM = attrOM.EnsOfM;
        ensOfM.RetrieveAll();

        foreach (Entity en in ensOfM)
        {
            //string pk = en.GetValStringByKey(attr.AttrOfMValue);
            //Microsoft.Web.UI.WebControls.TreeNode cb = this.Tree1.GetNodeByID("TN_" + pk);
            //if (cb == null)
            //    continue;

            //if (cb.Checked == false)
            //    continue;

            //Entity en1 = ensOfMM.GetNewEntity;
            //en1.SetValByKey(attr.AttrOfOneInMM, this.PK);
            //en1.SetValByKey(attr.AttrOfMInMM, pk);
            //en1.Insert();
        }

        Entity enP = ClassFactory.GetEn(this.Request.QueryString["EnsName"]);
        if (enP.EnMap.EnType != EnType.View)
        {
            enP.SetValByKey(enP.PK, this.PK);// =this.PK;
            enP.Retrieve(); //查询。
            enP.Update(); // 执行更新，处理写在 父实体 的业务逻辑。
        }
        //  this.Response.Redirect(this.Request.RawUrl + "&IsCheckTree=1", true);
    }
    #endregion

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }
    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    //public string RowUrl
    //{
    //    get
    //    {
    //        //string str = "EnsName="+this.EnsName+"&EnName="+this.EnName+"&AttrKey="+this.AttrKey+"&NodeID=101";
    //        return str;
    //    }
    //}
    private void DDL_Group_SelectedIndexChanged(object sender, EventArgs e)
    {

        string str = this.Request.RawUrl;
        if (str.Contains("ShowWay="))
            str =str.Replace("&ShowWay=", "&1=");
        this.Response.Redirect(str + "&ShowWay=" + this.DDL_Group.SelectedItem.Value, true);
        return;

        // this.SetDataV2();
        CheckBox mycb = this.ToolBar1.GetCBByID("RB_Tree");
        if (mycb == null)
            this.SetDataV2();
        else
            this.BindTree();
    }
    #endregion

   
}


