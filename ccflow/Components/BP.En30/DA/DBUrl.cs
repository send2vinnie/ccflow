using System;
using System.Data.SqlClient;

namespace BP.DA
{
	/// <summary>
	///�����ӵ��ĸ����ϣ�
	///  ���Ǵ���� web.config ���б��ڣ�
	/// </summary>
	public enum DBUrlType
	{ 
		/// <summary>
		/// ��Ҫ��Ӧ�ó���
		/// </summary>
		AppCenterDSN,
		/// <summary>
		/// DBAccessOfOracle9i
		/// </summary>
		DBAccessOfOracle9i,
		/// <summary>
		/// DBAccessOfOracle9i1
		/// </summary>
		DBAccessOfOracle9i1,
		/// <summary>
		/// DBAccessOfMSSQL2000
		/// </summary>
		DBAccessOfMSSQL2000,
		/// <summary>
		/// access�����ӣ�
		/// </summary>
		DBAccessOfOLE,
		/// <summary>
		/// DBAccessOfODBC
		/// </summary>
		DBAccessOfODBC
	}
	/// <summary>
	/// DBUrl ��ժҪ˵����
	/// </summary>
	public class DBUrl
	{
		/// <summary>
		/// ����
		/// </summary>
		public DBUrl()
		{
		} 
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="type">����type</param>
		public DBUrl(DBUrlType type)
		{
			this.DBUrlType=type;
		}
		/// <summary>
		/// Ĭ��ֵ
		/// </summary>
		public DBUrlType  _DBUrlType=DBUrlType.AppCenterDSN;
		/// <summary>
		/// Ҫ���ӵĵ��Ŀ⡣
		/// </summary>
		public DBUrlType DBUrlType
		{
			get
			{
				return _DBUrlType;
			}
			set
			{
				_DBUrlType=value;
			}
		}
        public string DBVarStr
        {
            get
            {
                switch (this.DBType)
                {
                    case DBType.Oracle9i:
                        return ":";
                    case DBType.MySQL:
                    case DBType.SQL2000_OK:
                        return "@";
                    case DBType.Informix:
                        return "?";
                    default:
                        return "@";
                }
            }
        }
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		public DBType DBType
		{
			get
			{
				switch(this.DBUrlType)
				{
					case DBUrlType.AppCenterDSN:
						return DBAccess.AppCenterDBType ; 
					case DBUrlType.DBAccessOfMSSQL2000:
						return DBType.SQL2000_OK;
					case DBUrlType.DBAccessOfOLE:
						return DBType.Access;
					case DBUrlType.DBAccessOfOracle9i1:
                    case DBUrlType.DBAccessOfOracle9i:
						return DBType.Oracle9i ;
					default:
						throw new Exception("����ȷ������");
				}
			}
		}
	}
	
}
