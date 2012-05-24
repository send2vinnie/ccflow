using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_MenuAttr : EntityNoNameAttr
    {
        public const string MenuNo = "MenuNo";
        public const string Pid = "Pid";
        public const string FK_Function = "FK_Function";
        public const string MenuName = "MenuName";
        public const string Title = "Title";
        public const string Img = "Img";
        public const string Url = "Url";
        public const string Path = "Path";
        public const string Status = "Status";
    }
    
    public partial class Port_Menu : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String MenuNo
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.MenuNo);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.MenuNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Pid
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.Pid);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.Pid, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Function
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.FK_Function);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.FK_Function, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String MenuName
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.MenuName);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.MenuName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Title
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.Title);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.Title, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Img
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.Img);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.Img, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Url
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.Url);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.Url, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Path
        {
            get
            {
                return this.GetValStringByKey(Port_MenuAttr.Path);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.Path, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(Port_MenuAttr.Status);
            }
            set
            {
                this.SetValByKey(Port_MenuAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_Menu()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_Menu(string No)
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
                Map map = new Map("Port_Menu");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_MenuAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_MenuAttr.MenuNo, null, "", true, false, 0,  10, 10);
                map.AddTBString(Port_MenuAttr.Pid, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_MenuAttr.FK_Function, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_MenuAttr.MenuName, null, "", true, false, 0,  100, 100);
                map.AddTBString(Port_MenuAttr.Title, null, "", true, false, 0,  100, 100);
                map.AddTBString(Port_MenuAttr.Img, null, "", true, false, 0,  100, 100);
                map.AddTBString(Port_MenuAttr.Url, null, "", true, false, 0,  1000, 1000);
                map.AddTBString(Port_MenuAttr.Path, null, "", true, false, 0,  1000, 1000);
                map.AddTBInt(Port_MenuAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Menus : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Menu(); }
        }
    }
}