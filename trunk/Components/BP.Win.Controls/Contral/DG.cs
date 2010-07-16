using System; 
using System.Data;
using System.Drawing;
using CWAI.En.Base;
using CWAI.DA;

namespace CWAI.Win32.Controls
{
	/// <summary>
	/// DG 的摘要说明。
	/// </summary>
	public class DG : System.Windows.Forms.DataGrid
	{	
		#region 运行时刻的变量
		/// <summary>
		/// 用于现时计算
		/// </summary>
		private float[]  MyCount= new float[50];
		public Entity HisEn=null;
		public string DataKeyField=null;
		#endregion


		#region 构造
		public DG()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#endregion

	

		#region 关于多行编辑的方法
		/// <summary>
		/// 初始化一个datagride 结构出事化他的列
		/// </summary>
		/// <param name="en">entity</param> 
		public void InitDGColumns(Entity  en)
		{
			this.DataKeyField=en.PK;
			this.HisEn = en;
			this.Columns.Clear();
			this.AllowSorting=true;
			this.AutoGenerateColumns=false;

			ButtonColumn btn1 = new ButtonColumn();
			btn1.HeaderText="操作";
			btn1.ButtonType = ButtonColumnType.LinkButton;
			btn1.CommandName="Select";
			btn1.Text="选择";			
			this.Columns.Add(btn1);
			Attrs attrs = en.EnMap.Attrs;
			foreach(Attr attr in attrs)
			{
				BoundColumn bc = new  BoundColumn(); 
				bc.DataField=attr.Key;
				bc.HeaderText=attr.Desc;
				bc.Visible=attr.UIVisible;
				this.Columns.Add(bc);
			}
		}		
		#endregion
	}
}
