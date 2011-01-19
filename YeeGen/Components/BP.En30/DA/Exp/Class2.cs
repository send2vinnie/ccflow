using System;
using System.Text;

namespace BP.DA
{
	 
	/// <summary>
	/// 字符串表达式计算实现,返回计算结果字符数组
	/// 日期：2005-05-17
	/// </summary>
	public class Calculate
	{
		public bool IsNumber(string s)
		{
			try
			{
				decimal i= decimal.Parse(s);
				return true;
			}
			catch
			{
				return false;
			}
		}
		private clsStack S=new clsStack();
		public Calculate()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
  
  
		/// <summary>
		/// 根据数字表达式字符串数组，返回计算结果字符数组
		/// </summary>
		/// <param name="strSoure">strSour 中缀表达式字符串，头部没有“#”，尾部需要加上“#”</param>
		/// <returns>计算结果</returns>
		public string[] Run(string[] strSoure)
		{
			if(strSoure==null)
			{
				return null;
			}
			string[] dRtn=new string[strSoure.Length];
			for(int k=0;k<strSoure.Length;k++)
			{
				string[] ATemp;
				string strRPN;
				strRPN=GetRPN(strSoure[k]);
				try
				{
					ATemp=strRPN.Trim().Split(' ');
					for(int i=0;i<ATemp.Length;i++)
					{
						if(this.IsNumber(ATemp[i]))
							S.Push(ATemp[i]);
						else
							DoOperate(ATemp[i]);
					}
   
					dRtn[k]=S.Pop();
				}
				catch{}
			}
			return dRtn;
		}
		/// <summary>
		///  Run 返回后缀表达式
		///  strSour 中缀表达式字符串，头部没有“#”，尾部需要加上“#”
		///  String  后缀表达式字符串，头尾都没有“#”
		/// </summary>
		/// <param name="strSource"></param>
		/// <returns></returns>
		private string GetRPN(string strSource)
		{
			string[] ATemp;
			string strRPN="",Y;
			ATemp=strSource.Trim().Split(' ');
			S.Initialize(ATemp.Length);
			S.MakeEmptly();
			S.Push("#");
			try
			{
				for(int k=0;k<ATemp.Length;k++)
				{
					//数字
					if(this.IsNumber(ATemp[k]))
					{
						strRPN += " "+ATemp[k];
					}
						//字符
					else 
					{
						if(ATemp[k]==")")
						{
							do
							{
								Y=S.Pop();
								if(Y!="(")
									strRPN += " "+Y;
							}
							while(Y.Trim()!="(");
						}
						else
						{
							do
							{
								Y = S.Pop();
								if (GetISP(Y) > GetICP(ATemp[k])) 
									strRPN += " "+Y;
							}
							while(GetISP(Y) > GetICP(ATemp[k]));
							S.Push(Y);
							S.Push(ATemp[k]);
						}
					}
				}
				do
				{
					Y=S.Pop();
					if(Y!="#")
						strRPN+=" "+Y;
				}
				while(Y!="#");
			}
			catch{}
			return strRPN;
		}

		#region 运算符优先级定义
		private enum isp
		{
			s35 = 0,
			s40 = 1,
			s94 = 7,
			s42 = 5,
			s47 = 5,
			s37 = 5,
			s43 = 3,
			s45 = 3,
			s41 = 8
		}
		private enum icp
		{
			s35 = 0,
			s40 = 8,
			s94 = 6,
			s42 = 4,
			s47 = 4,
			s37 = 4,
			s43 = 2,
			s45 = 2,
			s41 = 1
		}
		private int GetISP(string a1)
		{
			Encoding ascii =Encoding.ASCII;
			byte[] a=ascii.GetBytes(a1);
			switch(Convert.ToInt32(a[0]))
			{
				case 35:
					return (int)isp.s35;

				case 40:
					return (int)isp.s40;
     
				case 94:
					return (int)isp.s94;
     
				case 42:
					return (int)isp.s42;
     
				case 47:
					return (int)isp.s47;
     
				case 37:
					return (int)isp.s37;
     
				case 43:
					return (int)isp.s43;
     
				case 45:
					return (int)isp.s45;
     
				case 41:
					return (int)isp.s41;
				default:
					return (int)isp.s35;
			}
   
		}
		private int GetICP(string a1)
		{
			Encoding ascii =Encoding.ASCII;
			byte[] a=ascii.GetBytes(a1);
			switch(Convert.ToInt32(a[0]))
			{
				case 35:
					return (int)icp.s35;
     
				case 40:
					return (int)icp.s40;
     
				case 94:
					return (int)isp.s94;
     
				case 42:
					return (int)icp.s42;
     
				case 47:
					return (int)icp.s47;
     
				case 37:
					return (int)icp.s37;
     
				case 43:
					return (int)icp.s43;
     
				case 45:
					return (int)icp.s45;
     
				case 41:
					return (int)icp.s41;
				default:
					return (int)icp.s35;
    
			}
   
		}
		#endregion

		/// <summary>
		/// 判断是否存在左右数字，并且复制
		/// </summary>
		/// <param name="dLeft">左数值</param>
		/// <param name="dRight">右的数值</param>
		/// <returns>是否成功</returns>
		private bool GetTwoItem(ref decimal dLeft,ref decimal dRight)
		{
			bool bRtn=true;
			try
			{
				if(S.IsEmptly())
					return false;
				else
					dRight = Convert.ToDecimal(S.Pop());
				if(S.IsEmptly())
					return false;
				else
					dLeft = Convert.ToDecimal(S.Pop());
			}
			catch
			{

			}
			return bRtn;
		}
		/// <summary>
		/// 根据运算符号计算，并且把计算结果以字符形式填充入栈
		/// </summary>
		/// <param name="op"></param>
		private void DoOperate(string op)
		{
			decimal NumLeft=0,NumRight=0;
			bool r;
			r=GetTwoItem(ref NumLeft,ref NumRight);
			if(r)
			{
				switch(op.Trim())
				{
					case "+":
						S.Push((NumLeft+NumRight).ToString());
						break;
					case "-":
						S.Push((NumLeft-NumRight).ToString());
						break;
					case "*":
						S.Push((NumLeft*NumRight).ToString());
						break;
					case "/":
						if(NumRight==0)
							S.Push("0");
						else
							S.Push((NumLeft/NumRight).ToString());
						break;

				}
			}
		}

	}

}

