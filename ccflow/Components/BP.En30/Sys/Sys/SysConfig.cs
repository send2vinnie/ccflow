using System;
using BP.En;
using BP.DA;

namespace BP.Sys
{
	
	public class SysConfigAttr
	{
		/// <summary>
		/// Key.
		/// </summary>
		public const string  ParaKey="ParaKey";
		/// <summary>
		/// ����
		/// </summary>
		public const string  Name="Name";
		/// <summary>
		/// ϵͳ���
		/// </summary>
		public const string  SysNo="SysNo";
		/// <summary>
		/// ���
		/// </summary>
		public const string  Val="Val";	
		/// <summary>
		/// Note
		/// </summary>
		public const string  Note="Note";	 
 
	}
	public class SysConfig : Entity
	{
		#region attr
		 
		public string ParaKey
		{
			get
			{
				return  this.GetValStringByKey(SysConfigAttr.ParaKey);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.ParaKey,value);
			}
		}	
		public string Name
		{
			get
			{
				return  this.GetValStringByKey(SysConfigAttr.Name);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.Name,value);
			}
		}
		public string SysNo
		{
			get
			{
				return  this.GetValStringByKey(SysConfigAttr.SysNo);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.SysNo,value);
			}
		}
		public object ValOfObject
		{
			get
			{
				return  this.GetValByKey(SysConfigAttr.Val);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.Val,value);
			}
		}	
		public string Val
		{
			get
			{
				return  this.GetValStringByKey(SysConfigAttr.Val);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.Val,value);
			}
		}	
		public float ValOfFloat
		{
			get
			{
				return this.GetValFloatByKey(SysConfigAttr.Val);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.Val,value);
			}
		}	
		public int ValOfInt
		{
			get
			{
				return this.GetValIntByKey(SysConfigAttr.Val);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.Val,value);
			}
		}
        public decimal ValOfDecimal
        {
            get
            {
                return this.GetValDecimalByKey(SysConfigAttr.Val);
            }
            set
            {
                this.SetValByKey(SysConfigAttr.Val, value);
            }
        }
		public bool ValOfBoolen
		{
			get
			{
				return this.GetValBooleanByKey(SysConfigAttr.Val);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.Val,value);
			}
			 
		}	
		public string Note
		{
			get
			{
				return  this.GetValStringByKey(SysConfigAttr.Note);
			}
			set
			{
				this.SetValByKey(SysConfigAttr.Note,value);
			}
		}	
		#endregion

		#region stru.
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
		/// 
		/// </summary>
		public SysConfig()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		public SysConfig(string key)
		{
			this.ParaKey=key;				
			this.Retrieve();
		}
        /// <summary>
		/// ��ֵ
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="isNullAsVal"></param> 
		public SysConfig(string key,object isNullAsVal)
		{
			try
			{
				this.ParaKey=key;
				this.Retrieve(); 
			}
			catch
			{				
				if (this.RetrieveFromDBSources()==0)
				{
					this.ValOfObject = isNullAsVal;
					this.Insert();
				}
			}
		}
			  
		 
		 
		/// <summary>
		/// ��д����ķ���
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_Config");
				map.EnDesc="ѡ��";
				map.EnType=EnType.Sys;
				 
				map.AddTBStringPK(SysConfigAttr.ParaKey,null,"������",true,true,3,50,10);
				//map.AddTBStringPK(SysConfigAttr.SysNo,BP.SystemConfig.ThirdPartySoftWareKey, "���������",false,true,2,50,10);
				map.AddTBString(SysConfigAttr.Name,null,"����",true,true,0,50,10);
				map.AddTBString(SysConfigAttr.Val,null, "ֵ",true,false,0,50,10);
				map.AddTBString(SysConfigAttr.Note,null, "��ע",true,true,0,50,10);

				//map.AttrsOfSearch.AddHidden("SysNo","=",BP.SystemConfig.ThirdPartySoftWareKey) ;
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion.

	}
	public class SysConfigs : Entities
	{
		 
		/// <summary>
		/// ���������ļ�
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="val">val</param>
		public static int SetValByKey(string key,object val)
		{
			SysConfig en = new SysConfig(key,val);
			en.ValOfObject=val;
			return en.Update();
		}

		#region get value by key
        public static string GetValByKeyHtml(string key)
        {
            return DataType.ParseText2Html( SysConfigs.GetValByKey(key) );
        }
		public static string GetValByKey(string key)
		{
			foreach(SysConfig cfg in SysConfigs.MySysConfigs)
			{
				if (cfg.ParaKey==key)
					return cfg.Val;
			}

			throw new Exception("error key="+key);
		}
		/// <summary>
		/// �õ���һ��key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetValByKey(string key,string isNullAs)
		{
			foreach(SysConfig cfg in SysConfigs.MySysConfigs)
			{
				if (cfg.ParaKey==key)
					return cfg.Val;
			}

			SysConfig en = new SysConfig(key,isNullAs);
			//SysConfig en = new SysConfig(key);
			return en.Val;
		}
		public static int GetValByKeyInt(string key,int isNullAs)
		{
			foreach(SysConfig cfg in SysConfigs.MySysConfigs)
			{
				if (cfg.ParaKey==key)
					return cfg.ValOfInt;
			}

			SysConfig en = new SysConfig(key,isNullAs);
			//SysConfig en = new SysConfig(key);
			return en.ValOfInt;
		}
        public static int GetValByKeyDecimal(string key, int isNullAs)
        {
            foreach (SysConfig cfg in SysConfigs.MySysConfigs)
            {
                if (cfg.ParaKey == key)
                    return cfg.ValOfInt;
            }

            SysConfig en = new SysConfig(key, isNullAs);
            //SysConfig en = new SysConfig(key);
            return en.ValOfInt;
        }
		public static bool GetValByKeyBoolen(string key,bool isNullAs)
		{

			foreach(SysConfig cfg in SysConfigs.MySysConfigs)
			{
				if (cfg.ParaKey==key)
					return cfg.ValOfBoolen;
			}
 

			int val=0;
			if (isNullAs)
				val=1;

			SysConfig en = new SysConfig(key,val);
 
			return en.ValOfBoolen;
		}
		public static float GetValByKeyFloat(string key,float isNullAs)
		{
			foreach(SysConfig cfg in SysConfigs.MySysConfigs)
			{
				if (cfg.ParaKey==key)
					return cfg.ValOfFloat;
			}

			SysConfig en = new SysConfig(key,isNullAs);			 
			return en.ValOfFloat;
		}
		private static SysConfigs _MySysConfigs=null;
		public static SysConfigs MySysConfigs
		{
			get
			{
				if (_MySysConfigs==null)
				{
					_MySysConfigs= new SysConfigs();
					_MySysConfigs.RetrieveAll();
				}
				return _MySysConfigs;
			}
		}
		public static void ReSetVal()
		{
			_MySysConfigs=null;
		}
		#endregion


		#region stru.
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysConfig();
			}
		}		
		/// <summary>
		/// Entity
		/// </summary>
		public SysConfigs()
		{
		}
		#endregion

	}
	 
	
	
}
