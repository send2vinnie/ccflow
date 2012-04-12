using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
public class CustomTreeViewItem : RadTreeViewItem
{
    public CustomTreeViewItem(TreeViewType TVType)
    {
        TreeViewType = TVType;
        switch (TVType)
        {
            case TreeViewType.Dept:

                break;
            case TreeViewType.Emp:
                break;
            default:
                break;
        }
    }

    public TreeViewType TreeViewType { set; get; }

    public CustomTreeViewItem()
    {

    }
}

public enum TreeViewType
{
    Dept = 1,
    Emp,
    Station
}