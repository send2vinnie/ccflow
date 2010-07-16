using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls;


namespace BP.Win32.Controls
{
	/// <summary>
	/// 按钮
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.Button))]
	public class Btn : System.Windows.Forms.Button
	{
		#region 定义属性
		/// <summary>
		/// 类型
		/// </summary>
		private BtnType _BtnType=BtnType.Confirm ; 
		/// <summary>
		/// 类型
		/// </summary>
		public BtnType BtnType
		{
			get
			{
				return _BtnType; 
			}
			set
			{
				_BtnType=value;
				this.OnLayout();
			}
		}
		#endregion
		
		#region 构造
		public Btn()
		{
		//this.OnLayout+=this.OnLayout();
			
		}
		/// <summary>
		/// 版面设计 
		/// </summary>
		protected   void    OnLayout()
		{
		 
			switch (this.BtnType )
			{
				case BtnType.ApplyTask :
					if (this.Text==null || this.Text=="")
						this.Text="申请任务(A)";
					break;
				case BtnType.Refurbish :
					if (this.Text==null || this.Text=="") 			 
						this.Text="刷新(R)";
					break;
				case BtnType.Back :
					if (this.Text==null || this.Text=="") 			 
						this.Text="返回(B)";
					break;
				case BtnType.Edit :
					if (this.Text==null || this.Text=="") 			 
						this.Text="修改(E)";
					break;
				case BtnType.Close :
					if (this.Text==null || this.Text=="") 			 
						this.Text="关闭(Q)"; 
					break;
				case BtnType.Cancel :
					if (this.Text==null || this.Text=="") 			 
						this.Text="取消(C)";					 
					break;				　
				case BtnType.Confirm :
					if (this.Text==null || this.Text=="")
						this.Text="确定(O)";					 
					break;
				case BtnType.Search :
					if (this.Text==null || this.Text=="") 			 
						this.Text="查找(F)";					 
					break;
				case BtnType.New :
					if (this.Text==null || this.Text=="") 			 
						this.Text="新建(N)";					 
					break;
				case BtnType.SaveAndNew :
					if (this.Text==null || this.Text=="") 			 
						this.Text="保存并新建(R)";					 
					break;
				case BtnType.Delete :
					if (this.Text==null || this.Text=="") 			 
						this.Text="删除(D)";					 
					break;
				case BtnType.Export :
					if (this.Text==null || this.Text=="") 			 
						this.Text="导出(G)";					 
					break;
				case BtnType.Insert :
					if (this.Text==null || this.Text=="") 			 
						this.Text="插入(I)";					 
					break ;
				case BtnType.Print :
					if (this.Text==null || this.Text=="") 			 
						this.Text="打印(P)";					 
					break ;
				case BtnType.Save :
					if (this.Text==null || this.Text=="") 			 
						this.Text="保存(S)";					 
					break;
				case BtnType.View:
					if (this.Text==null || this.Text=="") 			 
						this.Text="浏览(V)";					 
					break;
				case BtnType.Add:
					if (this.Text==null || this.Text=="") 			 
						this.Text="增加(A)";					 
					break;
				case BtnType.SelectAll:
					if (this.Text==null || this.Text=="") 			 
						this.Text="全选择(A)";					 
					break;
				case BtnType.SelectNone:
					if (this.Text==null || this.Text=="") 			 
						this.Text="全不选(N)";					 
					break;
				case BtnType.Reomve:
					if (this.Text==null || this.Text=="") 			 
						this.Text="移除(M)";					 
					break;
				default:
					if (this.Text==null || this.Text=="")
						this.Text="确定(O)";					 
					break; 
			}
		}
		#endregion
	}
}
