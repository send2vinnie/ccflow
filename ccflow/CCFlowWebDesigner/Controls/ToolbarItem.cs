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

namespace WF.Controls
{
    public class ToolbarItem
    {

        public string No { get; set; }
 
        public string Name { get; set; }
 
        #region 单一实例
        public static readonly ToolbarItem Instance = new ToolbarItem();
        #endregion

        #region 公共方法
        public List<ToolbarItem> GetLists()
        {
            List<ToolbarItem> ToolList = new List<ToolbarItem>()
            {
                new ToolbarItem(){No="ToolBarLogin", Name = " 登录"},
                new ToolbarItem(){No="ToolBarNewNode", Name="添加节点"},
                new ToolbarItem(){No="ToolBarNewLine", Name="添加连线"},
                new ToolbarItem(){No="ToolBarNewLabel", Name="添加标签"},
                new ToolbarItem(){No="ToolBarSave", Name="保存"},
                new ToolbarItem(){No="ToolBarDesignReport", Name="设计报表"},
                new ToolbarItem(){No="ToolBarCheck", Name="检查"},
                new ToolbarItem(){No="ToolBarRun", Name="运行"},
                new ToolbarItem(){No="ToolBarEditFlow", Name="属性"},
                new ToolbarItem(){No="ToolBarDeleteFlow", Name="删除"},
                new ToolbarItem(){No="ToolBarGenerateModel", Name="导出"},
                new ToolbarItem(){No="ToolBarReleaseToFTP", Name="发布到FTP"},
                new ToolbarItem(){No="ToolBarHelp", Name="帮助"}
                
            };
            return ToolList;
        }
        #endregion
    }
}

