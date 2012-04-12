using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.XML;

namespace BP.CCOA
{
    public class MenuAttr
    {
        public const string Url ="Url";

        public const string Img = "Img";

        public const string ForUser = "ForUser";

        public const string DESC = "DESC";

        public const string DFor = "DFor";
    }

    public class Menu : XmlEnNoName
    {
        public string Url
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.Url);
            }
        }

        public string Img
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.Img);
            }
        }

        public string ForUser
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.ForUser);
            }
        }

        public string DESC
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.DESC);
            }
        }

        public string DFor
        {
            get
            {
                return this.GetValStringByKey(MenuAttr.DFor);
            }
        }

        public override XmlEns GetNewEntities
        {
            get { return new Menus(); }
        }
    }

    public class Menus : XmlEns
    {

        public override XmlEn GetNewEntity
        {
            get
            {
                return new Menu();
            }
        }

        public override string File
        {
            get { return SystemConfig.PathOfWebApp + "\\CCOA\\Menu.xml"; }
        }

        public override string TableName
        {
            get { return "Item"; }
        }

        public override En.Entities RefEns
        {
            get { return null; }
        }
    }
}
