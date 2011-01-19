using System;
using System.Web;
using System.Data;
using System.Text;

namespace Tax666.Common
{
    /// <summary>
    /// 系统提示，同时终止页面传输及响应
    /// </summary>
    public class Terminator
    {
        #region 页面输出字符串
        private void Echo(string s)
        {
            HttpContext.Current.Response.Write(s);
        }
        #endregion

        #region 终止页面输出显示
        /// <summary>
        /// 终止页面输出显示
        /// </summary>
        private void End()
        {
            HttpContext.Current.Response.End();
        }
        #endregion

        #region Javascript的Alert提示
        /// <summary>
        /// alert javascript
        /// </summary>
        /// <param name="s"></param>
        public virtual void Alert(string s)
        {
            Echo("<script language='javascript'>alert('" + s.Replace("'", @"\'") + "');history.back();</script>");
            End();
        }

        /// <summary>
        /// 页面地址跳转设置
        /// </summary>
        /// <param name="s">提示字符串</param>
        /// <param name="backurl">跳转地址</param>
        public virtual void Alert(string s, string backurl)
        {
            Echo("<script language='javascript'>alert('" + s.Replace("'", @"\'") + "');location.href='" + backurl + "';</script>");
            End();
        }
        #endregion

        #region 抛出异常信息
        /// <summary>
        /// 抛出异常信息
        /// </summary>
        /// <param name="message">定义异常信息</param>
        public virtual void Throw(string message)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            string linkurl = "<li><a href='" + WebBootPath + "/Default.aspx'>返回主页</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            Throw(message, null, linkurl, null, true);
        }
        #endregion

        #region 抛出异常信息
        /// <summary>
        /// 抛出异常信息
        /// </summary>
        /// <param name="message">定义异常信息</param>
        public virtual void ThrowError(string message)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            Throw(message, null, null, null, true);
        }

        /// <summary>
        /// 抛出异常信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="retUrl">返回的链接地址</param>
        public virtual void ThrowError(string message, string retUrl)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            Throw(message, null, retUrl, null, true);
        }
        #endregion

        #region 输出指定的提示信息
        /// <summary>
        /// 输出指定的提示信息
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">标题</param>
        /// <param name="links">链接地址</param>
        /// <param name="autojump">自动跳转定向地址</param>
        /// <param name="showback">是否显示返回链接</param>
        public virtual void Throw(string message, string title, string links, string autojump, bool showback)
        {
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.AddHeader("Content-Type", "text/html");

            StringBuilder sb = new StringBuilder(template);

            sb.Replace("{$Message}", message);
            sb.Replace("{$Title}", (title == null || title == "") ? "系统提示" : title);

            if (links != null && links != "" && !showback)
            {
                string s = "<li style='padding-left:20px;'>" + links + "</li>";
                sb.Replace("{$Links}", s);
            }

            if (autojump != null && autojump != string.Empty)
            {
                string s = autojump == "back" ? "javascript:history.back()" : autojump;
                //5秒钟后进行页面跳转；
                sb.Replace("{$AutoJump}", "<meta http-equiv='refresh' content='5; url=" + s + "' />");
            }
            else
            {
                sb.Replace("{$AutoJump}", "<!-- no jump -->");
            }

            if (showback)
            {
                if (links != null)
                    sb.Replace("{$Links}", "<a href='" + links + "'>返回上一页</a></li>");
                else
                    sb.Replace("{$Links}", "<li><a href='javascript:history.back()'>返回上一页</a></li>");
            }
            else
            {
                sb.Replace("{$Links}", "<!-- no back -->");
            }
            Echo(sb.ToString());
            End();
        }
        #endregion

        #region 页面终止页面模板
        /// <summary>
        /// 页面终止页面模板
        /// </summary>
        public virtual string template
        {
            get
            {
                return @"<html xmlns:v>
				<head>
				<title>{$Title}</title>
				<meta http-equiv='Content-Type' content='text/html; charset=" + Encoding.Default.BodyName + @"' />
				<meta name='description' content='.NET类库 页面中止程序' />
				<meta name='copyright' content='http://www.zmsoft.net/' />
				<meta name='generator' content='vs2005' />
				<meta name='usefor' content='application termination' />
				{$AutoJump}
				<style rel='stylesheet'>
				v\:*	{
					behavior:url(#default#vml);
				}

				body, div, span, li, td, a {
					color: #222222;
					font-size: 12px !important;
					font-size: 11px;
					font-family: tahoma, arial, 'courier new', verdana, sans-serif;
					line-height: 19px;
				}
				a {
					color: #2c78c5;
					text-decoration: none;
				}
				a:hover {
					color: red;
					text-decoration: none;
				}
				</style>
				</head>
				<body style='text-align:center;margin:90px 20px 50px 20px'>
				<?xml:namespace prefix='v' />
				<div style='margin:auto; width:450px; text-align:center'>
					<v:roundrect style='text-align:left; display:table; margin:auto; padding:15px; width:450px; height:210px; overflow:hidden; position:relative;' arcsize='3200f' coordsize='21600,21600' fillcolor='#fdfdfd' strokecolor='#e6e6e6' strokeweight='1px'>
						<table width='100%' cellpadding='0' cellspacing='0' border='0' style='padding-bottom:6px; border-bottom:1px #cccccc solid'>
							<tr>
								<td><b>{$Title}</b></td>
								<td align='right' style='color:#c0c0c0'>--- 智盟软件·365联盟系统【积分银行】</td>
							</tr>
						</table>
						<table width='100%' cellpadding='0' cellspacing='0' border='0' style='word-break:break-all; overflow:hidden'>
							<tr>
								<td width='80' valign='top' style='padding-top:13px'><font style='font-size:16px; zoom:4; color:#aaaaaa;font-family:webdings;'>i</font></td>
								<td valign='top' style='padding-top:17px'>
									<p style='margin-bottom:22px'>{$Message}</p>
									{$Links}
								</td>
							</tr>
						</table>
					</v:roundrect>
				</div>
				</body>
				</html>";
            }
        }
        #endregion

        #region 返回系统安装后的Web根目录虚拟根路径(/Tax666 )
        /// <summary>
        /// 返回系统安装后的Web根目录虚拟根路径(/Tax666 )。
        /// </summary>
        public static string WebBootPath
        {
            get
            {
                if (!HttpContext.Current.Request.Url.IsDefaultPort)
                {
                    return @"http://" + string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString());
                }
                else
                {
                    return @"http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                }
            }
        }
        #endregion

    }
}
