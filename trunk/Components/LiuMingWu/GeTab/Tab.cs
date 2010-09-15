using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.GE
{
    public class Tab
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Tab()
        { 
        }
        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Content"></param>
        public Tab(string title, string Content)
        {
            this.Title = title;
            this.Content = Content;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
    }
    public class Tabs : List<Tab>
    {

        public Tabs()
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
        public new void Add(Tab item)
        {
            base.Add(item);
        }
        /// <summary>
        /// 添加对象到集合
        /// </summary>
        /// <param name="item"></param>
        public void Add(string Title,string Content)
        {
            Tab item = new Tab(Title, Content);
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
        public new bool Contains(Tab item)
        {
            return base.Contains(item);
        }
        /// <summary>
        /// 移除一个对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public new bool Remove(Tab item)
        {
            return base.Remove(item);
        }
        /// <summary>
        /// 设置或取得集合索引项
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new Tab this[int index]
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