using System;
//using OWC10;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.Rpt
{
	/// <summary>
	/// RptChart 的摘要说明。
	/// </summary>
	public class RptChart
	{
		/// <summary>
		/// 生成图表
		/// </summary>
		/// <param name="sdr">数据源(sqlDataReader)</param>
		/// <param name="_type">图表类型(枚举)</param>
		/// <param name="filePath">图片路径</param>
		/// <param name="chartWidth">图片宽度</param>
		/// <param name="chartHeight">图片高度</param>
		/// <returns>图片名称</returns>
		public static string GenerChart(Rpt3DEntity rpt , ChartChartTypeEnum charttype, int chartWidth,int chartHeight,string fileName)
		{
			OWC10.ChartSpace objCSpace = new OWC10.ChartSpaceClass(); 
			//在ChartSpace对象中添加图表，Add方法返回chart对象
			OWC10.ChChart objChart = objCSpace.Charts.Add (0);
			
			//指定图表是否需要图例
			objChart.HasLegend=true;
			objChart.HasTitle=true;
			objChart.Title.Caption=rpt.Title;
			//指定图表的类型。类型由OWC.ChartChartTypeEnum枚举值得到
			//objChart.Type = OWC10.ChartChartTypeEnum.chChartTypeColumnClustered;
			objChart.Type = charttype;			
			
			#region chart1
			int j=-1;
			foreach(EntityNoName en1 in rpt.SingleDimensionItem1)
			{
				j++;
				//在 ChartSpace 对象中添加图表，Add方法返回chart对象。
				objChart.SeriesCollection.Add(j);
				objChart.SeriesCollection[j].DataLabelsCollection.Add();
				//string strSeriesName=""+(i+1);
				string strSeriesName=en1.No+en1.Name;
				//给定series的名字
				objChart.SeriesCollection[j].SetData (OWC10.ChartDimensionsEnum.chDimSeriesNames,
					(int)OWC10.ChartSpecialDataSourcesEnum.chDataLiteral, strSeriesName);

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
				//给定分类
				objChart.SeriesCollection[j].SetData (OWC10.ChartDimensionsEnum.chDimCategories,
					(int)OWC10.ChartSpecialDataSourcesEnum.chDataLiteral,strCategory);
				//给定值
				objChart.SeriesCollection[j].SetData
					(OWC10.ChartDimensionsEnum.chDimValues,
					(int)OWC10.ChartSpecialDataSourcesEnum.chDataLiteral,strValue);
				//objChart.SeriesCollection[i].Caption="";
			}		 
			#endregion

			if (fileName=="" || fileName==null)
			{

				fileName=DBAccess.GenerOID()+".gif";
				fileName = "D:\\WebApp\\"+fileName;
			}

			try
			{
				objCSpace.ExportPicture(fileName, "GIF", chartWidth, chartHeight); 
			}
			catch(Exception ex)
			{
				throw new Exception("@不能创建文件,可能是权限的问题，请把该目录设置为任何人都可以修改。"+fileName+" Exception:"+ex.Message);
			}
			return  fileName ;
		}
	}
}
