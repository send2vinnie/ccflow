using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// �����б�
	/// </summary>
    public class EntityTreeAttr
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
        /// <summary>
        /// ˳���
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public const string IsDir = "IsDir";
    }
	/// <summary>
	/// ���б�����ƵĻ���ʵ��
	/// </summary>
    abstract public class EntityTree : Entity
    {
        #region ����
        /// <summary>
        /// Ψһ��ʾ
        /// </summary>
        public string ID
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.ID);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.ID, value);
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public string TreeNo
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.TreeNo);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.TreeNo, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.Name, value);
            }
        }
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public string PID
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.PID);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.PID, value);
            }
        }
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public bool IsDir
        {
            get
            {
                return this.GetValBooleanByKey(EntityTreeAttr.IsDir);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.IsDir, value);
            }
        }
        /// <summary>
        /// ˳���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(EntityTreeAttr.Idx);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.Idx, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int Grade
        {
            get
            {
                return this.TreeNo.Length / 2;
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
                return EntityTreeAttr.ID;
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public EntityTree()
        {
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        /// <param name="_no"></param>
        public EntityTree(string _id)
        {
            if (string.IsNullOrEmpty(_id))
                throw new Exception(this.EnDesc + "@�Ա�[" + this.EnDesc + "]���в�ѯǰ����ָ����š�");

            this.ID = _id;
            if (this.Retrieve() == 0)
            {
                throw new Exception("@û��" + this._enMap.PhysicsTable + ", No = " + this.ID + "�ļ�¼��");
            }
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

        #region ����������õķ���
        /// <summary>
        /// �½�ͬ���ڵ�
        /// </summary>
        /// <returns></returns>
        public string DoCreateSameLevelNode()
        {
            EntityTree en = this.CreateInstance() as EntityTree;
            en.ID = en.GenerNewNoByKey(EntityTreeAttr.ID);
            en.Name = "�½��ڵ�" + en.ID;
            en.PID = this.PID;
            en.IsDir = false;
            // en.TreeNo=this.GenerNewNoByKey(EntityTreeAttr.TreeNo,EntityTreeAttr.PID,this.PID)
            en.TreeNo = this.GenerNewNoByKey(EntityTreeAttr.TreeNo,EntityTreeAttr.PID, this.PID);
            en.Insert();
            return en.ID;
        }
        /// <summary>
        /// �½��ӽڵ�
        /// </summary>
        /// <returns></returns>
        public string DoCreateSubNode()
        {
            EntityTree en = this.CreateInstance() as EntityTree;
            en.ID = en.GenerNewNoByKey(EntityTreeAttr.ID);
            en.Name = "�½��ڵ�" + en.ID;
            en.PID = this.ID;
            en.IsDir = false;
            en.TreeNo = this.GenerNewNoByKey(EntityTreeAttr.TreeNo, EntityTreeAttr.PID, this.ID);
            if (en.TreeNo.Substring(en.TreeNo.Length - 2) == "01")
                en.TreeNo = this.TreeNo + "01";
            en.Insert();

            // ���ô˽ڵ���Ŀ¼
            if (this.IsDir == false)
            {
                this.IsDir = true;
                this.Update(EntityTreeAttr.IsDir, true);
            }
            return en.ID;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string DoUp()
        {
            this.DoOrderUp(EntityTreeAttr.PID, this.PID, EntityTreeAttr.Idx);
            return null;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string DoDown()
        {
            this.DoOrderDown(EntityTreeAttr.PID, this.PID, EntityTreeAttr.Idx);
            return null;
        }
        #endregion
    }
	/// <summary>
    /// ���б�����ƵĻ���ʵ��s
	/// </summary>
    abstract public class EntitiesTree : Entities
    {
        /// <summary>
        /// ��ȡ�����ӽڵ�
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public EntitiesTree GenerHisChinren(EntityTree en)
        {
            EntitiesTree ens = this.CreateInstance() as EntitiesTree;
            foreach (EntityTree item in ens)
            {
                if (en.PID == en.ID)
                    ens.AddEntity(item);
            }
            return ens;
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public new EntityTree this[int index]
        {
            get
            {
                return (EntityTree)this.InnerList[index];
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public EntitiesTree()
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
