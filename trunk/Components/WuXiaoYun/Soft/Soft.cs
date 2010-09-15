using System;
using System.Collections.Generic;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;

namespace BP.GE
{
    /// <summary>
    /// 软件
    /// </summary>
    public class SoftAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 软件描述
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        /// 软件标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 更新日期
        /// </summary>
        public const string RDT = "RDT";

        //public const string FileName = "FileName";
        /// <summary>
        /// 软件类型
        /// </summary>
        public const string FK_Sort = "FK_Sort";
        ///// <summary>
        ///// 软件大小
        ///// </summary>
        //public const string FSize = "FSize";
        /// <summary>
        /// 下载次数
        /// </summary>
        public const string DownTimes = "DownTimes";
        /// <summary>
        /// 软件授权
        /// </summary>
        public const string SoftRole = "SoftRole";
        /// <summary>
        /// 软件语言
        /// </summary>
        public const string SoftLanguage = "SoftLanguage";
        /// <summary>
        /// 推荐指数
        /// </summary>
        public const string RecomIdx = "RecomIdx";
        /// <summary>
        ///ICON路径
        /// </summary>
        public const string WebPath = "WebPath";
    }
    /// <summary>
    ///软件
    /// </summary>
    public class Soft : EntityNoName
    {
        #region

        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// 应用平台
        /// </summary>
        SysEnums AppPla = new SysEnums("AppPla", "@0=Win2000@1=Win2003@2=WinXP@3=Win9X@4=Win7@5=Vista");
        public string AppPlaT
        {
            get
            {
                string appPla = "";
                foreach (SysEnum sys in AppPla)
                {
                    if (this.GetValBooleanByKey("Is" + sys.Lab))
                    {
                        appPla += sys.Lab + "、";
                    }
                }

                return appPla.Length > 0 ? appPla.Remove(appPla.Length - 1) : appPla;
            }
        }

        /// <summary>
        /// 下载次数
        /// </summary>
        public string DownTimes
        {
            get
            {
                return this.GetValStringByKey(SoftAttr.DownTimes);
            }
            set
            {
                this.SetValByKey(SoftAttr.DownTimes, value);
            }
        }
        /// <summary>
        /// ICON路径
        /// </summary>
        public string WebPath
        {
            get
            {
                return this.GetValStringByKey(SoftAttr.WebPath);
            }
            set
            {
                this.SetValByKey(SoftAttr.WebPath, value);
            }
        }

        /// <summary>
        /// 软件类型
        /// </summary>
        public string SoftRole
        {

            get
            {
                return this.GetValStringByKey(SoftAttr.SoftRole);
            }
            set
            {
                this.SetValByKey(SoftAttr.SoftRole, value);
            }
        }
        public string SoftRoleT
        {
            get
            {
                return this.GetValRefTextByKey(SoftAttr.SoftRole);
            }
        }

        /// <summary>
        /// 软件语言
        /// </summary>
        public string SoftLanguage
        {
            get
            {
                return this.GetValStringByKey(SoftAttr.SoftLanguage);
            }
            set
            {
                this.SetValByKey(SoftAttr.SoftLanguage, value);
            }
        }
        public string SoftLanguageT
        {
            get
            {
                return this.GetValRefTextByKey(SoftAttr.SoftLanguage);
            }
        }
        /// <summary>
        /// 推荐指数
        /// </summary>
        public string RecomIdx
        {
            get
            {
                return this.GetValStringByKey(SoftAttr.RecomIdx);
            }
            set
            {
                this.SetValByKey(SoftAttr.RecomIdx, value);
            }
        }
        public string RecomIdxT
        {
            get
            {
                return this.GetValRefTextByKey(SoftAttr.RecomIdx);
            }
        }

        /// <summary>
        /// 软件类型
        /// </summary>
        public string FK_Sort
        {

            get
            {
                return this.GetValStringByKey(SoftAttr.FK_Sort);
            }
            set
            {
                this.SetValByKey(SoftAttr.FK_Sort, value);
            }


        }
        /// <summary>
        /// 软件类型
        /// </summary>
        public string FK_TypeT
        {
            get
            {
                return this.GetValRefTextByKey(SoftAttr.FK_Sort);
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValHtmlStringByKey(SoftAttr.Doc);
            }
            set
            {
                this.SetValByKey(SoftAttr.Doc, value);
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SoftAttr.Title);
            }
            set
            {
                this.SetValByKey(SoftAttr.Title, value);
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SoftAttr.RDT);
            }
            set
            {
                this.SetValByKey(SoftAttr.RDT, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        ///获取软件
        /// </summary>
        public Soft()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;

                Map map = new Map("GE_Soft");
                map.EnType = EnType.App;
                map.EnDesc = BP.Sys.EnsAppCfgs.GetValString("BP.GE.Softs", "AppName");
                map.DepositaryOfEntity = Depositary.None;
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.SoftSorts' >类别</a>";
                map.CodeStruct = "6";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(SoftAttr.No, null, "编号", true, true, 6, 6, 6);
                map.AddDDLEntities(SoftAttr.FK_Sort, null, "软件类别", new BP.GE.SoftSorts(), true);
                map.AddTBString(SoftAttr.Name, null, "软件名称", true, false, 1, 300, 30,true);

                map.AddDDLSysEnum(SoftAttr.SoftLanguage, 0, "软件语言", true, true, SoftAttr.SoftLanguage, "@0=简体中文@1=繁体中文@2=英文");
                map.AddDDLSysEnum(SoftAttr.SoftRole, 0, "软件授权", true, true, SoftAttr.SoftRole, "@0=免费软件@1=收费软件@2=共享软件");
                map.AddTBInt(SoftAttr.DownTimes, 0, "下载次数", false, false);
                map.AddTBDate(SoftAttr.RDT, "更新日期", true, false);
                map.AddDDLSysEnum(SoftAttr.RecomIdx, 0, "推荐指数", true, true, SoftAttr.RecomIdx, "@0=0颗星@1=1颗星@2=2颗星@3=3颗星@4=4颗星@5=5颗星");
                map.AddTBStringDoc(SoftAttr.Doc, null, "描述", true, false, true);

                //软件应用平台
                foreach (SysEnum sys in AppPla)
                {
                    map.AddBoolean("Is" + sys.Lab, false, sys.Lab, true, true, false);
                }

                map.AddSearchAttr(SoftAttr.FK_Sort);

                map.AddMyFile("软件图片");
                map.AddMyFile("软件", "Soft");

                //string sql = "select * from sys_encfg where no='BP.GE.Soft'";
                //if (BP.DA.DBAccess.RunSQLReturnCount(sql) == 0)
                //{
                //    sql = "insert into sys_encfg(No,GroupTitle) values('BP.GE.Soft','@IsWin2000=软件应用平台@No=基本信息')";
                //    BP.DA.DBAccess.RunSQL(sql);
                //}

                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeUpdateInsertAction()
        {
            SysFileManager file = new SysFileManager();
            int i = file.Retrieve(SysFileManagerAttr.RefVal, this.No,
                SysFileManagerAttr.EnName, this.ToString(), SysFileManagerAttr.AttrFileNo, "ICON");

            if (i != 0)
            {
                this.WebPath = file.WebPath;
            }

            return base.beforeUpdateInsertAction();
        }

        #endregion
    }
    /// <summary>
    /// 软件s
    /// </summary>
    public class Softs : EntitiesNoName
    {
        /// <summary>
        /// 软件s
        /// </summary>
        public Softs()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Soft();
            }
        }
        /// <summary>
        /// 软件推荐:top小于等于0时查询所有
        /// </summary>
        /// <returns></returns>
        public int RetrieveByRecom(int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.addOrderByDesc(SoftAttr.RecomIdx);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }

        /// <summary>
        /// 某一类别软件推荐:top小于等于0时查询所有
        /// </summary>
        /// <returns></returns>
        public int RetrieveRecomByType(string fk_type, int top)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(SoftAttr.FK_Sort, fk_type);
            qo.addOrderByDesc(SoftAttr.RecomIdx);
            if (top > 0)
            {
                qo.Top = top;
            }
            return qo.DoQuery();
        }
    }
}
