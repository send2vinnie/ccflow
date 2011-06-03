using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
      
    /// <summary>
    /// 标签
    /// </summary>
    public class FrmLabAttr : EntityMyPKAttr
    {
        /// <summary>
        /// Text
        /// </summary>
        public const string Text = "Text";
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
        public const string FontSize = "FontSize";
        /// <summary>
        /// 颜色
        /// </summary>
        public const string FontColor = "FontColor";
        /// <summary>
        /// 风格
        /// </summary>
        public const string FontName = "FontName";
        /// <summary>
        /// 字体风格
        /// </summary>
        public const string FontStyle = "FontStyle";
    }
    /// <summary>
    /// 标签
    /// </summary>
    public class FrmLab : EntityMyPK
    {
        #region 属性
        /// <summary>
        /// FontStyle
        /// </summary>
        public string FontStyle
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FontStyle);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontStyle, value);
            }
        }
        /// <summary>
        /// FontColor
        /// </summary>
        public string FontColor
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FontColor);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontColor, value);
            }
        }
        /// <summary>
        /// FontName
        /// </summary>
        public string FontName
        {
            get
            {
                return this.GetValStringByKey(FrmLabAttr.FontName);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontName, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmLabAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmLabAttr.X);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.X, value);
            }
        }
        /// <summary>
        /// FontSize
        /// </summary>
        public int FontSize
        {
            get
            {
                return this.GetValIntByKey(FrmLabAttr.FontSize);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.FontSize, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
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
        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                return this.GetValStrByKey(FrmLabAttr.Text);
            }
            set
            {
                this.SetValByKey(FrmLabAttr.Text, value);
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
        /// <summary>
        /// 标签
        /// </summary>
        /// <param name="mypk"></param>
        public FrmLab(string mypk)
        {
            this.MyPK = mypk;
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

                map.AddMyPK();
                map.AddTBString(FrmLabAttr.FK_MapData, null, "FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmLabAttr.Text, "New Label", "Label", true, false, 0, 3900, 20);

                map.AddTBFloat(FrmLabAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmLabAttr.Y, 5, "Y", false, false);

                map.AddTBInt(FrmLabAttr.FontSize, 12, "FontSize", false, false);
                map.AddTBString(FrmLabAttr.FontColor, "black", "FontColor", true, false, 0, 15, 20);
                map.AddTBString(FrmLabAttr.FontName, null, "FontName", true, false, 0, 15, 20);
                map.AddTBString(FrmLabAttr.FontStyle, "normal", "FontStyle", true, false, 0, 15, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 标签s
    /// </summary>
    public class FrmLabs : EntitiesMyPK
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
