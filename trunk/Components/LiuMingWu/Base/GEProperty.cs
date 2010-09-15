using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;


namespace BP.GE
{
   public class GEPropertyAttr : EntityOIDAttr
    {
       public const string ID = "ID";
       public const string ControlName = "ControlName";
       public const string ControlGroup = "ControlGroup";
       public const string PropertyName = "PropertyName";
       public const string PropertyValue = "PropertyValue";
    }
   public class GEProperty : EntityOID
   {
       /// <summary>
       /// 编号
       /// </summary>
       public int ID
       {
           get
           {
               return this.GetValIntByKey(GEPropertyAttr.ID);
           }
           set
           {
               this.SetValByKey(GEPropertyAttr.ID, value);
           }
       }
       /// <summary>
       /// 控件名称
       /// </summary>
       public string ControlName
       {
           get
           {
               return this.GetValStringByKey(GEPropertyAttr.ControlName);
           }
           set
           {
               this.SetValByKey(GEPropertyAttr.ControlName, value);
           }
       }
       /// <summary>
       /// 控件组别
       /// </summary>
       public string ControlGroup
       {
           get
           {
               return this.GetValStringByKey(GEPropertyAttr.ControlGroup);
           }
           set
           {
               this.SetValByKey(GEPropertyAttr.ControlGroup, value);
           }
       }
       /// <summary>
       /// 属性名称
       /// </summary>
       public string PropertyName
       {
           get
           {
               return this.GetValStringByKey(GEPropertyAttr.PropertyName);
           }
           set
           {
               this.SetValByKey(GEPropertyAttr.PropertyName, value);
           }
       }
       /// <summary>
       /// 属性值
       /// </summary>
       public string PropertyValue
       {
           get
           {
               return this.GetValStringByKey(GEPropertyAttr.PropertyValue);
           }
           set
           {
               this.SetValByKey(GEPropertyAttr.PropertyValue, value);
           }
       }
       public override Map EnMap
       {
           get { throw new NotImplementedException(); }
       }
   }

}
