using System;
using System.Collections.Generic;
using System.Text;

namespace Tax666.WebControls
{
    #region 权限类型
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PopedomType
    {
        /// <summary>
        /// 浏览
        /// </summary>
        List = 2,
        /// <summary>
        /// 添加
        /// </summary>
        New = 4,
        /// <summary>
        /// 编辑
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
        /// 有效/无效
        /// </summary>
        Valid = 128,
        /// <summary>
        /// 审核
        /// </summary>
        Audit = 256,
        /// <summary>
        /// 搜索
        /// </summary>
        Search = 512,
        /// <summary>
        /// 移动
        /// </summary>
        Move = 1024,
        /// <summary>
        /// 下载
        /// </summary>
        Download = 2048,
        /// <summary>
        /// 上传
        /// </summary>
        Upload = 4096,
        /// <summary>
        /// 导出
        /// </summary>
        Output = 8192,
        /// <summary>
        /// 导入
        /// </summary>
        Input = 16384,
        /// <summary>
        /// 报表
        /// </summary>
        Report = 32768,
        /// <summary>
        /// 备份
        /// </summary>
        Backup = 65536,
        /// <summary>
        /// 恢复
        /// </summary>
        Restore = 131072,
        /// <summary>
        /// 授权
        /// </summary>
        Grint = 262144,
        /// <summary>
        /// 其它
        /// </summary>
        Other = 1
    }
    #endregion
}
