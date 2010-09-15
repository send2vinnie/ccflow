using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.GE
{
    public class ImageRunItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageRunItem()
        {
        }
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Content"></param>
        public ImageRunItem(string _strHref, string _strContent)
        {
            this.strHref = _strHref;
            this.strContent = _strContent;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string strHref
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string strContent
        {
            get;
            set;
        }
    }
    public class ImageRunItems : List<ImageRunItem>
    {

        public ImageRunItems()
            : base()
        { }

        /// <summary>
        /// 得到集合元素的个数
        /// </summary>
        public new int Count
        {
            get
            {
                return base.Count;
            }
        }
        /// <summary>
        /// 表示集合是否为只读
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 添加对象到集合
        /// </summary>
        /// <param name="item"></param>
        public new void Add(ImageRunItem item)
        {
            base.Add(item);
        }
        /// <summary>
        /// 添加对象到集合
        /// </summary>
        /// <param name="item"></param>
        public void Add(string strHref, string strContent)
        {
            ImageRunItem item = new ImageRunItem(strHref, strContent);
            base.Add(item);
        }
        /// <summary>
        /// 清空集合
        /// </summary>
        public new void Clear()
        {
            base.Clear();
        }
        /// <summary>
        /// 判断集合中是否包含元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public new bool Contains(ImageRunItem item)
        {
            return base.Contains(item);
        }
        /// <summary>
        /// 移除一个对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public new bool Remove(ImageRunItem item)
        {
            return base.Remove(item);
        }
        /// <summary>
        /// 设置或取得集合索引项
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new ImageRunItem this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}