using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
using BP.YG;

namespace BP.SF
{
	/// <summary>
	/// 文件类别
	/// </summary>
    public class SFSortAttr : EntityNoNameAttr
    {
        #region 基本属性
        public const string DFor = "DFor";
        public const string Grade = "Grade";
        public const string IsDtl = "IsDtl";
        #endregion
    }
	/// <summary>
	/// SFSort 的摘要说明。
	/// </summary>
    public class SFSort : EntityNoName
    {
        #region 基本属性
        #endregion

        #region 构造函数
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// 文件类别
        /// </summary>		
        public SFSort() { }
        public SFSort(string no)
            : base(no)
        {
        }
        /// <summary>
        /// SFSortMap
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
                map.PhysicsTable = "SF_Sort";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "文件类别";
                map.EnType = EnType.App;
                #endregion

                #region 字段
                map.AddTBStringPK(SFSortAttr.No, null, "编号", true, false, 2, 10, 50);
                map.AddTBString(SFSortAttr.Name, null, "名称", true, false, 0, 50, 200);
                map.AddTBInt(SFSortAttr.Grade, 1, "Grade", true, false);
                map.AddTBInt(SFSortAttr.IsDtl, 1, "IsDtl", true, false);
                map.AddTBString(SFSort1Attr.FK_Sort1, null, "FK_Sort1", true, false, 0, 50, 200);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 文件类别
	/// </summary>
    public class SFSorts : EntitiesNoName
    {
        #region 得到它的Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SFSort();
            }
        }
        #endregion

        #region 构造方法
        public SFSorts()
        {
        }
        #endregion
    }
	
}
