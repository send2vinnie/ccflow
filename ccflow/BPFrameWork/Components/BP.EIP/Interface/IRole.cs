using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using BP.EIP.Enum;

namespace BP.EIP.Interface
{
    public interface IRole : IBase
    {
        /// <summary>
        /// 按部门获取角色
        /// </summary>
        /// <param name="userInfo">操作员</param>
        /// <param name="departmentId">部门主键</param>
        /// <returns>数据表</returns>
        DataTable GetDTByDept(string departmentId);
        /// <summary>
        /// 获得角色中的用户主键
        /// </summary>
        /// <param name="roleId">角色主键</param>
        /// <returns>数据表</returns>
        DataTable GetDTUserByRole(string roleId);
        /// <summary>
        /// 清除角色用户关联
        /// </summary>
        /// <param name="roleId">角色主键</param>
        /// <returns>影响行数</returns>
        int ClearRoleUser(string roleId);
    }
}
