using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_ChannelAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string ChannelName = "ChannelName";
        public const string ParentId = "ParentId";
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
        public const string Status = "Status";
        public const string Name = "Name";
        public const string ParentNo = "ParentNo";
        public const string State = "State";
    }
    
    public partial class OA_Channel : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 主键Id
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.No);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.No, value);
            }
        }
        
        /// <summary>
        /// 栏目名称
        /// </summary>
        public String ChannelName
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.ChannelName);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.ChannelName, value);
            }
        }
        
        /// <summary>
        /// 父Id
        /// </summary>
        public String ParentId
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.ParentId);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.ParentId, value);
            }
        }
        
        /// <summary>
        /// 全路径
        /// </summary>
        public String FullPath
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.FullPath);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.FullPath, value);
            }
        }
        
        /// <summary>
        /// 全Url
        /// </summary>
        public String FullUrl
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.FullUrl);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.FullUrl, value);
            }
        }
        
        /// <summary>
        /// 描述
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.Description);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Description, value);
            }
        }
        
        /// <summary>
        /// 模板名称
        /// </summary>
        public String TemplateName
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.TemplateName);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.TemplateName, value);
            }
        }
        
        /// <summary>
        /// 详细模板名称
        /// </summary>
        public String DetailTemplate
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.DetailTemplate);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.DetailTemplate, value);
            }
        }
        
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sequence
        {
            get
            {
                return this.GetValIntByKey(OA_ChannelAttr.Sequence);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Sequence, value);
            }
        }
        
        /// <summary>
        /// 是否允许评论
        /// </summary>
        public int IsComment
        {
            get
            {
                return this.GetValIntByKey(OA_ChannelAttr.IsComment);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.IsComment, value);
            }
        }
        
        /// <summary>
        /// 参考Id
        /// </summary>
        public String ReferenceID
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.ReferenceID);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.ReferenceID, value);
            }
        }
        
        /// <summary>
        /// 进度
        /// </summary>
        public int Process
        {
            get
            {
                return this.GetValIntByKey(OA_ChannelAttr.Process);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Process, value);
            }
        }
        
        /// <summary>
        /// 类型
        /// </summary>
        public String Type
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.Type);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Type, value);
            }
        }
        
        /// <summary>
        /// 标签
        /// </summary>
        public String Tags
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.Tags);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Tags, value);
            }
        }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public String Created
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.Created);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Created, value);
            }
        }
        
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(OA_ChannelAttr.Status);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Status, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Name
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.Name);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.Name, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ParentNo
        {
            get
            {
                return this.GetValStringByKey(OA_ChannelAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.ParentNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int State
        {
            get
            {
                return this.GetValIntByKey(OA_ChannelAttr.State);
            }
            set
            {
                this.SetValByKey(OA_ChannelAttr.State, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Channel()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Channel(string No)
        {
            this.No = No;
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
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_ChannelAttr.No, null, "主键Id", true, true, 0, 50, 50);
                map.AddTBString(OA_ChannelAttr.ChannelName, null, "栏目名称", true, false, 0,  50, 50);
                map.AddTBString(OA_ChannelAttr.ParentId, null, "父Id", true, false, 0,  50, 50);
                map.AddTBString(OA_ChannelAttr.FullPath, null, "全路径", true, false, 0,  100, 100);
                map.AddTBString(OA_ChannelAttr.FullUrl, null, "全Url", true, false, 0,  100, 100);
                map.AddTBString(OA_ChannelAttr.Description, null, "描述", true, false, 0,  1000, 1000);
                map.AddTBString(OA_ChannelAttr.TemplateName, null, "模板名称", true, false, 0,  100, 100);
                map.AddTBString(OA_ChannelAttr.DetailTemplate, null, "详细模板名称", true, false, 0,  100, 100);
                map.AddTBInt(OA_ChannelAttr.Sequence, 0, "顺序", true, false);
                map.AddTBInt(OA_ChannelAttr.IsComment, 0, "是否允许评论", true, false);
                map.AddTBString(OA_ChannelAttr.ReferenceID, null, "参考Id", true, false, 0,  50, 50);
                map.AddTBInt(OA_ChannelAttr.Process, 0, "进度", true, false);
                map.AddTBString(OA_ChannelAttr.Type, null, "类型", true, false, 0,  1, 1);
                map.AddTBString(OA_ChannelAttr.Tags, null, "标签", true, false, 0,  30, 30);
                map.AddTBString(OA_ChannelAttr.Created, null, "创建时间", true, false, 0,  50, 50);
                map.AddTBInt(OA_ChannelAttr.Status, 0, "状态", true, false);
                map.AddTBString(OA_ChannelAttr.Name, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_ChannelAttr.ParentNo, null, "", true, false, 0,  50, 50);
                map.AddTBInt(OA_ChannelAttr.State, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_Channels : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Channel(); }
        }
    }
}