using System;
using System.Collections.Generic;
using System.Text;

namespace BP.En
{
    /// <summary>
    /// ͼ������
    /// </summary>
    public enum ChartType
    {
        /// <summary>
        /// ��״ͼ
        /// </summary>
        Histogram,
        /// <summary>
        /// ��״ͼ
        /// </summary>
        Pie,
        /// <summary>
        /// ����ͼ
        /// </summary>
        Line
    }
    /// <summary>
    /// ���鷽ʽ
    /// </summary>
    public enum GroupWay
    {
        /// <summary>
        /// ���
        /// </summary>
        BySum,
        /// <summary>
        /// ��ƽ��
        /// </summary>
        ByAvg
    }
    /// <summary>
    /// ����ʽ
    /// </summary>
    public enum OrderWay
    {
        /// <summary>
        /// ����
        /// </summary>
        OrderByUp,
        /// <summary>
        /// ����
        /// </summary>
        OrderByDown
    }
}
