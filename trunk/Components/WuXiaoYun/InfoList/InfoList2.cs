using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	 	/// <summary>
    /// 信息发布
	/// </summary>
    public class InfoList2Attr : InfoListBaseAttr
    {
    }
	/// <summary>
    /// 信息发布
	/// </summary>
    public class InfoList2 : InfoListBase
    {
        /// <summary>
        /// 获取Table
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_InfoList2";
            }
        }
        /// <summary>
        /// 上一篇新闻
        /// </summary>
        public InfoList2 PreviousInfo
        {
            get
            {
                InfoList2 en = new InfoList2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList2.No < '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList2Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoList2Attr.No);
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
        public InfoList2 NextInfo
        {
            get
            {
                InfoList2 en = new InfoList2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList2.No > '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList2Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoList2Attr.No);
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
        public InfoList2(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public InfoList2()
        {
        }
        #endregion
    }
	/// <summary>
    /// 信息发布s
	/// </summary>
    public class InfoList2s : InfoListBases
    {
        /// <summary>
        /// 信息发布s
        /// </summary>
        public InfoList2s()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoList2();
            }
        }
    }
}
