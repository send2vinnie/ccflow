using System;
using System.Data;
using BP.DA;
using BP.En;
using System.IO;

namespace BP.GE
{
	/// <summary>
	/// 文件柜
	/// </summary>
    public class FDBAttr
    {
        #region 基本属性
        /// <summary>
        /// 主键
        /// </summary>
        public const string MyPK = "MyPK";
        public const string No = "No";
        /// <summary>
        /// 文件柜标题
        /// </summary>
        public const string NameShort = "NameShort";
        /// <summary>
        /// 完整路径
        /// </summary>
        public const string NameFull = "NameFull";
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public const string Ext = "Ext";
        /// <summary>
        /// 大小
        /// </summary>
        public const string FSize = "FSize";
        /// <summary>
        /// 类别
        /// </summary>
        public const string FK_FDBDir = "FK_FDBDir";
        /// <summary>
        /// 记录日期
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// 阅读次
        /// </summary>
        public const string ReadTimes = "ReadTimes";
        public const string Doc = "Doc";
        public const string PayCent = "PayCent";
        public const string RDT = "RDT";
        #endregion
    }
	/// <summary>
	/// 文件柜
	/// </summary>
    public class FDB : EntityMyPK
    {
        #region 基本属性
        public string No
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.No);
            }
            set
            {
                this.SetValByKey(FDBAttr.No, value);
            }
        }
        public int FSize
        {
            get
            {
                return this.GetValIntByKey(FDBAttr.FSize);
            }
            set
            {
                this.SetValByKey(FDBAttr.FSize, value);
            }
        }
        public int PayCent
        {
            get
            {
                return this.GetValIntByKey(FDBAttr.PayCent);
            }
            set
            {

                this.SetValByKey(FDBAttr.PayCent, value);
            }
        }
        public string NameShort
        {
            get
            {
                return "<img src='/Edu/Images/FileType/" + this.Ext + ".gif' border=0 />" + this.GetValStringByKey(FDBAttr.NameShort);
            }
            set
            {
                this.SetValByKey(FDBAttr.NameShort, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.RDT);
            }
            set
            {
                this.SetValByKey(FDBAttr.RDT, value);
            }
        }
        public string NameS
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.NameShort);
            }
            set
            {
                this.SetValByKey(FDBAttr.NameShort, value);
            }
        }
        public string NameFull
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.NameFull);
            }
            set
            {
                this.SetValByKey(FDBAttr.NameFull, value);
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string FK_FDBDir
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.FK_FDBDir);
            }
            set
            {
                this.SetValByKey(FDBAttr.FK_FDBDir, value);
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string FK_FDBDirText
        {
            get
            {
                return this.GetValRefTextByKey(FDBAttr.FK_FDBDir);
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.CDT);
            }
            set
            {
                this.SetValByKey(FDBAttr.CDT, value);
            }
        }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.Doc);
            }
            set
            {
                this.SetValByKey(FDBAttr.Doc, value);
            }
        }
        public string Ext
        {
            get
            {
                return this.GetValStringByKey(FDBAttr.Ext);
            }
            set
            {
                this.SetValByKey(FDBAttr.Ext, value);
            }
        }
        #endregion

        #region 构造函数
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsInsert = false;
                uac.IsUpdate = true;
                uac.IsDelete = true;
                //uac.OpenForAppAdmin();
                return uac;
            }
        }
        /// <summary>
        /// 文件柜
        /// </summary>		
        public FDB()
        {
        }
        public FDB(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// FDBMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_FDB");

                #region 基本属性
                map.EnType = EnType.Sys;
                map.EnDesc = BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName");
                map.DepositaryOfEntity = Depositary.None;
                //map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.FDBDirs' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName") + "目录</a> - <a href=\"javascript:WinOpen('./Sys/EnsAppCfg.aspx?EnsName=BP.GE.FDBs')\">属性设置</a>";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.FDBDirs' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName")
                    + "目录</a> - <a href=\"javascript:WinOpen('./../GE/FDB/OpenFtp.aspx?DoType=OpenFtp','FTP目录',1000,700)\" >打开FTP</a>"
                +" - <a href=\"javascript:WinOpen('Method.aspx?M=BP.GE.FDBDTS');\" >调度数据</a>";

                #endregion

                #region 字段
                map.AddTBStringPK(FDBAttr.MyPK, null, "MyPK", false, true, 0, 200, 200);
                map.AddTBString(FDBAttr.No, null, "No", false, true, 0, 200, 200);
                map.AddDDLEntities(FDBAttr.FK_FDBDir, "10", "目录", new FDBDirs(), false);
                map.AddTBString(FDBAttr.NameShort, null, "短名称", true, true, 1, 50, 200);
                map.AddTBString(FDBAttr.NameFull, null, "长名称", true, true, 1, 300, 200);
                map.AddTBString(FDBAttr.Ext, null, "类型", true, false, 0, 50, 200);
                map.AddTBInt(FDBAttr.FSize, 0, "大小", true, true);
                map.AddTBDate(FDBAttr.CDT, null, "建立时间", true, true);
                map.AddTBInt(FDBAttr.PayCent, 0, "支付分", true, false);
                map.AddTBStringDoc("Doc", null, "说明", true, false);
                map.AddTBIntMyNum();
                #endregion

                map.AddSearchAttr(FDBAttr.FK_FDBDir);
                
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region 方法
        #endregion
    }
	/// <summary>
	/// 文件柜
	/// </summary>
    public class FDBs : EntitiesMyPK
    {
        #region 实体
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FDB();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 文件柜
        /// </summary>
        public FDBs()
        {

        }
        public FDBs(string fk_sort)
        {
            this.Retrieve(FDBAttr.FK_FDBDir, fk_sort);
        }
        #endregion
    }
}
