using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
      
    /// <summary>
    /// ��
    /// </summary>
    public class FrmLineAttr : EntityOIDNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// X1
        /// </summary>
        public const string X1 = "X1";
        /// <summary>
        /// Y1
        /// </summary>
        public const string Y1 = "Y1";
        /// <summary>
        /// X2
        /// </summary>
        public const string X2 = "X2";
        /// <summary>
        /// Y2
        /// </summary>
        public const string Y2 = "Y2";
        /// <summary>
        /// ���
        /// </summary>
        public const string BorderWidth = "BorderWidth";
        /// <summary>
        /// ��ɫ
        /// </summary>
        public const string BorderColor = "BorderColor";
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
    }
    /// <summary>
    /// ��
    /// </summary>
    public class FrmLine : EntityMyPK
    {
        #region ����
        public string BorderColorHtml
        {
            get
            {
                return PubClass.ToHtmlColor(this.BorderColor);
            }
        }
        public string BorderColor
        {
            get
            {
            //    return this.GetValStringByKey(FrmLineAttr.BorderColor);
                return this.GetValStringByKey("BorderColor");

            }
            set
            {
                this.SetValByKey(FrmLineAttr.BorderColor, value);
            }
        }
        
        public float BorderWidth
        {
            get
            {
                return this.GetValFloatByKey(FrmLineAttr.BorderWidth);
            }
            set
            {
                this.SetValByKey(FrmLineAttr.BorderWidth, value);
            }
        }
        /// <summary>
        /// �Ƿ�����Ա��Ȩ��
        /// </summary>
        public float Y1
        {
            get
            {
                return this.GetValFloatByKey(FrmLineAttr.Y1);
            }
            set
            {
                this.SetValByKey(FrmLineAttr.Y1, value);
            }
        }
        public float X1
        {
            get
            {
                return this.GetValFloatByKey(FrmLineAttr.X1);
            }
            set
            {
                this.SetValByKey(FrmLineAttr.X1, value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmLineAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmLineAttr.FK_MapData, value);
            }
        }
        public float Y2
        {
            get
            {
                return this.GetValFloatByKey(FrmLineAttr.Y2);
            }
            set
            {
                this.SetValByKey(FrmLineAttr.Y2, value);
            }
        }
        public float X2
        {
            get
            {
                return this.GetValFloatByKey(FrmLineAttr.X2);
            }
            set
            {
                this.SetValByKey(FrmLineAttr.X2, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��
        /// </summary>
        public FrmLine()
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
                Map map = new Map("Sys_FrmLine");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(FrmLineAttr.FK_MapData, null, "����", true, false, 0, 30, 20);

                map.AddTBFloat(FrmLineAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmLineAttr.Y, 5, "Y", false, false);

                map.AddTBFloat(FrmLineAttr.X1, 5, "X1", true, false);
                map.AddTBFloat(FrmLineAttr.Y1, 5, "Y1", false, false);

                map.AddTBFloat(FrmLineAttr.X2, 9, "X2", false, false);
                map.AddTBFloat(FrmLineAttr.Y2, 9, "Y2", false, false);

                map.AddTBFloat(FrmLineAttr.BorderWidth, 1, "���", false, false);
                map.AddTBString(FrmLineAttr.BorderColor, "black", "��ɫ", true, false, 0, 30, 20);

                //map.AddTBString(FrmLineAttr.BorderStyle, "dot", "�߿���", true, false, 0, 30, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��s
    /// </summary>
    public class FrmLines : Entities
    {
        #region ����
        /// <summary>
        /// ��s
        /// </summary>
        public FrmLines()
        {
        }
        /// <summary>
        /// ��s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmLines(string fk_mapdata)
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
                return new FrmLine();
            }
        }
        #endregion
    }
}
