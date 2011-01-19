namespace Tax666.Common
{
    #region 枚举类型

    /// <summary>
    /// 数据库软件类型
    /// </summary>
    public enum DbProviderEnum
    {
        Access = 0,
        SqlServer = 1,
        Oracle = 2
    }

    /// <summary>
    /// QueryString值的检查
    /// </summary>
    public enum CheckGetEnum
    {
        Int = 0,
        Safety = 1
    }

    /// <summary>
    /// 使用标签
    /// </summary>
    public enum UsingTagEnum
    {
        None = 0,
        Ubb = 1,
        Html = 2
    }

    /// <summary>
    /// 删除文件路径类型
    /// </summary>
    public enum DeleteFilePathType
    {
        /// <summary>
        /// 物理路径
        /// </summary>
        PhysicsPath = 1,
        /// <summary>
        /// 虚拟路径
        /// </summary>
        DummyPath = 2,
        /// <summary>
        /// 当前目录
        /// </summary>
        NowDirectoryPath = 3
    }

    /// <summary>
    /// 获取方式
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// Post方式
        /// </summary>
        Post = 1,
        /// <summary>
        /// Get方式
        /// </summary>
        Get = 2
    }

    /// <summary>
    /// 获取数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 字符
        /// </summary>
        Str = 1,
        /// <summary>
        /// 日期
        /// </summary>
        Dat = 2,
        /// <summary>
        /// 整型
        /// </summary>
        Int = 3,
        /// <summary>
        /// 长整型
        /// </summary>
        Long = 4,
        /// <summary>
        /// 双精度小数
        /// </summary>
        Double = 5,
        /// <summary>
        /// 只限字符和数字
        /// </summary>
        CharAndNum = 6,
        /// <summary>
        /// 只限邮件地址
        /// </summary>
        Email = 7,
        /// <summary>
        /// 只限字符和数字和中文
        /// </summary>
        CharAndNumAndChinese = 8

    }

    /// <summary>
    /// 表操作方法
    /// </summary>
    public enum DataTable_Action
    {
        /// <summary>
        /// 插入
        /// </summary>
        Insert = 0,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2
    }
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum OnlineCountType
    {
        /// <summary>
        /// 缓存
        /// </summary>
        Cache = 0,
        /// <summary>
        /// 数据库
        /// </summary>
        DataBase = 1
    }

    /// <summary>
    /// 提示Icon类型
    /// </summary>
    public enum Icon_Type : byte
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        OK,
        /// <summary>
        /// 操作标示
        /// </summary>
        Alert,
        /// <summary>
        /// 操作失败
        /// </summary>
        Error
    }

    /// <summary>
    /// 按钮链接类型
    /// </summary>
    public enum UrlType : byte
    {
        /// <summary>
        /// 超级链接
        /// </summary>
        Href,
        /// <summary>
        /// JavaScript 脚本
        /// </summary>
        JavaScript,
        /// <summary>
        /// VBScript 脚本
        /// </summary>
        VBScript
    }

    #endregion
}
