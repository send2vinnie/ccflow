using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// 属性列表
	/// </summary>
    public class EntityNoNameIntAttr : EntityNoAttr
    {
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "Name";
        public const string NameOfS = "NameOfS";
        /// <summary>
        /// NoInt
        /// </summary>
        public const string NoInt = "NoInt";
    }
    public class EntityNoNameIntMyFileAttr : EntityNoMyFileAttr
    {
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "Name";

    }
	/// <summary>
	/// NoEntity 的摘要说明。
	/// </summary>
    abstract public class EntityNoNameInt : EntityNo
    {
        #region 属性
        public override string PK
        {
            get
            {
                return "No";
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityNoNameIntAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityNoNameIntAttr.Name, value);
            }
        }
        public string NameE
        {
            get
            {
                return this.GetValStringByKey("NameE");
            }
            set
            {
                this.SetValByKey("NameE", value);
            }
        }
        /// <summary>
        /// Int PK
        /// </summary>
        public int NoInt
        {
            get
            {
                return this.GetValIntByKey(EntityNoNameIntAttr.NoInt);
            }
            set
            {
                this.SetValByKey(EntityNoNameIntAttr.NoInt, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public EntityNoNameInt()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_No"></param>
        protected EntityNoNameInt(string _No) : base(_No) { }
        #endregion

        #region 业务逻辑处理
        /// <summary>
        /// 检查名称的问题.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 1)
                        throw new Exception("@插入错误[" + this.EnMap.EnDesc + "] 名称[" + Name + "]重复.");
                }
            }
            return base.beforeInsert();
        }

        #endregion

        #region 查询
        /// <summary>
        /// 安名称查询
        /// </summary>
        /// <returns>返回查询的Num</returns>
        public int RetrieveByName()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere("Name", this.Name);
            return qo.DoQuery();
        }
        public int RetrieveByName(string name)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere("Name", name);
            return qo.DoQuery();
        }
        /// <summary>
        /// 按照名称模糊查询
        /// </summary>
        /// <param name="likeName">likeName</param>
        /// <returns>返回查询的Num</returns>
        public int RetrieveByLikeName(string likeName)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere("Name", "like", " %" + likeName + "% ");
            return qo.DoQuery();
        }
        #endregion 查询
    }
	/// <summary>
	/// EntitiesNoName
	/// </summary>
    abstract public class EntitiesNoNameInt : EntitiesNo
    {
        /// <summary>
        /// 将对象添加到集合尾处，如果对象已经存在，则不添加.
        /// </summary>
        /// <param name="entity">要添加的对象</param>
        /// <returns>返回添加到的地方</returns>
        public virtual int AddEntity(EntityNoNameInt entity)
        {
            foreach (EntityNoNameInt en in this)
            {
                if (en.No == entity.No)
                    return 0;
            }
            return this.InnerList.Add(entity);
        }
        /// <summary>
        /// 将对象添加到集合尾处，如果对象已经存在，则不添加
        /// </summary>
        /// <param name="entity">要添加的对象</param>
        /// <returns>返回添加到的地方</returns>
        public virtual void Insert(int index, EntityNoNameInt entity)
        {
            foreach (EntityNoNameInt en in this)
            {
                if (en.No == entity.No)
                    return;
            }

            this.InnerList.Insert(index, entity);
        }
        /// <summary>
        /// 构造
        /// </summary>
        public EntitiesNoNameInt()
        {
        }
        public override int RetrieveAll()
        {
            return base.RetrieveAll("No");
        }


        #region 与文件有关系的操作
        /// <summary>
        /// 把指定的目录放进数据库中
        /// </summary>
        /// <param name="dirPath">目录文件</param>
        public int ReadDirToData(string dirPath, string parentNo)
        {
            //  string dirName = dirPath.Substring(dirPath.LastIndexOf("\\") + 1);
            //  string dirNo = dirName.Substring(0, 2);

            this.Clear();
            string[] dirs = System.IO.Directory.GetDirectories(dirPath); // 获取文件夹。
            foreach (string dir in dirs)
            {
                string dirShortName = dir.Substring(dir.LastIndexOf("\\") + 1);
                string no = dirShortName.Substring(0, 2);

                EntityNoNameInt en = this.GetNewEntity as EntityNoNameInt;
                en.No = parentNo + no;
                en.Name = dirShortName.Substring(2);

                en.SetValByKey("FK_Sort", parentNo);
                en.SetValByKey("FK_Item1", parentNo);
                en.SetValByKey("FK_Type", parentNo);

                en.Save();
                this.AddEntity(en);
            }
            return this.Count;
        }
        /// <summary>
        /// 读取文件到数据库
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void ReadFileToData(string filePath, string fk_item1, string item2)
        {
            string[] strs = System.IO.Directory.GetFiles(filePath);

            for (int i = 0; i < strs.Length; i++)
            {
                string file = strs[i];
                string shortName = file.Substring(file.LastIndexOf("\\") + 1);
                string ext = shortName.Substring(shortName.LastIndexOf(".") + 1); // 文件后缀。
                int paycent = 0;
                if (shortName.IndexOf("@") != -1)
                {
                    string centStr = shortName.Substring(shortName.LastIndexOf("@") + 1); // 得到 xxx.doc 部分。
                    centStr = centStr.Replace("." + ext, "");
                    paycent = int.Parse(centStr);
                }

                EntityNoNameInt en = this.GetNewEntity as EntityNoNameInt;
                en.No = item2 + i.ToString().PadLeft(3, '0');
                en.Name = shortName;

                en.SetValByKey("FK_Item1", fk_item1);
                en.SetValByKey("FK_Item", item2);

                en.SetValByKey("FK_Sort1", fk_item1);
                en.SetValByKey("FK_Sort", item2);

                System.IO.FileInfo info = new System.IO.FileInfo(file);
                en.SetValByKey("FileSize", info.Length / 1000);
                en.SetValByKey("Ext", info.Extension.Replace(".", ""));

                en.Save();
                //fdb.FSize = ;
                //fdb.CDT = info.CreationTime.ToString(DataType.SysDataFormatCN);
                //fdb.PayCent = paycent;
                //fdb.Insert();
            }

        }
        #endregion


    }
}
