using System;
using System.Data;
using System.Collections;
 

namespace BP.En
{
	/// <summary>
	/// Row ��ժҪ˵����
	/// ��������һ����¼�Ĵ�ţ����⡣
	/// </summary>
	public class Row : Hashtable
	{
        public Row():base(System.StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, true))
        {
        }
        /// <summary>
        /// LoadAttrs
        /// </summary>
        /// <param name="attrs"></param>
        public void LoadAttrs(Attrs attrs)
        {
            this.Clear();
            foreach (Attr attr in attrs)
            {
                switch (attr.MyDataType)
                {
                    case BP.DA.DataType.AppInt:
                        this.Add(attr.Key, int.Parse(attr.DefaultVal.ToString()));
                        break;
                    case BP.DA.DataType.AppFloat:
                        this.Add(attr.Key, float.Parse(attr.DefaultVal.ToString()));
                        break;
                    case BP.DA.DataType.AppMoney:
                        this.Add(attr.Key, decimal.Parse(attr.DefaultVal.ToString()));
                        break;
                    case BP.DA.DataType.AppDouble:
                        this.Add(attr.Key, double.Parse(attr.DefaultVal.ToString()));
                        break;
                    default:
                        this.Add(attr.Key, attr.DefaultVal);
                        break;
                }
            }
        }

         /// <summary>
        /// LoadAttrs
        /// </summary>
        /// <param name="attrs"></param>
        public void LoadDataTable(DataTable dt, DataRow dr)
        {
            this.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                this.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
        }

		/// <summary>
		/// ����һ��ֵby key . 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
        public void SetValByKey(string key, object val)
        {
            if (val == null)
            {
                this[key] = val;
                return;
            }

            //this.Values[].Add(key,val);
            if (val.GetType() == typeof(TypeCode))
                this[key] = (int)val;
            else
                this[key] = val;
        }
        public object GetValByKey(string key)
        {
            return this[key];
            /*
            if (SystemConfig.IsDebug)
            {
                try
                {
                    return this[key];
                }
                catch(Exception ex)
                {
                    throw new Exception("@GetValByKeyû���ҵ�key="+key+"������Vale , ��ȷ��Map �����Ƿ��д�����."+ex.Message);
                }
            }
            else
            {
                return this[key];

            }
            */
        }

	}
	/// <summary>
	/// row ����
	/// </summary>
	public class Rows : System.Collections.CollectionBase
	{
		public Rows()
		{
		}
		public Row this[int index]
		{
			get 
			{
				return (Row)this.InnerList[index];
			}
		}	 
		/// <summary>
		/// ����һ��Row .
		/// </summary>
		/// <param name="r">row</param>
		public void Add(Row r)
		{
			this.InnerList.Add(r);
		}
	}
}
