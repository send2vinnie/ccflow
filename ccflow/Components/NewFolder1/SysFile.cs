using System;
using System.IO;
using System.Collections;
using BP.DA;
using BP.En;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace BP.Sys
{
    public class SysFileAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 上传日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 记录人
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// 关联的Table
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// 关联的key
        /// </summary>
        public const string RefVal = "RefVal";
        /// <summary>
        /// ImgH
        /// </summary>
        public const string ImgH = "ImgH";
        /// <summary>
        /// ImgW
        /// </summary>
        public const string ImgW = "ImgW";
        /// <summary>
        /// 文件大小
        /// </summary>
        public const string FileSize = "FileSize";
        /// <summary>
        /// 文件类型
        /// </summary>
        public const string FileType = "FileType";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// FileID
        /// </summary>
        public const string FileID = "FileID";
        public const string FileName = "FileName";
    }
    public class SysFile : BP.En.EntityMyPK
    {
        #region 实现基本属性
        public string FileID
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileID);
            }
            set
            {
                this.SetValByKey(SysFileAttr.FileID, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.Name);
            }
            set
            {
                this.SetValByKey(SysFileAttr.Name, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.Rec);
            }
            set
            {
                this.SetValByKey(SysFileAttr.Rec, value);
            }
        }
        /// <summary>
        /// EnName  
        /// </summary>
        public string EnName
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.EnName);
            }
            set
            {
                this.SetValByKey(SysFileAttr.EnName, value);
            }
        }
        public object RefVal
        {
            get
            {
                return this.GetValByKey(SysFileAttr.RefVal);
            }
            set
            {
                this.SetValByKey(SysFileAttr.RefVal, value);
            }
        }
        public int ImgH
        {
            get
            {
                return this.GetValIntByKey(SysFileAttr.ImgH);
            }
            set
            {
                this.SetValByKey(SysFileAttr.ImgH, value);
            }
        }
        public int ImgW
        {
            get
            {
                return this.GetValIntByKey(SysFileAttr.ImgW);
            }
            set
            {
                this.SetValByKey(SysFileAttr.ImgW, value);
            }
        }
        public string FileSize
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileSize);
            }
            set
            {
                this.SetValByKey(SysFileAttr.FileSize, value);
            }
        }
        public string FileName
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileName);
            }
            set
            {
                this.SetValByKey(SysFileAttr.FileName, value);
            }
        }
        public string FileType
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.FileType);
            }
            set
            {
                string s = value;
                s = s.Replace(".", "");
                this.SetValByKey(SysFileAttr.FileType, s);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.RDT);
            }
            set
            {
                this.SetValByKey(SysFileAttr.RDT, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(SysFileAttr.Note);
            }
            set
            {
                this.SetValByKey(SysFileAttr.Note, value);
            }
        }
        #endregion

        #region 构造方法

        public SysFile() { }
        /// <summary>
        /// 文件管理者
        /// </summary>
        /// <param name="_OID"></param>
        public SysFile(string _OID)
            : base(_OID)
        {
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_File");
                map.EnDesc = "文件管理者";
                map.CodeStruct = "3";
                map.AddMyPK();
                map.AddTBString(SysFileAttr.Rec, null, "上传人", true, false, 0, 50, 20);
                map.AddTBString(SysFileAttr.Name, null, "名称", true, false, 0, 500, 20);
                map.AddTBString(SysFileAttr.EnName, null, "类", false, true, 0, 50, 20);
                map.AddTBString(SysFileAttr.RefVal, null, "主键", false, true, 0, 50, 10);
                map.AddTBString(SysFileAttr.FileID, null, "FileID", false, true, 0, 50, 10);

                map.AddTBInt(SysFileAttr.ImgH, 0, "ImgH", false, true);
                map.AddTBInt(SysFileAttr.ImgW, 0, "ImgW", false, true);

                map.AddTBString(SysFileAttr.FileSize, null, "大小", true, true, 0, 20, 10);
                map.AddTBString(SysFileAttr.FileType, null, "文件类型", true, true, 0, 50, 20);

                map.AddTBDate(SysFileAttr.RDT, null, "时间", true, true);
                //   map.AddTBString(SysFileAttr.Note, null, "备注", true, false, 0, 200, 30);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 文件管理者 
	/// </summary>
	public class SysFiles :EntitiesMyPK
	{
        /// <summary>
        /// 产生图片串
        /// </summary>
        /// <param name="en"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static string GenerImgs(Entity en, string pk)
        {
            string html = "";
            if (en.GetValStrByKey("MyFileName") != "")
                html += "<img src='/Data/" + en.ToString() + "/" + en.PKVal + "." + en.GetValStrByKey("MyFileExt") + "' align=left border=0/>";

            if (en.GetValIntByKey("MyFileNum") == 0)
                return html;

            SysFiles ens = new SysFiles(en.ToString(), pk);
            foreach (SysFile sf in ens)
            {
                html += "<br><img align=left src='/Data/" + en.ToString() + "/" + sf.FileID + "." + sf.FileType + "' width='" + sf.ImgW + "px' height='" + sf.ImgH + "px' border=0/>";
            }
            return html;
        }
        /// <summary>
        /// 文件管理者
        /// </summary>
		public SysFiles(){}
        /// <summary>
        /// 文件管理者
        /// </summary>
        /// <param name="enName"></param>
        /// <param name="key"></param>
        public SysFiles(string enName, string key)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(SysFileAttr.EnName, enName);
            qo.addAnd();
            qo.AddWhere(SysFileAttr.RefVal, key);
            qo.DoQuery();
        }
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysFile();
			}
		}
	}
}
