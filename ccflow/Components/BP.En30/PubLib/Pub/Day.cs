using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// ����
	/// </summary>
	public class Day :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		 
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_Day";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("Day", "��"); // "��";
			}
		}
		#endregion 

		#region ���췽��
		 
		public Day(){}
		 
		public Day(string _No ): base(_No){}
        public override Entities GetNewEntities
        {
            get { return new Days(); }
        }
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class Days :SimpleNoNameFixs
	{
		/// <summary>
		/// ���ڼ���
		/// </summary>
		public Days()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Day();
			}
		}
	}
}
