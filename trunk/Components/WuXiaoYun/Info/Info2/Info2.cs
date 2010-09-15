using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	/// <summary>
    /// 信息发布
	/// </summary>
    public class Info2Attr : InfoBaseAttr
    {
    }
	/// <summary>
    /// 信息发布
	/// </summary>
    public class Info2 : InfoBase
    {
        #region 属性
        /// <summary>
        /// 数据库主表
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_Info2";
            }
        }
        /// <summary>
        /// 数据库类别实体类
        /// </summary>
        public override string SortEntity
        {
            get
            {
                return "BP.GE.InfoSort2";
            }
        }
        /// <summary>
        /// 类别DDLEntitees
        /// </summary>
        public override EntitiesNoName SortDDLEntities
        {
            get
            {
                return new InfoSort2s();
            }
        }

        /// <summary>
        /// 上一篇新闻(某一类别中)
        /// </summary>
        public Info2 PreviousInfo
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info2Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info2Attr.No);
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
        public Info2 NextInfo
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(Info2Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info2Attr.No);
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
        public Info2 PreviousInfoOfAll
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(Info2Attr.No);
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
        public Info2 NextInfoOfAll
        {
            get
            {
                Info2 en = new Info2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(Info2Attr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(Info2Attr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(Info2Attr.No);
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
        public Info2(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public Info2()
        {
        }
        #endregion
    }
	/// <summary>
    /// 信息发布s
	/// </summary>
    public class Info2s : InfoBases
    {
        /// <summary>
        /// 信息发布s
        /// </summary>
        public Info2s()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Info2();
            }
        }
    }
}
