using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    public enum SFTableType
    {
        /// <summary>
        /// �Զ����
        /// </summary>
        SFTable,
        /// <summary>
        /// ���
        /// </summary>
        ClsLab,
        /// <summary>
        /// ϵͳ��
        /// </summary>
        SysTable
    }
	/// <summary>
	/// �û��Զ����
	/// </summary>
    public class SFTableAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public const string IsDel = "IsDel";
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string FK_Val = "FK_Val";
        /// <summary>
        /// ����
        /// </summary>
        public const string SFTableType = "SFTableType";
        /// <summary>
        /// ����
        /// </summary>
        public const string TableDesc = "TableDesc";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public const string DefVal = "DefVal";
        /// <summary>
        /// IsEdit
        /// </summary>
        public const string IsEdit = "IsEdit";

    }
	/// <summary>
	/// �û��Զ����
	/// </summary>
    public class SFTable : EntityNoName
    {
        #region ����
        public bool IsEdit
        {
            get
            {
                return this.GetValBooleanByKey(SFTableAttr.IsEdit);
            }
            set
            {
                this.SetValByKey(SFTableAttr.IsEdit, value);
            }
        }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsClass
        {
            get
            {
                if (this.No.Contains("."))
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// ֵ
        /// </summary>
        public string FK_Val
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.FK_Val);
            }
            set
            {
                this.SetValByKey(SFTableAttr.FK_Val, value);
            }
        }
        public string TableDesc
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.TableDesc);
            }
            set
            {
                this.SetValByKey(SFTableAttr.TableDesc, value);
            }
        }
        public string DefVal
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.DefVal);
            }
            set
            {
                this.SetValByKey(SFTableAttr.DefVal, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string SFTableTypeT
        {
            get
            {
                return this.GetValRefTextByKey(SFTableAttr.SFTableType);
            }
        }
        public SFTableType HisSFTableType
        {
            get
            {
                return (SFTableType)this.GetValIntByKey(SFTableAttr.SFTableType);
            }
            set
            {
                this.SetValByKey(SFTableAttr.SFTableType, (int)value);
            }
        }
        public bool IsDel
        {
            get
            {
                if (this.HisSFTableType== SFTableType.SFTable )
                    return true;
                else
                    return false;
            }
        }
        public EntitiesNoName HisEns
        {
            get
            {
                if (this.IsClass)
                {
                    EntitiesNoName ens = (EntitiesNoName)BP.DA.ClassFactory.GetEns(this.No);
                    ens.RetrieveAll();
                    return ens;
                }

                BP.En.GENoNames ges = new GENoNames(this.No, this.Name);
                ges.RetrieveAll();
                return ges;
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �û��Զ����
        /// </summary>
        public SFTable()
        {

        }
        public SFTable(string mypk)
        {
            this.No = mypk;
            try
            {
                this.Retrieve();
            }
            catch (Exception ex)
            {
                switch (this.No)
                {
                    case "BP.Pub.NYs":
                        this.Name = "����";
                        this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_NY";
                        this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.YFs":
                        this.Name = "��";
                        this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_YF";
                        this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.Days":
                        this.Name = "��";
                        this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_Day";
                        this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.NDs":
                        this.Name = "��";
                        this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_ND";
                        this.IsEdit = true;
                        this.Insert();
                        break;
                    default:
                        throw new Exception(ex.Message);
                }
            }
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
                Map map = new Map("Sys_SFTable");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "�û��Զ����";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(SFTableAttr.No, null, "���", true, false, 1, 20, 20);
                map.AddTBString(SFTableAttr.Name, null, "������", true, false, 0, 30, 20);
                map.AddTBString(SFTableAttr.FK_Val, null, "�ֶΣ���ʾ�������", true, false, 0, 50, 20);
                map.AddDDLSysEnum(SFTableAttr.SFTableType, 0, "������", true, false, SFTableAttr.SFTableType, "@0=�û�����@1=���@2=ϵͳ��");
                map.AddTBString(SFTableAttr.TableDesc, null, "������", true, false, 0, 50, 20);
                map.AddTBString(SFTableAttr.DefVal, null, "Ĭ��ֵ(����)", true, false, 0, 200, 20);
                map.AddBoolean(SFTableAttr.IsEdit, true, "�Ƿ�ɱ༭", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeDelete()
        {
            //MapAttrs attrs = new MapAttrs(this.No);
            //attrs.Delete();
            return base.beforeDelete();
        }
    }
	/// <summary>
	/// �û��Զ����s
	/// </summary>
    public class SFTables : EntitiesNoName
	{		
		#region ����
        /// <summary>
        /// �û��Զ����s
        /// </summary>
		public SFTables()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SFTable();
			}
		}
		#endregion
	}
}
