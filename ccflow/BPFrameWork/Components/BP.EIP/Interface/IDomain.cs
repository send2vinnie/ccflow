using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using BP.EIP.Enum;

namespace BP.EIP.Interface
{
    public interface IDomain : IBase
    {
        /// <summary>
        /// 获取域中的用户
        /// </summary>
        /// <param name="domainId">域主键</param>
        /// <returns>数据集</returns>
        DataTable GetUsersInDomain(string domainId);
        /// <summary>
        /// 获取域中的角色
        /// </summary>
        /// <param name="domainId">域主键</param>
        /// <returns>数据集</returns>
        DataTable GetRolesInDomain(string domainId);
        /// <summary>
        /// 获取子域
        /// </summary>
        /// <param name="domainId">域主键</param>
        /// <returns>数据集</returns>
        DataTable GetSubDomain(string domainId);
        /// <summary>
        /// 获取父域
        /// </summary>
        /// <param name="domainId">域主键</param>
        /// <returns>数据集</returns>
        DataTable GetParentDomain(string domainId);
        /// <summary>
        /// 获取域中所有规则列表
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        DataTable GetRulesInDomain(string domainId);
        /// <summary>
        /// 移动域
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="parentId">父结点主键</param>
        /// <returns>影响行数</returns>
        int MoveTo(string id, string parentId);
        /// <summary>
        /// 批量移动域
        /// </summary>
        /// <param name="ids">主键组</param>
        /// <param name="parentId">父结点主键</param>
        /// <returns>影响行数</returns>
        int BatchMoveTo(string[] ids, string parentId);
    }
}
