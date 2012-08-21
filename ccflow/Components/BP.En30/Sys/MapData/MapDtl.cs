using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ��ϸ����ʾ��ʽ
    /// </summary>
    public enum DtlShowModel
    {
        /// <summary>
        /// ���ʽ
        /// </summary>
        Table,
        /// <summary>
        /// ��Ƭ��ʽ
        /// </summary>
        Card
    }
    /// <summary>
    /// ���д���
    /// </summary>
    public enum WhenOverSize
    {
        /// <summary>
        /// ������
        /// </summary>
        None,
        /// <summary>
        /// ����һ��
        /// </summary>
        AddRow,
        /// <summary>
        /// ��ҳ
        /// </summary>
        TurnPage
    }
    public enum DtlOpenType
    {
        /// <summary>
        /// ����Ա����
        /// </summary>
        ForEmp,
        /// <summary>
        /// �Թ�������
        /// </summary>
        ForWorkID,
        /// <summary>
        /// �����̿���
        /// </summary>
        ForFID
    }
    /// <summary>
    /// ��ϸ
    /// </summary>
    public class MapDtlAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
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
        /// �������λ��
        /// </summary>
        public const string RowIdx = "RowIdx";
        public const string RowsOfList = "RowsOfList";
        public const string IsShowSum = "IsShowSum";
        public const string IsShowIdx = "IsShowIdx";
        public const string IsCopyNDData = "IsCopyNDData";
        public const string IsReadonly = "IsReadonly";
        /// <summary>
        /// WhenOverSize
        /// </summary>
        public const string WhenOverSize = "WhenOverSize";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";

        public const string IsDelete = "IsDelete";
        public const string IsInsert = "IsInsert";
        public const string IsUpdate = "IsUpdate";
        public const string IsEnablePass = "IsEnablePass";
        /// <summary>
        /// �Ƿ��Ǻ�����������
        /// </summary>
        public const string IsHLDtl = "IsHLDtl";
        /// <summary>
        /// �Ƿ���ʾtitlt
        /// </summary>
        public const string IsShowTitle = "IsShowTitle";
        /// <summary>
        /// ��ʾ��ʽ
        /// </summary>
        public const string DtlShowModel = "DtlShowModel";
        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        public const string IsView = "IsView";
        public const string X = "X";
        public const string Y = "Y";
        public const string H = "H";
        public const string W = "W";
        public const string FrmW = "FrmW";
        public const string FrmH = "FrmH";
        /// <summary>
        /// �Ƿ���Ե���
        /// </summary>
        public const string IsExp = "IsExp";
        /// <summary>
        /// �Ƿ���Ե��룿
        /// </summary>
        public const string IsImp = "IsImp";
        /// <summary>
        /// �Ƿ����ö฽��
        /// </summary>
        public const string IsEnableAthM = "IsEnableAthM";
        /// <summary>
        /// IsEnableM2M
        /// </summary>
        public const string IsEnableM2M = "IsEnableM2M";
        /// <summary>
        /// IsEnableM2MM
        /// </summary>
        public const string IsEnableM2MM = "IsEnableM2MM";
        /// <summary>
        /// ���ͷ��
        /// </summary>
        public const string MTR = "MTR";
    }
    /// <summary>
    /// ��ϸ
    /// </summary>
    public class MapDtl : EntityNoName
    {
        #region �������
        /// <summary>
        /// ��ʼ���������
        /// </summary>
        protected override void InitRefObjects()
        {
            this.SetRefObject("FrmLines", new FrmLines(this.No));
            this.SetRefObject("FrmLabs", new FrmLabs(this.No));
            this.SetRefObject("FrmImgs", new FrmImgs(this.No));
            this.SetRefObject("FrmAttachments", new FrmAttachments(this.No));
            this.SetRefObject("FrmImgAths", new FrmImgAths(this.No));
            this.SetRefObject("FrmRBs", new FrmRBs(this.No));
            this.SetRefObject("MapAttrs", new MapAttrs(this.No));
            this.SetRefObject("FrmEles", new FrmEles(this.No));
            this.SetRefObject("FrmBtns", new FrmBtns(this.No));
            this.SetRefObject("FrmLinks", new FrmLinks(this.No));
            this.SetRefObject("MapM2Ms", new MapM2Ms(this.No));
         //   this.SetRefObject("MapDtls", new MapDtls(this.No));
            this.SetRefObject("FrmEvents", new FrmEvents(this.No));
            this.SetRefObject("MapExts", new MapExts(this.No));
            this.SetRefObject("GroupFields", new GroupFields(this.No));
            this.SetRefObject("MapFrames", new MapFrames(this.No));
            base.InitRefObjects();
        }
        public MapFrames MapFrames
        {
            get
            {
                return this.GetRefObject("MapFrames") as MapFrames;
            }
        }
        public GroupFields GroupFields
        {
            get
            {
                return this.GetRefObject("GroupFields") as GroupFields;
            }
        }
        public MapExts MapExts
        {
            get
            {
                return this.GetRefObject("MapExts") as MapExts;
            }
        }
        public FrmEvents FrmEvents
        {
            get
            {
                return this.GetRefObject("FrmEvents") as FrmEvents;
            }
        }
        public MapM2Ms MapM2Ms
        {
            get
            {
                return this.GetRefObject("MapM2Ms") as MapM2Ms;
            }
        }
      
        public FrmLinks FrmLinks
        {
            get
            {
                return this.GetRefObject("FrmLinks") as FrmLinks;
            }
        }
        public FrmBtns FrmBtns
        {
            get
            {
                return this.GetRefObject("FrmBtns") as FrmBtns;
            }
        }
        public FrmEles FrmEles
        {
            get
            {
                return this.GetRefObject("FrmEles") as FrmEles;
            }
        }
        public FrmLines FrmLines
        {
            get
            {
                return this.GetRefObject("FrmLines") as FrmLines;
            }
        }
        public FrmLabs FrmLabs
        {
            get
            {
                return this.GetRefObject("FrmLabs") as FrmLabs;
            }
        }
        public FrmImgs FrmImgs
        {
            get
            {
                return this.GetRefObject("FrmImgs") as FrmImgs;
            }
        }
        public FrmAttachments FrmAttachments
        {
            get
            {
                return this.GetRefObject("FrmAttachments") as FrmAttachments;
            }
        }
        public FrmImgAths FrmImgAths
        {
            get
            {
                return this.GetRefObject("FrmImgAths") as FrmImgAths;
            }
        }
        public FrmRBs FrmRBs
        {
            get
            {
                return this.GetRefObject("FrmRBs") as FrmRBs;
            }
        }
        public MapAttrs MapAttrs
        {
            get
            {
                return this.GetRefObject("MapAttrs") as MapAttrs;
            }
        }
        #endregion

        #region ����
        public GEDtls HisGEDtls_temp = null;
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
        /// <summary>
        /// 
        /// </summary>
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
        public bool IsExp
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsExp);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsExp, value);
            }
        }
        public bool IsImp
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsImp);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsImp, value);
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
        /// <summary>
        /// �Ƿ��Ǻ�����������
        /// </summary>
        public bool IsHLDtl
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsHLDtl);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsHLDtl, value);
            }
        }
        public int _IsReadonly = 2;
        public bool IsReadonly
        {
            get
            {
                if (_IsReadonly != 2)
                {
                    if (_IsReadonly == 1)
                        return true;
                    else
                        return false;
                }

                if (this.IsDelete || this.IsInsert || this.IsUpdate)
                {
                    _IsReadonly = 0;
                    return false;
                }
                _IsReadonly = 1;
                return true;
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
        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        public bool IsView
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsView);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsView, value);
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
        /// �Ƿ����ö฽��
        /// </summary>
        public bool IsEnableAthM
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsEnableAthM);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsEnableAthM, value);
            }
        }

        /// <summary>
        /// �Ƿ������������
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
        /// <summary>
        /// �Ƿ�����һ�Զ�
        /// </summary>
        public bool IsEnableM2M
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsEnableM2M);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsEnableM2M, value);
            }
        }
        /// <summary>
        /// �Ƿ�����һ�Զ��
        /// </summary>
        public bool IsEnableM2MM
        {
            get
            {
                return this.GetValBooleanByKey(MapDtlAttr.IsEnableM2MM);
            }
            set
            {
                this.SetValByKey(MapDtlAttr.IsEnableM2MM, value);
            }
        }

        public bool IsUse = false;
        /// <summary>
        /// �Ƿ�����Ա��Ȩ��
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
        /// <summary>
        /// ���ͷ
        /// </summary>
        public string MTR
        {
            get
            {
                string s= this.GetValStrByKey(MapDtlAttr.MTR);
                s = s.Replace("��","<");
                s = s.Replace( "��",">");
                s = s.Replace("��","'");
                return s;
            }
            set
            {
                string s = value;
                s = s.Replace("<","��");
                s = s.Replace(">", "��");
                s = s.Replace("'", "��");
                this.SetValByKey(MapDtlAttr.MTR, value);
            }
        }
        #endregion

        #region ���췽��
        public Map GenerMap()
        {
            bool isdebug = SystemConfig.IsDebug;

            if (isdebug == false)
            {
                Map m = BP.DA.Cash.GetMap(this.No);
                if (m != null)
                    return m;
            }

            MapAttrs mapAttrs = this.MapAttrs;
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
        public GEEntity GenerGEMainEntity(string mainPK)
        {
            GEEntity en = new GEEntity(this.FK_MapData, mainPK);
            return en;
        }
        /// <summary>
        /// ��ϸ
        /// </summary>
        public MapDtl()
        {
        }
        public MapDtl(string mypk)
        {
            this.No = mypk;
            this._IsReadonly = 2;
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
                map.EnDesc = "��ϸ";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(MapDtlAttr.No, null, "���", true, false, 1, 20, 20);
                map.AddTBString(MapDtlAttr.Name, null, "����", true, false, 1, 50, 20);
                map.AddTBString(MapDtlAttr.FK_MapData, null, "����", true, false, 0, 30, 20);
                map.AddTBString(MapDtlAttr.PTable, null, "�����", true, false, 0, 30, 20);

                map.AddTBInt(MapDtlAttr.RowIdx, 99, "λ��", false, false);
                map.AddTBInt(MapDtlAttr.GroupID, 0, "GroupID", false, false);
                map.AddTBInt(MapDtlAttr.RowsOfList, 6, "Rows", false, false);

                map.AddBoolean(MapDtlAttr.IsShowSum, true, "IsShowSum", false, false);
                map.AddBoolean(MapDtlAttr.IsShowIdx, true, "IsShowIdx", false, false);
                map.AddBoolean(MapDtlAttr.IsCopyNDData, true, "IsCopyNDData", false, false);
                map.AddBoolean(MapDtlAttr.IsHLDtl, false, "�Ƿ��Ǻ�������", false, false);

                map.AddBoolean(MapDtlAttr.IsReadonly, false, "IsReadonly", false, false);
                map.AddBoolean(MapDtlAttr.IsShowTitle, true, "IsShowTitle", false, false);
                map.AddBoolean(MapDtlAttr.IsView, true, "�Ƿ�ɼ�", false, false);

                map.AddBoolean(MapDtlAttr.IsExp, true, "IsExp", false, false);
                map.AddBoolean(MapDtlAttr.IsImp, true, "IsImp", false, false);

                map.AddBoolean(MapDtlAttr.IsInsert, true, "IsInsert", false, false);
                map.AddBoolean(MapDtlAttr.IsDelete, true, "IsDelete", false, false);
                map.AddBoolean(MapDtlAttr.IsUpdate, true, "IsUpdate", false, false);

                map.AddBoolean(MapDtlAttr.IsEnablePass, false, "�Ƿ�����ͨ����˹���?", false, false);
                map.AddBoolean(MapDtlAttr.IsEnableAthM, false, "�Ƿ����ö฽��", false, false);

                map.AddBoolean(MapDtlAttr.IsEnableM2M, false, "�Ƿ�����M2M", false, false);
                map.AddBoolean(MapDtlAttr.IsEnableM2MM, false, "�Ƿ�����M2M", false, false);

                map.AddDDLSysEnum(MapDtlAttr.WhenOverSize, 0, "WhenOverSize", true, true,
                 MapDtlAttr.WhenOverSize, "@0=������@1=����˳����@2=��ҳ��ʾ");

                map.AddDDLSysEnum(MapDtlAttr.DtlOpenType, 1, "���ݿ�������", true, true,
                    MapDtlAttr.DtlOpenType, "@0=����Ա@1=����ID@2=����ID");

                map.AddDDLSysEnum(MapDtlAttr.DtlShowModel, 0, "��ʾ��ʽ", true, true,
               MapDtlAttr.DtlShowModel, "@0=���@1=��Ƭ");

                map.AddTBFloat(MapDtlAttr.X, 5, "X", true, false);
                map.AddTBFloat(MapDtlAttr.Y, 5, "Y", false, false);

                map.AddTBFloat(MapDtlAttr.H, 150, "H", true, false);
                map.AddTBFloat(MapDtlAttr.W, 200, "W", false, false);

                map.AddTBFloat(MapDtlAttr.FrmW, 900, "FrmW", true, true);
                map.AddTBFloat(MapDtlAttr.FrmH, 1200, "FrmH", true, true);

                //MTR ���ͷ��.
                map.AddTBString(MapDtlAttr.MTR, null, "���ͷ��", true, false, 0, 3000, 20);


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
        public float FrmW
        {
            get
            {
                return this.GetValFloatByKey(MapDtlAttr.FrmW);
            }
        }
        public float FrmH
        {
            get
            {
                return this.GetValFloatByKey(MapDtlAttr.FrmH);
            }
        }
        /// <summary>
        /// ��ȡ����
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
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = this.No;
            if (md.RetrieveFromDBSources() == 0)
            {
                md.Name = this.Name;
                md.Insert();
            }

            MapAttrs attrs = new MapAttrs(this.No);
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            if (attrs.Contains(MapAttrAttr.KeyOfEn, "OID") == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = this.No;
                attr.HisEditType = EditType.Readonly;

                attr.KeyOfEn = "OID";
                attr.Name = "����";
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
                attr.Name = "����ID";
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
                attr.Name = "��¼ʱ��";
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
                attr.Name = "��¼��";
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
        private void InitExtMembers()
        {
            /* ��������˶฽��*/
            if (this.IsEnableAthM)
            {
                BP.Sys.FrmAttachment athDesc = new BP.Sys.FrmAttachment();
                athDesc.MyPK = this.No + "_AthM";
                if (athDesc.RetrieveFromDBSources() == 0)
                {
                    athDesc.FK_MapData = this.No;
                    athDesc.NoOfObj = "AthM";
                    athDesc.Name = this.Name;
                    athDesc.Insert();
                }
            }

            if (this.IsEnableM2M)
            {
                MapM2M m2m = new MapM2M();
                m2m.MyPK = this.No + "_M2M";
                m2m.Name = "M2M";
                m2m.NoOfObj = "M2M";
                m2m.FK_MapData = this.No;
                if (m2m.RetrieveFromDBSources() == 0)
                {
                    m2m.FK_MapData = this.No;
                    m2m.NoOfObj = "M2M";
                    m2m.Insert();
                }
            }

            if (this.IsEnableM2MM)
            {
                MapM2M m2m = new MapM2M();
                m2m.MyPK = this.No + "_M2MM";
                m2m.Name = "M2MM";
                m2m.NoOfObj = "M2MM";
                m2m.FK_MapData = this.No;
                if (m2m.RetrieveFromDBSources() == 0)
                {
                    m2m.FK_MapData = this.No;
                    m2m.NoOfObj = "M2MM";
                    m2m.Insert();
                }
            }
        }
        protected override bool beforeInsert()
        {
            this.InitExtMembers();
            return base.beforeInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            if (this.IsEnablePass)
            {
                /*�ж��Ƿ���IsPass �ֶΡ�*/
                MapAttrs attrs = new MapAttrs(this.No);
                if (attrs.Contains(MapAttrAttr.KeyOfEn, "IsPass") == false)
                    throw new Exception("����������ϸ��(" + this.Name + ")���������ѡ����Ǹ���ϸ����ûIsPass�ֶΣ���ο������ĵ���");
            }
            return base.beforeUpdateInsertAction();
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
            this.InitExtMembers();
            return base.beforeUpdate();
        }
        protected override bool beforeDelete()
        {
            string sql = "";
            sql += "@DELETE FROM Sys_FrmLine WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmLab WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmLink WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmImg WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmImgAth WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmRB WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_FrmAttachment WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapFrame WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapExt WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapAttr WHERE FK_MapData='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapData WHERE No='" + this.No + "'";
            sql += "@DELETE FROM Sys_GroupField WHERE EnName='" + this.No + "'";
            sql += "@DELETE FROM Sys_MapM2M WHERE FK_MapData='" + this.No + "'";
            DBAccess.RunSQLs(sql);
            try
            {
                BP.DA.DBAccess.RunSQL("DROP TABLE " + this.PTable);
            }
            catch
            {
            }
            return base.beforeDelete();
        }
    }
    /// <summary>
    /// ��ϸs
    /// </summary>
    public class MapDtls : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ��ϸs
        /// </summary>
        public MapDtls()
        {
        }
        /// <summary>
        /// ��ϸs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapDtls(string fk_mapdata)
        {
            this.Retrieve(MapDtlAttr.FK_MapData, fk_mapdata, MapDtlAttr.No);
        }
        /// <summary>
        /// �õ����� Entity
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
