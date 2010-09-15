using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	 	/// <summary>
    /// 信息发布
	/// </summary>
    public class InfoList1Attr : InfoListBaseAttr
    {
    }
	/// <summary>
    /// 信息发布
	/// </summary>
    public class InfoList1 : InfoListBase
    {
        /// <summary>
        /// 获取Table
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_InfoList1";
            }
        }
        /// <summary>
        /// 上一篇新闻
        /// </summary>
        public InfoList1 PreviousInfo
        {
            get
            {
                InfoList1 en = new InfoList1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList1.No < '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList1Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoList1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }
        /// <summary>
        /// 下一篇新闻
        /// </summary>
        public InfoList1 NextInfo
        {
            get
            {
                InfoList1 en = new InfoList1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList1.No > '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList1Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoList1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }


        #region 构造方法
        /// <summary>
        /// 信息发布
        /// </summary>
        public InfoList1(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public InfoList1()
        {
        }
        #endregion
    }
	/// <summary>
    /// 信息发布s
	/// </summary>
    public class InfoList1s : InfoListBases
    {
        /// <summary>
        /// 信息发布s
        /// </summary>
        public InfoList1s()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoList1();
            }
        }
    }
}
