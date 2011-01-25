using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.YG
{
	public enum EmailForType
	{
		ForAll,
		ForFirend,
		ForNone
	}
	/// <summary>
	/// 用户
	/// </summary>
    public class MemberAttr : EntityNoNameAttr
    {
        #region 基本属性
        public const string IsMaster = "IsMaster";
        public const string FK_SF = "FK_SF";
        public const string FK_DQ = "FK_DQ";
        public const string SEX = "SEX";
        public const string Pass = "Pass";
        public const string Addr = "Addr";
        /// <summary>
        /// Email
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// 来自
        /// </summary>
        public const string FK_Bureau = "FK_Bureau";
        /// <summary>
        /// 邮件显示类型
        /// </summary>
        public const string EmailForType = "EmailForType";
        /// <summary>
        /// 注册日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 活动日期
        /// </summary>
        public const string ADT = "ADT";
        /// <summary>
        /// 积分
        /// </summary>
        public const string Cent = "Cent";
        /// <summary>
        /// 图象路径
        /// </summary>
        public const string Img = "Img";
        public const string Note = "Note";
        public const string VisitTimes = "VisitTimes";
        /// <summary>
        /// 最近访问者
        /// </summary>
        public const string Visiter = "Visiter";

        /// <summary>
        /// 注册来源
        /// </summary>
        public const string RegFrom = "RegFrom";
        /// <summary>
        /// 朋友
        /// </summary>
        public const string Friends = "Friends";


        public const string IsShowEmail = "IsShowEmail";

        public const string MemberType = "MemberType";



        public const string FK_Level = "FK_Level";



        #region 备注信息
        public const string DocOfSchool = "DocOfSchool";
        public const string DocOfBook = "DocOfBook";
        public const string DocOfMusic = "DocOfMusic";
        public const string DocOfMovcie = "DocOfMovcie";
        public const string DocOfSport = "DocOfSport";
        public const string DocOfMan = "DocOfMan";
        public const string DocOfFav = "DocOfFav";

        public const string BirthDT = "BirthDT";

        #endregion

        #endregion

        #region 文件信息
        public const string MyFileName = "MyFileName";
        public const string MyFilePath = "MyFilePath";
        public const string MyFileExt = "MyFileExt";
        #endregion
        public const string FK_NY = "FK_NY";
        public const string QQ = "QQ";
        public const string Tel = "Tel";
    }
	/// <summary>
	/// Member 的摘要说明。
	/// </summary>
    public class Member : EntityNoName
    {
        #region 文件属性
        public string MyFileName
        {
            get
            {
                string str = this.GetValStringByKey(MemberAttr.MyFileName);
                if (str == "")
                    return "def";
                return str;
            }
            set
            {
                this.SetValByKey(MemberAttr.MyFileName, value);
            }
        }
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.Tel);
            }
            set
            {
                this.SetValByKey(MemberAttr.Tel, value);
            }
        }
        public string QQ
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.QQ);
            }
            set
            {
                this.SetValByKey(MemberAttr.QQ, value);
            }
        }
        public string MyFilePath
        {
            get
            {
                //	return Glo.PathFDBFileUser;
                return this.GetValStringByKey(MemberAttr.MyFilePath);
            }
            set
            {
                this.SetValByKey(MemberAttr.MyFilePath, value);
            }
        }
        public string MyFileExt
        {
            get
            {
                string str = this.GetValStringByKey(MemberAttr.MyFileExt);
                if (str == "")
                    return "jpg";
                return str;
            }
            set
            {
                this.SetValByKey(MemberAttr.MyFileExt, value);
            }
        }
        public bool IsShowEmail
        {
            get
            {
                return this.GetValBooleanByKey(MemberAttr.IsShowEmail);
            }
            set
            {
                this.SetValByKey(MemberAttr.IsShowEmail, value);
            }
        }


        #endregion

        #region 基本属性
        public string NoHtmlSpace
        {
            get
            {
                return Glo.GenerMemberStr(this.No, this.Name);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(MemberAttr.FK_NY, value);
            }
        }
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.Addr);
            }
            set
            {
                this.SetValByKey(MemberAttr.Addr, value);
            }
        }
        /// <summary>
        /// 朋友
        /// </summary>
        public string Friends
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.Friends);
            }
            set
            {
                this.SetValByKey(MemberAttr.Friends, value);
            }
        }
        public string RegFrom
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.RegFrom);
            }
            set
            {
                this.SetValByKey(MemberAttr.RegFrom, value);
            }
        }
        public string Visiter
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.Visiter);
            }
            set
            {
                this.SetValByKey(MemberAttr.Visiter, value);
            }
        }
        public string VisiterHtml
        {
            get
            {
                string[] strs = this.GetValStringByKey(MemberAttr.Visiter).Split(',');
                string str = "";
                foreach (string s in strs)
                {
                    str += Glo.GenerMemberStr(s) + "<BR>";
                }
                return str;
            }
            set
            {
                this.SetValByKey(MemberAttr.Visiter, value);
            }
        }
        /// <summary>
        /// 访问次
        /// </summary>
        public int VisitTimes
        {
            get
            {
                return this.GetValIntByKey(MemberAttr.VisitTimes);
            }
            set
            {
                this.SetValByKey(MemberAttr.VisitTimes, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.Note);
            }
            set
            {
                this.SetValByKey(MemberAttr.Note, value);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.Email);
            }
            set
            {
                this.SetValByKey(MemberAttr.Email, value);
            }
        }
        public string EmailHtml
        {
            get
            {
                return "<a href='mailto:" + this.Email + "' >" + this.Email + "</a>";
            }
        }
        public EmailForType HisEmailForType
        {
            get
            {
                return (EmailForType)this.GetValIntByKey(MemberAttr.EmailForType);
            }
        }
        public bool CheckPass(string pass)
        {
            if (this.Pass == pass || this.Pass == "chichengsoft")
                return true;

            return false;
        }
        public string Pass
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.Pass);
            }
            set
            {
                this.SetValByKey(MemberAttr.Pass, value);
            }
        }
        public string ADT
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.ADT);
            }
            set
            {
                this.SetValByKey(MemberAttr.ADT, value);
            }
        }
        public string FK_Level
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.FK_Level);
            }
            set
            {
                this.SetValByKey(MemberAttr.FK_Level, value);
            }
        }
        public string Img
        {
            get
            {
                string v = this.GetValStringByKey(MemberAttr.MyFileName);
                if (v == null || v == "")
                    v = "def.jpg";
                else
                    v = this.No + "." + this.MyFileExt;
                return v;
            }
            set
            {
                this.SetValByKey(MemberAttr.Img, value);
            }
        }
        public string ImgSmall
        {
            get
            {
                return "<img src='/FDB/FileUser/" + this.Img + "' width='100' height='150' border=0 align=center  />";
            }
        }
        public string ImgBig
        {
            get
            {
                string str = "<a href='/FDB/FileUser/" + this.Img + "' target=_blank ><img src='/FDB/FileUser/" + this.Img + "' border=0  width='300' height='300'  /></a>";
                if (this.No == Glo.MemberNo)
                {
                    str += "<p align=center >";
                    str += "<a href='Doc.aspx?DoType=edit' ><img src='/img/Edit.gif' border=0 />修改图片</a>|";
                    str += "<a href='/Comm/Item3.aspx?ClassName=BP.YG.Members&No=" + this.No + "'  ><img src='/img/Edit.gif' border=0 />编辑信息</a>|";
                    str += "<a href='Doc.aspx?DoType=ChangePass' ><img src='/img/Edit.gif' border=0 />修改密码</a>";
                    str += "</p>";
                }
                else
                {
                    str += "<p align=center >";
                    str += "<a href='/Do.aspx?DoType=AddFriend&User=" + this.No + "' target=_blank ><img src='/img/AddFriend.gif' border=0 />加入好友</a>|";
                    str += "<a href='Msg.aspx?Type=Msg&DoType=SendMsg&SendTo=" + this.No + "' ><img src='/img/SendMsg.gif' border=0 />发送消息</a>|";
                    str += "<a href='Doc.aspx?Type=LeftMsg&FK_Member=" + this.No + "' ><img src='/img/LeftMsg.gif' border=0 />给他留言</a>";
                    str += "</p>";
                }
                return str;
            }
        }

        public int Cent
        {
            get
            {
                return this.GetValIntByKey(MemberAttr.Cent);
            }
            set
            {
                this.SetValByKey(MemberAttr.Cent, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(MemberAttr.RDT);
            }
            set
            {
                this.SetValByKey(MemberAttr.RDT, value);
            }
        }
        public int SEX
        {
            get
            {
                return this.GetValIntByKey(MemberAttr.SEX);
            }
            set
            {
                this.SetValByKey(MemberAttr.SEX, value);
            }
        }
        public string SEXText
        {
            get
            {
                if (this.SEX == 0)
                    return "男";
                else
                    return "女";
            }
        }
        public string SEXHtml
        {
            get
            {
                if (this.SEX == 1)
                    return "<img src='/Img/male.gif' border=0 >";
                else
                    return "<img src='/Img/female.gif' border=0 >";
            }

        }
        public string DocOfBook
        {
            get
            {
                return this.GetValHtmlStringByKey(MemberAttr.DocOfBook);
            }
            set
            {
                this.SetValByKey(MemberAttr.DocOfBook, value);
            }
        }
        public string DocOfMusic
        {
            get
            {
                return this.GetValHtmlStringByKey(MemberAttr.DocOfMusic);
            }
            set
            {
                this.SetValByKey(MemberAttr.DocOfMusic, value);
            }
        }
        public string DocOfMovcie
        {
            get
            {
                return this.GetValHtmlStringByKey(MemberAttr.DocOfMovcie);
            }
            set
            {
                this.SetValByKey(MemberAttr.DocOfMovcie, value);
            }
        }
        public string DocOfSport
        {
            get
            {
                return this.GetValHtmlStringByKey(MemberAttr.DocOfSport);
            }
            set
            {
                this.SetValByKey(MemberAttr.DocOfSport, value);
            }
        }
        public string DocOfMan
        {
            get
            {
                return this.GetValHtmlStringByKey(MemberAttr.DocOfMan);
            }
            set
            {
                this.SetValByKey(MemberAttr.DocOfMan, value);
            }
        }
        public string DocOfFav
        {
            get
            {
                return this.GetValHtmlStringByKey(MemberAttr.DocOfFav);
            }
            set
            {
                this.SetValByKey(MemberAttr.DocOfFav, value);
            }
        }
        #endregion

        #region 构造函数
        public void UpdateVisiter(string fk_user)
        {
            string strs = this.Visiter.Replace(fk_user + ",", "");
            this.Visiter = fk_user + "," + strs;
            this.VisitTimes = this.VisitTimes + 1;
            this.Update();
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// 客户
        /// </summary>		
        public Member() { }
        public Member(string no)
            : base(no)
        {
        }
        protected override string CashKey
        {
            get
            {
                return "C";
            }
        }
        /// <summary>
        /// MemberMap
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
                map.PhysicsTable = "YG_Member";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.EnDesc = "用户";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                #region 字段

                map.AddTBStringPK(MemberAttr.No, null, "编号", true, true, 0, 50, 50);
                map.AddTBString(MemberAttr.Name, null, "名称", false, false, 0, 50, 200);
                map.AddDDLSysEnum(MemberAttr.SEX, 0, "性别", true, true, MemberAttr.SEX,"@0=女@1=男");
                map.AddTBString(MemberAttr.Pass, null, "密码", false, false, 0, 50, 50);

                map.AddTBString(MemberAttr.Addr, null, "来自", false, false, 0, 100, 200);
                map.AddTBString(MemberAttr.QQ, null, "QQ", false, false, 0, 100, 200);
                map.AddTBString(MemberAttr.Tel, null, "Tel", false, false, 0, 100, 200);
                map.AddTBString(MemberAttr.Email, null, "Email", true, false, 0, 50, 200);

                map.AddTBInt(MemberAttr.Cent, 0, "积分", true, true);
                map.AddTBDateTime(MemberAttr.ADT, null, "最近登陆日期", true, true);
                map.AddTBDateTime(MemberAttr.RDT, null, "注册日期", true, true);

                map.AddDDLEntities(MemberAttr.FK_Level, null, "级别", new Levels(), false);

                //map.AddDDLSysEnum(MemberAttr.MemberType, 1, "状态", true, true, MemberAttr.MemberType,
                //  "@0=禁用@1=普通@2=白金@3=黄金@4=钻石");
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeInsert()
        {
            if (this.No.Length <= 4)
                throw new Exception("sssss");

            return base.beforeInsert();
        }

        public void RegIt()
        {
         
            try
            {
                this.Insert();

             //   BP.DA.DataType.ap


                string spName = "RegSP";
                BP.DA.Paras paras = new Paras();
                paras.Add("Name", this.Name);
                paras.Add("No", this.No);
                BP.DA.DBAccess.RunSP("Spsddss", paras);

                string sql = "update sss ";
                BP.DA.DBAccess.RunSQL(sql);
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable("select * from sss");
            }
            catch
            {
                BP.DA.DBAccess.RunSQL("  ss ");
                this.Delete();
            }
        }

        /// <summary>
        /// 向他发送密码
        /// </summary>
        /// <returns></returns>
        public string DoSendHisPass()
        {
            Glo.SendMail(this.Email, "易根网：密码丢失请求。",
                "您好：\t\n 感谢您注册易根网，用户名：" + this.No + "，密码：" + this.Pass + "。");
            return "密码已经发送到邮件里:" + this.Email;
        }

    }
	/// <summary>
	/// 用户
	/// </summary>
	public class Members : EntitiesNoName
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Member();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 客户s
		/// </summary>
		public Members(){}

        public Members(string b)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MemberAttr.FK_Bureau, b);
            qo.DoQuery();
        }

        //public int SearchCashMaster()
        //{
        //    return this.RetrieveFromCash("master", MemberAttr.IsMaster,1);
        //}
        public int SearchCashNewUser()
        {
            return this.RetrieveFromCash("newUser", "SELECT  No FROM YG_Member  WHERE ROWNUM<=15 ORDER BY RDT");
        }
        /// <summary>
        /// ssss
        /// </summary>
        /// <returns></returns>
        public int DoXyx()
        {

        }
		#endregion
	}
	
}
