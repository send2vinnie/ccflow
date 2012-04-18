using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class GloVarAttr : EntityNoNameAttr
    {
        /// <summary>
        /// Val
        /// </summary>
        public const string Val = "Val";
        /// <summary>
        /// Note
        /// </summary>
        public const string Note = "Note";
    }
    /// <summary>
    /// 全局变量
    /// </summary>
    public class GloVar : EntityNoName
    {
        #region 属性
        /// <summary>
        /// FontStyle
        /// </summary>
        public string Val
        {
            get
            {
                return this.GetValStringByKey(GloVarAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Val, value);
            }
        }
        /// <summary>
        /// note
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(GloVarAttr.Note);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Note, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 全局变量
        /// </summary>
        public GloVar()
        {
        }
        /// <summary>
        /// 全局变量
        /// </summary>
        /// <param name="mypk"></param>
        public GloVar(string no)
        {
            this.No = no;
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
                Map map = new Map("Sys_GloVar");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "全局变量";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(GloVarAttr.No, null, "键", true, false, 1, 30, 20);
                map.AddTBString(GloVarAttr.Name, null, "名称", true, false, 0, 120, 20);
                map.AddTBString(GloVarAttr.Val, null, "值", true, false, 0, 120, 20);
                map.AddTBString(GloVarAttr.Note, null, "Note", true, false, 0, 4000, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 全局变量s
    /// </summary>
    public class GloVars : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 全局变量s
        /// </summary>
        public GloVars()
        {
        }
        /// <summary>
        /// 全局变量s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public GloVars(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve(FrmLineAttr.FK_MapData, fk_mapdata);
            else
                this.RetrieveFromCash(FrmLineAttr.FK_MapData, (object)fk_mapdata);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GloVar();
            }
        }
        #endregion
    }
}
