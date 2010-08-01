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
using BP.Web.Comm.UC;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Web.Controls;
using BP.Sys;
using BP.Web;
using BP;
public partial class Comm_UC_UIEn : BP.Web.UC.UCBase3
{
    #region 属性
    /// <summary>
    /// 类名成．
    /// </summary>
    public string EnName
    {
        get
        {
            if (this.Request.QueryString["EnName"] == null)
            {
                string s = this.Request.QueryString["EnName"];
                if (s == null)
                    return "BP.Port.Emp";
                else
                    return s;
            }
            else
                return this.Request.QueryString["EnName"];
        }
    }
    /// <summary>
    /// 得到一个新的事例数据．
    /// </summary>
    public Entity GetEnDa
    {
        get
        {
            Entity en = BP.DA.ClassFactory.GetEn(this.EnName);
            if (en.PKCount == 1)
            {
                if (this.Request.QueryString["PK"] != null)
                {
                    en.PKVal = this.Request.QueryString["PK"];
                }
                else
                {
                    if (this.Request.QueryString[en.PK] == null)
                        return en;
                    else
                        en.PKVal = this.Request.QueryString[en.PK];
                }
                if (en.IsExits == false)
                    throw new Exception("@记录不存在,或者没有保存.");
                else
                    en.RetrieveFromDBSources();
                return en;
            }
            else if (en.IsMIDEntity)
            {
                string val = this.Request.QueryString["MID"];
                if (val == null)
                    val = this.Request.QueryString["PK"];
                if (val == null)
                {
                    return en;
                }
                else
                {
                    en.SetValByKey("MID", val);
                    en.RetrieveFromDBSources();
                    return en;
                }
            }

            Attrs attrs = en.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.IsPK)
                {
                    string str = this.Request.QueryString[attr.Key];
                    if (str == null)
                    {
                        if (en.IsMIDEntity)
                        {
                            en.SetValByKey("MID", this.Request.QueryString["PK"]);
                            continue;
                        }
                        else
                        {
                            throw new Exception("@没有把主键值[" + attr.Key + "]传输过来.");
                        }
                    }

                    en.SetValByKey(attr.Key, this.Request.QueryString[attr.Key]);
                }
            }
            if (en.IsExits == false)
            {
                throw new Exception("@数据没有记录.");
            }
            else
            {
                en.RetrieveFromDBSources();
            }
            return en;
        }
    }
    public BP.Web.Controls.Btn Btn_New
    {
        get
        {
            return this.ToolBar1.GetBtnByID(NamesOfBtn.New);
        }
    }
    public BP.Web.Controls.Btn Btn_Copy
    {
        get
        {
            return this.ToolBar1.GetBtnByID(NamesOfBtn.Copy);
        }
    }
    public BP.Web.Controls.Btn Btn_Delete
    {
        get
        {
            return this.ToolBar1.GetBtnByID(NamesOfBtn.Delete);
        }
    }
    public BP.Web.Controls.Btn Btn_Adjunct
    {
        get
        {
            return this.ToolBar1.GetBtnByID(NamesOfBtn.Adjunct);
        }
    }
    ///// <summary>
    ///// 当前的实体集合．
    ///// </summary>
    //public Entities GetEns
    //{
    //    get
    //    {
    //        if (_GetEns == null)
    //        {
    //            if (this.EnName != null)
    //            {
    //                Entity en = BP.DA.ClassFactory.GetEn(EnName);
    //                _GetEns = en.GetNewEntities;
    //            }
    //            else
    //            {
    //                _GetEns = BP.DA.ClassFactory.GetEns(EnName);
    //            }
    //        }
    //        return _GetEns;
    //    }
    //}
    public Entity _CurrEn = null;
    public Entity CurrEn
    {
        get
        {
            if (_CurrEn == null)
            {
                _CurrEn = this.GetEnDa;
            }
            return _CurrEn;
        }
        set
        {
            _CurrEn = value;
        }
    }
    /// <summary>
    /// 是否Readonly.
    /// </summary>
    public bool IsReadonly
    {
        get
        {
            return (bool)this.ViewState["IsReadonly"];
        }
        set
        {
            ViewState["IsReadonly"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 清除缓存;
        this.Response.Expires = -1;
        this.Response.ExpiresAbsolute = DateTime.Now.AddMonths(-1);
        this.Response.CacheControl = "no-cache";
        #endregion 清除缓存

        try
        {
            #region 判断权限
            UAC uac = this.CurrEn.HisUAC;
            if (uac.IsView == false)
                throw new Exception("@对不起，您没有查看的权限！");

            this.IsReadonly = !uac.IsUpdate;  //是否更有修改的权限．
            if (this.Request.QueryString["IsReadonly"] == "1"
                || this.Request.QueryString["Readonly"] == "1")
                this.IsReadonly = true;
            #endregion

            //  this.ToolBar1.DivInfoBlockBegin();
            this.ToolBar1.Add("&nbsp;&nbsp;");
            this.ToolBar1.InitFuncEn(uac, this.CurrEn);

            //  this.ToolBar1.DivInfoBlockEnd();

            this.UCEn1.IsReadonly = this.IsReadonly;
            this.UCEn1.IsShowDtl = true;
            this.UCEn1.HisEn = this.CurrEn;

            //if (this.IsReadonly)
            //    this.ToolBar1.Enabled = false;

            string pk = this.Request.QueryString["PK"];
            if (pk == null)
                pk = this.Request.QueryString[this.CurrEn.PK];

            this.UCEn1.Bind(this.CurrEn, this.CurrEn.ToString(), this.IsReadonly, false);

        }
        catch (Exception ex)
        {
            this.Response.Write(ex.Message);
            Entity en = ClassFactory.GetEn(this.EnName);
            en.CheckPhysicsTable();
            return;
        }

        this.Page.Title = this.CurrEn.EnDesc;


        #region 设置事件
        if (this.Btn_DelFile != null)
            this.Btn_DelFile.Click += new ImageClickEventHandler(Btn_DelFile_Click);

        if (this.ToolBar1.IsExit(NamesOfBtn.New))
            this.ToolBar1.GetBtnByID(NamesOfBtn.New).Click += new System.EventHandler(this.ToolBar1_ButtonClick);

        if (this.ToolBar1.IsExit(NamesOfBtn.Save))
            this.ToolBar1.GetBtnByID(NamesOfBtn.Save).Click += new System.EventHandler(this.ToolBar1_ButtonClick);

        if (this.ToolBar1.IsExit(NamesOfBtn.SaveAndClose))
            this.ToolBar1.GetBtnByID(NamesOfBtn.SaveAndClose).Click += new System.EventHandler(this.ToolBar1_ButtonClick);

        if (this.ToolBar1.IsExit(NamesOfBtn.SaveAndNew))
            this.ToolBar1.GetBtnByID(NamesOfBtn.SaveAndNew).Click += new System.EventHandler(this.ToolBar1_ButtonClick);

        if (this.ToolBar1.IsExit(NamesOfBtn.Delete))
            this.ToolBar1.GetBtnByID(NamesOfBtn.Delete).Click += new System.EventHandler(this.ToolBar1_ButtonClick);


        AttrFiles fls = this.CurrEn.EnMap.HisAttrFiles;
        foreach (AttrFile fl in fls)
        { 
            if (this.UCEn1.IsExit("Btn_DelFile" + fl.FileNo))
                this.UCEn1.GetImageButtonByID("Btn_DelFile" + fl.FileNo).Click += new ImageClickEventHandler(Btn_DelFile_X_Click);
        }
        #endregion 设置事件
    }


    public ImageButton Btn_DelFile
    {
        get
        {
            return this.UCEn1.FindControl("Btn_DelFile") as ImageButton;
        }
    }
    public void DelFile(string id)
    {
    }
    private void Btn_DelFile_X_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;

        string id = btn.ID.Replace("Btn_DelFile", "");
        SysFileManager sf = new SysFileManager();

        //     Entity en = this.UCEn1.GetEnData(this.GetEns.GetNewEntity);

        string sql = "DELETE " + sf.EnMap.PhysicsTable + " WHERE " + SysFileManagerAttr.EnName + "='" +  this.EnName + "' and RefVal='" + this.PKVal + "' and " + SysFileManagerAttr.AttrFileNo + "='" + id + "'";
        BP.DA.DBAccess.RunSQL(sql);
        this.Response.Redirect("UIEn.aspx?EnName=" + this.EnName + "&PK=" + this.PKVal, true);
    }

    private void Btn_DelFile_Click(object sender, ImageClickEventArgs e)
    {
        Entity en = this.UCEn1.GetEnData( this.CurrEn );
        string file = en.GetValStringByKey("MyFilePath") + "//" + en.PKVal + "." + en.GetValStringByKey("MyFileExt");
        try
        {
            System.IO.File.Delete(file);
        }
        catch
        {

        }
        en.SetValByKey("MyFileExt", "");
        en.SetValByKey("MyFileName", "");
        en.SetValByKey("MyFilePath", "");
        en.Update();
        this.Response.Redirect("UIEn.aspx?EnName=" + this.EnName + "&PK=" + this.PKVal, true);
    }

    private void ToolBar1_ButtonClick(object sender, System.EventArgs e)
    {
        Btn btn = (Btn)sender;
        try
        {
            switch (btn.ID)
            {
                case NamesOfBtn.Copy:
                    Copy();
                    break;
                case NamesOfBtn.Help:
                    //this.Helper(this.GetEns.GetNewEntity.EnMap.Helper);
                    break;
                case NamesOfBtn.New:
                    //   New();
                    this.Response.Redirect("UIEn.aspx?EnName=" + this.EnName, true);
                    break;
                case NamesOfBtn.SaveAndNew:
                    try
                    {
                        this.Save();
                    }
                    catch (Exception ex)
                    {
                        this.ResponseWriteBlueMsg(ex.Message);
                        return;
                    }
                    this.Response.Redirect("UIEn.aspx?EnName=" + this.EnName, true);
                    break;
                case NamesOfBtn.SaveAndClose:
                    try
                    {
                        this.Save();
                        this.WinClose();
                    }
                    catch (Exception ex)
                    {
                        this.ResponseWriteBlueMsg(ex.Message);
                        return;
                    }
                    break;
                case NamesOfBtn.Save:
                    try
                    {
                        this.Save();
                    }
                    catch (Exception ex)
                    {
                        this.Alert(ex.Message);
                        return;
                    }
                    this.Response.Redirect("UIEn.aspx?EnName=" + this.EnName + "&PK=" + this.PKVal, true);
                    break;
                case NamesOfBtn.Delete:
                    try
                    {
                        Entity en =  this.CurrEn;
                        if (this.PKVal != null)
                            en.PKVal = this.PKVal;
                        en.Delete();
                        this.ToMsgPage("删除成功!!!");
                        return;
                    }
                    catch (Exception ex)
                    {
                        this.ToMsgPage("删除期间出现错误: \t\n" + ex.Message);
                        //this.ToMsgPage("删除成功!!!");
                        return;
                    }
                    return;
                case NamesOfBtn.Close:
                    this.WinClose();
                    break;
                case "Btn_EnList":
                    this.EnList();
                    break;
                case NamesOfBtn.Export:
                    //this.ExportDGToExcel_OpenWin(this.UCEn1,"" );
                    break;
                case NamesOfBtn.Adjunct:
                    //this.InvokeFileManager(this.GetEnDa);
                    break;
                default:
                    throw new Exception("@没有找到" + btn.ID);
            }
        }
        catch (Exception ex)
        {
            this.ResponseWriteRedMsg(ex.Message);
        }
    }
    public object PKVal
    {
        get
        {
            object obj = ViewState["MyPK"];
            if (obj == null)
                obj = this.Request.QueryString["PK"];

            if (obj == null)
                obj = this.Request.QueryString["OID"];

            if (obj == null)
                obj = this.Request.QueryString["No"];

            if (obj == null)
                obj = this.Request.QueryString["MyPK"];

            return obj;
        }
        set
        {
            this.ViewState["MyPK"] = value;
        }
    }

    #region 操作
    /// <summary>
    /// new
    /// </summary>
    public void New()
    {
        this.Response.Redirect("UIEn.aspx?EnName=" + this.EnName, true);
        return;
    }
    public void Copy()
    {
        try
        {
            this.PKVal = null;
            Entity en = this.UCEn1.GetEnData( this.CurrEn );
            en.Copy();
            this.UCEn1.Bind(en, en.ToString(), this.IsReadonly, true);
        }
        catch (Exception ex)
        {
            this.ResponseWriteRedMsg(ex);
        }
    }
    /// <summary>
    /// delete
    /// </summary>
    public void Delete()
    {
        Entity en = this.GetEnDa;
        en.PKVal = this.PKVal;
        en.Delete();
        this.WinClose();
    }
    public void Save()
    {
        Entity en = this.UCEn1.GetEnData( this.CurrEn ); 
        if (this.PKVal != null)
            en.PKVal = this.PKVal;

        this.CurrEn = en;
        en.Save();
        this.PKVal = en.PKVal;

        #region 保存 实体附件
        try
        {
            if (en.EnMap.Attrs.Contains("MyFileName"))
            {
                HtmlInputFile file = this.UCEn1.FindControl("file") as HtmlInputFile;
                if (file != null && file.Value.IndexOf(".") != -1)
                {
                    BP.Sys.EnCfg cfg = new EnCfg(en.ToString());
                    if (System.IO.Directory.Exists(cfg.FJSavePath) == false)
                        System.IO.Directory.CreateDirectory(cfg.FJSavePath);

                    /* 如果包含这二个字段。*/
                    string fileName = file.PostedFile.FileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);

                    string filePath = cfg.FJSavePath;
                    en.SetValByKey("MyFilePath", filePath);

                    string ext = "";
                    if (fileName.IndexOf(".") != -1)
                        ext = fileName.Substring(fileName.LastIndexOf(".") + 1);

                    en.SetValByKey("MyFileExt", ext);
                    en.SetValByKey("MyFileName", fileName);
                    en.SetValByKey("WebPath", cfg.FJWebPath + en.PKVal + "." + ext);

                    string fullFile = filePath + "/" + en.PKVal + "." + ext;

                    file.PostedFile.SaveAs(fullFile);
                    file.PostedFile.InputStream.Close();
                    file.PostedFile.InputStream.Dispose();
                    file.Dispose();

                    System.IO.FileInfo info = new System.IO.FileInfo(fullFile);
                    en.SetValByKey("MyFileSize", BP.DA.DataType.PraseToMB(info.Length));
                    if (DataType.IsImgExt(ext))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(fullFile);
                        en.SetValByKey("MyFileH", img.Height);
                        en.SetValByKey("MyFileW", img.Width);
                        img.Dispose();
                    }
                    en.Update();
                }
            }
        }
        catch (Exception ex)
        {
            this.Alert("保存附件出现错误：" + ex.Message);
        }
        #endregion


        #region 保存 属性 附件
        try
        {
            AttrFiles fils = en.EnMap.HisAttrFiles;
            SysFileManagers sfs = new SysFileManagers(en.ToString(), en.PKVal.ToString());
            foreach (AttrFile fl in fils)
            {
                HtmlInputFile file = (HtmlInputFile)this.UCEn1.FindControl("F" + fl.FileNo);
                if (file.Value.Contains(".") == false)
                    continue;

                SysFileManager enFile = sfs.GetEntityByKey(SysFileManagerAttr.AttrFileNo, fl.FileNo) as SysFileManager;
                SysFileManager enN = null;
                if (enFile == null)
                {
                    enN = this.FileSave(null, file, en);
                }
                else
                {
                    enFile.Delete();
                    enN = this.FileSave(null, file, en);
                }

                enN.AttrFileNo = fl.FileNo;
                enN.AttrFileName = fl.FileName;
                enN.EnName = en.ToString();
                enN.Update();
            }
        }
        catch (Exception ex)
        {
            this.Alert("保存附件出现错误：" + ex.Message);
        }
        #endregion
    }
    /// <summary>
    /// 文件保存
    /// </summary>
    /// <param name="fileNameDesc"></param>
    /// <param name="File1"></param>
    /// <returns></returns>
    private SysFileManager FileSave(string fileNameDesc, HtmlInputFile File1, Entity myen)
    {
        SysFileManager en = new SysFileManager();
        en.EnName = myen.ToString();
        // en.FileID = this.RefPK + "_" + count.ToString();
        EnCfg cfg = new EnCfg(en.EnName);

        string filePath = cfg.FJSavePath; // BP.SystemConfig.PathOfFDB + "\\" + this.EnName + "\\";
        if (System.IO.Directory.Exists(filePath) == false)
            System.IO.Directory.CreateDirectory(filePath);

        string ext = System.IO.Path.GetExtension(File1.PostedFile.FileName);
        ext = ext.Replace(".", "");
        en.MyFileExt = ext;
        if (fileNameDesc == "" || fileNameDesc == null)
            en.MyFileName = System.IO.Path.GetFileNameWithoutExtension(File1.PostedFile.FileName);
        else
            en.MyFileName = fileNameDesc;
        en.RDT = DataType.CurrentData;
        en.RefVal = myen.PKVal.ToString();
        en.MyFilePath = filePath;
        en.Insert();

        string fileName = filePath + en.OID + "." + en.MyFileExt;
        File1.PostedFile.SaveAs(fileName);

        File1.PostedFile.InputStream.Close();
        File1.PostedFile.InputStream.Dispose();
        File1.Dispose();

        System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
        en.MyFileSize = DataType.PraseToMB(fi.Length);

        if (DataType.IsImgExt(en.MyFileExt))
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            en.MyFileH = img.Height;
            en.MyFileW = img.Width;
            img.Dispose();
        }
        en.WebPath = cfg.FJWebPath + en.OID + "." + en.MyFileExt;
        en.Update();
        return en;
    }

    public void EnList()
    {
        this.Response.Redirect(this.Request.ApplicationPath + "/Comm/UIEns.aspx?EnName=" + this.EnName, true);
    }
    #endregion
}
