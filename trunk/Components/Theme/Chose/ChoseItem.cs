using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	public class ChoseItemAttr:EntityOIDAttr
	{
		/// <summary>
		/// 选择题
		/// </summary>
		public const string Item="Item";
		/// <summary>
		/// 是否单项选择
		/// </summary>
		public const string ItemDoc="ItemDoc";
		/// <summary>
		/// 选择题
		/// </summary>
		public const string FK_Chose="FK_Chose";
		/// <summary>
		/// 是否正确
		/// </summary>
		public const string IsOK="IsOK";
	}
	/// <summary>
	/// 国税征收机关
	/// </summary>
	public class ChoseItem :EntityOID
	{

		#region attrs
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(ChoseItemAttr.FK_Chose);
			}
			set
			{
				this.SetValByKey(ChoseItemAttr.FK_Chose,value);
			}
		}
		public string Item
		{
			get
			{
				return this.GetValStringByKey(ChoseItemAttr.Item);
			}
			set
			{
				this.SetValByKey(ChoseItemAttr.Item,value);
			}
		}
		public string ItemDoc
		{
			get
			{
				return this.GetValStringByKey(ChoseItemAttr.ItemDoc);
			}
			set
			{
				this.SetValByKey(ChoseItemAttr.ItemDoc,value);
			}
		}
		public bool IsOK
		{
			get
			{
				return this.GetValBooleanByKey(ChoseItemAttr.IsOK);
			}
			set
			{
				this.SetValByKey(ChoseItemAttr.IsOK,value);
			}
		}
		#endregion

		#region 实现基本的方法
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_ChoseItem");
				map.EnDesc="选项";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBIntPKOID();
				map.AddTBString(ChoseItemAttr.Item,null,"选项",true,false,0,1,50);
				map.AddTBString(ChoseItemAttr.ItemDoc,null,"内容",true,false,0,600,600);
				map.AddBoolean(ChoseItemAttr.IsOK,true,"答案",true,false);
				map.AddDDLEntities(ChoseItemAttr.FK_Chose,"0001","选择题",new Choses(),false);
				//map.AddSearchAttr( ChoseAttr.FK_Chose);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 征收机关
		/// </summary> 
		public ChoseItem()
		{
		}
        public ChoseItem(string fk_chose, string item)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(ChoseItemAttr.FK_Chose, fk_chose);
            qo.addAnd();
            qo.AddWhere(ChoseItemAttr.Item, item);
            int i=qo.DoQuery();
            if (i == 0)
            {
                this.FK_Chose = fk_chose;
                this.Item = item;
                this.Insert();
            }

            
        }
		protected override void afterUpdate()
		{

			// 解决单项多项选择题的自动判断问题。
			if (this.IsOK)
			{
				int i = DBAccess.RunSQLReturnValInt("select count(*) from "+this.EnMap.PhysicsTable+" where FK_Chose='"+this.FK_Chose+"' and isOk=1 ");
				if (i>1)
				{
					/*多项选择题 */
					Chose ch = new Chose(this.FK_Chose);
					ch.ChoseType = 1;
					ch.DirectUpdate();
				}
				else  
				{
					Chose ch = new Chose(this.FK_Chose);
					ch.ChoseType = 0;
					ch.DirectUpdate();
				}

			}
			

			base.afterUpdate ();
		}

		#endregion 

	 
	}
	/// <summary>
	/// ChoseItems
	/// </summary>
	public class ChoseItems :Entities
	{
		public string NameExt
		{
			get
			{
				string str="";
				foreach(ChoseItem ci in this)
				{
					str+="    "+ci.Item+"、"+ci.ItemDoc+"\n";
				}
				return str;
				//return ChoseBase.GenerStr(str);
				//return str;
			}
		}

		public ChoseItems(string FK_Chose)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(ChoseItemAttr.FK_Chose,FK_Chose);
			qo.addOrderBy(ChoseItemAttr.Item);
			qo.DoQuery();
		}
		/// <summary>
		/// 查询出来正确的Items.
		/// </summary>
		/// <param name="fk_chose"></param>
		/// <returns></returns>
		public int RetrieveRightItems(string fk_chose)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(ChoseItemAttr.FK_Chose,fk_chose);
			qo.addAnd();
			qo.AddWhere(ChoseItemAttr.IsOK,true);
			return qo.DoQuery();
		}
		public string Val
		{
			get
			{
				string val="";
				foreach(ChoseItem ct in this)
				{
					val+=ct.Item+"@";
				}
				if (val=="")
					return val;
				return val.Substring(0,val.Length-1);

			}
		}

		/// <summary>
		/// Choses
		/// </summary>
		public ChoseItems(){}
		/// <summary>
		/// Chose
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ChoseItem();
			}
		}
	}
}
