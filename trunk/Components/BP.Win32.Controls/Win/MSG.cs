using System;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// MSG 的摘要说明。
	/// </summary>
	public class MSG
	{
		public MSG()
		{
		}

		public static DialogResult ShowQuestion(string text ,string caption)
		{
			return MessageBox.Show(text,caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		}
	}
}
