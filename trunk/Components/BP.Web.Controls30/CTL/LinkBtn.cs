using System;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ComponentModel;


namespace  BP.Web.Controls
{
	/// <summary>
	/// GenerButton ��ժҪ˵����
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(System.Web.UI.WebControls.LinkButton))]
	public class LinkBtn : System.Web.UI.WebControls.LinkButton
	{
		public enum LinkBtnType
		{
			Normal,			 
			Confirm,
			Save,
			Search,
		    Cancel,
			Delete,
			Update,
			Insert,
			Edit,
			New,
			View,
			Close,
			Export,
			Print,
			Add,
			Reomve
		}		
		private LinkBtnType _ShowType=LinkBtnType.Normal;
		public LinkBtnType ShowType
		{
			get
			{
				return _ShowType;
			}
			set
			{
				this._ShowType=value;
			}
		}
		private string _Hit=null;
		/// <summary>
		/// ��ʾ��Ϣ��
		/// </summary>
		public string  Hit
		{
			get
			{ 
				return _Hit;
			}
			set
			{
				this._Hit=value;
			}
		}
		public LinkBtn()
		{	
			this.CssClass="LinkBtn"+WebUser.Style;
			this.PreRender += new System.EventHandler(this.LinkBtnPreRender);
		}
		private void LinkBtnPreRender( object sender, System.EventArgs e )
		{
			if (this.Hit!=null)
				this.Attributes["onclick"] = "javascript: return confirm('�Ƿ������'); ";

			switch (this.ShowType )
			{
				case LinkBtnType.Edit :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�޸�(E)";
					if (this.AccessKey==null) 	
						this.AccessKey="e";
					break;
				case LinkBtnType.Close :
					if (this.Text==null || this.Text=="") 			 
				        this.Text="�ر�(Q)";
					if (this.AccessKey==null) 	
						this.AccessKey="q";					 
					break;
				case LinkBtnType.Cancel :
					if (this.Text==null || this.Text=="") 			 
						this.Text="ȡ��(C)";
					if (this.AccessKey==null) 	
						this.AccessKey="c";
					break;				��
				case LinkBtnType.Confirm :
					if (this.Text==null || this.Text=="")
						this.Text="ȷ��(O)";
					if (this.AccessKey==null)
						this.AccessKey="o";
				    break;
				case LinkBtnType.Search :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(F)";
					if (this.AccessKey==null)
						this.AccessKey="f";
					break;
				case LinkBtnType.New :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�½�(N)";
					if (this.AccessKey==null) 
						this.AccessKey="n";
					break;
				case LinkBtnType.Delete :
					if (this.Text==null || this.Text=="") 			 
						this.Text="ɾ��(D)";
					if (this.AccessKey==null)
						this.AccessKey="c";
					if (this.Hit==null)
					    this.Attributes["onclick"] = " return confirm('�˲���Ҫִ��ɾ�����Ƿ������');";
					else
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ��ɾ����["+this.Hit+"]���Ƿ������');";

					break;
				case LinkBtnType.Export :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(G)";
					if (this.AccessKey==null) 	
						this.AccessKey="g";
					break;
				case LinkBtnType.Insert :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(I)";
					if (this.AccessKey==null) 	
						this.AccessKey="i";
					break ;
				case LinkBtnType.Print :
					if (this.Text==null || this.Text=="") 			 
						this.Text="��ӡ(P)";
					if (this.AccessKey==null) 	
						this.AccessKey="p";

					if (this.Hit==null)
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ�д�ӡ���Ƿ������');";
					else
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ�д�ӡ��["+this.Hit+"]���Ƿ������');";
					break ;
				case LinkBtnType.Save :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(S)";
					if (this.AccessKey==null)
						this.AccessKey="s";
					break;
				case LinkBtnType.View:
					if (this.Text==null || this.Text=="") 			 
						this.Text="���(V)";
					if (this.AccessKey==null) 	
						this.AccessKey="v";
					break;
				case LinkBtnType.Add:
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(A)";
					if (this.AccessKey==null) 	
						this.AccessKey="a";
					break;
				case LinkBtnType.Reomve:
					if (this.Text==null || this.Text=="") 			 
						this.Text="�Ƴ�(M)";
					if (this.AccessKey==null) 	
						this.AccessKey="m";

					if (this.Hit==null)
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ���Ƴ����Ƿ������');";
					else
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ���Ƴ���["+this.Hit+"]���Ƿ������');";
					break;
				default:
					if (this.Text==null || this.Text=="")
						this.Text="ȷ��(O)";
					if (this.AccessKey==null)
						this.AccessKey="o";
					break; 
			}		 
			
			this.PublicScheme();			 
			this.StyleScheme();	
		}	
		private void PublicScheme()
		{
			if (this.Text==null || this.Text=="") 
			{
				this.Text="ȷ��(O)";
			}
			this.BorderStyle=BorderStyle.Ridge;		 
			//this.Font.Name="��������";
			//this.BorderWidth=Unit.Pixel(1); 
		}
		
		public void StyleScheme()
		{
			//this.BorderStyle=BorderStyle="Ridge"
			if (WebUser.Style=="1")
				this.Style1();
			else if (WebUser.Style=="2")
				this.Style2();
			else  
				this.Style3();

		}
		public void Style3()
		{
			this.BorderColor=Color.Transparent;
			this.BackColor=Color.FromName("#006699");
			this.ForeColor=Color.White;			 
		}
		public void Style2()
		{
			this.BorderColor=System.Drawing.Color.FromName("#DEBA84");
			this.BackColor=Color.FromName("#DEBA84");
			this.ForeColor=Color.Black;
		}
		/// <summary>
		/// Style1
		/// </summary>
		public void Style1()
		{ 
			 		 			 
		}
	}
}
