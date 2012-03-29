using System;
using System.Data;
using System.Collections;
using BP;
using BP.DA;
using BP.En;

namespace BP.DTS
{
   
    public class GenerDateData : DataIOEn
    {
        public GenerDateData()
        {
            this.HisDoType = DoType.Especial;
            this.Title = "产生 Pub_ND,Pub_YF,Pub_Day,pub_week,数据。";
            this.HisUserType = Web.UserType.SysAdmin;

            this.DefaultEveryMin = "00";
            this.DefaultEveryHH = "00";
            this.DefaultEveryDay = "00";
            this.DefaultEveryMonth = "00";
            this.Note = "";
        }
        public override void Do()
        {
            this.GenerDay();
            this.GenerMonth();
            this.GenerNYR();
            this.GenerWeek();
        }
        public void GenerWeek()
        {
            //BP.DA.DBAccess.RunSQL("delete pub_week");
            //for (int i = 1; i < 55; i++)
            //{
            //    BP.Pub.Week wk = new BP.Pub.Week();
            //    wk.No = i.ToString().PadLeft(2, '0');
            //    wk.Name = "第" + i + "周";
            //    wk.Insert();
            //}
        }
        public void GenerNYR()
        {
            //Pub.NYR nyr = new BP.Pub.NYR();
            //DateTime dt = DateTime.Now.AddYears(-2);
            //DateTime dt2 = DateTime.Now.AddYears(1);
            //while (dt.ToString(DataType.SysDataFormat) != dt2.ToString(DataType.SysDataFormat))
            //{
            //    nyr.No = dt.ToString(DataType.SysDataFormat);
            //    nyr.Name = dt.ToString(DataType.SysDataFormatCN);
            //    try
            //    {
            //        nyr.Save();
            //    }
            //    catch
            //    {
            //        break;
            //    }
            //    dt = dt.AddDays(1);
            //}
        }

        public void GenerMonth()
        {
            DateTime dt = DataType.ParseSysDate2DateTime("2008-01-01");
            int i = -2;
            while (true)
            {
                i++;
                if (i > 12)
                    break;

                dt = dt.AddMonths(1);
                try
                {
                    BP.Pub.NY d = new BP.Pub.NY();
                    d.No = dt.ToString("yyyy-MM");
                    d.Name = dt.ToString("yy年MM月");
                    d.Insert();
                }
                catch
                {
                }
            }
        }
        public void GenerDay()
        {
            //DateTime dt = DataType.ParseSysDate2DateTime("2008-01-01");
            //int i = -2;
            //while (true)
            //{
            //    i++;
            //    if (i > 366)
            //        break;

            //    dt = dt.AddDays(1);

            //    try
            //    {
            //        BP.Pub.Day d = new BP.Pub.Day();
            //        d.No = dt.ToString("yyyy-MM-dd");
            //        d.Name = dt.ToString("yy年MM月dd日");
            //        d.Insert();
            //    }
            //    catch
            //    {
            //    }
            //}
        }
    }

    public class DataBakToAccess : DataIOEn
    {
        public DataBakToAccess()
        {
            this.HisDoType = DoType.Especial;
            this.Title = "备份数据库到 Access (OLE 连接中的Access) 完成后请注意查看日志文件，防止调度遗漏数据。";
            this.HisUserType = Web.UserType.SysAdmin;

            this.DefaultEveryMin = "00";
            this.DefaultEveryHH = "00";
            this.DefaultEveryDay = "00";
            this.DefaultEveryMonth = "00";
            this.Note = "";
        }
        public override void Do()
        {
            PubClass.DBIOToAccess();
        }
    }
    public class DataBakToOracle : DataIOEn
    {
        public DataBakToOracle()
        {
            this.HisDoType = DoType.Especial;
            this.Title = "备份数据库到 Oracle (连接中的DBAccessOfOracle9i) 完成后请注意查看日志文件，防止调度遗漏数据。";
            this.HisUserType = Web.UserType.SysAdmin;
            this.DefaultEveryMin = "00";
            this.DefaultEveryHH = "00";
            this.DefaultEveryDay = "00";
            this.DefaultEveryMonth = "00";
            this.Note = "";
        }
        public override void Do()
        {
            ArrayList al = BP.DA.ClassFactory.GetObjects("BP.En.Entities");
            PubClass.DBIO(DBType.Oracle9i, al, false);
        }
    }

    public class DataBakToSQLServer : DataIOEn
    {
        public DataBakToSQLServer()
        {
            this.HisDoType = DoType.Especial;
            this.Title = "备份数据库到 MSSQL2000 (连接中的DBAccessOfMSSQL2000) ,完成后请注意查看日志文件，防止调度遗漏数据。";
            this.HisUserType = Web.UserType.SysAdmin;
            this.DefaultEveryMin = "00";
            this.DefaultEveryHH = "00";
            this.DefaultEveryDay = "00";
            this.DefaultEveryMonth = "00";
            this.Note = "";
        }
        public override void Do()
        {
            ArrayList al = BP.DA.ClassFactory.GetObjects("BP.En.Entities");
            PubClass.DBIO(DBType.SQL2000, al, false);
        }
    }


	public class XmlOfDTS:DataIOEn
	{
		/// <summary>
		/// 空调度
		/// </summary>
		public XmlOfDTS()
		{
			this.HisDoType=DoType.Especial;
			this.Title="通用的xml配置调度";
			this.HisRunTimeType=RunTimeType.Day;
			this.HisUserType = Web.UserType.SysAdmin;

			this.DefaultEveryMin="00";
			this.DefaultEveryHH="00";
			this.DefaultEveryDay="00";
			this.DefaultEveryMonth="00";
			this.Note="";

			//this.DefaultEveryDay="01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31";
			//this.DefaultEveryHH="01,02,03,04,05,06,07,08,09,10,11,12";
			//this.DefaultEveryMin="01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60";
		}
		/// <summary>
		/// 空调度
		/// </summary>
		public override void Do()
		{
			DTSXmls xmls = new DTSXmls();
			xmls.RetrieveAll();
			foreach(DTSXml xml in xmls)
			{
				this.FromDBUrl=xml.DBUrlFrom;
				this.ToDBUrl=xml.DBUrlTo;
				string[] pks=xml.PKs.Split(',');
				switch(pks.Length)
				{
					case 0:
						throw new Exception("您没有为表"+xml.ToTable+"设置主键。");
						//this.Directly(xml.SQL, xml.ToTable, pks.ToString() );
					case 1:
						this.Directly(xml.SQL, xml.ToTable, pks[0] );
						break;
					case 2:
						this.Directly(xml.SQL, xml.ToTable, pks[0],pks[1]);
						break;
					case 3:
						this.Directly(xml.SQL, xml.ToTable, pks[0],pks[1],pks[2]);
						break;
					default:
						throw new Exception("error lenght");
				}
			}
		}
	}


	public class Blank:DataIOEn
	{
		/// <summary>
		/// 空调度
		/// </summary>
		public Blank()
		{
			this.HisDoType=DoType.Especial;
			this.Title="空调度";
			this.HisRunTimeType=RunTimeType.Day;
			this.HisUserType = Web.UserType.SysAdmin;

			this.DefaultEveryMin="00";
			this.DefaultEveryHH="00";
			this.DefaultEveryDay="00";
			this.DefaultEveryMonth="00";
			this.Note="用来检调度的运行情况。";

			//this.DefaultEveryDay="01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31";
			//this.DefaultEveryHH="01,02,03,04,05,06,07,08,09,10,11,12";
			//this.DefaultEveryMin="01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60";
		}
		/// <summary>
		/// 空调度
		/// </summary>
		public override void Do()
		{
			Log.DebugWriteInfo("Blank DTS。"); 
		}
	}
}
