using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 信息发布
    /// </summary>
    public class InfoAttr : InfoBaseAttr
    {
    }
    /// <summary>
    /// 信息发布
    /// </summary>
    public class Info : InfoBase
    {
        #region 属性
        /// <summary>
        /// 数据库主表
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_Info";
            }
        }
        /// <summary>
        /// 数据库类别实体类
        /// </summary>
        public override string SortEntity
        {
            get
            {
                return "BP.GE.InfoSort";
            }
        }
        /// <summary>
        /// 类别DDLEntitees
        /// </summary>
        public override EntitiesNoName SortDDLEntities
        {
            get
            {
                return new InfoSorts();
            }
        }

        /// <summary>
        /// 上一篇新闻(某一类别中)
        /// </summary>
        public Info PreviousInfo
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(InfoAttr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoAttr.No);
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
        public Info NextInfo
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.FK_Sort, this.FK_Sort);
                qo.addAnd();
                qo.AddWhere(InfoAttr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoAttr.No);
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
        public Info PreviousInfoOfAll
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.No, ">", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoAttr.No);
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
        public Info NextInfoOfAll
        {
            get
            {
                Info en = new Info();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere(InfoAttr.No, "<", this.No);
                qo.addAnd();
                qo.AddWhereNotIn(InfoAttr.InfoSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoAttr.No);
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
        public Info(string no)
            : base(no)
        {

        }
        /// <summary>
        /// 信息发布
        /// </summary>
        public Info()
        {
        }
        #endregion
    }
    /// <summary>
    /// 信息发布s
    /// </summary>
    public class Infos : InfoBases
    {
        /// <summary>
        /// 信息发布s
        /// </summary>
        public Infos()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Info();
            }
        }
    }
}
