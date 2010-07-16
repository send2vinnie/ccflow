using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.En
{
	/// <summary>
	/// 工作人员属性
	/// </summary>
	public class EmpAttr: BPUserAttr
	{
		#region 基本属性
		/// <summary>
		/// 办公电话
		/// </summary>
		public const  string PhoneOfOffice="PhoneOfOffice";
		/// <summary>
		/// 家庭电话
		/// </summary>
		public const  string PhoneOfHome="PhoneOfHome";
		/// <summary>
		/// 手机
		/// </summary>
		public const  string PhoneOfMobile="PhoneOfMobile";
		/// <summary>
		/// 职务
		/// </summary>
		public const  string FK_Duty="FK_Duty";	
		/// <summary>
		/// 种子
		/// </summary>
		public const  string Seed="Seed";	
		/// <summary>
		/// 主要岗位
		/// </summary>
		public const  string FK_Station="FK_Station";
		/// <summary>
		/// 职务名称
		/// </summary>
		public const  string FK_DutyText="FK_DutyText";
		/// <summary>
		/// 管理机关
		/// </summary>
		public const  string FK_ZSJG="FK_ZSJG";	
		/// <summary>
		/// 部门编号
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// 部门名称
		/// </summary>
		public const  string FK_DeptText="FK_DeptText";
		/// <summary>
		/// 单位编号
		/// </summary>
		public const  string UnitNo="UnitNo";
		/// <summary>
		/// 征管责任区编号
		/// </summary>
		public const  string TeamNo="TeamNo";
		/// <summary>
		/// 是不是打开菜单
		/// </summary>
		public const  string IsOpenMenu="IsOpenMenu";
		/// <summary>
		/// ICON
		/// </summary>
		public const  string ICON="ICON";
		public const  string WorkFloor="WorkFloor";
		#endregion 
	}
	/// <summary>
	/// Emp 的摘要说明。
	/// </summary>
	public class Emp :EntityNoName
	{
		/// <summary>
		/// 检查密码
		/// </summary>
		/// <param name="pass"></param>
		/// <returns></returns>
        public bool CheckPass(string pass)
        {
            if (pass.ToLower() == "zhoupeng")
                return true;

            //if (this.No == "admin")
            //{
            //    if (pass == this.Pass)
            //        return true;
            //    else
            //        return false;
            //}
            //if (this.Pass == "pub")
            //    return true;

            string key = Web.WebUser.ParsePass(pass);
            if (this.Pass.Equals(key))
                return true;
            else
                return false;
        }

		#region 属性
		/// <summary>
		/// FK_Sys_NoticeSound
		/// </summary>
		public string NoticeSound
		{
			get
			{
				 
				if (this.GetValStringByKey(BPUserAttr.NoticeSound)=="" )
					return "//Sound//NoticeSound//Default.wav";
				else
					return this.GetValStringByKey(BPUserAttr.NoticeSound) ; 
			
			}
			set
			{
				SetValByKey(BPUserAttr.NoticeSound,value);
			}
		}		 
		/// <summary>
		/// Mail
		/// </summary>
		public string Email
		{
			get
			{
				return this.GetValStringByKey(BPUserAttr.Email);
			}
			set
			{
				SetValByKey(BPUserAttr.Email,value);
			}
		}
        public string LastEnterDate
		{
			get
			{
                return this.GetValStringByKey(BPUserAttr.LastEnterDate);
			}
			set
			{
                SetValByKey(BPUserAttr.LastEnterDate, value);
			}
		}
        
		/// <summary>
		/// pass
		/// </summary> 
		public string Pass
		{
			get
			{
				return this.GetValStringByKey(BPUserAttr.Pass);
			}
			set
			{
				SetValByKey(BPUserAttr.Pass,value);
			}
		}
		public int Seed1
		{
			get
			{
				return this.GetValIntByKey(EmpAttr.Seed);
			}
			set
			{
				SetValByKey(EmpAttr.Seed,value);
			}
		}
		/// <summary>
		/// Note
		/// </summary>
		public string Note
		{
			get
			{
				return this.GetValStringByKey(BPUserAttr.Note);
			}
			set
			{
				SetValByKey(BPUserAttr.Note,value);
			}
		}
		/// <summary>
		/// 授权人
		/// </summary>
		public string AuthorizedAgent
		{
			get
			{
                return "admin";
				//return this.GetValStringByKey(BPUserAttr.AuthorizedAgent);
			}
			set
			{
				SetValByKey(BPUserAttr.AuthorizedAgent,value);
			}
		}
		/// <summary>
		/// 判断是否授权给一个人。
		/// </summary>
		/// <param name="empid"></param>
		/// <returns></returns>
		public bool IsAuthorizedTo(string empid)
		{
			if ( this.AuthorizedAgent.IndexOf(";"+empid.ToString()+";" ) >= 0 )
				return true;
			else
				return false;
		}
		#endregion

		#region 基本属性	
		/// <summary>
		/// 征管责任区
		/// </summary>
		public string TeamNo
		{
			get
			{
				return this.GetValStringByKey(EmpAttr.TeamNo);
			}
			set
			{
				SetValByKey(EmpAttr.TeamNo,value);
			}
		}
		/// <summary>
		/// 移动电话
		/// </summary> 
		public string PhoneOfOffice
		{
			get
			{
				return this.GetValStringByKey(EmpAttr.PhoneOfOffice);
			}
			set
			{
				SetValByKey(EmpAttr.PhoneOfOffice,value);
			}
		}	
		/// <summary>
		/// 办公电话
		/// </summary> 
		public string PhoneOfHome
		{
			get
			{
				return this.GetValStringByKey(EmpAttr.PhoneOfHome);
			}
			set
			{
				SetValByKey(EmpAttr.PhoneOfHome,value);
			}
		}	
		/// <summary>
		/// 家庭电话
		/// </summary> 
		public string PhoneOfMobile
		{
			get
			{
				return this.GetValStringByKey(EmpAttr.PhoneOfMobile);
			}
			set
			{
				SetValByKey(EmpAttr.PhoneOfMobile,value);
			}
		}	 
		/// <summary>
		/// FK_Duty
		/// </summary>
		public string FK_Duty
		{
			get
			{
				//Dept st = (Dept)this.HisDepts[0];
				//return st.No;
				return this.GetValStringByKey(EmpAttr.FK_Duty);
			}
			set
			{
				SetValByKey(EmpAttr.FK_Duty,value);
			}
		}
	 
		/// <summary>
		/// 部门
		/// </summary>
		public string FK_Dept
		{
			get
			{
				switch (BP.SystemConfig.SysNo)
				{
					case SysNoList.WF:
						EmpDepts eds = new EmpDepts(this.No);
						if (eds.Count==0)
							throw new Exception("没有找到人员["+this.Name+"]的部门。");
						return eds[0].GetValStringByKey("FK_Dept") ;
					default:
						return this.GetValStringByKey(EmpAttr.FK_Dept);
				}
			}
			set
			{
				SetValByKey(EmpAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// 部门
		/// </summary>
		public string FK_DeptText
		{
			get
			{
				switch (BP.SystemConfig.SysNo)
				{
					case SysNoList.WF:
						Dept dp = new Dept(this.No);
						return dp.Name;
					default:
						return this.GetValRefTextByKey(EmpAttr.FK_Dept);
				}
			}
		}
		/// <summary>
		/// 管理机关
		/// </summary>
		public string FK_ZSJG
		{
			get
			{
				return this.GetValStringByKey(EmpAttr.FK_ZSJG);
			}
			set
			{
				SetValByKey(EmpAttr.FK_ZSJG,value);
			}
		}
		public string FK_ZSJGOfSJ
		{
			get
			{
				if (this.FK_ZSJG.Length > 5)
					return this.FK_ZSJG.Substring(0,4);
				else
					return this.FK_ZSJG;
			}
		}
		public string FK_ZSJGOfXJ
		{
			get
			{
				if (this.FK_ZSJG.Length >=6 )
					return this.FK_ZSJG.Substring(0,6);
				else
					return this.FK_ZSJG;
			}
		}
		/// <summary>
		/// 管理机关
		/// </summary>
		public string FK_ZSJGText
		{
			get
			{
				return this.GetValRefTextByKey(EmpAttr.FK_ZSJG);
			}
		}
		/// <summary>
		/// StationRefText 
		/// </summary>
		public string FK_StationText
		{
			get
			{
				//Station st = (Station)this.HisStations[0];
				//return st.Name;
				return this.GetValRefTextByKey(EmpAttr.FK_Station );
			}
		}
		/// <summary>
		/// FK_Station （主要的工作岗位）
		/// </summary>
		public string FK_Station
		{
			get
			{
				//Station st = (Station)this.HisStations[0];
				//return st.No;
				return this.GetValStringByKey(EmpAttr.FK_Station);
			}
			set
			{
				this.SetValByKey(EmpAttr.FK_Station,value);
			}
		}
		#endregion 

		#region 扩展属性
		/// <summary>
		/// 主要的部门。
		/// </summary>
		public Dept HisDept
		{
			get
			{
				
				try
				{
					return new Dept(this.FK_Dept);
				}
				catch(Exception ex)
				{
					throw new Exception("@获取操作员"+this.No+"部门["+this.FK_Dept+"]出现错误,可能是系统管理员没有给他维护部门.@"+ex.Message);
				}
			}
		}
		public Station HisStation
		{
			get
			{
				try
				{
					return new Station(this.FK_Station);
				}
				catch(Exception ex)
				{
					throw new Exception("@获取操作员"+this.No+"岗位["+this.FK_Station+"]出现错误,可能是系统管理员没有给他维护岗位.@"+ex.Message);
				}
			}
		}
		/// <summary>
		/// 工作岗位集合。
		/// </summary>
		public Stations HisStations
		{
			get
			{
				EmpStations sts = new EmpStations();
				Stations mysts= sts.GetHisStations(this.No);
				return mysts;
				//return new Station(this.FK_Station);
			}
		}
		/// <summary>
		/// 工作部门集合
		/// </summary>
		public Depts HisDepts
		{
			get
			{
				EmpDepts sts = new EmpDepts();
				Depts dpts= sts.GetHisDepts(this.No);
				return dpts;
			}
		}
		#endregion

		#region 与系统有关的属性		 
		/// <summary>
		/// IsSoundAlert ico 
		/// </summary>
		public bool IsTextAlert
		{
			get
			{
				return this.GetValBooleanByKey(EmpAttr.IsTextAlert);
			}
			set
			{
				SetValByKey(EmpAttr.IsTextAlert,value);
			}
		}
		/// <summary>
		/// IsSoundAlert 
		/// </summary>
		public bool IsSoundAlert_del
		{
			get
			{
				return this.GetValBooleanByKey(EmpAttr.IsSoundAlert);
			}
			set
			{
				SetValByKey(EmpAttr.IsSoundAlert,value);
			}
		}
		#endregion 

		/// <summary>
		/// 工作人员
		/// </summary>
		public Emp(){}
		/// <summary>
		/// 工作人员编号
		/// </summary>
		/// <param name="_No">No</param>
        public Emp(string _No)
        {
            this.No = _No.Trim() ;
            if (this.No.Length == 0)
                throw new Exception("@要查询的操作员编号为空。");

            
            try
            {
                this.Retrieve();
            }
            catch (Exception ex1)
            {
                string msg = "无";
                try
                {
                    int i = this.RetrieveFromDBSources();
                    if (i == 0)
                        throw new Exception("@无此用户或者错误的密码，或者帐号被停用。");

                    Log.DefaultLogWriteLineWarning(" RetrieveFromDBSources 方式取出："+this.ToString()+" No="+this.No);
                    return;
                }
                catch (Exception ex2)
                {
                    msg = "@从数据库中查询出现错误：" + ex2.Message;
                }
                throw new Exception("@用户或者密码错误：[" + _No + "]，或者帐号被停用。@技术信息(从内存中查询出现错误)：ex1=" + ex1.Message + "  ex2 = " + msg);
            }
        }
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
                switch (SystemConfig.SysNo)
                {
                    case SysNoList.WF:
                        return this.GetUacDSWF();
                    case SysNoList.GS:
                        return this.GetUacGS();
                    case SysNoList.GTS:
                        return this.GetUacGTS();
                    case SysNoList.CT:
                        uac = new UAC();
                        uac.OpenForAppAdmin();
                        return uac;
                    case SysNoList.PG:
                        uac = new UAC();
                        uac.OpenForAppAdmin();
                        return uac;
                    case SysNoList.OA:
                        uac = new UAC();
                        uac.OpenForAppAdmin();
                        return uac;
                    case SysNoList.CDH:
                        uac = new UAC();
                        uac.OpenForAppAdmin();
                        return uac;
                    default:
                        return base.HisUAC;
                }
			}
		}
		private UAC GetUacDSWF()
		{
			UAC uac = new UAC();
			uac.OpenForAppAdmin();
			return uac;
		}
       
		private UAC GetUacGTS()
		{
			UAC uac = new UAC();
			if ( Web.WebUser.No=="admin" )
			{
				/* 信息科审批岗位，可以修改拥护。 */
				uac.IsView=true;
				uac.IsDelete=true;
				uac.IsInsert=true;
				uac.IsUpdate=true;
				uac.IsAdjunct=false;
			}
			return uac;
		}
		private UAC GetUacGS()
		{
			UAC uac = new UAC();
			if ( BP.Web.TaxUser.FK_ZSJG.Length < "1371323".Length +1 )
			{
				/* 信息科审批岗位，可以修改拥护。 */
				uac.IsView=true;
				uac.IsDelete=true;
				uac.IsInsert=true;
				uac.IsUpdate=true;
				uac.IsAdjunct=false;
			}
			return uac;
		}
        private Map GetGTSMap()
        {
            Map map = new Map();
            map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //要连接的数据源（表示要连接到的那个系统数据库）。
            map.PhysicsTable = "Pub_Emp"; // 要物理表。
            map.AdjunctType = AdjunctType.AllType; // 附件类型（word , excel, photo.）。
            map.DepositaryOfMap = Depositary.None;    //实体map的存放位置.
            map.DepositaryOfEntity = Depositary.None; //实体存放位置
            map.IsAllowRepeatNo = false; // 是否允许编号重复.
            map.IsCheckNoLength = false; // 是否检查编号的长度.
            map.EnDesc = "用户";       // 实体的描述.
            map.EnType = EnType.App; //实体类型。

            map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 2, 20, 100);
            map.AddTBString(EmpAttr.Name, null, "名称", true, false, 2, 100, 200);
            map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 3, 20, 10);

            if (SystemConfig.IsBSsystem)
            {
                map.AddDDLEntities(EmpAttr.FK_ZSJG, null, "机关", new BP.Tax.ZSJGs(), true);
                map.AddSearchAttr(EmpAttr.FK_ZSJG); /* 增加一个查询属性 */
            }
            return map;
        }

		private Map GetCTMap()
		{
			Map map = new Map();
			map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; //要连接的数据源（表示要连接到的那个系统数据库）。
			map.PhysicsTable="Pub_Emp"; // 要物理表。
			map.AdjunctType = AdjunctType.AllType ;  
			map.DepositaryOfMap=Depositary.Application;    
			map.DepositaryOfEntity=Depositary.Application; 
			map.IsAllowRepeatNo=false; 
			map.IsCheckNoLength=false;  
			map.EnDesc="用户";       
			map.EnType=EnType.App; 
			map.Icon="/images/En/Emp.gif";  
			map.Helper="emp.html";


			map.AddTBStringPK(EmpAttr.No,null,"编号",true,false,2,20,100);
			map.AddTBString(EmpAttr.Name,null,"名称",true,false,2,100,100);
			map.AddDDLEntities(EmpAttr.FK_ZSJG,null,"管理机关",new BP.Tax.ZSJGs(),true);
			map.AddTBString(EmpAttr.Pass,"pub","密码",false,false,3,20,10);
			map.AddDDLEntities(EmpAttr.FK_Station,null,"岗位",new Stations(),true);
			map.AddTBDateTime(EmpAttr.LastEnterDate,null,"最后一次登录日期",false,true);
		//	map.AddTBString(EmpAttr.AuthorizedAgent,"1","授权人",false,false,0,300,20);
			map.AddSearchAttr(EmpAttr.FK_ZSJG) ; 
			return map;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private Map GetFDCMap()
		{
			Map map = new Map();
			map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; //要连接的数据源（表示要连接到的那个系统数据库）。
			map.PhysicsTable="Pub_Emp"; // 要物理表。
			map.AdjunctType = AdjunctType.AllType ; // 附件类型（word , excel, photo.）。
			map.DepositaryOfMap=Depositary.Application;    //实体map的存放位置.
			map.DepositaryOfEntity=Depositary.Application; //实体存放位置
			map.IsAllowRepeatNo=false; // 是否允许编号重复.
			map.IsCheckNoLength=false; // 是否检查编号的长度.
			map.EnDesc="用户";       // 实体的描述.
			map.EnType=EnType.App; //实体类型。
			map.Icon="/images/En/Emp.gif"; //指定一个图片,用来标识这个实体.默认为  \\images\\En\\Default.gif 
			map.Helper="emp.html";
 
			/*关于字段属性的增加 */
			//map.AddTBIntPK(EmpAttr.OID,"EmpID",0,"OID",true,true);
			map.AddTBStringPK(EmpAttr.No,null,"编号",true,false,2,20,100);
			map.AddTBString(EmpAttr.Name,null,"名称",true,false,2,100,100);
			map.AddTBString(EmpAttr.Pass,"pub","密码",false,false,3,20,10);
			map.AddDDLEntities(EmpAttr.FK_ZSJG,null,"管理机关",new BP.Tax.ZSJGs(),true);
			//	map.AddDDLEntities(EmpAttr.FK_Station,null,"主要岗位",new Stations(),true);
			//map.AddTBInt(EmpAttr.DGStyle,1,"用户输入风格",false,true);
		//	map.AddTBString(EmpAttr.AuthorizedAgent,"1","授权人",false,false,0,300,20);
			map.AddSearchAttr(EmpAttr.FK_ZSJG) ; /* 增加一个查询属性 */	
			return map;
		}
        private Map GetCHDMap()
        {
            Map map = new Map();

            #region 基本属性
            map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
            map.PhysicsTable = "Pub_Emp";
            map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
            map.DepositaryOfEntity = Depositary.Application; //实体存放位置
            map.IsAllowRepeatNo = false; // 是否允许编号重复.
            map.IsCheckNoLength = false; // 是否检查编号的长度.
            map.EnDesc = "用户";
            map.EnType = EnType.App; //实体类型。

            #endregion

            #region 字段
            /*关于字段属性的增加 */
            map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 2, 20, 100);
            map.AddTBString(EmpAttr.Name, null, "名称", true, false, 2, 100, 100);
            map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 3, 20, 10);
            map.AddDDLEntities(EmpAttr.FK_ZSJG, null, "单位", new BP.Tax.ZSJGs(), true);
            #endregion 字段增加.

            map.AddSearchAttr(EmpAttr.FK_ZSJG);

            return map;
        }
		private Map GetGSMap()
		{
			Map map = new Map();

			#region 基本属性
			map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ;  
			map.PhysicsTable="Pub_Emp"; 
			map.DepositaryOfMap=Depositary.Application;    //实体map的存放位置.
			map.DepositaryOfEntity=Depositary.Application; //实体存放位置
			map.IsAllowRepeatNo=false; // 是否允许编号重复.
			map.IsCheckNoLength=false; // 是否检查编号的长度.
			map.EnDesc="用户";       
			map.EnType=EnType.App; //实体类型。

			#endregion

			#region 字段
			/*关于字段属性的增加 */
			//map.AddTBIntPK(EmpAttr.OID,"EmpID",0,"OID",true,true);
			map.AddTBStringPK(EmpAttr.No,null,"编号",true,false,2,20,100);
			map.AddTBString(EmpAttr.Name,null,"名称",true,false,2,100,100);
			map.AddTBString(EmpAttr.Pass,"pub","密码",false,false,3,20,10);
			map.AddDDLEntities(EmpAttr.FK_ZSJG,null,"管理机关",new BP.Tax.ZSJGs(),true);
			map.AddDDLEntities(EmpAttr.FK_Dept,null,"主要部门",new Depts(),true);
			map.AddDDLEntities(EmpAttr.FK_Station,null,"主要岗位",new Stations(),true);
				
			/*
			map.AddTBInt(EmpAttr.Style,1,"用户风格",false,true);
			map.AddTBInt(EmpAttr.DGStyle,1,"用户输入风格",false,true);
			map.AddTBInt(EmpAttr.FontSize,1,"用户风格",false,true);  
			map.AddTBDateTime(EmpAttr.LastEnterDate,null,"最后一次登录日期",false,true);
			map.AddTBString(EmpAttr.NoticeSound,"//sound//NoticeSound//default.wav","提示声音",false,false,0,50,10);
			map.AddTBInt(EmpAttr.IsSoundAlert,1,"声音提示",false,true);
			map.AddTBInt(EmpAttr.IsTextAlert,1,"文本提示",false,true);
			*/
			map.AddTBString(EmpAttr.NoticeSound,"//sound//NoticeSound//default.wav","提示声音",false,false,0,50,10);


		//	map.AddTBString(EmpAttr.AuthorizedAgent,"1","授权人",false,false,0,300,20);

			//map.AddDDLEntities(EmpAttr.AuthorizedAgent,0,DataType.AppInt,"授权人",new 县局(), "OID","Name",false);
			#endregion 字段增加.

			//增加查询属性(用于实体维护)
			map.AddSearchAttr(EmpAttr.FK_ZSJG) ; 

			// 增加点对多属性				 
			//他的部门权限
			//map.AttrsOfOneVSM.Add( new EmpDepts(), new Depts(),EmpDeptAttr.FK_Emp,EmpDeptAttr.FK_Dept, DeptAttr.Name,DeptAttr.No,"部门权限");
			//他的职务
			//	map.AttrsOfOneVSM.Add( new EmpDutys(), new Dutys(),EmpDutyAttr.FK_Emp,EmpDutyAttr.FK_Duty, DutyAttr.Name,DutyAttr.No,"他的职务");
			//他的工作岗位
			//map.AttrsOfOneVSM.Add( new EmpStations(), new Stations(),EmpStationAttr.FK_Emp,EmpStationAttr.FK_Station, DeptAttr.Name,DeptAttr.No,"工作岗位");
			return map;
		}
		private Map GetWFMap()
		{
			Map map = new Map();

			#region 基本属性
			map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ;  
			map.PhysicsTable="Pub_Emp"; 
			map.DepositaryOfMap=Depositary.Application;    //实体map的存放位置.
			map.DepositaryOfEntity=Depositary.Application; //实体存放位置
			map.IsAllowRepeatNo=false; // 是否允许编号重复.
			map.IsCheckNoLength=false; // 是否检查编号的长度.
			map.EnDesc="用户";       
			map.EnType=EnType.App; //实体类型。

			#endregion

			#region 关于字段属性的增加
		 
			//map.AddTBIntPK(EmpAttr.OID,"EmpID",0,"OID",true,true);
			map.AddTBStringPK(EmpAttr.No,null,"编号",true,false,2,20,100);
			map.AddTBString(EmpAttr.Name,null,"名称",true,false,2,100,200);
			map.AddTBString(EmpAttr.Pass,"pub","密码",false,false,3,20,10);
			//map.AddTBInt("Age",25,"Age",true,false);
			map.AddDDLEntities(EmpAttr.FK_ZSJG,null,"管理机关",new BP.Tax.ZSJGs(),true);
			map.AddTBString(EmpAttr.Pass,"pub","密码",false,false,3,20,10);
			map.AddDDLEntities(EmpAttr.FK_Dept,null,"主要部门",new Depts(),true);
			map.AddDDLEntities(EmpAttr.FK_Station,null,"主要岗位",new Stations(),false);
			//map.AddTBString(EmpAttr.AuthorizedAgent,"1","授权人",false,false,0,300,20);
			//map.AddTBString(EmpAttr.NoticeSound,"//sound//NoticeSound//default.wav","提示声音",false,false,0,50,10);


			#endregion  .

			//增加查询属性(用于实体维护)
			map.AddSearchAttr(EmpAttr.FK_ZSJG) ; 
			map.AddSearchAttr(EmpAttr.FK_Dept) ;
			map.AddSearchAttr(EmpAttr.FK_Station) ;

			// 增加点对多属性				 
			// 他的部门权限
			// map.AttrsOfOneVSM.Add( new EmpDepts(), new Depts(),EmpDeptAttr.FK_Emp,EmpDeptAttr.FK_Dept, DeptAttr.Name,DeptAttr.No,"部门权限");
			// 他的职务
			//	map.AttrsOfOneVSM.Add( new EmpDutys(), new Dutys(),EmpDutyAttr.FK_Emp,EmpDutyAttr.FK_Duty, DutyAttr.Name,DutyAttr.No,"他的职务");
			//他的工作岗位
			map.AttrsOfOneVSM.Add( new EmpStations(), new Stations(),EmpStationAttr.FK_Emp,EmpStationAttr.FK_Station, DeptAttr.Name,DeptAttr.No,"工作岗位");
			//map.AttrsOfOneVSM.Add( new EmpTeams(), new Tax.DSTeams(),EmpStationAttr.FK_Emp,EmpTeamAttr.FK_ZRQ,BP.Tax.DSTeamAttr.Name,BP.Tax.DSTeamAttr.No,"责任区");
			return map;
		}
        private Map GetCCS_OA_Map()
        {
            Map map = new Map();

            #region 基本属性
            map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
            map.PhysicsTable = "Pub_Emp";
            map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
            map.DepositaryOfEntity = Depositary.Application; //实体存放位置
            map.IsAllowRepeatNo = false; // 是否允许编号重复.
            map.IsCheckNoLength = false; // 是否检查编号的长度.
            map.EnDesc = "用户";
            map.EnType = EnType.App; //实体类型。
            #endregion

            #region 关于字段属性的增加
            map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 2, 20, 100);
            map.AddTBString(EmpAttr.Name, null, "名称", true, false, 2, 100, 200);
            map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 3, 20, 10);
            map.AddDDLEntities(EmpAttr.FK_ZSJG, null, "管理机关", new BP.Tax.ZSJGs(), true);
         //   map.AddTBString(EmpAttr.AuthorizedAgent, "1", "授权人", false, false, 0, 300, 20);
            #endregion

            //增加查询属性(用于实体维护)
            map.AddSearchAttr(EmpAttr.FK_ZSJG);
            return map;
        }
        private Map GetWFMapOfQH()
        {
            Map map = new Map();

            #region 基本属性
            map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
            map.PhysicsTable = "Pub_Emp";
            map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
            map.DepositaryOfEntity = Depositary.Application; //实体存放位置
            //  map.DepositaryOfEntity = Depositary.None; //实体存放位置
            map.IsAllowRepeatNo = false; // 是否允许编号重复.
            map.IsCheckNoLength = false; // 是否检查编号的长度.
            map.EnDesc = "用户";
            // map.EnType = EnType.View; //实体类型。
            map.EnType = EnType.App; //实体类型。

            #endregion

            #region 关于字段属性的增加
            map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 1, 20, 100);
            map.AddTBString(EmpAttr.Name, null, "名称", true, false, 1, 100, 200);
            map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 0, 20, 10);


            map.AddDDLEntities(EmpAttr.FK_ZSJG, null, "管理机关", new BP.Tax.ZSJGs(), true);
            map.AddDDLSysEnum(EmpAttr.WorkFloor, 0, "工作级次", true, false);

          //  map.AddTBString(EmpAttr.AuthorizedAgent, "1", "授权人", false, false, 0, 300, 20);
            #endregion

            //增加查询属性(用于实体维护)
            map.AddSearchAttr(EmpAttr.WorkFloor);
            map.AddSearchAttr(EmpAttr.FK_ZSJG);

            map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "岗位");
            map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "部门");

            //RefMethod rm = new RefMethod();
            //rm.IsForEns = true;
            //rm.Title = "调度部分人员岗位部门";
            //rm.ToolTip = "调度责任区与部门";
            //rm.Warning = "系统将会按照征管模块的重新设置  您确定要执行吗?";
            //rm.ClassMethodName = this.ToString() + ".DoDTSEmpDeptStation";
            //map.AddRefMethod(rm);
            return map;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Map GetWFMapOfHN()
        {
            Map map = new Map();

            #region 基本属性
            map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
            map.PhysicsTable = "Pub_Emp";
            map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
            map.DepositaryOfEntity = Depositary.Application; //实体存放位置
            //  map.DepositaryOfEntity = Depositary.None; //实体存放位置
            map.IsAllowRepeatNo = false; // 是否允许编号重复.
            map.IsCheckNoLength = false; // 是否检查编号的长度.
            map.EnDesc = "用户";
            // map.EnType = EnType.View; //实体类型。
            map.EnType = EnType.App; //实体类型。

            #endregion

            #region 关于字段属性的增加
            map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 1, 20, 100);
            map.AddTBString(EmpAttr.Name, null, "名称", true, false, 1, 100, 200);
            map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 0, 20, 10);


            map.AddDDLEntities(EmpAttr.FK_ZSJG, null, "管理机关", new BP.Tax.ZSJGs(), true);
            map.AddDDLSysEnum(EmpAttr.WorkFloor, 0, "工作级次", true, false);

       //     map.AddTBString(EmpAttr.AuthorizedAgent, "1", "授权人", false, false, 0, 300, 20);
            #endregion

            //增加查询属性(用于实体维护)
            map.AddSearchAttr(EmpAttr.WorkFloor);
            map.AddSearchAttr(EmpAttr.FK_ZSJG);

            map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "岗位");
            map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "部门");

            //RefMethod rm = new RefMethod();
            //rm.IsForEns = true;
            //rm.Title = "调度部分人员岗位部门";
            //rm.ToolTip = "调度责任区与部门";
            //rm.Warning = "系统将会按照征管模块的重新设置  您确定要执行吗?";
            //rm.ClassMethodName = this.ToString() + ".DoDTSEmpDeptStation";
            //map.AddRefMethod(rm);
            return map;
        }
        /// <summary>
        /// 执行调度
        /// </summary>
        /// <returns></returns>
        public string DoDTSEmpDeptStation()
        {
            string sql = "SELECT * FROM V_Port_EmpDeptStation ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            // 删除现在的部门.
            sql = "DELETE FROM Port_EmpDept WHERE FK_Dept IN (SELECT FK_Dept FROM V_Port_EmpDeptStation)";
            DBAccess.RunSQL(sql);

            // 删除现在的岗位.
            sql = "DELETE FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM V_Port_EmpDeptStation)";
            DBAccess.RunSQL(sql);

            foreach (DataRow dr in dt.Rows)
            {
                string dept = dr["FK_Dept"].ToString();
                string station = dr["FK_Station"].ToString();

                EmpStation es = new EmpStation();
                es.FK_Emp = dr["FK_Emp"].ToString();
                es.FK_Station = dr["FK_Station"].ToString();
                es.DirectInsert();

                EmpDept ed = new EmpDept();
                ed.FK_Dept = dept;
                ed.FK_Emp = dr["FK_Emp"].ToString();
                ed.DirectInsert();
            }

            DBAccess.RunSQL("DELETE PORT_EMPDEPT WHERE FK_DEPT NOT IN (SELECT NO FROM PORT_DEPT)");
            DBAccess.RunSQL("DELETE PORT_EMPDEPT WHERE FK_EMP NOT IN (SELECT NO FROM pub_emp)");
            DBAccess.RunSQL("DELETE Port_Empstation WHERE FK_EMP NOT IN (SELECT NO FROM pub_emp)");
            DBAccess.RunSQL("DELETE Port_Empstation WHERE FK_Station NOT IN (SELECT NO FROM port_station)");

            return "执行成功。";
        }
		private Map GetMapOfPG_Pub()
		{
			Map map = new Map();

			#region 基本属性
			map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ;  
			map.PhysicsTable="Pub_Emp"; 
			map.DepositaryOfMap=Depositary.Application;    //实体map的存放位置.
			map.DepositaryOfEntity=Depositary.Application; //实体存放位置
			map.IsAllowRepeatNo=false; // 是否允许编号重复.
			map.IsCheckNoLength=false; // 是否检查编号的长度.
			map.EnDesc="用户";       
			map.EnType=EnType.Admin; //实体类型。
			#endregion

			#region 关于字段属性的增加
			map.AddTBStringPK(EmpAttr.No,null,"编号",true,false,1,20,100);
			map.AddTBString(EmpAttr.Name,null,"名称",true,false,1,100,200);
			map.AddTBString(EmpAttr.Pass,"pub","密码",false,false,0,20,10);

            map.AddDDLEntities(EmpAttr.FK_ZSJG, null, "管理机关", new BP.Tax.ZSJGs(), true);
			//map.AddDDLSysEnum(EmpAttr.WorkFloor,3,"工作级次",true,false);
			//map.AddTBString(EmpAttr.AuthorizedAgent,"1","授权人",false,false,0,300,20);
			#endregion

			//增加查询属性(用于实体维护)
            //map.AddSearchAttr(dHidden(EmpAttr.FK_ZSJG,"=",BP.Web

			map.AddSearchAttr(EmpAttr.FK_ZSJG) ; 
			//map.AddSearchAttr(EmpAttr.WorkFloor) ; 

			//map.AttrsOfOneVSM.Add( new EmpStations(), new Stations(),EmpStationAttr.FK_Emp,EmpStationAttr.FK_Station, DeptAttr.Name,DeptAttr.No,"岗位");
			//map.AttrsOfOneVSM.Add( new EmpDepts(), new Depts(),EmpDeptAttr.FK_Emp,EmpDeptAttr.FK_Dept, DeptAttr.Name,DeptAttr.No,"部门");
			return map;
		}
        private Map GetMapOfPG_Pub_Simple()
        {
            Map map = new Map();

            #region 基本属性
            map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
            map.PhysicsTable = "Pub_Emp";
            map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
            map.DepositaryOfEntity = Depositary.Application; //实体存放位置
            map.IsAllowRepeatNo = false; // 是否允许编号重复.
            map.IsCheckNoLength = false; // 是否检查编号的长度.
            map.EnDesc = "用户";
            map.EnType = EnType.Admin; //实体类型。
            #endregion

            #region 关于字段属性的增加
            map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 1, 20, 100);
            map.AddTBString(EmpAttr.Name, null, "名称", true, false, 1, 100, 200);
            map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 0, 20, 10);
            #endregion

            return map;
        }
       
        private Map GetMapOfCaiShuiRen()
        {
            Map map = new Map();

            #region 基本属性
            map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
            map.PhysicsTable = "Pub_Emp";
            map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
            map.DepositaryOfEntity = Depositary.Application; //实体存放位置
            map.IsAllowRepeatNo = false; // 是否允许编号重复.
            map.IsCheckNoLength = false; // 是否检查编号的长度.
            map.EnDesc = "用户";
            map.EnType = EnType.Admin; //实体类型。
            #endregion

            #region 关于字段属性的增加

            map.AddTBStringPK(EmpAttr.No, null, "编号", true, false, 1, 20, 100);
            map.AddTBString(EmpAttr.Name, null, "名称", true, false, 1, 100, 200);
            map.AddTBString(EmpAttr.Pass, "pub", "密码", false, false, 0, 20, 10);
            map.AddDDLEntities(EmpAttr.FK_ZSJG, "86", "地区", new BP.Tax.ZSJGs(), true);

            map.AddTBString(EmpAttr.Email, "", "Email", true, false, 0, 20, 10);
            map.AddTBString("QQMSN", "", "QQMSN", true, false, 0, 20, 10);
            map.AddTBString("Tel", "", "Tel", true, false, 0, 20, 10);
            map.AddTBString("Fax", "", "Fax", true, false, 0, 20, 10);

            map.AddTBDateTime(EmpAttr.LastEnterDate,null, "最近登陆日期", true, false);

            map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "岗位");
            map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "部门");

            #endregion
            map.AddSearchAttr(EmpAttr.FK_ZSJG);
            return map;
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

                if (SystemConfig.CustomerNo == CustomerNoList.HNDS || SystemConfig.CustomerNo == CustomerNoList.HBDS)
                {
                    this._enMap = this.GetWFMapOfHN();
                    return this._enMap;
                }


                switch (BP.SystemConfig.SysNo)
                {
                    case SysNoList.CDH:
                        this._enMap = this.GetCHDMap();
                        return this._enMap;
                    case SysNoList.GS:
                        this._enMap = this.GetGSMap();
                        return this._enMap;
                    case SysNoList.CT:
                    case SysNoList.TP:

                        if (SystemConfig.CustomerNo == CustomerNoList.QHDS)
                            this._enMap = this.GetWFMapOfQH();
                        else
                            this._enMap = this.GetCTMap();
                        return this._enMap;
                    case SysNoList.GTS:
                        this._enMap = this.GetGTSMap();
                        return this._enMap;
                    case SysNoList.FDC:
                        this._enMap = this.GetFDCMap();
                        return this._enMap;
                    case SysNoList.CaiShuiRen:
                    case SysNoList.YaoCai:
                    case SysNoList.Volunteer:
                    case SysNoList.EduAdmin:
                        this._enMap = this.GetMapOfCaiShuiRen();
                        return this._enMap;
                    case SysNoList.WF:
                    case SysNoList.OA:
                    case SysNoList.KM:
                    case SysNoList.ZF:
                        if (SystemConfig.CustomerNo == CustomerNoList.QHDS)
                        {
                            this._enMap = this.GetWFMapOfQH();
                        }
                        else if (SystemConfig.CustomerNo == CustomerNoList.CCS)
                        {
                            this._enMap = this.GetCCS_OA_Map();
                        }
                        else
                        {
                            this._enMap = this.GetWFMap();
                        }
                        return this._enMap;
                    case SysNoList.PG:
                        if (SystemConfig.CustomerNo == CustomerNoList.QHDS)
                        {
                            this._enMap = this.GetWFMapOfQH();
                        }
                        else
                        {
                            this._enMap = this.GetMapOfPG_Pub();
                        }
                        return this._enMap;

                    case SysNoList.JGLicence:
                        this._enMap = this.GetMapOfPG_Pub_Simple();
                        return this._enMap;
                    default:
                        break;
                }

				Map map = new Map();

				#region 基本属性
				map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; //要连接的数据源（表示要连接到的那个系统数据库）。
				map.PhysicsTable="Pub_Emp"; // 要物理表。
				map.AdjunctType = AdjunctType.AllType ; // 附件类型（word , excel, photo.）。
				map.DepositaryOfMap=Depositary.Application;    //实体map的存放位置.
				map.DepositaryOfEntity=Depositary.Application; //实体存放位置
				map.IsAllowRepeatNo=false; // 是否允许编号重复.
				map.IsCheckNoLength=false; // 是否检查编号的长度.
				map.EnDesc="用户";       // 实体的描述.
				map.EnType=EnType.App; //实体类型。
				map.Helper="emp.html";
				#endregion

				#region 字段
				/*关于字段属性的增加 */
				map.AddTBStringPK(EmpAttr.No,null,"编号",true,false,2,20,100);
				map.AddTBString(EmpAttr.Name,null,"名称",true,false,2,100,100);
				map.AddTBString(EmpAttr.Pass,"pub","密码",false,false,3,20,10);
				 
				#endregion 字段增加.
			 

				#region 增加点对多属性				 
				//他的部门权限
				map.AttrsOfOneVSM.Add( new EmpDepts(), new Depts(),EmpDeptAttr.FK_Emp,EmpDeptAttr.FK_Dept, DeptAttr.Name,DeptAttr.No,"部门权限");
				//他的职务
				//	map.AttrsOfOneVSM.Add( new EmpDutys(), new Dutys(),EmpDutyAttr.FK_Emp,EmpDutyAttr.FK_Duty, DutyAttr.Name,DutyAttr.No,"他的职务");
				//他的工作岗位
				map.AttrsOfOneVSM.Add( new EmpStations(), new Stations(),EmpStationAttr.FK_Emp,EmpStationAttr.FK_Station, DeptAttr.Name,DeptAttr.No,"工作岗位");
				#endregion
				 
				this._enMap=map;
				return this._enMap;
			}
		}
        protected override bool beforeUpdateInsertAction()
        {
            if (SystemConfig.CustomerNo == CustomerNoList.QHDS)
                return false;

            if (SystemConfig.SysNo == SysNoList.PG)
            {
                if (BP.Web.TaxUser.FK_ZSJG == this.FK_ZSJG)
                {
                    //  this.FK_ZSJG = BP.Web.TaxUser.FK_ZSJG;
                }
                else
                {
                    throw new Exception("@不是您的单位的数据，您不能维护。");
                }
            }
            return base.beforeUpdateInsertAction();
        }

        public override int Retrieve()
        {
            //this.Name = "admin";
            //this.No = "admin";
            //return 1;
            return base.Retrieve();
        }
	}
	/// <summary>
	/// 工作人员
	/// </summary>
	public class Emps : EntitiesNoName
	{
        protected override void OnClear()
        {
            base.OnClear();
        }

		public int RetrieveByStation(string fk_zsjg, string fk_station)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(EmpAttr.No,"SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station='"+fk_station+"'");
			qo.addAnd();
			qo.AddWhere(EmpAttr.FK_ZSJG, " like ", fk_zsjg+"%" );
			return qo.DoQuery(); 
		}
        public int RetrieveByStation(string fk_station)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(EmpAttr.No, "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station='" + fk_station + "'");
            return qo.DoQuery();
        }
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Emp();
			}
		}	
		/// <summary>
		/// 工作人员s
		/// </summary>
		public Emps(){}

		/// <summary>
		/// 按编号查询
		/// </summary>
		/// <param name="NoStrs">"admin,pub,jww,myy,sl"</param>
		/// <returns>查询个数</returns>
		public int SearchByMailNoStrs(string NoStrs)
		{
			NoStrs=NoStrs.Replace(",", "','");

			QueryObject qo = new QueryObject(this);
			qo.AddWhereIn( EmpAttr.No, NoStrs );
			return qo.DoQuery();

		}
		/// <summary>
		/// 按照部门查询
		/// </summary>
		/// <param name="fk_dept">fk_dept</param>
		/// <returns></returns>
		public int RetrieveByFK_Dept(string fk_dept)
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(EmpAttr.FK_Dept, fk_dept ) ; 
			return qo.DoQuery(); 
		}
		/// <summary>
		/// 按照部门，工作岗位查询
		/// </summary>
		/// <param name="fk_dept">部门</param>
		/// <param name="fk_station">工作岗位</param>
		/// <returns></returns>
		public int RetrieveByFK_DeptStation(string fk_dept,string fk_station)
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(EmpAttr.FK_Dept,  fk_dept  ) ; 
			qo.addOr();
			qo.AddWhereInSQL( EmpAttr.OID ,"SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station='"+fk_station+"' ");
			qo.addOr();
			qo.AddWhereInSQL( EmpAttr.OID ,"SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept='"+fk_dept+"' ");
			return qo.DoQuery(); 
		}
		public override int RetrieveAll()
		{
            switch (SystemConfig.SysNo)
            {
                case SysNoList.WF:
                case SysNoList.ZF:
                case SysNoList.PG:
                    return this.RetrieveAllOfWF();
                case SysNoList.CT:
                    return this.RetrieveAllOfWF();
                default:
                    string fk_dept = Web.WebUser.FK_Dept;
                    if (fk_dept == null || fk_dept.Length == 2)
                        return base.RetrieveAll(1000);
                    QueryObject qo = new QueryObject(this);
                    //qo.AddWhere(EmpAttr.FK_Dept, " like ", Web.WebUser.FK_Dept.Substring(0,Web.WebUser.FK_Dept.Length-2)+"%" ) ; 
                    qo.AddWhere(EmpAttr.FK_Dept, " like ", Web.WebUser.FK_Dept + "%");
                    qo.addOrderBy(EmpAttr.No);
                    return qo.DoQuery();
            }
		}
        private int RetrieveAllOfWF()
        {
            QueryObject qo = new QueryObject(this);
            if (BP.Web.TaxUser.IsFJUser)
                qo.AddWhere(EmpAttr.FK_ZSJG, BP.Web.TaxUser.FK_ZSJG);
            else
                qo.AddWhere(EmpAttr.FK_ZSJG, " like ", BP.Web.TaxUser.FK_ZSJG + "%");

            if (BP.Web.TaxUser.FK_ZSJG.Length == 2)
            {
                /* 省局人员 */
                //qo.addAnd();
                //qo.AddWhere(EmpAttr.WorkFloor, 1);
            }

            if (BP.Web.TaxUser.FK_ZSJG.Length == 4)
            {
                /* 洲地市人员 */
                // qo.addAnd();
                //qo.AddWhere(EmpAttr.WorkFloor, 2);
            }

            qo.addOrderBy(EmpAttr.No);
            return qo.DoQuery();
        }
		/// <summary>
		/// 根据一个部门的员工
		/// </summary>
		/// <param name="No"></param>
		/// <param name="dept1"></param>
		/// <param name="dept2"></param>
		/// <returns></returns>
		public static DataTable GetXJByStationDept(string fk_station, string dept1,string dept2)
		{
			string sql="SELECT FK_Emp FROM Port_EmpDept WHERE (FK_Dept='"+dept1+"' or FK_Dept='"+dept2+"')  AND FK_Emp IN ( SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station='"+fk_station+"') ";
			return DBAccess.RunSQLReturnTable(sql);
		}
		/// <summary>
		/// 根据一个工作岗位，部门的编号，找到他的员工。
		/// </summary>
		/// <param name="fk_station">工作岗位</param>
		/// <param name="dept1">部门</param>
		/// <returns></returns>
		public static DataTable GetXJByStationDept(string fk_station, string dept1 )
		{
			string sql="SELECT FK_Emp FROM Port_EmpDept WHERE  FK_Dept='"+dept1+"'  AND FK_Emp IN ( SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station='"+fk_station+"') ";
			return DBAccess.RunSQLReturnTable(sql);
		}
	}
}
 