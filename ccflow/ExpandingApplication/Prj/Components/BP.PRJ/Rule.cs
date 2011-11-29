using System;
using System.Data;
using BP.DA;
using BP.Port; 
using BP.En;

namespace BP.PRJ
{
	/// <summary>
	/// 上传规则
	/// </summary>
    public class RuleAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 是否多文件
        /// </summary>
        public const string IsMultityFile = "IsMultityFile";
        /// <summary>
        /// 可下载的人员
        /// </summary>
        public const string CanDownSQL = "CanDownSQL";
        /// <summary>
        /// 格式要求
        /// </summary>
        public const string FileFormat = "FileFormat";
        /// <summary>
        /// 文件目录
        /// </summary>
        public const string FK_Dir = "FK_Dir";
    }
	/// <summary>
	/// 上传规则
	/// </summary>
    public class Rule : EntityNoName
    {
        #region 基本属性
        /// <summary>
        /// 是否多文件
        /// </summary>
        public bool IsMultityFile
        {
            get
            {
                return this.GetValBooleanByKey(RuleAttr.IsMultityFile);
            }
            set
            {
                this.SetValByKey(RuleAttr.IsMultityFile, value);
            }
        }
        public string FK_Dir
        {
            get
            {
                return this.GetValStrByKey(RuleAttr.FK_Dir);
            }
            set
            {
                this.SetValByKey(RuleAttr.FK_Dir, value);
            }
        }
        /// <summary>
        /// 可下载的人员
        /// </summary>
        public string CanDownSQL
        {
            get
            {
                return this.GetValStrByKey(RuleAttr.CanDownSQL);
            }
            set
            {
                this.SetValByKey(RuleAttr.CanDownSQL, value);
            }
        }
        /// <summary>
        /// 文件格式
        /// </summary>
          public string FileFormat
        {
            get
            {
                return this.GetValStrByKey(RuleAttr.FileFormat);
            }
            set
            {
                this.SetValByKey(RuleAttr.FileFormat, value);
            }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 上传规则
        /// </summary>
        public Rule() { }
        /// <summary>
        /// 上传规则
        /// </summary>
        public Rule(string no)
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

                Map map = new Map("PRJ_FileDesc");
                map.EnDesc = "上传规则";
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "3";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(RuleAttr.No, null, "编号", true, false, 3, 3, 3);
                map.AddTBString(RuleAttr.Name, null, "名称", true, false, 2, 60, 500);
                map.AddBoolean(RuleAttr.IsMultityFile, false, "是否多文件", true, false);
                map.AddTBString(RuleAttr.CanDownSQL, null, "可下载的人员", true, false, 0, 60, 500);
                map.AddTBString(RuleAttr.FileFormat, null, "格式要求", true, false, 0, 60, 500);
                map.AddTBString(RuleAttr.FK_Dir, null, "文件目录", true, false, 0, 60, 500);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 上传规则s
	/// </summary>
	public class Rules : EntitiesNoName
	{	
		#region 构造方法
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new Rule();
			}
		}
		/// <summary>
		/// 上传规则s 
		/// </summary>
        public Rules() { }
		#endregion
	}
	
}
