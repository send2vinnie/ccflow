using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BP.DA
{
    public class BPStrs
    {
        /// <summary>
        /// �ж��Ƿ�������
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            string pattern = @"^[+-]?\d+$";
            return Regex.IsMatch(value, pattern);
        }
        /// <summary>
        /// �ж��Ƿ���С��
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDecimal(string value)
        {
            string pattern = @"^(\d*\.)?\d+$";
            return Regex.IsMatch(value, pattern);
        }
        public static bool IsTelOrHandSetNum(string no)
        {
            if (no.Length < 7)
                return false;

            return true;
        }

        /// <summary>
        /// �Ƿ���ʵ��
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsRealNum(string value)
        {
            string pattern = @"^[-+]?[0-9]+\.?[0-9]*$";
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// �ж��Ƿ��ǺϷ���Email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        ///  �ж��Ƿ�����ĸ�����ֺ������ַ���-_.'&�������
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWord(string value)
        {
            string pattern = @"^([a-zA-z0-9_\-\.\'\&])*$";
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// �ж��Ƿ��ǺϷ��������ʽ
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPassword(string value)
        {
            string pattern = @"^(\w|\s|[`~!@#\$%\^&\*\(\)_\+\-=\{\}\[\]\:\'\<\>,\.\?\|/\\;""])*$";
            return Regex.IsMatch(value, pattern);
        }
        /// <summary>
        /// �ж��Ƿ�Ϊ��Ч���û���������Ӣ����ĸ�����֡��������ַ���'_-.&��,�����ַ���������ĸ������֮�󣬲���ֻ�����ִ��м�
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUserName(string value)
        {
            string pattern = @"^(([a-zA-z0-9][\'_\-\.\&])*)?[a-zA-z0-9]+$";
            return Regex.IsMatch(value, pattern);
        }
        /// <summary>
        /// �ж��Ƿ������ġ���ĸ�����ֺ������ַ���-_.'&�������
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsName(string value)
        {
            string pattern = @"^(\w|\s|[\'_\-\.\&])*$";
            return Regex.IsMatch(value, pattern);
        }
        /// <summary>
        /// ��ȡ�����ַ�
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static string GetStrUnit(string unit)
        {
            return Regex.Replace(unit, @"[0-9]", "", RegexOptions.IgnoreCase).ToString();
        }
        /// <summary>
        /// �õ�������λ�е�����
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static decimal GetUnitNum(string unit)
        {
            unit = unit.Trim().Replace(" ", "");
            //���岢��ʼ������ֵ
            decimal unitNum = 0;

            //���岢��ʼ�������ֲ���
            string left = Regex.Replace(unit, @"[A-Za-z]", "", RegexOptions.IgnoreCase);
            try
            {
                unitNum = decimal.Parse(left);
            }
            catch //��������в���������
            {
                if (unit.Trim().ToLower().Replace(" ", "") == "twounit")
                {
                    unitNum = 2;
                }
                else
                {
                    unitNum = 1;
                }
            }
            return unitNum;
        }

        /// <summary>
        /// ������������������������λ�õ�������
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static int GetQuantity(decimal quantity, string unit)
        {
            decimal dunit = GetUnitNum(unit);
            return (int)(quantity * dunit + 0.5m);
        }

        /// <summary>
        /// ���ݸ���������������λ�õ���������
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static decimal GetUnitQty(decimal quantity, string unit)
        {
            decimal dunit = GetUnitNum(unit);
            return quantity / dunit;
        }

        /// <summary>
        ///  �õ���С��λ�۸����������������ĵ�λ��Ҫ��������
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal GetLeastPrice(string unit, decimal price)
        {
            //ȥ�������е����ֲ���
            string sUnit = Regex.Replace(unit, @"[0-9]", "", RegexOptions.IgnoreCase);

            //����������������(g��pound)
            if (sUnit.Trim().ToLower() == "g" || sUnit.ToLower().IndexOf("pound") > 0)
            {
                //�õ�������λ�е����ֲ���
                decimal dUnit = GetUnitNum(unit);
                return decimal.Divide(price, dUnit);
            }
            else
            {
                //�����Ѿ�����С��λ
                return price;
            }
        }

        /// <summary>
        /// ������ʾ��Ʒ�۸�ע�����ݿ����汣�������С��λ�۸�
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal GetUnitPrice(string unit, decimal price)
        {
            // ȥ�������е����ֲ���
            string sUnit = Regex.Replace(unit, @"[0-9]", "", RegexOptions.IgnoreCase);

            // ����������������(g��pound)��Ҫ��ʾ�����۸�
            if (sUnit.Trim().ToLower() == "g" || sUnit.ToLower().IndexOf("pound") > 0)
            {
                // �õ�������λ�е����ֲ���
                decimal dUnit = GetUnitNum(unit);
                return decimal.Multiply(price, dUnit);
            }
            else
            {
                // ���嵥λ��������Ʒ��ʾ��С��λ�۸�
                return price;
            }
        }

        /// <summary>
        /// ��ȡת��Ϊ����ʱ����ʱ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public static DateTime GetAmericTime(DateTime dt)
        //{
        //    int tSpan = (int)Config.ConfigHelper.tSpan;
        //    dt = dt.AddHours(-tSpan);
        //    return dt;
        //}

        /// <summary>
        /// ת��Ϊ����ʱ����ʱ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public static DateTime GetCurrentTime(DateTime dt)
        //{
        //    int tSpan = (int)Config.ConfigHelper.tSpan;
        //    dt = dt.AddHours(tSpan);
        //    return dt;
        //}


    }
}
