using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// 属性
	/// </summary>
	public class DataCellAttr
	{
		/// <summary>
		/// SaveDataCell
		/// </summary>
        public const string RefVal = "RefVal";
	}
	/// <summary>
	/// SaveDataCell
	/// </summary>
	public class DataCell: EntityOID
	{
		#region 基本属性
        public string RefVal
        {
            get
            {
                return this.GetValStrByKey("RefVal");
            }
            set
            {
                this.SetValByKey("RefVal", value);
            }
        }
		  
		#endregion

		#region 构造方法
		/// <summary>
		/// SaveDataCell
		/// </summary>
		public DataCell()
		{
		}
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_DataCell");
                map.EnType = EnType.Sys;
                map.EnDesc = "DataCell";
                map.DepositaryOfEntity = Depositary.None;
                map.AddTBIntPKOID();
                map.AddTBString(DataCellAttr.RefVal, null, "RefVal", false, true, 1, 50, 10);
                for (int i = 0; i < 20; i++)
                {
                    string val = "F" + i;
                    map.AddTBString(val, null, val, false, true, 0, 2000, 10);
                }
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	
       
	}
	/// <summary>
	/// SaveDataCells
	/// </summary>
	public class DataCells : Entities
	{
		/// <summary>
		/// SaveDataCells
		/// </summary>
		public DataCells()
		{

		}
        public DataCells(string pk)
        {
            this.Retrieve(DataCellAttr.RefVal, pk, "OID");
        }
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DataCell();
			}
		}
	}
}
