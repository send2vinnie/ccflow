using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.GE
{
    public class PJEmpInfoAttr : EntityOIDAttr
    {
        //外键 -- 哪组投票
        public const string FK_Subject = "FK_Subject";
        //投票人编号
        public const string FK_EmpT = "FK_EmpT";
        //投票人姓名
        public const string FK_Emp = "FK_Emp";
        //投票人IP
        public const string IP = "IP";
        //投票次数
        public const string PJTimes = "PJTimes";
        //评价时间
        public const string RDT = "RDT";
    }

    public class PJEmpInfo : EntityOID
    {
        /// <summary>
        /// 评价组别
        /// </summary>
        public string FK_Subject
        {
            get
            {
                return this.GetValStringByKey(PJEmpInfoAttr.FK_Subject);
            }
            set
            {
                this.SetValByKey(PJEmpInfoAttr.FK_Subject, value);
            }
        }
        /// <summary>
        /// 评价人编号
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(PJEmpInfoAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(PJEmpInfoAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// 投票人名
        /// </summary>
        public string FK_EmpT
        {
            get
            {
                return this.GetValStringByKey(PJEmpInfoAttr.FK_EmpT);
            }
            set
            {
                this.SetValByKey(PJEmpInfoAttr.FK_EmpT, value);
            }
        }
        /// <summary>
        /// 投票人的IP
        /// </summary>
        public string IP
        {
            get
            {
                return this.GetValStringByKey(PJEmpInfoAttr.IP);
            }
            set
            {
                this.SetValByKey(PJEmpInfoAttr.IP, value);
            }
        }
        /// <summary>
        /// 投票次数
        /// </summary>
        public int PJTimes
        {
            get
            {
                return this.GetValIntByKey(PJEmpInfoAttr.PJTimes);
            }
            set
            {
                this.SetValByKey(PJEmpInfoAttr.PJTimes, value);
            }
        }
        /// <summary>
        /// 投票时间
        /// </summary>
        public DateTime RDT
        {
            get
            {
                return this.GetValDateTime(PJEmpInfoAttr.RDT);
            }
            set
            {
                this.SetValByKey(PJEmpInfoAttr.RDT, value);
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
                    map.PhysicsTable = "GE_PJEmpInfo"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "评价人信息";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBString(PJEmpInfoAttr.FK_Subject, string.Empty, "评价组别", true, false, 0, 20, 20);
                    map.AddTBString(PJEmpInfoAttr.FK_Emp, string.Empty, "评价人编号", true, false, 0, 30, 10);
                    map.AddTBString(PJEmpInfoAttr.FK_EmpT, string.Empty, "评价人名称", true, false, 0, 30, 10);
                    map.AddTBString(PJEmpInfoAttr.IP, string.Empty, "IP", true, false, 0, 30, 20);
                    map.AddTBInt(PJEmpInfoAttr.PJTimes, 1, "投票次数", true, false);
                    map.AddTBDateTime(PJEmpInfoAttr.RDT, string.Empty, "投票时间", true, false);
                    #endregion 字段增加.
                    this._enMap = map;
                    return this._enMap;
                }
            }
        }
    }
    public class PJEmpInfos : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new PJEmpInfo();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public PJEmpInfos() { }
        #endregion
    }
}