
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// ��ͨ����
	/// </summary>
	public class GEWorkAttr :WorkAttr
	{
	}
    /// <summary>
    /// ��ͨ����
    /// </summary>
    public class GEWork : Work
    {
        #region ��_SQLCash �����й�
        private SQLCash _SQLCash = null;
        public override SQLCash SQLCash
        {
            get
            {
                if (_SQLCash == null)
                {
                    _SQLCash = BP.DA.Cash.GetSQL(this.NodeID.ToString());
                    if (_SQLCash == null)
                    {
                        _SQLCash = new SQLCash(this);
                        BP.DA.Cash.SetSQL(this.NodeID.ToString(), _SQLCash);
                    }
                }
                return _SQLCash;
            }
            set
            {
                _SQLCash = value;
            }
        }
        #endregion

        #region ���캯��        
        /// <summary>
        /// ��ͨ����
        /// </summary>
        public GEWork()
        {
        }
        /// <summary>
        /// ��ͨ����
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        public GEWork(int nodeid)
        {
            this.NodeID = nodeid;
            this.SQLCash = null;
        }
        /// <summary>
        /// ��ͨ����
        /// </summary>
        /// <param name="nodeid">�ڵ�ID</param>
        /// <param name="_oid">OID</param>
        public GEWork(int nodeid, Int64 _oid)
        {
            this.NodeID = nodeid;
            this.OID = _oid;
            this.SQLCash = null;
        }
        #endregion

        #region Map
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                //if (this._enMap != null)
                //    return this._enMap;
                //   BP.Sys.MapData md = new BP.Sys.MapData();
                this._enMap = BP.Sys.MapData.GenerHisMap("ND" + this.NodeID.ToString());
                return this._enMap;
            }
        }
        /// <summary>
        /// GEWorks
        /// </summary>
        public override Entities GetNewEntities
        {
            get
            {
                if (this.NodeID == 0)
                    return new GEWorks();

                return new GEWorks(this.NodeID);
            }
        }
        #endregion
    }
    /// <summary>
    /// ��ͨ����s
    /// </summary>
    public class GEWorks : Works
    {
        #region ���ػ��෽��
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID = 0;
        #endregion

        #region ����
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.NodeID == 0)
                    return new GEWork();
                return new GEWork(this.NodeID);
            }
        }
        /// <summary>
        /// ��ͨ����ID
        /// </summary>
        public GEWorks()
        {
        }
        /// <summary>
        /// ��ͨ����ID
        /// </summary>
        /// <param name="nodeid"></param>
        public GEWorks(int nodeid)
        {
            this.NodeID = nodeid;
        }
        #endregion
    }
}
