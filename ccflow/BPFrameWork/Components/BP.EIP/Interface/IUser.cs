using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using System.Data;using BP.EIP.Enum;
using BP.EIP.Enum;

namespace BP.EIP.Interface
{
    public interface IUser : IBase
    {
        /// <summary>
        /// 用户名是否重复
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool ExistsName(string userName);
        /// <summary>
        /// 根据部门查找用户
        /// </summary>
        /// <param name="departmentNo"></param>
        /// <param name="containChildren"></param>
        /// <returns></returns>
        DataTable GetDTByDept(string departmentNo, bool containChildren);
        /// <summary>
        /// 根据角色获取用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        DataTable GetDTByRole(string roleId);
        /// <summary>
        /// 根据审核状态获取用户列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        DataTable GetDTByStatus(AuditStatus status);
        /// <summary>
        /// 设置用户的默认角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns>影响行数</returns>
        int SetDefaultRole(string userId, string roleId);
        /// <summary>
        /// 批量设置用户的默认角色
        /// </summary>
        /// <param name="userIds">用户主键</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>影响的行数</returns>
        int BatchSetDefaultRole(string[] userIds, string roleId);
        /// <summary>
        /// 获取职员角色列表
        /// </summary>
        /// <param name="staffId">职员主键</param>
        /// <returns>主键数组</returns>
        string[] GetUserRoleIds(string userId);
        /// <summary>
        /// 加入到批量角色
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="staffId">职员主键</param>
        /// <param name="addToRoleIds">加入角色主键集</param>
        /// <returns>影响的行数</returns>
        int AddUserToRole(string userId, string[] addToRoleIds);
        /// <summary>
        /// 批量移出角色
        /// </summary>
        /// <param name="staffId">职员主键</param>
        /// <param name="removeRoleIds">移出角色主键集</param>
        /// <returns>影响的行数</returns>
        int RemoveUserFromRole(string userId, string[] removeRoleIds);
        /// <summary>
        /// 获得登录用户列表
        /// </summary>
        /// <returns>数据表</returns>
        DataTable GetLoginUserDT();
        /// <summary>
        /// 激活帐户
        /// </summary>
        /// <param name="uid">唯一识别码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>用户实体</returns>
        CurrentUser AccountActivation(string uid, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 按唯一识别码登录
        /// </summary>
        /// <param name="uid">唯一识别码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>用户实体</returns>
        CurrentUser LoginByUid(string uid, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 按用户名登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>用户实体</returns>
        CurrentUser LoginByUserName(string userName, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>登录实体类</returns>
        CurrentUser UserLogOn(string userName, string password, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>登录实体类</returns>
        CurrentUser UserLogOn(string userName, string password, string ipAddress, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 操作员退出应用程序
        /// </summary>
        void Exit();
        /// <summary>
        /// 检查在线状态(服务器专用)
        /// </summary>
        /// <returns>在线人数</returns>
        int ServerCheckOnLine();
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="userIds">被设置的用户主键</param>
        /// <param name="password">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>影响行数</returns>
        int SetPassword(string[] userIds, string password, out StatusCode statusCode, out string statusMessage);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="oldPassword">原始密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>影响行数</returns>
        int ChangePassword(string userId,string oldPassword, string newPassword, out StatusCode statusCode, out string statusMessage);
    }
}
