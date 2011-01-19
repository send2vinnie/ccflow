using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.DA;

namespace BP.Web.UC
{
    /// <summary>
    /// YG 的摘要说明。
    /// </summary>
    public class UCBase3 : BP.Web.UC.UCBase2
    {
        #region 信息块- 套用       
        public void DivInfoBlockBegin()
        {
            string path = this.Request.ApplicationPath;

            this.Add("\n<table cellspacing='0' align=left>");
            this.Add("\n<tr class='yj_style'>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/tl_df.jpg'></td>");
            this.Add("\n<td style='border-top:1px #ccc solid;background:#f9f9f9;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/tr_df.jpg'>");
            this.Add("\n</td></tr>");
            this.Add("\n<tr><td width=4 class='line_l' style='border-left:1px #ccc solid;background:#f9f9f9;'></td><td width='100%' style='background:#f9f9f9;' align=left >");
        }

        public void DivInfoBlockEnd()
        {
            string path = this.Request.ApplicationPath;

            this.Add("\n</td><td width=4 class='line_l' style='border-right:1px #ccc solid;background:#f9f9f9;'></td>");
            this.Add("\n</tr>");
            this.Add("\n<tr class='yj_style'>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/bl_df.jpg'></td>");
            this.Add("\n<td style='border-bottom:1px #ccc solid;background:#f9f9f9;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/br_df.jpg'>");
            this.Add("\n</td></tr>");
            this.AddTableEnd();
        }

        #endregion 信息块- 套用


        #region AddMsgGreen
        public void AddMsgGreen(string title, string msg)
        {

            this.DivInfoBlock(title, msg);
            //this.AddTableGreen();
            //this.AddTableBarGreen(title, 1);
            //if (msg != null)
            //{
            //    this.AddTR();
            //    this.Add("<TD class=BigDoc >" + msg + "</TD>");
            //    this.AddTREnd();
            //}
            //this.AddTableEnd();
        }

        public void AddMsgInfo(string title, string msg)
        {

            this.DivInfoBlockRed(title, msg);

            //this.AddTable();

            //this.AddTR();
            //this.Add("<TD class=TitleDef >" + title + "</TD>");
            //this.AddTREnd();

            //this.AddTR();
            //this.Add("<TD class=BigDoc >" + msg + "</TD>");
            //this.AddTREnd();

            //this.AddTableEnd();
        }
        #endregion

        #region 信息块- 函数
        /// <summary>
        /// 默认的信息块 
        /// </summary>
        /// <param name="html">html信息</param>
        public void DivInfoBlock(string html)
        {
            this.DivInfoBlockBegin();
            this.Add(html);
            this.DivInfoBlockEnd();
        }
        /// <summary>
        /// 默认的信息块
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="html">内容</param>
        public void DivInfoBlock(string title, string html)
        {
            this.DivInfoBlockBegin();
            this.Add("<b>"+title+"</b><br>");
            this.Add(html);
            this.DivInfoBlockEnd();
        }
        public void DivInfoBlockRed(string html)
        {
            DivInfoBlockRed(null, html);
        }
        /// <summary>
        /// 红色的信息块 
        /// </summary>
        /// <param name="html">html信息</param>
        public void DivInfoBlockRed(string title, string html)
        {
            string path = this.Request.ApplicationPath;
            this.Add("\n<table  cellspacing='0'>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/tl_red.jpg'></td>");
            this.Add("\n<td style='border-top:1px #ffb9b6 solid;background:#ffebea;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/tr_red.jpg'>");
            this.Add("\n</td></tr>");
            this.Add("\n<tr><td width=4 class='line_l' style='border-left:1px #ffb9b6 solid;background:#ffebea;'></td><td width='100%' style='background:#ffebea;'>");

            if (title != null)
                this.Add("<b>" + title + "</b><br>");

            this.Add(html);

            this.Add("\n</td><td width=4 class='line_l' style='border-right:1px #ffb9b6 solid;background:#ffebea;'></td>");
            this.Add("\n</tr>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/bl_red.jpg'></td>");
            this.Add("\n<td style='border-bottom:1px #ffb9b6 solid;background:#ffebea;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/br_red.jpg'>");
            this.Add("\n</td></tr>");
            this.AddTableEnd();
        }
        /// <summary>
        /// 绿色的信息块 
        /// </summary>
        /// <param name="html">html信息</param>
        public void DivInfoBlockGreen(string html)
        {
            string path = this.Request.ApplicationPath;
            this.Add("\n<table cellspacing='0'>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/tl_green.jpg'></td>");
            this.Add("\n<td style='border-top:1px #b5d95e solid;background:#efffc9;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/tr_green.jpg'>");
            this.Add("\n</td></tr>");
            this.Add("\n<tr><td width=4 class='line_l' style='border-left:1px #b5d95e solid;background:#efffc9;'></td><td width='100%' style='background:#efffc9;'>");
            this.Add(html);
            this.Add("\n</td><td width=4 class='line_l' style='border-right:1px #b5d95e solid;background:#efffc9;'></td>");
            this.Add("\n</tr>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/bl_green.jpg'></td>");
            this.Add("\n<td style='border-bottom:1px #b5d95e solid;background:#efffc9;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/br_green.jpg'>");
            this.Add("\n</td></tr>");
            this.AddTableEnd();
        }
        /// <summary>
        /// 蓝色的信息块 
        /// </summary>
        /// <param name="html">html信息</param>
        public void DivInfoBlockBlue(string html)
        {
            string path = this.Request.ApplicationPath;
            this.Add("\n<table  cellspacing='0'>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/tl_Blue.jpg'></td>");
            this.Add("\n<td style='border-top:1px #b5e8fa solid;background:#f0fbff;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/tr_Blue.jpg'>");
            this.Add("\n</td></tr>");
            this.Add("\n<tr><td width=4 class='line_l' style='border-left:1px #b5e8fa solid;background:#f0fbff;'></td><td width='100%' style='background:#f0fbff;'>");
            this.Add(html);
            this.Add("\n</td><td width=4 class='line_l' style='border-right:1px #b5e8fa solid;background:#f0fbff;'></td>");
            this.Add("\n</tr>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/bl_Blue.jpg'></td>");
            this.Add("\n<td style='border-bottom:1px #b5e8fa solid;background:#f0fbff;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/br_Blue.jpg'>");
            this.Add("\n</td></tr>");
            this.AddTableEnd();

        }
        /// <summary>
        /// 黄色的信息块 
        /// </summary>
        /// <param name="html">html信息</param>
        public void DivInfoBlockYellow(string html)
        {
            string path = this.Request.ApplicationPath;
            this.Add("\n<table  cellspacing='0'>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/tl_yellow.jpg'></td>");
            this.Add("\n<td style='border-top:1px #f1e167 solid;background:#fffce5;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/tr_yellow.jpg'>");
            this.Add("\n</td></tr>");
            this.Add("\n<tr><td width=4 class='line_l' style='border-left:1px #f1e167 solid;background:#fffce5;'></td><td width='100%' style='background:#fffce5;'>");
            this.Add(html);
            this.Add("\n</td><td width=4 class='line_l' style='border-right:1px #f1e167 solid;background:#fffce5;'></td>");
            this.Add("\n</tr>");
            this.Add("\n<tr>");
            this.Add("\n<td style='text-align:right'><img src='" + path + "/Images/Div/bl_yellow.jpg'></td>");
            this.Add("\n<td style='border-bottom:1px #f1e167 solid;background:#fffce5;'></td>");
            this.Add("\n<td>");
            this.Add("\n<img src='" + path + "/Images/Div/br_yellow.jpg'>");
            this.Add("\n</td></tr>");
            this.AddTableEnd();

        }
        #endregion 信息块

        #region 菜单
        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="selectVal"></param>
        public void Menu(BP.XML.XmlMenus ens, string selectVal)
        {
            this.Add("\n<Table style='border-bottom:1px #96c1cc solid;border-collapse:collapse;' cellpadding='0' cellspacing='1' >");
            this.Add("\n<TR>");
            this.Add("\n<TD width='2%' ></TD>");
            foreach (BP.XML.XmlMenu en in ens)
            {
                if (selectVal == en.No)
                    this.Add("\n<TD class=MenuS><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
                else
                    this.Add("\n<TD class=Menu ><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
            }
            this.Add("\n<TD ></TD>");
            this.Add("\n</TR>");
            this.AddTableEnd();
        }

        /// <summary>
        /// 显示菜单Red
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="selectVal"></param>
        public void MenuRed(BP.XML.XmlMenus ens, string selectVal)
        {
            this.Add("\n<Table style='border-bottom:1px #75001b solid;' cellpadding='0' cellspacing='0' >");
            this.Add("\n<TR>");
            this.Add("\n<TD width='2%' ></TD>");
            foreach (BP.XML.XmlMenu en in ens)
            {
                if (selectVal == en.No)
                    this.Add("\n<TD class=MenuS><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
                else
                    this.Add("\n<TD class=MenuRed><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
            }
            this.Add("\n<TD  ></TD>");
            this.Add("\n</TR>");
            this.AddTableEnd();
        }

        /// <summary>
        /// 显示菜单 Green
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="selectVal"></param>
        public void MenuGreen(BP.XML.XmlMenus ens, string selectVal)
        {
            this.Add("\n<Table width='100%' style='border-bottom:1px #5c8a0b solid;' cellpadding='0' cellspacing='0' >");
            this.Add("\n<TR>");
            this.Add("\n<TD width='24%' ></TD>");
            foreach (BP.XML.XmlMenu en in ens)
            {
                if (selectVal == en.No)
                    this.Add("\n<TD class=MenuS><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
                else
                    this.Add("\n<TD class=MenuGreen><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
            }
            this.Add("\n<TD   ></TD>");
            this.Add("\n</TR>");
            this.AddTableEnd();
        }

        /// <summary>
        /// 显示菜单 Blue
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="selectVal"></param>
        public void MenuBlue(BP.XML.XmlMenus ens, string selectVal)
        {
            this.Add("\n<Table style='border-bottom:1px #4d71c3 solid;' cellpadding='0' cellspacing='0' >");
            this.Add("\n<TR>");
            this.Add("\n<TD width='2%' ></TD>");
            foreach (BP.XML.XmlMenu en in ens)
            {
                if (selectVal == en.No)
                    this.Add("\n<TD class=MenuS><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
                else
                    this.Add("\n<TD class=MenuBlue><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
            }
            this.Add("\n<TD ></TD>");
            this.Add("\n</TR>");
            this.AddTableEnd();
        }

        /// <summary>
        /// 显示菜单 Yellow
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="selectVal"></param>
        public void MenuYellow(BP.XML.XmlMenus ens, string selectVal)
        {
            this.Add("\n<Table style='border-bottom:1px #ffcc00 solid;' cellpadding='0' cellspacing='0' >");
            this.Add("\n<TR>");
            this.Add("\n<TD width='2%' ></TD>");
            foreach (BP.XML.XmlMenu en in ens)
            {
                if (selectVal == en.No)
                    this.Add("\n<TD class=MenuS><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
                else
                    this.Add("\n<TD class=MenuYellow><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
            }
            this.Add("\n<TD ></TD>");
            this.Add("\n</TR>");
            this.AddTableEnd();
        }



        /// <summary>
        /// 显示菜单 Win7
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="selectVal"></param>
        public void MenuWin7(BP.XML.XmlMenus ens, string selectVal)
        {
            this.Add("\n<Table  cellpadding='0' cellspacing='0' >");
            this.Add("\n<TR>");
            this.Add("\n<TD width='2%' ></TD>");
            foreach (BP.XML.XmlMenu en in ens)
            {
                if (selectVal == en.No)
                    this.Add("\n<TD class=MenuWin7S><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
                else
                    this.Add("\n<TD class=MenuWin7><a href='" + en.Url + "' target=" + en.Target + ">" + en.Name + "</a></TD>");
            }
            this.Add("\n<TD  ></TD>");
            this.Add("\n</TR>");
            this.AddTableEnd();
        }

        #endregion


        #region 生成菜单的方法 着的。
        public void MenuSelfVerticalBegin()
        {
            this.Add("<ul class=MenuSelfVertical >");
        }
        public void MenuSelfVerticalItem(string url, string lab, string target)
        {
            if (target == null)
                target = "_self";
            this.Add("\t\n<li class=MenuSelfVerticalItem ><a href=\"" + url + "\" target=" + target + " >" + lab + "</li>");
        }
        public void MenuSelfVerticalItemS(string url, string lab, string target)
        {
            if (target == null)
                target = "_self";
            this.Add("\t\n<li class=MenuSelfVerticalItemS ><a href=\"" + url + "\" target=" + target + " >" + lab + "</li>");
        }
        public void MenuSelfVerticalEnd()
        {
            this.Add("</ul>");
        }
        #endregion 生成菜单的方法 横着的。

      


        #region 生成菜单的方法 横着的。
        /// <summary>
        /// 开始增加菜单
        /// </summary>
        public void MenuSelfBegin()
        {
            this.Add("\n<Table style='width:100%;' cellpadding='5' cellspacing='5'>");
            this.Add("\n<TR>");
        }
        /// <summary>
        /// 增加一个lab
        /// </summary>
        /// <param name="attr">TD里面的属性</param>
        /// <param name="lab">标签</param>
        public void MenuSelfLab(string attr, string lab)
        {
            this.Add("\n<TD "+attr+">" + lab + "</TD>");
        }

        public void MenuSelfItem(string url, string lab, string target)
        {
                this.Add("\n<TD class=Menu><a href=\"" + url + "\" target=" + target + ">" + lab + "</a></TD>");
        }

        public void MenuSelfItemLab(string lab)
        {
            this.Add("\n<TD class=Menu>" + lab + "</TD>");
        }

        public void MenuSelfItem(string url, string lab, string target, bool selected)
        {
            if (selected)
                MenuSelfItem(url, lab, target);
            else
                MenuSelfItemS(url, lab, target);
        }
        public void MenuSelfItemS(string url, string lab, string target)
        {
            this.Add("\n<TD class=MenuS >" + lab + "</TD>");
        }
        /// <summary>
        /// 结束菜单
        /// </summary>
        public void MenuSelfEnd(int perBlankLeft)
        {
            this.Add("\n<TD width='" + perBlankLeft + "%' ></TD>");
            this.Add("\n</TR>");
            this.AddTableEnd();
        }
        /// <summary>
        /// 结束菜单
        /// </summary>
        public void MenuSelfEnd()
        {
            this.Add("\n</TR>");
            this.AddTableEnd();
        }
        #endregion 菜单



    }
}
