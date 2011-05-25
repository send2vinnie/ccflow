using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
      
    /// <summary>
    /// 标签
    /// </summary>
    public class FrmLabAttr : EntityOIDNameAttr
    {
        /// <summary>
        /// 主表
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// X2
        /// </summary>
        public const string X2 = "X2";
        /// <summary>
        /// Y2
        /// </summary>
        public const string Y2 = "Y2";
        /// <summary>
        /// 宽度
        /// </summary>
        public const string FrontSize = "FrontSize";
        /// <summary>
        /// 颜色
        /// </summary>
        public const string FrontColor = "FrontColor";
        /// <summary>
        /// 风格
        /// </summary>
        public const string FrontName = "FrontName";
        /// <summary>
        /// 字体风格
        /// </summary>
        public const string FrontWeight = "FrontWeight";
    }
    /// <summary>
    /// 标签
    /// </summary>
    public class FrmLab : EntityOIDName
    {
        #region 属性
        public string FrontWeight
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FrontWeight);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontWeight, value);
            }
        }
        public string FrontColor
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FrontColor);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontColor, value);
            }
        }
 
       
        public string FrontName
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FrontName);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontName, value);
            }
        }
        /// <summary>
        /// 是否检查人员的权限
        /// </summary>
        public int Y
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.Y, value);
            }
        }
        public int X
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.X);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.X, value);
            }
        }
        public int FrontSize
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.FrontSize);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FrontSize, value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmLabAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FK_MapData, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 标签
        /// </summary>
        public FrmLab()
        {
        }
        public FrmLab(int oid)
        {
            this.OID = oid;
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
                Map map = new Map("Sys_FrmLab");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "标签";
                map.EnType = EnType.Sys;

                map.AddTBIntPKOID();
                map.AddTBString(FrmLabAttr.FK_MapData, null, "主表", true, false, 1, 30, 20);
                map.AddTBString(FrmLabAttr.Name, "新建标签", "名称", true, false, 0, 3900, 20);

                map.AddTBInt(FrmLabAttr.X, 5, "X", true, false);
                map.AddTBInt(FrmLabAttr.Y, 5, "Y", false, false);

                map.AddTBInt(FrmLabAttr.FrontSize, 12, "大小", false, false);
                map.AddTBString(FrmLabAttr.FrontColor, "black", "颜色", true, false, 0, 30, 20);
                map.AddTBString(FrmLabAttr.FrontName, "宋体", "字体名称", true, false, 0, 30, 20);
                map.AddTBString(FrmLabAttr.FrontWeight, "normal", "字体风格", true, false, 0, 30, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 标签s
    /// </summary>
    public class FrmLabs : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 标签s
        /// </summary>
        public FrmLabs()
        {
        }
        /// <summary>
        /// 标签s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmLabs(string fk_mapdata)
        {
            this.Retrieve(FrmLabAttr.FK_MapData, fk_mapdata);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmLab();
            }
        }
        #endregion
    }
}
