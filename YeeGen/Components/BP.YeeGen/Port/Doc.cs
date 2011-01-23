using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.YG
{
	public class DocAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// 创建人
		/// </summary>
		public const string Cent="Cent";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string FK_Member="FK_Member";
        /// <summary>
        /// 内容
        /// </summary>
		public const string Doc="Doc";
        /// <summary>
        /// 关键字
        /// </summary>
		public const string KeyWords="KeyWords";
        /// <summary>
        /// 记录日期
        /// </summary>
		public const string RDT="RDT";
        /// <summary>
        /// 修改时间
        /// </summary>
        public const string EDT = "EDT";

        /// <summary>
        /// 阅读次数
        /// </summary>
        public const string ReadTimes = "ReadTimes";
	}
	/// <summary>
	/// 会员文章
	/// </summary>
	public class Doc :EntityOIDName
	{
		#region cent .
        public int ReadTimes
        {
            get
            {
                return this.GetValIntByKey(DocAttr.ReadTimes);
            }
            set
            {
                this.SetValByKey(DocAttr.ReadTimes, value);
            }
        }
		public string DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(DocAttr.Doc);
			}
			set
			{
				this.SetValByKey(DocAttr.Doc,value);
			}
		}
		public string FK_Member
		{
			get
			{
				return this.GetValStringByKey(DocAttr.FK_Member);
			}
			set
			{
				this.SetValByKey(DocAttr.FK_Member,value);
			}
		}
		public string KeyWords
		{
			get
			{
				return this.GetValStringByKey(DocAttr.KeyWords);
			}
			set
			{
				this.SetValByKey(DocAttr.KeyWords,value);
			}
		}
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(DocAttr.RDT);
			}
			set
			{
				this.SetValByKey(DocAttr.RDT,value);
			}
		}
        public string EDT
        {
            get
            {
                return this.GetValStringByKey(DocAttr.EDT);
            }
            set
            {
                this.SetValByKey(DocAttr.EDT, value);
            }
        }
		#endregion

		#region 实现基本的方法
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// FLinkMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map();

				#region 基本属性 
				map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; 
				map.PhysicsTable="YG_Doc";
				map.AdjunctType = AdjunctType.AllType ;  
				map.DepositaryOfMap=Depositary.Application; 
				map.DepositaryOfEntity=Depositary.None;
				map.EnDesc="会员文章";
				map.EnType=EnType.App;

                map.AddTBIntPKOID();

                map.AddTBString(DocAttr.Name, null, "标题", true, false, 1, 100, 100);
                map.AddTBStringDoc();// ("文章内容");
                map.AddTBString(DocAttr.KeyWords, null, "KeyWords", true, false, 0, 500, 10);

                map.AddTBStringPK(DocAttr.FK_Member, null, "FK_Member", true, false, 1, 100, 100);

                map.AddTBInt(DocAttr.ReadTimes, 0, "阅读次数", true, false);

				map.AddTBDateTime(DocAttr.RDT,null,"发布日期",true,false);
                map.AddTBDateTime(DocAttr.EDT, null, "修改日期", true, false);

				#endregion

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 会员文章
		/// </summary>
		public Doc(){}
		#endregion 
	}
	/// <summary>
	/// 会员文章
	/// </summary>
	public class Docs :EntitiesOID
	{
		#region 构造
		/// <summary>
		/// 会员文章s
		/// </summary>
		public Docs(){}
		/// <summary>
		/// 会员文章
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Doc();
			}
		}
		#endregion
	}
}
