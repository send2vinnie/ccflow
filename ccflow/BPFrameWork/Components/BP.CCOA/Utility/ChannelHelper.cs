using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace BP.CCOA.Utility
{
    public class ChannelHelper : BaseHelper
    {
        /// <summary>
        /// 获取首页栏目信息
        /// </summary>
        /// <returns>栏目信息</returns>
        public Channel GetFirstChannel()
        {
            return GetFirstChannel(Guid.Empty.ToString());
        }

        /// <summary>
        /// 获取某个父栏目下的第一个子栏目（索引最大的一个子栏目）
        /// </summary>
        /// <param name="ParentID">父栏目ID</param>
        /// <returns>栏目信息</returns>
        public Channel GetFirstChannel(string ParentID)
        {
            //Order[] ods = new Order[] { new Order("Index") };
            //Criteria c = new Criteria(CriteriaType.Equals, "ParentID", ParentID);
            //List<Channel> channels = Assistant.List<Channel>(c, ods, 0, 1);
            //if (channels.Count > 0)
            //{
            //    return channels[0];
            //}
            return null;
        }

        /// <summary>
        /// 通过url获取栏目ID
        /// </summary>
        /// <param name="fullurl"></param>
        /// <returns></returns>
        public string GetChannelIDByFullUrl(string fullurl)
        {
            //Order[] ods = new Order[] { new Order("Index") };
            //Criteria c = new Criteria(CriteriaType.Equals, "FullUrl", fullurl);
            //List<Channel> channels = Assistant.List<Channel>(c, ods, 0, 1);
            //if (channels.Count > 0)
            //{
            //    return channels[0].ID;
            //}
            //else
            //    return We7Helper.NotFoundID;

            Channel channel = new Channel();
            channel.RetrieveByAttr("FullUrl", fullurl);
            if (channel != null)
            {
                return channel.No;
            }
            else
                return "";
        }

        /// <summary>
        /// 获取一个栏目的子栏目
        /// </summary>
        /// <param name="parentID">栏目ID</param>
        /// <param name="recusive">true代表所有子栏目，false代表只为这个栏目的子栏目</param>
        /// <returns></returns>
        public List<Channel> GetSubChannelList(string parentID, bool recusive)
        {
            return GetSubChannelList(parentID, recusive, false);
        }

        /// <summary>
        /// 获取一个栏目的子栏目
        /// </summary>
        /// <param name="parentID">栏目ID</param>
        /// <param name="recusive">是否只包括这个栏目的第一级子栏目</param>
        /// <param name="OnlyInUser">是否只包含启用的栏目</param>
        /// <returns></returns>
        public List<Channel> GetSubChannelList(string parentID, bool recusive, bool OnlyInUser)
        {
            //Criteria c = new Criteria(CriteriaType.Equals, "ParentID", parentID);
            //if (OnlyInUser)
            //    c.Add(CriteriaType.Equals, "State", 1);

            //Order od = new Order("Index", OrderMode.Asc);
            //List<Channel> chs = Assistant.List<Channel>(c, new Order[] { od });
            //List<Channel> allchs = Assistant.List<Channel>(c, new Order[] { od });
            //if (recusive)
            //{
            //    foreach (Channel ch in chs)
            //    {
            //        List<Channel> subs = GetSubChannelList(ch.ID, recusive);
            //        foreach (Channel sub in subs)
            //        {
            //            ch.Channels.Add(sub);
            //            allchs.Add(sub);
            //        }
            //    }
            //}
            //return allchs;

            return null;
        }

        #region 静态URL

        private static readonly string ChannelKeyName = "Channel:{0}";

        /// <summary>
        /// 通过URL取得栏目ID号
        /// </summary>
        /// <returns></returns>
        public string GetChannelIDFromURL()
        {
            HttpContext Context = HttpContext.Current;
            if (Context.Request["id"] != null)
                return Context.Request["id"];
            else
            {
                string chID = string.Empty;
                string channelUrl = GetChannelUrlFromUrl(Context.Request.RawUrl, Context.Request.ApplicationPath);
                string key = string.Format(ChannelKeyName, channelUrl);
                string channelID = (string)Context.Items[key];
                if (string.IsNullOrEmpty(channelUrl)) channelID = Guid.Empty.ToString();
                if (channelID == null || channelID.Length == 0)
                {
                    channelID = (string)Context.Cache[key];
                    if (channelID == null || channelID.Length == 0)
                    {
                        if (channelUrl != string.Empty)
                            channelID = GetChannelIDByFullUrl(channelUrl);
                        if (channelID != null && channelID.Length > 0)
                        {
                            CacherCache(key, Context, channelID, CacheTime.Short);
                        }
                    }
                    if (Context.Items[key] == null)
                    {
                        Context.Items.Remove(key);
                        Context.Items.Add(key, channelID);
                    }
                }
                return channelID;
            }
        }


        /// <summary>
        /// 通过URL取得栏目唯一名称
        /// </summary>
        /// <param name="path"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public string GetChannelNameFromUrl(string path, string app)
        {
            //GeneralConfigInfo si = GeneralConfigs.GetConfig();
            //if (si == null) return "";
            //string ext = si.UrlFormat;
            string ext = "yyyy-MM-dd";
            if (ext == null || ext.Length == 0) ext = "html";

            if (path.LastIndexOf("?") > -1)
            {
                if (path.ToLower().IndexOf("channel=") > -1)
                {
                    path = path.Substring(path.ToLower().IndexOf("channel=") + 8);
                    if (path.IndexOf("&") > -1)
                        path = path.Remove(path.IndexOf("&"));
                }
                else
                    path = path.Remove(path.LastIndexOf("?"));
            }

            if (path.ToLower().EndsWith(".aspx") || path.ToLower().EndsWith("." + ext))
                path = path.Remove(path.LastIndexOf("/") + 1);

            if (!path.StartsWith("/")) path = "/" + path;
            string mathstr = @"(?:\/(\w|\s|(-)|(_))+((\/?))?)$";
            if (Regex.IsMatch(path, mathstr))
            {
                if (!app.StartsWith("/"))
                {
                    app = "/" + app;
                }
                if (!app.EndsWith("/"))
                {
                    app += "/";
                }
                path = path.Replace("//", "/");
                if (path.ToLower().StartsWith(app.ToLower()))
                {
                    path = path.Remove(0, app.Length);
                }
                if (path.EndsWith("/"))
                {
                    path = path.Remove(path.Length - 1);
                }

                int lastSlash = path.LastIndexOf("/");
                if (lastSlash > -1)
                {
                    path = path.Remove(0, lastSlash + 1);
                }

                if (path.ToLower() == "go") path = string.Empty;
                return path;
            }
            else
                return string.Empty;
        }

        ///// <summary>
        ///// 通过url获取当前栏目名称
        ///// </summary>
        ///// <returns></returns>
        //public string GetChannelNameFromURL()
        //{
        //    HttpContext Context = HttpContext.Current;
        //    if (Context.Request["id"] != null)
        //        return Context.Request["id"];
        //    else
        //    {
        //        string chID = string.Empty;
        //        string channelName = GetChannelUrlFromUrl(Context.Request.RawUrl, Context.Request.ApplicationPath);
        //        return channelName;
        //    }
        //}

        /// <summary>
        /// 通过URL取得栏目唯一名称
        /// 2.6版修改：栏目唯一名称变更为 FullUrl
        /// </summary>
        /// <param name="path"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public string GetChannelUrlFromUrl(string path, string app)
        {
            //GeneralConfigInfo si = GeneralConfigs.GetConfig();
            //if (si == null) return "";
            //string ext = si.UrlFormat;
            string ext = "yyyy-MM-dd";
            if (ext == null || ext.Length == 0) ext = "html";

            if (path.LastIndexOf("?") > -1)
            {
                if (path.ToLower().IndexOf("channel=") > -1)
                {
                    path = path.Substring(path.ToLower().IndexOf("channel=") + 8);
                    if (path.IndexOf("&") > -1)
                        path = path.Remove(path.IndexOf("&"));
                }
                else
                    path = path.Remove(path.LastIndexOf("?"));
            }

            if (path.ToLower().EndsWith(".aspx") || path.ToLower().EndsWith("." + ext))
                path = path.Remove(path.LastIndexOf("/") + 1);

            if (!path.StartsWith("/")) path = "/" + path;
            string mathstr = @"(?:\/(\w|\s|(-)|(_))+((\/?))?)$";
            if (Regex.IsMatch(path, mathstr))
            {
                if (!app.StartsWith("/"))
                {
                    app = "/" + app;
                }
                if (!app.EndsWith("/"))
                {
                    app += "/";
                }
                path = path.Replace("//", "/");
                if (path.ToLower().StartsWith(app.ToLower()))
                {
                    path = path.Remove(0, app.Length);
                }
                //if (path.EndsWith("/"))
                //{
                //    path = path.Remove(path.Length - 1);
                //}

                //int lastSlash = path.LastIndexOf("/");
                //if (lastSlash > -1)
                //{
                //    path = path.Remove(0, lastSlash + 1);
                //}

                if (path.ToLower() == "go") path = string.Empty;
                if (!path.EndsWith("/")) path += "/";
                if (!path.StartsWith("/")) path = "/" + path;
                return path;
            }
            else
                return string.Empty;
        }

        #endregion
    }
}
