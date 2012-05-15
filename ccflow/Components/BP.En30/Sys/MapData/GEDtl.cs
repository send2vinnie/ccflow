
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;

namespace BP.Sys
{
	/// <summary>
	/// ͨ����ϸ��
	/// </summary>
    public class GEDtlAttr : EntityOIDAttr
    {
        public const string RefPK = "RefPK";
        public const string FID = "FID";
        public const string Rec = "Rec";
        public const string RDT = "RDT";
    }
    /// <summary>
    /// ͨ����ϸ��
    /// </summary>
    public class GEDtl : EntityOID
    {
        #region ���캯��
        public override string ToString()
        {
            return this.FK_MapDtl;
        }
        public override string ClassID
        {
            get
            {
                return this.FK_MapDtl;
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(GEDtlAttr.RDT);
            }
            set
            {
                this.SetValByKey(GEDtlAttr.RDT, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(GEDtlAttr.Rec);
            }
            set
            {
                this.SetValByKey(GEDtlAttr.Rec, value);
            }
        }
        /// <summary>
        /// ������PKֵ
        /// </summary>
        public string RefPK
        {
            get
            {
                return this.GetValStringByKey(GEDtlAttr.RefPK);
            }
            set
            {
                this.SetValByKey(GEDtlAttr.RefPK, value);
            }
        }
        /// <summary>
        /// ������PKint
        /// </summary>
        public int RefPKInt
        {
            get
            {
                return this.GetValIntByKey(GEDtlAttr.RefPK);
            }
            set
            {
                this.SetValByKey(GEDtlAttr.RefPK, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GEDtlAttr.FID);
            }
            set
            {
                this.SetValByKey(GEDtlAttr.FID, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_MapDtl = null;
        /// <summary>
        /// ͨ����ϸ��
        /// </summary>
        public GEDtl()
        {
        }
        /// <summary>
        /// ͨ����ϸ��
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        public GEDtl(string fk_mapdtl)
        {
            this.FK_MapDtl = fk_mapdtl;
        }
        /// <summary>
        /// ͨ����ϸ��
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="_oid">OID</param>
        public GEDtl(string fk_mapdtl, int _oid)
        {
            this.FK_MapDtl = fk_mapdtl;
            this.OID = _oid;
        }
        #endregion

        #region Map
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                if (this.FK_MapDtl == null)
                    throw new Exception("û�и�" + this.FK_MapDtl + "ֵ�������ܻ�ȡ����Map��");

                BP.Sys.MapDtl md = new BP.Sys.MapDtl(this.FK_MapDtl);
                this._enMap = md.GenerMap();
                return this._enMap;
            }
        }
        /// <summary>
        /// GEDtls
        /// </summary>
        public override Entities GetNewEntities
        {
            get
            {
                if (this.FK_MapDtl == null)
                    return new GEDtls();

                return new GEDtls(this.FK_MapDtl);
            }
        }
        public bool IsChange(GEDtl dtl)
        {
            Attrs attrs = dtl.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (this.GetValByKey(attr.Key) == dtl.GetValByKey(attr.Key))
                    continue;
                else
                    return true;
            }
            return false;
        }
        /// <summary>
        /// ��¼��
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            // �ж��Ƿ��б仯����Ŀ�������Ƿ�ִ�д��档
            MapAttrs mattrs = new MapAttrs(this.FK_MapDtl);
            bool isC = false;
            foreach (MapAttr mattr in mattrs)
            {
                if (isC)
                    break;

                switch (mattr.KeyOfEn)
                {
                    case "Rec":
                    case "RDT":
                    case "RefPK":
                    case "FID":
                        break;
                    default:
                        if (mattr.IsNum)
                        {
                            string s = this.GetValStrByKey(mattr.KeyOfEn);
                            if (string.IsNullOrEmpty(s))
                            {
                                this.SetValByKey(mattr.KeyOfEn, mattr.DefVal);
                                s = mattr.DefVal.ToString();
                            }

                            if (decimal.Parse(s) == mattr.DefValDecimal)
                                continue;
                            isC = true;
                            break;
                        }
                        else
                        {
                            if (this.GetValStrByKey(mattr.KeyOfEn) == mattr.DefVal)
                                continue;
                            isC = true;
                            break;
                        }
                        break;
                }
            }
            if (isC == false)
                return false;

            this.Rec = BP.Web.WebUser.No;
            this.RDT = DataType.CurrentDataTime;
            return base.beforeInsert();
        }
        #endregion
    }
    /// <summary>
    /// ͨ����ϸ��s
    /// </summary>
    public class GEDtls : EntitiesOID
    {
        #region ���ػ��෽��
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public string FK_MapDtl = null;
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.FK_MapDtl == null)
                    return new GEDtl();
                return new GEDtl(this.FK_MapDtl);
            }
        }
        /// <summary>
        /// ͨ����ϸ��ID
        /// </summary>
        public GEDtls()
        {
        }
        /// <summary>
        /// ͨ����ϸ��ID
        /// </summary>
        /// <param name="fk_mapdtl"></param>
        public GEDtls(string fk_mapdtl)
        {
            this.FK_MapDtl = fk_mapdtl;
        }
        #endregion
    }
}
