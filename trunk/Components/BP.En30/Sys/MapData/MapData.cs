using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
	/// <summary>
	/// 映射基础
	/// </summary>
    public class MapDataAttr : EntityNoNameAttr
    {
        public const string PTable = "PTable";
        public const string Dtls = "Dtls";
        public const string EnPK = "EnPK";
        public const string SearchKeys = "SearchKeys";

        public const string CellsFrom = "CellsFrom";
        public const string CellsX = "CellsX";
        public const string CellsY = "CellsY";

    }
	/// <summary>
	/// 映射基础
	/// </summary>
    public class MapData : EntityNoName
    {
        public static Boolean IsEditDtlModel
        {
            get
            {
                string s = BP.Web.WebUser.GetSessionByKey("IsEditDtlModel", "0");
                if (s == "0")
                    return false;
                else
                    return true;
            }
            set
            {
                BP.Web.WebUser.SetSessionByKey("IsEditDtlModel", "1");
            }
        }

        #region 属性
        public string PTable
        {
            get
            {
                string s = this.GetValStrByKey(MapDataAttr.PTable);
                if (s == "" || s == null)
                    return this.No;
                return s;
            }
            set
            {
                this.SetValByKey(MapDataAttr.PTable, value);
            }
        }
        public int CellsX
        {
            get
            {
                return this.GetValIntByKey(MapDataAttr.CellsX);
            }
            set
            {
                this.SetValByKey(MapDataAttr.CellsX, value);
            }
        }
        public int CellsY
        {
            get
            {
                return this.GetValIntByKey(MapDataAttr.CellsY);
            }
            set
            {
                this.SetValByKey(MapDataAttr.CellsY, value);
            }
        }
        public string Dtls
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.Dtls);
            }
            set
            {
                this.SetValByKey(MapDataAttr.Dtls, value);
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string EnPK
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.EnPK);
            }
            set
            {
                this.SetValByKey(MapDataAttr.EnPK, value);
            }
        }
        /// <summary>
        /// cells from Field
        /// </summary>
        public string CellsFrom_del
        {
            get
            {
                string s = this.GetValStrByKey(MapDataAttr.CellsFrom);
                if (s.Trim() == "")
                    return null;
                return s;
            }
            set
            {
                this.SetValByKey(MapDataAttr.CellsFrom, value);
            }
        }
        
        public string SearchKeys
        {
            get
            {
                return this.GetValStrByKey(MapDataAttr.SearchKeys);
            }
            set
            {
                this.SetValByKey(MapDataAttr.SearchKeys, value);
            }
        }
        #endregion

        #region 构造方法
        public MapAttrs GenerHisTableCells
        {
            get
            {
                if ( this.CellsFrom == null)
                    return null;

                MapAttrs mapAttrs = new MapAttrs(this.No+"T");
                if (mapAttrs.Count == 0)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        MapAttr attr = new MapAttr();
                        attr.FK_MapData = this.No + "T";
                        attr.KeyOfEn = "F" + i.ToString();
                        attr.Name = "Lab" + i;
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = true;
                        attr.UIIsEnable = true;
                        attr.UIWidth = 30;
                        attr.UIIsLine = true;
                        attr.MinLen = 0;
                        attr.MaxLen = 600;
                        attr.IDX = i;
                        attr.Insert();
                    }
                    mapAttrs = new MapAttrs(this.No + "T");
                }

                return mapAttrs;

            }
        }

        public Map GenerHisMap()
        {
            MapAttrs mapAttrs = new MapAttrs(this.No);
            Map map = new Map(this.No);
            map.EnDesc = this.Name;
            map.EnType = EnType.App;
            map.DepositaryOfEntity = Depositary.None;
            map.DepositaryOfMap = Depositary.Application;

            Attrs attrs = new Attrs();
            foreach (MapAttr mapAttr in mapAttrs)
                map.AddAttr(mapAttr.HisAttr);

            if (this.SearchKeys.Contains("@") == true)
            {
                string[] strs = this.SearchKeys.Split('@');
                foreach (string s in strs)
                {
                    if (s == "" || s == null)
                        continue;
                    try
                    {
                        map.AddSearchAttr(s);
                    }
                    catch
                    {
                    }
                }

            }

            // 产生明细表。
            MapDtls dtls = new MapDtls(this.No);
            foreach (MapDtl dtl in dtls)
            {
                BP.Sys.GEDtls dtls1 = new GEDtls(dtl.No);
                map.AddDtl(dtls1, "RefPK");
            }
            return map;
        }
        public static Map GenerHisMap(string no)
        {
            if (SystemConfig.IsDebug)
            {
                MapData md = new MapData(no);
                return md.GenerHisMap();
            }
            else
            {
                Map map = BP.DA.Cash.GetMap(no);
                if (map == null)
                {
                    MapData md = new MapData(no);
                    map = md.GenerHisMap();
                    BP.DA.Cash.SetMap(no, map);
                }
                return map;
            }
        }
        /// <summary>
        /// 映射基础
        /// </summary>
        public MapData()
        {
        }
        public MapData(string mypk)
        {
            this.No = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_MapData");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "映射基础";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(MapDataAttr.No, null, "编号", true, false, 1, 20, 20);
                map.AddTBString(MapDataAttr.Name, null, "描述", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.EnPK, null, "实体主键", true, false, 0, 10, 20);
                map.AddTBString(MapDataAttr.SearchKeys, null, "查询键", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.PTable, null, "物理表", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Dtls, null, "明细表", true, false, 0, 500, 20);
                map.AddTBString(MapDataAttr.Name, null, "描述", true, false, 0, 500, 20);

                // from which filed to show the cells data. if is null or "" then unenable it..
                map.AddTBString(MapDataAttr.CellsFrom, null, "CellsFromField", true, false, 0, 500, 20);
                map.AddTBInt(MapDataAttr.CellsX, 4, "X", true, true);
                map.AddTBInt(MapDataAttr.CellsY, 5, "Y", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

         

        protected override bool beforeDelete()
        {
            MapAttrs attrs = new MapAttrs(this.No);
            attrs.Delete();
            return base.beforeDelete();
        }
        /// <summary>
        /// 生成自动的ｊｓ程序。
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="attrs"></param>
        /// <param name="attr"></param>
        /// <param name="tbPer"></param>
        /// <returns></returns>
        public static string GenerAutoFull(string pk, MapAttrs attrs, MapAttr attr, string tbPer)
        {
            string left = "\n document.forms[0]." + tbPer + "_TB" + attr.KeyOfEn + "_" + pk + ".value = ";
            string right = attr.AutoFullDoc;
            foreach (MapAttr mattr in attrs)
            {
                right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + tbPer + "_TB_" + mattr.KeyOfEn + "_" + pk + ".value) ");
            }
            return " alert( document.forms[0]." + tbPer + "_TB" + attr.KeyOfEn + "_" + pk + ".value ) ; \t\n " + left + right;
        }
        public static string GenerAutoFull_d(Entity en, MapAttrs attrs, MapAttr attr, string tbPer)
        {
            return null;
            //string script = "\n<script language='JavaScript'> function C" + en.PKVal + "(){ \n ";

            //script += " \n var leftval=0;";
            //script += " \n var rightval=0; ";

            //string rightStr = en.FormularRight;  //   a+b-c+d
            //string scriptRight = "";
            //char[] chars = attr.AutoFull.ToCharArray();
            //string para = ""; // 参数
            //foreach (char c in chars)
            //{
            //    if (c == '+' || c == '-')
            //    {
            //        scriptRight += "  parseFloat( document.forms[0]." + this.GetTBByID(para).ClientID + ".value) " + c.ToString();
            //        //float f= parseFloat( document.forms[0]."+this.GetTBByID( para ).ClientID+".value)";
            //        para = "";
            //    }
            //    else
            //    {
            //        para = para + c.ToString();
            //    }
            //}
            //scriptRight += " parseFloat( document.forms[0]." + this.GetTBByID(para).ClientID + ".value ) ";
            //scriptRight = "  rightval = parseFloat( " + scriptRight + " );  ";
            //scriptRight += "   document.forms[0]." + tbPer + attr.Key + ".value = rightval";
            //script += scriptRight;

            //script += " } </script>";
            //return script;
        }
         
    }
	/// <summary>
	/// 映射基础s
	/// </summary>
    public class MapDatas : EntitiesMyPK
	{		
		#region 构造
        /// <summary>
        /// 映射基础s
        /// </summary>
		public MapDatas()
		{
		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new MapData();
			}
		}
		#endregion
	}
}
