using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
   
    /// <summary>
    /// GroupTitle
    /// </summary>
    public class GroupTitleAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 主表
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// Lab
        /// </summary>
        public const string Lab = "Lab";
        /// <summary>
        /// RowIdx
        /// </summary>
        public const string RowIdx = "RowIdx";
    }
    /// <summary>
    /// GroupTitle
    /// </summary>
    public class GroupTitle : EntityOID
    {
        #region 属性
        public string EnName
        {
            get
            {
                return this.GetValStrByKey(GroupTitleAttr.Lab);
            }
            set
            {
                this.SetValByKey(GroupTitleAttr.EnName, value);
            }
        }
        public string Lab
        {
            get
            {
                return this.GetValStrByKey(GroupTitleAttr.Lab);
            }
            set
            {
                this.SetValByKey(GroupTitleAttr.Lab, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(GroupTitleAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(GroupTitleAttr.RowIdx, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// GroupTitle
        /// </summary>
        public GroupTitle()
        {
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
                Map map = new Map("Sys_GroupTitle");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "GroupTitle";
                map.EnType = EnType.Sys;

                map.AddTBIntPKOID();
                map.AddTBString(GroupTitleAttr.Lab, null, "Lab", true, false, 0, 30, 20);
                map.AddTBString(GroupTitleAttr.EnName, null, "主表", true, false, 0, 30, 20);
                map.AddTBInt(GroupTitleAttr.RowIdx, 99, "RowIdx", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// GroupTitles
    /// </summary>
    public class GroupTitles : EntitiesOID
    {
        #region 构造
        /// <summary>
        /// GroupTitles
        /// </summary>
        public GroupTitles()
        {
        }
        /// <summary>
        /// GroupTitles
        /// </summary>
        /// <param name="EnName">s</param>
        public GroupTitles(string EnName)
        {
            this.Retrieve(GroupTitleAttr.EnName, EnName);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GroupTitle();
            }
        }
        #endregion
    }
}
