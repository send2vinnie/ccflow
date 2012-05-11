using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_LayoutDetailAttr : EntityNoNameAttr
    {
        public const string Layout_Id = "Layout_Id";
        public const string ColumnNo = "ColumnNo";
        public const string PanelId = "PanelId";
        public const string PanelTitle = "PanelTitle";
        public const string ShowCollapseButton = "ShowCollapseButton";
        public const string Width = "Width";
        public const string Height = "Height";
        public const string SeqNo = "SeqNo";
        public const string Url = "Url";
        public const string IsEdit = "IsEdit";
        public const string IsShow = "IsShow";
    }
    
    public partial class EIP_LayoutDetail : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String Layout_Id
        {
            get
            {
                return this.GetValStringByKey(EIP_LayoutDetailAttr.Layout_Id);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.Layout_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int ColumnNo
        {
            get
            {
                return this.GetValIntByKey(EIP_LayoutDetailAttr.ColumnNo);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.ColumnNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String PanelId
        {
            get
            {
                return this.GetValStringByKey(EIP_LayoutDetailAttr.PanelId);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.PanelId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String PanelTitle
        {
            get
            {
                return this.GetValStringByKey(EIP_LayoutDetailAttr.PanelTitle);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.PanelTitle, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool ShowCollapseButton
        {
            get
            {
                return this.GetValBooleanByKey(EIP_LayoutDetailAttr.ShowCollapseButton);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.ShowCollapseButton, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get
            {
                return this.GetValIntByKey(EIP_LayoutDetailAttr.Width);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.Width, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get
            {
                return this.GetValIntByKey(EIP_LayoutDetailAttr.Height);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.Height, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int SeqNo
        {
            get
            {
                return this.GetValIntByKey(EIP_LayoutDetailAttr.SeqNo);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.SeqNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Url
        {
            get
            {
                return this.GetValStringByKey(EIP_LayoutDetailAttr.Url);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.Url, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.GetValBooleanByKey(EIP_LayoutDetailAttr.IsEdit);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.IsEdit, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsShow
        {
            get
            {
                return this.GetValBooleanByKey(EIP_LayoutDetailAttr.IsShow);
            }
            set
            {
                this.SetValByKey(EIP_LayoutDetailAttr.IsShow, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_LayoutDetail()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_LayoutDetail(string No)
        {
            this.No = No;
            this.Retrieve();
        }
        #endregion
        
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("EIP_LayoutDetail");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(EIP_LayoutDetailAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(EIP_LayoutDetailAttr.Layout_Id, null, "", true, false, 0,  50, 50);
                map.AddTBInt(EIP_LayoutDetailAttr.ColumnNo, 0, "", true, false);
                map.AddTBString(EIP_LayoutDetailAttr.PanelId, null, "", true, false, 0,  10, 10);
                map.AddTBString(EIP_LayoutDetailAttr.PanelTitle, null, "", true, false, 0,  20, 20);
                map.AddBoolean(EIP_LayoutDetailAttr.ShowCollapseButton, true, "", true, false);
                map.AddTBInt(EIP_LayoutDetailAttr.Width, 0, "", true, false);
                map.AddTBInt(EIP_LayoutDetailAttr.Height, 0, "", true, false);
                map.AddTBInt(EIP_LayoutDetailAttr.SeqNo, 0, "", true, false);
                map.AddTBString(EIP_LayoutDetailAttr.Url, null, "", true, false, 0,  200, 200);
                map.AddBoolean(EIP_LayoutDetailAttr.IsEdit, true, "", true, false);
                map.AddBoolean(EIP_LayoutDetailAttr.IsShow, true, "", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_LayoutDetails : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_LayoutDetail(); }
        }
    }
}