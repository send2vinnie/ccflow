using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
    public class DKFileAttr : EntityOIDNoNameAttr
    {
        /// <summary>
        /// 文件上传日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 文件目录
        /// </summary>
        public const string FK_DKDir = "FK_DKDir";
        /// <summary>
        /// 人员
        /// </summary>
        public const string FK_Emp = "FK_Emp";

    }
    /// <summary>
    /// 用户文件
    /// </summary>
    public class DKFile : EntityOIDName
    {
        #region  属性
        /// <summary>
        /// 用户
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DKFileAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(DKFileAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// 目录
        /// </summary>
        public string FK_DKDir
        {
            get
            {
                return this.GetValStringByKey(DKFileAttr.FK_DKDir);
            }
            set
            {
                this.SetValByKey(DKFileAttr.FK_DKDir, value);
            }
        }
        /// <summary>
        /// 上传日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(DKFileAttr.RDT);
            }
            set
            {
                this.SetValByKey(DKFileAttr.RDT, value);
            }
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string MyFileName
        {
            get
            {
                return this.GetValStringByKey("MyFileName");
            }
            set
            {
                this.SetValByKey("MyFileName", value);
            }
        }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey("MyFileExt");
            }
            set
            {
                this.SetValByKey("MyFileExt", value);
            }
        }
        /// <summary>
        /// 文件高度
        /// </summary>
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey("MyFileH");
            }
            set
            {
                this.SetValByKey("MyFileH", value);
            }
        }
        /// <summary>
        /// 文件宽度
        /// </summary>
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey("MyFileW");
            }
            set
            {
                this.SetValByKey("MyFileW", value);
            }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int  MyFileSize
        {
            get
            {
                return this.GetValIntByKey ("MyFileSize");
            }
            set
            {
                this.SetValByKey("MyFileSize", value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 用户文件
        /// </summary>
        public DKFile()
        {
        }
        /// <summary>
        /// 用户文件
        /// </summary>
        /// <param name="no"></param>
        public DKFile(int no)
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

                Map map = new Map("GE_DKFile");
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.DKFiles' >类别</a> - <a href=\"javascript:WinOpen('./Sys/EnsAppCfg.aspx?EnsName=BP.GE.DKFiles')\">属性设置</a>";

                map.EnDesc = "网络磁盘文件";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBInt(DKFileAttr.FK_DKDir, 0, "文件目录", true, false);
                map.AddTBString(DKFileAttr.FK_Emp, null, "人员", true, false, 0, 20, 20);
                map.AddTBDate(DKFileAttr.RDT, "文件上传日期", true, true);
                map.AddMyFile();
               

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 用户文件 
    /// </summary>
    public class DKFiles : EntitiesNoName
    {
        /// <summary>
        /// 获取用户文件
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DKFile();
            }
        }

        #region 构造函数
        /// <summary>
        /// 用户文件
        /// </summary>
        public DKFiles()
        {
        }


      
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DKFileAttr.FK_Emp, Web.WebUser.No);
            qo.DoQuery();
            return qo.DoQuery();
        }
        #endregion
    }
}
 

