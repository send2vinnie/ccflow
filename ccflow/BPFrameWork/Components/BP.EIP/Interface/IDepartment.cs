using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BP.EIP.Interface
{
    public interface IDepartment:IBase
    {
        /// <summary>
        /// 获得上级部门列表
        /// </summary>
        /// <param name="parentId">父亲节点主键</param>
        /// <returns>数据表</returns>
        DataTable GetDTByParent(string parentId);
        /// <summary>
        /// 获取内部部门列表
        /// </summary>
        /// <param name="departmentId">主键</param>
        /// <returns>数据表</returns>
        DataTable GetDTInner(string departmentId);
        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="departmentId">主键</param>
        /// <returns>数据表</returns>
        DataTable GetStaffs(string departmentId);
        /// <summary>
        /// 获取上级部门成员
        /// </summary>
        /// <param name="parentId">主键</param>
        /// <returns>数据表</returns>
        DataTable GetParentStaffs(string parentId);
        /// <summary>
        /// 移动数据
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <param name="parentId">父结点主键</param>
        /// <returns>影响行数</returns>
        int MoveTo(string departmentId, string parentId);
        /// <summary>
        /// 批量移动数据
        /// </summary>
        /// <param name="departmentIds">部门主键</param>
        /// <param name="parentId">父结点主键</param>
        /// <returns>影响行数</returns>
        int BatchMoveTo(string[] departmentIds, string parentId);
        /// <summary>
        /// 保存组织机构编号
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <param name="codes">编号数组</param>
        /// <returns>影响行数</returns>
        int BatchSetCode(string[] ids, string[] codes);
    }
}
