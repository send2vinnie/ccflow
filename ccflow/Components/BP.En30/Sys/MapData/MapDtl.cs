using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// 明细表显示方式
    /// </summary>
    public enum DtlShowModel
    {
        /// <summary>
        /// 表格方式
        /// </summary>
        Table,
        /// <summary>
        /// 卡片方式
        /// </summary>
        Card
    }
    /// <summary>
    /// 棫行处理
    /// </summary>
    public enum WhenOverSize
    {
        /// <summary>
        /// 不处理
        /// </summary>
        None,
        /// <summary>
        /// 增加一行
        /// </summary>
        AddRow,
        /// <summary>
        /// 翻页
        /// </summary>
        TurnPage
    }
    public enum DtlOpenType
    {
        /// <summary>
        /// 对人员开放
        /// </summary>
        ForEmp,
        /// <summary>
        /// 对工作开放
        /// </summary>
        ForWorkID,
        /// <summary>
        /// 对流程开放
        /// </summary>
        ForFID
    }
    /// <summary>
    /// 明细
    /// </summary>
    public class MapDtlAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 主表
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// PTable
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// DtlOpenType
        /// </summary>
        public const string DtlOpenType = "DtlOpenType";
        /// <summary>
        /// 插入表单的位置
        /// </summary>
        public const string RowIdx = "RowIdx";
        public const string RowsOfList = "RowsOfList";
        public const string IsShowSum = "IsShowSum";
        public const string IsShowIdx = "IsShowIdx";
        public const string IsCopyNDData = "IsCopyNDData";
        public const string IsReadonly = "IsReadonly";
        public const string WhenOverSize = "WhenOverSize";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";

        public const string IsDelete = "IsDelete";
        public const string IsInsert = "IsInsert";
        public const string IsUpdate = "IsUpdate";
        public const string IsEnablePass = "IsEnablePass";

        public const string IsShowTitle = "IsShowTitle";
        /// <summary>
        /// 显示格式
        /// </summary>
        public const string DtlShowModel = "DtlShowModel";
    }
    /// <summary>
    /// 明细
    /// </summary>
    public class MapDtl : EntityNoName
    {
        #region 属性
        public DtlShowModel HisDtlShowModel
        {
            get
            {
                return (DtlShowModel)this.GetValIntByKey(MapDtlAttr.DtlShowModel);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.DtlShowModel, (int)value);
            }
        }
       
        public WhenOverSize HisWhenOverSize
        {
            get
            {
                return (WhenOverSize)this.GetValIntByKey(MapDtlAttr.WhenOverSize);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.WhenOverSize, (int)value);
            }
        }
        public bool IsShowSum
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsShowSum);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsShowSum, value);
            }
        }
        public bool IsShowIdx
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsShowIdx);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsShowIdx, value);
            }
        }
        public bool IsReadonly_del
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsReadonly);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsReadonly, value);
            }
        }
        public bool IsShowTitle
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsShowTitle);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsShowTitle, value);
            }
        }
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsDelete, value);
            }
        }
        public bool IsInsert
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsInsert);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsInsert, value);
            }
        }
        public bool IsUpdate
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsUpdate);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsUpdate, value);
            }
        }
        /// <summary>
        /// 是否起用审核连接
        /// </summary>
        public bool IsEnablePass
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsEnablePass);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsEnablePass, value);
            }
        }
        public bool IsCopyNDData
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsCopyNDData);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsCopyNDData, value);
            }
        }

        public bool IsUse = false;
        /// <summary>
        /// 是否检查人员的权限
        /// </summary>
        public DtlOpenType DtlOpenType
        {
            get
            {
                return (DtlOpenType)this.GetValIntByKey(MapDtlAttr.DtlOpenType);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.DtlOpenType, (int)value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapDtlAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.FK_MapData, value);
            }
        }
        public int RowsOfList
        {
            get
            {
                return this.GetValIntByKey(MapDtlAttr.RowsOfList);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.RowsOfList, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(MapDtlAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.RowIdx, value);
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapDtlAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.GroupID, value);
            }
        }
        public string PTable
        {
            get
            {
                string s = this.GetValStrByKey(MapDtlAttr.PTable);
                if (s == "" || s == null)
                {
                    s = this.No;
                    if (s.Substring(0, 1) == "0")
                    {
                        return "T" + this.No;
                    }
                    else
                        return s;
                }
                else
                {
                    if (s.Substring(0, 1) == "0")
                    {
                        return "T" + this.No;
                    }
                    else
                        return s;
                }
            }
            set
            {
                this.SetValByKey(MapDtlAttr.PTable, value);
            }
        }
        #endregion

        #region 构造方法
        public Map GenerMap()
        {
            bool isdebug = SystemConfig.IsDebug;
            if (isdebug == false)
            {
                Map m = BP.DA.Cash.GetMap(this.No);
                if (m != null)
                    return m;
            }

            MapAttrs mapAttrs = new MapAttrs(this.No);
            Map map = new Map(this.PTable);
            map.EnDesc = this.Name;
            map.EnType = EnType.App;
            map.DepositaryOfEntity = Depositary.None;
            map.DepositaryOfMap = Depositary.Application;

            Attrs attrs = new Attrs();
            foreach (MapAttr mapAttr in mapAttrs)
                map.AddAttr(mapAttr.HisAttr);

            BP.DA.Cash.SetMap(this.No, map);
            return map;
        }
        public GEDtl HisGEDtl
        {
            get
            {
                GEDtl dtl = new GEDtl(this.No);
                return dtl;
            }
        }
        /// <summary>
        /// 明细
        /// </summary>
        public MapDtl()
        {
        }
        public MapDtl(string mypk)
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
                Map map = new Map("Sys_MapDtl");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "明细";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(MapDtlAttr.No, null, "编号", true, false, 1, 20, 20);
                map.AddTBString(MapDtlAttr.Name, null, "描述", true, false, 1, 50, 20);
                map.AddTBString(MapDtlAttr.FK_MapData, null, "主表", true, false, 0, 30, 20);
                map.AddTBString(MapDtlAttr.PTable, null, "物理表", true, false, 0, 30, 20);

                map.AddTBInt(MapDtlAttr.RowIdx, 99, "位置", false, false);
                map.AddTBInt(MapDtlAttr.GroupID, 0, "GroupID", false, false);
                map.AddTBInt(MapDtlAttr.RowsOfList, 6, "Rows", false, false);

                map.AddBoolean(MapDtlAttr.IsShowSum, true, "IsShowSum", false, false);
                map.AddBoolean(MapDtlAttr.IsShowIdx, true, "IsShowIdx", false, false);
                map.AddBoolean(MapDtlAttr.IsCopyNDData, true, "IsCopyNDData", false, false);
                map.AddBoolean(MapDtlAttr.IsReadonly, false, "IsReadonly", false, false);
                map.AddBoolean(MapDtlAttr.IsShowTitle, true, "IsShowTitle", false, false);


                map.AddBoolean(MapDtlAttr.IsInsert, true, "IsInsert", false, false);
                map.AddBoolean(MapDtlAttr.IsDelete, true, "IsDelete", false, false);
                map.AddBoolean(MapDtlAttr.IsUpdate, true, "IsUpdate", false, false);


                map.AddBoolean(MapDtlAttr.IsEnablePass, false, "是否启用通过审核功能?", false, false);


                map.AddDDLSysEnum(MapDtlAttr.WhenOverSize, 0, "WhenOverSize", true, true,
                 MapDtlAttr.WhenOverSize, "@0=不处理@1=向下顺增行@2=次页显示");

                map.AddDDLSysEnum(MapDtlAttr.DtlOpenType, 0, "数据开放类型", true, true,
                    MapDtlAttr.DtlOpenType, "@0=操作员@1=工作ID@2=流程ID");

                map.AddTBInt(MapDtlAttr.DtlShowModel, 0, "DtlShowModel", false, false);



                map.AddTBFloat(FrmImgAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmImgAttr.Y, 5, "Y", false, false);
                map.AddTBFloat(FrmImgAttr.H, 150, "H", true, false);
                map.AddTBFloat(FrmImgAttr.W, 200, "W", false, false);

                
                this._enMap = map;
                return this._enMap;
            }
        }
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.X);
            }
        }
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.Y);
            }
        }
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.W);
            }
        }
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.H);
            }
        }
        /// <summary>
        /// 获取个数
        /// </summary>
        /// <param name="fk_val"></param>
        /// <returns></returns>
        public int GetCountByFK(int workID)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("select COUNT(OID) from " + this.PTable + " WHERE WorkID=" + workID);
        }

        public int GetCountByFK(string field, string val)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("select COUNT(OID) from " + this.PTable + " WHERE " + field + "='" + val + "'");
        }
        public int GetCountByFK(string field, Int64 val)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("select COUNT(OID) from " + this.PTable + " WHERE " + field + "=" + val);
        }
        public int GetCountByFK(string f1, Int64 val1, string f2, string val2)
        {
            return BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(OID) from " + this.PTable + " WHERE " + f1 + "=" + val1 + " AND " + f2 + "='" + val2 + "'");
        }
        #endregion

        public void IntMapAttrs()
        {
            MapAttrs attrs = new MapAttrs(this.No);
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            if (attrs.Contains(MapAttrAttr.KeyOfEn, "OID") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;

                attr.KeyOfEn = "OID";
                attr.Name = "主键";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }

            if (attrs.Contains(MapAttrAttr.KeyOfEn, "RefPK") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;
            
                attr.KeyOfEn = "RefPK";
                attr.Name = "关联ID";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }


            if (attrs.Contains(MapAttrAttr.KeyOfEn, "FID") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;
                
                attr.KeyOfEn = "FID";
                attr.Name = "FID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }

            if (attrs.Contains(MapAttrAttr.KeyOfEn, "RDT") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.UnDel;

                attr.KeyOfEn = "RDT";
                attr.Name = "记录时间";
                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Tag = "1";
                attr.Insert();
            }

            if (attrs.Contains(MapAttrAttr.KeyOfEn, "Rec") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;
             
                attr.KeyOfEn = "Rec";
                attr.Name = "记录人";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MaxLen = 20;
                attr.MinLen = 0;
                attr.DefVal = "@WebUser.No";
                attr.Tag = "@WebUser.No";
                attr.Insert();
            }
        }
        protected override bool beforeUpdate()
        {
            MapAttrs attrs = new MapAttrs(this.No);
            bool isHaveEnable = false;
            foreach (MapAttr attr in attrs)
            {
                if (attr.UIIsEnable && attr.UIContralType == UIContralType.TB)
                    isHaveEnable = true;
            }
            // this.IsReadonly = !isHaveEnable;
            return base.beforeUpdate();
        }
        protected override bool beforeDelete()
        {
            try
            {
                BP.DA.DBAccess.RunSQL("DELETE Sys_MapAttr WHERE FK_MapData='" + this.No + "'");
                BP.DA.DBAccess.RunSQL("DELETE Sys_MapExt WHERE FK_MapData='" + this.No + "'");
                BP.DA.DBAccess.RunSQL("DROP TABLE " + this.PTable);
            }
            catch
            {
            }
            return base.beforeDelete();
        }
    }
    /// <summary>
    /// 明细s
    /// </summary>
    public class MapDtls : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 明细s
        /// </summary>
        public MapDtls()
        {
        }
        /// <summary>
        /// 明细s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapDtls(string fk_mapdata)
        {
            this.Retrieve(MapDtlAttr.FK_MapData, fk_mapdata, MapDtlAttr.GroupID);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapDtl();
            }
        }
        #endregion
    }
}
