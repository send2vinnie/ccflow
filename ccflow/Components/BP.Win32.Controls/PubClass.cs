using System;
using BP.Win32.Controls ;

using BP.En ;
using System.Windows.Forms;


namespace BP.Win32
{
	/// <summary>
	/// Class2 的摘要说明。
	/// </summary>
	public class PubClass
	{
		public static bool Warning(string msg)
		{
			if (MessageBox.Show(msg,"执行确认", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly)==DialogResult.No)
				return false;
			return true;
		}
		public static bool Question(string msg)
		{
            if (MessageBox.Show(msg, BP.Sys.Language.GetValByUserLang("PChose", "请选择"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
				return false;
			return true;
		}
		public static void Information(string msg)
		{
			MessageBox.Show(msg, "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		public static void Alert(Exception ex)
		{
			UIAlert ui = new UIAlert();
			ui.Show(ex.Message) ;
			ui.ShowDialog();
		}
		public static void Alert(string msg)
		{
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //UIAlert ui = new UIAlert();
            //ui.Show(msg) ;
            //ui.ShowDialog();
		}
		/// <summary>
		/// 从一个集合里面找到他的孩子节点。
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="en"></param>
		/// <returns></returns>
		//		public static GradeEntitiesNoNameBase GetHisChildEns(GradeEntitiesNoNameBase ens, GradeEntityNoNameBase en)
		//		{
		//
		//		}
	}
	
}
