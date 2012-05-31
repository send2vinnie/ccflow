using System;
using System.Collections.Generic;
using System.Text;

namespace BP.DA
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DBType
    {
        /// <summary>
        /// sql 2000
        /// </summary>
        SQL2000_OK,
        /// <summary>
        /// oracle 9i 
        /// </summary>
        Oracle9i,
        /// <summary>
        /// Access
        /// </summary>
        Access,
        /// <summary>
        /// Sybase
        /// </summary>
        Sybase,
        /// <summary>
        /// DB2
        /// </summary>
        DB2,
        /// <summary>
        /// MySQL
        /// </summary>
        MySQL,
        /// <summary>
        /// InforMix
        /// </summary>
        InforMix
    }
    /// <summary>
    /// 保管位置
    /// </summary>
    public enum Depositary
    {
        /// <summary>
        /// 全体
        /// </summary>
        Application,
        /// <summary>
        /// 对话
        /// </summary>
        Session,
        /// <summary>
        /// 不保管
        /// </summary>
        None
    }
}
