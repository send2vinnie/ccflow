using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// 附件
    /// </summary>
    public class FrmAttachmentAttr : EntityMyPKAttr
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
        /// W
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// H
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "URL";
    }
    /// <summary>
    /// 附件
    /// </summary>
    public class FrmAttachment : EntityMyPK
    {
        #region 属性
        /// <summary>
        /// URL
        /// </summary>
        public string URL
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.URL);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.URL, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.X);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.X, value);
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.H);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.H, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.W);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.W, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmAttachmentAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.FK_MapData, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 附件
        /// </summary>
        public FrmAttachment()
        {
        }
        /// <summary>
        /// 附件
        /// </summary>
        /// <param name="mypk"></param>
        public FrmAttachment(string mypk)
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
                Map map = new Map("Sys_FrmAttachment");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "附件";
                map.EnType = EnType.Sys;
                map.AddMyPK();
                map.AddTBString(FrmAttachmentAttr.FK_MapData, null, "FK_MapData", true, false, 1, 30, 20);
                
                map.AddTBFloat(FrmAttachmentAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmAttachmentAttr.Y, 5, "Y", false, false);

                map.AddTBFloat(FrmAttachmentAttr.H, 5, "H", true, false);
                map.AddTBFloat(FrmAttachmentAttr.W, 5, "W", false, false);

                map.AddTBString(FrmAttachmentAttr.URL, "black", "URL", true, false, 0, 200, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 附件s
    /// </summary>
    public class FrmAttachments : EntitiesMyPK
    {
        #region 构造
        /// <summary>
        /// 附件s
        /// </summary>
        public FrmAttachments()
        {
        }
        /// <summary>
        /// 附件s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmAttachments(string fk_mapdata)
        {
            this.Retrieve(FrmAttachmentAttr.FK_MapData, fk_mapdata);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmAttachment();
            }
        }
        #endregion
    }
}
