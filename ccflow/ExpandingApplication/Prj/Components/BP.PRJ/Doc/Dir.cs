
using System;
using System.Data;
using BP.DA;
using BP.Port; 
using BP.En;

namespace BP.PRJ
{
	/// <summary>
	/// 文件目录
	/// </summary>
    public class DirAttr : EntityNoNameAttr
    {
        public const string DirPath = "DirPath";
        public const string ID = "ID";
        public const string PID = "PID";
    }
	/// <summary>
	/// 文件描述
	/// </summary>
    public class Dir : EntityNoName
    {
        #region 基本属性
        /// <summary>
        /// 位值
        /// </summary>
        public string DirPath
        {
            get
            {
                return this.GetValStrByKey(DirAttr.DirPath);
            }
            set
            {
                this.SetValByKey(DirAttr.DirPath, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 文件描述
        /// </summary>
        public Dir() { }
        /// <summary>
        /// 文件描述
        /// </summary>
        public Dir(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("PRJ_Dir");
                map.EnDesc = "文件描述";
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(DirAttr.No, null, "编号", true, false, 10, 10, 10);
                map.AddTBString(DirAttr.Name, null, "名称", true, false, 0, 60, 500);
                map.AddTBString(DirAttr.ID, null, "ID", true, false, 0, 60, 500);
                map.AddTBString(DirAttr.PID, null, "PID", true, false, 0, 60, 500);
                map.AddTBString(DirAttr.DirPath, null, "文件路径", true, false, 0, 60, 500);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 文件描述s
	/// </summary>
	public class Dirs : EntitiesNoName
	{	
		#region 构造方法
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new Dir();
			}
		}
		/// <summary>
		/// 文件描述s 
		/// </summary>
		public Dirs(){}
		#endregion
	}
	
}
