using System;
using System.Collections;
using BP.DA;
using BP.En;
using System.IO;

namespace BP.GE
{
    public class FDBDirAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 主键
        /// </summary>
        public const string MyPK = "MyPK";
        public const string Grade = "Grade";
        public const string IsDtl = "IsDtl";
        public const string ParentDir = "ParentDir";
        public const string FileNum = "FileNum";

        //文件夹ICON
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string WebPath = "WebPath";

    }
    /// <summary>
    /// 资料类型
    /// </summary>
    public class FDBDir : EntityNoName
    {
        #region 属性
        public string MyPK
        {
            get
            {
                return this.GetValStrByKey(FDBDirAttr.MyPK);
            }
            set
            {
                this.SetValByKey(FDBDirAttr.MyPK, value);
            }
        }

        //文件夹ICON
        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey(FDBDirAttr.MyFileExt);
            }
            set
            {
                this.SetValByKey(FDBDirAttr.MyFileExt, value);
            }
        }
        public string WebPath
        {
            get
            {
                return this.GetValStringByKey(FDBDirAttr.WebPath);
            }
            set
            {
                this.SetValByKey(FDBDirAttr.WebPath, value);
            }
        }
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey(FDBDirAttr.MyFileW);
            }
            set
            {
                this.SetValByKey(FDBDirAttr.MyFileW, value);
            }
        }
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey(FDBDirAttr.MyFileH);
            }
            set
            {
                this.SetValByKey(FDBDirAttr.MyFileExt, value);
            }
        }

        public string NameExt_del
        {
            get
            {
                string name = this.GetValStringByKey(EntityNoNameAttr.Name);
                if (name.Length > 10)
                    name = name.Substring(0, 7) + "...";

                string strs = "<img src='/Images/dir.gif' border=0 />" + name;
                //strs="".PadLeft(this.Grade-1,'@')+strs;
                //strs=strs.Replace("@","&nbsp;&nbsp;");
                return strs;
            }
            set
            {
                this.SetValByKey(EntityNoNameAttr.Name, value);
            }
        }
        /// <summary>
        /// 物理目录
        /// </summary>
        public string PhyPath
        {
            get
            {
                //return null;
                //return Global.PathFDBSource + this.ParentDir + "\\" + this.No.Substring(this.No.Length - 2) + this.Name;
                return FDBDTS.PathFDBSource + this.ParentDir + "\\" + this.No.Substring(this.No.Length - 2) + this.Name;
            }
        }
        public string ParentDir
        {
            get
            {
                return this.GetValStringByKey(FDBDirAttr.ParentDir);
            }
            set
            {
               // string val = value.Replace(FDBDTS.PathFDBSource, "\\");
                //string val = value.Replace(Global.PathFDBSource, "\\");
                this.SetValByKey(FDBDirAttr.ParentDir, value);
            }
        }
        public string NameFull
        {
            get
            {
                return this.GetValStringByKey("NameFull");
            }
            set
            {
                this.SetValByKey("NameFull", value);
            }
        }

        public string ParentDirHtmls
        {
            get
            {
                return this.GetValStringByKey(FDBDirAttr.ParentDir);
            }
        }
        public int FileNum
        {
            get
            {
                return GetValIntByKey(FDBDirAttr.FileNum);
            }
            set
            {
                SetValByKey(FDBDirAttr.FileNum, value);
            }
        }
        /// <summary>
        /// 级次
        /// </summary>
        public int Grade
        {
            get
            {
                return GetValIntByKey(FDBDirAttr.Grade);
            }
            set
            {
                SetValByKey(FDBDirAttr.Grade, value);
            }
        }
        /// <summary>
        /// 是否明细
        /// </summary>
        public bool IsDtl
        {
            get
            {
                return GetValBooleanByKey(FDBDirAttr.IsDtl);
            }
            set
            {
                SetValByKey(FDBDirAttr.IsDtl, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 资料类型
        /// </summary>
        public FDBDir() { }
        /// <summary>
        /// 资料类型
        /// </summary>
        /// <param name="_No">编号</param>
        public FDBDir(string _No) : base(_No) { }
        #endregion

        #region 实现基本的方法
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsInsert = false;
                uac.IsUpdate = true;
                uac.IsDelete = true;
                //uac.OpenForSysAdmin();
                return uac;
            }
        }

        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GE_FDBDir");
                map.EnType = EnType.Sys;
                map.EnDesc = "资源类型";
                map.IsAutoGenerNo = false;
                map.DepositaryOfEntity = Depositary.None;
                
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.FDBs' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.FDBs", "AppName")
                            + "</a> - <a href=\"javascript:openwin('./../GE/FDB/OpenFtp.aspx?DoType=UploadFolder','FTP目录',1000,700)\" >导入目录</a>";

                map.AddTBStringPK(FDBDirAttr.MyPK, null, "MyPK", false, true, 0, 200, 200);
                map.AddTBString(FDBDirAttr.No, null, "编号", true, true, 1, 20, 20);
                map.AddTBString(FDBDirAttr.Name, null, "名称", true, true, 1, 50, 200);
                map.AddBoolean(FDBDirAttr.IsDtl, false, "明细", true, true);
                map.AddTBInt(FDBDirAttr.Grade, 1, "级次", true, true);
                map.AddTBString(FDBDirAttr.ParentDir, null, "ParentDir", true, true, 0, 200, 200);
                map.AddTBString("NameFull", null, "NameFull", false, true, 0, 200, 200);
                map.AddTBInt(FDBDirAttr.FileNum, 0, "FileNum", true, true);
                map.AddTBStringDoc("Doc", null, "说明", true, false);
                //封面图片
                map.AddMyFile();

                RefMethod rm = new RefMethod();
                rm.Title = "文件上传";
                rm.ClassMethodName = this.ToString() + ".DoUpload";
                map.AddRefMethod(rm);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public string DoUpload()
        {
            PubClass.WinOpen("./../GE/FDB/OpenFtp.aspx?DoType=UploadFile&FK_Dir=" + this.No, "FTP目录", 1000, 700);
            return null;
        }

        #region init data.
        //public static string PathFDBSource = "";
        ///// <summary>
        ///// 入口
        ///// </summary>
        //public static void intIt(string basePath)
        //{
        //    string[] dirs = System.IO.Directory.GetDirectories(basePath); // 获取文件夹。
        //    DBAccess.RunSQL("DELETE GE_FDBDir");
        //    DBAccess.RunSQL("DELETE GE_FDB");
        //    foreach (string dir in dirs)
        //    {
        //        string dirShortName = dir.Substring(dir.LastIndexOf("\\") + 1);
        //        string no = dirShortName.Substring(0, 2);

        //        FDBDir d = new FDBDir();
        //        d.No = no;
        //        d.Name = dirShortName.Substring(3);
        //        d.Grade = 1;
        //        d.FileNum = System.IO.Directory.GetFiles(dir).Length; /*本目录下的文件个数*/
        //        d.ParentDir = "\\";
        //        d.NameFull = dir;
        //        d.Insert();
        //        FDBDir.intItFiles(d, dir);
        //        FDBDir.intIt(dir, no);
        //    }
        //}
        ///// <summary>
        ///// 递归调用
        ///// </summary>
        ///// <param name="baseDir"></param>
        ///// <param name="parentNo"></param>
        //public static void intIt(string baseDir, string parentNo)
        //{
        //    try
        //    {
        //        string[] strs = System.IO.Directory.GetDirectories(baseDir);
        //        foreach (string str in strs)
        //        {
        //            string dirShortName = str.Substring(str.LastIndexOf("\\") + 1);
        //            string no = dirShortName.Substring(0, 2);
        //            FDBDir dir = new FDBDir();
        //            dir.No = parentNo + no;
        //            dir.Name = dirShortName.Substring(3);
        //            dir.Grade = dir.No.Length / 2;
        //            dir.FileNum = System.IO.Directory.GetFiles(str).Length; /*本目录下的文件个数*/
        //            dir.ParentDir = baseDir;
        //            dir.NameFull = str;
        //            dir.Insert();

        //            FDBDir.intItFiles(dir, str);
        //            FDBDir.intIt(str, dir.No);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "  baseDir=" + baseDir);
        //    }
        //}
        ///// <summary>
        ///// 文件信息
        ///// </summary>
        ///// <param name="dir"></param>
        ///// <param name="path"></param>
        //public static void intItFiles(FDBDir dir, string path)
        //{
        //    string[] strs = System.IO.Directory.GetFiles(path);
        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        string file = strs[i];
        //        string shortName = file.Substring(file.LastIndexOf("\\") + 1);
        //        string ext = shortName.Substring(shortName.LastIndexOf(".") + 1); // 文件后缀。
        //        int paycent = 0;
        //        if (shortName.IndexOf("@") != -1)
        //        {
        //            string centStr = shortName.Substring(shortName.LastIndexOf("@") + 1); // 得到 xxx.doc 部分。
        //            centStr = centStr.Replace("." + ext, "");
        //            paycent = int.Parse(centStr);
        //        }

        //        FDB fdb = new FDB();
        //        fdb.MyPK = dir.No + "_" + i;
        //        fdb.NameFull = file;
        //        fdb.NameS = shortName;
        //        fdb.Ext = ext;
        //        fdb.FK_FDBDir = dir.No;
        //        FileInfo info = new FileInfo(fdb.NameFull);
        //        fdb.FSize = Convert.ToInt32(BP.DA.DataType.PraseToMB(info.Length));
        //        fdb.CDT = info.CreationTime.ToString(DataType.SysDataFormat);
        //        fdb.PayCent = paycent;
        //        fdb.Insert();
        //    }
        //}
        #endregion
    }
    /// <summary>
    /// 资料类型
    /// </summary>
    public class FDBDirs : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 资料类型s
        /// </summary>
        public FDBDirs() { }


        /// <summary>
        /// 资料类型
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FDBDir();
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获得一级目录节点
        /// </summary>
        /// <param name="fk_imgLink">外键:专辑资源No</param>
        /// <returns></returns>
        public int ReRootDir(string fk_imgLink)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FDBDirAttr.MyPK, "like", "'" + fk_imgLink + "@%'");
            qo.addAnd();
            qo.AddWhereLen(FDBDirAttr.No, "=", 2, DBType.SQL2000);
            qo.addOrderBy(FDBDirAttr.No);
            return qo.DoQuery();
        }
        /// <summary>
        /// 获得根目录文件
        /// </summary>
        /// <param name="fk_imgLink">外键:专辑资源No</param>
        /// <returns></returns>
        public FDBs ReRootFiles(string fk_imgLink)
        {
            FDBs ens = new FDBs();
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(FDBAttr.MyPK, "like", "'" + fk_imgLink + "@%'");
            qo.addAnd();
            qo.AddWhere(FDBAttr.No, "like","00_%");
            qo.addOrderBy(FDBAttr.No);
            qo.DoQuery();
            return ens;
        }

        /// <summary>
        /// 获取它的子节点。
        /// </summary>
        /// <param name="fk_emp"></param>
        /// <param name="pNo"></param>
        /// <returns></returns>
        public int ReHisChilds(string pNo,string fk_imgLink)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FDBDirAttr.MyPK, "like", "'" + fk_imgLink + "@%'");
            qo.addAnd();
            qo.AddWhere(FDBDirAttr.No, " LIKE ", "'" + pNo + "%'");
            qo.addAnd();
            qo.AddWhereLen(FDBDirAttr.No, "=", pNo.Length + 2,
                DBType.SQL2000);
            qo.addOrderBy(FDBDirAttr.No);
            return qo.DoQuery();
        }
        #endregion
    }
}
