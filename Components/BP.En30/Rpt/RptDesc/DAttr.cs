
using System;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;

namespace BP.Rpt
{
	public enum ChartType
	{
		/// <summary>
		/// ��״ͼ
		/// </summary>
		Pie,
		/// <summary>
		/// ��״ͼ
		/// </summary>
		Histogram,
		/// <summary>
		/// ����ͼ		
		/// </summary>
		Line
	}
	/// <summary>
	/// ������ʽ
	/// </summary>
	public enum WorkWay
	{
		/// <summary>
		/// �Լ�����
		/// </summary>
		Self,
		/// <summary>
		/// ���
		/// </summary>
		Sum,
		/// <summary>
		/// ��ƽ����
		/// </summary>
		Avg,
		/// <summary>
		/// ��������
		/// </summary>
		Count
		
	}
	/// <summary>
	/// Entity ��ժҪ˵����
	/// </summary>	
	[Serializable]
	public class DAttr
	{
		#region ������ʽ
		public string Tag=null;
		public Attr Attr=null;
		/// <summary>
		/// ������ʽ
		/// </summary>
		public WorkWay WorkWay=WorkWay.Count;
		/// <summary>
		/// �����0�Ƿ����ȥ����
		/// </summary>
		public bool IsCutIfIsZero=false;
		#endregion

		#region ����
		/// <summary>
		/// ʵ��
		/// </summary>
		public DAttr(Attr attr,WorkWay ww,string tag,bool IsCutIfIsZero)
		{
			this.Attr=attr;
			this.WorkWay=ww;
			this.IsCutIfIsZero= IsCutIfIsZero;
			this.Tag=tag;
		}
		#endregion

	}
	/// <summary>
	/// 
	/// </summary>
	public class DAttrs :System.Collections.CollectionBase
	{
		#region public 
		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="attr">DAttr</param>
		public void Add(DAttr attr)
		{
			this.InnerList.Add(attr);
		}
		/// <summary>
		/// ����һ��key���dattr.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public DAttr GetDAttrByKey(string key)
		{
			foreach(DAttr attr in this)
			{
				if (attr.Attr.Key==key)
					return attr;
			}
			throw new Exception("@������key=["+key+"]�����ԡ�");
		}
		/// <summary>
		/// ����attr����
		/// </summary>
		public Attrs HisAttrs
		{
			get
			{
				Attrs attrs = new Attrs();
				foreach(DAttr attr in this)
				{
					attrs.Add(attr.Attr);
				}
				return attrs;
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// ���췽��
		/// </summary>
		public DAttrs()
		{
		} 
		#endregion

		#region ��������
		/// <summary>
		/// �����������ʼ����ڵ�Ԫ��Attr��
		/// </summary>
		public DAttr this[int index]
		{			
			get
			{	
				return (DAttr)this.InnerList[index];
			}
		}
		#endregion
	}
}
