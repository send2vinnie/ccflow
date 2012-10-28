using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CY.Net
{
    [Serializable]
    public class FsItem
    {
        public FsItem(string Name, string Path, bool IsFolder)
        {
            this.Name = Name;
            this.Path = Path;
            this.IsFolder = IsFolder;
        }

        public FsItem()
        {

        }

        /// <summary>
        /// 文件夹/文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 是否是文件夹
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }
    }
}
