/*
��飺�����ȡ���ݵ���
����ʱ�䣺2002-10
����޸�ʱ�䣺2002-10
*/
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections; 
using System.Collections.Specialized;
using System.Web;
using BP.En;

namespace BP.DA
{
	public class EnDA
	{
		#region ��ʵ��Ļ�������
		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="en"></param>
		/// <returns></returns>
		public static int Delete(Entity en) 
		{
			if (en.EnMap.EnType==EnType.View)
				return 0;
			switch(en.EnMap.EnDBUrl.DBUrlType)
			{
				case DBUrlType.AppCenterDSN :
                    return DBAccess.RunSQL(en.SQLCash.Delete, SqlBuilder.GenerParasPK(en) );
                //case DBUrlType.DBAccessOfMSSQL2000 :
                //    return DBAccessOfMSSQL2000.RunSQL(SqlBuilder.DeleteForPara(en,"@"));
                //case DBUrlType.DBAccessOfOracle9i :
                //    return DBAccessOfOracle9i.RunSQL(SqlBuilder.DeleteForPara(en, ":"));
				default :
					throw new Exception("@û���������͡�");
			}
		}
        public static int Update(Entity en)
        {
            try
            {
                switch (en.EnMap.EnDBUrl.DBUrlType)
                {
                    case DBUrlType.AppCenterDSN:
                        switch (SystemConfig.AppCenterDBType)
                        {
                            case DBType.Oracle9i:
                                return DBAccess.RunSQL(en.SQLCash.Update, SqlBuilder.GenerParas(en,null) );
                            case DBType.Access:
                                return DBAccess.RunSQL(SqlBuilder.UpdateOfMSAccess(en, null));
                            default:
                                return DBAccess.RunSQL(SqlBuilder.Update(en, null));
                        }
                    case DBUrlType.DBAccessOfMSSQL2000:
                        return DBAccessOfMSSQL2000.RunSQL(SqlBuilder.Update(en, null));
                    case DBUrlType.DBAccessOfOracle9i:
                        return DBAccessOfOracle9i.RunSQL(SqlBuilder.Update(en, null));
                    default:
                        throw new Exception("@û���������͡�");
                }
            }
            catch (Exception ex)
            {
                if (BP.SystemConfig.IsDebug)
                    en.CheckPhysicsTable();
                throw ex;
            }

        }
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="en">����Ҫ���µ����</param>
		/// <param name="keys">Ҫ���µ�����(null,��Ϊ����ȫ��)</param>
		/// <returns>sql</returns>
		public static int Update(Entity en, string[] keys)
		{
			if (en.EnMap.EnType==EnType.View)
				return 0;
            try
            {
                switch (en.EnMap.EnDBUrl.DBUrlType)
                {
                    case DBUrlType.AppCenterDSN:
                        switch (SystemConfig.AppCenterDBType)
                        {
                            case DBType.SQL2000_OK:
                            case DBType.Oracle9i:
                            case DBType.MySQL:
                                return DBAccess.RunSQL(en.SQLCash.GetUpdateSQL(en, keys), SqlBuilder.GenerParas(en, keys));
                            case DBType.Informix:
                                return DBAccess.RunSQL(en.SQLCash.GetUpdateSQL(en, keys), SqlBuilder.GenerParas_Update_Informix(en, keys));
                            case DBType.Access:
                                return DBAccess.RunSQL(SqlBuilder.UpdateOfMSAccess(en, keys));
                            default:
                                //return DBAccess.RunSQL(en.SQLCash.GetUpdateSQL(en, keys),
                                //    SqlBuilder.GenerParas(en, keys));
                                if (keys != null)
                                {
                                    Paras ps = new Paras();
                                    Paras myps = SqlBuilder.GenerParas(en, keys);
                                    foreach (Para p in myps)
                                    {
                                        foreach (string s in keys)
                                        {
                                            if (s == p.ParaName)
                                            {
                                                ps.Add(p);
                                                break;
                                            }
                                        }
                                    }
                                    return DBAccess.RunSQL(en.SQLCash.GetUpdateSQL(en, keys), ps);
                                }
                                else
                                {
                                    return DBAccess.RunSQL(en.SQLCash.GetUpdateSQL(en, keys), 
                                        SqlBuilder.GenerParas(en, keys));
                                }
                                break;

                        }
                    case DBUrlType.DBAccessOfMSSQL2000:
                        return DBAccessOfMSSQL2000.RunSQL(SqlBuilder.Update(en, keys));
                    case DBUrlType.DBAccessOfOracle9i:

                        return DBAccessOfOracle9i.RunSQL(SqlBuilder.Update(en, keys));
                    default:
                        throw new Exception("@û���������͡�");
                }
            }
            catch (Exception ex)
            {
                if (BP.SystemConfig.IsDebug)
                    en.CheckPhysicsTable();
                throw ex;
            }
		}
		 
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="en"></param>
		/// <returns></returns>
		public static int Insert_del(Entity en)
		{
			if (en.EnMap.EnType==EnType.Ext )
				throw new Exception("@ʵ��["+en.EnDesc+"]����չ���ͣ�����ִ�в��롣");

			if (en.EnMap.EnType==EnType.View)
				throw new Exception("@ʵ��["+en.EnDesc+"]����ͼ���ͣ�����ִ�в��롣");

			try
			{
				switch(en.EnMap.EnDBUrl.DBUrlType)
				{
					case DBUrlType.AppCenterDSN :
						return DBAccess.RunSQL(SqlBuilder.Insert(en));
					case DBUrlType.DBAccessOfMSSQL2000 :
						return DBAccessOfMSSQL2000.RunSQL(SqlBuilder.Insert(en));
					case DBUrlType.DBAccessOfOracle9i :
						return DBAccessOfOracle9i.RunSQL(SqlBuilder.Insert(en));
					default :
						throw new Exception("@û���������͡�");
				}		 
			}
			catch(Exception ex)
			{
				en.CheckPhysicsTable(); // ��������
				throw ex;
			}
		}
		#endregion 
	
		#region �������к��뷽��
		 
		#endregion

        public static int RetrieveV2(Entity en, string sql,Paras paras)
        {
            try
            {
                DataTable dt = new DataTable();
                switch (en.EnMap.EnDBUrl.DBUrlType)
                {
                    case DBUrlType.AppCenterDSN:
                        dt = DBAccess.RunSQLReturnTable(sql, paras);
                        break;
                    case DBUrlType.DBAccessOfMSSQL2000:
                        dt = DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
                        break;
                    case DBUrlType.DBAccessOfOracle9i:
                        dt = DBAccessOfOracle9i.RunSQLReturnTable(sql);
                        break;
                    default:
                        throw new Exception("@û������DB���͡�");
                }

                if (dt.Rows.Count == 0)
                    return 0;
                Attrs attrs = en.EnMap.Attrs;
                EnDA.fullDate(dt, en, attrs);
                int i = dt.Rows.Count;
                dt.Dispose();
                return i;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static int Retrieve(Entity en, string sql, Paras paras)
        {

            DataTable dt ;
            switch (en.EnMap.EnDBUrl.DBUrlType)
            {
                case DBUrlType.AppCenterDSN:
                    dt = DBAccess.RunSQLReturnTable(sql, paras);
                    break;
                case DBUrlType.DBAccessOfMSSQL2000:
                    dt = DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
                    break;
                case DBUrlType.DBAccessOfOracle9i:
                    dt = DBAccessOfOracle9i.RunSQLReturnTable(sql);
                    break;
                default:
                    throw new Exception("@û������DB���͡�");
            }

            if (dt.Rows.Count == 0)
                return 0;
            Attrs attrs = en.EnMap.Attrs;
            EnDA.fullDate(dt, en, attrs);
            int i = dt.Rows.Count;
            dt.Dispose();
            return i;
        }
		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="en">ʵ��</param>
		/// <param name="sql">��֯�Ĳ�ѯ���</param>
		/// <returns></returns>
        public static int Retrieve(Entity en, string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                switch (en.EnMap.EnDBUrl.DBUrlType)
                {
                    case DBUrlType.AppCenterDSN:
                        dt = DBAccess.RunSQLReturnTable(sql);
                        break;
                    case DBUrlType.DBAccessOfMSSQL2000:
                        dt = DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
                        break;
                    case DBUrlType.DBAccessOfOracle9i:
                        dt = DBAccessOfOracle9i.RunSQLReturnTable(sql);
                        break;
                    default:
                        throw new Exception("@û������DB���͡�");
                }

                if (dt.Rows.Count == 0)
                    return 0;
                Attrs attrs = en.EnMap.Attrs;
                EnDA.fullDate(dt, en, attrs);
                int i = dt.Rows.Count;
                dt.Dispose();
                return i;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
		private static void fullDate(DataTable dt, Entity en, Attrs attrs )
		{
            foreach (Attr attr in attrs)
            {
                en.Row.SetValByKey(attr.Key, dt.Rows[0][attr.Key]);
            }
		}
        public static int Retrieve(Entities ens, string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                switch (ens.GetNewEntity.EnMap.EnDBUrl.DBUrlType)
                {
                    case DBUrlType.AppCenterDSN:
                        dt = DBAccess.RunSQLReturnTable(sql);
                        break;
                    case DBUrlType.DBAccessOfMSSQL2000:
                        dt = DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
                        break;
                    case DBUrlType.DBAccessOfOracle9i:
                        dt = DBAccessOfOracle9i.RunSQLReturnTable(sql);
                        break;
                    case DBUrlType.DBAccessOfOLE:
                        dt = DBAccessOfOLE.RunSQLReturnTable(sql);
                        break;
                    default:
                        throw new Exception("@û������DB���͡�");
                }

                if (dt.Rows.Count == 0)
                    return 0;

                Map enMap = ens.GetNewEntity.EnMap;
                Attrs attrs = enMap.Attrs;

                //Entity  en1 = ens.GetNewEntity;
                foreach (DataRow dr in dt.Rows)
                {
                    Entity en = ens.GetNewEntity;
                    //Entity  en = en1.CreateInstance();
                    foreach (Attr attr in attrs)
                    {
                        en.Row.SetValByKey(attr.Key, dr[attr.Key]);
                    }
                    ens.AddEntity(en);
                }
                int i = dt.Rows.Count;
                dt.Dispose();
                return i;
                //return dt.Rows.Count;
            }
            catch (System.Exception ex)
            {
                // ens.GetNewEntity.CheckPhysicsTable();
                throw new Exception("@��[" + ens.GetNewEntity.EnDesc + "]��ѯʱ���ִ���:" + ex.Message);
            }
        }
        public static int Retrieve(Entities ens, string sql, Paras paras, string[] fullAttrs)
        {
            DataTable dt =null;
            switch (ens.GetNewEntity.EnMap.EnDBUrl.DBUrlType)
            {
                case DBUrlType.AppCenterDSN:
                    dt = DBAccess.RunSQLReturnTable(sql, paras);
                    break;
                case DBUrlType.DBAccessOfMSSQL2000:
                    dt = DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
                    break;
                case DBUrlType.DBAccessOfOracle9i:
                    dt = DBAccessOfOracle9i.RunSQLReturnTable(sql);
                    break;
                case DBUrlType.DBAccessOfOLE:
                    dt = DBAccessOfOLE.RunSQLReturnTable(sql);
                    break;
                default:
                    throw new Exception("@û������DB���͡�");
            }

            if (dt.Rows.Count == 0)
                return 0;

            if (fullAttrs == null)
            {
                Map enMap = ens.GetNewEntity.EnMap;
                Attrs attrs = enMap.Attrs;
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Entity en = ens.GetNewEntity;
                        foreach (Attr attr in attrs)
                        {
                            en.Row.SetValByKey(attr.Key, dr[attr.Key]);
                        }
                        ens.AddEntity(en);
                    }
                }
                catch(Exception ex)
                {
                    #warning ��Ӧ�ó��ֵĴ���. 2011-12-03 add
                    string cols = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        cols += " , " + dc.ColumnName;
                    }
                    throw new Exception("Columns="+cols+"@Ens=" + ens.ToString() + "@SQL=" + sql + ". @�쳣��Ϣ:" + ex.Message);
                }
            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Entity en = ens.GetNewEntity;
                    foreach (string str in fullAttrs)
                        en.Row.SetValByKey(str, dr[str]);
                    ens.AddEntity(en);
                }
            }
            int i = dt.Rows.Count;
            dt.Dispose();
            return i;
            //return dt.Rows.Count;
        }
		public static void DoCheckSession()
		{
			//return ;
			if (SystemConfig.IsDebug==false)
			{	
				//throw new Exception("@���ĵ�¼ʱ��̫���������µ�¼��") ; 
				
				HttpContext.Current.Session["url"]=HttpContext.Current.Request.RawUrl;
				string str="���ĵ�¼ʱ��̫���������µ�¼��";
				HttpContext.Current.Session["info"]=str;				
				System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/SignIn.aspx");
				//System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Portal/ErrPage.aspx");
				
			}

		}
	}
	
}
