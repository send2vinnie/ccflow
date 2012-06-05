using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.En;
using BP.DA;
using System.Data.SqlClient;
using Lizard.DBUtility;
using System.Data.OracleClient;
using BP.EIP;

namespace BP.CCOA
{
    public partial class EIP_LayoutDetail
    {
        #region Extend Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetJsonList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (BP.DA.DBAccess.AppCenterDBType == DBType.Oracle9i)
            {
                strSql.Append(" SELECT \"COLUMN\",id,title,showCollapseButton,height,url ");
                strSql.Append(" FROM V_LAYOUT order by \"COLUMN\",seqno");
            }
            else
            {
                strSql.Append("select [column],id,title,showCollapseButton,height,url ");
                strSql.Append(" FROM V_LAYOUT order by [column],seqno");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBAccess.RunSQLReturnDataSet(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EIP_LayoutDetail GetModel(string DetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 No,Layout_Id,ColumnNo,PanelId,PanelTitle,ShowCollapseButton,Width,Height,SeqNo,Url,IsEdit from EIP_LayoutDetail ");
            strSql.Append(" where No=@No ");
            SqlParameter[] parameters = {
					new SqlParameter("@No", SqlDbType.VarChar,50)			};
            parameters[0].Value = DetailId;

            EIP_LayoutDetail model = new EIP_LayoutDetail();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["No"] != null && ds.Tables[0].Rows[0]["No"].ToString() != "")
                {
                    model.No = ds.Tables[0].Rows[0]["No"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Layout_Id"] != null && ds.Tables[0].Rows[0]["Layout_Id"].ToString() != "")
                {
                    model.Layout_Id = ds.Tables[0].Rows[0]["Layout_Id"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ColumnNo"] != null && ds.Tables[0].Rows[0]["ColumnNo"].ToString() != "")
                {
                    model.ColumnNo = int.Parse(ds.Tables[0].Rows[0]["ColumnNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PanelId"] != null && ds.Tables[0].Rows[0]["PanelId"].ToString() != "")
                {
                    model.PanelId = ds.Tables[0].Rows[0]["PanelId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PanelTitle"] != null && ds.Tables[0].Rows[0]["PanelTitle"].ToString() != "")
                {
                    model.PanelTitle = ds.Tables[0].Rows[0]["PanelTitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShowCollapseButton"] != null && ds.Tables[0].Rows[0]["ShowCollapseButton"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ShowCollapseButton"].ToString() == "1") || (ds.Tables[0].Rows[0]["ShowCollapseButton"].ToString().ToLower() == "true"))
                    {
                        model.ShowCollapseButton = true;
                    }
                    else
                    {
                        model.ShowCollapseButton = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Width"] != null && ds.Tables[0].Rows[0]["Width"].ToString() != "")
                {
                    model.Width = int.Parse(ds.Tables[0].Rows[0]["Width"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Height"] != null && ds.Tables[0].Rows[0]["Height"].ToString() != "")
                {
                    model.Height = int.Parse(ds.Tables[0].Rows[0]["Height"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SeqNo"] != null && ds.Tables[0].Rows[0]["SeqNo"].ToString() != "")
                {
                    model.SeqNo = int.Parse(ds.Tables[0].Rows[0]["SeqNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Url"] != null && ds.Tables[0].Rows[0]["Url"].ToString() != "")
                {
                    model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsEdit"] != null && ds.Tables[0].Rows[0]["IsEdit"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsEdit"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsEdit"].ToString().ToLower() == "true"))
                    {
                        model.IsEdit = true;
                    }
                    else
                    {
                        model.IsEdit = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsShow"] != null && ds.Tables[0].Rows[0]["IsShow"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsShow"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsShow"].ToString().ToLower() == "true"))
                    {
                        model.IsEdit = true;
                    }
                    else
                    {
                        model.IsEdit = false;
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteList(string DetailIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from EIP_LayoutDetail ");
            strSql.Append(" where No in (" + DetailIdlist + ")  ");
            int rows = DBAccess.RunSQL(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select No,Layout_Id,ColumnNo,PanelId,PanelTitle,ShowCollapseButton,Width,Height,SeqNo,Url,IsEdit,IsShow ");
            strSql.Append(" FROM EIP_LayoutDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY ColumnNo");
            return DBAccess.RunSQLReturnDataSet(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(IDictionary<string, string> keyValue)
        {
            StringBuilder sbrSql = new StringBuilder();

            List<SqlParameter> sqlParas = new List<SqlParameter>();
            //List<OracleParameter> oraParas = new List<OracleParameter>();
            List<string> strList = new List<string>();

            int i = 0;
            foreach (var item in keyValue)
            {
                if (BP.DA.DBAccess.AppCenterDBType == DBType.Oracle9i)
                {
                    string strSql = "UPDATE EIP_LAYOUTDETAIL SET COLUMNNO='{1}',SEQNO='{2}' WHERE PANELID='{0}'";
                    strSql = string.Format(strSql, item.Key, item.Value.Split('|')[0], item.Value.Split('|')[1]);
                    strList.Add(strSql);
                }
                else
                {
                    sbrSql.Append("UPDATE EIP_LayoutDetail set ");
                    sbrSql.Append("ColumnNo=@ColumnNo" + i + ",SeqNo=@SeqNo" + i);
                    sbrSql.Append(" WHERE PanelId=@PanelId" + i + ";");

                    SqlParameter sp1 = new SqlParameter("@PanelId" + i, item.Key);
                    SqlParameter sp2 = new SqlParameter("@ColumnNo" + i, item.Value.Split('|')[0]);
                    SqlParameter sp3 = new SqlParameter("@SeqNo" + i, item.Value.Split('|')[1]);

                    sqlParas.Add(sp1);
                    sqlParas.Add(sp2);
                    sqlParas.Add(sp3);
                }

                i++;
            }
            int rows = 0;
            if (BP.DA.DBAccess.AppCenterDBType == DBType.Oracle9i)
            {
                rows = XDBHelper.RunSql(strList);
            }
            else
            {
                rows = DbHelperSQL.ExecuteSql(sbrSql.ToString(), sqlParas.ToArray());
            }
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
