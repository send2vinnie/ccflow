using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    public enum FrmFrom
    {
        Flow,
        Node,
        Dtl
    }
	/// <summary>
	/// 映射基础
	/// </summary>
    public class MapDataAttr : EntityNoNameAttr
    {
        public const string PTable = "PTable";
        public const string Dtls = "Dtls";
        public const string EnPK = "EnPK";
        public const string SearchKeys = "SearchKeys";
        //public const string CellsFrom = "CellsFrom";
        public const string FrmW = "FrmW";
        public const string FrmH = "FrmH";
        /// <summary>
        /// 来源
        /// </summary>
        public const string FrmFrom = "FrmFrom";
        /// <summary>
        /// DBURL
        /// </summary>
        public const string DBURL = "DBURL";

        /// <summary>
        /// 设计者
        /// </summary>
        public const string Designer = "Designer";
        /// <summary>
        /// 设计者单位
        /// </summary>
        public const string DesignerUnit = "DesignerUnit";
        /// <summary>
        /// 设计者联系方式
        /// </summary>
        public const string DesignerContext = "DesignerContext";
        /// <summary>
        /// 表单类别
        /// </summary>
        public const string FK_FrmSort = "FK_FrmSort";
    }
	/// <summary>
	/// 映射基础
	/// </summary>
    public class MapData : EntityNoName
    {
        #region 修饰属性
        private FrmLines _HisFrmLines = null;
        public FrmLines FrmLines
        {
            get
            {
                if (_HisFrmLines == null)
                    _HisFrmLines = new FrmLines(this.No);
                return _HisFrmLines;
            }
        }
        private FrmLabs _FrmLabs = null;
        public FrmLabs FrmLabs
        {
            get
            {
                if (_FrmLabs == null)
                    _FrmLabs = new FrmLabs(this.No);
                return _FrmLabs;
            }
        }
        private FrmImgs _FrmImgs = null;
        public FrmImgs FrmImgs
        {
            get
            {
                if (_FrmImgs == null)
                    _FrmImgs = new FrmImgs(this.No);
                return _FrmImgs;
            }
        }
        private FrmAttachments _FrmAttachments = null;
        public FrmAttachments FrmAttachments
        {
            get
            {
                if (_FrmAttachments == null)
                    _FrmAttachments = new FrmAttachments(this.No);
                return _FrmAttachments;
            }
        }

        private FrmImgAths _FrmImgAths = null;
        public FrmImgAths FrmImgAths
        {
            get
            {
                if (_FrmImgAths == null)
                    _FrmImgAths = new FrmImgAths(this.No);
                return _FrmImgAths;
            }
        }

        private FrmRBs _FrmRBs = null;
        public FrmRBs FrmRBs
        {
            get
            {
                if (_FrmRBs == null)
                    _FrmRBs = new FrmRBs(this.No);
                return _FrmRBs;
            }
        }
        #endregion
        public static Boolean IsEditDtlModel
        {
            get
            {
                string s = BP.Web.WebUser.GetSessionByKey("IsEditDtlModel", "0");
                if (s == "0")
                    return false;
                else
                    return true;
            }
            set
            {
                BP.Web.WebUser.SetSessionByKey("IsEditDtlModel", "1");
            }
        }

        #region 属性
        public string PTable
        {
            get
            {
                string s = this.GetValStrByKey(MapDataAttr.PTable);
                if (s == "" || s == null)
                    return this.No;
                return s;
            }
            set
            {
                this.SetValByKey(MapDataAttr.PTable, value);
            }
        }
        public DBUrlType HisDBUrl
        {
            get
            {
                return (DBUrlType)this.GetValIntByKey(MapDataAttr.DBURL);
            }
        }
        public string Dtls
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.Dtls);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Dtls, value);
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string EnPK
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.EnPK);
            }
            set
            {
                this.SetValByKey(MapDataAttr.EnPK, value);
            }
        }
        public string SearchKeys
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.SearchKeys);
            }
            set
            {
                this.SetValByKey(MapDataAttr.SearchKeys, value);
            }
        }
        public float FrmW
        {
            get
            {
                return this.GetValFloatByKey(MapDataAttr.FrmW);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmW, value);
            }
        }
        public float FrmH
        {
            get
            {
                return this.GetValFloatByKey(MapDataAttr.FrmH);
            }
            set
            {
                this.SetValByKey(MapDataAttr.FrmH, value);
            }
        }
        #endregion

        #region 构造方法
        //public MapAttrs GenerHisTableCells
        //{
        //    get
        //    {
        //        //if ( this.CellsFrom == null)
        //        //    return null;
        //        MapAttrs mapAttrs = new MapAttrs(this.No+"T");
        //        if (mapAttrs.Count == 0)
        //        {
        //            for (int i = 1; i < 4; i++)
        //            {
        //                MapAttr attr = new MapAttr();
        //                attr.FK_MapData = this.No + "T";
        //                attr.KeyOfEn = "F" + i.ToString();
        //                attr.Name = "Lab" + i;
        //                attr.MyDataType = BP.DA.DataType.AppString;
        //                attr.UIContralType = UIContralType.TB;
        //                attr.LGType = FieldTypeS.Normal;
        //                attr.UIVisible = true;
        //                attr.UIIsEnable = true;
        //                attr.UIWidth = 30;
        //                attr.UIIsLine = true;
        //                attr.MinLen = 0;
        //                attr.MaxLen = 600;
        //                attr.IDX = i;
        //                attr.Insert();
        //            }
        //            mapAttrs = new MapAttrs(this.No + "T");
        //        }
        //        return mapAttrs;
        //    }
        //}
        public Map GenerHisMap()
        {
            MapAttrs mapAttrs = new MapAttrs(this.No);
            Map map = new Map(this.PTable);

            DBUrl u = new DBUrl(this.HisDBUrl);
            map.EnDBUrl = u;

            map.EnDesc = this.Name;
            map.EnType = EnType.App;
            map.DepositaryOfEntity = Depositary.None;
            map.DepositaryOfMap = Depositary.Application;

            Attrs attrs = new Attrs();
            foreach (MapAttr mapAttr in mapAttrs)
                map.AddAttr(mapAttr.HisAttr);

            if (this.SearchKeys.Contains("@") == true)
            {
                string[] strs = this.SearchKeys.Split('@');
                foreach (string s in strs)
                {
                    if (s == "" || s == null)
                        continue;
                    try
                    {
                        map.AddSearchAttr(s);
                    }
                    catch
                    {
                    }
                }
            }

            // 产生明细表。
            MapDtls dtls = new MapDtls(this.No);
            foreach (MapDtl dtl in dtls)
            {
                BP.Sys.GEDtls dtls1 = new GEDtls(dtl.No);
                map.AddDtl(dtls1, "RefPK");
            }
            return map;
        }
        private GEEntity _HisEn = null;
        public GEEntity HisGEEn
        {
            get
            {
                if (this._HisEn == null)
                    _HisEn = new GEEntity(this.No);
                return _HisEn;
            }
        }
        public static Map GenerHisMap(string no)
        {
            if (SystemConfig.IsDebug)
            {
                MapData md = new MapData(no);
                return md.GenerHisMap();
            }
            else
            {
                Map map = BP.DA.Cash.GetMap(no);
                if (map == null)
                {
                    MapData md = new MapData(no);
                    map = md.GenerHisMap();
                    BP.DA.Cash.SetMap(no, map);
                }
                return map;
            }
        }
        /// <summary>
        /// 映射基础
        /// </summary>
        public MapData()
        {
        }
        public MapData(string mypk)
        {
            this.No = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_MapData");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "映射基础";
                map.EnType = EnType.Sys;
                map.CodeStruct = "4";

                map.AddTBStringPK(MapDataAttr.No, null, "编号", true, false, 1, 20, 20);
                map.AddTBString(MapDataAttr.Name, null, "描述", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.EnPK, null, "实体主键", true, false, 0, 10, 20);
                map.AddTBString(MapDataAttr.SearchKeys, null, "查询键", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.PTable, null, "物理表", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Dtls, null, "明细表", true, false, 0, 500, 20);





                map.AddTBFloat(MapDataAttr.FrmW, 900, "FrmW", true, true);
                map.AddTBFloat(MapDataAttr.FrmH, 1200, "FrmH", true, true);

                map.AddTBInt(MapDataAttr.DBURL, 0, "DBURL", true, false);


                map.AddTBString(MapDataAttr.Designer, null, "设计者", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerUnit, null, "单位", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.DesignerContext, null, "联系方式", true, false, 0, 500, 20);


                // 可以为空这个字段。
                map.AddTBString(MapDataAttr.FK_FrmSort, null, "表单类别", true, false, 0, 500, 20);
                // map.AddTBInt(MapDataAttr.FrmFrom, 0, "来源", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public static void ImpMapData(string fk_mapdata, DataSet ds)
        {
            MapData mdOld = new MapData();
            mdOld.No = fk_mapdata;
            mdOld.Delete();

            string timeKey = DateTime.Now.ToString("yyyyMMddhhmmss");
            foreach (DataTable dt in ds.Tables)
            {
                int idx = 0;
                switch (dt.TableName)
                {
                    case "Sys_MapData":
                        DataRow drMD = dt.Rows[0];
                        MapData md = new MapData();
                        foreach (Attr attr in md.EnMap.Attrs)
                        {
                            try
                            {
                                md.SetValByKey(attr.Key, drMD[attr.Key]);
                            }
                            catch
                            {
                            }
                        }
                        md.No = fk_mapdata;
                        md.Insert();
                        break;
                    case "Sys_FrmBtn":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmBtn en = new FrmBtn();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;

                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "Btn" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmLine":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLine en = new FrmLine();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;

                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "LE" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmLab":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLab en = new FrmLab();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "LB" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmLink":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmLink en = new FrmLink();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "LK" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmImg":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmImg en = new FrmImg();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "Img" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmImgAth":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmImgAth en = new FrmImgAth();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "ImgA" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_FrmRB":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmRB en = new FrmRB();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            try
                            {
                                en.Save();
                            }
                            catch
                            {

                            }
                        }
                        break;
                    case "Sys_FrmAttachment":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            FrmAttachment en = new FrmAttachment();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "Ath" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapM2M":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapM2M en = new MapM2M();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.No = "D" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapFrame":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapFrame en = new MapFrame();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.No = "Fra" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapExt":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapExt en = new MapExt();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.MyPK = "Ext" + timeKey + "_" + idx;
                            en.Insert();
                        }
                        break;
                    case "Sys_MapAttr":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            MapAttr en = new MapAttr();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.FK_MapData = fk_mapdata;
                            en.Insert();
                        }
                        break;
                    case "Sys_GroupField":
                        foreach (DataRow dr in dt.Rows)
                        {
                            idx++;
                            GroupField en = new GroupField();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string val = dr[dc.ColumnName] as string;
                                en.SetValByKey(dc.ColumnName, val);
                            }
                            en.EnName = fk_mapdata;
                            en.OID = 0;
                            en.Insert();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        protected override bool beforeDelete()
        {
            MapAttrs attrs = new MapAttrs();
            attrs.Delete(MapAttrAttr.FK_MapData, this.No);
            try
            {
                BP.DA.DBAccess.RunSQL("DROP TABLE " + this.PTable);
            }
            catch
            {
            }

            string sql = "";
            sql += "@DELETE Sys_FrmLine WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_FrmLab WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_FrmLink WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_FrmImg WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_FrmImgAth WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_FrmRB WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_FrmAttachment WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_MapM2M WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_MapFrame WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_MapExt WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_MapAttr WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE Sys_GroupField WHERE EnName='" + this.No + "'";
            DBAccess.RunSQLs(sql);

            MapDtls dtls = new MapDtls(this.No);
            foreach (MapDtl dtl in dtls)
            {
                dtl.Delete();
            }
            return base.beforeDelete();
        }
        public System.Data.DataSet GenerHisDataSet()
        {
            DataSet ds = new DataSet();
            ds.Namespace = "ccFlowFrm";

            FrmLines lins = new FrmLines(this.No);
            ds.Tables.Add(lins.ToDataTableField("Sys_FrmLine"));

            FrmLabs labs = new FrmLabs(this.No);
            ds.Tables.Add(labs.ToDataTableField("Sys_FrmLab"));

            FrmLinks links = new FrmLinks(this.No);
            ds.Tables.Add(links.ToDataTableField("Sys_FrmLink"));

            MapAttrs attrs = new MapAttrs(this.No);
            ds.Tables.Add(attrs.ToDataTableField("Sys_MapAttr"));

            MapDtls dtls = new MapDtls(this.No);
            ds.Tables.Add(dtls.ToDataTableField("Sys_MapDtl"));

            MapExts exts = new MapExts(this.No);
            ds.Tables.Add(exts.ToDataTableField("Sys_MapExt"));

            MapFrames frms = new MapFrames(this.No);
            ds.Tables.Add(frms.ToDataTableField("Sys_MapFrame"));

            MapM2Ms m2ms = new MapM2Ms(this.No);
            ds.Tables.Add(m2ms.ToDataTableField("Sys_MapM2M"));

            MapDatas mds = new MapDatas();
            mds.AddEntity(this);
            ds.Tables.Add(mds.ToDataTableField("Sys_MapData"));

            FrmAttachments aths = new FrmAttachments(this.No);
            ds.Tables.Add(aths.ToDataTableField("Sys_FrmAttachment"));

            FrmImgs imgs = new FrmImgs(this.No);
            ds.Tables.Add(imgs.ToDataTableField("Sys_FrmImg"));

            FrmRBs rbs = new FrmRBs(this.No);
            ds.Tables.Add(rbs.ToDataTableField("Sys_FrmRB"));

            FrmBtns btns = new FrmBtns(this.No);
            ds.Tables.Add(btns.ToDataTableField("Sys_FrmBtn"));

            GroupFields gfs = new GroupFields(this.No);
            ds.Tables.Add(gfs.ToDataTableField("Sys_GroupField"));
            return ds;
        }
        /// <summary>
        /// 生成自动的ｊｓ程序。
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="attrs"></param>
        /// <param name="attr"></param>
        /// <param name="tbPer"></param>
        /// <returns></returns>
        public static string GenerAutoFull(string pk, MapAttrs attrs, MapAttr attr, string tbPer)
        {
            string left = "\n document.forms[0]." + tbPer + "_TB" + attr.KeyOfEn + "_" + pk + ".value = ";
            string right = attr.AutoFullDoc;
            foreach (MapAttr mattr in attrs)
            {
                right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + tbPer + "_TB_" + mattr.KeyOfEn + "_" + pk + ".value) ");
            }
            return " alert( document.forms[0]." + tbPer + "_TB" + attr.KeyOfEn + "_" + pk + ".value ) ; \t\n " + left + right;
        }
    }
	/// <summary>
	/// 映射基础s
	/// </summary>
    public class MapDatas : EntitiesMyPK
	{		
		#region 构造
        /// <summary>
        /// 映射基础s
        /// </summary>
		public MapDatas()
		{
		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new MapData();
			}
		}
		#endregion
	}
}
