using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GE
{
    public class GEFavNameAttr : EntityOIDAttr
    {
        public const string FK_Emp = "FK_Emp";
        public const string Name = "Name";
    }
    public class GEFavName : EntityOID
    {
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(GEFavNameAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(GEFavNameAttr.FK_Emp, value);
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(GEFavNameAttr.Name);
            }
            set
            {
                this.SetValByKey(GEFavNameAttr.Name, value);
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
                    map.PhysicsTable = "GE_FavName"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "收藏夹名字";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBIntPKOID();
                    map.AddTBString(GEFavNameAttr.FK_Emp, string.Empty, "收藏人", true, false, 0, 20, 20);
                    map.AddTBString(GEFavNameAttr.Name, string.Empty, "文件夹名", true, false, 0, 20, 20);
                    #endregion 字段增加.

                    this._enMap = map;
                    return this._enMap;
                }
            }
        }
    }

    public class GEFavNames : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GEFavName();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public GEFavNames() { }
        #endregion
    }
}