using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BP.En.Base;
using BP.WF;
using BP.Port;
using BP.En;
using BP.DTS;
using BP.Tax;
using BP.DA;

namespace BP.WFV2.DTS
{
    public class Tmp : DataIOEn
    {
        public Tmp()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "临时调度,输出Emps错误的数据。";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = "";
            string sql2 = "";
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            foreach (Node nd in nds)
            {
                try
                {
                    sql = "SELECT COUNT(*) FROM " + nd.PTable + " Where emps like '200%' or emps Is null or emps='' ";
                    int i = DBAccess.RunSQLReturnValInt(sql);
                    if (i == 0)
                        continue;
                    Log.DefaultLogWriteLineInfo("@节点" + nd.FK_Flow + "" + nd.FlowName + "有错，sql= " + sql);
                }
                catch
                {

                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Tmp2 : DataIOEn
    {
        public Tmp2()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "手工注销纳税人(流程注销失败，需要手工注销的调度,系统把执行结果写到日志文件中)";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }

        public override void Do()
        {
            string sql = "select fk_taxpayer, taxpayerName, recorder, fk_xj from nd_20100 where wfstate=1 and fk_taxpayer in (select no from ds_taxpayer) order by recorder ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            Log.DefaultLogWriteLineInfo("有["+dt.Rows.Count+"]户异常。");


            foreach (DataRow dr in dt.Rows)
            {
                string fk_taxpayer = dr["fk_taxpayer"].ToString();
                string taxpayerName = dr["taxpayerName"].ToString();
                string recorder = dr["recorder"].ToString();

                try
                {
                    Log.DefaultLogWriteLineInfo("开始执行注销:" + fk_taxpayer + "," + taxpayerName);

                    Paras ps = new Paras();
                    ps.Add("UserNo", recorder); //如果需要此用户编号，就加上此属性
                    ps.Add("DoWhat", "ZX");       //添加DoWhat参数
                    ps.Add("FK_Taxpayer", fk_taxpayer); //添加DoWhat参数.
                    DBAccess.RunSP("DSBM.DealBuess", ps);
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineInfo("注销:" + fk_taxpayer + "," + taxpayerName + "失败" + ex.Message);
                }
            }
        }
    }
}
