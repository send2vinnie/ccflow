using System;

namespace BP.DA
{
	/// <summary>
	/// 栈堆设定。
	/// 日期：2005-05-17
	/// </summary>
    public class clsStack
    {
        private long Top;    //栈最大序号
        private int MaxSize; // MaxSize 栈的容量
        private string[] Element;
        public clsStack()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            Top = -1;
        }
        /// <summary>
        /// 设定栈大最大容量
        /// </summary>
        /// <param name="Size"></param>
        public void Initialize(int Size)
        {
            MaxSize = Size;
            Element = new string[Size];
        }
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="strItem"></param>
        public void Push(string strItem)
        {
            if (!IsFull())
            {
                Top = Top + 1;
                Element[Top] = strItem;
            }
        }
        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public string Pop()
        {
            string strRtn = " ";
            if (!IsEmptly())
            {
                strRtn = Element[Top];
                Top = Top - 1;
            }
            return strRtn;
        }
        public string GetTop()
        {
            string strRtn = " ";
            if (!IsEmptly())
            {
                strRtn = Element[Top];
            }
            return strRtn;
        }
        public bool IsFull()
        {
            bool IsFull = Top == (MaxSize - 1) ? true : false;
            return IsFull;
        }
        public void MakeEmptly()
        {
            Top = -1;
        }
        public bool IsEmptly()
        {
            bool IsEmptly = Top == -1 ? true : false;
            return IsEmptly;
        }
    }
}

