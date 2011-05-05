using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// 框架
    /// </summary>
    public class MapFrameAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 主表
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// 插入表单的位置
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        public const string Height = "Height";
        public const string Width = "Width";
        /// <summary>
        /// 是否可以自适应大小
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
    }
    /// <summary>
    /// 框架
    /// </summary>
    public class MapFrame : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 是否自适应大小
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(MapFrameAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.IsAutoSize, value);
            }
        }
        public string URL
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.URL);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.URL, value);
            }
        }
        public string Height
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.Height);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.Height, value);
            }
        }
        public string Width
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.Width);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.Width, value);
            }
        }
        public bool IsUse = false;
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapFrameAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.FK_MapData, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(MapFrameAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.RowIdx, value);
            }
        }
        
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapFrameAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapFrameAttr.GroupID, value);
            }
        }
       
        #endregion

        #region 构造方法
        /// <summary>
        /// 框架
        /// </summary>
        public MapFrame()
        {
        }
        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="no"></param>
        public MapFrame(string no)
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
                Map map = new Map("Sys_MapFrame");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "框架";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(MapFrameAttr.No, null, "编号", true, false, 1, 20, 20);

                map.AddTBString(MapFrameAttr.Name, null, "描述", true, false, 1, 200, 20);
                map.AddTBString(MapFrameAttr.FK_MapData, null, "主表", true, false, 0, 30, 20);

                map.AddTBString(MapFrameAttr.URL, null, "URL", true, false, 0, 3000, 20);

                map.AddTBString(MapFrameAttr.Width, null, "Width", true, false, 0, 20, 20);
                map.AddTBString(MapFrameAttr.Height, null, "Height", true, false, 0, 20, 20);


                //map.AddTBInt(MapFrameAttr.H, 500, "高度", false, false);
                //map.AddTBInt(MapFrameAttr.W, 400, "宽度", false, false);

                map.AddBoolean(MapFrameAttr.IsAutoSize, true, "是否自动设置大小", false, false);

                
                map.AddTBInt(MapFrameAttr.RowIdx, 99, "位置", false, false);
                map.AddTBInt(MapFrameAttr.GroupID, 0, "GroupID", false, false);

                
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 框架s
    /// </summary>
    public class MapFrames : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 框架s
        /// </summary>
        public MapFrames()
        {
        }
        /// <summary>
        /// 框架s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapFrames(string fk_mapdata)
        {
            this.Retrieve(MapFrameAttr.FK_MapData, fk_mapdata, MapFrameAttr.GroupID);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapFrame();
            }
        }
        #endregion
    }
}
