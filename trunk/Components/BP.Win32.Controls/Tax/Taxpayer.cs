using System;
using System.Collections;
using System.Data ; 
using BP.DA;
using BP.En.Base;
using BP.En;

namespace BP.Tax
{
	/// <summary>
	/// 纳税人属性
	/// </summary>
	public class TaxpayerAttr : EntityNoNameAttr
	{	
		#region 属性
		/// <summary>
		/// 小组
		/// </summary>
		public const string FK_Team="FK_Team";
		/// <summary>
		/// 识别号
		/// </summary>
		public const string SBH="SBH";
		/// <summary>
		/// 地址
		/// </summary>
		public const string Addr="Addr";
		/// <summary>
		/// 电话
		/// </summary>
		public const string Tel="Tel";
		/// <summary>
		/// 邮编
		/// </summary>
		public const string PostCard="PostCard";
		/// <summary>
		/// 注册资金
		/// </summary>
		public const string ZCZJ="ZCZJ";
		/// <summary>
		/// 法人
		/// </summary>
		public const string ArtificialPerson="ArtificialPerson";
		/// <summary>
		/// 财务人
		/// </summary>
		public const string FinancePerson="FinancePerson";
		/// <summary>
		/// 乡镇编号
		/// </summary>
		public const string FK_XZBM="FK_XZBM";
		/// <summary>
		/// 经济性质
		/// </summary>
		public const string FK_JJXZ="FK_JJXZ";
		/// <summary>
		/// 行业
		/// </summary>
		public const string FK_HY="FK_HY";
		/// <summary>
		/// 登记状态
		/// </summary>
		public const string FK_DJZT="FK_DJZT";
		/// <summary>
		/// 经营范围
		/// </summary>
		public const string JJFW="JJFW";
		/// <summary>
		/// 注册日期
		/// </summary>
		public const string SignDate="SignDate";
		/// <summary>
		/// 征收机关编号
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		/// <summary>
		/// 月核定额
		/// </summary>
		public const string CheckTax="CheckTax";
		#endregion 
	}
	/// <summary>
	///  Taxpayer的摘要说明。
	/// </summary>
	public class Taxpayer : EntityNoName
	{
		#region 基本属性
		/// <summary>
		/// FK_Team
		/// </summary>
		public string FK_Team
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_Team);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_Team,value);
			}
		}		 
		/// <summary>
		/// SBH
		/// </summary>
		public string SBH
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.SBH);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.SBH,value);
			}
		}
		/// <summary>
		/// 地址
		/// </summary>
		public string Addr
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.Addr);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.Addr,value);
			}
		}
		/// <summary>
		/// 法人
		/// </summary>
		public string ArtificialPerson
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.ArtificialPerson);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.ArtificialPerson,value);
			}
		}
		/// <summary>
		/// 财务负责人
		/// </summary>
		public string FinancePerson
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FinancePerson);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FinancePerson,value);
			}
		}
		/// <summary>
		/// 登记状态
		/// </summary>
		public string FK_DJZT
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_DJZT);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_DJZT,value);
			}
		}
		/// <summary>
		/// 行业
		/// </summary>
		public string FK_HY
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_HY);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_HY,value);
			}
		}
		/// <summary>
		/// 经济性质　
		/// </summary>
		public string FK_JJXZ
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_JJXZ);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_JJXZ,value);
			}
		}
		/// <summary>
		/// 乡镇编码
		/// </summary>
		public string FK_XZBM
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_XZBM);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.FK_XZBM,value);
			}
		}
		/// <summary>
		/// 经营范围
		/// </summary>
		public string JJFW
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.JJFW);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.JJFW,value);
			}
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string PostCard
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.PostCard);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.PostCard,value);
			}
		}
		/// <summary>
		/// 注册日期
		/// </summary>
		public string SignDate
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.SignDate);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.SignDate,value);
			}
		}
		/// <summary>
		/// 电话
		/// </summary>
		public string Tel
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.Tel);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.Tel,value);
			}
		}
		/// <summary>
		/// 注册资金
		/// </summary>
		public string ZCZJ
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.ZCZJ);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.ZCZJ,value);
			}
		}
		/// <summary>
		/// 所属征收机关编号
		/// </summary>
		public string FK_ZSJG
		{
			get
			{
				return this.GetValStringByKey(TaxpayerAttr.FK_ZSJG);
			}
			set
			{
				if( value.Length==2)					
					this.SetValByKey(TaxpayerAttr.FK_ZSJG,SystemConfigOfTax.FK_ZSJG+value);
				else
					this.SetValByKey(TaxpayerAttr.FK_ZSJG,value);
			}
		}
		public string FK_ZSJGText
		{
			get
			{
				return this.GetValRefTextByKey(TaxpayerAttr.FK_ZSJG);
			}
		}
		/// <summary>
		/// 月核定额
		/// </summary>
		public float CheckTax
		{
			get
			{
				return this.GetValFloatByKey(TaxpayerAttr.CheckTax);
			}
			set
			{
				this.SetValByKey(TaxpayerAttr.CheckTax,value);
			}
		}		
		#endregion

		#region 扩展属性
		 
		 
		#endregion

		#region 构造方法
		/// <summary>
		/// /dfsdf
		/// </summary>
		public Taxpayer(){}		
		/// <summary>
		/// 根据纳税人编码查询信息
		/// </summary>
		/// <param name="_No">纳税人编码</param>
		public Taxpayer(string _No)
		{
			try
			{
				if (_No.Length !=14 )
					throw new Exception("@纳税人的编号["+_No+"]错误.");
				this.No =_No;
				this.Retrieve();
			}
			catch
			{
				this.Name =_No;
				if (this.RetrieveByName()==1)
					return ;
				if (SystemConfig.SysNo=="FeiCheng")
					throw new Exception();

				string sql="";
				if (SystemConfig.ThirdPartySoftWareKey=="YongYou")
				{
					sql="SELECT SWJG AS DeptNo, SGY AS FK_Team, QYBM AS No,TYBM AS SBH,QYMC AS Name,"+
						" QYDH AS Tel, QYDZ AS Addr,FRXM AS ArtificialPerson,CWFZ AS FinancePerson,SWFZRQ AS SignDate,"+
						" FZCH AS FK_DJZT ,	CZJC AS XZBM, JYFW AS JJFW, JKJJ AS FK_JJXZ,HY AS HY , '' AS PostCard,ZZZJ AS ZCZJ "+
						" FROM DSBM.DJSW "+
						" WHERE QYBM='"+ _No+"'	";
					DataTable dt = DBAccessOfOracle9i.RunSQLReturnTable(sql);
					if(dt.Rows.Count==0)
					{
						throw new Exception("@纳税人编码["+_No+"]输入有误!!");
					}
					else if (dt.Rows.Count >=2 )
					{
						throw new Exception("@纳税人编码["+_No+"]在主系统中不唯一．");
					}
					else
					{
						this.FK_ZSJG=SystemConfigOfTax.FK_ZSJG+dt.Rows[0]["DeptNo"].ToString();
						this.FK_Team=dt.Rows[0]["FK_Team"].ToString();
						this.No=dt.Rows[0]["No"].ToString();
						this.SBH=dt.Rows[0]["SBH"].ToString();
						this.Name=dt.Rows[0]["Name"].ToString();
						this.Tel=dt.Rows[0]["Tel"].ToString();
						this.Addr=dt.Rows[0]["Addr"].ToString();
						this.ArtificialPerson=dt.Rows[0]["ArtificialPerson"].ToString();
						this.FinancePerson=dt.Rows[0]["FinancePerson"].ToString();
						this.SignDate=dt.Rows[0]["SignDate"].ToString();
						this.FK_DJZT=dt.Rows[0]["FK_DJZT"].ToString();
						//  this.XZBM=dt.Rows[0]["XZBM"].ToString(); , , , ... ... ... ... ... 
						this.JJFW=dt.Rows[0]["JJFW"].ToString();
						this.FK_JJXZ=dt.Rows[0]["FK_JJXZ"].ToString();
						//this.HY=dt.Rows[0]["HY"].ToString();
						this.PostCard=dt.Rows[0]["PostCard"].ToString();
						if (dt.Rows[0]["ZCZJ"].ToString()=="")
							this.ZCZJ="0";
						else
							this.ZCZJ=dt.Rows[0]["ZCZJ"].ToString();
						this.Insert();
						this.Retrieve();						 
					}
				}
				else
				{
					throw new Exception("暂时未提供接口!!"); 
				}
			}
		}
		#endregion 

		#region Map
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("DS_Taxpayer");
				map.IsAllowRepeatNo=false;
				map.IsAllowRepeatName=true;
				map.EnDesc="纳税人";
				map.IsAdjunct=true;
				map.IsDelete=false;
				map.IsUpdate=false;
				map.IsInsert=false;
				map.IsView=true;
				map.DepositaryOfEntity=Depositary.None;
				map.DepositaryOfMap=Depositary.Application;

				map.EnType=EnType.ThirdPart;
				
				//map.PhysicsTable;
				map.AddTBStringPK(TaxpayerAttr.No,null,"纳税人编号",true,true,0,20,14);
				map.AddTBString(TaxpayerAttr.SBH,null,"纳税人识别号",true,true,0,20,18);
				map.AddTBString(TaxpayerAttr.ArtificialPerson,null,"法人",true,true,0,20,14);
				map.AddTBString(TaxpayerAttr.Addr,null,"纳税人地址",true,true,0,50,14);
				map.AddTBString(TaxpayerAttr.FinancePerson,null,"财务人",true,true,0,20,14);
				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_DJZT,null,"登记状态",new DJZTs(),false);
				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_HY,null,"行业",new HYs(),false);
				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_JJXZ,null,"经济性质",new JJXZs(),false);
				map.AddTBString(TaxpayerAttr.JJFW,null,"经营范围",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.Name,null,"纳税人名称",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.SignDate,null,"注册日期",true,true,0,20,14);
				map.AddTBString(TaxpayerAttr.Tel,null,"纳税人电话",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.ZCZJ,null,"注册资金",true,false,0,20,14);
				map.AddTBString(TaxpayerAttr.PostCard,null,"邮政编码",true,false,0,20,14);

				map.AddDDLEntitiesNoName(TaxpayerAttr.FK_ZSJG,null,"征收机关",new ZSJGs(),false);
				map.AddTBMoney(TaxpayerAttr.CheckTax,0,"月核定额",true,true);

				//map.AttrsOfSearch.Add("注册资金大于","ZCZJ",">",1000);

				map.AddSearchAttr(TaxpayerAttr.FK_DJZT);
				map.AddSearchAttr(TaxpayerAttr.FK_HY);
				map.AddSearchAttr(TaxpayerAttr.FK_JJXZ);
				map.AddSearchAttr(TaxpayerAttr.FK_XZBM);
				map.AddSearchAttr(TaxpayerAttr.FK_Team);

				this._enMap=map;
				return this._enMap; 
			}
		}
		#endregion 				

		#region  重写基类的方法。
//		protected override bool beforeInsert()
//		{
//			base.beforeInsert();
//			return true;
//		}
//		protected override bool beforeUpdate()
//		{
//			base.beforeUpdate();
//			return true;
//		}
//		protected override bool beforeDelete()
//		{
//			base.beforeDelete();
//			return true;
//		}
//
//		protected override void afterDelete()
//		{
//			base.afterDelete();
//			return ;
//
//		}
//		protected override  void afterInsert()
//		{
//			base.afterInsert();
//			return ;
//		}
//		protected override void afterUpdate()
//		{
//			base.afterUpdate();
//			return ;
//		}
		#endregion

		#region 静态方法
		/// <summary>
		/// GenerNSRString
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GenerNSRString(string key)
		{
			if (key.Length==0)
				throw new Exception("@关键字不能为空");
			DataTable dt = DBAccess.RunSQLReturnTable( "select No from ds_taxpayer where Name like '%"+key+"%'" ) ;
			string str = "";
			foreach ( DataRow dr in dt.Rows )
			{
				str= str + " '"+dr[0].ToString()+"',";
			}
			if (str!="")
				return str.Substring(0,str.Length-1);
			if (str.Trim().Length==0)
				str="'222222222222222'";
			return str;
		}
		/// <summary>
		/// 根据一些条件产生纳税人的string 用, 分隔.
		/// </summary>
		/// <param name="from">去年纳税额从</param>
		/// <param name="to">去年纳税额到</param>
		/// <param name="ZSJG">征收机关(all)</param>
		/// <param name="Level">级次</param>
		/// <param name="TaxpayerType">纳税人类型(all)</param>
		/// <param name="HY">行业(all)</param>
		/// <returns></returns>
		public static string GenerNSRString(int from, int to, string ZSJG, string Level, string TaxpayerType, string HY )
		{
			DataTable dt = DBAccess.RunSQLReturnTable( GenerNSRSQL(from,to,ZSJG,Level,TaxpayerType, HY) ) ;
			string str = "";
			foreach ( DataRow dr in dt.Rows )
			{
				str= str + " '"+dr[0].ToString()+"',";
			}
			if (str!="")
				return str.Substring(0,str.Length-1);
			if (str.Trim().Length==0)
				str="'222222222222222'";
			return str;
		}		 
		/// <summary>
		/// 产生纳税人sql 
		/// </summary>
		/// <param name="from">去年纳税额从</param>
		/// <param name="to">到 </param>
		/// <param name="ZSJG">征收机关</param>
		/// <param name="Level">级次</param>
		/// <param name="TaxpayerType">类型</param>
		/// <param name="HY">行业</param>
		/// <returns>bulider sql</returns>
		public static string GenerNSRSQL(int from, int to, string ZSJG, string Level, string TaxpayerType, string HY )
		{
			//定义出来全部的条件。
			// from to  条件
			string where1= " 1=1 ";
		    // string having = " ";
			//having = " having sum( NSE) > "+ from.ToString() +" and sum(NSE)  <  " + to.ToString(); 
			//征收机关
			string where2 = " " ; 
			if ( ZSJG.Equals("all")==false)
			{				
				where2 = " and ( aaa.DeptNo = '"+ ZSJG +"' ) ";
			}
			//级次
			string where3 = " " ;
			if ( Level.Equals("all")==false)
			{
			}
			//企业性质
			string where4 = " " ; 
			if (TaxpayerType.Equals("all")==false)
			{
				where3 = " and ( aaa.FK_JJXZ ='"+ TaxpayerType+"' ) ";
			}
			//行业
			string where5 = " ";
			if ( HY.Equals("all")==false)
			{
				where4 = " and ( aaa.FK_HY like '"+ HY +"%' ) ";
			}
			string WhereSQL=where1+ where2+where3+where4+where5+" AND ( No in (  SELECT TaxpayerNo FROM DS_TaxpayerYearSE2003 WHERE BenNianYiJiao > "+from + " AND BenNianYiJiao <  "+to +" ) ) ";
			string mysql=" SELECT aaa.No as TaxpayerNo FROM  DS_Taxpayer aaa WHERE " + WhereSQL; //;+ "  group by   aaa.c_qydm " + having;
			return mysql;
		}
		#endregion
	}
	/// <summary>
	///  纳税人s
	/// </summary>
	public class Taxpayers : EntitiesNoName
	{

		#region Init
//		public static string InitTaxpayer()
//		{
//			 string sql="";
//		}
		#endregion

		#region 纳税人
		/// <summary>
		/// 纳税人s
		/// </summary>
		public Taxpayers()
		{
		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Taxpayer();
			}
		}
		#endregion
		
	}
}
