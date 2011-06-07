using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using Silverlight;

namespace FreeFrm
{
    public class Glo
    {
        public static void BindComboBoxFontSize(ComboBox cb, double selectDB)
        {
            cb.Items.Clear();
            for (int i = 6; i < 25; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = i.ToString();
                if (i.ToString() == selectDB.ToString())
                    cbi.IsSelected = true;
                cb.Items.Add(cbi);
            }
        }
        public static void BindComboBoxLineBorder(ComboBox cb, double selectDB)
        {
            cb.Items.Clear();
            for (int i = 1; i < 15; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = i.ToString();
                if (i.ToString() == selectDB.ToString())
                    cbi.IsSelected = true;
                cb.Items.Add(cbi);
            }
        }

        public static Color PreaseColor(string colorName)
        {
            switch (colorName)
            {
                case "Red":
                    return Colors.Red;
                case "Yellow":
                    return Colors.Yellow;
                case "Blue":
                    return Colors.Blue;
                case "Brown":
                    return Colors.Brown;
                case "Cyan":
                    return Colors.Cyan;
                case "DarkGray":
                    return Colors.DarkGray;
                case "Gray":
                    return Colors.Gray;
                case "Orange":
                    return Colors.Orange;
                default:
                    return Colors.Black;
            }

        }
        /// <summary>
        /// 字体类型
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="selectDB"></param>
        public static void BindComboBoxFontStyle(ComboBox cb, string selectDB)
        {
            cb.Items.Clear();
            ComboBoxItem cbi = new ComboBoxItem();
            cbi.Content = "italic";
            cb.Items.Add(cbi);

            cbi = new ComboBoxItem();
            cbi.Content = "normal";
            cb.Items.Add(cbi);

            cbi = new ComboBoxItem();
            cbi.Content = "oblique";
            cb.Items.Add(cbi);

            cbi = new ComboBoxItem();
            cbi.Content = "inherit";
            cb.Items.Add(cbi);

            foreach (ComboBoxItem li in cb.Items)
            {
                if (li.Content.ToString() == selectDB)
                {
                    li.IsSelected = true;
                    break;
                }
            }

            if (cb.SelectedIndex < 0)
                cb.SelectedIndex = 0;
        }
        public static void BindComboBoxColors(ComboBox cb, string selectDB)
        {
            cb.Items.Clear();
            ComboBoxItem cbi = new ComboBoxItem();
            cbi.Content = "Black";

            cb.Items.Add(cbi);
            cbi = new ComboBoxItem();
            cbi.Content = "Red";
            cb.Items.Add(cbi);
            cbi = new ComboBoxItem();
            cbi.Content = "Blue";
            cb.Items.Add(cbi);

            cbi = new ComboBoxItem();
            cbi.Content = "Green";
            cb.Items.Add(cbi);

            foreach (ComboBoxItem li in cb.Items)
            {
                if (li.Content.ToString() == selectDB)
                {
                    li.IsSelected = true;
                    break;
                }
            }

            if (cb.SelectedIndex < 0)
                cb.SelectedIndex = 0;
        }
        public static void WinOpen(string url)
        {
            HtmlPage.Window.Eval("window.showModalDialog('" + url + "',window,'dialogHeight:600px;dialogWidth:800px;center:Yes;help:No;scroll:auto;resizable:No;status:No;');");
        }
        public static void IE_ShowAddFGuide()
        {
            Glo.WinOpen("http://127.0.0.1/Flow/WF/MapDef/Do.aspx?DoType=AddF&MyPK=" + Glo.FK_MapData);
        }
        public static string FK_Flow
        {
            get
            {
                if (System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("FK_Flow") == false)
                    return "005";
                return System.Windows.Browser.HtmlPage.Document.QueryString["FK_Flow"];
            }
        }
        public static string FK_MapData
        {
            get
            {
                if (System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("FK_MapData") == false)
                    return "ND5";
                return System.Windows.Browser.HtmlPage.Document.QueryString["FK_MapData"];
            }
        }
    }
    public class Tools
    {
        public const string Mouse = "Mouse";
        public const string Line = "Line";
        /// <summary>
        /// 标签
        /// </summary>
        public const string Label = "Label";
        /// <summary>
        /// 连接
        /// </summary>
        public const string Link = "Link";
        /// <summary>
        /// 文本框
        /// </summary>
        public const string TextBox = "TextBox";
        /// <summary>
        /// 下拉框
        /// </summary>
        public const string DDLEnum = "DDLEnum";
        /// <summary>
        /// 数据表
        /// </summary>
        public const string DDLTable = "DDLTable";
        /// <summary>
        /// 单选按钮
        /// </summary>
        public const string RBS = "RBS";
        /// <summary>
        /// 选择框
        /// </summary>
        public const string CheckBox = "CheckBox";
        /// <summary>
        /// 图片
        /// </summary>
        public const string Img = "Img";
        public const string Dtl = "Dtl";
        public const string M2M = "M2M";
    }
}

