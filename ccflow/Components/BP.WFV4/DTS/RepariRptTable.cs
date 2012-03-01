using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
namespace BP.WF
{
    /// <summary>
    /// 修复数据库 的摘要说明
    /// </summary>
    public class RepariRptTable : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public RepariRptTable()
        {
            this.Title = "修复Rpt数据表";
            this.Help = "在老版本的程序中NDxxxRpt表有丢失字段的情况，现在自动的修复它，以保证可以查询到数据。"; 
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
            Flows fls = new Flows();
            fls.RetrieveAll();
            foreach (Flow fl in fls)
            {
                string flowID = int.Parse(fl.No).ToString();



            }
            return null;
        }
    }
}
