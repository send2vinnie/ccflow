using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BP.EIP.Interface
{
    public interface IBase
    {
        /// <summary>
        /// 根据No判断是否存在
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        bool Exists(string No);
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状态信息</param>
        void Add(BaseEntity entity, out string statusCode, out string statusMessage);
        /// <summary>
        /// 根据No获取实体
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        BaseEntity GetEntity(string No);
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <returns></returns>
        DataTable GetDT();
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <returns></returns>
        DataTable GetDT(string[] Ids);
        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int Delete(string Id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        int BatchDelete(string[] ids);
        /// <summary>
        /// 批量做删除标志
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        int SetDeleted(string[] ids);
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="search">查询字符</param>
        /// <returns>数据表</returns>
        DataTable Search(string searchValue);
        /// <summary>
        /// 更新一个
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>影响行数</returns>
        int Update(BaseEntity entity, out string statusCode, out string statusMessage);
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="entityList">实体列表</param>
        /// <returns>影响行数</returns>
        int BatchSave(List<BaseEntity> entityList);
    }
}
