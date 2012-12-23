using System;
using System.IO;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// �ļ�����
	/// </summary>
    public class SysFileTypeAttr
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
	/// SysFileType
	/// </summary>
    public class SysFileType : EntityNoName
    {
        #region ʵ�ֻ�������
        public string Icon
        {
            get
            {
                return this.GetValStringByKey(SysFileTypeAttr.Icon);
            }
            set
            {
                this.SetValByKey(SysFileTypeAttr.Icon, value);
            }
        }
        #endregion

        #region ���췽��
        public SysFileType() { }
        /// <summary>
        /// �ļ�������
        /// </summary>
        /// <param name="_No"></param>
        public SysFileType(string _No)
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
                Map map = new Map("Sys_FileType");
                map.EnType = EnType.Sys;
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "�ļ�����";
                map.AddTBStringPK(SysFileTypeAttr.No, null, "���", true, false, 1, 10, 10);
                map.AddTBString(SysFileTypeAttr.Name, null, "����", true, false, 0, 50, 20);
                map.AddTBString(SysFileTypeAttr.Icon, "ͼ��", "ͼ��", true, false, 0, 50, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �ļ������� 
	/// </summary>
	public class SysFileTypes :Entities
	{
		/// <summary>
		/// �ļ�������s
		/// </summary>
		public SysFileTypes(){}		 
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysFileType();
			}
		}
	}
}
