using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
    public class DKDirAttr : EntityOIDNoNameAttr
    {
        /// <summary>
        /// 级别编号
        /// </summary>
        public const string GradeNo = "GradeNo";
        /// <summary>
        /// 操作员
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 是否共享
        /// </summary>
        public const string IsShare = "IsShare";
    }

    /// <summary>
    /// 用户目录
    /// </summary>
    public class DKDir : EntityOIDName
    {
        #region  属性
        /// <summary>
        /// 用户
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DKDirAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(DKDirAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string GradeNo
        {
            get
            {
                return this.GetValStringByKey(DKDirAttr.GradeNo);
            }
            set
            {
                this.SetValByKey(DKDirAttr.GradeNo, value);
            }
        }

        // 获取本节点所有孩子
        public DKDirs HisChilds
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNo + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addAnd();
                qo.AddWhere(DKDirAttr.GradeNo, "<>", this.GradeNo);
                qo.addOrderBy(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }

        // 获取父节点编号 
        public string GradeNoOfParent
        {
            get
            {
                if (this.GradeNo.Length == 2)
                    return "";
                return this.GradeNo.Substring(0, this.GradeNo.Length - 2);
            }
        }

        // 获取父节点的所有孩子节点
        public DKDirs HisParentChilds
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNoOfParent + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addOrderBy(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }

        // 获取所有的兄弟节点
        public DKDirs HisBrotherNodes
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNoOfParent + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addAnd();
                qo.AddWhereLen(DKDirAttr.GradeNo, "=", this.GradeNo.Length, DBType.SQL2000);
                qo.addOrderBy(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }

        // 获取所有的兄弟节点(降序)
        public DKDirs HisBrotherNodesDescOrder
        {
            get
            {
                DKDirs ens = new DKDirs();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", this.GradeNoOfParent + "%");
                qo.addAnd();
                qo.AddWhere(DKDirAttr.FK_Emp, this.FK_Emp);
                qo.addAnd();
                qo.AddWhereLen(DKDirAttr.GradeNo, "=", this.GradeNo.Length, DBType.SQL2000);
                qo.addOrderByDesc(DKDirAttr.GradeNo);
                qo.DoQuery();
                return ens;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 用户目录
        /// </summary>
        public DKDir()
        {
        }
        /// <summary>
        /// 用户目录
        /// </summary>
        /// <param name="no"></param>
        public DKDir(int no)
        {
            this.OID = no;
            this.Retrieve();
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_DKDir");
                map.EnDesc = "网络磁盘目录";
                //  map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Infos' >信息发布</a>";
                map.IsAutoGenerNo = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBString(DKDirAttr.GradeNo, null, "编号", true, true, 100, 100, 100);
                map.AddTBString(DKDirAttr.Name, null, "名称", true, false, 0, 50, 300);
                map.AddTBString(DKDirAttr.FK_Emp, null, "用户", false, false, 0, 50, 300);
                map.AddTBInt(DKDirAttr.IsShare, 0, "共享否?", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeInsert()
        {
            //this.FK_Emp = Web.WebUser.No;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// 用户目录 
    /// </summary>
    public class DKDirs : EntitiesOID
    {
        /// <summary>
        /// 获取用户目录
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DKDir();
            }
        }

        #region 构造函数
        /// <summary>
        /// 用户目录
        /// </summary>
        public DKDirs()
        {
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DKDirAttr.FK_Emp, Web.WebUser.No);
            int i = qo.DoQuery();
            if (i == 0)
            {
                DKDir en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "01";
                en.Name = "我的文档";
                en.Insert();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "02";
                en.Name = "我的课件";
                en.InsertAsNew();


                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "03";
                en.Name = "我的素材";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0301";
                en.Name = "图片";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0302";
                en.Name = "视频";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0303";
                en.Name = "音频";
                en.InsertAsNew();

                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "0304";
                en.Name = "其它";
                en.InsertAsNew();


                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "04";
                en.Name = "我的照片";
                en.InsertAsNew();


                en = new DKDir();
                en.FK_Emp = Web.WebUser.No;
                en.GradeNo = "05";
                en.Name = "我的视频";
                en.InsertAsNew();
                return qo.DoQuery();
            }
            return i;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获得第一级节点
        /// </summary>
        /// <param name="fk_emp">用户名</param>
        /// <returns></returns>
        public int ReGrade1(string fk_emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereLen(DKDirAttr.GradeNo, "=", 2, DBType.SQL2000);
            qo.addAnd();
            qo.AddWhere(DKDirAttr.FK_Emp, fk_emp);

            qo.addOrderBy(DKDirAttr.GradeNo);
            return qo.DoQuery();
        }

        /// <summary>
        /// 获得下一级节点（不包括本节点）
        /// </summary>
        /// <param name="fk_emp">用户名</param>
        /// <param name="pNo">GradeNo</param>
        /// <returns></returns>
        public int ReNextChild(string fk_emp, string pNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", pNo + "%");
            qo.addAnd();
            qo.AddWhereLen(DKDirAttr.GradeNo, "=", pNo.Length + 2,
                DBType.SQL2000);
            qo.addAnd();
            qo.AddWhere(DKDirAttr.FK_Emp, fk_emp);
            qo.addOrderBy(DKDirAttr.GradeNo);
            return qo.DoQuery();
        }

        /// <summary>
        /// 获取它的所有子节点（包括本节点）。
        /// </summary>
        /// <param name="fk_emp">用户名</param>
        /// <param name="pNo">GradeNo</param>
        /// <returns></returns>
        public int ReHisChilds(string fk_emp, string pNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DKDirAttr.GradeNo, " LIKE ", pNo + "%");
            qo.addAnd();
            qo.AddWhere(DKDirAttr.FK_Emp, fk_emp);
            qo.addOrderBy(DKDirAttr.GradeNo);
            return qo.DoQuery();
        }
        #endregion
    }
}
 

