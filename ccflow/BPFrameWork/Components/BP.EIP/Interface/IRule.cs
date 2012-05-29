using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using BP.EIP.Enum;

namespace BP.EIP.Interface
{
    public interface IRule : IBase
    {
        /// <summary>
        /// 01.用户是否在某个角色里
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="roleCode">角色主键</param>
        /// <returns>是否存在</returns>
        bool IsUserInRole(string userId, string roleId);

        /// <summary>
        /// 02.当前用户是否有相应的操作权限
        /// </summary>
        /// <param name="permissionItemCode">操作权限编号</param>
        /// <returns>是否有权限</returns>
        bool IsAuthorized(string permissionItemCode);
        /// <summary>
        /// 根据应用获取相应的菜单列表
        /// </summary>
        /// <param name="appName">应用</param>
        /// <returns>数据表</returns>
        DataTable GetMenus(string appName);
    }
}
