using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.CN
{
	/// <summary>
	/// IP地址
	/// </summary>
    public class IPAttr : EntityNoNameAttr
    {
        #region 基本属性
        public const string FK_PQ = "FK_PQ";
        public const string FK_Area = "FK_Area";
        public const string Names = "Names";
        public const string S = "S";
        public const string E = "E";
        public const string MyPK = "MyPK";

        #endregion
    }
	/// <summary>
    /// IP地址
	/// </summary>
    public class IP : Entity
    {
        #region 基本属性
        public string MyPK
        {
            get
            {
                return this.GetValStrByKey(IPAttr.MyPK);
            }
            set
            {
                this.SetValByKey(IPAttr.MyPK, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStrByKey(CityAttr.Name);
            }
        }
        public string Names
        {
            get
            {
                return this.GetValStrByKey(CityAttr.Names);
            }
        }
        public string S
        {
            get
            {
                return this.GetValStrByKey(IPAttr.S);
            }
        }
        public string E
        {
            get
            {
                return this.GetValStrByKey(IPAttr.E);
            }
        }
        public string FK_Area
        {
            get
            {
                return this.GetValStrByKey(IPAttr.FK_Area);
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
        /// IP地址
        /// </summary>		
        public IP() { }
        //public IP(string ip)
        //{
        //    Int64 myip = this.ParseIP(ip);

        //    QueryObject qo = new QueryObject(this);
        //    qo.AddWhere(IPAttr.S, ">=", myip);
        //    qo.addAnd();
        //    qo.AddWhere(IPAttr.E, "<", myip);
        //    int i = qo.DoQuery();
        //    if (i == 0)
        //        throw new Exception("系统没有根据IP[" + ip + "]获取地址。");
        //}
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "CN_IP";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.EnDesc = "IP地址";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                #region 字段
                map.AddMyPK();
                map.AddTBString(IPAttr.Name, null, "名称", true, false, 0, 500, 200);
                map.AddTBInt(IPAttr.S, 0, "S", false, false);
                map.AddTBInt(IPAttr.E, 0, "e", false, false);
                map.AddDDLEntities(IPAttr.FK_Area, null, "Areas", new Areas(), false);
                map.AddSearchAttr(IPAttr.FK_Area);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public static Int64  ParseIPString2Int64(string ip)
        {
            string[] ipTemp2 = ip.Split('.');
            long ipTo = Convert.ToInt64(ipTemp2[0]) * 256 * 256 * 256 + Convert.ToInt64(ipTemp2[1]) * 256 * 256 + Convert.ToInt64(ipTemp2[2]) * 256 + Convert.ToInt64(ipTemp2[3]);
            return ipTo;
        }
        public static string GenerIPString2AreaNo(string ip)
        {
            if (ip == "127.0.0.1")
                return null;

            Int64 ipint = IP.ParseIPString2Int64(ip);
            string sql = "select   fk_area   from cn_ip   where    " + ipint.ToString() + "   between   s   and   e ";
            string s = DBAccess.RunSQLReturnString(sql);
            if (s == null)
                return null;

            return s;
            //Area a = new Area(s);
            //return a;
        }
        /// <summary>
        /// 产生areaNo.
        /// </summary>
        public void GenerAreaNo()
        {
            string sql = "select * from cn_ip where name like '% %' and fk_area is null  ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            BP.CN.Citys its = new Citys();
            its.RetrieveAll();

            foreach (DataRow dr in dt.Rows)
            {
                string name = dr["Name"].ToString();
                string MyPK = dr["MyPK"].ToString();

                string fk_area = BP.CN.QXS.GenerQXSNoByName(name, null);
                if (fk_area == null)
                    DBAccess.RunSQL("UPDATE CN_IP SET fk_area='unName' where mypk='" + MyPK + "'");
                else
                    DBAccess.RunSQL("UPDATE CN_IP SET fk_area='" + fk_area + "' where mypk='" + MyPK + "'");
            }
        }

        //public static uint IPtoUint(string strip)//格式化IP 
        //{
        //    uint ip1 = Convert.ToUInt32(strip.Split('.')[0]);//截取IP地址第0位，并转为无符号的整数 
        //    uint ip2 = Convert.ToUInt32(strip.Split('.')[1]);
        //    uint ip3 = Convert.ToUInt32(strip.Split('.')[2]);
        //    uint ip4 = Convert.ToUInt32(strip.Split('.')[3]);
        //    return ip1 * 256 * 256 * 256 + ip2 * 256 * 256 + ip3 * 256 + ip4;//返回最终10进制结果 
        //}

        /** 
        除以256,余数为第4个数字 
        上一步的商除以256，余数为第3个数字 
        上一步的商除以256，余数为第2个数字 
        上一步的商为第1个数 
        **/
        //将10进制整数形式转换成127.0.0.1形式的IP地址 
        public static string longToIP(long longIP)
        {
            long ip4 = longIP % 256;
            long ip3 = longIP / 256 % 256;
            long ip2 = longIP / 256 / 256 % 256;
            long ip1 = longIP / 256 / 256 / 256;
            return ip1.ToString() + "." + ip2.ToString() + "." + ip3.ToString() + "." + ip4.ToString();
        }
    }
	/// <summary>
	/// IP地址
	/// </summary>
    public class IPs : EntitiesNoName
    {
        #region  得到它的 Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new IP();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// IP地址s
        /// </summary>
        public IPs() { }
        /// <summary>
        /// IP地址s
        /// </summary>
        /// <param name="sf">省份</param>
        public IPs(string sf)
        {
            this.Retrieve(IPAttr.FK_Area, sf);
        }
        #endregion
    }
}
