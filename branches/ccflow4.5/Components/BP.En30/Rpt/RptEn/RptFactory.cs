using System;
using BP.En ; 
using BP.DA;
using BP.Web ; 

namespace BP.Rpt
{
	/// <summary>
	/// RptFactory ��ժҪ˵����
	/// </summary>
	public class RptFactory
	{
		public RptFactory()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����3ά��Rpt3DEntity
		/// ����ͳ�Ƹ���
		/// </summary>
		/// <param name="en">ʵ��</param>
		/// <param name="attrOfD1">γ��1����</param>
		/// <param name="attrOfD2">γ��2����</param>
		/// <param name="attrOfD3">γ��3����</param>
		/// <param name="d1d2RefKey">γ��2��3�������ԣ�����Ϊ�գ�</param>
		/// <param name="cellUrl">Url��������Ϊ�գ�</param>
		/// <returns>���ɵ�ʵ�塣</returns>
		public static Rpt3DEntity Rpt3DEntity(Entity en, string attrOfD1, string attrOfD2, string attrOfD3, string d1d2RefKey, string cellUrl)
		{
			return new Rpt3DEntity();
		}
 
	}
}
