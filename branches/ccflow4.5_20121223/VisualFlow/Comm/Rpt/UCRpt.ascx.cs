namespace BP.Web.Comm.Rpt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BP.Rpt;
	using BP.En;
	using BP.DA;
	using BP.Sys;
	using BP.Web.Controls;
	using BP.Web.UC;

	/// <summary>
	/// UCRpt2D ��ժҪ˵����
	/// </summary>
	public partial class UCRpt : UCGraphics
	{

		#region 
		public void RptTemplateSet(Rpt3D rpt, RptTemplate rt)
		{
            //RptTemplates rts = new RptTemplates(rt.RptName);

            //this.Controls.Clear(); // cellpadding='0' cellspacing='0'
            //this.Add("<table border='1' class='OpTable'   >");
            //// ��ѡ����
            //this.Add("<TR>");
            //this.Add("<TD class='OpLeftTD' >��ѡ����</TD>");			
            //this.Add("<TD>");

            //DDL ddl = new DDL();
            //ddl.BindEntities(rts,RptTemplateAttr.OID, RptTemplateAttr.RptDesc);
            //this.Add(ddl);

            //this.Add("</TD>");
            //this.AddTREnd();

            //// ��������
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >��������</TD>");
            //this.Add("<TD>");

            //int i=0;
            //foreach(AnalyseObj ao in rpt.HisAnalyseObjs)
            //{
            //    CheckBox cb1 = new CheckBox();
            //    cb1.ID="CB_AO_"+i.ToString();
            //    cb1.Text=ao.DataProperty;

            //    if (rt.AlObjs.IndexOf(ao.DataProperty+"@")!=-1)
            //    {
            //        cb1.Checked=true;
            //    }
            //    this.Add(cb1);
            //    i++;
            //}
            //this.Add("</TD>");
            //this.AddTREnd();


            //#region ����ָ��
            //// ����ָ��
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >��ָ��</TD>");
            //this.Add("<TD>");

            //ddl = new DDL();
            //ddl.ID="DDL_D1";
            //foreach(Attr attr in rpt.DAttrs)
            //{
            //    ddl.Items.Add( new ListItem(attr.Desc, attr.Key) );
            //}
            //ddl.SetSelectItem(rt.D1);
            //this.Add(ddl);

            //this.Add("</TD>");

            //// ����ָ��2
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >��ָ��1</TD>");
            //this.Add("<TD>");

            //ddl = new DDL();
            //ddl.ID="DDL_D2";
            //foreach(Attr attr in rpt.DAttrs)
            //{
            //    ddl.Items.Add( new ListItem(attr.Desc, attr.Key) );
            //}
            //this.Add(ddl);
            //ddl.SetSelectItem(rt.D2);

            //this.Add("</TD>");
            //this.AddTREnd();

            //// ����ָ��3
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >��ָ��2</TD>");
            //this.Add("<TD>");
            //ddl = new DDL();
            //ddl.ID="DDL_D3";
            //foreach(Attr attr in rpt.DAttrs)
            //{
            //    ddl.Items.Add( new ListItem(attr.Desc, attr.Key) );
            //}
            //ddl.SetSelectItem(rt.D3);
            //this.Add(ddl);
            //this.Add("</TD>");
            //this.AddTREnd();
            //#endregion

            //// ������ʾ 
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >������ʾ</TD>");
            //this.Add("<TD>");

            //RadioBtn rb = new RadioBtn();
            //rb.GroupName="RB_Rate";
            //rb.ID="RB_Rate0";
            //rb.Text="����ʾ";
            //if (rt.PercentModel==PercentModel.None)
            //    rb.Checked=true;
            //this.Add(rb);


            //rb = new RadioBtn();
            //rb.ID="RB_Rate1";
            //rb.GroupName="RB_Rate";
            //rb.Text="�����";
            //if (rt.PercentModel==PercentModel.Transverse)
            //    rb.Checked=true;
            //this.Add(rb);

            //rb = new RadioBtn();
            //rb.ID="RB_Rate2";
            //rb.GroupName="RB_Rate";
            //rb.Text="�����";
            //if (rt.PercentModel==PercentModel.Vertical)
            //    rb.Checked=true;
            //this.Add(rb);

 
            //this.Add("</TD>");
            //this.AddTREnd();

            //// �ϼ���ʾ 
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >�ϼ���ʾ</TD>");
            //this.Add("<TD>");

            //CheckBox cb = new CheckBox();
            //cb.ID="CB_BigSum";
            //cb.Text="��ϼ�";
            //cb.Checked=rt.IsSumBig;
            //this.Add(cb);

            //cb = new CheckBox();
            //cb.ID="CB_BigLittle";
            //cb.Text="С�ϼ�";
            //cb.Checked=rt.IsSumLittle;
            //this.Add(cb);

            //cb = new CheckBox();
            //cb.ID="CB_BigRight";
            //cb.Text="�Һϼ�";
            //cb.Checked=rt.IsSumRight;
            //this.Add(cb);

            //this.Add("</TD>");
            //this.AddTREnd();

            //// ͼƬ����
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >ͼƬ����</TD>");
            //this.Add("<TD>");
			
            //this.Add("���");
            //TextBox tb1 = new TextBox();
            //tb1.ID="TB_Width";
            //tb1.Columns=4;
            //tb1.Text=rt.Width.ToString();
            //this.Add(tb1);

            //this.Add("�߶�");
            //tb1 = new TextBox();
            //tb1.ID="TB_Height";
            //tb1.Columns=4;
            //tb1.Text=rt.Height.ToString();
            //this.Add(tb1);
 
            //this.Add("</TD>");
            //this.AddTREnd();

            //// ��ع���
            //this.Add("<TR>");
            //this.Add("<TD  class='OpLeftTD' >��ع���</TD>");
            //this.Add("<TD>");

            //this.Add("[<a href='../PanelEns.aspx?EnsName="+rpt.HisEns.ToString()+"'>�����ѯ</a>]");
            //this.Add("[<a href='../UIEnsCols.aspx?EnsName="+rpt.HisEns.ToString()+"'>ѡ���в�ѯ</a>]");
            //this.Add("[<a href='../GroupEnsMNum.aspx?EnsName="+rpt.HisEns.ToString()+"'>���ݷ���</a>]");
            //this.Add("</TD>");
            //this.AddTREnd();

            //// btn 
            //this.Add("<TR>");
            //this.Add("<TD colspan='2'  align='right' >");

            //Btn btn = new Btn();
            //btn.ID="Btn_Output";
            //btn.Text=" �� �� ";
            //btn.Click+=new EventHandler(btn_Click);
            //this.Add(btn);
            //this.Add("&nbsp;&nbsp;");

            //btn = new Btn();
            //btn.ID="Btn_Print";
            //btn.Text=" �� ӡ ";
            //btn.Click+=new EventHandler(btn_Click);
            //this.Add(btn);
            //this.Add("&nbsp;&nbsp;");


            //btn = new Btn();
            //btn.ID="Btn_App";
            //btn.Text=" Ӧ �� ";
            //btn.Click+=new EventHandler(btn_Click);
            //this.Add(btn);
            //this.Add("&nbsp;&nbsp;");


            //this.Add("</TD>");
            //this.AddTREnd();

            //this.Add("</Table>");
		}
		private void btn_Click(object sender, EventArgs e)
		{

		}

		public RptTemplate RptTemplateGet()
		{
			return new RptTemplate();
		}
		#endregion 

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}
		/// <summary>
		/// ��ʾ����
		/// </summary>
		/// <param name="rpt">RptEntitiesNoNameWithNum</param>
		/// <param name="numName">numName</param>
		/// <param name="IsShowRate">�Ƿ���ʾ�ٷֱ�</param>
		public void BindRpt(RptEntitiesNoNameWithNum rpt, string numName, bool IsShowRate)
		{

			this.Text="";
			this.Text="<p align=center><b>"+rpt.Title+"</b></p>" ; 
			this.Text+="<hr>";
			this.Text+="<Table align=center border=1 class='Table'>" ;

			#region out ����ͷ. 
			this.Text+="<TR  nowrap>";
			this.Text+="<TD class='D1'>���</TD>";
			this.Text+="<TD class='D1'>����</TD>";
			this.Text+="<TD class='D1'>"+numName+"</TD>";
			if (IsShowRate)
				this.Text+="<TD class='D1'>�ٷֱ�</TD>";

			this.Text+="</TR>";
			#endregion 

			#region out body. 
			decimal f = 0;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				decimal obj =rpt.Rpt1DCells.GetCell(en1.No).valOfDecimal ;
				f +=obj;
				// ���2γ  onmousedown='javascript:OnMousedown(this) '
				this.Text+="<TR   >";
				//this.Text+="<TR   >";
				this.Text+="<TD class='D2'>"+en1.No+"</TD>";
				this.Text+="<TD class='D2'>"+en1.Name+"</TD>";
				this.Text+="<TD class='Cell'>"+obj+"</TD>";
				if (IsShowRate)
					this.Text+="<TD class='Cell'>"+rpt.Rpt1DCells.GetRate(en1.No) +"</TD>";
				this.Text+="</TR>"; 
			}
			this.Text+="</TR>";
			this.Text+="<TR><tr><td   valign='top' colspan='2'class='D1' >�ϼ�</td>";

			
			this.Text+="<td>"+f.ToString("0.00")+"</td>";
			if (IsShowRate)
				this.Text+="<TD class='Cell'>--</TD>";

			this.Text+="</TR>";

			#endregion 

			this.Text+="</Table>";

			this.Text+="<p align=center>���ߣ�"+rpt.Author+"  ����:"+DA.DataType.CurrentDataTime+"</p>";
			this.ParseControl();
			//this.Response.Write(this.Text);
		}
		/// <summary>
		/// ����Bind��
		/// </summary>
		/// <param name="rpt">3γ������ʵ�塣</param>
		/// <param name="IsShowRate">�Ƿ���ʾ�ٷֱ�</param>
		/// <param name="isShowRightSum">�Ƿ���ʾ�ұߵĺϼơ�</param>
		/// <param name="isShowBottomSum">�Ƿ���ʾ�ϼ�</param>
		/// <param name="isShowLittleSum">�Ƿ���ʾС��</param>
		public void BindRpt(Rpt3DEntity rpt, PercentModel pm, bool isShowRightSum, bool isShowBottomSum, bool isShowLittleSum)
		{
			string percentName="";
			if (pm==PercentModel.Transverse)
				percentName="�����";
			else
				percentName="�����";

			//this.Text="";
			//this.Text="<p align=center><b>")+rpt.Title+"</b></p>\n";
			//this.Text+="<hr>\n";

			this.AddTable();

			//this.Text+="<Table align=left border=1 class='Table' style='border-collapse: collapse' bordercolor='#111111' >") ;

			//this.Text+="<Table align=center border=1 class='Table'>\n" ;

			#region out ����ͷ.
			int rowspan=1;
			if ( pm!= PercentModel.None && isShowBottomSum) 
				rowspan=2; 
			
			this.Add("<TR class='D1'  nowrap>\n");
			this.Add("<TD colspan='2' rowspan="+rowspan+" >\n");
			this.Add(rpt.LeftTitle ) ;
			this.Add("</TD>\n" ) ;

			rowspan=1;
            if (pm != PercentModel.None && isShowBottomSum)
                rowspan = 2;
			 
			foreach(EntityNoName en in rpt.SingleDimensionItem1)
			{
				/* γ��1���� */
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >");
				this.Add(rpt.GetD1Context(en) ) ;
				this.Add("</TD>\n" );
			}

			if (pm!= PercentModel.None && isShowBottomSum && isShowRightSum )			 
			{
				/* ���Ҫ��ʾ�ұߵĺ˼ơ�*/
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
                this.Add(this.ToE("Sum","�ϼ�"));
				this.Add("</TD>\n");
			}
			else if ( pm!= PercentModel.None  && isShowBottomSum==false && isShowRightSum)
			{
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
                this.Add(this.ToE("Sum","�ϼ�"));
				this.Add("</TD>\n");

				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
				this.Add(percentName);
				this.Add("</TD>\n");
			}
			else if (isShowRightSum)
			{
				this.Add("<TD class='D1' colspan='"+rowspan+"'  >\n");
                this.Add(this.ToE("Sum","�ϼ�"));
				this.Add("</TD>\n");
			}

			this.Add("</TR>\n" ) ;

			if (pm!= PercentModel.None && isShowBottomSum )
			{
				this.Add("<TR>\n");
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='D1'  >");
					this.Add(rpt.DataProperty);
					this.Add("</TD>\n");
					this.Add("<TD class='D1'  >");
					this.Add( percentName) ;
					this.Add("</TD>\n");
				}

				if (isShowRightSum==true)
				{
					this.Add("<TD class='D1'  >");
					this.Add(rpt.DataProperty);
					this.Add("</TD>\n");

					this.Add("<TD class='D1'  >");
					this.Add(percentName);
					this.Add("</TD>\n");
				}
				this.Add("</TR>\n");
			}
			#endregion 

			#region out ���left��ͷ. 

			foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
			{
				bool isfirst=true;				
				this.Add("<TR >\n"); // ���2γ��

				#region �������2d��Ԫ��Ŀ�ȡ�
				int count=0 ; 
				if (rpt.D2D3RefKey==null)
				{
					/* ���d2,d3 û���κι����� ���Ŀ�Ⱦ��� 3d �ĸ��� */
					count =rpt.SingleDimensionItem3.Count;
				}
				else
				{
					/* ���d2,d3 �й���
					 * �Ϳ��ԣ��ҵ�����ϸ������
					 *  */
					foreach(Entity en in rpt.SingleDimensionItem3)
					{
						if (en.GetValStringByKey(rpt.D2D3RefKey)==en2.No)
						{
							count++;
						}
					}
				}
				if (isShowLittleSum )
				{ 
					/*���Ҫ��ʾ 2ά��Ԫ�� �ϼ�*/
					count=count+1;
				}
				#endregion

				this.Add("  <TD class='D2' rowspan='"+count+"'  >");
				this.Add(rpt.GetD2Context(en2) );
				//this.Add(en2.Name+en2.No; //���2γ�ȡ�
				this.Add("  </TD>\n");
				foreach(EntityNoName en3 in rpt.SingleDimensionItem3)
				{
					if (rpt.D2D3RefKey!=null)
					{
						/* �ж��ǲ���2γ��3γ�ȹ�����*/
						if (en3.GetValStringByKey(rpt.D2D3RefKey)!=en2.No)
							continue;
					}

					#region ���3ά
					// ���3γ
					if (isfirst==false)
						this.Add("<TR>\n" ) ;
					this.Add("  <TD class='D3'  >");
					this.Add(rpt.GetD3Context(en3) ) ; 
					this.Add("  </TD>\n" ) ;

					decimal rightsum=0;
					foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
					{
						this.Add("  <TD class='Cell'  >");
						Rpt3DCell cell =rpt.HisCells.GetCell(en1.No,en2.No,en3.No) ;
						decimal cellVal=cell.valOfDecimal ;  // decimal.Parse( cell.val.ToString() );
						rightsum+=cellVal;
						this.Add( rpt.GetCellContext(en1.No,en2.No,en3.No,rpt.HisADT ) );  //�����Ԫ��
						this.Add("  </TD>\n" );
						if (pm!= PercentModel.None && isShowBottomSum)
						{
							if (cellVal==0)
								this.Add("<TD  class='Rate'  > -- </TD>\n");
							else
							{
								if (pm==PercentModel.Vertical)
								{
									/*�����*/
									this.Add("<TD  class='Rate'  > "+DataType.GetPercent(cellVal,rpt.GetSumByD1(en1.No))+" </TD>\n" );
								}
								else
								{
									/*�����*/
									//this.Add("<TD  class='Rate' > "+DataType.GetPercent(mysum,rpt.GetSumByD2(en1.No))+" </TD>\n";
									//this.Add("<TD  class='Rate' >  ����� </TD>\n";
									this.Add("<TD  class='Rate'  > "+DataType.GetPercent( cellVal,rpt.GetSumByD2D3(en2.No,en3.No) )+" </TD>\n" );
								}
							}
						}
					}
					#endregion

					#region �����ʾ right �ϼ�
					if (isShowRightSum)
					{
						this.Add("<TD class='RightSum'>\n" );

						switch(rpt.HisADT)
						{
							case AnalyseDataType.AppFloat:
								this.Add(rightsum.ToString() ) ;					 
								break;
							case AnalyseDataType.AppInt:
								this.Add(rightsum.ToString("0") );
								break;
							case AnalyseDataType.AppMoney:
								this.Add(rightsum.ToString("0.00") );
								break;
							default:
								break;
						}

						this.Add("</TD>\n");

						if ( pm!= PercentModel.None)
						{
							if (rightsum==0)
							{
								this.Add("<TD class='RightSum'   > -- </TD>\n" );
							}
							else
							{
								if (pm==PercentModel.Vertical)
								{
									this.Add("<TD class='RightSum'  >"+DataType.GetPercent(rightsum ,rpt.HisSum )+" </TD>\n" ) ;
								}
								else
								{
									this.Add("<TD class='RightSum'  >--</TD>\n");
								}
							}
						}
					}
					#endregion

					if (isfirst)
						isfirst=false;
					this.Add("</TR>\n");
				}  // ����3γ�ȵı�����`


				#region ���С��
				if (isShowLittleSum)
				{
					this.Add("<TR>\n");
					this.Add("<TD class='LittleSum'  >С��</TD>");

					decimal mySum=0 ;
					foreach(EntityNoName en1 in rpt.SingleDimensionItem1 )
					{
						decimal endsum=0;
						foreach(EntityNoName en3 in rpt.SingleDimensionItem3)
						{
							if (rpt.D2D3RefKey!=null)
							{
								/* �ж��ǲ���2γ��3γ�ȹ�����*/
								if (en3.GetValStringByKey(rpt.D2D3RefKey)!=en2.No)
									continue;
							}
							Rpt3DCell cell =rpt.HisCells.GetCell(en1.No,en2.No,en3.No);
							endsum+=decimal.Parse( cell.val.ToString() );
						}

						switch(rpt.HisADT)
						{
							case AnalyseDataType.AppFloat:
								this.Add("<TD class='LittleSum'  >"+endsum+"</TD>");					 
								break;
							case AnalyseDataType.AppInt:
								this.Add("<TD class='LittleSum'  >"+endsum.ToString("0")+"</TD>");
								break;
							case AnalyseDataType.AppMoney:
								this.Add("<TD class='LittleSum'  >"+endsum.ToString("0.00")+"</TD>)" ) ;
								break;
							default:
								break;
						}


						if (isShowBottomSum && pm!= PercentModel.None)
						{
							if (endsum==0)							 
								this.Add("<TD class='LittleSum'  > -- </TD>");							 
							else 
							{
								if (pm==PercentModel.Vertical)
								{
									this.Add("<TD class='LittleSum'  > "+DataType.GetPercent(endsum ,rpt.GetSumByD1(en1.No))+" </TD>\n");
								}
								else if (pm==PercentModel.Transverse)
								{
									//this.Add("<TD class='LittleSum' > "+DataType.GetPercent(endsum ,rpt.GetSumByD2(en1.No))+" </TD>\n";
									this.Add("<TD class='LittleSum'  >  "+DataType.GetPercent(endsum ,rpt.GetSumByD2(en2.No))+"  </TD>\n");
								}
							}
						}

						mySum+=endsum ;
					}
					if ( isShowRightSum )
					{
						/* ��ʾ�ұߵĺϼ� */
						switch(rpt.HisADT)
						{
							case AnalyseDataType.AppFloat:
								this.Add("<TD class='LittleSum'  >"+mySum+"</TD>");					 
								break;
							case AnalyseDataType.AppInt:
								this.Add("<TD class='LittleSum'  >"+mySum.ToString("0")+"</TD>");
								break;
							case AnalyseDataType.AppMoney:
								this.Add("<TD class='LittleSum'   >"+mySum.ToString("0.00")+"</TD>");
								break;
							default:
								break;
						}

						if ( pm!= PercentModel.None )
						{
							/* ��ʾ����.*/
							if ( mySum==0)							 
								this.Add("<TD class='LittleSum' > -- </TD>" ) ;							 
							else
								this.Add("<TD class='LittleSum' > "+DataType.GetPercent(mySum ,rpt.HisSum )+" </TD>");
						}
					}
					this.Add("</TR>\n");
				}
				#endregion ���С�ϼ�

				this.Add("</TR>\n" );
			}

			if (isShowBottomSum)
			{
				// ���2γ.
				this.Add("<TR>" );
				this.Add(" <TD class='D1' colspan='2'  >" );
				this.Add(this.ToE("Sum","�ϼ�") );
				this.Add("</TD>" ) ;
				float x=0;
				float sum=0;
				foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
				{
					x=rpt.HisCells.GetSumByPK1(en1.No);
					sum+=x;
					this.Add("<TD class='BottomSum' >" );

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(x.ToString() );					 
							break;
						case AnalyseDataType.AppInt:
							this.Add(x.ToString("0") );
							break;
						case AnalyseDataType.AppMoney:
							this.Add(x.ToString("0.00") );
							break;
						default:
							break;
					}

					//this.Add(x.ToString();
					this.Add("</TD>" ) ;
					if (pm!= PercentModel.None)
					{
						if (pm==PercentModel.Vertical)
						{
							this.Add("<TD class='BottomSum' > -- </TD>\n" );
						}
						else
						{
							this.Add("<TD class='BottomSum' > "+DataType.GetPercent( (decimal)x, (decimal)rpt.HisCells.Sum ) +" </TD>\n");
						}
					}
				}
				if (isShowRightSum)
				{
					this.Add("<TD class='BottomSum'>" ) ;

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(sum.ToString() ) ;					 
							break;
						case AnalyseDataType.AppInt:
							this.Add(sum.ToString("0") ) ;
							break;
						case AnalyseDataType.AppMoney:
							this.Add(sum.ToString("0.00") ) ;
							break;
						default:
							break;
					}

					//this.Add(sum.ToString();
					this.Add("</TD>" );
					if (pm!= PercentModel.None)
					{
						if (pm==PercentModel.Vertical)
							this.Add("<TD class='BottomSum' > -- </TD>\n");
						else
							this.Add("<TD class='BottomSum' > -- </TD>\n");
					}
				}
				this.AddTREnd();
			}
			#endregion 


			this.Add("</Table>");
			//this.Text+="<hr><p align=left>���ߣ�"+rpt.Author+"  ����:"+DA.DataType.CurrentDataTime+"</p>";
			//this.ParseControl();
		}
		/// <summary>
		/// RptPlanarEntity
		/// </summary>
		/// <param name="rpt">RptPlanarEntity</param>
		/// <param name="IsRowOrder">�Ƿ���ʾ�д�</param>
		/// <param name="isShowLefSum">�Ƿ���ʾ��ߵĺϼ�</param>
		/// <param name="isShowBottomSum">�Ƿ���ʾ�ұߵĺϼ�</param>
		public void BindRpt(RptPlanarEntity rpt,bool IsRowOrder, bool isShowLefSum, bool isShowBottomSum, PercentModel pm )
		{
			string percentName="";
			if (pm==PercentModel.Transverse)
				percentName="�����";
			else
				percentName="�����";

			//IsRowOrder=false;
			//this.Text="";
			//this.Text="<p align=center><b>"+rpt.Title+"</b></p>" ; 
			//this.Add("<hr>";

			this.AddTable();
			//this.Add("<Table id='tb1' align=left border=1 class='Table' style='border-collapse: collapse' bordercolor='#111111' >") ;

			#region out ����ͷ. 
			 
			this.Add("<TR  id='trleft'  nowrap  >");
			if ( pm!=PercentModel.None )
				this.Add("<TD rowspan='2'  >��Ŀ</TD>");
			else
				this.Add("<TD rowspan='1'  >��Ŀ</TD>");

			if (IsRowOrder)
			{
				/* �д� */				
				this.Add("<TD class='D1' nowrap >");
				this.Add("�д�");
				this.Add("</TD>");
			}
			if ( pm!=PercentModel.None )
			{
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='D1' colspan='2'  >");
					this.Add(en.Name);
					this.Add("</TD>");
				}
			}
			else
			{
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='D1'  >");
					this.Add(en.Name);
					this.Add("</TD>");
				}
			}

			if ( pm!=PercentModel.None )
			{
				if (isShowLefSum)
				{
					/* ���Ҫ��ʾ��ߵĺ˼ơ�*/
					this.Add("<TD colspan='2'>");
					this.Add(this.ToE("Sum","�ϼ�"));
					this.Add("</TD>");
				}
			}
			else
			{
				if (isShowLefSum)
				{
					/* ���Ҫ��ʾ��ߵĺ˼ơ�*/
					this.Add("<TD>");
					this.Add(this.ToE("Sum","�ϼ�"));
					this.Add("</TD>");
				}
			}
			this.AddTREnd();
			if ( pm!=PercentModel.None )
			{
				this.Add("<TR>");
				foreach(EntityNoName en in rpt.SingleDimensionItem1)
				{
					//en;
					this.Add("<TD class='D1'   >"+rpt.DataProperty+"</TD>");
					this.Add("<TD class='D1'   >"+percentName+"</TD>");
				}

				this.Add("<TD class='D1' >"+rpt.DataProperty+"</TD>");
				this.Add("<TD class='D1' >"+percentName+"</TD>");
				this.AddTREnd();
			}
			#endregion 

			#region out ����ͷ. 
			int i = 0;
			foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
			{
				/* ���� 2γ�� */
				i++;
				// ���2γ 
				this.Add("<TR>");
				this.Add("<TD class='D2'   >");
				this.Add(en2.Name ) ;
				this.Add("</TD>");

				if (IsRowOrder)
				{
					// �Ƿ���ʾ�д� 					 
					this.Add("<TD class='D2'   >");
					this.Add(i.ToString() ) ;
					this.Add("</TD>");
				}

				foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
				{
					this.Add("<TD class='Cell'  >");
					this.Add(rpt.PlanarCells.GenerHtmlStrBy(rpt.Key1,en1.No, rpt.Key2 ,en2.No,rpt.HisADT) ) ;
					//this.Add(rpt.PlanarCells.GenerHtmlStrBy(en1.No,en2.No);
					this.Add("</TD>");

					if (pm==PercentModel.Vertical)
					{
						/* ����ٷֱ� */
						this.Add("<TD class='Rate'  >"+rpt.PlanarCells.GetRateVertical(en1.No,en2.No).ToString("0.00%")+"</TD>");
					}
					else if (pm==PercentModel.Transverse)
					{
						/* ����ٷֱ� */
						this.Add("<TD class='Rate'  >"+rpt.PlanarCells.GetRateTransverse(en1.No,en2.No).ToString("0.00%")+"</TD>");
					}
				}
				if (isShowLefSum)
				{
					/*�����ʾright�ĺ˼�*/
					float sum =0 ;
					foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
					{
						try
						{
							sum+=float.Parse( rpt.PlanarCells.GetCell(en1.No,en2.No).val.ToString() ) ;
						}
						catch
						{
							//string str=rpt.PlanarCells.GetCell(en1.No,en2.No).val.ToString();
							//	throw new Exception( str  ); 
						}
					}
					this.Add("<TD class='RightSum'   >");

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add( sum.ToString()  ) ;
							break;
						case AnalyseDataType.AppInt:
							this.Add(sum.ToString("0") ) ;	
							break;
						case AnalyseDataType.AppMoney:
							this.Add(sum.ToString("0.00") ) ;	
							break;
						default:
							throw new Exception("error rpt.hisADT");
					}

		 
					this.Add("</TD>");

					if (pm==PercentModel.Vertical)
					{
						/* ����ٷֱ� */
						this.Add("<TD class='RightSum'  >"+rpt.PlanarCells.GetRateByPK2(en2.No).ToString("0.00%")+"</TD>");
					}
					else if (pm==PercentModel.Transverse)
					{
						/* ����ٷֱ� */
						this.Add("<TD class='RightSum'  >--</TD>");
					}
				}
				this.AddTREnd();
			}

			if (isShowBottomSum)
			{
				// ���2γ  onmousedown=\"D2Down('Cell')\"
				this.Add("<TR  >");
				this.Add("<TD class='Sum' >");
				this.Add(this.ToE("Sum","�ϼ�") ) ;
				this.Add("</TD>");
				foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
				{
					float x=0;
					float sumX=0;
					foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
					{
						try
						{
							x+= rpt.PlanarCells.GetCell(en1.No,en2.No).valOfFloat;
						}
						catch
						{

						}
					}
					this.Add("<TD class='Sum'  >");

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(x.ToString() ) ;
							break;
						case AnalyseDataType.AppInt:
							this.Add(x.ToString("0") ) ;	
							break;
						case AnalyseDataType.AppMoney:
							this.Add(x.ToString("0.00") ) ;
							break;
						default:
							throw new Exception("error rpt.hisADT x");
					}

					this.Add("</TD>");

					if (pm == PercentModel.Vertical )
					{
						/* ����Ǻ��� */
						this.Add("<TD  >--</TD>");
					}
					else if (pm==PercentModel.Transverse)
					{
						/* ��������� */
						float cel= x/rpt.PlanarCells.SumOfFloat;
						this.Add("<TD class='RightSum'  >"+cel.ToString("0.00%") +"</TD>");
					}
				}
				if (isShowLefSum)
				{
					this.Add("<TD class='Sum'>");

					switch(rpt.HisADT)
					{
						case AnalyseDataType.AppFloat:
							this.Add(rpt.PlanarCells.SumOfDecimal.ToString() ) ;
							break;
						case AnalyseDataType.AppInt:							
							this.Add(rpt.PlanarCells.SumOfInt.ToString() ) ;
							break;
						case AnalyseDataType.AppMoney:
							this.Add(rpt.PlanarCells.SumOfDecimal.ToString("0.00") );
							break;
					}

					this.Add("</TD>");

					if (pm == PercentModel.Transverse)
					{
						/* ����� */
						this.Add("<TD  >--</TD>");
						//this.Add("<TD   >--</TD>");
					}
					else if (pm == PercentModel.Vertical)
					{
						this.Add("<TD  >--</TD>");
					}
				}
				this.AddTREnd();
			}
			#endregion

			this.Add("</Table>");

			//this.Text+="<p align=center>���ߣ�"+rpt.Author+"  ����:"+DA.DataType.CurrentDataTime+"</p>";
			//this.ParseControl();

			//this.Response.Write(this.Text);
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		
	}
}
