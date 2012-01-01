using System;
using System.Collections;
using System.Data.SqlClient;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;
using System.Reflection;

namespace BP.En
{
    /// <summary>
    /// Entity 的摘要说明。
    /// </summary>	
    [Serializable]
    abstract public class Entity : EnObj
    {
        #region 与缓存有关的操作
        private Entities _GetNewEntities = null;
        public virtual Entities GetNewEntities
        {
            get
            {
                if (_GetNewEntities == null)
                {
                    string str = this.ToString();
                    ArrayList al = BP.DA.ClassFactory.GetObjects("BP.En.Entities");
                    foreach (Object o in al)
                    {
                        Entities ens = o as Entities;

                        if (ens == null)
                            continue;
                        if (ens.GetNewEntity.ToString() == str)
                        {
                            _GetNewEntities = ens;
                            return _GetNewEntities;
                        }
                    }
                    throw new Exception("@no ens" + this.ToString());
                }
                return _GetNewEntities;
            }
        }
        protected virtual string CashKey
        {
            get
            {
                return null;
            }
        }
        #endregion

        #region 与sql操作有关
        private SQLCash _SQLCash = null;
        public virtual SQLCash SQLCash
        {
            get
            {
                if (_SQLCash == null)
                {
                    _SQLCash = BP.DA.Cash.GetSQL(this.ToString());
                    if (_SQLCash == null)
                    {
                        _SQLCash = new SQLCash(this);
                        BP.DA.Cash.SetSQL(this.ToString(), _SQLCash);
                    }
                }
                return _SQLCash;
            }
            set
            {
                _SQLCash = value;
            }
        }
        #endregion

        #region 关于database 操作
        public int RunSQL(string sql)
        {
            Paras ps = new Paras();
            ps.SQL = sql;
            return this.RunSQL(ps);
        }
        /// <summary>
        /// 在此实体是运行sql 返回结果集合
        /// </summary>
        /// <param name="sql">要运行的sql</param>
        /// <returns>执行的结果</returns>
        public int RunSQL(Paras ps)
        {
            switch (this.EnMap.EnDBUrl.DBUrlType)
            {
                case DBUrlType.AppCenterDSN:
                    return DBAccess.RunSQL(ps);
                case DBUrlType.DBAccessOfMSSQL2000:
                    return DBAccessOfMSSQL2000.RunSQL(ps.SQL);
                case DBUrlType.DBAccessOfOracle9i:
                    return DBAccessOfOracle9i.RunSQL(ps.SQL);
                default:
                    throw new Exception("@没有设置类型。");
            }
        }
        public int RunSQL(string sql, Paras paras)
        {
            switch (this.EnMap.EnDBUrl.DBUrlType)
            {
                case DBUrlType.AppCenterDSN:
                    return DBAccess.RunSQL(sql, paras);
                case DBUrlType.DBAccessOfMSSQL2000:
                    return DBAccessOfMSSQL2000.RunSQL(sql);
                case DBUrlType.DBAccessOfOracle9i:
                    return DBAccessOfOracle9i.RunSQL(sql);
                default:
                    throw new Exception("@没有设置类型。");
            }
        }
        /// <summary>
        /// 在此实体是运行sql 返回结果集合
        /// </summary>
        /// <param name="sql">要运行的 select sql</param>
        /// <returns>执行的查询结果</returns>
        public DataTable RunSQLReturnTable(string sql)
        {
            switch (this.EnMap.EnDBUrl.DBUrlType)
            {
                case DBUrlType.AppCenterDSN:
                    return DBAccess.RunSQLReturnTable(sql);
                case DBUrlType.DBAccessOfMSSQL2000:
                    return DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
                case DBUrlType.DBAccessOfOracle9i:
                    return DBAccessOfOracle9i.RunSQLReturnTable(sql);
                default:
                    throw new Exception("@没有设置类型。");
            }
        }
        #endregion

        #region 关于明细的操作
        public Entities GetEnsDaOfOneVSM(AttrOfOneVSM attr)
        {
            Entities ensOfMM = attr.EnsOfMM;
            Entities ensOfM = attr.EnsOfM;
            ensOfM.Clear();

            QueryObject qo = new QueryObject(ensOfMM);
            qo.AddWhere(attr.AttrOfOneInMM, this.PKVal.ToString());
            qo.DoQuery();

            foreach (Entity en in ensOfMM)
            {
                Entity enOfM = ensOfM.GetNewEntity;
                enOfM.PKVal = en.GetValStringByKey(attr.AttrOfMInMM);
                enOfM.Retrieve();
                ensOfM.AddEntity(enOfM);
            }
            return ensOfM;
        }
        /// <summary>
        /// 取得实体集合多对多的实体集合.
        /// </summary>
        /// <param name="ensOfMMclassName">实体集合的类名称</param>
        /// <returns>数据实体</returns>
        public Entities GetEnsDaOfOneVSM(string ensOfMMclassName)
        {
            AttrOfOneVSM attr = this.EnMap.GetAttrOfOneVSM(ensOfMMclassName);

            return GetEnsDaOfOneVSM(attr);
        }
        public Entities GetEnsDaOfOneVSMFirst()
        {
            AttrOfOneVSM attr = this.EnMap.AttrsOfOneVSM[0];
            //	throw new Exception("err "+attr.Desc); 
            return this.GetEnsDaOfOneVSM(attr);
        }
        #endregion

        #region 关于明细的操作
        /// <summary>
        /// 得到他的数据实体
        /// </summary>
        /// <param name="EnsName">类名称</param>
        /// <returns></returns>
        public Entities GetDtlEnsDa(string EnsName)
        {
            Entities ens = ClassFactory.GetEns(EnsName);
            return GetDtlEnsDa(ens);
            /*
            EnDtls eds =this.EnMap.Dtls; 
            foreach(EnDtl ed in eds)
            {
                if (ed.EnsName==EnsName)
                {
                    Entities ens =ClassFactory.GetEns(EnsName) ; 
                    QueryObject qo = new QueryObject(ClassFactory.GetEns(EnsName));
                    qo.AddWhere(ed.RefKey,this.PKVal.ToString());
                    qo.DoQuery();
                    return ens;
                }
            }
            throw new Exception("@实体["+this.EnDesc+"],不包含"+EnsName);	
            */
        }
        /// <summary>
        /// 取出他的数据实体
        /// </summary>
        /// <param name="ens">集合</param>
        /// <returns>执行后的实体信息</returns>
        public Entities GetDtlEnsDa(Entities ens)
        {
            foreach (EnDtl dtl in this.EnMap.Dtls)
            {
                if (dtl.Ens.GetType() == ens.GetType())
                {
                    QueryObject qo = new QueryObject(dtl.Ens);
                    qo.AddWhere(dtl.RefKey, this.PKVal.ToString());
                    qo.DoQuery();
                    return dtl.Ens;
                }
            }
            throw new Exception("@在取[" + this.EnDesc + "]的明细时出现错误。[" + ens.GetNewEntity.EnDesc + "],不在他的集合内。");
        }

        //		/// <summary>
        //		/// 返回第一个实体
        //		/// </summary>
        //		/// <returns>返回第一个实体,如果没有就抛出异常</returns>
        //		public Entities GetDtl()
        //		{
        //			 return this.GetDtls(0);
        //		}
        //		/// <summary>
        //		/// 返回第一个实体
        //		/// </summary>
        //		/// <returns>返回第一个实体</returns>
        //		public Entities GetDtl(int index)
        //		{
        //			try
        //			{
        //				return this.GetDtls(this.EnMap.Dtls[index].Ens);
        //			}
        //			catch( Exception ex)
        //			{
        //				throw new Exception("@在取得按照顺序取["+this.EnDesc+"]的明细,出现错误:"+ex.Message);
        //			}			 
        //		}
        /// <summary>
        /// 取出他的明细集合。
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetDtlsDatasOfArrayList()
        {
            ArrayList al = new ArrayList();
            foreach (EnDtl dtl in this.EnMap.Dtls)
            {
                al.Add(this.GetDtlEnsDa(dtl.Ens));
            }
            return al;
        }
        #endregion

        #region 检查一个属性值是否存在于实体集合中
        /// <summary>
        /// 检查一个属性值是否存在于实体集合中
        /// 这个方法经常用到在beforeinsert中。
        /// </summary>
        /// <param name="key">要检查的key.</param>
        /// <param name="val">要检查的key.对应的val</param>
        /// <returns></returns>
        protected int ExitsValueNum(string key, string val)
        {
            string field = this.EnMap.GetFieldByKey(key);
            Paras ps = new Paras();
            ps.Add("p", val);

            string sql = "SELECT COUNT( " + key + " ) FROM " + this.EnMap.PhysicsTable + " WHERE " + key + "=" + this.HisDBVarStr + "p";
            return int.Parse(DBAccess.RunSQLReturnVal(sql, ps).ToString());
        }
        #endregion

        #region 于编号有关系的处理。

        /// <summary>
        /// 这个方法是为不分级字典，生成一个编号。根据制订的 属性.
        /// </summary>
        /// <param name="attrKey">属性</param>
        /// <returns>产生的序号</returns> 
        public string GenerNewNoByKey(string attrKey)
        {
            try
            {
                string sql = null;
                Attr attr = this.EnMap.GetAttrByKey(attrKey);
                if (attr.UIIsReadonly == false)
                    return "";

                string field = this.EnMap.GetFieldByKey(attrKey);
                switch (this.EnMap.EnDBUrl.DBType)
                {
                    case DBType.SQL2000:
                        sql = "SELECT CONVERT(INT, MAX(" + field + ") )+1 AS No FROM " + this.EnMap.PhysicsTable;
                        break;
                    case DBType.Access:
                        sql = "SELECT MAX( [" + field + "]) +1 AS  No FROM " + this.EnMap.PhysicsTable;
                        break;
                    case DBType.Oracle9i:
                        //sql = "SELECT MAX("+field+") +1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE  "+ field +" LIKE '%0%'";
                        sql = "SELECT MAX(" + field + ") +1 AS No FROM " + this.EnMap.PhysicsTable;
                        break;
                    default:
                        throw new Exception("error");
                }
                string str = DBAccess.RunSQLReturnVal(sql).ToString();
                if (str == "0" || str == "")
                    str = "1";
                return str.PadLeft(int.Parse(this.EnMap.CodeStruct), '0');
            }
            catch (Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
        /// <summary>
        /// 按照一列产生顺序号码。
        /// </summary>
        /// <param name="attrKey">要产生的列</param>
        /// <param name="attrGroupKey">分组的列名</param>
        /// <param name="FKVal">分组的主键</param>
        /// <returns></returns>		
        public string GenerNewNoByKey(int nolength, string attrKey, string attrGroupKey, string attrGroupVal)
        {
            if (attrGroupKey == null || attrGroupVal == null)
                throw new Exception("@分组字段attrGroupKey attrGroupVal 不能为空");

            Paras ps = new Paras();
            ps.Add("groupKey", attrGroupKey);
            ps.Add("groupVal", attrGroupVal);

            string sql = "";
            string field = this.EnMap.GetFieldByKey(attrKey);
            ps.Add("f", attrKey);

            switch (this.EnMap.EnDBUrl.DBType)
            {
                case DBType.SQL2000:
                    sql = "SELECT CONVERT(INT, MAX([" + field + "]) )+1 AS Num FROM " + this.EnMap.PhysicsTable + " WHERE " + attrGroupKey + "='" + attrGroupVal + "'";
                    break;
                case DBType.Access:
                    sql = "SELECT MAX([" + field + "]) +1 AS Num FROM " + this.EnMap.PhysicsTable + " WHERE " + attrGroupKey + "='" + attrGroupVal + "'";
                    break;
                case DBType.Oracle9i:
                    //sql = "SELECT   MAX( :f )+1 AS No FROM " + this.EnMap.PhysicsTable + " WHERE :groupKey=:groupVal AND :f LIKE '%0%'";
                    sql = "SELECT   MAX( :f )+1 AS No FROM " + this.EnMap.PhysicsTable + " WHERE " + this.HisDBVarStr + "groupKey=" + this.HisDBVarStr + "groupVal ";
                    // sql = "SELECT   MAX( :f )+1 AS No FROM " + this.EnMap.PhysicsTable + " WHERE :groupKey=:groupVal AND :f LIKE '%0%'";
                    // sql = "SELECT   MAX( " + field + " )+1 AS No FROM " + this.EnMap.PhysicsTable + " WHERE " + this.EnMap.GetFieldByKey(attrGroupKey) + "='" + attrGroupVal + "'";
                    //sql = "SELECT "+this.EnMap.GetFieldByKey( attrKey )+" +1 AS No FROM "+this.EnMap.PhysicsTable;
                    break;
                default:
                    throw new Exception("error");
            }

            DataTable dt = DBAccess.RunSQLReturnTable(sql, ps);
            string str = "1";
            if (dt.Rows.Count != 0)
            {
                //System.DBNull n = new DBNull();
                if (dt.Rows[0][0] is DBNull)
                    str = "1";
                else
                    str = dt.Rows[0][0].ToString();
            }
            return str.PadLeft(nolength, '0');
        }
        public string GenerNewNoByKey(string attrKey, string attrGroupKey, string attrGroupVal)
        {
            return this.GenerNewNoByKey(int.Parse(this.EnMap.CodeStruct), attrKey, attrGroupKey, attrGroupVal);
        }
        /// <summary>
        /// 按照两列查生顺序号码。
        /// </summary>
        /// <param name="attrKey"></param>
        /// <param name="attrGroupKey1"></param>
        /// <param name="attrGroupKey2"></param>
        /// <param name="attrGroupVal1"></param>
        /// <param name="attrGroupVal2"></param>
        /// <returns></returns>
        public string GenerNewNoByKey(string attrKey, string attrGroupKey1, string attrGroupKey2, object attrGroupVal1, object attrGroupVal2)
        {
            string f = this.EnMap.GetFieldByKey(attrKey);
            Paras ps = new Paras();
            //   ps.Add("f", f);

            string sql = "";
            switch (this.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                    sql = "SELECT   MAX(" + f + ") +1 AS No FROM " + this.EnMap.PhysicsTable;
                    break;
                case DBType.SQL2000:
                    sql = "SELECT CONVERT(INT, MAX(" + this.EnMap.GetFieldByKey(attrKey) + ") )+1 AS No FROM " + this.EnMap.PhysicsTable + " WHERE " + this.EnMap.GetFieldByKey(attrGroupKey1) + "='" + attrGroupVal1 + "' AND " + this.EnMap.GetFieldByKey(attrGroupKey2) + "='" + attrGroupVal2 + "'";
                    break;
                case DBType.Access:
                    sql = "SELECT CONVERT(INT, MAX(" + this.EnMap.GetFieldByKey(attrKey) + ") )+1 AS No FROM " + this.EnMap.PhysicsTable + " WHERE " + this.EnMap.GetFieldByKey(attrGroupKey1) + "='" + attrGroupVal1 + "' AND " + this.EnMap.GetFieldByKey(attrGroupKey2) + "='" + attrGroupVal2 + "'";
                    break;
                default:
                    break;
            }

            DataTable dt = DBAccess.RunSQLReturnTable(sql, ps);
            string str = "1";
            if (dt.Rows.Count != 0)
                str = dt.Rows[0][0].ToString();
            return str.PadLeft(int.Parse(this.EnMap.CodeStruct), '0');
        }
        #endregion

        #region 构造方法
        public Entity()
        {
        }
        #endregion


        #region 排序操作
        protected void DoOrderUp(string groupKeyAttr, string groupKeyVal, string idxAttr)
        {
            //  string pkval = this.PKVal as string;
            string pkval = this.PKVal.ToString();
            string pk = this.PK;
            string table = this.EnMap.PhysicsTable;

            string sql = "SELECT " + pk + "," + idxAttr + " FROM " + table + " WHERE " + groupKeyAttr + "='" + groupKeyVal + "' ORDER BY " + idxAttr;
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            int idx = 0;
            string beforeNo = "";
            string myNo = "";
            bool isMeet = false;

            foreach (DataRow dr in dt.Rows)
            {
                idx++;
                myNo = dr[pk].ToString();
                if (myNo == pkval)
                    isMeet = true;

                if (isMeet == false)
                    beforeNo = myNo;
                DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idx + " WHERE " + pk + "='" + myNo + "'");
            }
            DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idxAttr + "+1 WHERE " + pk + "='" + beforeNo + "'");
            DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idxAttr + "-1 WHERE " + pk + "='" + pkval + "'");
        }
        protected void DoOrderUp(string groupKeyAttr, string groupKeyVal, string gKey2, string gVal2, string idxAttr)
        {
            //  string pkval = this.PKVal as string;
            string pkval = this.PKVal.ToString();
            string pk = this.PK;
            string table = this.EnMap.PhysicsTable;

            string sql = "SELECT " + pk + "," + idxAttr + " FROM " + table + " WHERE (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKey2 + "='" + gVal2 + "') ORDER BY " + idxAttr;
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            int idx = 0;
            string beforeNo = "";
            string myNo = "";
            bool isMeet = false;

            foreach (DataRow dr in dt.Rows)
            {
                idx++;
                myNo = dr[pk].ToString();
                if (myNo == pkval)
                    isMeet = true;

                if (isMeet == false)
                    beforeNo = myNo;
                DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idx + " WHERE " + pk + "='" + myNo + "'  AND  (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKey2 + "='" + gVal2 + "') ");
            }
            DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idxAttr + "+1 WHERE " + pk + "='" + beforeNo + "'  AND  (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKey2 + "='" + gVal2 + "')");
            DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idxAttr + "-1 WHERE " + pk + "='" + pkval + "'  AND   (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKey2 + "='" + gVal2 + "')");
        }
        protected void DoOrderDown(string groupKeyAttr, string groupKeyVal, string idxAttr)
        {
            string pkval = this.PKVal.ToString();
            string pk = this.PK;
            string table = this.EnMap.PhysicsTable;

            string sql = "SELECT " + pk + " ," + idxAttr + " FROM " + table + " WHERE " + groupKeyAttr + "='" + groupKeyVal + "' order by " + idxAttr;
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            int idx = 0;
            string nextNo = "";
            string myNo = "";
            bool isMeet = false;
            foreach (DataRow dr in dt.Rows)
            {
                myNo = dr[pk].ToString();
                if (isMeet == true)
                {
                    nextNo = myNo;
                    isMeet = false;
                }
                idx++;

                if (myNo == pkval)
                    isMeet = true;
                DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idx + " WHERE " + pk + "='" + myNo + "'");
            }

            DBAccess.RunSQL("UPDATE  " + table + " SET " + idxAttr + "=" + idxAttr + "-1 WHERE " + pk + "='" + nextNo + "'");
            DBAccess.RunSQL("UPDATE  " + table + " SET " + idxAttr + "=" + idxAttr + "+1 WHERE " + pk + "='" + pkval + "'");
        }
        protected void DoOrderDown(string groupKeyAttr, string groupKeyVal, string gKeyAttr2, string gKeyVal2, string idxAttr)
        {
            string pkval = this.PKVal.ToString();
            string pk = this.PK;
            string table = this.EnMap.PhysicsTable;

            string sql = "SELECT " + pk + " ," + idxAttr + " FROM " + table + " WHERE (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKeyAttr2 + "='" + gKeyVal2 + "' ) order by " + idxAttr;
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            int idx = 0;
            string nextNo = "";
            string myNo = "";
            bool isMeet = false;
            foreach (DataRow dr in dt.Rows)
            {
                myNo = dr[pk].ToString();
                if (isMeet == true)
                {
                    nextNo = myNo;
                    isMeet = false;
                }
                idx++;

                if (myNo == pkval)
                    isMeet = true;
                DBAccess.RunSQL("UPDATE " + table + " SET " + idxAttr + "=" + idx + " WHERE " + pk + "='" + myNo + "' AND  (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKeyAttr2 + "='" + gKeyVal2 + "' ) ");
            }

            DBAccess.RunSQL("UPDATE  " + table + " SET " + idxAttr + "=" + idxAttr + "-1 WHERE " + pk + "='" + nextNo + "' AND (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKeyAttr2 + "='" + gKeyVal2 + "' )");
            DBAccess.RunSQL("UPDATE  " + table + " SET " + idxAttr + "=" + idxAttr + "+1 WHERE " + pk + "='" + pkval + "' AND (" + groupKeyAttr + "='" + groupKeyVal + "' AND " + gKeyAttr2 + "='" + gKeyVal2 + "' )");
        }
        #endregion 排序操作

        #region 基本操作

        #region 直接操作
        /// <summary>
        /// 直接更新
        /// </summary>
        public int DirectUpdate()
        {
            return EnDA.Update(this, null);
        }
        /// <summary>
        /// 直接的Insert
        /// </summary>
        public virtual void DirectInsert()
        {
            this.RunSQL(this.SQLCash.Insert, SqlBuilder.GenerParas(this, null));
        }
        /// <summary>
        /// 直接的Delete
        /// </summary>
        public void DirectDelete()
        {
            EnDA.Delete(this);
        }
        public void DirectSave()
        {
            if (this.IsExits)
                this.DirectUpdate();
            else
                this.DirectInsert();
        }
        #endregion

        #region Retrieve
        /// <summary>
        /// 按照属性查询
        /// </summary>
        /// <param name="attr">属性名称</param>
        /// <param name="val">值</param>
        /// <returns>是否查询到</returns>
        public bool RetrieveByAttrAnd(string attr1, object val1, string attr2, object val2)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(attr1, val1);
            qo.addAnd();
            qo.AddWhere(attr2, val2);

            if (qo.DoQuery() >= 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 按照属性查询
        /// </summary>
        /// <param name="attr">属性名称</param>
        /// <param name="val">值</param>
        /// <returns>是否查询到</returns>
        public bool RetrieveByAttrOr(string attr1, object val1, string attr2, object val2)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(attr1, val1);
            qo.addOr();
            qo.AddWhere(attr2, val2);

            if (qo.DoQuery() == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 按照属性查询
        /// </summary>
        /// <param name="attr">属性名称</param>
        /// <param name="val">值</param>
        /// <returns>是否查询到</returns>
        public bool RetrieveByAttr(string attr, object val)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(attr, val);
            if (qo.DoQuery() == 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 从DBSources直接查询
        /// </summary>
        /// <returns>查询的个数</returns>
        public virtual int RetrieveFromDBSources()
        {
            try
            {
               return EnDA.Retrieve(this, this.SQLCash.Select, SqlBuilder.GenerParasPK(this));
            }
            catch
            {
                this.CheckPhysicsTable();
                return EnDA.Retrieve(this, this.SQLCash.Select, SqlBuilder.GenerParasPK(this));
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int Retrieve(string key, object val)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            return qo.DoQuery();
        }

        public int Retrieve(string key1, object val1, string key2, object val2)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key1, val1);
            qo.addAnd();
            qo.AddWhere(key2, val2);
            return qo.DoQuery();
        }
        public int Retrieve(string key1, object val1, string key2, object val2, string key3, object val3)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key1, val1);
            qo.addAnd();
            qo.AddWhere(key2, val2);
            qo.addAnd();
            qo.AddWhere(key3, val3);
            // qo.AddWhere("sd", " like >" , "sds")
            return qo.DoQuery();
        }
        public virtual int Retrieve()
        {
            return this.RetrieveIt();

        }
        public virtual int Retrieve_del()
        {
            try
            {
                return this.RetrieveIt();
            }
            catch (Exception ex)
            {
                if (SystemConfig.IsDebug)
                    this.CheckPhysicsTable();

                if (ex.Message.Contains("中没有找到") == true && SystemConfig.IsBSsystem == false && SystemConfig.AppCenterDBType == DBType.Access)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(1000);
                            return this.RetrieveIt();
                        }
                        catch
                        {
                        }
                    }
                }
                throw ex;
            }
        }
        /// <summary>
        /// 按主键查询，返回查询出来的个数。
        /// 如果查询出来的是多个实体，那把第一个实体给值。	 
        /// </summary>
        /// <returns>查询出来的个数</returns>
        private int RetrieveIt()
        {
            // 判断缓存是否存在
            if (this.CashKey != null)
            {
                object obj = Cash1.Get(this.CashKey, this.PKVal.ToString());
                if (obj != null)
                {
                    Entity en = obj as Entity;
                    this.Row = en.Row;
                    //Log.DebugWriteInfo("@数据[" + this.ToString() + "][" + this.PKVal + "]来自缓存。");
                    return 1;
                }
            }

            if (this.EnMap.DepositaryOfEntity == Depositary.None)
            {
                int i = 0;
                try
                {
                    i = EnDA.Retrieve(this, this.SQLCash.Select, SqlBuilder.GenerParasPK(this));
                    if (i == 0)
                    {
                        if (SystemConfig.IsBSsystem == false)
                        {
                            this.RetrieveFromCash();
                            i = EnDA.Retrieve(this, this.SQLCash.Select, SqlBuilder.GenerParasPK(this));
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("无效"))
                        this.CheckPhysicsTable();
                    throw ex;
                }

                if (i == 0)
                {
                    string msg = "";
                    Hashtable ht = this.PKVals;
                    foreach (string key in ht.Keys)
                        msg += "[ 主键=" + key + " 值=" + ht[key] + " ]";

                    Log.DefaultLogWriteLine(LogType.Error, "@没有[" + this.EnMap.EnDesc + "  " + this.EnMap.PhysicsTable + ", 类[" + this.ToString() + "], 物理表[" + this.EnMap.PhysicsTable + "] 实例。PK = " + this.GetValByKey(this.PK));
                    if (SystemConfig.IsDebug)
                        throw new Exception("@没有[" + this.EnMap.EnDesc + "  " + this.EnMap.PhysicsTable + ", 类[" + this.ToString() + "], 物理表[" + this.EnMap.PhysicsTable + "] 实例。" + msg);
                    else
                        throw new Exception("@没有找到记录[" + this.EnMap.EnDesc + "  " + this.EnMap.PhysicsTable + ", " + msg + "记录不存在,请与管理员联系, 或者确认输入错误.");
                }

                if (this.CashKey != null)
                    Cash1.Set(this.CashKey, this.PKVal.ToString(), this, 0);
                return i;
            }

            //从全部中取数据。
            try
            {
                return this.RetrieveFromCash();
            }
            catch
            {
                Log.DefaultLogWriteLineError("@两次内存查询 EnName=" + this.ToString() + ",PKVal=" + this.PKVal);
                return this.RetrieveFromCash();
            }
        }
        private int RetrieveFromCash()
        {
            //string name=TheNameInCash;
            /* 判断在实体集合是不存在与 application 内存 */
            Entities ens;
            try
            {
                ens = Cash.GetEnsData(this.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("@在内存查询期间出现错误@" + ex.Message);
            }

            if (ens == null)
            {
                ens = this.GetNewEntities;
                ens.FlodInCash();
            }

            string pk = this.PK;
            string pkval = this.GetValStrByKey(pk);
            int count = ens.Count;

            for (int i = 0; i < count; i++)
            {
                Entity en = ens[i];
                if (en.GetValStrByKey(pk) == pkval)
                {
                    //this.Row.Clear();
                    this.Row = en.Row; /* 如果有，就返回它。*/
                    return 1;
                }
            }

            //foreach (Entity en in ens)
            //{
            //    #warning 为什么会返回的不一样的实体？但是编号一样。
            //    if (en.GetValStrByKey(pk) == pkval)
            //    {
            //        this.Row = en.Row; /* 如果有，就返回它。*/
            //        return 1;
            //    }
            //}

            if (this.RetrieveFromDBSources() != 0)
            {
                /* 从数据表中查询 */
                ens.FlodInCash();
                return 1;
            }

            Attr attr = this.EnMap.GetAttrByKey(pk);
            if (SystemConfig.IsDebug)
                throw new Exception("@在[" + this.EnDesc + this.EnMap.PhysicsTable + "]中没有找到[" + attr.Field + attr.Desc + "]=[" + this.PKVal + "]的记录。");
            else
                throw new Exception("@在[" + this.EnDesc + "]中没有找到[" + attr.Desc + "]=[" + this.PKVal + "]的记录。");
        }
        /// <summary>
        /// 判断是不是存在的方法.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsExits
        {
            get
            {
                try
                {
                    object obj = this.PKVal;
                    if (obj == null || obj.ToString() == "")
                        return false;


                    if (this.IsOIDEntity)
                        if (obj.ToString() == "0")
                            return false;


                    // 生成数据库判断语句。
                    string selectSQL = "SELECT " + this.PKField + " FROM " + this.EnMap.PhysicsTable + " WHERE ";
                    switch (this.EnMap.EnDBUrl.DBType)
                    {
                        case DBType.SQL2000:
                            selectSQL += SqlBuilder.GetKeyConditionOfMS(this);
                            break;
                        case DBType.Access:
                            selectSQL += SqlBuilder.GetKeyConditionOfOLE(this);
                            break;
                        case DBType.Oracle9i:
                            selectSQL += SqlBuilder.GetKeyConditionOfOraForPara(this);
                            break;
                        default:
                            throw new Exception("@没有设计到。" + this.EnMap.EnDBUrl.DBUrlType);
                    }

                    // 从数据库里面查询，判断有没有。
                    switch (this.EnMap.EnDBUrl.DBUrlType)
                    {
                        case DBUrlType.AppCenterDSN:
                            return DBAccess.IsExits(selectSQL, SqlBuilder.GenerParasPK(this));
                        case DBUrlType.DBAccessOfMSSQL2000:
                            return DBAccessOfMSSQL2000.IsExits(selectSQL);
                        case DBUrlType.DBAccessOfOLE:
                            return DBAccessOfOLE.IsExits(selectSQL);
                        case DBUrlType.DBAccessOfOracle9i:
                            return DBAccessOfOracle9i.IsExits(selectSQL);
                        default:
                            throw new Exception("@没有设计到的DBUrl。" + this.EnMap.EnDBUrl.DBUrlType);
                    }

                }
                catch (Exception ex)
                {
                    this.CheckPhysicsTable();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 按照主键查询，查询出来的结果不赋给当前的实体。
        /// </summary>
        /// <returns>查询出来的个数</returns>
        public DataTable RetrieveNotSetValues()
        {
            return this.RunSQLReturnTable(SqlBuilder.Retrieve(this));
        }
        /// <summary>
        /// 这个表里是否存在
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool IsExit(string pk, object val)
        {
            if (pk == "OID")
            {
                if (int.Parse(val.ToString()) == 0)
                    return false;
                else
                    return true;
            }
            //else
            //{
            //    string sql = "SELECT " + pk + " FROM " + this.EnMap.PhysicsTable + " WHERE " + pk + " ='" + val + "'";
            //    return DBAccess.IsExits(sql);
            //}

            QueryObject qo = new QueryObject(this);
            qo.AddWhere(pk, val);
            if (qo.DoQuery() == 0)
                return false;
            else
                return true;
        }
        public bool IsExit(string pk1, object val1, string pk2, object val2)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(pk1, val1);
            qo.addAnd();
            qo.AddWhere(pk2, val2);

            if (qo.DoQuery() == 0)
                return false;
            else
                return true;
        }

        public bool IsExit(string pk1, object val1, string pk2, object val2, string pk3, object val3)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(pk1, val1);
            qo.addAnd();
            qo.AddWhere(pk2, val2);
            qo.addAnd();
            qo.AddWhere(pk3, val3);

            if (qo.DoQuery() == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region delete
        private bool CheckDB()
        {

            #region 检查数据.
            //CheckDatas  ens=new CheckDatas(this.EnMap.PhysicsTable);
            //foreach(CheckData en in ens)
            //{
            //    string sql="DELETE  "+en.RefTBName+"   WHERE  "+en.RefTBFK+" ='"+this.GetValByKey(en.MainTBPK) +"' ";	
            //    DBAccess.RunSQL(sql);
            //}
            #endregion

            #region 判断是否有明细
            foreach (BP.En.EnDtl dtl in this.EnMap.Dtls)
            {
                string sql = "DELETE  FROM  " + dtl.Ens.GetNewEntity.EnMap.PhysicsTable + "   WHERE  " + dtl.RefKey + " ='" + this.PKVal.ToString() + "' ";
                //DBAccess.RunSQL(sql);
                /*
                //string sql="SELECT "+dtl.RefKey+" FROM  "+dtl.Ens.GetNewEntity.EnMap.PhysicsTable+"   WHERE  "+dtl.RefKey+" ='"+this.PKVal.ToString() +"' ";	
                DataTable dt= DBAccess.RunSQLReturnTable(sql); 
                if(dt.Rows.Count==0)
                    continue;
                else
                    throw new Exception("@["+this.EnDesc+"],删除期间出现错误，它有["+dt.Rows.Count+"]个明细存在,不能删除！");
                    */
            }
            #endregion

            return true;
        }
        /// <summary>
        /// 删除之前要做的工作
        /// </summary>
        /// <returns></returns>
        protected virtual bool beforeDelete()
        {
            if (this.EnMap.Attrs.Contains("MyFileName"))
                this.DeleteHisFiles();

            this.CheckDB();
            return true;
        }
        /// <summary>
        /// 删除它的文件
        /// </summary>
        public void DeleteHisFiles()
        {
            //  BP.DA.DBAccess.RunSQL("SELECT * FROM sys_filemanager WHERE EnName='" + this.ToString() + "' AND RefVal='" + this.PKVal + "'");

            try
            {
                BP.DA.DBAccess.RunSQL("DELETE FROM sys_filemanager WHERE EnName='" + this.ToString() + "' AND RefVal='" + this.PKVal + "'");
            }
            catch
            {

            }
        }
        /// <summary>
        /// 删除它关连的实体．
        /// </summary>
        public void DeleteHisRefEns()
        {
            #region 检查数据.
            //			CheckDatas  ens=new CheckDatas(this.EnMap.PhysicsTable);
            //			foreach(CheckData en in ens)
            //			{
            //				string sql="DELETE  "+en.RefTBName+"   WHERE  "+en.RefTBFK+" ='"+this.GetValByKey(en.MainTBPK) +"' ";	
            //				DBAccess.RunSQL(sql); 
            //			}
            #endregion

            #region 判断是否有明细
            foreach (BP.En.EnDtl dtl in this.EnMap.Dtls)
            {
                string sql = "DELETE FROM " + dtl.Ens.GetNewEntity.EnMap.PhysicsTable + "   WHERE  " + dtl.RefKey + " ='" + this.PKVal.ToString() + "' ";
                DBAccess.RunSQL(sql);
            }
            #endregion

            #region 判断是否有一对对的关系.
            foreach (BP.En.AttrOfOneVSM dtl in this.EnMap.AttrsOfOneVSM)
            {
                string sql = "DELETE  FROM " + dtl.EnsOfMM.GetNewEntity.EnMap.PhysicsTable + "   WHERE  " + dtl.AttrOfOneInMM + " ='" + this.PKVal.ToString() + "' ";
                DBAccess.RunSQL(sql);
            }
            #endregion
        }
        public int Delete()
        {
            if (this.beforeDelete() == false)
                return 0;

            int i = 0;
            try
            {
                i = EnDA.Delete(this);

                if (this.CashKey != null)
                    Cash1.Remove(this.CashKey, this.PKVal.ToString());
            }
            catch (Exception ex)
            {
                Log.DebugWriteInfo(ex.Message);
                //  this.CheckPhysicsTable();
                throw ex;
            }
            this.afterDelete();
            return i;
        }
        /// <summary>
        /// 直接删除指定的
        /// </summary>
        /// <param name="pk"></param>
        public int Delete(object pk)
        {
            Paras ps = new Paras();
            ps.Add(this.PK, pk);
            switch (this.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                case DBType.SQL2000:
                    return DBAccess.RunSQL("DELETE FROM " + this.EnMap.PhysicsTable + " WHERE " + this.PK + " =" + this.HisDBVarStr + pk);
                default:
                    throw new Exception("没有涉及到的类型。");
            }
        }
        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="val"></param>
        public int Delete(string attr, object val)
        {
            Paras ps = new Paras();
            ps.Add(attr, val);
            switch (this.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                case DBType.SQL2000:
                    return DBAccess.RunSQL("DELETE FROM " + this.EnMap.PhysicsTable + " WHERE " + this.EnMap.GetAttrByKey(attr).Field + " =" + this.HisDBVarStr + attr, ps);
                case DBType.Access:
                    return DBAccess.RunSQL("DELETE FROM " + this.EnMap.PhysicsTable + " WHERE " + this.EnMap.GetAttrByKey(attr).Field + " =" + this.HisDBVarStr + attr, ps);
                default:
                    throw new Exception("没有涉及到的类型。");
            }
        }
        public int Delete(string attr1, object val1, string attr2, object val2)
        {
            Paras ps = new Paras();
            ps.Add(attr1, val1);
            ps.Add(attr2, val2);
            switch (this.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                case DBType.SQL2000:
                case DBType.Access:
                    return DBAccess.RunSQL("DELETE FROM " + this.EnMap.PhysicsTable + " WHERE " + this.EnMap.GetAttrByKey(attr1).Field + " =" + this.HisDBVarStr + attr1 + " AND " + this.EnMap.GetAttrByKey(attr2).Field + " =" + this.HisDBVarStr + attr2, ps);
                default:
                    throw new Exception("没有涉及到的类型。");
            }
        }
        public int Delete(string attr1, object val1, string attr2, object val2, string attr3, object val3)
        {
            Paras ps = new Paras();
            ps.Add(attr1, val1);
            ps.Add(attr2, val2);
            ps.Add(attr3, val3);

            switch (this.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                case DBType.SQL2000:
                case DBType.Access:
                    return DBAccess.RunSQL("DELETE FROM " + this.EnMap.PhysicsTable + " WHERE " + this.EnMap.GetAttrByKey(attr1).Field + " =" + this.HisDBVarStr + attr1 + " AND " + this.EnMap.GetAttrByKey(attr2).Field + " =" + this.HisDBVarStr + attr2 + " AND " + this.EnMap.GetAttrByKey(attr3).Field + " =:" + attr3, ps);
                default:
                    throw new Exception("没有涉及到的类型。");
            }
        }
        protected virtual void afterDelete()
        {
            return;
        }
        #endregion



        #region 通用方法

        #endregion

        #region insert
        /// <summary>
        /// 在插入之前要做的工作。
        /// </summary>
        /// <returns></returns>
        protected virtual bool beforeInsert()
        {
            return true;
        }
        protected virtual bool roll()
        {
            return true;
        }
        public virtual void InsertWithOutPara()
        {
            this.RunSQL(SqlBuilder.Insert(this));
        }
        /// <summary>
        /// Insert .
        /// </summary>
        public virtual void Insert()
        {
            if (this.beforeInsert() == false)
                return;

            if (this.beforeUpdateInsertAction() == false)
                return;

            try
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case DBType.SQL2000:
                        this.RunSQL(this.SQLCash.Insert, SqlBuilder.GenerParas(this, null));
                        break;
                    case DBType.Access:
                        this.RunSQL(this.SQLCash.Insert, SqlBuilder.GenerParas(this, null));
                        break;
                    default:
                        this.RunSQL(this.SQLCash.Insert.Replace("[", "").Replace("]", ""), SqlBuilder.GenerParas(this, null));
                        break;
                }
            }
            catch (Exception ex)
            {
                this.roll();
                if (SystemConfig.IsDebug)
                {
                    try
                    {
                        this.CheckPhysicsTable();
                    }
                    catch (Exception ex1)
                    {
                        throw new Exception(ex.Message + " ===== " + ex1.Message);
                    }
                }
                throw new Exception(ex + "@或者检查是否有多个PK的问题.");
            }

            if (this.CashKey != null)
                Cash1.Set(this.CashKey, this.PKVal.ToString(), this, 0);

            //EnDA.Insert(this);
            //this.Retrieve();
            this.afterInsert();
            this.afterInsertUpdateAction();
        }
        protected virtual void afterInsert()
        {
            return;
        }
        /// <summary>
        /// 在更新与插入之后要做的工作.
        /// </summary>
        protected virtual void afterInsertUpdateAction()
        {
            if (this.EnMap.HisFKEnumAttrs.Count > 0)
                this.RetrieveFromDBSources();

            if (this.EnMap.IsAddRefName)
            {
                this.ReSetNameAttrVal();
                this.DirectUpdate();
            }
            return;
        }
        /// <summary>
        /// 从一个副本上copy.
        /// 用于两个数性基本相近的 实体 copy. 
        /// </summary>
        /// <param name="fromEn"></param>
        public virtual void Copy(Entity fromEn)
        {
            foreach (Attr attr in this.EnMap.Attrs)
            {
                object obj = fromEn.GetValByKey(attr.Key);
                if (obj == null)
                    continue;

                this.SetValByKey(attr.Key, obj);
            }
        }
        /// <summary>
        /// 从一个副本上
        /// </summary>
        /// <param name="fromRow"></param>
        public virtual void Copy(Row fromRow)
        {
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                try
                {
                    this.SetValByKey(attr.Key, fromRow.GetValByKey(attr.Key));
                }
                catch
                {
                }
            }
        }
        public virtual void Copy(XML.XmlEn xmlen)
        {
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                object obj = null;
                try
                {
                    obj = xmlen.GetValByKey(attr.Key);
                }
                catch
                {
                    continue;
                }

                if (obj == null || obj.ToString() == "")
                    continue;


                this.SetValByKey(attr.Key, xmlen.GetValByKey(attr.Key));
            }
        }
        public void Copy(DataRow dr)
        {
            foreach (Attr attr in this.EnMap.Attrs)
            {
                try
                {
                    this.SetValByKey(attr.Key, dr[attr.Key]);
                }
                catch
                {
                }
            }
        }
        public string Copy(string refDoc)
        {
            foreach (Attr attr in this._enMap.Attrs)
            {
                refDoc = refDoc.Replace("@" + attr.Key, this.GetValStrByKey(attr.Key));
            }
            return refDoc;
        }


        public void Copy()
        {
            foreach (Attr attr in this.EnMap.Attrs)
            {
                if (attr.IsPK == false)
                    continue;

                if (attr.MyDataType == DataType.AppInt)
                    this.SetValByKey(attr.Key, 0);
                else
                    this.SetValByKey(attr.Key, "");
            }

            try
            {
                this.SetValByKey("No", "");
            }
            catch
            {
            }
        }
        #endregion

        #region verify
        /// <summary>
        /// 校验数据
        /// </summary>
        /// <returns></returns>
        public bool verifyData()
        {
            string str = "";
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.MyDataType == DataType.AppString && attr.MinLength > 0)
                {
                    if (attr.UIIsReadonly)
                        continue;

                    string valstr = this.GetValStrByKey(attr.Key);
                    if (valstr.Length < attr.MinLength || valstr.Length > attr.MaxLength)
                    {
                        if (attr.Key == "No" && attr.UIIsReadonly)
                        {
                            if (this.GetValStringByKey(attr.Key).Trim().Length == 0 || this.GetValStringByKey(attr.Key) == "自动生成")
                                this.SetValByKey("No", this.GenerNewNoByKey("No"));
                        }
                        else
                        {

                            if (valstr.Length == 0)
                                str += "@[" + attr.Desc + "]不能为空。请输入 " + attr.MinLength + " ～ " + attr.MaxLength + " 个字符范围。";
                            else
                                str += "@[" + attr.Desc + "]输入错误，请输入 " + attr.MinLength + " ～ " + attr.MaxLength + " 个字符范围。";
                        }
                    }
                }

                //else if (attr.MyDataType == DataType.AppDateTime)
                //{
                //    if (this.GetValStringByKey(attr.Key).Trim().Length != 16)
                //    {
                //        //str+="@["+ attr.Desc +"]输入日期时间格式错误，输入的字段值["+this.GetValStringByKey(attr.Key)+"]不符合系统格式"+BP.DA.DataType.SysDataTimeFormat+"要求。";
                //    }
                //}
                //else if (attr.MyDataType == DataType.AppDate)
                //{
                //    if (this.GetValStringByKey(attr.Key).Trim().Length != 10)
                //    {
                //        //str+="@["+ attr.Desc +"]输入日期格式错误，输入的字段值["+this.GetValStringByKey(attr.Key)+"]不符合系统格式"+BP.DA.DataType.SysDataFormat+"要求。";
                //    }
                //}
            }

            if (str == "")
                return true;



            // throw new Exception("@在保存[" + this.EnDesc + "],PK[" + this.PK + "=" + this.PKVal + "]时出现信息录入不整错误：" + str);

            if (SystemConfig.IsDebug)
                throw new Exception("@在保存[" + this.EnDesc + "],主键[" + this.PK + "=" + this.PKVal + "]时出现信息录入不整错误：" + str);
            else
                throw new Exception("@在保存[" + this.EnDesc + "][" + this.EnMap.GetAttrByKey(this.PK).Desc + "=" + this.PKVal + "]时错误：" + str);
        }
        #endregion

        #region 更新，插入之前的工作。
        protected virtual bool beforeUpdateInsertAction()
        {
            switch (this.EnMap.EnType)
            {
                case EnType.View:
                case EnType.XML:
                case EnType.Ext:
                    return false;
                default:
                    break;
            }

            this.verifyData();
            return true;
        }
        #endregion 更新，插入之前的工作。

        #region 更新操作
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public virtual int Update()
        {
            return this.Update(null);
        }
        /// <summary>
        /// 仅仅更新一个属性
        /// </summary>
        /// <param name="key1">key1</param>
        /// <param name="val1">val1</param>
        /// <returns>更新的个数</returns>
        public int Update(string key1, object val1)
        {
            this.SetValByKey(key1, val1);
            return this.Update(key1.Split(','));
        }
        public int Update(string key1, object val1, string key2, object val2)
        {
            this.SetValByKey(key1, val1);
            this.SetValByKey(key2, val2);

            key1 = key1 + "," + key2;
            return this.Update(key1.Split(','));
        }
        public int Update(string key1, object val1, string key2, object val2, string key3, object val3)
        {
            this.SetValByKey(key1, val1);
            this.SetValByKey(key2, val2);
            this.SetValByKey(key3, val3);

            key1 = key1 + "," + key2 + "," + key3;
            return this.Update(key1.Split(','));
        }
        public int Update(string key1, object val1, string key2, object val2, string key3, object val3, string key4, object val4)
        {
            this.SetValByKey(key1, val1);
            this.SetValByKey(key2, val2);
            this.SetValByKey(key3, val3);
            this.SetValByKey(key4, val4);
            key1 = key1 + "," + key2 + "," + key3 + "," + key4;
            return this.Update(key1.Split(','));
        }
        public int Update(string key1, object val1, string key2, object val2, string key3, object val3, string key4, object val4, string key5, object val5)
        {
            this.SetValByKey(key1, val1);
            this.SetValByKey(key2, val2);
            this.SetValByKey(key3, val3);
            this.SetValByKey(key4, val4);
            this.SetValByKey(key5, val5);

            key1 = key1 + "," + key2 + "," + key3 + "," + key4 + "," + key5;
            return this.Update(key1.Split(','));
        }
        public int Update(string key1, object val1, string key2, object val2, string key3, object val3, string key4, object val4, string key5, object val5, string key6, object val6)
        {
            this.SetValByKey(key1, val1);
            this.SetValByKey(key2, val2);
            this.SetValByKey(key3, val3);
            this.SetValByKey(key4, val4);
            this.SetValByKey(key5, val5);
            this.SetValByKey(key6, val6);
            key1 = key1 + "," + key2 + "," + key3 + "," + key4 + "," + key5 + "," + key6;
            return this.Update(key1.Split(','));
        }
        protected virtual bool beforeUpdate()
        {
            return true;
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        public int Update(string[] keys)
        {
            string str = "";
            try
            {
                str = "@更新之前出现错误 ";
                if (this.beforeUpdate() == false)
                    return 0;

                str = "@更新插入之前出现错误";
                if (this.beforeUpdateInsertAction() == false)
                    return 0;

                str = "@更新时出现错误";
                int i = EnDA.Update(this, keys);
                str = "@更新之后出现错误";

                if (this.CashKey != null)
                    Cash1.Set(this.CashKey, this.PKVal.ToString(), this, 0);

                // 开始更新内存数据。
                switch (this.EnMap.DepositaryOfEntity)
                {
                    case Depositary.Application:
                    case Depositary.Session:
                        Cash.Remove(this.ToString());
                        break;
                    case Depositary.None:
                        break;
                }
                this.afterUpdate();
                str = "@更新插入之后出现错误";
                this.afterInsertUpdateAction();
                return i;
            }
            catch (System.Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, ex.Message);
                if (SystemConfig.IsDebug)
                    throw new Exception("@[" + this.EnDesc + "]更新期间出现错误:" + str + ex.Message);
                else
                    throw ex;
            }
        }
        private int UpdateOfDebug(string[] keys)
        {
            string str = "";
            try
            {
                str = "@在更新之前出现错误";
                if (this.beforeUpdate() == false)
                {
                    return 0;
                }
                str = "@在beforeUpdateInsertAction出现错误";
                if (this.beforeUpdateInsertAction() == false)
                {
                    return 0;
                }
                int i = EnDA.Update(this, keys);
                str = "@在afterUpdate出现错误";
                this.afterUpdate();
                str = "@在afterInsertUpdateAction出现错误";
                this.afterInsertUpdateAction();
                //this.UpdateMemory();
                return i;
            }
            catch (System.Exception ex)
            {
                string msg = "@[" + this.EnDesc + "]UpdateOfDebug更新期间出现错误:" + str + ex.Message;
                Log.DefaultLogWriteLine(LogType.Error, msg);

                if (SystemConfig.IsDebug)
                    throw new Exception(msg);
                else
                    throw ex;
            }
        }
        private void UpdateMemory_del()
        {
            Depositary where = this.EnMap.DepositaryOfEntity;

            if (where == Depositary.None)
                return;

            //string name=TheNameInCash;
            //Cash.AddObj( name, where, this);
        }
        protected virtual void afterUpdate()
        {
            return;
        }
        public virtual int Save()
        {
            switch (this.PK)
            {
                case "OID":
                    if (this.GetValIntByKey("OID") == 0)
                    {
                        //this.SetValByKey("OID",EnDA.GenerOID());
                        this.Insert();
                        return 1;
                    }
                    else
                    {
                        this.Update();
                        return 1;
                    }
                    break;
                case "MyPK":
                case "No":
                    string pk = this.GetValStrByKey(this.PK);
                    if (pk == "" || pk == null)
                    {
                        this.Insert();
                        return 1;
                    }
                    else
                    {
                        int i = this.Update();
                        if (i == 0)
                        {
                            this.Insert();
                            i = 1;
                        }
                        return i;
                    }
                    break;
                default:
                    if (this.Update() == 0)
                        this.Insert();
                    return 1;
                    break;
            }
        }
        /// <summary>
        /// 保存实体信息
        /// </summary>
        public virtual int SaveV1()
        {
            if (this.IsOIDEntity)
            {
                if (this.GetValIntByKey("OID") == 0)
                {
                    //this.SetValByKey("OID",EnDA.GenerOID());
                    this.Insert();
                    return 0;
                }
                else
                {
                    this.Update();
                    return 1;
                }
            }
            else if (this.PK == "No")
            {
                /*如果包含编号。 */
                if (this.IsExits)
                {
                    return this.Update();
                }
                else
                {
                    this.Insert();
                    return 0;
                }
            }
            else if (this.PK == "MyPK")
            {
                if (this.Update() == 0)
                    this.Insert();
                return 1;
            }


            if (this.IsExits == false)
            {
                this.Insert();
                return 0;
            }
            else
            {
                this.Update();
                return 1;
            }

        }
        public virtual int Save_Del()
        {
            if (this.IsOIDEntity)
            {
                if (this.GetValIntByKey("OID") == 0)
                {
                    //this.SetValByKey("OID",EnDA.GenerOID());
                    this.Insert();
                    return 0;
                }
                else
                {
                    this.Update();
                    return 1;
                }
            }
            else if (this.PK == "No")
            {
                /*如果包含编号。 */
                if (this.IsExits)
                {
                    return this.Update();
                }
                else
                {
                    this.Insert();
                    return 0;
                }
            }

            if (this.Update() == 0)
            {
                this.Insert();
                return 0;
            }
            else
            {
                return 1;
            }

            /*
            if (this.IsExits==false)
            {
                this.Insert();
                return 0 ;
            }
            else
            {
                this.Update();
                return 1 ;
            }			
            */
        }
        #endregion

        #endregion

        #region 关于数据库的处理
        /// <summary>
        /// 把系统日期转换为 Oracle 能够存储的日期类型.
        /// </summary>
        protected void TurnSysDataToOrData()
        {
            Map map = this.EnMap;
            string val = "";
            foreach (Attr attr in map.Attrs)
            {
                try
                {
                    val = this.GetValStringByKey(attr.Key);
                    switch (attr.MyDataType)
                    {
                        case DataType.AppDateTime:
                            if (val.ToUpper().IndexOf("_DATE") > 0)
                                continue;
                            this.SetValByKey(attr.Key, " TO_DATE('" + val + "', '" + DataType.SysDataTimeFormat + "') ");
                            break;
                        case DataType.AppDate:
                            if (val.ToUpper().IndexOf("_DATE") > 0)
                                continue;

                            if (val.Length > 10)
                                val = val.Substring(0, 10);
                            this.SetValByKey(attr.Key, " TO_DATE('" + val + "', '" + DataType.SysDataFormat + "'    )");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("执行日期转换期间出现错误:EnName=" + this.ToString() + " TurnSysDataToOrData@ Attr=" + attr.Key + " , Val=" + this.GetValStringByKey(attr.Key) + " Message=" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 检查是否是日期
        /// </summary>
        protected void CheckDateAttr()
        {
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.MyDataType == DataType.AppDate || attr.MyDataType == DataType.AppDateTime)
                {

                    DateTime dt = this.GetValDateTime(attr.Key);

                }
            }
        }
        /// <summary>
        /// 建立物理表
        /// </summary>
        protected void CreatePhysicsTable()
        {
            switch (DBAccess.AppCenterDBType)
            {
                case DBType.Oracle9i:
                    DBAccess.RunSQL(SqlBuilder.GenerCreateTableSQLOfOra(this));
                    break;
                case DBType.SQL2000:
                    DBAccess.RunSQL(SqlBuilder.GenerCreateTableSQLOfMS(this));
                    break;
                case DBType.Access:
                    DBAccess.RunSQL(SqlBuilder.GenerCreateTableSQLOf_OLE(this));
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 如果一个属性是外键，并且它还有一个字段存储它的名称。
        /// 设置这个外键名称的属性。
        /// </summary>
        protected void ReSetNameAttrVal()
        {
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.IsFKorEnum == false)
                    continue;

                string s = this.GetValRefTextByKey(attr.Key);
                this.SetValByKey(attr.Key + "Name", s);
            }
        }
        private void CheckPhysicsTableSQL()
        {
            string table=this.EnMap.PhysicsTable;
            DBType dbtype = this.EnMap.EnDBUrl.DBType;
            string sqlFields = "";
            string sqlYueShu = "";
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                    sqlFields = "SELECT column_name as FName,data_type as FType,CHARACTER_MAXIMUM_LENGTH as FLen from information_schema.columns where table_name='" + this.EnMap.PhysicsTable + "'";
                    sqlYueShu = "SELECT b.name, a.name FName from sysobjects b join syscolumns a on b.id = a.cdefault where a.id = object_id('"+this.EnMap.PhysicsTable+"') ";
                    break;
                case DBType.Oracle9i:
                    sqlFields = "SELECT column_name as FName,data_type as FType,CHARACTER_MAXIMUM_LENGTH as FLen from information_schema.columns where table_name='" + this.EnMap.PhysicsTable + "'";
                    sqlYueShu = "SELECT b.name, a.name FName from sysobjects b join syscolumns a on b.id = a.cdefault where a.id = object_id('" + this.EnMap.PhysicsTable + "') ";
                    break;
                case DBType.DB2:
                    sqlFields = "SELECT column_name as FName,data_type as FType,CHARACTER_MAXIMUM_LENGTH as FLen from information_schema.columns where table_name='" + this.EnMap.PhysicsTable + "'";
                    sqlYueShu = "SELECT b.name, a.name FName from sysobjects b join syscolumns a on b.id = a.cdefault where a.id = object_id('" + this.EnMap.PhysicsTable + "') ";
                    break;
                case DBType.MySQL:
                    sqlFields = "SELECT column_name as FName,data_type as FType,CHARACTER_MAXIMUM_LENGTH as FLen from information_schema.columns where table_name='" + this.EnMap.PhysicsTable + "'";
                    sqlYueShu = "SELECT b.name, a.name FName from sysobjects b join syscolumns a on b.id = a.cdefault where a.id = object_id('" + this.EnMap.PhysicsTable + "') ";
                    break;
                default:
                    throw new Exception("没有判断的数据库类型。");
            }

            DataTable dtAttr = DBAccess.RunSQLReturnTable(sqlFields);
            DataTable dtYueShu = DBAccess.RunSQLReturnTable(sqlYueShu);
          

            #region 修复表字段。
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.IsRefAttr)
                    continue;

                string FType = "";
                string Flen = "";

                #region 判断是否存在.
                bool isHave = false;
                foreach (DataRow dr in dtAttr.Rows)
                {
                    if (dr["FName"].ToString().ToLower() == attr.Key.ToLower())
                    {
                        isHave = true;
                        FType = dr["FType"] as string;
                        Flen = dr["FLen"].ToString();
                        break;
                    }
                }
                if (isHave == false)
                {
                    /*不存在此列 , 就增加此列。*/
                    switch (attr.MyDataType)
                    {
                        case DataType.AppString:
                        case DataType.AppDate:
                        case DataType.AppDateTime:
                            int len = attr.MaxLength;
                            if (len == 0)
                                len = 200;
                            //throw new Exception("属性的最小长度不能为0。");
                            if (dbtype == DBType.Access && len >= 254)
                                DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + "  Memo DEFAULT '" + attr.DefaultVal + "' NULL");
                            else
                                DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " VARCHAR(" + len + ") DEFAULT '" + attr.DefaultVal + "' NULL");
                            continue;
                        case DataType.AppInt:
                        case DataType.AppBoolean:
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " INT DEFAULT '" + attr.DefaultVal + "' NULL");
                            continue;
                        case DataType.AppFloat:
                        case DataType.AppMoney:
                        case DataType.AppRate:
                        case DataType.AppDouble:
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " FLOAT DEFAULT '" + attr.DefaultVal + "' NULL");
                            continue;
                        default:
                            throw new Exception("error MyFieldType= " + attr.MyFieldType + " key=" + attr.Key);
                    }
                }
                #endregion

                #region 检查类型是否匹配.
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        if (FType == "varchar")
                        {
                            /*类型正确，检查长度*/
                            if (Flen == null)
                                throw new Exception(""+attr.Key+" -"+sqlFields);
                            int len = int.Parse(Flen);
                            if (len < attr.MaxLength)
                            {
                                /*需要改变长度.*/
                                switch (dbtype)
                                {
                                    case DBType.SQL2000:
                                        DBAccess.RunSQL("alter table " + this.EnMap.PhysicsTable + " alter column " + attr.Key + " varchar(" + attr.MaxLength + ")");
                                        continue;
                                    case DBType.Oracle9i:
                                        DBAccess.RunSQL("alter table " + this.EnMap.PhysicsTable + " modify " + attr.Key + " varchar2(" + attr.MaxLength + ")");
                                        continue;
                                    default:
                                        throw new Exception("没有判断的类型。");
                                }
                            }
                        }
                        else
                        {
                            /*如果类型不匹配，就删除它在重新建, 先删除约束，在删除列，在重建。*/
                            foreach (DataRow dr in dtYueShu.Rows)
                            {
                                if (dr["FName"].ToString().ToLower() == attr.Key.ToLower())
                                    DBAccess.RunSQL("alter table " + table + " drop constraint " + dr[0].ToString()  );
                            }
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " drop column " + attr.Field);
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " VARCHAR(" + attr.MaxLength + ") DEFAULT '" + attr.DefaultVal + "' NULL");
                            continue;
                        }
                        break;
                    case DataType.AppInt:
                    case DataType.AppBoolean:
                        if (FType != "int")
                        {
                            /*如果类型不匹配，就删除它在重新建, 先删除约束，在删除列，在重建。*/
                            foreach (DataRow dr in dtYueShu.Rows)
                            {
                                if (dr["FName"].ToString().ToLower() == attr.Key.ToLower())
                                    DBAccess.RunSQL("alter table " + table + " drop constraint " + dr[0].ToString());
                            }
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " drop column " + attr.Field);
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " INT DEFAULT '" + attr.DefaultVal + "' NULL");
                            continue;
                        }
                        break;
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                    case DataType.AppRate:
                    case DataType.AppDouble:
                        if (FType != "float")
                        {
                            /*如果类型不匹配，就删除它在重新建, 先删除约束，在删除列，在重建。*/
                            foreach (DataRow dr in dtYueShu.Rows)
                            {
                                if (dr["FName"].ToString().ToLower() == attr.Key.ToLower())
                                    DBAccess.RunSQL("alter table " + table + " drop constraint " + dr[0].ToString() );
                            }
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " drop column " + attr.Field);
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " FLOAT DEFAULT '" + attr.DefaultVal + "' NULL");
                            continue;
                        }
                        break;
                    default:
                        throw new Exception("error MyFieldType= " + attr.MyFieldType + " key=" + attr.Key);
                        break;
                }
                #endregion
            }
            #endregion 修复表字段。

            #region 检查枚举类型是否存在.
            attrs = this._enMap.HisEnumAttrs;
            foreach (Attr attr in attrs)
            {
                if (attr.MyDataType != DataType.AppInt)
                    continue;

                if (attr.UITag == null)
                    continue;

                try
                {
                    SysEnums ses = new SysEnums(attr.UIBindKey, attr.UITag);
                    continue;
                }
                catch
                {
                }

                try
                {
                    string[] strs = attr.UITag.Split('@');
                    SysEnums ens = new SysEnums();
                    ens.Delete(SysEnumAttr.EnumKey, attr.UIBindKey);
                    foreach (string s in strs)
                    {
                        if (s == "" || s == null)
                            continue;

                        string[] vk = s.Split('=');
                        SysEnum se = new SysEnum();
                        se.IntKey = int.Parse(vk[0]);
                        se.Lab = vk[1];
                        se.EnumKey = attr.UIBindKey;
                        se.Insert();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("@自动增加枚举时出现错误，请确定您的格式是否正确。" + ex.Message + "attr.UIBindKey=" + attr.UIBindKey);
                }

            }
            #endregion

            #region 建立索引
            try
            {
                int pkconut = this.PKCount;
                if (pkconut == 1)
                {
                    DBAccess.CreatIndex(this.EnMap.PhysicsTable, this.PKField);
                }
                else if (pkconut == 2)
                {
                    string pk0 = this.PKs[0];
                    string pk1 = this.PKs[1];
                    DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1);
                }
                else if (pkconut == 3)
                {
                    try
                    {
                        string pk0 = this.PKs[0];
                        string pk1 = this.PKs[1];
                        string pk2 = this.PKs[2];
                        DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1, pk2);
                    }
                    catch
                    {
                    }
                }
                else if (pkconut == 4)
                {
                    try
                    {
                        string pk0 = this.PKs[0];
                        string pk1 = this.PKs[1];
                        string pk2 = this.PKs[2];
                        string pk3 = this.PKs[3];
                        DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1, pk2, pk3);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLineError(ex.Message);
                throw ex;
                //throw new Exception("create pk error :"+ex.Message );
            }
            #endregion

            #region 建立主键
            if (DBAccess.IsExitsTabPK(this.EnMap.PhysicsTable) == false)
            {
                try
                {
                    int pkconut = this.PKCount;
                    if (pkconut == 1)
                    {
                        try
                        {
                            DBAccess.CreatePK(this.EnMap.PhysicsTable, this.PKField);
                            DBAccess.CreatIndex(this.EnMap.PhysicsTable, this.PKField);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (pkconut == 2)
                    {
                        try
                        {
                            string pk0 = this.PKs[0];
                            string pk1 = this.PKs[1];
                            DBAccess.CreatePK(this.EnMap.PhysicsTable, pk0, pk1);
                            DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1);
                        }
                        catch
                        {
                        }
                    }
                    else if (pkconut == 3)
                    {
                        try
                        {
                            string pk0 = this.PKs[0];
                            string pk1 = this.PKs[1];
                            string pk2 = this.PKs[2];
                            DBAccess.CreatePK(this.EnMap.PhysicsTable, pk0, pk1, pk2);
                            DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1, pk2);
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineError(ex.Message);
                    throw ex;
                }
            }
            #endregion
        }
        /// <summary>
        /// 检查物理表
        /// </summary>
        public void CheckPhysicsTable()
        {
            //  string msg = "";
            if (this.EnMap.EnType == EnType.View
                || this.EnMap.EnType == EnType.XML
                || this.EnMap.EnType == EnType.ThirdPartApp
                || this.EnMap.EnType == EnType.Ext)
                return;


            if (DBAccess.IsExitsObject(this.EnMap.PhysicsTable) == false)
            {
                /* 如果物理表不存在就新建立一个物理表。*/
                this.CreatePhysicsTable();
            }
            if (this._enMap == null)
                this._enMap = this.EnMap;

            DBType dbtype = this._enMap.EnDBUrl.DBType;
            if (this.EnMap.IsView)
                return;

            // 如果不是主应用程序的数据库就不让执行检查. 考虑第三方的系统的安全问题.
            if (this._enMap.EnDBUrl.DBUrlType 
                != DBUrlType.AppCenterDSN)
                return;

            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                    this.CheckPhysicsTableSQL();
                    return;
                default:
                    break;
            }

            #region 检查字段是否存在
            string sql = "SELECT *  FROM " + this.EnMap.PhysicsTable + " WHERE 1=2";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

            // 如果不存在.
            foreach (Attr attr in this.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.IsPK)
                    continue;

                if (dt.Columns.Contains(attr.Key) == true)
                    continue;

                if (attr.Key == "AID")
                {
                    /* 自动增长列 */
                    DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " INT  Identity(1,1)");
                    continue;
                }

                /*不存在此列 , 就增加此列。*/
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        int len = attr.MaxLength;
                        if (len == 0)
                            len = 200;
                        //throw new Exception("属性的最小长度不能为0。");
                        if (dbtype == DBType.Access && len >= 254)
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + "  Memo DEFAULT '" + attr.DefaultVal + "' NULL");
                        else
                            DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " VARCHAR(" + len + ") DEFAULT '" + attr.DefaultVal + "' NULL");
                        break;
                    case DataType.AppInt:
                    case DataType.AppBoolean:
                        DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " INT DEFAULT '" + attr.DefaultVal + "' NULL");
                        break;
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                    case DataType.AppRate:
                    case DataType.AppDouble:
                        DBAccess.RunSQL("ALTER TABLE " + this.EnMap.PhysicsTable + " ADD " + attr.Field + " FLOAT DEFAULT '" + attr.DefaultVal + "' NULL");
                        break;
                    default:
                        throw new Exception("error MyFieldType= " + attr.MyFieldType + " key=" + attr.Key);
                }
            }
            #endregion

            #region 检查字段长度是否符合最低要求
            foreach (Attr attr in this.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.MyDataType == DataType.AppDouble
                    || attr.MyDataType == DataType.AppFloat
                    || attr.MyDataType == DataType.AppInt
                    || attr.MyDataType == DataType.AppMoney
                    || attr.MyDataType == DataType.AppBoolean
                    || attr.MyDataType == DataType.AppRate)
                    continue;

                int maxLen = attr.MaxLength;
                dt = new DataTable();
                switch (this.EnMap.EnDBUrl.DBType)
                {
                    case BP.DA.DBType.Oracle9i:
                        sql = "SELECT DATA_LENGTH AS LEN, OWNER FROM ALL_TAB_COLUMNS WHERE upper(TABLE_NAME)='" + this.EnMap.PhysicsTableExt.ToUpper() + "' AND UPPER(COLUMN_NAME)='" + attr.Field.ToUpper() + "' AND DATA_LENGTH < " + attr.MaxLength;
                        dt = this.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                            continue;

                        foreach (DataRow dr in dt.Rows)
                        {
                            try
                            {
                                this.RunSQL("alter table " + dr["OWNER"] + "." + this.EnMap.PhysicsTableExt + " modify " + attr.Field + " varchar2(" + attr.MaxLength + ")");
                            }
                            catch (Exception ex)
                            {
                                BP.DA.Log.DebugWriteWarning(ex.Message);
                            }
                        }
                        break;
                    default:
                        continue;
                }
            }
            #endregion

            #region 检查枚举类型字段是否是INT 类型
            Attrs attrs = this._enMap.HisEnumAttrs;
            foreach (Attr attr in attrs)
            {
                if (attr.MyDataType != DataType.AppInt)
                    continue;

                switch (dbtype)
                {
                    case DBType.Oracle9i:
                        sql = "SELECT DATA_TYPE FROM ALL_TAB_COLUMNS WHERE upper(TABLE_NAME)='" + this.EnMap.PhysicsTableExt.ToUpper() + "' AND UPPER(COLUMN_NAME)='" + attr.Field.ToUpper() + "' ";
                        string val = DBAccess.RunSQLReturnString(sql);
                        if (val == null)
                            Log.DefaultLogWriteLineError("@没有检测到字段eunm" + attr.Key);
                        if (val.IndexOf("CHAR") != -1)
                        {
                            /*如果它是 varchar 字段*/
                            sql = "SELECT OWNER FROM ALL_TAB_COLUMNS WHERE upper(TABLE_NAME)='" + this.EnMap.PhysicsTableExt.ToUpper() + "' AND UPPER(COLUMN_NAME)='" + attr.Field.ToUpper() + "' ";
                            string OWNER = DBAccess.RunSQLReturnString(sql);
                            try
                            {
                                this.RunSQL("alter table  " + this.EnMap.PhysicsTableExt + " modify " + attr.Field + " NUMBER ");
                            }
                            catch (Exception ex)
                            {
                                Log.DefaultLogWriteLineError("运行sql 失败:" + "alter table  " + this.EnMap.PhysicsTableExt + " modify " + attr.Field + " NUMBER " + ex.Message);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
            #endregion

            #region 检查枚举类型是否存在.
            attrs = this._enMap.HisEnumAttrs;
            foreach (Attr attr in attrs)
            {
                if (attr.MyDataType != DataType.AppInt)
                    continue;

                if (attr.UITag == null)
                    continue;

                try
                {
                    SysEnums ses = new SysEnums(attr.UIBindKey, attr.UITag);
                    continue;
                }
                catch
                {
                }

                try
                {
                    string[] strs = attr.UITag.Split('@');
                    SysEnums ens = new SysEnums();
                    ens.Delete(SysEnumAttr.EnumKey, attr.UIBindKey);
                    foreach (string s in strs)
                    {
                        if (s == "" || s == null)
                            continue;

                        string[] vk = s.Split('=');
                        SysEnum se = new SysEnum();
                        se.IntKey = int.Parse(vk[0]);
                        se.Lab = vk[1];
                        se.EnumKey = attr.UIBindKey;
                        se.Insert();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("@自动增加枚举时出现错误，请确定您的格式是否正确。" + ex.Message + "attr.UIBindKey=" + attr.UIBindKey);
                }

            }
            #endregion

            #region 建立索引
            try
            {
                int pkconut = this.PKCount;
                if (pkconut == 1)
                {
                    DBAccess.CreatIndex(this.EnMap.PhysicsTable, this.PKField);
                }
                else if (pkconut == 2)
                {
                    string pk0 = this.PKs[0];
                    string pk1 = this.PKs[1];
                    DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1);
                }
                else if (pkconut == 3)
                {
                    try
                    {
                        string pk0 = this.PKs[0];
                        string pk1 = this.PKs[1];
                        string pk2 = this.PKs[2];
                        DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1, pk2);
                    }
                    catch
                    {
                    }
                }
                else if (pkconut == 4)
                {
                    try
                    {
                        string pk0 = this.PKs[0];
                        string pk1 = this.PKs[1];
                        string pk2 = this.PKs[2];
                        string pk3 = this.PKs[3];
                        DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1, pk2, pk3);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLineError(ex.Message);
                throw ex;
                //throw new Exception("create pk error :"+ex.Message );
            }
            #endregion

            #region 建立主键
            if (DBAccess.IsExitsTabPK(this.EnMap.PhysicsTable) == false)
            {
                try
                {
                    int pkconut = this.PKCount;
                    if (pkconut == 1)
                    {
                        try
                        {
                            DBAccess.CreatePK(this.EnMap.PhysicsTable, this.PKField);
                            DBAccess.CreatIndex(this.EnMap.PhysicsTable, this.PKField);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (pkconut == 2)
                    {
                        try
                        {
                            string pk0 = this.PKs[0];
                            string pk1 = this.PKs[1];
                            DBAccess.CreatePK(this.EnMap.PhysicsTable, pk0, pk1);
                            DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1);
                        }
                        catch
                        {
                        }
                    }
                    else if (pkconut == 3)
                    {
                        try
                        {
                            string pk0 = this.PKs[0];
                            string pk1 = this.PKs[1];
                            string pk2 = this.PKs[2];
                            DBAccess.CreatePK(this.EnMap.PhysicsTable, pk0, pk1, pk2);
                            DBAccess.CreatIndex(this.EnMap.PhysicsTable, pk0, pk1, pk2);
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineError(ex.Message);
                    throw ex;
                }
            }
            #endregion

            //增加注释.
        }
        #endregion

        #region 自动处理数据
        public void AutoFull()
        {
            if (this.PKVal == "0" || this.PKVal == "")
                return;

            if (this.EnMap.IsHaveAutoFull == false)
                return;

            Attrs attrs = this.EnMap.Attrs;
            string jsAttrs = "";
            ArrayList al = new ArrayList();
            foreach (Attr attr in attrs)
            {
                if (attr.AutoFullDoc == null || attr.AutoFullDoc.Length == 0)
                    continue;

                // 这个代码需要提纯到基类中去。
                switch (attr.AutoFullWay)
                {
                    case AutoFullWay.Way0:
                        continue;
                    case AutoFullWay.Way1_JS:
                        al.Add(attr);
                        break;
                    case AutoFullWay.Way2_SQL:
                        string sql = attr.AutoFullDoc;
                        sql = sql.Replace("~", "'");
                        Attrs attrs1 = this.EnMap.Attrs;
                        foreach (Attr a1 in attrs1)
                        {
                            if (sql.Contains("@" + a1.Key) == false)
                                continue;

                            if (a1.IsNum)
                                sql = sql.Replace("@" + a1.Key, this.GetValStrByKey(a1.Key));
                            else
                                sql = sql.Replace("@" + a1.Key, "'" + this.GetValStrByKey(a1.Key) + "'");
                        }

                        //sql = sql.Replace("''", "'");
                        //  sql = sql.Replace("'''", "''");

                        string val = "";
                        try
                        {
                            val = DBAccess.RunSQLReturnString(sql);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("@自动获取数据期间错误:" + sql.Replace("'", "“") + " @Tech Info:" + ex.Message.Replace("'", "“") + "@" + sql);
                        }

                        if (attr.IsNum)
                        {
                            /* 如果是数值类型的就尝试着转换数值，转换不了就跑出异常信息。*/
                            try
                            {
                                decimal d = decimal.Parse(val);
                            }
                            catch
                            {
                                throw new Exception(val);
                            }
                        }
                        this.SetValByKey(attr.Key, val);
                        break;
                    case AutoFullWay.Way3_FK:
                        try
                        {
                            string sqlfk = "SELECT @Field FROM @Table WHERE No=@AttrKey";
                            string[] strsFK = attr.AutoFullDoc.Split('@');
                            foreach (string str in strsFK)
                            {
                                if (str == null || str.Length == 0)
                                    continue;

                                string[] ss = str.Split('=');
                                if (ss[0] == "AttrKey")
                                {
                                    string tempV = this.GetValStringByKey(ss[1]);
                                    if (tempV == "" || tempV == null)
                                    {
                                        if (this.EnMap.Attrs.Contains(ss[1]) == false)
                                            throw new Exception("@自动获取值信息不完整,Map 中已经不包含Key=" + ss[1] + "的属性。");

                                        //throw new Exception("@自动获取值信息不完整,Map 中已经不包含Key=" + ss[1] + "的属性。");
                                        sqlfk = sqlfk.Replace('@' + ss[0], "'@xxx'");
                                        Log.DefaultLogWriteLineWarning("@在自动取值期间出现错误:" + this.ToString() + " , " + this.PKVal + "没有自动获取到信息。");
                                    }
                                    else
                                    {
                                        sqlfk = sqlfk.Replace('@' + ss[0], "'" + this.GetValStringByKey(ss[1]) + "'");
                                    }
                                }
                                else
                                {
                                    sqlfk = sqlfk.Replace('@' + ss[0], ss[1]);
                                }
                            }

                            sqlfk = sqlfk.Replace("''", "'");
                            this.SetValByKey(attr.Key, DBAccess.RunSQLReturnStringIsNull(sqlfk, null));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("@在处理自动完成：外键[" + attr.Key + ";" + attr.Desc + "],时出现错误。异常信息：" + ex.Message);
                        }
                        break;
                    case AutoFullWay.Way4_Dtl:
                        if (this.PKVal == "0")
                            continue;

                        string mysql = "SELECT @Way(@Field) FROM @Table WHERE RefPK =" + this.PKVal;
                        string[] strs = attr.AutoFullDoc.Split('@');
                        foreach (string str in strs)
                        {
                            if (str == null || str.Length == 0)
                                continue;

                            string[] ss = str.Split('=');
                            mysql = mysql.Replace('@' + ss[0], ss[1]);
                        }

                        string v = DBAccess.RunSQLReturnString(mysql);
                        if (v == null)
                            v = "0";
                        this.SetValByKey(attr.Key, decimal.Parse(v));

                        break;
                    default:
                        throw new Exception("未涉及到的类型。");
                }
            }

            // 处理JS的计算。
            foreach (Attr attr in al)
            {
                string doc = attr.AutoFullDoc.Clone().ToString();
                foreach (Attr a in attrs)
                {
                    if (a.Key == attr.Key)
                        continue;

                    doc = doc.Replace("@" + a.Key, this.GetValStrByKey(a.Key));
                    doc = doc.Replace("@" + a.Desc, this.GetValStrByKey(a.Key));
                }

                try
                {
                    decimal d = DataType.ParseExpToDecimal(doc);
                    this.SetValByKey(attr.Key, d);
                }
                catch (Exception ex)
                {
                    throw new Exception("@在处理自动计算{" + this.EnDesc + "}：" + this.PK + "=" + this.PKVal + "时，属性[" + attr.Key + "]，计算内容[" + doc + "]，出现错误：" + ex.Message);
                }
            }

            //// 先处理js 计算问题。
            //if (jsAttrs.Contains("@"))
            //{
            //    string[] attrsJS = jsAttrs.Split('@');
            //    foreach (string str in attrsJS)
            //    {

            //    }
            //}
        }
        #endregion

    }
    /// <summary>
    /// 数据实体集合
    /// </summary>
    [Serializable]
    public abstract class Entities : CollectionBase
    {
        public string ToE(string no, string chVal)
        {
            return Sys.Language.GetValByUserLang(no, chVal);
        }
        public string ToEP1(string no, string chVal, string v)
        {
            return string.Format(Sys.Language.GetValByUserLang(no, chVal), v);
        }
        public string ToEP2(string no, string chVal, string v, string v1)
        {
            return string.Format(Sys.Language.GetValByUserLang(no, chVal), v, v1);
        }
        /// <summary>
        /// 获取应用配置信息
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>返回值</returns>
        public string GetEnsAppCfgByKeyString(string key)
        {
            BP.Sys.EnsAppCfg cfg = new EnsAppCfg(this.ToString(), key);
            return cfg.CfgVal;
        }
        public int GetEnsAppCfgByKeyInt(string key)
        {
            BP.Sys.EnsAppCfg cfg = new EnsAppCfg(this.ToString(), key);
            return cfg.CfgValOfInt;
        }
        public bool GetEnsAppCfgByKeyBoolen(string key)
        {
            BP.Sys.EnsAppCfg cfg = new EnsAppCfg(this.ToString(), key);
            return cfg.CfgValOfBoolen;
        }
        /// <summary>
        /// 写入到xml.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual int ExpDataToXml(string file)
        {
            DataTable dt = this.ToDataTableField();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.WriteXml(file);
            return dt.Rows.Count;
        }
        public virtual int RetrieveAllFromDBSource()
        {
            QueryObject qo = new QueryObject(this);
            return qo.DoQuery();
        }

        protected override void OnClear()
        {
            // int i = 0;
            //  throw new Exception("OnClear ");
            //  base.OnClear();
        }

        #region 过滤
        public Entity Filter(string key, string val)
        {
            foreach (Entity en in this)
            {
                if (en.GetValStringByKey(key) == val)
                    return en;
            }
            return null;
        }
        public Entity Filter(string key1, string val1, string key2, string val2)
        {
            foreach (Entity en in this)
            {
                if (en.GetValStringByKey(key1) == val1 && en.GetValStringByKey(key2) == val2)
                    return en;
            }
            return null;
        }
        public Entity Filter(string key1, string val1, string key2, string val2, string key3, string val3)
        {
            foreach (Entity en in this)
            {
                if (en.GetValStringByKey(key1) == val1 &&
                    en.GetValStringByKey(key2) == val2 &&
                    en.GetValStringByKey(key3) == val3)
                    return en;
            }
            return null;
        }
        #endregion


        #region 虚拟方法
        /// <summary>
        /// 按照属性查询
        /// </summary>
        /// <param name="attr">属性名称</param>
        /// <param name="val">值</param>
        /// <returns>是否查询到</returns>
        public int RetrieveByAttr(string attr, object val)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(attr, val);
            return qo.DoQuery();
        }
        public int RetrieveLikeAttr(string attr, string val)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(attr, " like ", val);
            return qo.DoQuery();
        }

        #endregion

        #region 扩展属性
        /// <summary>
        /// 是不是分级的字典。
        /// </summary>
        public bool IsGradeEntities
        {
            get
            {
                try
                {
                    Attr attr = null;
                    attr = this.GetNewEntity.EnMap.GetAttrByKey("Grade");
                    attr = this.GetNewEntity.EnMap.GetAttrByKey("IsDtl");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion

        #region 通过datatable 转换为实体集合
        #endregion

        #region 公共方法

        /// <summary>
        /// DBSimpleNoNames
        /// </summary>
        /// <returns></returns>
        public DBSimpleNoNames ToEntitiesNoName(string refNo, string refName)
        {
            DBSimpleNoNames ens = new DBSimpleNoNames();
            foreach (Entity en in this)
            {
                ens.AddByNoName(en.GetValStringByKey(refNo), en.GetValStringByKey(refName));
            }
            return ens;
        }
        /// <summary>
        /// 通过datatable 转换为实体集合这个Table其中一个列名称是主键
        /// </summary>
        /// <param name="dt">Table</param>
        /// <param name="fieldName">字段名称，这个字段时包含在table 中的主键 </param>
        public void InitCollectionByTable(DataTable dt, string fieldName)
        {
            Entity en = this.GetNewEntity;
            string pk = en.PK;
            foreach (DataRow dr in dt.Rows)
            {
                Entity en1 = this.GetNewEntity;
                en1.SetValByKey(pk, dr[fieldName]);
                en1.Retrieve();
                this.AddEntity(en1);
            }
        }
        /// <summary>
        /// 通过datatable 转换为实体集合.
        /// 这个Table 的结构需要与属性结构相同。
        /// </summary>
        /// <param name="dt">转换为Table</param>
        public void InitCollectionByTable(DataTable dt)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entity en = this.GetNewEntity;
                    foreach (Attr attr in en.EnMap.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.RefText)
                        {
                            try
                            {
                                en.Row.SetValByKey(attr.Key, dr[attr.Key]);
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            en.Row.SetValByKey(attr.Key, dr[attr.Key]);
                        }

                    }
                    this.AddEntity(en);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("@此表不能向集合转换详细的错误:" + ex.Message);
            }
        }
        /// <summary>
        /// 判断两个实体集合是不是相同.
        /// </summary>
        /// <param name="ens"></param>
        /// <returns></returns>
        public bool Equals(Entities ens)
        {
            if (ens.Count != this.Count)
                return false;

            foreach (Entity en in this)
            {
                bool isExits = false;
                foreach (Entity en1 in ens)
                {
                    if (en.PKVal.Equals(en1.PKVal))
                    {
                        isExits = true;
                        break;
                    }
                }
                if (isExits == false)
                    return false;
            }
            return true;
        }
        #endregion

        #region 扩展属性
        //		/// <summary>
        //		/// 他的相关功能。
        //		/// </summary>
        //		public SysUIEnsRefFuncs HisSysUIEnsRefFuncs
        //		{
        //			get
        //			{
        //				return new SysUIEnsRefFuncs(this.ToString()) ; 
        //			}
        //
        //		}
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public Entities() { }
        #endregion

        #region 公共方法
        /// <summary>
        /// 是否存在key= val 的实体。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool IsExits(string key, object val)
        {
            foreach (Entity en in this)
            {
                if (en.GetValStringByKey(key) == val.ToString())
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 创建一个该集合的元素的类型的新实例
        /// </summary>
        /// <returns></returns>
        public abstract Entity      GetNewEntity { get; }
        /// <summary>
        /// 根据位置取得数据
        /// </summary>
        public Entity this[int index]
        {
            get
            {
                return this.InnerList[index] as Entity;
            }
        }
        /// <summary>
        /// 将对象添加到集合尾处，如果对象已经存在，则不添加
        /// </summary>
        /// <param name="entity">要添加的对象</param>
        /// <returns>返回添加到的地方</returns>
        public virtual int AddEntity(Entity entity)
        {
            return this.InnerList.Add(entity);
        }
        public virtual int AddEntity(Entity entity, int idx)
        {
            this.InnerList.Insert(idx,entity);
            return idx;
        }
        public virtual void AddEntities(Entities ens)
        {
            foreach (Entity en in ens)
                this.AddEntity(en);
            // this.InnerList.AddRange(ens);
        }
        /// <summary>
        /// 增加entities
        /// </summary>
        /// <param name="pks">主键的值，中间用@符号隔开</param>
        public virtual void AddEntities(string pks)
        {
            this.Clear();
            if (pks == null || pks == "")
                return;

            string[] strs = pks.Split('@');
            foreach (string str in strs)
            {
                if (str == null || str == "")
                    continue;

                Entity en = this.GetNewEntity;
                en.PKVal = str;
                if (en.RetrieveFromDBSources() == 0)
                    continue;
                this.AddEntity(en);
            }
        }
        public virtual void Insert(int index, Entity entity)
        {
            this.InnerList.Insert(index, entity);
        }
        /// <summary>
        /// 判断是不是包含指定的Entity .
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public bool Contains(Entity en)
        {
            return this.Contains(en.PKVal);
        }
        /// <summary>
        /// 是否包含这个集合
        /// </summary>
        /// <param name="ens"></param>
        /// <returns>true / false </returns>
        public bool Contains(Entities ens)
        {
            return this.Contains(ens, ens.GetNewEntity.PK);
        }
        public bool Contains(Entities ens, string key)
        {
            if (ens.Count == 0)
                return false;
            foreach (Entity en in ens)
            {
                if (this.Contains(key, en.GetValByKey(key)) == false)
                    return false;
            }
            return true;
        }
        public bool Contains(Entities ens, string key1, string key2)
        {
            if (ens.Count == 0)
                return false;
            foreach (Entity en in ens)
            {
                if (this.Contains(key1, en.GetValByKey(key1), key2, en.GetValByKey(key2)) == false)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 是不是包含指定的PK
        /// </summary>
        /// <param name="pkVal"></param>
        /// <returns></returns>
        public bool Contains(object pkVal)
        {
            string pk = this.GetNewEntity.PK;
            return this.Contains(pk, pkVal);
        }
        /// <summary>
        /// 指定的属性里面是否包含指定的值.
        /// </summary>
        /// <param name="attr">指定的属性</param>
        /// <param name="pkVal">指定的值</param>
        /// <returns>返回是否等于</returns>
        public bool Contains(string attr, object pkVal)
        {
            foreach (Entity myen in this)
            {
                if (myen.GetValByKey(attr).ToString().Equals(pkVal.ToString()))
                    return true;
            }
            return false;
        }
        public bool Contains(string attr1, object pkVal1,string attr2, object pkVal2)
        {
            foreach (Entity myen in this)
            {
                if (myen.GetValByKey(attr1).ToString().Equals(pkVal1.ToString()) && myen.GetValByKey(attr2).ToString().Equals(pkVal2.ToString())
                    )
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 取得当前集合于传过来的集合交集.
        /// </summary>
        /// <param name="ens">一个实体集合</param>
        /// <returns>比较后的集合</returns>
        public Entities GainIntersection(Entities ens)
        {
            Entities myens = this.CreateInstance();
            string pk = this.GetNewEntity.PK;
            foreach (Entity en in this)
            {
                foreach (Entity hisen in ens)
                {
                    if (en.GetValByKey(pk).Equals(hisen.GetValByKey(pk)))
                    {
                        myens.AddEntity(en);
                    }
                }
            }
            return myens;
        }
        /// <summary>
        /// 创建立本身的一个实例.
        /// </summary>
        /// <returns>Entities</returns>
        public Entities CreateInstance()
        {
            return ClassFactory.GetEns(this.ToString());
        }
        #endregion

        #region 获取一个实体
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="val">值</param>
        /// <returns></returns>
        public Entity GetEntityByKey(object val)
        {
            string pk = this.GetNewEntity.PK;
            foreach (Entity en in this)
            {
                if (en.GetValStrByKey(pk) == val.ToString())
                    return en;
            }
            return null;
        }
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="attr">属性</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        public Entity GetEntityByKey(string attr, object val)
        {
            foreach (Entity en in this)
            {
                if (en.GetValByKey(attr).Equals(val))
                    return en;
            }
            return null;
        }
        public Entity GetEntityByKey(string attr, int val)
        {
            foreach (Entity en in this)
            {
                if (en.GetValIntByKey(attr) == val)
                    return en;
            }
            return null;
        }
        public Entity GetEntityByKey(string attr1, object val1, string attr2, object val2)
        {
            foreach (Entity en in this)
            {
                if (en.GetValStrByKey(attr1) == val1.ToString()
                    && en.GetValStrByKey(attr2) == val2.ToString())
                    return en;
            }
            return null;
        }
        public Entity GetEntityByKey(string attr1, object val1, string attr2, object val2, string attr3, object val3)
        {
            foreach (Entity en in this)
            {
                if (en.GetValByKey(attr1).Equals(val1)
                    && en.GetValByKey(attr2).Equals(val2)
                    && en.GetValByKey(attr3).Equals(val3))
                    return en;
            }
            return null;
        }
        #endregion

        #region  对一个属性操作
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal GetSumDecimalByKey(string key)
        {
            decimal sum = 0;
            foreach (Entity en in this)
            {
                sum = sum + en.GetValDecimalByKey(key);
            }
            return sum;
        }
        public decimal GetSumDecimalByKey(string key, string attrOfGroup, object valOfGroup)
        {
            decimal sum = 0;
            foreach (Entity en in this)
            {
                if (en.GetValStrByKey(attrOfGroup) == valOfGroup.ToString())
                    sum = sum + en.GetValDecimalByKey(key);
            }
            return sum;
        }
        public decimal GetAvgDecimalByKey(string key)
        {
            if (this.Count == 0)
                return 0;
            decimal sum = 0;
            foreach (Entity en in this)
            {
                sum = sum + en.GetValDecimalByKey(key);
            }
            return sum / this.Count;
        }
        public decimal GetAvgIntByKey(string key)
        {
            if (this.Count == 0)
                return 0;
            decimal sum = 0;
            foreach (Entity en in this)
            {
                sum = sum + en.GetValDecimalByKey(key);
            }
            return sum / this.Count;
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetSumIntByKey(string key)
        {
            int sum = 0;
            foreach (Entity en in this)
            {
                sum = sum + en.GetValIntByKey(key);
            }
            return sum;
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetSumFloatByKey(string key)
        {
            float sum = 0;
            foreach (Entity en in this)
            {
                sum = sum + en.GetValFloatByKey(key);
            }
            return sum;
        }

        /// <summary>
        /// 求个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetCountByKey(string key, string val)
        {
            int sum = 0;
            foreach (Entity en in this)
            {
                if (en.GetValStringByKey(key) == val)
                    sum++;
            }
            return sum;
        }
        public int GetCountByKey(string key, int val)
        {
            int sum = 0;
            foreach (Entity en in this)
            {
                if (en.GetValIntByKey(key) == val)
                    sum++;
            }
            return sum;
        }
        #endregion

        #region 对集合的操作
        /// <summary>
        /// 执行一次数据检查
        /// </summary>
        public string DoDBCheck(DBLevel level)
        {
            return PubClass.DBRpt1(level, this);
        }
        /// <summary>
        /// 从集合中删除该对象
        /// </summary>
        /// <param name="entity"></param>
        public virtual void RemoveEn(Entity entity)
        {
            this.InnerList.Remove(entity);
        }

      
        public virtual void RemoveEn(string pk)
        {
            string key = this.GetNewEntity.PK;
            RemoveEn(key, pk);
        }
        public virtual void RemoveEn(string key, string val)
        {
            foreach (Entity en in this)
            {
                if (en.GetValStringByKey(key) == val)
                {
                    this.RemoveEn(en);
                    return;
                }
            }
        }
        public virtual void Remove(string pks)
        {
            string[] mypks = pks.Split('@');
            string pkAttr = this.GetNewEntity.PK;

            foreach (string pk in mypks)
            {
                if (pk == null || pk.Length == 0)
                    continue;

                this.RemoveEn(pkAttr, pk);
            }
        }

        /// <summary>
        /// 删除table.
        /// </summary>
        /// <returns></returns>
        public int ClearTable()
        {
            Entity en = this.GetNewEntity;
            return en.RunSQL("DELETE FROM " + en.EnMap.PhysicsTable);
        }
        /// <summary>
        /// 删除集合内的对象
        /// </summary>
        public void Delete()
        {
            foreach (Entity en in this)
                if (en.IsExits)
                    en.Delete();

            this.Clear();
        }
        public int RunSQL(string sql)
        {
            return this.GetNewEntity.RunSQL(sql);
        }
        public int Delete(string key, object val)
        {
            Entity en = this.GetNewEntity;
            Paras ps = new Paras();
            ps.SQL = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " + key + "=" + en.HisDBVarStr + "p";
            ps.Add("p", val);
            return en.RunSQL(ps);
        }

        public int Delete(string key1, object val1, string key2, object val2)
        {
            Entity en = this.GetNewEntity;
            Paras ps = new Paras();
            ps.SQL = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " + key1 + "=" + en.HisDBVarStr + "p1 AND " + key2 + "=" + en.HisDBVarStr + "p2";
            ps.Add("p1", val1);
            ps.Add("p2", val2);
            return en.RunSQL(ps);

        }
        public int Delete(string key1, object val1, string key2, object val2, string key3, object val3)
        {
            Entity en = this.GetNewEntity;
            Paras ps = new Paras();
            ps.SQL = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " + key1 + "=" + en.HisDBVarStr + "p1 AND " + key2 + "=" + en.HisDBVarStr + "p2 AND " + key3 + "=" + en.HisDBVarStr + "p3";
            ps.Add("p1", val1);
            ps.Add("p2", val2);
            ps.Add("p3", val3);
            return en.RunSQL(ps);
        }
        public int Delete(string key1, object val1, string key2, object val2, string key3, object val3, string key4, object val4)
        {
            Entity en = this.GetNewEntity;
            Paras ps = new Paras();
            ps.SQL = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " + key1 + "=" + en.HisDBVarStr + "p1 AND " + key2 + "=" + en.HisDBVarStr + "p2 AND " + key3 + "=" + en.HisDBVarStr + "p3 AND " + key4 + "=" + en.HisDBVarStr + "p4";
            ps.Add("p1", val1);
            ps.Add("p2", val2);
            ps.Add("p3", val3);
            ps.Add("p4", val4);

            return en.RunSQL(ps);
        }
        /// <summary>
        /// 更新集合内的对象
        /// </summary>
        public void Update()
        {
            //string msg="";
            foreach (Entity en in this)
                en.Update();

        }
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            foreach (Entity en in this)
                en.Save();
        }
        public void SaveToXml(string file)
        {
            string dir = "";

            if (file.Contains("\\"))
            {
                dir = file.Substring(0, file.LastIndexOf('\\'));
            }
            else if (file.Contains("/"))
            {
                dir = file.Substring(0, file.LastIndexOf("/"));
            }

            if (dir != "")
            {
                if (System.IO.Directory.Exists(dir) == false)
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
            }

            DataSet ds = this.ToDataSet();
            ds.WriteXml(file);
        }
        #endregion

        #region 查询方法
        public int RetrieveByKeyNoConnection(string attr, object val)
        {
            Entity en = this.GetNewEntity;
            string pk = en.PK;

            DataTable dt = DBAccess.RunSQLReturnTable("SELECT " + pk + " FROM " + this.GetNewEntity.EnMap.PhysicsTable + " WHERE " + attr + "=" + en.HisDBVarStr + "v", "v", val);
            foreach (DataRow dr in dt.Rows)
            {
                Entity en1 = this.GetNewEntity;
                en1.SetValByKey(pk, dr[0]);
                en1.Retrieve();
                this.AddEntity(en1);
            }
            return dt.Rows.Count;
        }
        /// <summary>
        /// 按照关键字查询。
        /// 说明这里是用Attrs接受
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="al">实体</param>
        /// <returns>返回Table</returns>
        public DataTable RetrieveByKeyReturnTable(string key, Attrs attrs)
        {
            QueryObject qo = new QueryObject(this);

            // 在 Normal 属性里面增加，查询条件。
            Map map = this.GetNewEntity.EnMap;
            qo.addLeftBracket();
            foreach (Attr en in map.Attrs)
            {
                if (en.UIContralType == UIContralType.DDL || en.UIContralType == UIContralType.CheckBok)
                    continue;
                qo.addOr();
                qo.AddWhere(en.Key, " LIKE ", key);
            }
            qo.addRightBracket();

            //            //
            //			Attrs searchAttrs = map.SearchAttrs;
            //			foreach(Attr attr  in attrs)
            //			{				
            //				qo.addAnd();
            //				qo.addLeftBracket();
            //				qo.AddWhere(attr.Key, attr.DefaultVal.ToString() ) ;
            //				qo.addRightBracket();
            //			}
            return qo.DoQueryToTable();
        }
        /// <summary>
        /// 按照KEY 查找。
        /// </summary>
        /// <param name="keyVal">KEY</param>
        /// <returns>返回朝找出来的个数。</returns>
        public int RetrieveByKey(string keyVal)
        {
            keyVal = "%" + keyVal.Trim() + "%";
            QueryObject qo = new QueryObject(this);
            Attrs attrs = this.GetNewEntity.EnMap.Attrs;
            //qo.addLeftBracket();
            string pk = this.GetNewEntity.PK;
            if (pk != "OID")
                qo.AddWhere(this.GetNewEntity.PK, " LIKE ", keyVal);
            foreach (Attr en in attrs)
            {

                if (en.UIContralType == UIContralType.DDL || en.UIContralType == UIContralType.CheckBok)
                    continue;

                if (en.Key == pk)
                    continue;

                if (en.MyDataType != DataType.AppString)
                    continue;

                if (en.MyFieldType == FieldType.FK)
                    continue;

                if (en.MyFieldType == FieldType.RefText)
                    continue;

                qo.addOr();
                qo.AddWhere(en.Key, " LIKE ", keyVal);
            }
            //qo.addRightBracket();
            return qo.DoQuery();
        }
        /// <summary>
        /// 把全部的信息调入
        /// </summary>
        /// <returns>调入内存的个数</returns>
        public int FlodInCash()
        {
            // BP.DA.Log.DebugWriteInfo("FlodInCash:" + this.ToString());
            return this.FlodInCash(null);
        }
        private int FlodInCash(string orderby)
        {
            //this.Clear();
            QueryObject qo = new QueryObject(this);
            if (orderby != null)
                qo.addOrderBy(orderby);

            // qo.Top = 2000;
            int num = qo.DoQuery();

            /* 把查询个数加入内存 */
            Entity en = this.GetNewEntity;
            Cash.EnsDataSet(en.ToString(), this);

            BP.DA.Log.DefaultLogWriteLineInfo("成功[" + en.ToString() + "-" + num + "]放入缓存。");
            return num;
        }
        private int FlodInCash(string orderby,string orderBy2)
        {
            //this.Clear();
            QueryObject qo = new QueryObject(this);
            if (orderby != null)
                qo.addOrderBy(orderby, orderBy2);

            // qo.Top = 2000;
            int num = qo.DoQuery();

            /* 把查询个数加入内存 */
            Entity en = this.GetNewEntity;
            Cash.EnsDataSet(en.ToString(), this);

            BP.DA.Log.DefaultLogWriteLineInfo("成功[" + en.ToString() + "-" + num + "]放入缓存。");
            return num;
        }
        public int RetrieveByLike(string key, string val)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, " like ", val);
            return qo.DoQuery();
        }
        /// <summary>
        ///  查询出来，包涵pks 的字串。
        ///  比例："001,002,003"
        /// </summary>
        /// <returns></returns>
        public int Retrieve(string pks)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(this.GetNewEntity.PK, " in ", pks);
            return qo.DoQuery();
        }
        public int RetrieveInSQL(string attr, string sql)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(attr, sql);
            return qo.DoQuery();
        }

        public int RetrieveInSQL(string sql)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(this.GetNewEntity.PK, sql);
            return qo.DoQuery();
        }
        public int RetrieveInSQL_Order(string sql, string orderby)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(this.GetNewEntity.PK, sql);
            qo.addOrderBy(orderby);
            return qo.DoQuery();
        }
        public int Retrieve(string key, bool val)
        {
            QueryObject qo = new QueryObject(this);
            if (val)
                qo.AddWhere(key, 1);
            else
                qo.AddWhere(key, 0);
            return qo.DoQuery();
        }
        public int Retrieve(string key, object val)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            return qo.DoQuery();
        }
        public int Retrieve(string key, object val, string orderby)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            qo.addOrderBy(orderby);
            return qo.DoQuery();
        }
        //public int Retrieve(string key, object val, string orderby, int top)
        //{
        //    QueryObject qo = new QueryObject(this);
        //    qo.Top = top;
        //    qo.AddWhere(key, val);
        //    qo.addOrderBy(orderby);
        //    return qo.DoQuery();
        //}
        public int Retrieve(string key, object val, string key2, object val2)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            qo.addAnd();
            qo.AddWhere(key2, val2);

            return qo.DoQuery();
        }
        public int Retrieve(string key, object val, string key2, object val2,string ordery)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            qo.addAnd();
            qo.AddWhere(key2, val2);

            qo.addOrderBy(ordery);
            
            return qo.DoQuery();
        }
        public int Retrieve(string key, object val, string key2, object val2, string key3, object val3)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            qo.addAnd();
            qo.AddWhere(key2, val2);
            qo.addAnd();
            qo.AddWhere(key3, val3);
            return qo.DoQuery();
        }

        public int Retrieve(string key, object val, string key2, object val2, string key3, object val3, string key4, object val4)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            qo.addAnd();
            qo.AddWhere(key2, val2);
            qo.addAnd();
            qo.AddWhere(key3, val3);
            qo.addAnd();
            qo.AddWhere(key4, val4);
            return qo.DoQuery();
        }
        public int Retrieve(string key, object val, string key2, object val2, string key3, object val3, string key4, object val4,string orderBy)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(key, val);
            qo.addAnd();
            qo.AddWhere(key2, val2);
            qo.addAnd();
            qo.AddWhere(key3, val3);
            qo.addAnd();
            qo.AddWhere(key4, val4);
            qo.addOrderBy(orderBy);
            return qo.DoQuery();
        }

        #region 执行查询全部
        private int RetrieveAllFromApp_del()
        {
            this.Clear();
            string clsName = this.GetNewEntity.ToString() + "_";
            foreach (System.Collections.DictionaryEntry de in System.Web.HttpContext.Current.Cache)
            {
                if (de.Key.ToString().IndexOf(clsName) == 0)
                    this.InnerList.Add(de.Value);
            }
            return this.Count;
        }
        #endregion

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public virtual int RetrieveAll()
        {
            return this.RetrieveAll(null);
        }
        public virtual int RetrieveAllOrderByRandom()
        {
            QueryObject qo = new QueryObject(this);
            qo.addOrderByRandom();
            return qo.DoQuery();
        }
        public virtual int RetrieveAllOrderByRandom(int topNum)
        {
            QueryObject qo = new QueryObject(this);
            qo.Top = topNum;
            qo.addOrderByRandom();
            return qo.DoQuery();
        }
        public virtual int RetrieveAll(int topNum, string orderby)
        {
            QueryObject qo = new QueryObject(this);
            qo.Top = topNum;
            qo.addOrderBy(orderby);
            return qo.DoQuery();
        }
        public virtual int RetrieveAll(int topNum, string orderby, bool isDesc)
        {
            QueryObject qo = new QueryObject(this);
            qo.Top = topNum;
            if (isDesc)
                qo.addOrderByDesc(orderby);
            else
                qo.addOrderBy(orderby);
            return qo.DoQuery();
        }
        /// <summary>
        /// 查询全部。
        /// </summary>
        /// <returns></returns>
        public virtual int RetrieveAll(string orderBy)
        {
            //  this.Clear();
            Entity en = this.GetNewEntity;
            if (en.EnMap.DepositaryOfEntity == Depositary.Application)
            {
                if (this.Count > 0)
                    return this.Count;

                if (SystemConfig.IsBSsystem_Test)
                {
                    Entities ens = Cash.GetEnsData(en.ToString());
                    if (ens == null)
                    {
                        /*如果不存在于系统中。*/
                        this.FlodInCash(orderBy);
                    }
                    else
                    {
                        if (ens.Count == 0)
                            throw new Exception("@数据为0");
                        this.AddEntities(ens);
                        BP.DA.Log.DebugWriteInfo(en.ToString() + ":从内存中取出。");
                    }
                    return this.Count;
                }
            }

            QueryObject qo = new QueryObject(this);
            if (orderBy != null)
                qo.addOrderBy(orderBy);
            return qo.DoQuery();
        }
        public virtual void RemoveCash()
        {
            Cash.Remove(this.ToString());
        }
        /// <summary>
        /// 查询全部。
        /// </summary>
        /// <returns></returns>
        public virtual int RetrieveAll(string orderBy1,string orderBy2)
        {
            //  this.Clear();
            Entity en = this.GetNewEntity;
            if (en.EnMap.DepositaryOfEntity == Depositary.Application)
            {
                if (this.Count > 0)
                    return this.Count;

                if (SystemConfig.IsBSsystem_Test)
                {
                    Entities ens = Cash.GetEnsData(en.ToString());
                    if (ens == null)
                    {
                        /*如果不存在于系统中。*/
                        this.FlodInCash(orderBy1, orderBy2);
                    }
                    else
                    {
                        if (ens.Count == 0)
                            throw new Exception("@数据为0");
                        this.AddEntities(ens);
                        BP.DA.Log.DebugWriteInfo(en.ToString() + ":从内存中取出。");
                    }
                    return this.Count;
                }
            }

            QueryObject qo = new QueryObject(this);
            if (orderBy1 != null)
                qo.addOrderBy(orderBy1, orderBy2);
            return qo.DoQuery();
        }
        /// <summary>
        /// 按照最大个数查询。
        /// </summary>
        /// <param name="MaxNum">最大NUM</param>
        /// <returns></returns>
        public int RetrieveAll(int MaxNum)
        {
            QueryObject qo = new QueryObject(this);
            qo.Top = MaxNum;
            return qo.DoQuery();
        }
        /// <summary>
        /// 查询全部的结果放到DataTable。
        /// </summary>
        /// <returns></returns>
        public DataTable RetrieveAllToTable()
        {
            QueryObject qo = new QueryObject(this);
            return qo.DoQueryToTable();
        }
        private DataTable DealBoolTypeInDataTable(Entity en, DataTable dt)
        {

            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyDataType == DataType.AppBoolean)
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = "tmp" + attr.Key;
                    col.DataType = typeof(bool);
                    dt.Columns.Add(col);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[attr.Key].ToString() == "1")
                            dr["tmp" + attr.Key] = true;
                        else
                            dr["tmp" + attr.Key] = false;
                    }
                    dt.Columns.Remove(attr.Key);
                    dt.Columns["tmp" + attr.Key].ColumnName = attr.Key;
                    continue;
                }
                if (attr.MyDataType == DataType.AppDateTime || attr.MyDataType == DataType.AppDate)
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = "tmp" + attr.Key;
                    col.DataType = typeof(DateTime);
                    dt.Columns.Add(col);
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            dr["tmp" + attr.Key] = DateTime.Parse(dr[attr.Key].ToString());
                        }
                        catch
                        {
                            if (attr.DefaultVal.ToString() == "")
                                dr["tmp" + attr.Key] = DateTime.Now;
                            else
                                dr["tmp" + attr.Key] = DateTime.Parse(attr.DefaultVal.ToString());

                        }

                    }
                    dt.Columns.Remove(attr.Key);
                    dt.Columns["tmp" + attr.Key].ColumnName = attr.Key;
                    continue;
                }
            }
            return dt;
        }
        /// <summary>
        /// 查询出来他的明细与明细的明细。
        /// 并编辑他们之间的关系。
        /// </summary>
        /// <returns>编辑好的，实体明细，以及实体明细的明细。</returns>
        public DataSet RetrieveAllDtlToDataSet()
        {

            DataSet ds = new DataSet(this.ToString());

            /* 把主表加入ds */
            Entity en = this.GetNewEntity;
            QueryObject qo = new QueryObject(this);
            DataTable dt = qo.DoQueryToTable();
            dt.TableName = en.EnDesc; //设定主表的名称。
            ds.Tables.Add(DealBoolTypeInDataTable(en, dt));


            foreach (EnDtl ed in en.EnMap.DtlsAll)
            {
                /* 循环主表的明细，编辑好关系并把他们放入 DataSet 里面。*/
                Entities edens = ed.Ens;
                Entity eden = edens.GetNewEntity;
                DataTable edtable = edens.RetrieveAllToTable();
                edtable.TableName = eden.EnDesc;
                ds.Tables.Add(DealBoolTypeInDataTable(eden, edtable));

                DataRelation r1 = new DataRelation(ed.Desc,
                    ds.Tables[dt.TableName].Columns[en.PK],
                    ds.Tables[edtable.TableName].Columns[ed.RefKey]);
                ds.Relations.Add(r1);


                //	int i = 0 ;

                foreach (EnDtl ed1 in eden.EnMap.DtlsAll)
                {
                    /* 主表的明细的明细。*/
                    Entities edlens1 = ed1.Ens;
                    Entity edlen1 = edlens1.GetNewEntity;

                    DataTable edlensTable1 = edlens1.RetrieveAllToTable();
                    edlensTable1.TableName = edlen1.EnDesc;
                    //edlensTable1.TableName =ed1.Desc ;


                    ds.Tables.Add(DealBoolTypeInDataTable(edlen1, edlensTable1));

                    DataRelation r2 = new DataRelation(ed1.Desc,
                        ds.Tables[edtable.TableName].Columns[eden.PK],
                        ds.Tables[edlensTable1.TableName].Columns[ed1.RefKey]);
                    ds.Relations.Add(r2);
                }




                //				foreach(AttrOfOneVSM oneVsM in eden.EnMap.AttrsOfOneVSM)
                //				{
                //					/* 当前的 Entity 点对点的关系  */
                //					Entities ensOfoneVsM =oneVsM.EnsOfMM;
                //					Entity enOfoneVsM =ensOfoneVsM.GetNewEntity;
                //
                //					DataTable ensOfoneVsMTable =ensOfoneVsM.RetrieveAllToTable();				 
                //					ensOfoneVsMTable.TableName = enOfoneVsM.EnDesc ;
                //
                //					ds.Tables.Add( DealBoolTypeInDataTable( enOfoneVsM , ensOfoneVsMTable ) );
                //					 
                //					DataRelation r2 = new DataRelation( oneVsM.Desc ,
                //						ds.Tables[edtable.TableName ].Columns[ en.PK ],
                //						ds.Tables[ensOfoneVsMTable.TableName].Columns[ oneVsM.AttrOfOneInMM ]);
                //					ds.Relations.Add(r2);
                //				}

            }
            return ds;
        }
        /// <summary>
        /// DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="en"></param>
        /// <param name="dtl"></param>
        /// <returns></returns>	 
        private DataSet AddRefDtl(DataSet ds, Entity en, Entity enDtl, EnDtl dtl)
        {
            return null;
            //			/* 主表的明细的明细。*/
            //			Entities edlens1 =ed1.Ens ; 
            //			Entity edlen1 =edlens1.GetNewEntity ; 
            //
            //			DataTable edlensTable1 =edlens1.RetrieveAllToTable();					 
            //			edlensTable1.TableName = edlen1.EnDesc ;
            //
            //			ds.Tables.Add( DealBoolTypeInDataTable(edlen1 ,edlensTable1) );					 
            //
            //			DataRelation r2 = new DataRelation("编辑明细:"+edlen1.EnDesc,
            //				ds.Tables[edtable.TableName ].Columns[ eden.PK ],
            //				ds.Tables[edlensTable1.TableName].Columns[ ed1.RefKey ]);
            //			ds.Relations.Add(r2);
        }
        /// <summary>
        /// 查询全部的结果放到RetrieveAllToDataSet。
        /// 包含它们的关联的信息。
        /// </summary>
        /// <returns></returns>
        public DataSet RetrieveAllToDataSet()
        {
            #region 形成dataset
            Entity en = this.GetNewEntity;
            DataSet ds = new DataSet(this.ToString());
            QueryObject qo = new QueryObject(this);
            DataTable dt = qo.DoQueryToTable();
            dt.TableName = en.EnMap.PhysicsTable;
            ds.Tables.Add(dt);
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                {
                    Entities ens = attr.HisFKEns;
                    QueryObject qo1 = new QueryObject(ens);
                    DataTable dt1 = qo1.DoQueryToTable();
                    dt1.TableName = ens.GetNewEntity.EnMap.PhysicsTable;
                    ds.Tables.Add(dt1);

                    /// 加入关系
                    DataColumn parentCol;
                    DataColumn childCol;
                    parentCol = dt.Columns[attr.Key];
                    childCol = dt1.Columns[attr.UIRefKeyValue];
                    DataRelation relCustOrder = new DataRelation(attr.Key, parentCol, childCol);
                    ds.Relations.Add(relCustOrder);
                    continue;


                }
                else if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                {
                    DataTable dt1 = DBAccess.RunSQLReturnTable("select * from sys_enum WHERE enumkey=" + en.HisDBVarStr + "k", "k", attr.UIBindKey);
                    dt1.TableName = attr.UIBindKey;
                    ds.Tables.Add(dt1);


                    /// 加入关系
                    DataColumn parentCol;
                    DataColumn childCol;
                    parentCol = dt.Columns[attr.Key];
                    childCol = dt1.Columns["IntKey"];
                    DataRelation relCustOrder = new DataRelation(attr.Key, childCol, parentCol);
                    ds.Relations.Add(relCustOrder);

                }
            }
            #endregion

            return ds;
        }
        /// <summary>
        /// 把当前实体集合的数据库转换成Dataset。
        /// </summary>
        /// <returns></returns>
        public DataSet ToDataSet()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.ToDataTableField());
            return ds;
        }
        public DataTable ToDataTableField()
        {
            return ToDataTableField("dt");
        }
        /// <summary>
        /// 把当前实体集合的数据库转换成Table。
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ToDataTableField(string tableName)
        {
            DataTable dt = this.ToEmptyTableField();
            Entity en = this.GetNewEntity;

            dt.TableName = tableName;
            foreach (Entity myen in this)
            {
                DataRow dr = dt.NewRow();
                foreach (Attr attr in en.EnMap.Attrs)
                {
                    if (attr.MyDataType == DataType.AppBoolean)
                    {
                        if (myen.GetValIntByKey(attr.Key) == 1)
                            dr[attr.Key] = "1";
                        else
                            dr[attr.Key] = "0";
                        continue;
                    }

                    /*如果是外键 就要去掉左右空格。
                     *  */
                    if (attr.MyFieldType == FieldType.FK
                        || attr.MyFieldType == FieldType.PKFK)
                        dr[attr.Key] = myen.GetValByKey(attr.Key).ToString().Trim();
                    else
                        dr[attr.Key] = myen.GetValByKey(attr.Key);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public DataTable ToDataTableDesc()
        {
            DataTable dt = this.ToEmptyTableDesc();
            Entity en = this.GetNewEntity;

            dt.TableName = en.EnMap.PhysicsTable;
            foreach (Entity myen in this)
            {
                DataRow dr = dt.NewRow();
                foreach (Attr attr in en.EnMap.Attrs)
                {
                    //if (attr.UIVisible == false)
                    //{
                    //    if (attr.Key.IndexOf("Text") == -1)
                    //        continue;
                    //}

                    if (attr.MyDataType == DataType.AppBoolean)
                    {
                        if (myen.GetValBooleanByKey(attr.Key))
                            dr[attr.Desc] = "是";
                        else
                            dr[attr.Desc] = "否";
                        continue;
                    }
                    dr[attr.Desc] = myen.GetValByKey(attr.Key);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public DataTable ToEmptyTableDescField()
        {
            DataTable dt = new DataTable();
            Entity en = this.GetNewEntity;
            try
            {

                foreach (Attr attr in en.EnMap.Attrs)
                {
                    //if (attr.UIVisible == false)
                    //    continue;

                    //if (attr.MyFieldType == FieldType.Enum && attr.MyDataType == DataType.AppInt )
                    //    continue;

                    switch (attr.MyDataType)
                    {
                        case DataType.AppString:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(string)));
                            break;
                        case DataType.AppInt:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(int)));
                            break;
                        case DataType.AppFloat:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(float)));
                            break;
                        case DataType.AppBoolean:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(string)));
                            break;
                        case DataType.AppDouble:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(double)));
                            break;
                        case DataType.AppMoney:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(double)));
                            break;
                        case DataType.AppDate:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(string)));
                            break;
                        case DataType.AppDateTime:
                            dt.Columns.Add(new DataColumn(attr.Desc.Trim() + attr.Key, typeof(string)));
                            break;
                        default:
                            throw new Exception("@bulider insert sql error: 没有这个数据类型");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(en.EnDesc + " error " + ex.Message);

            }
            return dt;
        }
        public DataTable ToDataTableDescField()
        {
            DataTable dt = this.ToEmptyTableDescField();
            Entity en = this.GetNewEntity;

            dt.TableName = en.EnMap.PhysicsTable;
            foreach (Entity myen in this)
            {
                DataRow dr = dt.NewRow();
                foreach (Attr attr in en.EnMap.Attrs)
                {
                    //if (attr.UIVisible == false)
                    //    continue;

                    //if (attr.MyFieldType == FieldType.Enum && attr.MyDataType == DataType.AppInt)
                    //    continue;

                    if (attr.MyDataType == DataType.AppBoolean)
                    {
                        if (myen.GetValBooleanByKey(attr.Key))
                            dr[attr.Desc.Trim() + attr.Key] = "是";
                        else
                            dr[attr.Desc.Trim() + attr.Key] = "否";
                        continue;
                    }
                    dr[attr.Desc.Trim()+attr.Key] = myen.GetValByKey(attr.Key);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 把系统的实体的PK转换为string
        /// 比如: "001,002,003,"。
        /// </summary>
        /// <param name="flag">分割符号, 一般来说用 ' ; '</param>
        /// <returns>转化后的string / 实体集合为空就 return null</returns>
        public string ToStringOfPK(string flag, bool isCutEndFlag)
        {
            string pk = null;
            foreach (Entity en in this)
            {
                pk += en.PKVal + flag;
            }
            if (isCutEndFlag)
                pk = pk.Substring(0, pk.Length - 1);

            return pk;
        }
        /// <summary>
        /// 把系统的实体的PK转换为 string
        /// 比如: "001,002,003,"。
        /// </summary>		 
        /// <returns>转化后的string / 实体集合为空就 return null</returns>
        public string ToStringOfSQLModelByPK()
        {
            if (this.Count == 0)
                return "''";
            return ToStringOfSQLModelByKey(this[0].PK);
        }
        /// <summary>
        /// 把系统的实体的PK转换为 string
        /// 比如: "001,002,003,"。
        /// </summary>		 
        /// <returns>转化后的string / 实体集合为空就 return "''"</returns>
        public string ToStringOfSQLModelByKey(string key)
        {
            if (this.Count == 0)
                return "''";

            string pk = null;
            foreach (Entity en in this)
            {
                pk += "'" + en.GetValStringByKey(key) + "',";
            }

            pk = pk.Substring(0, pk.Length - 1);

            return pk;
        }

        /// <summary>
        /// 空的Table
        /// 取到一个空表结构。
        /// </summary>
        /// <returns></returns>
        public DataTable ToEmptyTableField()
        {
            DataTable dt = new DataTable();
            Entity en = this.GetNewEntity;
            dt.TableName = en.EnMap.PhysicsTable;

            foreach (Attr attr in en.EnMap.Attrs)
            {
                switch (attr.MyDataType)
                {
                    case DataType.AppString:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(string)));
                        break;
                    case DataType.AppInt:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(int)));
                        break;
                    case DataType.AppFloat:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(float)));
                        break;
                    case DataType.AppBoolean:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(string)));
                        break;
                    case DataType.AppDouble:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(double)));
                        break;
                    case DataType.AppMoney:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(double)));
                        break;
                    case DataType.AppDate:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(string)));
                        break;
                    case DataType.AppDateTime:
                        dt.Columns.Add(new DataColumn(attr.Key, typeof(string)));
                        break;
                    default:
                        throw new Exception("@bulider insert sql error: 没有这个数据类型");
                }
            }
            return dt;
        }
        public DataTable ToEmptyTableDesc()
        {
            DataTable dt = new DataTable();
            Entity en = this.GetNewEntity;
            try
            {

                foreach (Attr attr in en.EnMap.Attrs)
                {
                    switch (attr.MyDataType)
                    {
                        case DataType.AppString:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(string)));
                            break;
                        case DataType.AppInt:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(int)));
                            break;
                        case DataType.AppFloat:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(float)));
                            break;
                        case DataType.AppBoolean:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(string)));
                            break;
                        case DataType.AppDouble:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(double)));
                            break;
                        case DataType.AppMoney:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(double)));
                            break;
                        case DataType.AppDate:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(string)));
                            break;
                        case DataType.AppDateTime:
                            dt.Columns.Add(new DataColumn(attr.Desc, typeof(string)));
                            break;
                        default:
                            throw new Exception("@bulider insert sql error: 没有这个数据类型");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(en.EnDesc + " error " + ex.Message);

            }
            return dt;
        }
     
        #endregion


        #region 内存查询方法

        #endregion

        #region 分组方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public int Group(Attrs attrs)
        {
            QueryObject qo = new QueryObject(this);
            //qo.gr
            return 0;



        }
        #endregion

        #region 查询from cash
        /// <summary>
        /// 缓存查询: 根据 in sql 方式进行。
        /// </summary>
        /// <param name="cashKey">指定的缓存Key，全局变量不要重复。</param>
        /// <param name="inSQL">sql 语句</param>
        /// <returns>返回放在缓存里面的结果集合</returns>
        public int RetrieveFromCashInSQL(string cashKey, string inSQL)
        {
            this.Clear();
            Entities ens = Cash.GetEnsDataExt(cashKey) as Entities;
            if (ens == null)
            {
                QueryObject qo = new QueryObject(this);
                qo.AddWhereInSQL(this.GetNewEntity.PK, inSQL);
                qo.DoQuery();
                Cash.SetEnsDataExt(cashKey, this);
            }
            else
            {
                this.AddEntities(ens);
            }
            return this.Count;
        }
        /// <summary>
        /// 缓存查询: 根据相关的条件
        /// </summary>
        /// <param name="attrKey">属性: 比如 FK_Sort</param>
        /// <param name="val">值: 比如:01 </param>
        /// <param name="top">最大的取值信息</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isDesc"></param>
        /// <returns>返回放在缓存里面的结果集合</returns>
        public int RetrieveFromCash(string attrKey, object val, int top, string orderBy, bool isDesc)
        {
            string cashKey = this.ToString() + attrKey + val + top + orderBy + isDesc;
            this.Clear();
            Entities ens = Cash.GetEnsDataExt(cashKey);
            if (ens == null)
            {
                QueryObject qo = new QueryObject(this);
                qo.Top = top;

                if (attrKey == "" || attrKey == null)
                {
                }
                else
                {
                    qo.AddWhere(attrKey, val);
                }

                if (orderBy != null)
                {
                    if (isDesc)
                        qo.addOrderByDesc(orderBy);
                    else
                        qo.addOrderBy(orderBy);
                }

                qo.DoQuery();
                Cash.SetEnsDataExt(cashKey, this);
            }
            else
            {
                this.AddEntities(ens);
            }
            return this.Count;
        }
        /// <summary>
        /// 缓存查询: 根据相关的条件
        /// </summary>
        /// <param name="attrKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int RetrieveFromCash(string attrKey, object val)
        {
            return RetrieveFromCash(attrKey, val, 99999, null, true);
        }
        /// <summary>
        /// 缓存查询: 根据相关的条件
        /// </summary>
        /// <param name="attrKey"></param>
        /// <param name="val"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public int RetrieveFromCash(string attrKey, object val,string orderby)
        {
            return RetrieveFromCash(attrKey, val, 99999, orderby, true);
        }
        /// <summary>
        /// 缓存查询: 根据相关的条件
        /// </summary>
        /// <param name="top"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public int RetrieveFromCash(string orderBy, bool isDesc,int top)
        {
            return RetrieveFromCash(null, null, top, orderBy, isDesc);
        }
        #endregion

    
    }

}
