using System;
using System.Reflection;
using System.Collections;


namespace BP.Report
{
	public class ClassFactory
	{
		/// <summary>
		/// ���ö���ʵ����ָ�����Ե�ֵ
		/// </summary>
		/// <param name="obj">����ʵ��</param>
		/// <param name="propertyName">������������Ϊ�Ǿ�̬����</param>
		/// <param name="val">ֵ</param>
		public static void SetValue(object obj ,string propertyName ,object val)
		{
			Type tp = obj.GetType();
			PropertyInfo p = tp.GetProperty( propertyName);
			if( p==null)
				throw new Exception( "��������ֵʧ�ܣ�����["+tp+"]û������["+propertyName+"]");
			p.SetValue( obj ,val ,null);
		}
		/// <summary>
		/// ��ȡ����ʵ����ָ�����Ե�ֵ
		/// </summary>
		/// <param name="obj">����ʵ��</param>
		/// <param name="propertyName">������</param>
		/// <returns>ֵ</returns>
		public static object GetValue(object obj ,string propertyName)
		{
			Type tp = obj.GetType();
			PropertyInfo p = tp.GetProperty( propertyName);
			if( p==null)
				throw new Exception( "��ȡ����ֵʧ�ܣ�����["+tp+"]û������["+propertyName+"]");
			object val = p.GetValue( obj ,null);
			return val;
		}
		/// <summary>
		/// ��ȡ����ʵ����ָ�����Ե�ֵ��ת��Ϊstring
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="propertyName"></param>
		/// <returns>ֵ</returns>
		public static string GetValueToStr(object obj ,string propertyName)
		{
			object val = GetValue(obj,propertyName);
			if(val==null)
				return "";
			else
				return val.ToString();
		}

	}
}