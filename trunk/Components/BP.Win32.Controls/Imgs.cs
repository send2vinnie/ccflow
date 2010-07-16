using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace BP.Win32
{

	/// <summary>
	/// Summary description for SampleImages.
	/// </summary>
	public class Imgs
	{
		public static Image GetImage(string imgName)
		{
			return System.Drawing.Image.FromFile(BP.SystemConfig.PathOfWebApp+"/Images/"+imgName );
		}
		public static Image Flag
		{
			get
			{
				return Imgs.GetImage("Flag.gif" );
			}
		}
		public static Image Delete
		{
			get
			{
				return Imgs.GetImage("Delete.gif" );
			}
		}
		public static Image OK
		{
			get
			{
				return Imgs.GetImage("OK.gif" );
			}
		}
	}
¡¡
}
