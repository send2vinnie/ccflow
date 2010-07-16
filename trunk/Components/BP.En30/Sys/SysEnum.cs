using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// sss
	/// </summary>
	public class SysEnumAttr 
	{
		/// <summary>
		/// 标题  
		/// </summary>
		public const string Lab="Lab";
		/// <summary>
		/// Int key
		/// </summary>
		public const string IntKey="IntKey";
		/// <summary>
		/// EnumKey
		/// </summary>
		public const string EnumKey="EnumKey";
		/// <summary>
		/// Language
		/// </summary>
		public const string FK_Language="FK_Language";
		/// <summary>
		/// 风格
		/// </summary>
		public const string Style="Style";
	}
	/// <summary>
	/// SysEnum
	/// </summary>
	public class SysEnum : Entity 
	{
		/// <summary>
		/// 得到一个String By LabKey.
		/// </summary>
		/// <param name="EnumKey"></param>
		/// <param name="intKey"></param>
		/// <returns></returns>
		public static string GetLabByPK(string EnumKey, int intKey)
		{
			SysEnum en = new SysEnum(EnumKey,intKey);
			return en.Lab;  
		}

		#region 实现基本的方方法
		/// <summary>
		/// 标签
		/// </summary>
		public  string  Lab
		{
			get
			{
			  return this.GetValStringByKey(SysEnumAttr.Lab);
			}
			set
			{
				this.SetValByKey(SysEnumAttr.Lab,value);
			}
		}
		/// <summary>
		/// 标签
		/// </summary>
		public  string  FK_Language
		{
			get
			{
				return this.GetValStringByKey(SysEnumAttr.FK_Language);
			}
			set
			{
				this.SetValByKey(SysEnumAttr.FK_Language,value);
			}
		}
		/// <summary>
		/// Int val
		/// </summary>
		public  int  IntKey
		{
			get
			{
				return this.GetValIntByKey(SysEnumAttr.IntKey);
			}
			set
			{
				this.SetValByKey(SysEnumAttr.IntKey,value);
			}
		}
		/// <summary>
		/// EnumKey
		/// </summary>
		public  string  EnumKey
		{
			get
			{
				return this.GetValStringByKey(SysEnumAttr.EnumKey);
			}
			set
			{
				this.SetValByKey(SysEnumAttr.EnumKey,value);
			}
		}
		/// <summary>
		/// 风格
		/// </summary>
		public  string  Style
		{
			get
			{
				return this.GetValStringByKey(SysEnumAttr.Style);
			}
			set
			{
				this.SetValByKey(SysEnumAttr.Style,value);
			}
		}
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// SysEnum
		/// </summary>
		public SysEnum(){}
		/// <summary>
		/// 税务编号
		/// </summary>
		/// <param name="_No">编号</param>
		public SysEnum(string EnumKey ,int val)
		{
			this.EnumKey =EnumKey ;
			this.FK_Language="CN";
			this.IntKey =val ; 
			this.Retrieve();
		}
	 
		public SysEnum(string EnumKey , string fk_language, int val)
		{
			this.EnumKey =EnumKey ;
			this.FK_Language=fk_language;
			this.IntKey =val;
			this.Retrieve();
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_Enum");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "枚举";
                map.EnType = EnType.Sys;
                //map.PhysicsTable;
                map.AddTBString(SysEnumAttr.Lab, null, "Lab", true, false, 1, 80, 8);
                map.AddTBStringPK(SysEnumAttr.EnumKey, null, "EnumKey", true, false, 1, 40, 8);
                //map.AddTBStringPK(SysEnumAttr.FK_Language,"CN","FK_Language",true,false,0,8,8);
                map.AddTBIntPK(SysEnumAttr.IntKey, 0, "Val", true, false);
                map.AddTBString(SysEnumAttr.Style, null, "Style", true, false, 0, 40, 8);
                this._enMap = map;
                return this._enMap;
            }
		}		 
		#endregion 
	}
	/// <summary>
	/// 纳税人集合 
	/// </summary>
	public class SysEnums : Entities
	{
        /// <summary>
        /// 此枚举类型的个数
        /// </summary>
        public int Num = -1;

        public string ToDesc()
        {
            string strs = "";
            foreach (SysEnum se in this)
            {
                strs += se.IntKey + " " + se.Lab + ";";
            }
            return strs;
        }
        public string GenerCaseWhenForOracle(string enName, string mTable, string key, string field, string enumKey, int def)
        {
            string sql = (string)Cash.GetObjFormApplication("ESQL" + enName + key + "_" + enumKey, null);
            // string sql = "";
            if (sql != null)
                return sql;

            sql = " CASE " + mTable + field;
            foreach (SysEnum se1 in this)
            {
                sql += " WHEN " + se1.IntKey + " THEN '" + se1.Lab + "'";
            }

            SysEnum se = (SysEnum)this.GetEntityByKey(SysEnumAttr.IntKey, def);

            if (se == null)
                sql += " END " + key + "Text";
            else
                sql += " WHEN NULL THEN '" + se.Lab + "' END " + key + "TEXT";

            Cash.AddObj("ESQL" + enName + key + "_" + enumKey, Depositary.Application, sql);
            return sql;
        }

        public string GenerCaseWhenForOracle(string mTable, string key, string field, string enumKey, int def)
        {
            string sql = "";
            sql = " CASE " + mTable + field;
            foreach (SysEnum se1 in this)
            {
                sql += " WHEN " + se1.IntKey + " THEN '" + se1.Lab + "'";
            }

            SysEnum se = (SysEnum)this.GetEntityByKey(SysEnumAttr.IntKey, def);

            if (se == null)
                sql += " END " + key + "Text";
            else
                sql += " WHEN NULL THEN '" + se.Lab + "' END " + key + "TEXT";

            // Cash.AddObj("ESQL" + enName + key + "_" + enumKey, Depositary.Application, sql);
            return sql;
        }
		/// <summary>
		/// SysEnums
		/// </summary>
		/// <param name="EnumKey"></param>
        public SysEnums(string EnumKey)
        {
            if (this.Full(EnumKey) == false)
                throw new Exception("@你没有预制[" + EnumKey + "]枚举值。");
        }
        public SysEnums(string EnumKey, string vals)
        {
            if (this.Full(EnumKey) == false)
            {
                if (vals == null || vals == "")
                    throw new Exception("@你没有预制[" + EnumKey + "]枚举值。");
                else
                {
                    this.RegIt(EnumKey, vals);
                }
            }
        }
        public void RegIt(string EnumKey, string vals)
        {
            try
            {
                string[] strs = vals.Split('@');
                SysEnums ens = new SysEnums();
                ens.Delete(SysEnumAttr.EnumKey, EnumKey);
                foreach (string s in strs)
                {
                    if (s == "" || s == null)
                        continue;

                    string[] vk = s.Split('=');
                    SysEnum se = new SysEnum();
                    se.IntKey = int.Parse(vk[0]);
                    se.Lab = vk[1];
                    se.EnumKey = EnumKey;
                    se.Insert();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + vals);
            }

            this.Full(EnumKey);
        }
        public bool Full(string EnumKey)
        {
            Entities ens = (Entities)Cash.GetObjFormApplication("EnumOf" + EnumKey, null);
            if (ens != null)
            {
                this.AddEntities(ens);
                return true;
            }
            else
            {
                QueryObject qo = new QueryObject(this);
                qo.AddWhere(SysEnumAttr.EnumKey, EnumKey);
                qo.addOrderBy(SysEnumAttr.IntKey);
                if (qo.DoQuery() == 0)
                {
                    BP.Sys.Xml.EnumInfoXml xml = new BP.Sys.Xml.EnumInfoXml(EnumKey);
                    if (xml.Vals.Length == 0)
                        return false;
                    else
                        this.RegIt(EnumKey, xml.Vals);
                }

                Cash.AddObj("EnumOf" + EnumKey, Depositary.Application, this);
                return true;
            }
        }
		/// <summary>
		/// DBSimpleNoNames
		/// </summary>
		/// <returns></returns>
        public DBSimpleNoNames ToEntitiesNoName()
        {
            DBSimpleNoNames ens = new DBSimpleNoNames();
            foreach (SysEnum en in this)
            {
                ens.AddByNoName(en.IntKey.ToString(), en.Lab);
            }
            return ens;
        }
		/// <summary>
		/// SysEnums
		/// </summary>
		public SysEnums(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysEnum();
			}
		}
		/// <summary>
		/// 通过int 得到Lab
		/// </summary>
		/// <param name="val">val</param>
		/// <returns>string val</returns>
		public string GetLabByVal(int val)
		{
			foreach(SysEnum en in this)
			{
				if (en.IntKey == val)
					return en.Lab;
			}
			return null;
		}
	}
}
