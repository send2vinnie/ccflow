using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.GE
{
    public class PJTypeAttr : EntityOIDAttr
    {
        //投票分类 类型一样的为一组
        public const string PJGroup = "PJGroup";
        //编号
        public const string PJNum = "PJNum";
        //文字
        public const string Title = "Title";
        //图片
        public const string Pic = "Pic";
        //分数
        public const string Score = "Score";
        //备注信息
        public const string Note = "Note";
    }

    public class PJType : EntityOID
    {
        /// <summary>
        /// 投票组
        /// </summary>
        public string PJGroup
        {
            get
            {
                return this.GetValStringByKey(PJTypeAttr.PJGroup);
            }
            set
            {
                this.SetValByKey(PJTypeAttr.PJGroup, value);
            }
        }
        /// <summary>
        /// 投票编号
        /// </summary>
        public string PJNum
        {
            get 
            {
                return this.GetValStringByKey(PJTypeAttr.PJNum);
            }
            set
            {
                this.SetValByKey(PJTypeAttr.PJNum, value);
            }
        }
        /// <summary>
        /// 文字
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(PJTypeAttr.Title);
            }
            set
            {
                this.SetValByKey(PJTypeAttr.Title, value);
            }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic
        {
            get
            {
                return this.GetValStringByKey(PJTypeAttr.Pic);
            }
            set
            {
                this.SetValByKey(PJTypeAttr.Pic, value);
            }
        }
        /// <summary>
        /// 分数
        /// </summary>
        public int Score
        {
            get
            {
                return this.GetValIntByKey(PJTypeAttr.Score);
            }
            set
            {
                this.SetValByKey(PJTypeAttr.Score, value);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(PJTypeAttr.Note);
            }
            set
            {
                this.SetValByKey(PJTypeAttr.Note, value);
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
                    map.PhysicsTable = "GE_PJType"; // 要连接的物理表。
                    map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                    map.DepositaryOfEntity = Depositary.None; //实体存放位置
                    map.EnDesc = "投票类型";       // 实体的描述.
                    #endregion

                    #region 字段
                    /*关于字段属性的增加 */
                    map.AddTBIntPKOID();
                    map.AddTBString(PJTypeAttr.PJGroup, string.Empty, "投票分组", true, false, 0, 10, 10);
                    map.AddTBString(PJTypeAttr.PJNum, string.Empty, "评价编号", true, false, 0, 10, 10);
                    map.AddTBString(PJTypeAttr.Title, string.Empty, "标题", true, false, 0, 50, 50);
                    map.AddTBString(PJTypeAttr.Pic, string.Empty, "图片", true, false, 0, 100, 50);
                    map.AddTBInt(PJTypeAttr.Score, 0, "分数", true, false);
                    map.AddTBString(PJTypeAttr.Note, string.Empty, "备注", true, false, 0, 500, 500);
                    #endregion 字段增加
                    this._enMap = map;
                    return this._enMap;
                }
            }
        }
    }
    public class PJTypes : EntitiesOID
    {
        #region Entity
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new PJType();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 评论
        /// </summary>
        public PJTypes() { }
        #endregion
    }
}
