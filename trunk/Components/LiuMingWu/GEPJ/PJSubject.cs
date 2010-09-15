using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.GE
{
    public class PJSubjectAttr : EntityOIDAttr
    {
        //主键
        public const string ID = "ID";
        //评价组别
        public const string NewsGroup = "NewsGroup";
        //评价专题
        public const string RefOID = "RefOID";
        //评价类型
        public const string PJGroup = "PJGroup";
    }
    public class PJSubject : EntityOID
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            get
            {
                return this.GetValStringByKey(PJSubjectAttr.ID);
            }
            set
            {
                this.SetValByKey(PJSubjectAttr.ID, value);
            }
        }
        /// <summary>
        /// 所属栏目的组别
        /// </summary>
        public string NewsGroup
        {
            get
            {
                return this.GetValStringByKey(PJSubjectAttr.NewsGroup);
            }
            set
            {
                this.SetValByKey(PJSubjectAttr.NewsGroup, value);
            }
        }
        /// <summary>
        /// 评价专题
        /// </summary>
        public string RefOID
        {
            get
            {
                return this.GetValStringByKey(PJSubjectAttr.RefOID);
            }
            set
            {
                this.SetValByKey(PJSubjectAttr.RefOID, value);
            }
        }
        /// <summary>
        /// 评价组
        /// </summary>
        public int PJGroup
        {
            get
            {
                return this.GetValIntByKey(PJSubjectAttr.PJGroup);
            }
            set
            {
                this.SetValByKey(PJSubjectAttr.PJGroup, value);
            }
        }


        public override Map EnMap
        {
            get
            {
                {
                    if (this._enMap != null)
                        return this._enMap;
                    Map map = new Map();
                    #region 基本属性
                    map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //要连接的数据源（表示要连接到的那个系统数据库）。
                    map.PhysicsTable = "GE_PJSubject"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "评价主体";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBStringPK(PJSubjectAttr.ID, string.Empty, "主键", true, true, 0, 20,20);
                    map.AddTBString(PJSubjectAttr.NewsGroup, string.Empty, "投票新闻组别", true, false, 0, 10, 10);
                    map.AddTBString(PJSubjectAttr.RefOID, string.Empty, "投票专题", true, false, 0, 10, 10);
                    map.AddTBInt(PJSubjectAttr.PJGroup, 0, "评论组别", true, false);
                    #endregion 字段增加.
                    this._enMap = map;
                    return this._enMap;
                }
            }
        }
    }

    public class PJSubjects : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new PJSubject();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public PJSubjects() { }
        #endregion
    }
}