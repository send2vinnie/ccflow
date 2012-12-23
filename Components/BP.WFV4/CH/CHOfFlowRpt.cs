using System;
using BP.Port;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;
using BP.Rpt;
using BP.Web;
using BP.WF;

namespace BP.WF
{
	/// <summary>
	/// ������������
	/// </summary>
	public class CHOfFlowRpt : Rpt3D
	{
		/// <summary>
		/// ������������
		/// </summary>
        public CHOfFlowRpt()
        {
            #region ����Ļ�������
            this.HisEns = new BP.WF.CHOfFlows();
            this.Title = "���̷���";
            this.DataProperty = "����";
            this.IsShowRate = true; //�Ƿ���ʾ�ٷֱȡ�
            this.IsShowSum = true;  //�Ƿ���ʾ�ϼơ�
            // ���� �����С�default  COUNT(*)
            //this.OperationColumn="AVG(ZCZJ)";
            #endregion

            #region ������ͬ�ֶβ�ѯ����
            this.IsShowSearchKey = true;
            //this.HisAttrsOfSearch.AddFromTo("��¼����",CHOfFlowAttr.RDT, DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"), DA.DataType.CurrentData, 7 );
            #endregion

            #region ���������ѯ����
            this.AddFKSearchAttrs(CHOfFlowAttr.WFState);
            this.AddFKSearchAttrs(CHOfFlowAttr.FK_Emp);
            this.AddFKSearchAttrs(CHOfFlowAttr.FK_Dept);
            #endregion

            #region ����γ������
            
                this.AddDAttrByKey(CHOfFlowAttr.FK_Dept);

            this.AddDAttrByKey(CHOfFlowAttr.FK_NY);
            this.AddDAttrByKey(CHOfFlowAttr.FK_Dept);
            this.AddDAttrByKey(CHOfFlowAttr.FK_Emp);
            #endregion

            #region ����Ĭ�ϵ�γ�����ԣ� ���û�����Ϳ���ʹ����.
            this.AttrOfD1 = CHOfFlowAttr.FK_NY;

            this.AddDAttrByKey(CHOfFlowAttr.FK_Flow);
            
                this.AttrOfD2 = CHOfFlowAttr.FK_Dept;
                this.AttrOfD3 = CHOfFlowAttr.FK_Dept;
            #endregion
        } 
	}
}
