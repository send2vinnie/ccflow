using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Enum;
using System.Data;

namespace BP.EIP.Interface
{
    public interface IPermission : IBase
    {
        /// <summary>
        /// 获取Function中的所有操作
        /// </summary>
        /// <param name="functionId">功能界面Id</param>
        /// <returns>数据集</returns>
        DataTable GetDTByFunction(string functionId);

        /// <summary>
        /// 获取所有Functions
        /// </summary>
        /// <returns>数据集</returns>
        DataTable GetAllFunctions();

        /// <summary>
        /// 获取指定域中所有Functions
        /// </summary>
        /// <param name="domainId">域Id</param>
        /// <returns>数据集</returns>
        DataTable GetAllFunctions(string domainId);

        /// <summary>
        /// 获取所有操作（Operates)
        /// </summary>
        /// <returns>数据集</returns>
        DataTable GetAllOperates();

        /// <summary>
        /// 获取指定域中所有操作（Operates)
        /// </summary>
        /// <param name="domainId">域Id</param>
        /// <returns>数据集</returns>
        DataTable GetAllOperates(string domainId);

        /// <summary>
        /// 获取用户所有权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>数据集</returns>
        DataTable GetDTByUser(string userId);

        /// <summary>
        /// 获取角色所有权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>数据集</returns>
        DataTable GetDTByRole(string roleId);

        /// <summary>
        /// 获取域中所有权限
        /// </summary>
        /// <param name="domainId">域Id</param>
        /// <returns>数据集</returns>
        DataTable GetDTByDomain(string domainId);

        /// <summary>
        /// 按编码获取一个权限
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns>实体</returns>
        BasePermission GetByCode(string code);
    }
}
