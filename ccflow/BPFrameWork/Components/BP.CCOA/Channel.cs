using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public class ChannelAttr : EntityNoNameAttr
    {
        public const string ParentNo = "ParentNo";
        public const string ChannelName = "ChannelName";
        public const string FullPath = "FullPath";
        public const string FullUrl = "FullUrl";
        public const string Description = "Description";
        public const string TemplateName = "TemplateName";
        public const string DetailTemplate = "DetailTemplate";
        public const string Sequence = "Sequence";
        public const string IsComment = "IsComment";
        public const string ReferenceID = "ReferenceID";
        public const string Process = "Process";
        public const string Type = "Type";
        public const string Tags = "Tags";
        public const string Created = "Created";
        public const string Updated = "Updated";
        public const string State = "State";
    }
    public class Channel : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 父级栏目ID
        /// </summary>
        public string ParentID
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(ChannelAttr.ParentNo, value);
            }
        }

        /// <summary>
        /// 显示全路径，如：/新闻/图片新闻
        /// </summary>
        public string FullPath
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.FullPath);
            }
            set
            {
                this.SetValByKey(ChannelAttr.FullPath, value);
            }
        }

        /// <summary>
        /// 全URL
        /// </summary>
        public string FullUrl
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.FullUrl);
            }
            set
            {
                this.SetValByKey(ChannelAttr.FullUrl, value);
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.Description);
            }
            set
            {
                this.SetValByKey(ChannelAttr.Description, value);
            }
        }

        /// <summary>
        /// 详细页模板
        /// </summary>
        public string DetailTemplate
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.DetailTemplate);
            }
            set
            {
                this.SetValByKey(ChannelAttr.DetailTemplate, value);
            }
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.TemplateName);
            }
            set
            {
                this.SetValByKey(ChannelAttr.TemplateName, value);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            get
            {
                return this.GetValIntByKey(ChannelAttr.TemplateName);
            }
            set
            {
                this.SetValByKey(ChannelAttr.TemplateName, value);
            }
        }

        /// <summary>
        /// 状态转化字符串
        /// </summary>
        public string StateText
        {
            get
            {
                return State == 0 ? "不可用" : "可用";
            }
        }

        /// <summary>
        /// 参考信息ID
        /// </summary>
        public string ReferenceID
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.ReferenceID);
            }
            set
            {
                this.SetValByKey(ChannelAttr.ReferenceID, value);
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created
        {
            get
            {
                return this.GetValDateTime(ChannelAttr.Created);
            }
            set
            {
                this.SetValByKey(ChannelAttr.Created, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        //public List<Channel> Channels
        //{
        //    get { return channels; }
        //    set { channels = value; }
        //}

        /// <summary>
        /// 是否走审批流程：1-审批，其他-不审批
        /// </summary>
        public string Process
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.Process);
            }
            set
            {
                this.SetValByKey(ChannelAttr.Process, value);
            }
        }

        /// <summary>
        /// 栏目类型
        /// </summary>
        public string Type
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.Type);
            }
            set
            {
                this.SetValByKey(ChannelAttr.Type, value);
            }
        }

        /// <summary>
        /// 频道唯一名称，用于URL
        /// </summary>
        public string ChannelName
        {
            get
            {
                return this.GetValStringByKey(ChannelAttr.ChannelName);
            }
            set
            {
                this.SetValByKey(ChannelAttr.ChannelName, value);
            }
        }

        /// <summary>
        /// 是否评论
        /// </summary>
        public int IsComment
        {
            get
            {
                return this.GetValIntByKey(ChannelAttr.IsComment);
            }
            set
            {
                this.SetValByKey(ChannelAttr.IsComment, value);
            }
        }

        /// <summary>
        /// 是否评论转化字符串
        /// </summary>
        public string IsCommentText
        {
            get
            {
                switch (IsComment)
                {
                    case 1: return "允许登录用户评论";
                    case 2: return "允许匿名评论";

                    default:
                    case 0: return "不允许评论";
                }
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 栏目
        /// </summary>
        public Channel()
        {
        }
        /// <summary>
        /// 栏目
        /// </summary>
        /// <param name="mypk"></param>
        public Channel(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        #endregion

        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("OA_Channel");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "栏目";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(ChannelAttr.No, null, "编号", true, true, 0, 50, 50);
                map.AddTBString(ChannelAttr.Name, null, "名称", true, false, 0, 50, 50);
                map.AddTBString(ChannelAttr.ParentNo, null, "ParentID", true, false, 0, 50, 50);
                map.AddTBString(ChannelAttr.ChannelName, null, "ChannelName", true, false, 0, 50, 50);
                map.AddTBString(ChannelAttr.FullPath, null, "FullPath", true, false, 0, 100, 100);
                map.AddTBString(ChannelAttr.FullUrl, null, "FullUrl", true, false, 0, 100, 100);
                map.AddTBString(ChannelAttr.Description, null, "Description", true, false, 0, 1000, 1000);
                map.AddTBString(ChannelAttr.TemplateName, null, "TemplateName", true, false, 0, 100, 100);
                map.AddTBString(ChannelAttr.DetailTemplate, null, "DetailTemplate", true, false, 0, 100, 100);
                map.AddTBInt(ChannelAttr.Sequence, 0, "Sequence", true, false);
                map.AddTBInt(ChannelAttr.IsComment, 0, "IsComment", true, false);
                map.AddTBString(ChannelAttr.ReferenceID, null, "ReferenceID", true, false, 0, 50, 50);
                map.AddTBInt(ChannelAttr.Process, 0, "Process", true, false);
                map.AddTBString(ChannelAttr.Type, null, "Type", true, false, 1, 1, 1);
                map.AddTBString(ChannelAttr.Tags, null, "Tags", true, false, 0, 30, 30);
                map.AddTBDateTime(ChannelAttr.Created, "Created", true, false);
                map.AddTBDateTime(ChannelAttr.Created, "Updated", true, false);
                map.AddTBInt(ChannelAttr.State, 1, "State", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public class Channels : Entities
    {

        public override Entity GetNewEntity
        {
            get { return new Channel(); }
        }
    }
}
