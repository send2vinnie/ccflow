using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// 选择题属性
	/// </summary>
	public class ChoseAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 选择题
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// 选择题类型
		/// </summary>
		public const string ChoseType="ChoseType";
		/// <summary>
		/// 项目个数
		/// </summary>
		public const string ItemNum="ItemNum";
	}
	/// <summary>
	/// 选择题
	/// </summary>
	public class Chose :EntityNoName
	{
		#region attrs
		public ChoseItems HisChoseItems
		{
			get
			{
				return  new ChoseItems(this.No);
			}
		}
		/// <summary>
		/// 选择题类型
		/// </summary>
		public int ChoseType
		{
			get
			{
				return this.GetValIntByKey(ChoseAttr.ChoseType);
			}
			set
			{
				this.SetValByKey(ChoseAttr.ChoseType,value);
			}
		}
        public string FK_ThemeSort
        {
            get
            {
                return this.GetValStrByKey(ChoseAttr.FK_ThemeSort);
            }
            set
            {
                this.SetValByKey(ChoseAttr.FK_ThemeSort, value);
            }
        }
		#endregion
	 
		#region 实现基本的方法
		/// <summary>
		/// power
		/// </summary>
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
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GTS_Chose");
                map.EnDesc = "选择题";
                map.CodeStruct = "5";
                map.EnType = EnType.Admin;

                map.AddTBStringPK(ChoseAttr.No, null, "编号", true, true, 0, 50, 20);
                map.AddTBString(ChoseAttr.Name, null, "题目内容", true, false, 0, 1000, 600);

                map.AddDDLEntities(ChoseAttr.FK_ThemeSort, "0001", "类型", new ThemeSorts(), true);

                map.AddDDLSysEnum(ChoseAttr.ChoseType, 0, "选项类型", true, false);

                map.AddDtl(new ChoseItems(), ChoseItemAttr.FK_Chose);
                map.AddSearchAttr(ChoseAttr.FK_ThemeSort);
                map.AddSearchAttr(ChoseAttr.ChoseType);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 选择题
		/// </summary> 
		public Chose()
		{
		}
		/// <summary>
		/// 选择题
		/// </summary>
		/// <param name="_No">选择题编号</param> 
		public Chose(string _No ):base(_No)
		{
		}
		#endregion 

		#region 逻辑处理
		protected override bool beforeInsert()
		{
			//			this.No=this.GenerNewNo;
			return base.beforeInsert();
		}

		protected override bool beforeUpdate()
		{
			int i = DBAccess.RunSQLReturnValInt("select count(*) from GTS_ChoseItem where FK_Chose='"+this.No+"' and isOk=1 ");
			if (i>1)
			{
				this.ChoseType=1;
				/*多项选择题 */
			}
			else if (i==1) 
			{
				this.ChoseType = 0;
			}

			this.AutoGenerItems();
			return base.beforeUpdate ();
		}
		protected override void afterDelete()
		{
			this.DeleteHisRefEns();
			base.afterDelete ();
		}
		 
		/// <summary>
		/// 自动生成4各选择项目。
		/// </summary> 
		protected override void afterInsert()
		{
			// 自动生成4各选择项目。
			this.AutoGenerItems();
			base.afterInsert ();
		}

		/// <summary>
		/// 自动生成Item
		/// </summary>
		/// <param name="themeNo"></param>
		public void AutoGenerItems()
		{
			ChoseItems cts = new ChoseItems(this.No);
			if (cts.Count!=0)
				return;

			ChoseItem ctA = new ChoseItem();
			ctA.FK_Chose = this.No;
			ctA.Item="A";
			ctA.Insert();

			ChoseItem ctB = new ChoseItem();
			ctB.FK_Chose = this.No;
			ctB.Item="B";
			ctB.Insert();

			ChoseItem ctC = new ChoseItem();
			ctC.FK_Chose = this.No;
			ctC.Item="C";
			ctC.Insert();

			ChoseItem ctD = new ChoseItem();
			ctD.FK_Chose = this.No;
			ctD.Item="D";
			ctD.Insert();
		}
		#endregion

	}
	/// <summary>
	/// 选择题
	/// </summary>
	public class Choses :EntitiesNoName
	{
		#region 
		/// <summary>
		/// RetrieveChonseOne
		/// </summary>
		/// <returns></returns>
		public int RetrieveAllChonseOne(int topNum)
		{
			QueryObject qo = new QueryObject(this);
			qo.Top = topNum;
			qo.AddWhere(ChoseAttr.ChoseType, 0 );
			qo.addOrderByRandom();
			return qo.DoQuery();
		}
		/// <summary>
		/// RetrieveChonseM
		/// </summary>
		/// <returns></returns>
		public int RetrieveAllChonseM(int topNum)
		{
			QueryObject qo = new QueryObject(this);
			qo.Top = topNum;
			qo.AddWhere(ChoseAttr.ChoseType,1);
			qo.addOrderByRandom();
			return qo.DoQuery();
		}
		#endregion
		 
		   
		/// <summary>
		/// 选择题
		/// </summary>
		public Choses()
		{
		}
		/// <summary>
		/// 选择题
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Chose();
			}
		}
	}
}
