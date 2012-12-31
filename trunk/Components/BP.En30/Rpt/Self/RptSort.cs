using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Rpt
{
	/// <summary>
	/// ����ڼ�
	/// </summary>
	public class RptSort :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Rpt_Sort";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "�������";
			}
		}
		#endregion 

		#region ���췽��
		public RptSort()
		{
		}
		public RptSort(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// Sorts
	/// </summary>
	public class RptSorts :SimpleNoNameFixs
	{
		/// <summary>
		/// ����ڼ伯��
		/// </summary>
		public RptSorts()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RptSort();
			}
		}
	}
}
