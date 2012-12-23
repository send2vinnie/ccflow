using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// �·�
	/// </summary>
	public class YF :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		 
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_YF";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("YF", "�·�");  // "�·�";
			}
		}
		#endregion 

		#region ���췽��
		public YF()
        {
        }
        /// <summary>
        /// _No
        /// </summary>
        /// <param name="_No"></param>
		public YF(string _No ): base(_No)
        {
        }
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class YFs :SimpleNoNameFixs
	{
		/// <summary>
		/// �·ݼ���
		/// </summary>
		public YFs()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new YF();
			}
		}
	}
}
