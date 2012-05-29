using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using BP.EIP.Enum;

namespace BP.EIP.Interface
{
    public interface IStaff : IBase
    {
        /// <summary>
        /// 获取内部通讯录
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <param name="search">查询内容</param>
        /// <returns>数据表</returns>
        DataTable GetAddressDT(string departmentId, string searchValue);
        /// <summary>
        /// 获取内部通讯录
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="DepartmentId">部门主键</param>
        /// <param name="containChildren">含子部门</param>
        /// <returns>数据表</returns>
        DataTable GetAddressDT(string departmentId, bool containChildren);
        /// <summary>
        /// 更新通讯地址
        /// </summary>
        /// <param name="staffEntity">实体</param>
        /// <returns>影响行数</returns>
        int UpdateAddress(Port_Staff staffEntity, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 更新通讯地址
        /// </summary>
        /// <param name="staffEntites">实体</param>
        /// <returns>影响行数</returns>
        int BatchUpdateAddress(List<Port_Staff> staffEntites, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 设置职员关联的用户
        /// </summary>
        /// <param name="staffId">职员主键</param>
        /// <param name="userId">用户主键</param>
        /// <returns>影响行数</returns>
        int SetStaffUser(string staffId, string userId);
        /// <summary>
        /// 删除职员关联的用户
        /// </summary>
        /// <param name="staffId">职员主键</param>
        /// <returns>影响行数</returns>
        int DeleteStaffUser(string staffId);
        /// <summary>
        /// 移动数据
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <returns>影响行数</returns>
        int MoveTo(string id, string departmentId);
        /// <summary>
        /// 批量移动数据
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <param name="departmentId">组织机构主键</param>
        /// <returns>影响行数</returns>
        int BatchMoveTo(string[] ids, string departmentId);


    }
}
