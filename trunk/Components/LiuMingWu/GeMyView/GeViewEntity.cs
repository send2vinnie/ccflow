using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;
using BP.Port;
using System.Data;
using System.Data.SqlClient;

namespace BP.GE
{
   public class GeViewEntityAttr:EntityOIDAttr
    {
       //访问人编号
       public const string FK_Emp = "FK_Emp";
       //访问人
       public const string FK_EmpT = "FK_EmpT";
       //资源编号
       public const string RefOID = "RefOID";
       //资源类别
       public const string RefGroup = "RefGroup";
       //资源标题
       public const string Title = "Title";
       //资源地址
       public const string Url = "Url";
       //访问日期
       public const string RDT = "RDT";
    }
   public class GeViewEntity : EntityOID
   {
       public GeViewEntity()
       { 

       }
       public GeViewEntity(string _FK_Emp, string _FK_EmpT, string _RefOID, string _RefGroup, string _Title)
       {
           this.FK_Emp = _FK_Emp;
           this.FK_EmpT = _FK_EmpT;
           this.RefOID = _RefOID;
           this.RefGroup = _RefGroup;
           this.Title = _Title;
           this.Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
           this.RDT = DateTime.Now.ToString();
       }
       public GeViewEntity(string _FK_Emp, string _FK_EmpT, string _RefOID, string _RefGroup, string _Title, string _Url)
       {
           this.FK_Emp = _FK_Emp;
           this.FK_EmpT = _FK_EmpT;
           this.RefOID = _RefOID;
           this.RefGroup = _RefGroup;
           this.Title = _Title;
           this.Url = _Url;
           this.RDT = DateTime.Now.ToString();
       }

       public new void Save()
       {
           try
           {
               if (!string.IsNullOrEmpty(this.FK_Emp) && !string.IsNullOrEmpty(this.RefOID))
               {
                   string sql = "select count(*) from GE_MyView where FK_Emp='" + this.FK_Emp + "'" + " and RefOID='" + this.RefOID + "'";
                   int i = BP.DA.DBAccess.RunSQLReturnValInt(sql);
                   if (i > 0)
                   {
                       sql = "update GE_MyView set RDT='" + this.RDT + "' where FK_Emp='" + this.FK_Emp + "'" + " and RefOID='" + this.RefOID + "'";
                   }
                   else
                   {
                       sql = "insert into GE_MyView values('" + FK_Emp + "','" + FK_EmpT + "','" + RefOID + "','" + RefGroup + "','" + Title + "','" + Url + "','" + RDT + "')";
                   }
                   BP.DA.DBAccess.RunSQL(sql);
               }
           }
           catch(Exception ex)
           {
               throw new Exception(ex.ToString());
           }
       }
       /// <summary>
       /// 访问人编号
       /// </summary>
       public string FK_Emp
       {
           get
           {
               return this.GetValStringByKey(GeViewEntityAttr.FK_Emp);
           }
           set
           {
               this.SetValByKey(GeViewEntityAttr.FK_Emp, value);
           }
       }
       /// <summary>
       /// 访问人名字
       /// </summary>
       public string FK_EmpT
       {
           get
           {
               return this.GetValStringByKey(GeViewEntityAttr.FK_EmpT);
           }
           set
           {
               this.SetValByKey(GeViewEntityAttr.FK_EmpT, value);
           }
       }
       /// <summary>
       /// 资源编号
       /// </summary>
       public string RefOID
       {
           get
           {
               return this.GetValStringByKey(GeViewEntityAttr.RefOID);
           }
           set
           {
               this.SetValByKey(GeViewEntityAttr.RefOID, value);
           }
       }
       /// <summary>
       /// 资源分类
       /// </summary>
       public string RefGroup
       {
           get
           {
               return this.GetValStringByKey(GeViewEntityAttr.RefGroup); 
           }
           set
           {
               this.SetValByKey(GeViewEntityAttr.RefGroup, value);
           }
       }
       /// <summary>
       /// 标题
       /// </summary>
       public string Title
       {
           get
           {
               return this.GetValStringByKey(GeViewEntityAttr.Title);
           }
           set
           {
               this.SetValByKey(GeViewEntityAttr.Title, value);
           }
       }
       /// <summary>
       /// 连接地址
       /// </summary>
       public string Url
       {
           get
           {
               return this.GetValStringByKey(GeViewEntityAttr.Url);
           }
           set
           {
               this.SetValByKey(GeViewEntityAttr.Url, value);
           }
       }
       /// <summary>
       /// 访问日期
       /// </summary>
       public string RDT
       {
           get
           {
               return this.GetValStringByKey(GeViewEntityAttr.RDT);
           }
           set
           {
               this.SetValByKey(GeViewEntityAttr.RDT, value);
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
                   map.PhysicsTable = "GE_MyView"; // 要连接的物理表。
                   map.DepositaryOfMap = Depositary.Application;    //实体map的存放位置.
                   map.DepositaryOfEntity = Depositary.None; //实体存放位置
                   map.EnDesc = "浏览记录";       // 实体的描述.
                   #endregion

                   #region 字段
                   /*关于字段属性的增加 */
                   map.AddTBIntPKOID();
                   map.AddTBString(GeViewEntityAttr.FK_Emp, string.Empty, "编号", true, false, 0, 20, 20);
                   map.AddTBString(GeViewEntityAttr.FK_EmpT, string.Empty, "名字", true, false, 0, 20, 20);
                   map.AddTBString(GeViewEntityAttr.RefOID, string.Empty, "资源编号", true, false, 0, 20, 20);
                   map.AddTBString(GeViewEntityAttr.RefGroup, string.Empty, "资源分组", true, false, 0, 20, 20);
                   map.AddTBString(GeViewEntityAttr.Title, string.Empty, "标题", true, false, 0, 100, 20);
                   map.AddTBString(GeViewEntityAttr.Url, string.Empty, "URL", true,false, 0, 100, 20);
                   map.AddTBString(GeViewEntityAttr.RDT,string.Empty, "访问日期", true, false,0,50,20);
                   #endregion 字段增加
                   this._enMap = map;
                   return this._enMap;
               }
           }
           
       }
   }

   /// <summary>
   /// 好友
   /// </summary>
   public class GeViewEntitys : EntitiesOID
   {
       #region Entity
       /// <summary>
       /// 得到它的 Entity 
       /// </summary>
       public override Entity GetNewEntity
       {
           get
           {
               return new GeViewEntity();
           }
       }
       #endregion

       #region 构造方法
       /// <summary>
       /// 评论
       /// </summary>
       public GeViewEntitys() { }
       #endregion
   }
}
