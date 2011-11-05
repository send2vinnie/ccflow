using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// 附件数据存储 - 属性
    /// </summary>
    public class FrmAttachmentDBAttr : EntityMyPKAttr
    {
        /// <summary>
        /// 附件
        /// </summary>
        public const string FK_FrmAttachment = "FK_FrmAttachment";
        /// <summary>
        /// 主表
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// RefPKVal
        /// </summary>
        public const string RefPKVal = "RefPKVal";
        /// <summary>
        /// 文件名称
        /// </summary>
        public const string FileName = "FileName";
        /// <summary>
        /// 文件扩展
        /// </summary>
        public const string FileExts = "FileExts";
        /// <summary>
        /// 文件大小
        /// </summary>
        public const string FileSize = "FileSize";
        /// <summary>
        /// 保存到
        /// </summary>
        public const string SaveTo = "SaveTo";
    }
    /// <summary>
    /// 附件数据存储
    /// </summary>
    public class FrmAttachmentDB : EntityMyPK
    {
        #region 属性
        public string SaveTo
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.SaveTo);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.SaveTo, value);
            }
        }
        
        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FileName);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileName, value);
            }
        }
        /// <summary>
        /// FileExts
        /// </summary>
        public string FileExts
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FileExts);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileExts, value);
            }
        }
        /// <summary>
        /// 相关附件
        /// </summary>
        public string FK_FrmAttachment
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FK_FrmAttachment);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FK_FrmAttachment, value);
            }
        }
        public string RefPKVal
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.RefPKVal);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.RefPKVal, value);
            }
        }
        
        /// <summary>
        /// 附件编号
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentDBAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public float FileSize
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentDBAttr.FileSize);
            }
            set
            {
                this.SetValByKey(FrmAttachmentDBAttr.FileSize, value);
            }
        }
        public string FileFull
        {
            get
            {
                return this.SaveTo + "\\" + this.FileName;
                //return this.GetValStringByKey(FrmAttachmentDBAttr.FK_FrmAttachment);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 附件数据存储
        /// </summary>
        public FrmAttachmentDB()
        {
        }
        /// <summary>
        /// 附件数据存储
        /// </summary>
        /// <param name="mypk"></param>
        public FrmAttachmentDB(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_FrmAttachmentDB");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "附件数据存储";
                map.EnType = EnType.Sys;
                map.AddMyPK();

                map.AddTBString(FrmAttachmentDBAttr.FK_MapData, null,"FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmAttachmentDBAttr.FK_FrmAttachment, null, "附件编号", true, false, 1, 50, 20);

                map.AddTBString(FrmAttachmentDBAttr.RefPKVal, null, "实体主键", true, false, 0, 50, 20);

                map.AddTBString(FrmAttachmentDBAttr.SaveTo, null, "SaveTo", true, false, 0, 200, 20);
                
                map.AddTBString(FrmAttachmentDBAttr.FileName, null,"名称", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentDBAttr.FileExts, null, "扩展", true, false, 0, 50, 20);
                map.AddTBFloat(FrmAttachmentDBAttr.FileSize, 0, "文件大小", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 附件数据存储s
    /// </summary>
    public class FrmAttachmentDBs : EntitiesMyPK
    {
        #region 构造
        /// <summary>
        /// 附件数据存储s
        /// </summary>
        public FrmAttachmentDBs()
        {
        }
        /// <summary>
        /// 附件数据存储s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmAttachmentDBs(string fk_mapdata,string pkval)
        {
            this.Retrieve(FrmAttachmentDBAttr.FK_MapData, fk_mapdata, 
                FrmAttachmentDBAttr.RefPKVal, pkval);
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmAttachmentDB();
            }
        }
        #endregion
    }
}
