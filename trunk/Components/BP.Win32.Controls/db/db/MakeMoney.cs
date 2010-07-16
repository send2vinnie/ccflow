using System;

namespace BP.Win32.Controls.CTI.App
{
	/// <summary>
	/// MakeMoney 的摘要说明。
	/// </summary>
	public class MakeMoney
	{
		 
		public MakeMoney()
		{
			 
		}

		public string[] GetMoney(double money)
		{
			double	fraction;
			int	RmbJiao, RmbFen, Rmb;
			double	RmbYuan;

			if (money >= 0)
				money = money + 0.005;          /* 四舍五入处理 */
			else
				money = money - 0.005;

			fraction = Math.Abs( Math.mod modf(money, &RmbYuan));	/* 将数分为整数和小数部分 */

			Rmb = floor(fraction*100.0);    /* 小数部分全变成'分' */
			RmbJiao = floor(Rmb/10);        /* 角的值 */
			RmbFen = Rmb - RmbJiao*10;      /* 分的值 */

			/* 语音字符串 MoneyString 加上'元角分'等语音内容: */

			if (RmbYuan) 
			{
				TV_MakeSentence (RmbYuan, MoneyString);  /* 将整数部分作处理 */
				//		StringLen = strlen (MoneyString);
				MoneyString[StringLen++] = UCN_YUAN;     /* 加上'元' */
			}
			else 
			{
				StringLen = 0;
				if (money < 0.0 && fabs(money) >= 0.01)	 /* 若为负, 加上'负' */
					MoneyString[StringLen++] = CN_NEGATIVE;
			}
			if (!RmbYuan && !RmbJiao && !RmbFen) 
			{  /* 0 元 0 角 0 分 */
				MoneyString[StringLen++] = CN_DIGIT0;     /* 只加 '0 分' */
				MoneyString[StringLen++] = UCN_FEN;
			}
			if (RmbYuan && !RmbJiao && RmbFen)      /* n 元 0 角 m 分 */
				MoneyString[StringLen++] = CN_DIGIT0;
			if (RmbJiao) 
			{                          /* n 角 */
				MoneyString[StringLen++] = CN_DIGIT0 + RmbJiao;
				MoneyString[StringLen++] = UCN_JIAO;
			}
			if (RmbFen) 
			{                           /* n 分 */
				MoneyString[StringLen++] = CN_DIGIT0 + RmbFen;
				MoneyString[StringLen++] = UCN_FEN;
			}
			MoneyString[StringLen++] = 0;
			return StringLen;
		}

	}
}
