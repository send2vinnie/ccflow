namespace BP.Web.UC
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Data.SqlClient;
	using System.Data.Odbc ;
	using System.Drawing;
	using System.Web;
	using System.Web.SessionState;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BP.Rpt ; 
	using BP.En;
	using BP.DA;
    //using Microsoft.Office;
    //using Microsoft.Office.Interop;
    //using Microsoft.Web.UI.WebControls;
    using Microsoft.Office.Interop.Owc11;
    //using OWC10;

	/// <summary>
	///		UCGraphics ��ժҪ˵����
	/// </summary>
	public partial class UCGraphics : UCBase
	{

		#region 3 d
		public void BindPie(Rpt3DEntity rpt , int chartWidth,int chartHeight)
		{
			Bind(rpt,ChartChartTypeEnum.chChartTypePie3D, chartWidth,chartHeight);
 		}
		public void BindLine(Rpt3DEntity rpt , int chartWidth,int chartHeight)
		{
			Bind(rpt,ChartChartTypeEnum.chChartTypeLineStacked, chartWidth,chartHeight);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rpt"></param>
		/// <param name="chartWidth"></param>
		/// <param name="chartHeight"></param>
		public void BindHistogram(Rpt3DEntity rpt , int chartWidth,int chartHeight)
		{
			//Bind(rpt,ChartChartTypeEnum.chChartTypeColumn3D, chartWidth,chartHeight);
			//Bind(rpt,ChartChartTypeEnum.chChartTypeColumnStacked, chartWidth,chartHeight);
			//Bind(rpt,ChartChartTypeEnum.chChartTypeColumnStacked100, chartWidth,chartHeight);
			Bind(rpt,ChartChartTypeEnum.chChartTypeColumnClustered, chartWidth,chartHeight);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colOfGroupField"></param>
        /// <param name="colOfGroupName"></param>
        /// <param name="colOfNumField"></param>
        /// <param name="colOfNumName"></param>
        /// <param name="title"></param>
        /// <param name="chartHeight"></param>
        /// <param name="chartWidth"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static string GenerChart(DataTable dt, string colOfGroupField, string colOfGroupName,
            string colOfNumField, string colOfNumName, string title, int chartHeight, int chartWidth, ChartType ct)
        {
            string strCategory = ""; // "1" + '\t' + "2" + '\t' + "3" + '\t'+"4" + '\t' + "5" + '\t' + "6" + '\t';
            string strValue = ""; // "9" + '\t' + "8" + '\t' + "4" + '\t'+"10" + '\t' + "12" + '\t' + "6" + '\t';
            //��������
            ChartSpace ThisChart = new ChartSpaceClass();
            ChChart ThisChChart = ThisChart.Charts.Add(0);
            ChSeries ThisChSeries = ThisChChart.SeriesCollection.Add(0);

            //��ʾͼ��
            ThisChChart.HasLegend = true;
            //����
            ThisChChart.HasTitle = true;
            ThisChChart.Title.Caption = title;

            //����x,y��ͼʾ˵��
            ThisChChart.Axes[0].HasTitle = true;
            ThisChChart.Axes[1].HasTitle = true;

            ThisChChart.Axes[0].Title.Caption = colOfGroupName;
            ThisChChart.Axes[1].Title.Caption = colOfNumName;

            switch (ct)
            {
                case ChartType.Histogram:
                    foreach (DataRow dr in dt.Rows)
                    {
                        strCategory += dr[colOfGroupField].ToString() + '\t';
                        strValue += dr[colOfNumField].ToString() + '\t';
                    }
                    ThisChChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
                    ThisChChart.Overlap = 50;
                    //��ת
                    ThisChChart.Rotation = 360;
                    ThisChChart.Inclination = 10;
                    //������ɫ
                    ThisChChart.PlotArea.Interior.Color = "red";
                    //��ɫ
                    ThisChChart.PlotArea.Floor.Interior.Color = "green";
                    ////����series������
                    ThisChSeries.SetData(ChartDimensionsEnum.chDimSeriesNames,
                        ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(), colOfGroupName);
                    //��������
                    ThisChSeries.SetData(ChartDimensionsEnum.chDimCategories,
                        ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(), strCategory);
                    //����ֵ
                    ThisChSeries.SetData(ChartDimensionsEnum.chDimValues,
                        ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(), strValue);
                    break;
                case ChartType.Pie:
                    // ��������
                    foreach (DataRow dr in dt.Rows)
                    {
                        strCategory += dr[colOfGroupField].ToString() + '\t';
                        strValue += dr[colOfNumField].ToString() + '\t';
                    }

                    ThisChChart.Type = ChartChartTypeEnum.chChartTypePie3D;
                    ThisChChart.SeriesCollection.Add(0);
                    //��ͼ������ʾ����
                    ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
                    ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position = ChartDataLabelPositionEnum.chLabelPositionAutomatic;
                    ThisChChart.SeriesCollection[0].Marker.Style = ChartMarkerStyleEnum.chMarkerStyleCircle;

                    //��������ͼ�����ݵ����� 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName");

                    //�������ݷ��� 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);

                    //����ֵ 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                        (int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
                    break;
                case ChartType.Line:
                    // ��������
                    foreach (DataRow dr in dt.Rows)
                    {
                        strCategory += dr[colOfGroupField].ToString() + '\t';
                        strValue += dr[colOfNumField].ToString() + '\t';
                    }
                    ThisChChart.Type = ChartChartTypeEnum.chChartTypeLineStacked;
                    ThisChChart.SeriesCollection.Add(0);
                    //��ͼ������ʾ����
                    ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
                    //ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
                    //ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionOutsideBase;

                    ThisChChart.SeriesCollection[0].Marker.Style = ChartMarkerStyleEnum.chMarkerStyleCircle;

                    //��������ͼ�����ݵ����� 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimSeriesNames,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName");

                    //�������ݷ��� 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimCategories,
                        +(int)ChartSpecialDataSourcesEnum.chDataLiteral, strCategory);

                    //����ֵ 
                    ThisChChart.SeriesCollection[0].SetData(ChartDimensionsEnum.chDimValues,
                        (int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
                    break;
            }

            //����ͼ���ļ�
            //ThisChart.ExportPicture("G:\\chart.gif","gif",600,350);

            string fileName = ct.ToString() + PubClass.GenerTempFileName("GIF");
            string strAbsolutePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Temp\\" + fileName;
            try
            {
                ThisChart.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight);
            }
            catch (Exception ex)
            {
                throw new Exception("@���ܴ����ļ�,������Ȩ�޵����⣬��Ѹ�Ŀ¼����Ϊ�κ��˶������޸ġ�" + strAbsolutePath + " Exception:" + ex.Message);
            }
            return fileName;
        }
		/// <summary>
		/// ����2γ�ȵı�
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="colOfGroupField1"></param>
		/// <param name="colOfGroupName1"></param>
		/// <param name="colOfGroupField2"></param>
		/// <param name="colOfGroupName2"></param>
		/// <param name="colOfNumField"></param>
		/// <param name="colOfNumName"></param>
		/// <param name="title"></param>
		/// <param name="chartHeight"></param>
		/// <param name="chartWidth"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public   static string  GenerChart2D(DataTable dt,string colOfGroupField1, string colOfGroupName1, 
			string colOfGroupField2, string colOfGroupName2,
			string colOfNumField,string colOfNumName, string title, int chartHeight, int chartWidth, ChartType ct )
		{
			string strCategory =""; // "1" + '\t' + "2" + '\t' + "3" + '\t'+"4" + '\t' + "5" + '\t' + "6" + '\t';
			string strValue =""; // "9" + '\t' + "8" + '\t' + "4" + '\t'+"10" + '\t' + "12" + '\t' + "6" + '\t';

			//��������
			ChartSpace ThisChart = new ChartSpaceClass();
			ChChart ThisChChart  = ThisChart.Charts.Add(0);
			//ChSeries ThisChSeries = ThisChChart.SeriesCollection.Add(0);

			//��ʾͼ��
			ThisChChart.HasLegend = true;
			//����
			ThisChChart.HasTitle = true;
			ThisChChart.Title.Caption = title;

			//����x,y��ͼʾ˵��
			ThisChChart.Axes[0].HasTitle = true;
			ThisChChart.Axes[1].HasTitle = true;

//			ThisChChart.Axes[0].Title.Caption = colOfGroupName1;
//			ThisChChart.Axes[1].Title.Caption = colOfNumName;

			switch( ct)
			{
				case ChartType.Histogram:
					ThisChChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
					DataTable dtC=dt.Clone();
					int j=-1;
					foreach(DataRow dr1 in dtC.Rows)
					{
						j++;
						ChSeries ThisChSeries =ThisChChart.SeriesCollection.Add(j);
						ThisChChart.SeriesCollection[j].DataLabelsCollection.Add();
						//����series������
						ThisChChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimSeriesNames,
							(int)ChartSpecialDataSourcesEnum.chDataLiteral, dr1[colOfGroupField1].ToString() );

						strCategory="";
						strValue="";
						foreach(DataRow dr in dt.Rows)
						{
							if (dr1[colOfGroupField1].Equals( dr[colOfGroupField1] ) ==false )
								continue;

							strCategory+= dr[ colOfGroupField1 ].ToString()+'\t'+dr[ colOfGroupField2 ].ToString() + '\t';
							strValue   += dr[ colOfNumField    ].ToString() + '\t';
						}

						//��������
						ThisChSeries.SetData (ChartDimensionsEnum.chDimCategories,
							(int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
						//����ֵ
						ThisChSeries.SetData
							(ChartDimensionsEnum.chDimValues,
							(int)ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
					}
//					ThisChChart.Overlap = 50;
//					//��ת
//					ThisChChart.Rotation  = 360;
//					ThisChChart.Inclination = 10;
//					//������ɫ
//					ThisChChart.PlotArea.Interior.Color = "red";
//					//��ɫ
//					ThisChChart.PlotArea.Floor.Interior.Color = "green";
					////����series������
					//ThisChSeries.SetData(ChartDimensionsEnum.chDimSeriesNames,ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(),"ssdd" );
					//��������
					//ThisChSeries.SetData(ChartDimensionsEnum.chDimCategories,ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(),strCategory);
					//����ֵ
					//ThisChSeries.SetData(ChartDimensionsEnum.chDimValues,ChartSpecialDataSourcesEnum.chDataLiteral.GetHashCode(),strValue);
					break;
				case ChartType.Pie:
					// ��������
					foreach(DataRow dr in dt.Rows)
					{
						strCategory+=dr[ colOfGroupField1 ].ToString() + '\t';
						strValue   +=dr[ colOfNumField   ].ToString() +'\t';
					}

					ThisChChart.Type = ChartChartTypeEnum.chChartTypePie3D;
					ThisChChart.SeriesCollection.Add(0); 
					//��ͼ������ʾ����
					ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
					ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
					ThisChChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

					//��������ͼ�����ݵ����� 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName" ); 

					//�������ݷ��� 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

					//����ֵ 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimValues, 
						(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
					break;
				case ChartType.Line:
					// ��������
					foreach(DataRow dr in dt.Rows)
					{
						strCategory+=dr[ colOfGroupField1 ].ToString() + '\t';
						strValue   +=dr[ colOfNumField   ].ToString() +'\t';
					}
					ThisChChart.Type = ChartChartTypeEnum.chChartTypeLineStacked;
					ThisChChart.SeriesCollection.Add(0); 
					//��ͼ������ʾ����
					ThisChChart.SeriesCollection[0].DataLabelsCollection.Add();
					//ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
					//ThisChChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionOutsideBase;

					ThisChChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

					//��������ͼ�����ݵ����� 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, "strSeriesName" ); 

					//�������ݷ��� 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
						+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

					//����ֵ 
					ThisChChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimValues, 
						(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
					break;
			}

			
			//����ͼ���ļ�
			//ThisChart.ExportPicture("G:\\chart.gif","gif",600,350);

			string fileName= ct.ToString()+PubClass.GenerTempFileName("GIF");
			string strAbsolutePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 

			try
			{
				ThisChart.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@���ܴ����ļ�,������Ȩ�޵����⣬��Ѹ�Ŀ¼����Ϊ�κ��˶������޸ġ�"+strAbsolutePath+" Exception:"+ex.Message);
			}
			return fileName;
			//
			//
			//			//����GIF�ļ������·��. 
			//			string strRelativePath = "./Temp/"+fileName;
			//
			//			//��ͼƬ��ӵ�placeholder.  onmousedown=\"CellDown('Cell')\"
			//			//string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'  />"; 
			//			return strRelativePath ;
		}
		/// <summary>
		/// ����ͼ��
		/// </summary>
		/// <param name="sdr">����Դ(sqlDataReader)</param>
		/// <param name="_type">ͼ������(ö��)</param>
		/// <param name="filePath">ͼƬ·��</param>
		/// <param name="chartWidth">ͼƬ���</param>
		/// <param name="chartHeight">ͼƬ�߶�</param>
		/// <returns>ͼƬ����</returns>
		public void Bind(Rpt3DEntity rpt , ChartChartTypeEnum charttype, int chartWidth,int chartHeight)
		{
			 
			ChartSpace objCSpace = new ChartSpaceClass(); 
			//��ChartSpace���������ͼ��Add��������chart����
			ChChart objChart = objCSpace.Charts.Add (0);
			
			//ָ��ͼ���Ƿ���Ҫͼ��
			objChart.HasLegend=true;
			objChart.HasTitle=true;
			objChart.Title.Caption=rpt.Title;
			
			//ָ��ͼ������͡�������OWC.ChartChartTypeEnumö��ֵ�õ�
			//objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;
			//objChart.Type = charttype;	
			objChart.Type =charttype ; // ChartChartTypeEnum.chChartTypeColumnClustered3D;
			
			#region chart1
			int j=-1;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				j++;
				//�� ChartSpace ���������ͼ��Add��������chart����
				objChart.SeriesCollection.Add(j);
				objChart.SeriesCollection[j].DataLabelsCollection.Add();
				//string strSeriesName=""+(i+1);
				string strSeriesName=en1.No+en1.Name;
				//����series������
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimSeriesNames,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);

				string strCategory="";
				string strValue="";
				foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
				{
					foreach(EntityNoName en3 in rpt.SingleDimensionItem3)
					{
						float val =float.Parse( rpt.HisCells.GetCell(en1.No,en2.No,en3.No).val.ToString())  ;
						if (val==0)
							continue;

						strCategory+= en2.Name + '\t'  +en3.Name + '\t' ;
						strValue+= val.ToString() + '\t';
					}
				}
				//��������
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimCategories,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
				//����ֵ
				objChart.SeriesCollection[j].SetData
					(ChartDimensionsEnum.chDimValues,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
				//objChart.SeriesCollection[i].Caption="";
			}		 

			//objChart.Rotation  = 360;
			//objChart.Inclination = 10;
			#endregion


			string fileName= (int)charttype+PubClass.GenerTempFileName("GIF");
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 

			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@���ܴ����ļ�,������Ȩ�޵����⣬��Ѹ�Ŀ¼����Ϊ�κ��˶������޸ġ�"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//����GIF�ļ������·��. 
			string strRelativePath = "./Temp/"+fileName;


			//��ͼƬ��ӵ�placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'  />"; 
			//string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'  />"; 
			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
			return  ;
	
		}
		#endregion 

		#region 2γ��ͼ��		 
		/// <summary>
		/// ���汨��
		/// </summary>
		/// <param name="rpt">����ʵ��</param>
		/// <param name="chartWidth">���</param>
		/// <param name="chartHeight">�߶�</param>
		public void BindLine(RptPlanarEntity rpt,int chartWidth,int chartHeight)
		{
			Bind( rpt , ChartChartTypeEnum.chChartTypeLineStacked ,  chartWidth, chartHeight);
		}
		public void BindPie(RptPlanarEntity rpt,int chartWidth,int chartHeight)
		{
			Bind( rpt , ChartChartTypeEnum.chChartTypePie3D,  chartWidth, chartHeight);
		}
		/// <summary>
		/// ���汨��
		/// </summary>
		/// <param name="rpt">����ʵ��</param>
		/// <param name="chartWidth">���</param>
		/// <param name="chartHeight">�߶�</param>
		public void BindHistogram(RptPlanarEntity rpt,int chartWidth,int chartHeight)
		{
			Bind( rpt , ChartChartTypeEnum.chChartTypeColumnClustered ,  chartWidth, chartHeight);
		}
		/// <summary>
		/// ����ͼ��
		/// </summary>
		/// <param name="sdr">����Դ(sqlDataReader)</param>
		/// <param name="_type">ͼ������(ö��)</param>
		/// <param name="filePath">ͼƬ·��</param>
		/// <param name="chartWidth">ͼƬ���</param>
		/// <param name="chartHeight">ͼƬ�߶�</param>
		/// <returns>ͼƬ����</returns>
		public void Bind(RptPlanarEntity rpt , ChartChartTypeEnum charttype, int chartWidth,int chartHeight)
		{
			#region  RptPlanarEntity

//			BP.Port.HYOfBigs d1= new BP.Port.HYOfBigs();
//			d1.RetrieveAll();
//
//			BP.Port.Depts d2= new BP.Port.Depts();
//			d2.RetrieveAll();
//
//			DataTable dt = DBAccess.RunSQLReturnTable("SELECT FK_HYOfBig,FK_Dept, COUNT(*) FROM GS_Taxpayer WHERE (1=1) AND ( ( ( 1=1 ) ) AND ( ( GS_Taxpayer.SignDate < '2004-10-14') ) AND ( ( 1=1 ) ) ) GROUP BY FK_HYOfBig, FK_Dept") ; 
//			RptPlanarEntity rpt= new RptPlanarEntity(d1,d2,dt);

			#endregion

			#region gener 
			//			ArrayList[] data_al=new ArrayList[rpt.SingleDimensionItem1.Count];
			//			for(int i=0;i<rpt.SingleDimensionItem1.Count;i++)
			//			{
			//				data_al[i] = new ArrayList();
			//			}
			//			int j=-1 ;
			//			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			//			{
			//				j++ ;
			//				
			//				foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
			//				{
			//					//data_al[j].Add(en1.No);
			//					data_al[j].Add(en1.Name);
			//
			//					//data_al[j].Add(en2.No);
			//					data_al[j].Add(en2.Name);
			//					data_al[j].Add(rpt.PlanarCells.GetCell(en1.No,en2.No)); 
			//				}
			//			}		 
			#endregion

			ChartSpace objCSpace = new ChartSpaceClass(); 
			//��ChartSpace���������ͼ��Add��������chart����
			ChChart objChart = objCSpace.Charts.Add (0); 
			
			//ָ��ͼ���Ƿ���Ҫͼ��
			objChart.HasLegend=true;
			objChart.HasTitle=true;
			objChart.Title.Caption=rpt.Title;
				
			//ָ��ͼ������͡�������OWC.ChartChartTypeEnumö��ֵ�õ�
			//objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered;

			objChart.Type = charttype;

			
			
			#region chart1
			int j=-1;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				j++;
				//�� ChartSpace ���������ͼ��Add��������chart����
				objChart.SeriesCollection.Add(j);			
				objChart.SeriesCollection[j].DataLabelsCollection.Add();
				//string strSeriesName=""+(i+1);
				string strSeriesName=en1.No+en1.Name;

				//����series������
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimSeriesNames,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);

				string strCategory="";
				string strValue="";
				foreach(EntityNoName en2 in rpt.SingleDimensionItem2)
				{
					float val =float.Parse( rpt.PlanarCells.GetCell(en1.No,en2.No).val.ToString())  ;
					if (val==0)
						continue;

					strCategory+=en2.Name + '\t' ;
					strValue+= val.ToString() + '\t';
				}
				//��������
				objChart.SeriesCollection[j].SetData (ChartDimensionsEnum.chDimCategories,
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
				//����ֵ
				objChart.SeriesCollection[j].SetData
					(ChartDimensionsEnum.chDimValues,					
					(int)ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
				//objChart.SeriesCollection[i].Caption="";
			}

		 
			#endregion

			string fileName=(int)charttype + PubClass.GenerTempFileName("gif")  ;
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName;

			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@���ܴ����ļ�,������ȫ�ߵ����⣬��Ѹ�Ŀ¼����Ϊ�κ��˶������޸ġ�"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//����GIF�ļ������·��. 
			// string strRelativePath = "./i/test.gif"; 
			string strRelativePath = "./Temp/"+fileName;


			//��ͼƬ��ӵ�placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'   />"; 
			//string strImageTag = "<IMG SRC='../../Temp/" + fileName + "' />"; 

			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
			return  ;
			
			//_page.Controls.AddAt(2,new LiteralControl(strImageTag));
		}
		 
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		/// <summary>
		/// BindPie
		/// </summary>
		/// <param name="rpt"></param>
		/// <param name="width"></param>
		/// <param name="Height"></param>
		public void BindPie(RptEntitiesNoNameWithNum rpt,int width, int Height)
		{
			this.Bind_pie(rpt,ChartChartTypeEnum.chChartTypePie3D );
		} 
		/// <summary>
		/// ��״ͼ
		/// </summary>
		/// <param name="rpt"></param>
		public void BindHistogram(RptEntitiesNoNameWithNum rpt, int width, int Height )
		{
			this.Bind(rpt,ChartChartTypeEnum.chChartTypeColumnClustered3D,width,Height);
		}
		/// <summary>
		/// chChartTypeLine
		/// </summary>
		/// <param name="rpt"></param>
		public void BindLine(RptEntitiesNoNameWithNum rpt, int width, int Height)
		{
			this.Bind(rpt,ChartChartTypeEnum.chChartTypeColumnClustered);
		}
		public void Bind(RptEntitiesNoNameWithNum rpt, ChartChartTypeEnum  _ChartChartTypeEnum)
		{
			Bind(rpt,_ChartChartTypeEnum,800,600);
		}
		/// <summary>
		/// �������߻�������ͼ�� 
		/// </summary>
		/// <param name="rpt">����һ������</param>
		/// <param name="_ChartChartTypeEnum">ͼ����״������״ͼ������ͼ</param>
		public void Bind(RptEntitiesNoNameWithNum rpt, ChartChartTypeEnum  _ChartChartTypeEnum, int width, int Height)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			//����ChartSpace����������ͼ��
			//ChartSpace����������ͼ����࣬ͼ����ɺ������������
			ChartSpace objCSpace = new ChartSpaceClass();

			//��ChartSpace���������ͼ��ChartSpace��Add��������ͼ������chart���󣬲�����ʾ������ͼ�������
			ChChart objChart = objCSpace.Charts.Add(0); 
			//objChart.SeriesCollection.Add(0);
			//ָ��ͼ������͡�������ChartChartTypeEnumö��ֵ�õ���ͨ���趨Chart������Type������ָ��ͼ������͡�
			/*���������Ͱ�����chChartTypeArea ���ͼ��chChartTypeBarClustered ����ͼ��chChartTypePie ��ͼ��
			 * chChartType RadarLine �״���ͼ��chChartTypeSmoothLine ƽ������ͼ��chChartTypeDoughnut ����ͼ�ȵ�*/
			//			objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered; 
			objChart.Type = _ChartChartTypeEnum ;  // ChartChartTypeEnum.chChartTypeSmoothLine; 

			//ChartChartTypeEnum.chChartTypeSmoothLine; 

			// chChartTypeColumnClustered3D
			//chChartTypeColumnClustered3D

			//ָ��ͼ���Ƿ���Ҫͼ�� 
			/*ͼʾ˵����Ҫ����ͼ��������ɫ��ʾ�������ͣ���ͼ�⣨ͼ��ı��⣩��
			 * �أ��������˵����һ������˵�������ϵ����ݵ�λ��*/
			objChart.HasLegend = true; 

			//�������� 
			objChart.HasTitle = true; //�Ƿ���ʾ���⣬����Ϊfalse�ձ��ⲻ�ܸ�ֵ
			objChart.Title.Caption= rpt.Title; 
			

			/*���´�������X��Y��ͼʾ��˵������������˵����������ʾ��ʽ��������ɫ��
			 * Axes[0]ΪX�ᣬAxes[1]ΪY��*/
			//����x,y���ͼʾ˵��. 
			objChart.Axes[0].HasTitle = true; 
			objChart.Axes[0].Title.Caption = rpt.SingleDimensionItem1.GetNewEntity.EnDesc ;
			objChart.Axes[0].Orientation=-90 ; 



			//objChart.Axes[0].


			//objChart.Axes[1].Title.Font.Color="red";
			objChart.Axes[1].HasTitle = true; 
			objChart.Axes[1].Title.Caption = rpt.LeftTitle; 
			//objChart.Axes[1].Title.Font.Color="red";
//			objCSpace.
//
//			objChart.Axes[0].ValueToPoint("78");
//			objChart.Axes[1].ValueToPoint("13");
//
//			 ActiveChart.ApplyDataLabels AutoText:=True, LegendKey:=False, _
//        HasLeaderLines:=False, ShowSeriesName:=False, ShowCategoryName:=False, _
//        ShowValue:=True, ShowPercentage:=False, ShowBubbleSize:=False
//End Sub


			


			//�������� 
			/*������ʱ��OWC�����������ж��ַ�����������Щ������Ҫ�õ� SetData ����������������д�� Chart �����
			 * ��˽���ϸ���ܴ˷�����SetData ����Ӧ���� WCChart��WCErrorBars �� WCSeries �������������ֶ�����
			 * �����ݡ� 
			 * SetData ����������������Dimension��DataSourceIndex �� DataReference��Dimension ��������ͼ���б���
			 * �����ݵ�һ���֡����õ�ά�ȳ���Ϊ SeriesNames��Categories��Values��YValues��XValues��OpenValues��
			 * CloseValues��HighValues��LowValues��BubbleValues��RValues �� ThetaValues����Щ����ÿһ�ֶ�������
			 * ͼ���һ���֣����磬SeriesNames ����������ÿ����������OpenValues �� CloseValues �������ù�Ʊͼ��
			 * ���̼ۺ����̼۵ȵȡ�ÿ������������Ϊ���� chDimSeriesNames��chDimCategories��chDimValues�ȴ����е�
			 * ö�ٳ��������ݸ��÷����ġ�
			 * �μ�http://www.webasp.net/tech/article_show.asp?id=13635*/
			/*categories �� values ������tab�ָ���ַ�������ʾ*/ 
			string strSeriesName = "ͼ�� 1"; 


//			string strCategory = "1" + '\t' + "2" + '\t' + "3" + '\t'+"4" + '\t' + "5" + '\t' + "6" + '\t'; 
//			string strValue =    "9" + '\t' + "8" + '\t' + "4" + '\t'+"10" + '\t' + "12" + '\t' + "6" + '\t';

			string strCategory="" ; 
			string strValue="" ; 

			foreach(BP.Rpt.Rpt1DCell cell in rpt.Rpt1DCells)
			{
				
				strCategory+= rpt.GetTextByValue(cell.PK) + '\t' ;
				strValue+= cell.val.ToString() + '\t';
			}
			//foreach(rpt

			//���һ��ͼ������ 
			/*���������Ҫ�趨Chart������SeriesCollection���ԡ�����ʹ��SeriesCollection��Add��������һ�����ݡ�
			 * Ȼ��ʹ��SetData��������������ݡ�*/
			objChart.SeriesCollection.Add(0); 
			//��ͼ������ʾ����
			objChart.SeriesCollection[0].DataLabelsCollection.Add();
			objChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

			//��������ͼ�����ݵ����� 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName); 

			//�������ݷ��� 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

			//����ֵ 
			objChart.SeriesCollection[0].SetData 
				(ChartDimensionsEnum.chDimValues, 
				(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);  

			//�����GIF�ļ�. 
			//string strAbsolutePath = (Server.MapPath(".")) + "\\i\\test.gif";

			string fileName= (int)_ChartChartTypeEnum +DateTime.Now.ToString("MMddhhmmss")+".gif";
			//string fileName=f+".gif";
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 
			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", width, Height); 
			}
			catch(Exception ex)
			{
				throw new Exception("@���ܴ����ļ�,������ȫ�ߵ����⣬��Ѹ�Ŀ¼����Ϊ�κ��˶������޸ġ�"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//����GIF�ļ������·��. 
			// string strRelativePath = "./i/test.gif"; 
			string strRelativePath = "./Temp/"+fileName;


			//��ͼƬ��ӵ�placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'   />"; 
			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
		}

		/// <summary>
		/// ���ɱ�״ͼ�� 
		/// </summary>
		/// <param name="rpt">����һ������</param>
		/// <param name="_ChartChartTypeEnum">ͼ����״������״ͼ������ͼ</param>
		public void Bind_pie(RptEntitiesNoNameWithNum rpt, ChartChartTypeEnum  _ChartChartTypeEnum)
		{
			//�ڴ˴������û������Գ�ʼ��ҳ��
			//����ChartSpace����������ͼ��
			//ChartSpace����������ͼ����࣬ͼ����ɺ������������
			ChartSpace objCSpace = new ChartSpaceClass(); 

			//��ChartSpace���������ͼ��ChartSpace��Add��������ͼ������chart���󣬲�����ʾ������ͼ�������
			ChChart objChart = objCSpace.Charts.Add(0); 
			//objChart.SeriesCollection.Add(0);

			//ָ��ͼ������͡�������ChartChartTypeEnumö��ֵ�õ���ͨ���趨Chart������Type������ָ��ͼ������͡�
			/*���������Ͱ�����chChartTypeArea ���ͼ��chChartTypeBarClustered ����ͼ��chChartTypePie ��ͼ��
			 * chChartType RadarLine �״���ͼ��chChartTypeSmoothLine ƽ������ͼ��chChartTypeDoughnut ����ͼ�ȵ�*/
			//			objChart.Type = ChartChartTypeEnum.chChartTypeColumnClustered; 
			// ChartChartTypeEnum.chChartTypeSmoothLine;
			objChart.Type = _ChartChartTypeEnum ;  

			//ָ��ͼ���Ƿ���Ҫͼ�� 
			/*ͼʾ˵����Ҫ����ͼ��������ɫ��ʾ�������ͣ���ͼ�⣨ͼ��ı��⣩��
			 * �أ��������˵����һ������˵�������ϵ����ݵ�λ��*/
			objChart.HasLegend = true; 

			//�������� 
			objChart.HasTitle = true; //�Ƿ���ʾ���⣬����Ϊfalse�ձ��ⲻ�ܸ�ֵ
			objChart.Title.Caption= rpt.Title; 
 
			/*categories �� values ������tab�ָ���ַ�������ʾ*/ 
			string strSeriesName = "ͼ�� 1"; 
			string strCategory="" ; 
			string strValue="";

			foreach(BP.Rpt.Rpt1DCell cell in rpt.Rpt1DCells)
			{
				//string val=
				strCategory+= rpt.GetTextByValue(cell.PK)+"%" + '\t' ;
				strValue+= cell.val.ToString() +"%"+ '\t';
			}
			//this.ResponseWriteBlueMsg(strCategory+"<br>"+strValue);
			//foreach(rpt

			//���һ��ͼ������ 
			/*���������Ҫ�趨Chart������SeriesCollection���ԡ�����ʹ��SeriesCollection��Add��������һ�����ݡ�
			 * Ȼ��ʹ��SetData��������������ݡ�*/
			objChart.SeriesCollection.Add(0); 
			//��ͼ������ʾ����
			objChart.SeriesCollection[0].DataLabelsCollection.Add();
			if (_ChartChartTypeEnum==ChartChartTypeEnum.chChartTypePie3D)
			{
				objChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionAutomatic;
			}
			else
			{
				objChart.SeriesCollection[0].DataLabelsCollection[0].Position=ChartDataLabelPositionEnum.chLabelPositionOutsideBase;
			}

			objChart.SeriesCollection[0].Marker.Style=ChartMarkerStyleEnum.chMarkerStyleCircle;

			//��������ͼ�����ݵ����� 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimSeriesNames, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName); 

			//�������ݷ��� 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimCategories, 
				+ (int)ChartSpecialDataSourcesEnum.chDataLiteral,strCategory); 

			//����ֵ 
			objChart.SeriesCollection[0].SetData (ChartDimensionsEnum.chDimValues, 
				(int)ChartSpecialDataSourcesEnum.chDataLiteral, strValue);
			//�����GIF�ļ�. 
			//string strAbsolutePath = (Server.MapPath(".")) + "\\i\\test.gif"; 

			string fileName= (int)_ChartChartTypeEnum + PubClass.GenerTempFileName("gif");
			string strAbsolutePath = this.Request.PhysicalApplicationPath+"\\Temp\\"+fileName; 

			try
			{
				objCSpace.ExportPicture(strAbsolutePath, "GIF", 800, 600); 
			}
			catch(Exception ex)
			{
				throw new Exception("@���ܴ����ļ�,������ȫ�ߵ����⣬��Ѹ�Ŀ¼����Ϊ�κ��˶������޸ġ�"+strAbsolutePath+" Exception:"+ex.Message);
			}

			//����GIF�ļ������·��. 
			// string strRelativePath = "./i/test.gif"; 
			string strRelativePath = "./Temp/"+fileName;


			//��ͼƬ��ӵ�placeholder.  onmousedown=\"CellDown('Cell')\"
			string strImageTag = "<IMG SRC='../../Temp/" + fileName + "'   />"; 
			this.Add(strImageTag);

			//PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));


			//			string fileName=DBAccess.GenerOID()+".gif";
			//			string strAbsolutePath = (Server.MapPath(".")) + "\\Temp\\"+fileName; 
			//			objCSpace.ExportPicture(strAbsolutePath, "GIF", 800, 600); 
			//
			//			//����GIF�ļ������·��. 
			//			// string strRelativePath = "./i/test.gif"; 
			//			string strRelativePath = "./Temp/"+fileName;
			//
			//
			//			//��ͼƬ��ӵ�placeholder. 
			//			string strImageTag = "<IMG SRC='" + strRelativePath + "'/>"; 
			//			PlaceHolder1.Controls.Add(new LiteralControl(strImageTag));
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
