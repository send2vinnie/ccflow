using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.GE
{
    public class PJTotalAttr : EntityOIDAttr
    {
        //外键--(哪组投票)
        public const string FK_Subject="FK_Subject";
        //内容 选择的项
        public const string FK_Num = "FK_Num";
        //票数
        public const string Total = "Total";
    }
    public class PJTotal : EntityOID
    {
        #region 基本属性
        /// <summary>
        /// 外键
        /// </summary>
        public string FK_Subject
        {
            get
            {
                return this.GetValStringByKey(PJTotalAttr.FK_Subject);
            }
            set
            {
                this.SetValByKey(PJTotalAttr.FK_Subject, value);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string FK_Num
        {
            get
            {
                return this.GetValStringByKey(PJTotalAttr.FK_Num);
            }
            set
            {
                this.SetValByKey(PJTotalAttr.FK_Num, value);
            }
        }
        public int Total
        {
            get
            {
                return this.GetValIntByKey(PJTotalAttr.Total);
            }
            set
            {
                this.SetValByKey(PJTotalAttr.Total, value);
            }
        }
        #endregion

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
                    map.PhysicsTable = "GE_PJTotal"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "投票统计";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBString(PJTotalAttr.FK_Subject, string.Empty, "投票组", true, false, 0, 20, 20);
                    map.AddTBString(PJTotalAttr.FK_Num, string.Empty, "投票编号", true, false, 0, 10, 10);
                    map.AddTBInt(PJTotalAttr.Total, 0, "投票总和", true, false);
                    #endregion 字段增加
                    this._enMap = map;
                    return this._enMap;
                }
            }
        }
    }
    public class PJTotals : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new PJTotal();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public PJTotals() { }
        #endregion
    }
}