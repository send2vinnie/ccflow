using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 信息发布
    /// </summary>
    public class Info1Attr : InfoBaseAttr
    {
    }
    /// <summary>
    /// 信息发布
    /// </summary>
    public class Info1 : InfoBase
    {
        #region 属性
        /// <summary>
        /// 数据库主表
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_Info1";
            }
        }
        /// <summary>
        /// 数据库类别实体类
        /// </summary>
        public override string SortEntity
        {
            get
            {
                return "BP.GE.InfoSort1";
            }
        }
        /// <summary>
        /// 类别DDLEntitees
        /// </summary>
        public override EntitiesNoName SortDDLEntities
        {
            get
            {
                return new InfoSort1s();
            }
        }

        /// <summary>
        /// 上一篇新闻(某一类别中)
        /// </summary>
        public Info1 PreviousInfo
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info1Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }
        /// <summary>
        /// 下一篇新闻(某一类别中)
        /// </summary>
        public Info1 NextInfo
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info1Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }

        /// <summary>
        /// 上一篇新闻(所有信息中)
        /// </summary>
        public Info1 PreviousInfoOfAll
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }
        /// <summary>
        /// 下一篇新闻(所有信息中)
        /// </summary>
        public Info1 NextInfoOfAll
        {
            get
            {
                Info1 en = new Info1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info1Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info1Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }

        #endregion 属性

        #region 构造方法
        /// <summary>
        /// 信息发布
        /// </summary>
        public Info1(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public Info1()
        {
        }
        #endregion
    }
    /// <summary>
    /// 信息发布s
    /// </summary>
    public class Info1s : InfoBases
    {
        /// <summary>
        /// 信息发布s
        /// </summary>
        public Info1s()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Info1();
            }
        }
    }
}
