using System;
//using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Web.Controls;
 


namespace BP.Web.Controls
{
	/// <summary>
	/// BPListBox ��ժҪ˵����
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(System.Web.UI.WebControls.Panel))]
	public class BPPanel:System.Web.UI.WebControls.Panel 
	{

		#region ����
		/// <summary>
		/// ��ʼ��map
		/// </summary>
		/// <param name="map">map</param>
		/// <param name="i">ѡ���ҳ</param>
		public void InitByMapV2(Map map,int page) 
		{
			InitByMapV2(true,map.AttrsOfSearch,map.SearchAttrs,null, page);
		}
        public string EnsName
        {
            get
            {
                string val = this.Page.Request.QueryString["EnsName"];
                if (val == null)
                    val = this.Page.Request.QueryString["EnsName"];

                return val;
            }
        }
		public void InitByMapVGroup(Map map)
		{
		 
			//AttrOfSearch
			// ������Բ�ѯ��		
	 
			Attrs attrs =map.SearchAttrs;
			bool isfirst=true;
			foreach(Attr attr in attrs )
			{
				if (attr.MyFieldType==FieldType.RefText)
					continue;

				DDL ddl = new DDL();
				ddl.ID="DDL_"+attr.Key;
				this.Add(ddl);  

				if (attr.MyFieldType==FieldType.Enum)
				{
					this.GetDDLByKey("DDL_"+attr.Key).BindSysEnum(attr.UIBindKey);
					this.GetDDLByKey("DDL_"+attr.Key).Items[0].Text="ѡ��"+attr.Desc;
				}
				else
				{
                    switch (attr.UIBindKey)
                    {
                        case "BP.Port.Depts":
                            BP.Port.Depts Depts = new BP.Port.Depts();
                            Depts.RetrieveAll();
                            foreach (Entity zsjg in Depts)
                            {
                                ListItem li = new ListItem(zsjg.GetValStringByKey("Name"), zsjg.GetValStringByKey("No"));
                                this.GetDDLByKey("DDL_" + attr.Key).Items.Add(li);
                            }
                            break;
                        default:
                            this.GetDDLByKey("DDL_" + attr.Key).SelfBind();
                            this.GetDDLByKey("DDL_" + attr.Key).Items[0].Text = "ѡ��" + attr.Desc;
                            break;
                    }
				}

				if (isfirst)
				{

					isfirst=false;
				}
			}
			this.Add("��ʽ");
			DDL ddl1 = new DDL();
			ddl1.ID="DDL_ShowType";
			this.Add(ddl1);
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("����","rpt") );
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("��״ͼ","a") );
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("����ͼ","b") );
			this.GetDDLByKey("DDL_ShowType").Items.Add( new ListItem("��ͼ","c") );

			Btn btn= new Btn();
			btn.ID="Btn_Search";
            btn.Text = this.ToE("Search","��ѯ");
			this.Add(btn);



			
			//this.AddBtn("Btn_Save","����Ϊģ��");

			 

		}
        public string ToE(string no,string chval)
        {
            return BP.Sys.Language.GetValByUserLang(no,chval);
        }
		/// <summary>
		/// ����Map, ����һ��ToolBar
		/// </summary>
		/// <param name="map"></param>
        public void InitByMapV2(bool isShowKey, AttrsOfSearch attrsOfSearch, AttrSearchs attrsOfFK, Attrs attrD1, int page) 
		{
			int keysNum=0;

			// �ؼ��֡�
			if (isShowKey)
			{
				this.Add(BP.Sys.Language.GetValByUserLang("Key","�ؼ���") );
				TB tb = new TB();
				tb.ID = "TB_Key";
				tb.Columns=9;
				this.Add(tb);
				keysNum++;
			}

			//			BP.Sys.Operators ops = new BP.Sys.Operators();
			//			ops.RetrieveAll();

			// ��������ԡ�			
			foreach(AttrOfSearch attr in  attrsOfSearch )
			{
				if (attr.IsHidden)
					continue;

				this.Add(attr.Lab);
				keysNum++;
				if (keysNum==3 || keysNum==6 || keysNum==9)
					this.Add("<BR>");


				if (attr.SymbolEnable==true)
				{
					DDL ddl = new DDL();
					ddl.ID = "DDL_"+attr.Key ;		 
					ddl.SelfShowType =DDLShowType.Ens ; //  attr.UIDDLShowType;		 
					ddl.SelfBindKey="BP.Sys.Operators" ; 
					ddl.SelfEnsRefKey ="No";
					ddl.SelfEnsRefKeyText ="Name";
					ddl.SelfDefaultVal =attr.DefaultSymbol;
					ddl.SelfAddAllLocation =AddAllLocation.None ; 
					ddl.SelfIsShowVal = false; ///������ʾ���
					ddl.ID="DDL_"+attr.Key;
					//ddl.SelfBind();
					this.Add(ddl);
					this.GetDDLByKey("DDL_"+attr.Key).SelfBind();
				}


				TB tb = new TB();
				tb.ID = "TB_"+attr.Key;
				tb.Text = attr.DefaultVal;
				tb.Columns=attr.TBWidth ; 
				this.Add(tb);
			}

			// ������Բ�ѯ��			 
			bool isfirst=true;
			foreach(Attr attr in attrsOfFK)
			{
				if (attr.MyFieldType==FieldType.RefText)
					continue;
				DDL ddl  = new DDL();
				ddl.ID="DDL_"+attr.Key;
				ddl.SelfAddAllLocation=AddAllLocation.TopAndEnd;
				this.Add( ddl );

				keysNum++;
				if (keysNum==3 || keysNum==6 || keysNum==9)
					this.Add("<BR>");

                if (attr.MyFieldType == FieldType.Enum)
                {
                    this.GetDDLByKey("DDL_" + attr.Key).BindSysEnum(attr.UIBindKey);
                    this.GetDDLByKey("DDL_" + attr.Key).Items[0].Text = "ѡ��" + attr.Desc;
                }
                else
                {
                    // �����е���Ϣ��Bind.
                    switch (attr.UIBindKey)
                    {
                        case "BP.Port.Depts":
                            BP.Port.Depts Depts = new BP.Port.Depts();
                            Depts.RetrieveAll();
                            foreach (Entity zsjg in Depts)
                            {
                                ListItem li = new ListItem(zsjg.GetValStringByKey("Name"), zsjg.GetValStringByKey("No"));
                                this.GetDDLByKey("DDL_" + attr.Key).Items.Add(li);
                            }
                            break;
                        default:
                            this.GetDDLByKey("DDL_" + attr.Key).SelfBind();
                            this.GetDDLByKey("DDL_" + attr.Key).Items[0].Text = "ѡ��" + attr.Desc;
                            break;
                    }
                }

				if (isfirst)
				{
					isfirst=false;
				}
			}
			Btn btn = new Btn();
			btn.ID="Btn_Search";
            btn.Text = this.ToE("Search", "��ѯ");
			this.Add( btn );

			DDL ddlPage = new DDL();
			ddlPage.ID = "DDL_Page";
			ddlPage.Items.Add(new ListItem("1","1"));
	
			this.Add(ddlPage);
			Btn btn1 = new Btn();
			btn1.ID="Btn_Go";
			btn1.Text="ת��";
			this.Add( btn1 );
		}

		/// <summary>
		/// �õ�һ��QueryObject
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="en"></param>
		/// <returns></returns>
		public QueryObject InitQueryObjectByEns(Entities ens,Entity en)
		{
			#region �ؼ���
			string keyVal="%"+this.GetTBByKey("TB_Key").Text.Trim()+"%";
			//int top=int.Parse(this.GetTBByKey("TB_Top").Text.Trim());
			QueryObject qo = new QueryObject(ens);
			//qo.Top=top;
			Attrs attrs = en.EnMap.Attrs;
            if (keyVal.Length > 2)
            {
                qo.addLeftBracket();
                string pk = ens.GetNewEntity.PK;
                if (pk == "OID" || pk.ToLower()=="workid" )
                    qo.AddHD_Not();
                else
                    qo.AddWhere(ens.GetNewEntity.PK, " LIKE ", "%"+keyVal+"%");

                foreach (Attr attr in attrs)
                {
                    if (attr.Key == "OID")
                        continue;

                    if (attr.MyDataType != DataType.AppString)
                        continue;

                    if (attr.MyFieldType == FieldType.RefText)
                        continue;

                    if (attr.UIContralType == UIContralType.DDL || attr.UIContralType == UIContralType.CheckBok)
                    {
                        if (attr.Key != "FK_Taxpayer")
                            continue;
                    }

                    qo.addOr();
                    qo.AddWhere(attr.Key, " LIKE ", keyVal);
                }
                qo.addRightBracket();
            }
            else
            {
                qo.addLeftBracket();
                qo.AddWhere("abc", "all");
                qo.addRightBracket();
            }
			#endregion

			#region ��ͨ����
			AttrsOfSearch attrsOfSearch = en.EnMap.AttrsOfSearch;

			string opkey=""; // �������š�
			foreach(AttrOfSearch attr in attrsOfSearch)
			{
				if (attr.IsHidden)
				{
					qo.addAnd();
					qo.addLeftBracket();
					qo.AddWhere(attr.RefAttrKey, attr.DefaultSymbol , attr.DefaultVal  ) ;
					qo.addRightBracket();
					continue;				
				}
				if (attr.SymbolEnable==true)
				{
					opkey=this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal;
					if (opkey=="all")
						continue;
				}
				else
				{
					opkey=attr.DefaultSymbol;
				}
 
				qo.addAnd();
				qo.addLeftBracket();
				qo.AddWhere(attr.RefAttrKey, opkey , this.GetTBByKey("TB_"+attr.Key).Text  ) ;
				qo.addRightBracket();
			}
			#endregion

			#region ���
			Attrs searchAttrs = en.EnMap.SearchAttrs;
			foreach(Attr attr in searchAttrs)
			{
				if (attr.MyFieldType==FieldType.RefText) 
					continue;

				qo.addAnd();
				qo.addLeftBracket();

				if (attr.UIBindKey=="BP.Port.Depts")  //�ж����������
					qo.AddWhere(attr.Key, " LIKE ", this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal+"%" ) ;
				else
					qo.AddWhere(attr.Key,this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal ) ;

				//qo.AddWhere(attr.Key,this.GetDDLByKey("DDL_"+attr.Key).SelectedItemStringVal ) ;
				qo.addRightBracket();
			}
			#endregion.

			return qo;

		}
		public int recordConut=0;

		public QueryObject InitTableByEnsV2OfOrcale(Entities ens,Entity en,int pageSize,int page)
		{
			QueryObject qo =this.InitQueryObjectByEns(ens,en);
			this.recordConut=qo.GetCount();
			int pageCount=recordConut/pageSize;
			int myleftCount= recordConut-(pageCount*pageSize);
			pageCount++;
			int top=pageSize*(page-1);
			this.GetDDLByKey("DDL_Page").Items.Clear();
			for(int i=1;i<=pageCount;i++)
			{
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem(i.ToString(),i.ToString()));
			}
			if (pageCount==0)
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem("1","1"));

			if (page==1)
			{
				qo.Top=pageSize;
				return qo;
			}

			

			this.GetDDLByKey("DDL_Page").SetSelectItemByIndex(page-1);
			string pk=en.PK;

			string wheresql=qo.SQL;
			string sql="";
 
			qo.addLeftBracket();

			qo.AddWhere("ROWID", " > " ,pageSize*page +pageSize  );
			qo.addAnd();
			qo.AddWhere("ROWID", " < " ,pageSize*page );

			qo.addRightBracket();



			qo.addAnd();

			qo.AddWhereNotInSQL(pk,sql);

			qo.Top=pageSize;

			//Log.DefaultLogWriteLineInfo(qo.SQL);

			return qo;
		}

		public QueryObject InitTableByEnsV2(Entities ens,Entity en,int pageSize,int page)
		{
			if (en.EnMap.EnDBUrl.DBType==DBType.Oracle9i)
				return this.InitTableByEnsV2OfOrcale(ens,en, pageSize,page);

			QueryObject qo =this.InitQueryObjectByEns(ens,en);
			this.recordConut=qo.GetCount();
			int pageCount=recordConut/pageSize;
			int myleftCount= recordConut-(pageCount*pageSize);

			pageCount++;

			int top=pageSize*(page-1);
			this.GetDDLByKey("DDL_Page").Items.Clear();

			for(int i=1;i<=pageCount;i++)
			{
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem(i.ToString(),i.ToString()));
			}
			if (pageCount==0)
				this.GetDDLByKey("DDL_Page").Items.Add(new ListItem("1","1"));


			if (page==1)
			{
				qo.Top=pageSize;
				return qo;
			}

			

			this.GetDDLByKey("DDL_Page").Items[page-1].Selected=true;
			string pk=en.PK;

			string wheresql=qo.SQL;
			string sql="";
			switch(DBAccess.AppCenterDBType)
			{
				case DBType.Oracle9i:
					sql="SELECT top "+top+" "+en.PKField+" FROM "+en.EnMap.PhysicsTable+" "+wheresql.Substring(wheresql.IndexOf("WHERE (1=1)")) ;
					break;
				default:
					sql="SELECT top "+top+" "+en.PKField+" FROM "+en.EnMap.PhysicsTable+" "+wheresql.Substring(wheresql.IndexOf("WHERE (1=1)")) ;
					break;
			}

			qo.addAnd();
			qo.AddWhereNotInSQL(pk,sql);
			qo.Top=pageSize;
			//Log.DefaultLogWriteLineInfo(qo.SQL);
			return qo;
		}

		#endregion


		#region  ���õ�
		public void Add(Control ctl)
		{
			this.Controls.Add(ctl);
		}
		public void Add(string html)
		{
			this.Controls.Add( new LiteralControl(html+"\n") );
		}
		public DDL GetDDLByKey(string key)
		{
			return (DDL)this.FindControl(key);
		}
		public TB GetTBByKey(string key)
		{
			return (TB)this.FindControl(key);
		}
		public Btn GetBtnByKey(string key)
		{
			return (Btn)this.FindControl(key);
		}
		#endregion 

		public BPPanel()
		{
			this.CssClass ="BPPanel"+WebUser.Style; 
			this.PreRender += new System.EventHandler(this.TBPreRender);
		}
		private void TBPreRender( object sender, System.EventArgs e )
		{
			#region
			#endregion 
		}
		/// <summary>
		/// ����һ�����ӡ�
		/// </summary>
		/// <param name="text"></param>
		/// <param name="url"></param>
		/// <param name="target"></param>
		public void AddHyperLink(string text, string url, string target)
		{
			BPHyperLink hl = new BPHyperLink();
			hl.Text=text;
			hl.NavigateUrl=url;
			hl.Target=target;
			this.Controls.Add(hl) ; 
		}

		public void AddBtn(string id)
		{
			string text="" ;
			switch(id)
			{
				case NamesOfBtn.DataGroup:
					text="�����ѯ";
					break;	
				case NamesOfBtn.Copy:
					text="����";
					break;	
				case NamesOfBtn.Go:
					text="ת��";
					break;	
				case NamesOfBtn.ExportToModel:
					text="ģ��";
					break;	
				case NamesOfBtn.DataCheck:
					text="���ݼ��";
					break;					
				case NamesOfBtn.DataIO:
					text="���ݵ���";
					break;
				case NamesOfBtn.Statistic:
					text="ͳ��";
					break;
				case NamesOfBtn.Balance:
					text="��ƽ";
					break;
				case NamesOfBtn.Down:
					text="�½�";
					break;
				case NamesOfBtn.Up:
					text="����";
					break;
				case NamesOfBtn.Chart:
					text="ͼ��";
					break;
				case NamesOfBtn.Rpt:
					text="����";
					break;
				case NamesOfBtn.ChoseCols:
					text="ѡ���в�ѯ";
					break;
				case NamesOfBtn.Excel:
					text="����";
					break;
				case NamesOfBtn.Xml:
					text="������Xml";
					break;				 
				case NamesOfBtn.Send:
					text="����";
					break;
				case NamesOfBtn.Reply:
					text="�ظ�";
					break;
				case NamesOfBtn.Forward:
					text="ת��";
					break;
				case NamesOfBtn.Next:
					text="��һ��";
					break;
				case NamesOfBtn.Previous:
					text="��һ��";
					break;
				case NamesOfBtn.Selected:
					text="ѡ��";
					break;
				case NamesOfBtn.Add:
					text="����";
					break;
				case NamesOfBtn.Adjunct:
					text="����";
					break;
				case NamesOfBtn.AllotTask:
					text="��������";
					break;
				case NamesOfBtn.Apply:
					text="����";
					break;
				case NamesOfBtn.ApplyTask:
					text="��������";
					break;
				case NamesOfBtn.Back:
					text="����";
					break;
				case NamesOfBtn.Card:
					text="��Ƭ";
					break;
				case NamesOfBtn.Close:
					text="�ر�";
					break;
				case NamesOfBtn.Confirm:
					text="ȷ��";
					break;
				case NamesOfBtn.Delete:
					text="ɾ��";
					break;
				case NamesOfBtn.Edit:
					text="�༭";
					break;
				case NamesOfBtn.EnList:
					text="�б�";
					break;
				case NamesOfBtn.Cancel:
					text="ȡ��";
					break;
				case NamesOfBtn.Export:
					text="����";
					break;
				case NamesOfBtn.FileManager:
					text="�ļ�����";
					break;
				case NamesOfBtn.Help:
					text="����";
					break;
				case NamesOfBtn.Insert:
					text="����";
					break;
				case NamesOfBtn.LogOut:
					text="ע��";
					break;
				case NamesOfBtn.Messagers:
					text="��Ϣ";
					break;
				case NamesOfBtn.New:
					text="�½�";
					break;
				case NamesOfBtn.Print:
					text="��ӡ";
					break;
				case NamesOfBtn.Refurbish:
					text="ˢ��";
					break;
				case NamesOfBtn.Reomve:
					text="�Ƴ�";
					break;
				case NamesOfBtn.Save:
					text="����";
					break;
				case NamesOfBtn.SaveAndClose:
					text="���沢�ر�";
					break;
				case NamesOfBtn.SaveAndNew:
					text="���沢�½�";
					break;
				case NamesOfBtn.SaveAsDraft:
					text="����ݸ�";
					break;
				case NamesOfBtn.Search:
					text="����";
					break;
				case NamesOfBtn.SelectAll:
					text="ѡ��ȫ��";
					break;			 
				case NamesOfBtn.SelectNone:
					text="��ѡ";
					break;
				case NamesOfBtn.View:
					text="�鿴";
					break;
				case NamesOfBtn.Update:
					text="����";
					break;
				default:
					throw new Exception("@û�ж���ToolBarBtn ��� "+id);
			}
			Btn en = new Btn();
	 
			en.ID=id;
			en.Text=text;
			this.Add(en);		
		}

		public void AddHtml(string html)
		{
			this.Controls.Add( new LiteralControl( html) ); 
		}


		
		 

	}
	
}
