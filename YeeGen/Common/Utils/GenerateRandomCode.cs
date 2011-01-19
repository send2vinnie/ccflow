using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Tax666.Common
{
    /// <summary>
    /// 产生随机码
    /// </summary>
    public class GenerateRandomCode
    {
        private static RNGCryptoServiceProvider rngp = new RNGCryptoServiceProvider();
        private static byte[] rb = new byte[4];

        /// <summary>
        /// 返回指定长度的数字+字母的随机码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string RadomNumberCode(int len)
        {
            string str = string.Format("0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z");
            //将字符串拆分成字符串数组
            string[] S = str.Split(new char[] { ',' });
            string strNum = "";

            ////用于记录一个随机数的值，避免产生两个重复的值
            int tag = -1;  

            //产生一个长度为了len的随机字符串  
            Random rnd = new Random();
            for (int i = 1; i <= len; i++)
            {
                if (tag == -1)
                {
                    //初始化一个实例
                    rnd = new Random(i * tag * unchecked((int)DateTime.Now.Ticks));
                }

                //返回小于61的随机数
                int rndNum = rnd.Next(36);
                //加入生成相同的数则重新生成。  
                if (tag != -1 && tag == rndNum)
                {
                    return RadomNumberCode(1);
                }

                tag = rndNum;
                strNum += S[rndNum];
            }
            return strNum;
        }

        /// <summary>
        /// 产生一个非负数的随机数
        /// </summary>
        public static int RadomNumber()
        {
            rngp.GetBytes(rb);
            int value = BitConverter.ToInt32(rb, 0);
            if (value < 0) value = -value;
            return value;
        }

        /// <summary>
        /// 产生一个非负数且最大值在 max 以下的随机数
        /// </summary>
        /// <param name="max">最大值</param>
        public static int RadomNumber(int max)
        {
            rngp.GetBytes(rb);
            int value = BitConverter.ToInt32(rb, 0);
            value = value % (max + 1);
            if (value < 0) value = -value;
            return value;
        }

        /// <summary>
        /// 产生一个非负数且最小值在 min 以上最大值在 max 以下的随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static int RadomNumber(int min, int max)
        {
            int value = RadomNumber(max - min) + min;
            return value;
        }

        /// <summary>
        /// 产生不重复的count个随机数
        /// 注意：count与maxValue必须隔一个数量级
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] GetRandomNumber(int minValue, int maxValue, int count)
        {
            int[] intList = new int[maxValue];
            for (int i = 0; i < maxValue; i++)
            {
                intList[i] = i + minValue;
            }
            int[] intRet = new int[count];
            int n = maxValue;
            for (int i = 0; i < count; i++)
            {
                int index = RadomNumber(minValue, maxValue);
                intRet[i] = intList[index];
                intList[index] = intList[--n];
            }

            return intRet;
        }

    }
}
