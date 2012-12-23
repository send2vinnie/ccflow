using System;
using System.IO;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// �����
	/// </summary>
    public class FrmSortAttr
    {
        /// <summary>
        /// ��׺
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ͼ��
        /// </summary>
        public const string Icon = "Icon";
    }
	/// <summary>
    /// �����
	/// </summary>
    public class FrmSort : EntityNoName
    {
        #region ʵ�ֻ�������
        public string Icon
        {
            get
            {
                return this.GetValStringByKey(FrmSortAttr.Icon);
            }
            set
            {
                this.SetValByKey(FrmSortAttr.Icon, value);
            }
        }
        #endregion

        #region ���췽��
        public FrmSort() { }
        /// <summary>
        /// �ļ�������
        /// </summary>
        /// <param name="_No"></param>
        public FrmSort(string _No)
        {
            this.No = _No;
            try
            {
                this.Retrieve();
            }
            catch
            {
                this.Name = "δ֪����";
                this.Insert();
            }
        }
        /// <summary>
        /// map 
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_FrmSort");
                map.EnType = EnType.Sys;
                map.CodeStruct = "2";
                map.IsAllowRepeatNo = false;
                map.IsAutoGenerNo = true;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "�����";
                map.AddTBStringPK(FrmSortAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(FrmSortAttr.Name, null, "����", true, false, 0, 50, 20);
                //   map.AddTBString(FrmSortAttr.Icon, "ͼ��", "ͼ��", true, false, 0, 50, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// �����s 
	/// </summary>
	public class FrmSorts :EntitiesNoName
	{
		/// <summary>
		/// �ļ�������s
		/// </summary>
		public FrmSorts(){}		 
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FrmSort();
			}
		}
        public override int RetrieveAll()
        {
            int i= base.RetrieveAll();
            if (i == 0)
            {
                FrmSort fs = new FrmSort();
                fs.No = "01";
                fs.Name = "Ĭ�����";
                fs.Insert();
                return base.RetrieveAll();
            }
            else
            {
                return i;
            }
        }
	}
}
