using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GE
{
    public class GEMsgDocAttr : EntityOIDAttr
    {
        public const string Title = "Title";            //标题    
        public const string Doc = "Doc";                //内容
    }

    public class GEMsgDoc : EntityOID
    {
        #region 基本属性

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(GEMsgDocAttr.Doc);
            }
            set
            {
                this.SetValByKey(GEMsgDocAttr.Doc, value);
            }
        }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(GEMsgDocAttr.Title);
            }
            set
            {
                this.SetValByKey(GEMsgDocAttr.Title, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        ///获取消息
        /// </summary>
        public GEMsgDoc()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //要连接的数据源（表示要连接到的那个系统数据库）。
                map.PhysicsTable = "GE_MsgDoc"; // 要连接的物理表。
                map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                map.DepositaryOfEntity = Depositary.None; //实体存放位置
                map.EnDesc = "消息";       // 实体的描述.
                #endregion

                #region 字段
                //map.AddTBStringPK(GEMsgDocAttr.OID, string.Empty, "主键", true, true, 0, 50, 25);
                map.AddTBIntPKOID();
                map.AddTBStringDoc(GEMsgDocAttr.Title, null, "标题", true, false);
                map.AddTBStringDoc(GEMsgDocAttr.Doc, null, "消息内容", true, false);
                this._enMap = map;
                return this._enMap;
                #endregion
            }
        }
        #endregion
    }
    /// <summary>
    /// 消息s
    /// </summary>
    public class GEMsgDocs : EntitiesOID
    {
        /// <summary>
        /// 消息s
        /// </summary>
        public GEMsgDocs()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GEMsgDoc();
            }
        }
    }
}
