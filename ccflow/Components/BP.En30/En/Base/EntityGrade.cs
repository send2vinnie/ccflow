using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// �����б�
	/// </summary>
    public class EntityGradeAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ���ڱ��
        /// </summary>
        public const string PID = "PID";
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public const string ID = "ID";
        /// <summary>
        /// �����
        /// </summary>
        public const string TreeNo = "TreeNo";
    }
	/// <summary>
	/// ���б�����ƵĻ���ʵ��
	/// </summary>
    abstract public class EntityGrade : Entity
    {
        #region ����
        /// <summary>
        /// Ψһ��ʾ
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
        /// ���ṹ���
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
        /// ����
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
        /// ���ڵ���
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
        /// ����
        /// </summary>
        public int Grade
        {
            get
            {
                return int.Parse(this.TreeNo) / 2;
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return EntityGradeAttr.PID;
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public EntityGrade()
        {
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        /// <param name="_no"></param>
        public EntityGrade(int _id)
        {
            if (_id==0)
                throw new Exception(this.EnDesc + "@�Ա�[" + this.EnDesc + "]���в�ѯǰ����ָ����š�");

            this.ID = _id;
            if (this.Retrieve() == 0)
            {
                throw new Exception("@û��" + this._enMap.PhysicsTable + ", No = " + this.ID + "�ļ�¼��");
            }
        }
        public EntityGrade(string treeNo)
        {
            if (string.IsNullOrEmpty(treeNo))
                throw new Exception(this.EnDesc + "@�Ա�[" + this.EnDesc + "]���в�ѯǰ����ָ����š�");

            this.TreeNo = treeNo;
            if (this.Retrieve(EntityGradeAttr.TreeNo,treeNo) == 0)
                throw new Exception("@û��" + this._enMap.PhysicsTable + ", No = " + treeNo + "�ļ�¼��");
        }
        #endregion

        #region ҵ���߼�����
        /// <summary>
        /// ��������treeNo
        /// </summary>
        public void ResetTreeNo()
        {
        }
        /// <summary>
        /// ������Ƶ�����.
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
                //    throw new Exception("@û�и�[" + this.EnDesc + " , " + this.Name + "]��������.");
            }

            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 1)
                        throw new Exception("@����ʧ��[" + this.EnMap.EnDesc + "] ���[" + this.ID + "]����[" + Name + "]�ظ�.");
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
                        throw new Exception("@����ʧ��[" + this.EnMap.EnDesc + "] ���[" + this.ID + "]����[" + Name + "]�ظ�.");
                }
            }
            return base.beforeUpdate();
        }
        #endregion
    }
	/// <summary>
    /// ���б�����ƵĻ���ʵ��s
	/// </summary>
    abstract public class EntitiesGrade : Entities
    {
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public new EntityGrade this[int index]
        {
            get
            {
                return (EntityGrade)this.InnerList[index];
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public EntitiesGrade()
        {
        }
        /// <summary>
        /// ��ѯȫ��
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            return base.RetrieveAll("TreeNo");
        }
    }
}
