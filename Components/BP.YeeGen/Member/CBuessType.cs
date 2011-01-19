using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.YG
{
	public class BBuessAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 创建人
		/// </summary>
		public const string Cent="Cent";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string Note="Note";
	}
	/// <summary>
	/// 业务类型
	/// </summary>
	public class CBuessType :EntityNoName
	{
		#region  日常操作
		/// <summary>
		/// 下载文件
		/// </summary>
		public const string FDB_Down="FDB_Down";
		/// <summary>
		/// 举报文件
		/// </summary>
		public const string FDB_QBJL="FDB_QBJL";
		#endregion 日常操作

		#region  日常操作
		/// <summary>
		/// 提供建议
		/// </summary>
		public const string CZ_Advices="CZ_Advices";
		/// <summary>
		/// 反馈系统错误
		/// </summary>
		public const string CZ_Debug="CZ_Debug";
		/// <summary>
		/// 登录系统
		/// </summary>
		public const string CZ_Login="CZ_Login";
		/// <summary>
		/// 注册成员
		/// </summary>
		public const string CZ_Reg="CZ_Reg";
		#endregion 日常操作

		#region FAQ
		/// <summary>
		/// 回答别人的问题
		/// </summary>
		public const string FAQ_Answer="FAQ_Answer";
		/// <summary>
		/// 回答撤消罚款
		/// </summary>
		public const string FAQ_AnswerFK="FAQ_AnswerFK";
		/// <summary>
		/// 答案被别人采纳
		/// </summary>
		public const string FAQ_AnswerOK="FAQ_AnswerOK";
		/// <summary>
		/// 提问问题
		/// </summary>
		public const string FAQ_Ask="FAQ_Ask";
		/// <summary>
		/// 问题撤消罚款
		/// </summary>
		public const string FAQ_AskFK="FAQ_AskFK";
		#endregion

		#region 文件共享
		/// <summary>
		/// 下载加分
		/// </summary>
		public const string SF_Down="SF_Down";
		/// <summary>
		/// 文件被举报
		/// </summary>
		public const string SF_QBFK="SF_QBFK";
		/// <summary>
		/// 举报奖励
		/// </summary>
		public const string SF_QBJL="SF_QBJL";
		/// <summary>
		/// 文件被编目
		/// </summary>
		public const string SF_SL="SF_SL";
		/// <summary>
		/// 上传文件
		/// </summary>
		public const string SF_Upload="SF_Upload";
        /// <summary>
        /// 购买原创文件
        /// </summary>
        public const string YC_Read = "YC_Read";
		#endregion 

		#region Post
		/// <summary>
		/// 发表违规贴子
		/// </summary>
		public const string Post_Del="Post_Del";
		/// <summary>
		/// 贴子被置顶
		/// </summary>
		public const string Post_Up="Post_Up";
		#endregion


		#region 文章发表
		/// <summary>
		/// 发表文章
		/// </summary>
		public const string WZ_FB="WZ_FB";
		/// <summary>
		/// 违规发表文章
		/// </summary>
		public const string WZ_FBFK="WZ_FBFK";
		/// <summary>
		/// 文章录用
		/// </summary>
		public const string WZ_FBOK="WZ_FBOK";
		#endregion

		#region cent .
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(BBuessAttr.Cent);
			}
			set
			{
				this.SetValByKey(BBuessAttr.Cent,value);
			}
		}
		public string Note
		{
			get
			{
				return this.GetValStringByKey(BBuessAttr.Note);
			}
			set
			{
				this.SetValByKey(BBuessAttr.Note,value);
			}
		}
		#endregion


		#region 实现基本的方法
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// FLinkMap
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "YG_CBuessType";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "业务类型";
                map.EnType = EnType.App;
                map.AddTBString("Sort", null, "类别", true, false, 1, 200, 10);
                map.AddTBStringPK(BBuessAttr.No, null, "编号", true, false, 1, 10, 10);
                map.AddTBString(BBuessAttr.Name, null, "名称", true, false, 1, 200, 10);
                map.AddTBInt(BBuessAttr.Cent, 0, "扣加分", true, false);
                map.AddTBString(BBuessAttr.Note, null, "说明", true, false, 1, 200, 10);

                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 业务类型
		/// </summary>
		public CBuessType(){}
		/// <summary>
		/// 业务类型
		/// </summary>
		/// <param name="_No">编号</param>
		public CBuessType(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// 业务类型
	/// </summary>
	public class CBuessTypes :Entities
	{
		#region 构造
		/// <summary>
		/// 业务类型s
		/// </summary>
		public CBuessTypes(){}
		/// <summary>
		/// 业务类型
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CBuessType();
			}
		}
		#endregion
	}
}
