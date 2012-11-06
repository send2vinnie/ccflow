using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// 属性列表
	/// </summary>
    public class EntityGradeAttr
    {
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// 父节编号
        /// </summary>
        public const string PID = "PID";
        /// <summary>
        /// 树结构编号
        /// </summary>
        public const string ID = "ID";
        /// <summary>
        /// 树编号
        /// </summary>
        public const string TreeNo = "TreeNo";
    }
	/// <summary>
	/// 具有编号名称的基类实体
	/// </summary>
    abstract public class EntityGrade : Entity
    {
        #region 属性
        /// <summary>
        /// 唯一标示
        /// </summary>
        public int ID
        {
            get
            {
                return this.GetValIntByKey(EntityGradeAttr.ID);
            }
            set
            {
                this.SetValByKey(EntityGradeAttr.ID, value);
            }
        }
        /// <summary>
        /// 树结构编号
        /// </summary>
        public string TreeNo
        {
            get
            {
                return this.GetValStringByKey(EntityGradeAttr.TreeNo);
            }
            set
            {
                this.SetValByKey(EntityGradeAttr.TreeNo, value);
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityGradeAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityGradeAttr.Name, value);
            }
        }
        /// <summary>
        /// 父节点编号
        /// </summary>
        public string PID
        {
            get
            {
                return this.GetValStringByKey(EntityGradeAttr.PID);
            }
            set
            {
                this.SetValByKey(EntityGradeAttr.PID, value);
            }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int Grade
        {
            get
            {
                return int.Parse(this.TreeNo) / 2;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 主键
        /// </summary>
        public override string PK
        {
            get
            {
                return EntityGradeAttr.PID;
            }
        }
        /// <summary>
        /// 树结构编号
        /// </summary>
        public EntityGrade()
        {
        }
        /// <summary>
        /// 树结构编号
        /// </summary>
        /// <param name="_no"></param>
        public EntityGrade(int _id)
        {
            if (_id==0)
                throw new Exception(this.EnDesc + "@对表[" + this.EnDesc + "]进行查询前必须指定编号。");

            this.ID = _id;
            if (this.Retrieve() == 0)
            {
                throw new Exception("@没有" + this._enMap.PhysicsTable + ", No = " + this.ID + "的记录。");
            }
        }
        public EntityGrade(string treeNo)
        {
            if (string.IsNullOrEmpty(treeNo))
                throw new Exception(this.EnDesc + "@对表[" + this.EnDesc + "]进行查询前必须指定编号。");

            this.TreeNo = treeNo;
            if (this.Retrieve(EntityGradeAttr.TreeNo,treeNo) == 0)
                throw new Exception("@没有" + this._enMap.PhysicsTable + ", No = " + treeNo + "的记录。");
        }
        #endregion

        #region 业务逻辑处理
        /// <summary>
        /// 重新设置treeNo
        /// </summary>
        public void ResetTreeNo()
        {
        }
        /// <summary>
        /// 检查名称的问题.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
          //  if (this.ID

            if (this.TreeNo.Trim().Length == 0)
            {
                //if (this.EnMap.IsAutoGenerNo)
                //    this.No = this.GenerNewNo;
                //else
                //    throw new Exception("@没有给[" + this.EnDesc + " , " + this.Name + "]设置主键.");
            }

            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 1)
                        throw new Exception("@插入失败[" + this.EnMap.EnDesc + "] 编号[" + this.ID + "]名称[" + Name + "]重复.");
                }
            }
            return base.beforeInsert();
        }
        protected override bool beforeUpdate()
        {
            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 2)
                        throw new Exception("@更新失败[" + this.EnMap.EnDesc + "] 编号[" + this.ID + "]名称[" + Name + "]重复.");
                }
            }
            return base.beforeUpdate();
        }
        #endregion
    }
	/// <summary>
    /// 具有编号名称的基类实体s
	/// </summary>
    abstract public class EntitiesGrade : Entities
    {
        /// <summary>
        /// 根据位置取得数据
        /// </summary>
        public new EntityGrade this[int index]
        {
            get
            {
                return (EntityGrade)this.InnerList[index];
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        public EntitiesGrade()
        {
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            return base.RetrieveAll("TreeNo");
        }
    }
}
