﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace WorkNode
{
    public class Func
    {
        #region attrs
        public const string New = "New";
        public const string Open = "Open";
        public const string Save = "Save";
        public const string Copy = "Copy";
        public const string View = "View";
        public const string Exp = "Exp";
        public const string Imp = "Imp";

        /// <summary>
        /// 图片
        /// </summary>
        public const string Alignment_Left = "Alignment_Left";
        public const string Alignment_Right = "Alignment_Right";
        public const string Alignment_Top = "Alignment_Top";
        public const string Alignment_Down = "Alignment_Down";
        public const string Alignment_Center = "Alignment_Center";
        public const string Delete = "Delete";
        public const string Undo = "Undo";
        public const string ForwardDo = "ForwardDo";
        public const string Property = "Property";
        #endregion

        #region 字段
        string _No;
        string _Name;
        BitmapImage _Img;
        #endregion

        #region 属性
        /// <summary>
        /// 图标名称
        /// </summary>
        public string No
        {
            get { return _No; }
            set { _No = value; }
        }
        /// <summary>
        /// 图标图像
        /// </summary>
        public BitmapImage Img
        {
            get { return _Img; }
            set { _Img = value; }
        }
        /// <summary>
        /// 图标文本
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        #endregion

        #region 单一实例
        public static readonly Func instance = new Func();
        #endregion

        #region 公共方法
        public List<Func> GetToolList()
        {
            List<Func> ToolList = new List<Func>()
            {
              //  new Func(){ No=Func.New, Name="新建", Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.Open+".png",UriKind.RelativeOrAbsolute))},
               // new Func(){ No=Func.Open, Name="打开", Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.Open+".png",UriKind.RelativeOrAbsolute))},

                new Func(){ No=Func.Property, Name="属性", Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.Property+".png",UriKind.RelativeOrAbsolute))},
                new Func(){ No=Func.Save, Name="保存", Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.Save+".png",UriKind.RelativeOrAbsolute))},
                new Func(){ No=Func.View,  Name="预览",Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.View+".png",UriKind.RelativeOrAbsolute))},
                //new Func(){ No=Func.Copy,  Name="复制",Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.Copy+".png",UriKind.RelativeOrAbsolute))},
                new Func(){ No=Func.Exp, Name="导出",Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.Exp+".png",UriKind.RelativeOrAbsolute))},
                new Func(){ No=Func.Imp,Name="导入", Img=new BitmapImage(new Uri("/CCForm;component/Img/"+Func.Imp+".png",UriKind.RelativeOrAbsolute))}
            };
            return ToolList;
        }
        #endregion
    }
}
