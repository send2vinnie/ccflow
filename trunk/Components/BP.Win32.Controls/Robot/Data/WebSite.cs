using System;
using System.Data;
using System.IO;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// 网站
	/// </summary>
    public class WebSiteAttr : EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 日志
        /// </summary>
        public const string Log = "Log";
        /// <summary>
        /// 
        /// </summary>
        public const string Note1 = "Note1";
        /// <summary>
        /// Note
        /// </summary>
        public const string Note2 = "Note2";
        public const string Url = "Url";
        public const string RDT = "RDT";
     //   public const string HostIP = "HostIP";
        public const string HostName = "HostName";

        public const string NumOfPage = "NumOfPage";
        /// <summary>
        /// 
        /// </summary>
        public const string UDTFrom = "UDTFrom";
        public const string UDTTo = "UDTTo";
        public const string S = "S";
        public const string UDT = "UDT";
        public const string IsEnable = "IsEnable";
        public const string FK_WebSiteType = "FK_WebSiteType";
        public const string FK_Encode = "FK_Encode";
        /// <summary>
        /// 速度
        /// </summary>
        public const string Pace = "Pace";
        /// <summary>
        /// 文件个数
        /// </summary>
        public const string NumOfFile = "NumOfFile";
        #endregion
    }
	/// <summary>
	/// 网站
	/// </summary>
    public class WebSite : EntityOID
    {
        #region 基本属性
        public string Log
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Log);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Log, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Name);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Name, value);
            }
        }
        public System.Text.Encoding HisEncode
        {
            get
            {
                return System.Text.Encoding.GetEncoding(this.FK_Encode);
                //  return System.Text.Encoding.GetEncoding(this.FK_Encode);
            }
        }
        public string FK_Encode
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.FK_Encode);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.FK_Encode, value);
            }
        }
        public string UDT
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.UDT);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.UDT, value);
            }
        }
        public int NumOfFile
        {
            get
            {
                return this.GetValIntByKey(WebSiteAttr.NumOfFile);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.NumOfFile, value);
            }
        }
        public int S
        {
            get
            {
                return this.GetValIntByKey(WebSiteAttr.S);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.S, value);
            }
        }
        public string UDTTo
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.UDTTo);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.UDTTo, value);
            }
        }
        /// <summary>
        /// 读取日期
        /// </summary>
        public string UDTFrom
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.UDTFrom);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.UDTFrom, value);
            }
        }
        /// <summary>
        /// NumOfPage
        /// </summary>
        public int NumOfPage
        {
            get
            {
                return this.GetValIntByKey(WebSiteAttr.NumOfPage);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.NumOfPage, value);
            }
        }
      
        public decimal Pace
        {
            get
            {
                return this.GetValDecimalByKey(WebSiteAttr.Pace);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Pace, value);
            }
        }
        /// <summary>
        /// HostName
        /// </summary>
        public string HostName
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.HostName);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.HostName, value);
            }
        }
        /// <summary>
        /// Url
        /// </summary>
        public string Url
        {
            get
            {
                string url = this.GetValStringByKey(WebSiteAttr.Url);
                if (url.ToLower().Contains("http://") == false)
                    url = "http://" + url;

                url = url.Replace("\t", "");
                url = url.Replace("\n", "");
                url = url.Replace("\r", "");
                return url;
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Url, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.RDT);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.RDT, value);
            }
        }
        /// <summary>
        /// 网站标题
        /// </summary>
        public string Note1
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Note1);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Note1, value);
            }
        }
        /// <summary>
        /// Note2
        /// </summary>
        public string Note2
        {
            get
            {
                return this.GetValStringByKey(WebSiteAttr.Note2);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.Note2, value);
            }
        }

        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(WebSiteAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(WebSiteAttr.IsEnable, value);
            }
        }
        #endregion

        #region 构造函数
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// 网站
        /// </summary>		
        public WebSite()
        {

        }
        /// <summary>
        /// 网站
        /// </summary>
        /// <param name="no"></param>
        public WebSite(int no)
            : base(no)
        {
        }
        public WebSite(string hostname)
        {
            this.Retrieve(WebSiteAttr.HostName, hostname);
        }
        /// <summary>
        /// WebSiteMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "RB_WebSite";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "网站";
                map.CodeStruct = "4";
                map.EnType = EnType.App;
                #endregion

                #region 基本属性
                map.AddTBIntPKOID();

                map.AddTBString(WebSiteAttr.Name, null, "网站名称", true, false, 0, 400, 30);
                map.AddTBString(WebSiteAttr.Url, null, "开始点", true, false, 0, 700, 30);
                map.AddTBString(WebSiteAttr.HostName, null, "主机名称", true, false, 0, 700, 30);
                map.AddBoolean(WebSiteAttr.IsEnable, true, "是否可用", true, true);
                map.AddTBDate(WebSiteAttr.RDT, "记录日期", true, true);

                //map.AddTBStringDoc(WebSiteAttr.Note1, null, "备注1", true, false);
                //map.AddTBStringDoc(WebSiteAttr.Note2, null, "备注2", true, false);
                // map.AddTBString(WebSiteAttr.HostIP, null, "主机IP", true, false, 0, 700, 30);


                map.AddTBDate(WebSiteAttr.UDT, null, "读取日期", true, true);
                map.AddTBInt(WebSiteAttr.NumOfPage, 0, "页面数", true, true);
                map.AddTBInt(WebSiteAttr.NumOfFile, 0, "文件数", true, true);
                map.AddTBInt(WebSiteAttr.S, 0, "用时(秒)", true, true);
                map.AddTBDecimal(WebSiteAttr.Pace, 0, "读取速度(s/p)", true, true);

                //map.AddTBString(WebSiteAttr.UDTFrom, null, "读取日期从", true, true, 0, 700, 30);
                //map.AddTBString(WebSiteAttr.UDTTo, null, "到", true, true, 0, 700, 30);
                


                map.AddDDLEntities(WebSiteAttr.FK_WebSiteType, "01", "网站类型", new BP.SE.WebSiteTypes(), true);
                map.AddDDLEntities(WebSiteAttr.FK_Encode, "GB2312", "编码类型", new Encodes(), true);

                map.AddTBString(WebSiteAttr.Log, null, "日志", true, false, 0, 7000, 30);
                #endregion

                RefMethod rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerPageFace";
                rm.Title = "抓取";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerTag";
                rm.Title = "提取标签";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerFile";
                rm.Title = "提取文件";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoGenerFLink";
                rm.Title = "提取外部连接";
                map.AddRefMethod(rm);



                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoRelease";
                rm.Title = "信息发布";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.ClassMethodName = this.ToString() + ".DoAll";
                rm.Title = "执行全部";
                map.AddRefMethod(rm);

                map.AddSearchAttr(WebSiteAttr.FK_WebSiteType);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region 方法
        public string DoAll()
        {
            // 查询全部。
            WebSites ens = new WebSites();
            ens.RetrieveAll();

            int i = 0;
            foreach (WebSite en in ens)
            {
                i++;
                if (en.IsEnable == false)
                    continue;

                try
                {
                    BP.DA.Log.DefaultLogWriteLineInfo("&&&&&&&&&&&&&&&& 开始执行网站：" + en.Name + " 进度：" + i + " / " + ens.Count);

                    // 执行前检查。
                    en.DoBeforeRun();
                    if (en.IsEnable == false)
                        continue;

                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerPage());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerTag());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerFLink());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoGenerFile());
                    BP.DA.Log.DefaultLogWriteLineError(en.DoRelease());
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineError(ex.Message);
                }
            }

            BP.DA.Log.OpenLogDir();
            return "成功执行完成。";
        }
        /// <summary>
        /// 提取文件
        /// </summary>
        /// <returns></returns>
        public string DoGenerFile()
        {
            Pages pgs = new Pages();
            int i = 0;
            try
            {
                DBAccess.RunSQL(" delete RB_PageFile where HostName='" + this.HostName + "'");
                pgs.Retrieve(PageAttr.HostName, this.HostName);
                try
                {
                    foreach (Page pg in pgs)
                    {
                        Hrefs hfs = PubClass.GetHrefs(pg.Url, this.HisEncode);
                        try
                        {
                            i += pg.GenerFiles(hfs, pg.Url);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "@执行DoGenerFile出现错误：" + ex.Message;
            }
            return "执行完毕，执行网页[" + pgs.Count + "]个，找到文件[" + i + "]个。";
        }
        /// <summary>
        /// 外连接
        /// </summary>
        /// <param name="hfs"></param>
        /// <returns></returns>
        public int GenerFLink(Hrefs hfs)
        {
            int num = 0;
            foreach (Href hf in hfs)
            {
                if (hf.IsLocalHost)
                    continue;

                try
                {
                    WebSiteLink wl = new WebSiteLink();
                    wl.HostName = hf.HostName;
                    wl.No = hf.HostNameOfUrl;
                    wl.Name = hf.Lab;
                    wl.Insert();
                    num++;
                }
                catch (Exception ex)
                {
                }
            }

            return num;
        }
        public string DoGenerFLink()
        {
            DBAccess.RunSQL(" delete RB_WebSiteLink where HostName='" + this.HostName + "'");
            Pages pgs = new Pages();
            pgs.Retrieve(PageAttr.HostName, this.HostName);

            int i = 0;
            try
            {
                foreach (Page pg in pgs)
                {
                    Hrefs hfs = PubClass.GetHrefs(pg.Url, this.HisEncode);
                    try
                    {
                        i += this.GenerFLink(hfs);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            string sql = "DELETE RB_WebSiteLink WHERE NO IN (SELECT HOSTNAME FROM RB_WebSite)";
            int num = DBAccess.RunSQL(sql);

            return "执行完毕，执行网页[" + pgs.Count + "]个，找到外部连接[" + i + "]个，被重复项[" + num + "]个。";
        }
        public string DoGenerPage()
        {
            try
            {
                string docs = PubClass.ReadContext(this.Url, this.HisEncode);
                Hrefs hfs = PubClass.GetHrefs(this.Url, this.HisEncode);
                foreach (Href hf in hfs)
                    PubClass.DoPage(hf);

                return "网页成功执行完成。";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DoGenerPageFace()
        {

            Frm.FrmRunWebSiteOne frm = new BP.RB.Frm.FrmRunWebSiteOne();
            frm.HisWebSite = this;
            frm.Text = this.Url + this.Name;
            frm.ShowDialog();
            return null;
        }
        /// <summary>
        /// 产生Tag
        /// </summary>
        /// <returns></returns>
        public string DoGenerTag()
        {
            DBAccess.RunSQL(" delete RB_PageTag where HostName='" + this.HostName + "'");

            Pages pgs = new Pages();
            pgs.Retrieve(PageAttr.HostName, this.HostName);

            Tags tgs = new Tags();
            tgs.RetrieveAll();

            int i = 0;
            try
            {
                foreach (Page pg in pgs)
                    i = i + pg.GenerTag(tgs);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "执行完毕，执行网页[" + pgs.Count + "]个，找到标签[" + i + "]个。";
        }
        /// <summary>
        /// 把文件发布到网站
        /// </summary>
        /// <returns></returns>
        public string DoRelease()
        {
            string msg = "";
            // Release page
            string sql = "delete SE_Page WHERE HOSTNAME='" + this.HostName + "'";
            int count = DBAccess.RunSQL(sql);

            sql = "insert  SE_Page (URL,Name,HostName,docHtml,docText) SELECT No as URL,Name,HostName,docHtml,docText from RB_Page WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            sql = "UPDATE SE_Page SET RDT='" + DataType.CurrentDataCNOfShort + "' WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            msg += "网页发布信息：" + count + "个。\t\n";

            // Release file
            sql = "delete SE_PageFile WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            sql = "INSERT  SE_PageFile (URL,Name,HostName,FDesc,PageUrl) SELECT No as URL,Name,HostName,FDesc,PageUrl FROM RB_PageFile WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);

            sql = "UPDATE SE_PageFile SET RDT='" + DataType.CurrentDataCNOfShort + "' WHERE HOSTNAME='" + this.HostName + "'";
            count = DBAccess.RunSQL(sql);
            msg += "网页发布信息：[" + count + "]个。";

            return msg;
        }
        /// <summary>
        /// 执行前要运行的
        /// </summary>
        public void DoBeforeRun()
        {
            #region 测试连接与主机是否可用
            try
            {
                Uri uri = new Uri(this.Url);
                string path = uri.AbsolutePath;
                path = path.Substring(0, path.LastIndexOf('/') + 1);
                path = path.ToLower();

                string hostName = uri.Authority.ToLower();
                Uri u = new Uri(this.Url);
                this.HostName = u.Authority;
            }
            catch (Exception ex)
            {
                this.Log = "设置主机时出现错误：可能是起点不正确，或者主机连接不上。" + ex.Message;
                this.IsEnable = false;
                this.Update();
            }
            #endregion


            BP.DA.DBAccess.RunSQL("DELETE RB_Page WHERE HostName='" + this.HostName + "'");
            BP.DA.DBAccess.RunSQL("DELETE RB_PageFile WHERE HostName='" + this.HostName + "'");

        }

        protected override bool beforeUpdate()
        {
            try
            {
                if (this.NumOfPage != 0)
                {
                    decimal p = decimal.Parse(this.S.ToString()) / decimal.Parse(this.NumOfPage.ToString());
                    this.Pace = p;
                }
            }
            catch
            {

            }
            return base.beforeUpdate();
        }
        #endregion
    }
	/// <summary>
	/// 网站
	/// </summary>
    public class WebSites : EntitiesOID
    {
        #region 得到它的 Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new WebSite();
            }
        }
        /// <summary>
        /// 获取一个站点
        /// </summary>
        /// <returns></returns>
        public WebSite GetOne()
        {
            string sql = "";
            return null;
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 网站
        /// </summary>
        public WebSites() { }
        #endregion

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            // return this.Retrieve(WebSiteAttr.IsEnable, 1);
            return base.RetrieveAll();
        }
        public static void RunAllSite()
        {
            WebSites ens = new WebSites();
            ens.RetrieveAll();

            foreach (WebSite en in ens)
            {
                try
                {

                    en.DoBeforeRun();
                    if (en.IsEnable == false)
                        continue;

                    Log.DefaultLogWriteLineInfo("================== 开始运行：" + en.Name + "====================");

                    DateTime dt = DateTime.Now;
                    Hrefs hfs = PubClass.GetHrefs(en.Url, en.HisEncode);
                    foreach (Href hf in hfs)
                    {
                        PubClass.DoPage(hf);
                    }

                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts = dt - dt2;
                    en.S = ts.Seconds;
                    en.UDT = dt2.ToString("MM月dd日HH时mm分");

                    en.NumOfPage = DBAccess.RunSQLReturnValInt("select count(*) NUM from RB_Page WHERE HostName='" + en.HostName + "'");
                    en.NumOfFile = DBAccess.RunSQLReturnValInt("select count(*) NUM from RB_PageFile WHERE HostName='" + en.HostName + "'");
                    en.Log = "OK";
                    en.Update();
                }
                catch (Exception ex)
                {
                    en.Log = "抓取网页出现错误：" + ex.Message;
                    en.Update();
                    Log.DefaultLogWriteLineInfo("运行：" + en.Name + "期间出现如下问题：" + ex.Message);
                }
            }
        }
    }
	
}
