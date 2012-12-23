using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ͼƬ
    /// </summary>
    public class FrmImgAttr : EntityMyPKAttr
    {
        /// <summary>
        /// Text
        /// </summary>
        public const string Text = "Text";
        /// <summary>
        /// ����
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
        public const string ImgURL = "ImgURL";
        /// <summary>
        /// LinkURL
        /// </summary>
        public const string LinkURL = "LinkURL";
        /// <summary>
        /// LinkTarget
        /// </summary>
        public const string LinkTarget = "LinkTarget";
    }
    /// <summary>
    /// ͼƬ
    /// </summary>
    public class FrmImg : EntityMyPK
    {
        #region ����
        public string LinkTarget
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.LinkTarget);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.LinkTarget, value);
            }
        }
        /// <summary>
        /// URL
        /// </summary>
        public string LinkURL
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.LinkURL);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.LinkURL, value);
            }
        }
        public string ImgURL
        {
            get
            {
                return this.GetValStringByKey(FrmImgAttr.ImgURL);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.ImgURL, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.X);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.X, value);
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.H);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.H, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmImgAttr.W);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.W, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmImgAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmImgAttr.FK_MapData, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ͼƬ
        /// </summary>
        public FrmImg()
        {
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        /// <param name="mypk"></param>
        public FrmImg(string mypk)
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
                Map map = new Map("Sys_FrmImg");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ͼƬ";
                map.EnType = EnType.Sys;
                map.AddMyPK();
                map.AddTBString(FrmImgAttr.FK_MapData, null, "FK_MapData", true, false, 1, 30, 20);
                
                map.AddTBFloat(FrmImgAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmImgAttr.Y, 5, "Y", false, false);

                map.AddTBFloat(FrmImgAttr.H, 200, "H", true, false);
                map.AddTBFloat(FrmImgAttr.W, 160, "W", false, false);

                map.AddTBString(FrmImgAttr.ImgURL, null, "URL", true, false, 0, 200, 20);
                map.AddTBString(FrmImgAttr.LinkURL, null, "LinkURL", true, false, 0, 200, 20);
                map.AddTBString(FrmImgAttr.LinkTarget, "_blank", "LinkTarget", true, false, 0, 200, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ͼƬs
    /// </summary>
    public class FrmImgs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ͼƬs
        /// </summary>
        public FrmImgs()
        {
        }
        /// <summary>
        /// ͼƬs
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmImgs(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve(FrmLineAttr.FK_MapData, fk_mapdata);
            else
                this.RetrieveFromCash(FrmLineAttr.FK_MapData, (object)fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmImg();
            }
        }
        #endregion
    }
}
