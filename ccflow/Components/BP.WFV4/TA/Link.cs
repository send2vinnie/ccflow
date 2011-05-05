using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.OA
{
	/// <summary>
	/// 链接属性
	/// </summary>
    public class LinkAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 回复消息
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// 记录人
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// 文件夹
        /// </summary>
        public const string Target = "Target";
    }
	/// <summary>
	/// 链接
	/// </summary> 
	public class Link : EntityNoName
	{
		#region 基本属性
        public string Target
        {
            get
            {
                return this.GetValStringByKey(LinkAttr.Target);
            }
            set
            {
                SetValByKey(LinkAttr.Target, value);
            }
        }
		public string Note
		{
			get
			{
				return this.GetValStringByKey(LinkAttr.Note);
			}
			set
			{
				SetValByKey(LinkAttr.Note,value);
			}
		}
		public string Url
		{
			get
			{
                return this.GetValStringByKey(LinkAttr.Url); 
			}
			set
			{
				SetValByKey(LinkAttr.Url,value);
			}
		}
		#endregion
 
		#region 构造函数
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}

		/// <summary>
		/// 链接
		/// </summary>
		public Link()
		{
		  
		}
		/// <summary>
		/// Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("TA_Link");
                map.EnDesc = "链接";
                map.CodeStruct = "3";
                map.IsAutoGenerNo = true;
                //map.Icon="./Images/Link_s.ico";
                //map.Icon = "../TA/Images/Link_s.ico";

                map.AddTBStringPK(LinkAttr.No, null, "编号", true, true, 3, 3, 3);
                map.AddTBString(LinkAttr.Name, null, "标题", true, false, 0, 50, 10);
                map.AddTBString(LinkAttr.Url, null, "URL", true, false, 0, 50, 10);
                map.AddDDLSysEnum(LinkAttr.Target, 0, "目标", true, true, LinkAttr.Target, "@0=新窗口@1=本窗口@2=父窗口");
                map.AddTBString(LinkAttr.Note, null, "说明", true, false, 0, 50, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// 链接s
	/// </summary> 
	public class Links: Entities
	{
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Link();
			}
		}
		public override int RetrieveAll()
		{
            int i = base.RetrieveAll();
            if (i == 0)
            {
                Link lk = new Link();
                lk.Name = "驰骋工作流引擎";
                lk.Url = "http://ccflow.cn";
                lk.Note = "转到驰骋工作流引擎主页，您可以获取流程帮助。";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "ftp服务器";
                lk.Url = "../../OA/Do.aspx?DoType=GotoFtp";
                lk.Note = "转到内部网的ftp服务器上。";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "新浪";
                lk.Url = "http://sina.com.cn";
                lk.Note = "新浪新闻，邮件，咨询，财经。";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "搜狐";
                lk.Url = "http://shou.com.cn";
                lk.Note = "搜狐新闻，邮件，咨询，财经。";
                lk.No = lk.GenerNewNo;
                lk.Insert();


                lk = new Link();
                lk.Name = "谷歌搜索";
                lk.Url = "http://google.com.hk";
                lk.Note = "谷歌搜索、翻译、gtalk、gmail。";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "百度";
                lk.Url = "http://baidu.com";
                lk.Note = "百度搜索、博客。";
                lk.No = lk.GenerNewNo;
                lk.Insert();
            }
            return i;
		}
		/// <summary>
		/// Links
		/// </summary>
		public Links()
		{
		}
		/// <summary>
		/// Links
		/// </summary>
		public Links(string Url)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(LinkAttr.Url, Url);
			qo.DoQuery();			
		}
	}
}
 