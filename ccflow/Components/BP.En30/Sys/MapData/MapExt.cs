using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// 扩展
    /// </summary>
    public class MapExtAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 主表
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// ExtType
        /// </summary>
        public const string ExtType = "ExtType";
        /// <summary>
        /// 插入表单的位置
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// 高度
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// 宽度
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// 是否可以自适应大小
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
        /// <summary>
        /// 设置的属性
        /// </summary>
        public const string AttrOfOper = "AttrOfOper";
        /// <summary>
        /// 激活的属性
        /// </summary>
        public const string AttrsOfActive = "AttrsOfActive";
        /// <summary>
        /// 执行方式
        /// </summary>
        public const string DoWay = "DoWay";
        /// <summary>
        /// Tag
        /// </summary>
        public const string Tag = "Tag";
        public const string Tag1 = "Tag1";
    }
    /// <summary>
    /// 扩展
    /// </summary>
    public class MapExt : EntityMyPK
    {
        #region 属性
        public string ExtDesc
        {
            get
            {
                string dec = "";
                switch (this.ExtType)
                {
                    case MapExtXmlList.ActiveDDL:
                        dec += "输入项目："+this.AttrOfOper+" 联动项目："+this.AttrsOfActive;
                        break;
                    case MapExtXmlList.FullCtrl:
                        dec += "输入项目： 联动项目：";
                        break;
                    case MapExtXmlList.InputCheck:
                        dec += "输入项目："+this.AttrOfOper+" 检查内容："+this.Tag1;
                        break;
                    case MapExtXmlList.PopVal:
                        dec += "输入项目：" + this.AttrOfOper + " 弹出的Url：" + this.Tag;
                        break;
                    default:
                        break;
                }
                return dec;
            }
        }
        /// <summary>
        /// 是否自适应大小
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(MapExtAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(MapExtAttr.IsAutoSize, value);
            }
        }
      
        public string ExtType
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.ExtType);
            }
            set
            {
                this.SetValByKey(MapExtAttr.ExtType, value);
            }
        }
        public int DoWay
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.DoWay);
            }
            set
            {
                this.SetValByKey(MapExtAttr.DoWay, value);
            }
        }
        /// <summary>
        /// 操作的attrs
        /// </summary>
        public string AttrOfOper
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.AttrOfOper);
            }
            set
            {
                this.SetValByKey(MapExtAttr.AttrOfOper, value);
            }
        }
        /// <summary>
        /// 激活的attrs
        /// </summary>
        public string AttrsOfActive
        {
            get
            {
              //  return this.GetValStrByKey(MapExtAttr.AttrsOfActive).Replace("~", "'");
                return this.GetValStrByKey(MapExtAttr.AttrsOfActive);
            }
            set
            {
                this.SetValByKey(MapExtAttr.AttrsOfActive, value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapExtAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapExtAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStrByKey("Doc").Replace("~","'");
            }
            set
            {
                this.SetValByKey("Doc", value);
            }
        }
        public string Tag
        {
            get
            {
                return this.GetValStrByKey("Tag").Replace("~", "'");
            }
            set
            {
                this.SetValByKey("Tag", value);
            }
        }
        public string Tag1
        {
            get
            {
                return this.GetValStrByKey("Tag1").Replace("~", "'");
            }
            set
            {
                this.SetValByKey("Tag1", value);
            }
        }
        public int H
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.H);
            }
            set
            {
                this.SetValByKey(MapExtAttr.H, value);
            }
        }
        public int W
        {
            get
            {
                return this.GetValIntByKey(MapExtAttr.W);
            }
            set
            {
                this.SetValByKey(MapExtAttr.W, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 扩展
        /// </summary>
        public MapExt()
        {
        }
        /// <summary>
        /// 扩展
        /// </summary>
        /// <param name="no"></param>
        public MapExt(string mypk)
        {
            try
            {
                this.MyPK = mypk;
                this.Retrieve();
            }
            catch
            {
                this.CheckPhysicsTable();
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

                Map map = new Map("Sys_MapExt");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "扩展";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(MapExtAttr.FK_MapData, null, "主表", true, false, 0, 30, 20);
                map.AddTBString(MapExtAttr.ExtType, null, "类型", true, false, 0, 30, 20);

                map.AddTBString(MapExtAttr.AttrOfOper, null, "操作的Attr", true, false, 0, 30, 20);
                map.AddTBString(MapExtAttr.AttrsOfActive, null, "激活的字段", true, false, 0, 900, 20);

                map.AddTBInt(MapExtAttr.DoWay, 0, "执行方式", true, false);

                map.AddTBStringDoc();

                map.AddTBString(MapExtAttr.Tag, null, "Tag", true, false, 0, 900, 20);
                map.AddTBString(MapExtAttr.Tag1, null, "Tag1", true, false, 0, 900, 20);

                map.AddTBInt(MapExtAttr.H, 500, "高度", false, false);
                map.AddTBInt(MapExtAttr.W, 400, "宽度", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 扩展s
    /// </summary>
    public class MapExts : Entities
    {
        #region 构造
        /// <summary>
        /// 扩展s
        /// </summary>
        public MapExts()
        {
        }
        /// <summary>
        /// 扩展s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public MapExts(string fk_mapdata)
        {
            this.Retrieve(MapExtAttr.FK_MapData, fk_mapdata);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapExt();
            }
        }
        #endregion
    }
}
