using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_MenuAttr : EntityNoNameAttr
    {
        //public const string No = "No";
        public const string MenuNo = "MenuNo";
        public const string Pid = "Pid";
        public const string FunctionId = "FunctionId";
        public const string MenuName = "MenuName";
        public const string Title = "Title";
        public const string Img = "Img";
        public const string Url = "Url";
        public const string Path = "Path";
        public const string Status = "Status";
    }
    
    public partial class EIP_Menu : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String MenuNo
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.MenuNo);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.MenuNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Pid
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.Pid);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.Pid, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FunctionId
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.FunctionId);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.FunctionId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String MenuName
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.MenuName);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.MenuName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Title
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.Title);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.Title, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Img
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.Img);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.Img, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Url
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.Url);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.Url, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Path
        {
            get
            {
                return this.GetValStringByKey(EIP_MenuAttr.Path);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.Path, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(EIP_MenuAttr.Status);
            }
            set
            {
                this.SetValByKey(EIP_MenuAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_Menu()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_Menu(string No)
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
                Map map = new Map("EIP_Menu");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(EIP_MenuAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(EIP_MenuAttr.MenuNo, null, "", true, false, 0,  10, 10);
                map.AddTBString(EIP_MenuAttr.Pid, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_MenuAttr.FunctionId, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_MenuAttr.MenuName, null, "", true, false, 0,  100, 100);
                map.AddTBString(EIP_MenuAttr.Title, null, "", true, false, 0,  100, 100);
                map.AddTBString(EIP_MenuAttr.Img, null, "", true, false, 0,  100, 100);
                map.AddTBString(EIP_MenuAttr.Url, null, "", true, false, 0,  1000, 1000);
                map.AddTBString(EIP_MenuAttr.Path, null, "", true, false, 0,  1000, 1000);
                map.AddTBInt(EIP_MenuAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_Menus : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_Menu(); }
        }
    }
}