using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
	/// 专家
	/// </summary>
	public class ZJAttr:EntityNoNameAttr
	{ 
		#region 基本属性
		public const string FK_ZJType="FK_ZJType";
        public const string RefNo = "RefNo";
		/// <summary>
		/// 简历
		/// </summary>
		public const string Doc="Doc";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string Note="Note";
		public const string Dept="Dept";
		/// <summary>
		/// 是否要审核用户的留言
		/// </summary>
		public const string IsCheckMB="IsCheckMB";
		public const string MyFileName="MyFileName";
		public const string MyFilePath="MyFilePath";
		public const string MyFileExt="MyFileExt";
		/// <summary>
		/// 高度
		/// </summary>
		public const string MyFileHeight="MyFileHeight";
		public const string Email="Email";
		public const string BDT="BDT";
		public const string VisitTimes="VisitTimes";

		public const string RongYu="RongYu";
		public const string BeGoodAt="BeGoodAt";
		public const string LifeNote="LifeNote";
		#endregion
	}
	/// <summary>
	/// 专家焦点
	/// </summary>
	public class ZJ : EntityOIDName
	{

		#region  属性
        public string RefNo
        {
            get
            {
                return this.GetValStringByKey(ZJAttr.RefNo);
            }
        }
        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey(ZJAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(ZJAttr.MyFileExt, value);
            }
        }
		public string FK_ZJType
		{
			get
			{
				return this.GetValStringByKey(ZJAttr.FK_ZJType);
			}
			set
			{
				this.SetValByKey(ZJAttr.FK_ZJType,value);
			}
		}
		public string FK_ZJTypeText
		{
			get
			{
				return this.GetValRefTextByKey(ZJAttr.FK_ZJType);
			}
		}
		/// <summary>
		/// 简历
		/// </summary>
		public string Doc
		{
			get
			{
				return this.GetValHtmlStringByKey(ZJAttr.Doc);
			}
			set
			{
				this.SetValByKey(ZJAttr.Doc,value);
			}
		}
        public string DocS
        {
            get
            {
                return this.GetValHtmlStringByKey(ZJAttr.Doc);
            }
        }
		/// <summary>
		/// 内部人员
		/// </summary>
		public string Dept
		{
			get
			{
				return this.GetValStringByKey(ZJAttr.Dept);
			}
			set
			{
				this.SetValByKey(ZJAttr.Dept,value);
			}
		}
		#endregion 
		 
		#region 构造函数
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
		/// 专家
		/// </summary>
		public ZJ(){}

		public ZJ(int no):base(no){}
		/// <summary>
		/// ZJMap
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("YG_ZJ");
                map.EnDesc = "专家表";
                map.EnType = EnType.App;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAutoGenerNo = false;

                map.AddTBIntPKOID();
                map.AddTBString(ZJAttr.Name, null, "名称", true, false, 0, 50, 200);

                map.AddTBString(ZJAttr.Dept, null, "任职于", true, false, 0, 500, 200);
                map.AddDDLEntities(ZJAttr.FK_ZJType, null, "类别", new ZJTypes(), true);
                map.AddTBStringDoc();
                map.AddTBString(ZJAttr.RefNo, null, "关联帐号", true, false, 0, 50, 200);
                map.AddMyFile();

                map.AddSearchAttr(ZJAttr.FK_ZJType);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region  初始化税务机关信息。
		protected override bool beforeUpdateInsertAction()
		{
		//	this.MyFilePath= Glo.PathFDBZJT;
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

	}
	/// <summary>
	/// 局集合
	/// </summary>
	public class ZJs: EntitiesOIDName
	{
		/// <summary>
		/// 专家
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ZJ();
			}
		}
		/// <summary>
		/// 局集合
		/// </summary>
		public ZJs()
		{
		}
		public ZJs(string type)
		{
			this.Retrieve(ZJAttr.FK_ZJType, type);
		}
		/// <summary>
		/// 查询全部
		/// </summary>
		/// <returns></returns>
		public override int RetrieveAll()
		{
			return base.RetrieveAll();
		}
	}
}
 