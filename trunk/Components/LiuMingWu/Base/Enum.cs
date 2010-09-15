using System;

namespace BP.GE
{
    /// <summary>
    /// 自定义控件类型
    /// </summary>
    public enum UCType
    {
        /// <summary>
        /// 文本
        /// </summary>
        //TextArea,

        /// <summary>
        /// 文章类型
        /// </summary>
        TextArticle,
        /// <summary>
        /// 列表类型
        /// </summary>
        UIList,
        /// <summary>
        /// Table数据记录
        /// </summary>
        TableList,
        /// <summary>
        /// Table图片
        /// </summary>
        ImgList
    }
    /// <summary>
    /// 数据源类型
    /// </summary>
    public enum DBSourceType
    {
        /// <summary>
        /// 文本
        /// </summary>
        SQL,
        /// <summary>
        /// 从txt文件取数据
        /// </summary>
        TxtFile,
        /// <summary>
        /// 从xml取数据
        /// </summary>
        Xml,
        /// <summary>
        /// 从Entities中取数据
        /// </summary>
        Ens,
        /// <summary>
        /// DataTable中取数据
        /// </summary>
        DataTable,
        /// <summary>
        /// 从DataSet中取数据
        /// </summary>
        DataSet
    }

    public enum ULorOL
    {
        /// <summary>
        /// 有序
        /// </summary>
        OL,
        /// <summary>
        /// 无序
        /// </summary>
        UL
    }

    public enum TitleDirection
    {
        /// <summary>
        /// 左
        /// </summary>
        Right,
        /// <summary>
        /// 下
        /// </summary>
        Bottom
    }

    public enum MyEncoding
    {
        /// <summary>
        /// 
        /// </summary>
        ASCII,
        /// <summary>
        /// 
        /// </summary>
        Unicode,
        /// <summary>
        /// 
        /// </summary>
        UTF8
    }
    /// <summary>
    /// URL参数类型
    /// </summary>
    public enum ParamType
    {
        /// <summary>
        /// 数据行
        /// </summary>
        DataRow,
        /// <summary>
        /// 查询字符
        /// </summary>
        QueryString,
        /// <summary>
        /// session
        /// </summary>
        Session,
        /// <summary>
        /// 类中获取
        /// </summary>
        Property
    }

    /// <summary>
    /// 左右
    /// </summary>
    public enum enumLR
    {
        /// <summary>
        /// 
        /// </summary>
        Left,
        /// <summary>
        /// 
        /// </summary>
        Right
    }

    public enum enumTL
    {
        /// <summary>
        /// 
        /// </summary>
        Top,
        /// <summary>
        /// 
        /// </summary>
        Left
    }

    public enum eShowType
    {
        /// <summary>
        /// 
        /// </summary>
        PopLayer,
        /// <summary>
        /// 
        /// </summary>
        ShowModaldialog
    }

    public enum ePagerStyle
    {
        /// <summary>
        /// 
        /// </summary>
        Style1,
        /// <summary>
        /// 
        /// </summary>
        Style2
    }
    public enum eDisplayMode
    {
        /// <summary>
        /// 水平
        /// </summary>
        Horizontal,
        /// <summary>
        /// 垂直
        /// </summary>
        Vertical
    }
    public enum eDirection
    {
        /// <summary>
        /// 从左到右
        /// </summary>
        LeftToRight,
        /// <summary>
        /// 从右到左
        /// </summary>
        RightToLeft,
        /// <summary>
        /// 从上到下
        /// </summary>
        TopToBottom,
        /// <summary>
        /// 从下到上
        /// </summary>
        BottomToTop
    }

    public enum eMouseAction
    {
        /// <summary>
        /// 鼠标划过
        /// </summary>
        onmouseover,
        /// <summary>
        /// 鼠标点击
        /// </summary>
        onclick
    }

    /// <summary>
    /// 位置
    /// </summary>
    public enum Pos
    {
        /// <summary>
        /// 头部
        /// </summary>
        Top,
        /// <summary>
        /// 底部
        /// </summary>
        End,
        /// <summary>
        /// 左边
        /// </summary>
        Left,
        /// <summary>
        /// 右边
        /// </summary>
        Rihgt
    }
    public enum ColorStyle
    {
        /// <summary>
        /// 默认风格
        /// </summary>
        Default,
        /// <summary>
        /// 蓝色的
        /// </summary>
        Blue,
        /// <summary>
        /// 绿色的
        /// </summary>
        Green,
        /// <summary>
        /// 红色的
        /// </summary>
        Red
    }
    public enum eParentType
    {
        /// <summary>
        /// 
        /// </summary>
        UserControl,
        /// <summary>
        /// 
        /// </summary>
        WebPage
    }
}
