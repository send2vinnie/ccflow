using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.GE
{
    public class GEFavListAttr : EntityOIDAttr
    {
        public const string FK_Emp = "FK_Emp";
        public const string RDT = "RDT";
        public const string Title = "Title";
        public const string Url = "Url";
        public const string FK_FavNameID = "FK_FavNameID";
    }
    public class GEFavList : EntityOID
    {
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(GEFavListAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(GEFavListAttr.FK_Emp, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(GEFavListAttr.RDT);
            }
            set
            {
                this.SetValByKey(GEFavListAttr.RDT, value);
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey(GEFavListAttr.Title);
            }
            set
            {
                this.SetValByKey(GEFavListAttr.Title, value);
            }
        }
        public string Url
        {
            get
            {
                return this.GetValStringByKey(GEFavListAttr.Url);
            }
            set
            {
                this.SetValByKey(GEFavListAttr.Url, value);
            }
        }
        public int  FK_FavNameID
        {
            get
            {
                return this.GetValIntByKey(GEFavListAttr.FK_FavNameID);
            }
            set
            {
                this.SetValByKey(GEFavListAttr.FK_FavNameID, value);
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
                    map.PhysicsTable = "GE_FavList"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "收藏列表";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBIntPKOID();
                    map.AddTBString(GEFavListAttr.FK_Emp, string.Empty, "收藏人", true, false, 0, 20, 20);
                    map.AddTBDateTime(GEFavListAttr.RDT, "收藏时间", true, false);
                    map.AddTBString(GEFavListAttr.Title, string.Empty, "标题", true, false, 0, 100,100);
                    map.AddTBString(GEFavListAttr.Url, string.Empty, "地址", true, false, 0, 100, 100);
                    map.AddTBInt(GEFavListAttr.FK_FavNameID, 0, "文件夹", true, false);
                    #endregion 字段增加.

                    this._enMap = map;
                    return this._enMap;
                }
            }
        }

    }

    public class GEFavLists : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GEFavList();
            }
        }
        #endregion
        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public GEFavLists() { }
        #endregion
    }
}
