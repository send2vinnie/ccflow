using System;
using System.Collections.Generic;
using System.Text;

namespace Tax666.WebControls.Common
{
    public class Permission
    {
        #region 私有变量
        private string _ApplicationName;
        private int _ApplicationID;
        private string _PageCode;
        private string _PageCodeName;
        private List<PermissionItem> _ItemList = new List<PermissionItem>();
        #endregion

        #region 属性
        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
            set
            {
                _ApplicationID = value;
            }
        }
        /// <summary>
        /// 模块代码
        /// </summary>
        public string PageCode
        {
            get
            {
                return _PageCode;
            }
            set
            {
                _PageCode = value;
            }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string PageCodeName
        {
            get
            {
                return _PageCodeName;
            }
            set
            {
                _PageCodeName = value;
            }
        }

        /// <summary>
        /// 权限文件列表
        /// </summary>
        public List<PermissionItem> ItemList
        {
            get
            {
                return _ItemList;
            }
            set
            {
                _ItemList = value;
            }
        }
        #endregion
    }
     /// <summary>
     /// 权限详细类
     /// </summary>
     public class PermissionItem
     {
         #region 私有变量
         private string _Item_Name;
         private int _Item_Value;
         private string _Item_FileList;
         #endregion

         #region 属性
         /// <summary>
         /// 权限名称
         /// </summary>
         public string Item_Name
         {
             get
             {
                 return _Item_Name;
             }
             set
             {
                 _Item_Name = value;
             }
         }
         /// <summary>
         /// 权限值
         /// </summary>
         public int Item_Value
         {
             get
             {
                 return _Item_Value;
             }
             set
             {
                 _Item_Value = value;
             }
         }

         /// <summary>
         /// 权限所属文件列表
         /// </summary>
         public string Item_FileList
         {
             get
             {
                 return _Item_FileList;
             }
             set
             {
                 _Item_FileList = value;
             }
         }
         #endregion
     }

     #region 权限类型
     /// <summary>
     /// 权限类型
     /// </summary>
     public enum PopedomType
     {
         /// <summary>
         /// 列表/查看
         /// </summary>
         List = 2,
         /// <summary>
         /// 新增
         /// </summary> 
         New = 4,
         /// <summary>
         /// 修改
         /// </summary>
         Edit = 8,
         /// <summary>
         /// 删除
         /// </summary>
         Delete = 16,
         /// <summary>
         /// 排序
         /// </summary>
         Orderby = 32,
         /// <summary>
         /// 打印
         /// </summary>
         Print = 64,
         /// <summary>
         /// 设置有效/无效
         /// </summary>
         Valid = 128,
         /// <summary>
         /// 审核
         /// </summary>
         Audit = 256
     }
     #endregion
}
