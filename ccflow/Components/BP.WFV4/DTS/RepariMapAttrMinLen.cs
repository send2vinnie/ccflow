using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// 修复表单物理表字段长度 的摘要说明
    /// </summary>
    public class RepariMapAttrMinLen : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public RepariMapAttrMinLen()
        {
            this.Title = "修复表单物理表字段长度";
            this.Help = "比如：一个备注字段原来设计长度为500字符后，运行一段时间需修改成2000字符，运行此功能就会自动完成字段长度的修复。";
        }
        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
        }
        /// <summary>
        /// 当前的操纵员是否可以执行这个方法
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns>返回执行结果</returns>
        public override object Do()
        {
            string sql = "SELECT * FROM Sys_MapAttr WHERE MaxLen >200 AND MyDataType=1";
            string msg = "";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            int idx = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string fk_mapdata = dr["FK_MapData"].ToString();
                string field = dr["KeyOfEn"].ToString();
                string MaxLen = dr["MaxLen"].ToString();
                sql = "ALTER TABLE " + fk_mapdata + " ALTER COLUMN " + field + " varchar(" + MaxLen + ")";
                try
                {
                    DBAccess.RunSQL(sql);
                    idx++;
                }
                catch (Exception ex)
                {
                    msg += "@错误:表:" + fk_mapdata + " , 字段:" + field + ",长度:" + MaxLen + ".<font color=red>" + ex.Message+"</font> @SQL="+sql;
                }
            }
            return "执行结果:成功执行了" + idx + "条记录。<hr><font color=red>" + msg + "</font>";
        }
    }
}
