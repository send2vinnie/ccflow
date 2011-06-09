using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls; 
using System.IO;
using System.Text; 
using BP.En;
using BP.DA;
using BP.Sys;
using BP.Web;
using System.Text.RegularExpressions;
using BP.Port;
 
namespace BP
{
	/// <summary>
	///  关于对Entity扩展，的方法。
	/// </summary>
    public class EnExt
    {
     
        ///   <summary>
        ///   判断一个字符串是否全部为英语字母组成的，如是返回true，否则返回false   
        ///   </summary>   
        ///   <param   name="sQYMC">字符串</param>   
        ///   <returns></returns>   
        public static bool IsZM(string sQYMC)
        {
            //将字符串变为小写形式，在转为字符串数组   
            char[] cQYMC = sQYMC.ToLower().ToCharArray();

            //遍历数组，与字母的ascii码值比较，如果有一个不在其范围之类，则非汉语拼音   
            //a--97,z--122   
            for (int i = 0; i < sQYMC.Length; i++)
            {
                if (!(cQYMC[i] >= 97 && cQYMC[i] <= 122))
                    return false;
            }

            return true;
        }

        #region 用到ddl 的方法。
        public static string GetTextByValue(Entities ens, string no, string isNullAsVal)
        {
            try
            {
                return GetTextByValue(ens, no);
            }
            catch
            {
                return isNullAsVal;
            }
        }
        public static string GetTextByValue(Entities ens, string no)
        {
            foreach (Entity en in ens)
            {
                if (en.GetValStringByKey("No") == no)
                    return en.GetValStringByKey("Name");
            }
            if (ens.Count == 0)
                throw new Exception("@实体集合里面没有数据.");
            else
                throw new Exception("@没有找到No=" + no + "在实体里面");
        }
        #endregion


        public static string GetEnFilesUrl(Entity en)
        {
            string str = null;
            SysFileManagers ens = en.HisSysFileManagers;

            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            foreach (SysFileManager file in ens)
            {
                str += "[<a href='" + path + file.MyFilePath + "' target='f" + file.OID + "' >" + file.MyFileName + "</a>]";
            }
            return str;
            //ClassFactory
        }

        #region 关于对entity 的处理

        #region 转换dataset
        /// <summary>
        /// 把指定的ens 转换为 dataset
        /// </summary>
        /// <param name="spen">指定的ens</param>
        /// <returns>返回关系dataset</returns>
        public static DataSet ToDataSet(Entities spens)
        {

            DataSet ds = new DataSet(spens.ToString());

            /* 把主表加入DataSet */
            Entity en = spens.GetNewEntity;
            DataTable dt = new DataTable();
            if (spens.Count == 0)
            {
                QueryObject qo = new QueryObject(spens);
                dt = qo.DoQueryToTable();
            }
            else
            {
                dt = spens.ToDataTableField();
            }
            dt.TableName = en.EnDesc; //设定主表的名称。
            dt.RowChanged += new DataRowChangeEventHandler(dt_RowChanged);

            //dt.RowChanged+=new DataRowChangeEventHandler(dt_RowChanged);

            ds.Tables.Add(DealBoolTypeInDataTable(en, dt));


            foreach (EnDtl ed in en.EnMap.DtlsAll)
            {
                /* 循环主表的明细，编辑好关系并把他们放入 DataSet 里面。*/
                Entities edens = ed.Ens;
                Entity eden = edens.GetNewEntity;
                DataTable edtable = edens.RetrieveAllToTable();
                edtable.TableName = eden.EnDesc;
                ds.Tables.Add(DealBoolTypeInDataTable(eden, edtable));

                DataRelation r1 = new DataRelation(ed.Desc,
                    ds.Tables[dt.TableName].Columns[en.PK],
                    ds.Tables[edtable.TableName].Columns[ed.RefKey]);
                ds.Relations.Add(r1);


                //	int i = 0 ;

                foreach (EnDtl ed1 in eden.EnMap.DtlsAll)
                {
                    /* 主表的明细的明细。*/
                    Entities edlens1 = ed1.Ens;
                    Entity edlen1 = edlens1.GetNewEntity;

                    DataTable edlensTable1 = edlens1.RetrieveAllToTable();
                    edlensTable1.TableName = edlen1.EnDesc;
                    //edlensTable1.TableName =ed1.Desc ;


                    ds.Tables.Add(DealBoolTypeInDataTable(edlen1, edlensTable1));

                    DataRelation r2 = new DataRelation(ed1.Desc,
                        ds.Tables[edtable.TableName].Columns[eden.PK],
                        ds.Tables[edlensTable1.TableName].Columns[ed1.RefKey]);
                    ds.Relations.Add(r2);
                }

            }


            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="en"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static DataTable DealBoolTypeInDataTable(Entity en, DataTable dt)
        {

            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyDataType == DataType.AppBoolean)
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = "tmp" + attr.Key;
                    col.DataType = typeof(bool);
                    dt.Columns.Add(col);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[attr.Key].ToString() == "1")
                            dr["tmp" + attr.Key] = true;
                        else
                            dr["tmp" + attr.Key] = false;
                    }
                    dt.Columns.Remove(attr.Key);
                    dt.Columns["tmp" + attr.Key].ColumnName = attr.Key;
                    continue;
                }
                if (attr.MyDataType == DataType.AppDateTime || attr.MyDataType == DataType.AppDate)
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = "tmp" + attr.Key;
                    col.DataType = typeof(DateTime);
                    dt.Columns.Add(col);
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            dr["tmp" + attr.Key] = DateTime.Parse(dr[attr.Key].ToString());
                        }
                        catch
                        {
                            if (attr.DefaultVal.ToString() == "")
                                dr["tmp" + attr.Key] = DateTime.Now;
                            else
                                dr["tmp" + attr.Key] = DateTime.Parse(attr.DefaultVal.ToString());

                        }

                    }
                    dt.Columns.Remove(attr.Key);
                    dt.Columns["tmp" + attr.Key].ColumnName = attr.Key;
                    continue;
                }
            }
            return dt;
        }
        /// <summary>
        /// DataRowChangeEventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            throw new Exception(sender.ToString() + "  rows change ." + e.Row.ToString());
        }

        #endregion

        /// <summary>
        /// 把属性信息,与vlaue 转换为Table
        /// </summary>
        /// <param name="en">要转换的entity</param>
        /// <param name="editStyle">编辑风格</param>
        /// <returns>datatable</returns>
        public static DataTable ToTable(Entity en, int editStyle)
        {
            if (editStyle == 0)
                return EnExt.ToTable0(en);
            else
                return EnExt.ToTable1(en);
        }
        /// <summary>
        /// 用户风格0
        /// </summary>
        /// <returns></returns>
        private static DataTable ToTable0(Entity en)
        {
            string nameOfEnterInfo = en.EnDesc;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("输入项目", typeof(string)));
            dt.Columns.Add(new DataColumn(nameOfEnterInfo, typeof(string)));
            dt.Columns.Add(new DataColumn("信息输入要求", typeof(string)));

            foreach (Attr attr in en.EnMap.Attrs)
            {
                DataRow dr = dt.NewRow();
                dr["输入项目"] = attr.Desc;
                //if (attr.Key=="NodeState" && this.GetValByKey(attr.Key).ToString()=="&nbsp;")
                //throw new Exception("@error ");
                dr[nameOfEnterInfo] = en.GetValByKey(attr.Key);
                dr["信息输入要求"] = attr.EnterDesc;
                dt.Rows.Add(dr);
            }
            // 如果实体需要附件。
            if (en.EnMap.AdjunctType != AdjunctType.None)
            {
                // 加入附件信息。
                DataRow dr1 = dt.NewRow();
                dr1["输入项目"] = "附件";
                dr1[nameOfEnterInfo] = "";
                dr1["信息输入要求"] = "编辑附件";
                dt.Rows.Add(dr1);
            }
            // 明细
            foreach (EnDtl dtl in en.EnMap.Dtls)
            {
                DataRow dr = dt.NewRow();
                dr["输入项目"] = dtl.Desc;
                dr[nameOfEnterInfo] = "EnsName_" + dtl.Ens.ToString() + "_RefKey_" + dtl.RefKey;
                dr["信息输入要求"] = "请进入编辑明细";
                dt.Rows.Add(dr);
            }
            foreach (AttrOfOneVSM attr in en.EnMap.AttrsOfOneVSM)
            {
                DataRow dr = dt.NewRow();
                dr["输入项目"] = attr.Desc;
                dr[nameOfEnterInfo] = "OneVSM" + attr.EnsOfMM.ToString();
                dr["信息输入要求"] = "请进入编辑一对多的关系";
                dt.Rows.Add(dr);
            }
            return dt;

        }
        /// <summary>
        /// 用户风格1
        /// </summary>
        /// <returns></returns>
        private static DataTable ToTable1(Entity en)
        {

            string col1 = "字段名1";
            string col2 = "内容1";
            string col3 = "字段名2";
            string col4 = "内容2";

            //string enterNote=null;
            //			if (this.EnMap.Dtls.Count==0 || this.EnMap.AttrsOfOneVSM.Count==0)
            //				enterNote="内容1";
            //			else
            //				enterNote="保存后才能编辑关联信息";


            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(col1, typeof(string)));
            dt.Columns.Add(new DataColumn(col2, typeof(string)));
            dt.Columns.Add(new DataColumn(col3, typeof(string)));
            dt.Columns.Add(new DataColumn(col4, typeof(string)));


            for (int i = 0; i < en.EnMap.HisPhysicsAttrs.Count; i++)
            {
                DataRow dr = dt.NewRow();
                Attr attr = en.EnMap.HisPhysicsAttrs[i];
                dr[col1] = attr.Desc;
                dr[col2] = en.GetValByKey(attr.Key);

                i++;
                if (i == en.EnMap.HisPhysicsAttrs.Count)
                {
                    dt.Rows.Add(dr);
                    break;
                }
                attr = en.EnMap.HisPhysicsAttrs[i];
                dr[col3] = attr.Desc;
                dr[col4] = en.GetValByKey(attr.Key);
                dt.Rows.Add(dr);
            }


            // 如果实体需要附件。
            if (en.EnMap.AdjunctType != AdjunctType.None)
            {
                // 加入附件信息。
                DataRow dr1 = dt.NewRow();
                dr1[col1] = "附件";
                dr1[col2] = "编辑附件";
                //dr["输入项目2"]="附件信息";

                dt.Rows.Add(dr1);
            }
            // 明细
            foreach (EnDtl dtl in en.EnMap.Dtls)
            {
                DataRow dr = dt.NewRow();
                dr[col1] = dtl.Desc;
                dr[col2] = "EnsName_" + dtl.Ens.ToString() + "_RefKey_" + dtl.RefKey;
                //dr["输入项目2"]="明细信息";
                dt.Rows.Add(dr);
            }
            // 多对多的关系
            foreach (AttrOfOneVSM attr in en.EnMap.AttrsOfOneVSM)
            {
                DataRow dr = dt.NewRow();
                dr[col1] = attr.Desc;
                dr[col2] = "OneVSM" + attr.EnsOfMM.ToString();
                //dr["输入项目2"]="一对多的关系";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region 张
        /// <summary>
        /// 通过一个集合，一个key，一个分割符号，获得这个属性的子串。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="listspt"></param>
        /// <returns></returns>
        public static string GetEnsString(Entities ens, string key, string listspt)
        {
            string str = "";
            foreach (Entity en in ens)
            {
                str += en.GetValByKey(key) + listspt;
            }
            return str;
        }
        /// <summary>
        /// 通过一个集合，一个分割符号，获得这个属性的子串。
        /// </summary>		
        /// <param name="listspt"></param>
        /// <returns></returns>
        public static string GetEnsString(Entities ens, string listspt)
        {
            return GetEnsString(ens, ens.GetNewEntity.PK, listspt);
        }
        /// <summary>
        /// 通过一个集合获得这个属性的子串。
        /// </summary>		
        /// <param name="listspt"></param>
        /// <returns></returns>
        public static string GetEnsString(Entities ens)
        {
            return GetEnsString(ens, ens.GetNewEntity.PK, ";");
        }
        #endregion

    }
	/// <summary>
	/// 数据检查级别
	/// </summary>
    public enum DBLevel
    {
        /// <summary>
        /// 低,只出报告,不操作任何数据
        /// </summary>
        Low = 1,
        /// <summary>
        /// 中,出检查报告,删除外键的左右空格.
        /// </summary>
        Middle = 2,
        /// <summary>
        /// 高,删除对应不上的数据.
        /// </summary>
        High = 3,
    }
	/// <summary>
	/// PageBase 的摘要说明。
	/// </summary>
	public class PubClass 
	{
        private static string _KeyFields = null;
        public static string KeyFields
        {
            get
            {
                if (_KeyFields == null)
                    _KeyFields = BP.DA.DataType.ReadTextFile(SystemConfig.PathOfData + "\\Sys\\FieldKeys.txt");
                return _KeyFields;
            }
        }
        public static bool IsNum(string str)
        {
            Boolean strResult;
            String cn_Regex = @"^[\u4e00-\u9fa5]+$";
            if (Regex.IsMatch(str, cn_Regex))
            {
                strResult = true;
            }
            else
            {
                strResult = false;
            }
            return strResult;
        }

        public static bool IsCN(string str)
        {
            Boolean strResult;
            String cn_Regex = @"^[\u4e00-\u9fa5]+$";
            if (Regex.IsMatch(str, cn_Regex))
            {
                strResult = true;
            }
            else
            {
                strResult = false;
            }
            return strResult;
        }

        public static bool IsImg(string ext)
        {
            ext = ext.Replace(".", "").ToLower();
            switch (ext)
            {
                case "gif":
                    return true;
                case "jpg":
                    return true;
                case "bmp":
                    return true;
                case "png":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 按照比例数小
        /// </summary>
        /// <param name="ObjH">目标高度</param>
        /// <param name="factH">实际高度</param>
        /// <param name="factW">实际宽度</param>
        /// <returns>目标宽度</returns>
        public static int GenerImgW_del(int ObjH, int factH, int factW, int isZeroAsWith)
        {
            if (factH == 0 || factW == 0)
                return isZeroAsWith;

            decimal d= decimal.Parse(ObjH.ToString()) / decimal.Parse(factH.ToString()) * decimal.Parse( factW.ToString()) ;

            try
            {
                return int.Parse(d.ToString("0"));
            }
            catch(Exception ex)
            {
                throw new Exception(d.ToString() +ex.Message );
            }
        }

        /// <summary>
        /// 按照比例数小
        /// </summary>
        /// <param name="ObjH">目标高度</param>
        /// <param name="factH">实际高度</param>
        /// <param name="factW">实际宽度</param>
        /// <returns>目标宽度</returns>
        public static int GenerImgH(int ObjW, int factH, int factW, int isZeroAsWith)
        {
            if (factH == 0 || factW == 0)
                return isZeroAsWith;

            decimal d = decimal.Parse(ObjW.ToString()) / decimal.Parse(factW.ToString()) * decimal.Parse(factH.ToString());

            try
            {
                return int.Parse(d.ToString("0"));
            }
            catch (Exception ex)
            {
                throw new Exception(d.ToString() + ex.Message);
            }
        }


        public static string FilesViewStr(string enName, object pk)
        {
            string url = System.Web.HttpContext.Current.Request.ApplicationPath + "/Comm/FileManager.aspx?EnsName=" + enName + "&PK=" + pk.ToString();
            string strs = "";
            SysFileManagers ens = new SysFileManagers(enName, pk.ToString());
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;

            foreach (SysFileManager file in ens)
            {
                strs += "<img src='" + path + "/Images/FileType/" + file.MyFileExt.Replace(".", "") + ".gif' border=0 /><a href='" + path + file.MyFilePath + "' target='_blank' >" + file.MyFileName + file.MyFileExt + "</a>&nbsp;";
                if (file.Rec == WebUser.No)
                {
                    strs += "<a title='打开它' href=\"javascript:DoAction('" + path + "/Comm/Do.aspx?ActionType=1&OID=" + file.OID + "&EnsName=" + enName + "&PK=" + pk + "','删除文件《" + file.MyFileName + file.MyFileExt + "》')\" ><img src='" + path + "/Images/Btn/delete.gif' border=0 alt='删除此附件' /></a>&nbsp;";
                }
            }
            return strs;
        }
		public static string GenerLabelStr(string title)
		{
			string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (path == "" || path == "/")
                path = "..";

			string str="";
			str+="<TABLE  height='100%' cellPadding='0' background='"+path+"/Images/DG_bgright.gif'>";
			str+="<TBODY>";
			str+="<TR   >";
			str+="<TD  >";
			str+="<IMG src='"+path+"/Images/DG_Title_Left.gif' border='0'></TD>";
			str+="<TD style='font-size:14px'  vAlign='bottom' noWrap background='"+path+"/Images/DG_Title_BG.gif'>&nbsp;";
			str+=" &nbsp;<b>"+title+"</b>&nbsp;&nbsp;";
			str+="</TD>";
			str+="<TD>";
			str+="<IMG src='"+path+"/Images/DG_Title_Right.gif' border='0'></TD>";
			str+="</TR>";
			str+="</TBODY>";
			str+="</TABLE>";
			return str;		 
			//return str;
		}
        /// <summary>
        /// 将汉字转换成拼音
        /// </summary>
        /// <param name="str">要转换的汉字</param>
        /// <returns>返回的拼音</returns>
        public string Chs2Pinyin(string str)
        {
            return BP.DA.chs2py.convert(str); 
        }

		public static string GenerTablePage(DataTable dt , string title )
		{
		
			string str="<Table id='tb' class=Table >";

            str += "<caption>" + title + "</caption>";
           

			// 标题
			str+="<TR>";
			foreach(DataColumn dc in dt.Columns)
			{
				str+= "<TD class='DGCellOfHeader"+BP.Web.WebUser.Style+"' nowrap >"+dc.ColumnName+"</TD>";
			}
			str+="</TR>";
			 
			//内容
			foreach(DataRow dr in dt.Rows)
			{
				str+="<TR>";

		
				foreach(DataColumn dc in dt.Columns)
				{
					//string doc=dr[dc.ColumnName];
					str+= "<TD nowrap=true >&nbsp;"+dr[dc.ColumnName]+"</TD>";
				}
				str+="</TR>";
			}
			str+="</Table>";
			return str;
		}
		/// <summary>
		/// 得到一个表从xml 数据里面
		/// </summary>
		/// <param name="tbname">物理表</param>
		/// <returns></returns>
		public static DataTable GetTableFromXmlByTBName(string xmlname, string tbname)
		{
			DataSet ds = new DataSet();
			ds.ReadXml("D:\\WebApp\\WF\\Data\\XML\\KHZB.xml");
			return  ds.Tables[tbname];
		}
		/// <summary>
		/// 产生临时文件名称
		/// </summary>
		/// <param name="hz"></param>
		/// <returns></returns>
		public static string GenerTempFileName(string hz)
		{
			return Web.WebUser.No+DateTime.Now.ToString("MMddhhmmss")+"."+hz;
		}
		public static void DeleteTempFiles()
		{
			//string[] strs = System.IO.Directory.GetFiles( MapPath( SystemConfig.TempFilePath )) ;
			string[] strs = System.IO.Directory.GetFiles(   SystemConfig.PathOfTemp  ) ;

			foreach(string s in strs)
			{
				System.IO.File.Delete(s);
			}
		}
		/// <summary>
		/// 重新建立索引
		/// </summary>
		public static void ReCreateIndex()
		{
			ArrayList als = ClassFactory.GetObjects("BP.En.Entity");
			string sql="";
			foreach(object obj in als)
			{
				Entity en = (Entity)obj;
				if (en.EnMap.EnType ==EnType.View)
					continue;
				sql+="IF EXISTS( SELECT name  FROM  sysobjects WHERE  name='"+en.EnMap.PhysicsTable+"') <BR> DROP TABLE "+en.EnMap.PhysicsTable+"<BR>";
				sql+="CREATE TABLE "+en.EnMap.PhysicsTable+" ( <BR>";
				sql+="";
			}


		}
		public static void DBIOToAccess()
		{
			ArrayList al =  BP.DA.ClassFactory.GetObjects("BP.En.Entities");
			PubClass.DBIO(DBType.Access,al,false);
		}
        /// <summary>
        /// 检查所有的物理表
        /// </summary>
        public static void CheckAllPTable(string nameS)
        {
            ArrayList al = BP.DA.ClassFactory.GetObjects("BP.En.Entities");
            foreach (Entities ens in al)
            {
                if (ens.ToString().Contains(nameS) == false)
                    continue;


                try
                {
                    Entity en = ens.GetNewEntity;
                    en.CheckPhysicsTable();
                }
                catch
                {
 
                }

            }

        }
        
		/// <summary>
		/// 数据传输
		/// </summary>
		/// <param name="dbtype">对象</param>
		/// <returns></returns>
        public static void DBIO(DA.DBType dbtype, ArrayList als, bool creatTableOnly)
        {
            foreach (Entities ens in als)
            {
                Entity myen = ens.GetNewEntity;
                if (myen.EnMap.EnType == EnType.View)
                    continue;


                #region create table
                switch (dbtype)
                {

                    case DBType.Oracle9i:
                        try
                        {
                           
                            DBAccessOfOracle9i.RunSQL("drop table " + myen.EnMap.PhysicsTable);
                        }
                        catch
                        {
                        }
                        try
                        {
                            DBAccessOfOracle9i.RunSQL(SqlBuilder.GenerCreateTableSQLOfOra(myen));
                        }
                        catch
                        {

                        }

                        break;
                    case DBType.SQL2000:
                        try
                        {
                            if (myen.EnMap.PhysicsTable.Contains("."))
                                continue;

                            if (DBAccessOfMSSQL2000.IsExitsObject(myen.EnMap.PhysicsTable))
                                continue;

                            DBAccessOfMSSQL2000.RunSQL("drop table " + myen.EnMap.PhysicsTable);
                        }
                        catch
                        {
                        }
                        DBAccessOfMSSQL2000.RunSQL(SqlBuilder.GenerCreateTableSQLOfMS(myen));
                        break;
                    case DBType.Access:
                        try
                        {
                            DBAccessOfOLE.RunSQL("drop table " + myen.EnMap.PhysicsTable);
                        }
                        catch
                        {
                        }
                        DBAccessOfOLE.RunSQL(SqlBuilder.GenerCreateTableSQLOf_OLE(myen));
                        break;
                    default:
                        throw new Exception("error :");

                }
                #endregion

                if (creatTableOnly)
                    return;

                try
                {
                    QueryObject qo = new QueryObject(ens);
                    qo.DoQuery();
                    // ens.RetrieveAll(1000);
                }
                catch
                {
                    continue;
                }

                #region insert data
                foreach (Entity en in ens)
                {
                    try
                    {
                        switch (dbtype)
                        {
                            case DBType.Oracle9i:
                                DBAccessOfOracle9i.RunSQL(SqlBuilder.Insert(en));
                                break;
                            case DBType.SQL2000:
                                DBAccessOfMSSQL2000.RunSQL(SqlBuilder.Insert(en));
                                break;
                            case DBType.Access:
                                DBAccessOfOLE.RunSQL(SqlBuilder.InsertOFOLE(en));
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.DefaultLogWriteLineError( dbtype.ToString()+ "bak出现错误：" + ex.Message);
                    }
                }
                #endregion
            }
        }

		#region 系统调度
        public static string GenerDBOfOreacle()
        {
            ArrayList als = ClassFactory.GetObjects("BP.En.Entity");
            string sql = "";
            foreach (object obj in als)
            {
                Entity en = (Entity)obj;
                sql += "IF EXISTS( SELECT name  FROM  sysobjects WHERE  name='" + en.EnMap.PhysicsTable + "') <BR> DROP TABLE " + en.EnMap.PhysicsTable + "<BR>";
                sql += "CREATE TABLE " + en.EnMap.PhysicsTable + " ( <BR>";
                sql += "";
            }
            //DA.Log.DefaultLogWriteLine(LogType.Error,msg.Replace("<br>@","\n") ); // 
            return sql;
        }
		public static string DBRpt(DBLevel level)
		{
			// 取出全部的实体
			ArrayList als = ClassFactory.GetObjects("BP.En.Entities");
			string msg="";
			foreach(object obj in als)
			{
				Entities ens = (Entities)obj;
                try
                {
                    msg += DBRpt1(level, ens);
                }
                catch(Exception ex)
                {
                    msg += "<hr>" + ens.ToString() + "体检失败:" + ex.Message;
                }
			}
			//DA.Log.DefaultLogWriteLine(LogType.Error,msg.Replace("<br>@","\n") ); // 
			return msg;
		}
        private static void RepleaceFieldDesc(Entity en)
        {
            string tableId = DBAccess.RunSQLReturnVal("select ID from sysobjects WHERE name='" + en.EnMap.PhysicsTable + "' AND xtype='U'").ToString();

            if (tableId == null || tableId == "")
                return;

            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

            }
        }
        /// <summary>
        /// 为表增加注释
        /// </summary>
        /// <returns></returns>
        public static string AddComment()
        {
            // 取出全部的实体
            ArrayList als = ClassFactory.GetObjects("BP.En.Entities");
            string msg = "";
            foreach (object obj in als)
            {
                Entities ens = (Entities)obj;
                Entity  en = ens.GetNewEntity;
                try
                {
                    switch (en.EnMap.EnDBUrl.DBType)
                    {
                        case DBType.Oracle9i:
                             AddCommentForTable_Ora(en);
                            break;
                        default:
                            AddCommentForTable_MS(en);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    msg += "<hr>" + ens.ToString() + "体检失败:" + ex.Message;
                }
            }
            //DA.Log.DefaultLogWriteLine(LogType.Error,msg.Replace("<br>@","\n") ); // 
            return msg;

        }
        public static void AddCommentForTable_Ora(Entity en)
        {
            if (en.EnMap.EnType == EnType.View
            || en.EnMap.EnType == EnType.ThirdPartApp)
            {
                return;
            }

            en.RunSQL("comment on table " + en.EnMap.PhysicsTable + " IS '" + en.EnDesc + "'");


            SysEnums ses = new SysEnums();
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                switch (attr.MyFieldType)
                {
                    case FieldType.Normal:
                        en.RunSQL("comment on table " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + en.EnDesc + "'");
                        break;
                    case FieldType.Enum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        en.RunSQL("comment on table " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + en.EnDesc + ",枚举类型:" + ses.ToDesc() + "'");
                        break;
                    case FieldType.PKEnum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        en.RunSQL("comment on table " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + en.EnDesc + ", 主键:枚举类型:" + ses.ToDesc() + "'");
                        break;
                    case FieldType.FK:
                        Entity myen = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        en.RunSQL("comment on table " + en.EnMap.PhysicsTable + "." + attr.Field + " IS " + en.EnDesc + ", 外键:对应物理表:" + myen.EnMap.PhysicsTable + ",表描述:" + myen.EnDesc);
                        break;
                    case FieldType.PKFK:
                        Entity myen1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        en.RunSQL("comment on table " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + en.EnDesc + ", 主外键:对应物理表:" + myen1.EnMap.PhysicsTable + ",表描述:" + myen1.EnDesc + "'");
                        break;
                    default:
                        break;
                }
            }
        }
		/// <summary>
		/// 为表增加解释
		/// </summary>
		/// <param name="en"></param>
        public static void AddCommentForTable_MS(Entity en)
		{

		//	 comment on table emp is 'Employee'

			en.RunSQL("comment on table "+ en.EnMap.PhysicsTable +" IS '"+en.EnDesc+"'");

 
			SysEnums  ses = new SysEnums();
			foreach(Attr attr in en.EnMap.Attrs)
			{
				if (attr.MyFieldType==FieldType.RefText)
					continue;
				switch(attr.MyFieldType)
				{
					case FieldType.Normal:
						en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+"'");
						break;
					case FieldType.Enum:
                        ses = new SysEnums(attr.Key, attr.UITag);
						en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+",枚举类型:"+ses.ToDesc()+"'" );
						break;
					case FieldType.PKEnum:
                        ses = new SysEnums(attr.Key, attr.UITag);
						en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+", 主键:枚举类型:"+ses.ToDesc()+"'" );
						break;
					case FieldType.FK:
                        Entity myen = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
						en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS "+en.EnDesc+", 外键:对应物理表:"+myen.EnMap.PhysicsTable+",表描述:"+myen.EnDesc  );
						break;
					case FieldType.PKFK:
                        Entity myen1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
						en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+", 主外键:对应物理表:"+myen1.EnMap.PhysicsTable+",表描述:"+myen1.EnDesc+"'"  );
						break;
					default:
						break;
				}
			}
			 
		}
		/// <summary>
		/// 产程系统报表，如果出现问题，就写入日志里面。
		/// </summary>
		/// <returns></returns>
        public static string DBRpt1(DBLevel level, Entities ens)
        {

            //
            //			try
            //			{
            //			 
            //				PubClass.AddExpForTable(ens.GetNewEntity);
            //			}
            //			catch(Exception ex)
            //			{
            //				return ex.Message;
            //			}
            //return "";

            Entity en = ens.GetNewEntity;
            if (en.EnMap.EnDBUrl.DBUrlType != DBUrlType.AppCenterDSN)
                return null;

            if (en.EnMap.EnType == EnType.ThirdPartApp)
                return null;

            if (en.EnMap.EnType == EnType.View)
                return null;

            if (en.EnMap.EnType == EnType.Ext)
                return null;

            // 检测物理表的字段。
            en.CheckPhysicsTable();
            string msg = "";
            if (level == DBLevel.High)
            {
                try
                {
                    DBAccess.RunSQL("update pub_emp set AuthorizedAgent='1' WHERE AuthorizedAgent='0' ");
                }
                catch
                {
                }
            }
            string table = en.EnMap.PhysicsTable;
            Attrs fkAttrs = en.EnMap.HisFKAttrs;
            if (fkAttrs.Count == 0)
                return msg;
            int num = 0;
            string sql;
            //string msg="";
            foreach (Attr attr in fkAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                string enMsg = "";
                try
                {
                    #region 更新他们，去掉左右空格，因为外键不能包含左右空格。
                    if (level == DBLevel.Middle || level == DBLevel.High)
                    {
                        /*如果是高中级别,就去掉左右空格*/
                        if (attr.MyDataType == DataType.AppString)
                        {
                            DBAccess.RunSQL("UPDATE " + en.EnMap.PhysicsTable + " SET " + attr.Field + " = rtrim( ltrim(" + attr.Field + ") )");
                        }
                    }
                    #endregion

                    #region 处理关联表的情况.
                    Entities refEns = attr.HisFKEns; // ClassFactory.GetEns(attr.UIBindKey);
                    Entity refEn = refEns.GetNewEntity;

                    //取出关联的表。
                    string reftable = refEn.EnMap.PhysicsTable;
                    //sql="SELECT COUNT(*) FROM "+en.EnMap.PhysicsTable+" WHERE "+attr.Key+" is null or len("+attr.Key+") < 1 ";
                    // 判断外键表是否存在。

                    sql = "SELECT COUNT(*) FROM  sysobjects  WHERE  name = '" + reftable + "'";
                    //num=DA.DBAccess.RunSQLReturnValInt(sql,0);
                    if (DBAccess.IsExitsObject(reftable) == false)
                    {
                        //报告错误信息
                        enMsg += "<br>@检查实体：" + en.EnDesc + ",字段 " + attr.Key + " , 字段描述:" + attr.Desc + " , 外键物理表:" + reftable + "不存在:" + sql;
                    }
                    else
                    {
                        Attr attrRefKey = refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue); // 去掉主键的左右 空格．
                        if (attrRefKey.MyDataType == DataType.AppString)
                        {
                            if (level == DBLevel.Middle || level == DBLevel.High)
                            {
                                /*如果是高中级别,就去掉左右空格*/
                                DBAccess.RunSQL("UPDATE " + reftable + " SET " + attrRefKey.Field + " = rtrim( ltrim(" + attrRefKey.Field + ") )");
                            }
                        }

                        Attr attrRefText = refEn.EnMap.GetAttrByKey(attr.UIRefKeyText);  // 去掉主键 Text 的左右 空格．

                        if (level == DBLevel.Middle || level == DBLevel.High)
                        {
                            /*如果是高中级别,就去掉左右空格*/
                            DBAccess.RunSQL("UPDATE " + reftable + " SET " + attrRefText.Field + " = rtrim( ltrim(" + attrRefText.Field + ") )");
                        }

                    }
                    #endregion

                    #region 外键的实体是否为空
                    switch (en.EnMap.EnDBUrl.DBType)
                    {
                        case DBType.Oracle9i:
                            sql = "SELECT COUNT(*) FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " is null or length(" + attr.Field + ") < 1 ";
                            break;
                        default:
                            sql = "SELECT COUNT(*) FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " is null or len(" + attr.Field + ") < 1 ";
                            break;
                    }

                    num = DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    if (num == 0)
                    {
                    }
                    else
                    {
                        enMsg += "<br>@检查实体：" + en.EnDesc + ",物理表:" + en.EnMap.PhysicsTable + "出现" + attr.Key + "," + attr.Desc + "不正确,共有[" + num + "]行记录没有数据。" + sql;
                    }
                    #endregion

                    #region 是否能够对应到外键
                    //是否能够对应到外键。
                    sql = "SELECT COUNT(*) FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " NOT IN ( SELECT " + refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue).Field + " FROM " + reftable + "	 ) ";
                    num = DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    if (num == 0)
                    {
                    }
                    else
                    {
                        /*如果是高中级别.*/
                        string delsql = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " NOT IN ( SELECT " + refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue).Field + " FROM " + reftable + "	 ) ";
                        //int i =DBAccess.RunSQL(delsql);
                        enMsg += "<br>@" + en.EnDesc + ",物理表:" + en.EnMap.PhysicsTable + "出现" + attr.Key + "," + attr.Desc + "不正确,共有[" + num + "]行记录没有关联到数据，请检查物理表与外键表。" + sql + "如果您想删除这些对应不上的数据请运行如下SQL: " + delsql + " 请慎重执行.";
                    }
                    #endregion

                    #region 判断 主键
                    //DBAccess.IsExits("");
                    #endregion
                }
                catch (Exception ex)
                {
                    enMsg += "<br>@" + ex.Message;
                }

                if (enMsg != "")
                {
                    msg += "<BR><b>-- 检查[" + en.EnDesc + "," + en.EnMap.PhysicsTable + "]出现如下问题,类名称:" + en.ToString() + "</b>";
                    msg += enMsg;
                }
            }
            return msg;
        }
        public static void AddNoteForEns()
        {
 
        }
		/// <summary>
		/// 初始化系统实体，要做的工作是。
		/// 1，初始化所有的Sys_Ens。
		/// 2，初始化所有的Sys_EnsPowerAble。
		/// 3，为每个用户初始化他的实体访问控制。
		/// </summary>
		public static void InitSysEns_del()
		{
			#region 检查字段与物理表的长度。

			ArrayList als =BP.DA.ClassFactory.GetObjects("BP.En.Entity");
		
			string msg="...";
			foreach(Entity en in als)
			{
				if (en.EnMap.PhysicsTable.Length > 26)
					msg+="\n类"+en.EnDesc+"物理表["+en.EnMap.PhysicsTable+"]的长度为大于25，不符合oracle 命名规则。";
				foreach(Attr attr in en.EnMap.Attrs)
				{
					if (attr.MyFieldType==FieldType.RefText)
						continue;

					if (attr.Field.Length > 25)
						msg+="\n类"+en.EnDesc+"物理表["+en.EnMap.PhysicsTable+"]的字段["+attr.Field+"]长度为大于25，不符合oracle 命名规则。";
				}
			}
			if (msg.Length>9)
				if ( SystemConfig.AppCenterDBType ==DBType.Oracle9i)
					throw new Exception("物理表命名错误：\n"+msg);
			 


			#endregion

			try
			{
				ArrayList al =BP.DA.ClassFactory.GetObjects("BP.En.Entities");				
				//				string sql ="";
				//				string sql1="";
                DBAccess.RunSQL("delete FROM  from Sys_Ens");
                DBAccess.RunSQL("delete FROM  from Sys_EnsPowerAble");
				 

				// 初始化系统实体。
				foreach(Object o in al)
				{
					try
					{
						Entities ens= o as Entities;
						Entity en= ens.GetNewEntity;

						
						en.CheckPhysicsTable(); // 检查物理表是否完整。

						


						Map map = ens.GetNewEntity.EnMap;
						SysEn sysEn = new SysEn( ens.ToString() );
						if (map.EnType==EnType.PowerAble)
						{
							//SysEnPowerAble sysEn1 = new SysEnPowerAble( ens.ToString() );
						}

						// 判断物理表是否存在.
					}
					catch(Exception ex)
					{
						throw new Exception("@初始化系统实体出现错误："+ex.Message);
					}
				}

				// 初始化访问控制。删除已经去掉的实体控制。
                DBAccess.RunSQL("DELETE FROM  Sys_EnsUAC WHERE FK_Ens NOT IN (SELECT FK_Ens FROM  Sys_EnsPowerAble ) ");
				
				Emps emps = new Emps();
				emps.RetrieveAll(1000);

                //SysEnPowerAbles pas = new SysEnPowerAbles();
                //pas.RetrieveAll(1000);		 

//				foreach(Emp emp in emps)
//				{
//					foreach(SysEnPowerAble pa in pas)
//					{
//						SysEnsUAC uac = new SysEnsUAC();
//						//uac.EmpID = emp.OID;
//						uac.FK_Ens = pa.EnsEnsName ;
//						if ( uac.IsExits==false)
//							uac.Insert();
//					}
//				} 
			}
			catch(Exception ex)
			{
				throw ex;
			}


			

		}
		#endregion

		
		#region 转化格式  chen
       

		/// <summary>
		/// 将某控件中的数据转化为Excel文件
		/// </summary>
		/// <param name="ctl"></param>
		public static void ToExcel(System.Web.UI.Control ctl ,string filename)  
		{
			HttpContext.Current.Response.Charset ="GB2312";	
			HttpContext.Current.Response.AppendHeader("Content-Disposition","attachment;filename="+ filename +".xls");
			HttpContext.Current.Response.ContentEncoding =System.Text.Encoding.GetEncoding("GB2312"); 
			HttpContext.Current.Response.ContentType ="application/ms-excel";//"application/ms-excel";
			//image/JPEG;text/HTML;image/GIF;application/ms-msword
			ctl.Page.EnableViewState =false;
			System.IO.StringWriter  tw = new System.IO.StringWriter() ;
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter (tw);
			ctl.RenderControl(hw);
			HttpContext.Current.Response.Write(tw.ToString());
			HttpContext.Current.Response.End();
		}
		/// <summary>
		/// 将某控件中的数据转化为Word文件
		/// </summary>
		/// <param name="ctl"></param>
		public static void ToWord(System.Web.UI.Control ctl ,string filename)  
		{
			HttpContext.Current.Response.Charset ="GB2312";	
			HttpContext.Current.Response.AppendHeader("Content-Disposition","attachment;filename="+ filename +".doc");
			HttpContext.Current.Response.ContentEncoding =System.Text.Encoding.GetEncoding("GB2312"); 
			HttpContext.Current.Response.ContentType ="application/ms-msword";//image/JPEG;text/HTML;image/GIF;application/ms-excel
			ctl.Page.EnableViewState =false;			
			System.IO.StringWriter  tw = new System.IO.StringWriter() ;
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter (tw);
			ctl.RenderControl(hw);
			HttpContext.Current.Response.Write(tw.ToString());
			//HttpContext.Current.Response.End();
		}

        public static void OpenWordDoc(string filepath, string tempName)
        {
            //PubClass.WinOpen(filepath);
            //return;

            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + tempName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/ms-msword";  //image/JPEG;text/HTML;image/GIF;application/ms-excel
            //HttpContext.Current.EnableViewState =false;
            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }
        public static void OpenWordDocV2(string filepath, string tempName)
        {
            FileInfo DownloadFile = new FileInfo(filepath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(tempName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
            HttpContext.Current.Response.WriteFile(DownloadFile.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }
		#endregion
		

		#region

		#region
		public static void To(string url)
		{
			 System.Web.HttpContext.Current.Response.Redirect(url,true);
		}
		public static void Print(string url)
		{
			System.Web.HttpContext.Current.Response.Write( "<script language='JavaScript'> var newWindow =window.open('"+url+"','p','width=0,top=10,left=10,height=1,scrollbars=yes,resizable=yes,toolbar=yes,location=yes,menubar=yes') ; newWindow.focus(); </script> ");
		}

        public static void WinClose(string reVal)
        {
            string clientscript = "<script language='javascript'> window.returnValue = '" + reVal + "'; window.close(); </script>";
            System.Web.HttpContext.Current.Response.Write(clientscript);
        }
        
        public static void WinClose()
        {
            System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>  window.close(); </script> ");
        }
        public static void Open(string url)
        {
          //  System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",scrollbars=yes,resizable=yes,toolbar=false,location=false') ; newWindow.focus(); </script> ");
            System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','p' ) ; newWindow.focus(); </script> ");
        }
		public static void WinOpen(string url)
		{
			PubClass.WinOpen(url,"","msg",300,300);
		}
        public static void WinOpen(string url,int w, int h)
        {
            PubClass.WinOpen(url, "", "msg", w, h);
        }
		public static void WinOpen(string url,string title,string winName, int width, int height)
		{
            PubClass.WinOpen(url, title, winName, width, height, 100, 200);
		}
        public static void WinOpen(string url, string title, int width, int height)
        {
            PubClass.WinOpen(url, title, "ActivePage", width, height, 100, 200);
        }
        public static void WinOpen(string url, string title, string winName, int width, int height, int top, int left)
        {
            url = url.Replace("<", "[");
            url = url.Replace(">", "]");
            url = url.Trim();
            title = title.Replace("<", "[");
            title = title.Replace(">", "]");
            title = title.Replace("\"", "‘");
            if (top == 0 && left == 0)
                System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",scrollbars=yes,resizable=yes,toolbar=false,location=false') ; </script> ");
            else
                System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",left=" + left + ",height=" + height + ",scrollbars=yes,resizable=yes,toolbar=false,location=false');</script>");
        }
		/// <summary>
		/// 输出到页面上红色的警告。
		/// </summary>
		/// <param name="msg">消息</param>
        protected void ResponseWriteRedMsg(string msg)
        {
            //this.Response.Write("<BR><font color='red' size='"+MsgFontSize.ToString()+"' > <b>"+msg+"</b></font>");
            //if (msg.Length < 200)
            //	return ;
            msg = msg.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            string url = System.Web.HttpContext.Current.Request.ApplicationPath + "/Comm/Port/ErrorPage.aspx";
            WinOpen(url, "警告", msg + DateTime.Now.ToString("mmss"), 500, 400, 150, 270);
        }
		/// <summary>
		/// 输出到页面上蓝色的信息。
		/// </summary>
		/// <param name="msg">消息</param>
		public static void ResponseWriteBlueMsg(string msg)
		{ 
			 
			if (SystemConfig.IsBSsystem)
			{
				msg=msg.Replace("@","<BR>@");
				System.Web.HttpContext.Current.Session["info"]=msg;
				string url=System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Port/InfoPage.aspx";
				WinOpen(url,"信息","sysmsg",500,400,150,270);
			}
			else
			{
				Log.DebugWriteInfo(msg);
			}
		}
		/// <summary>
		/// 保存成功
		/// </summary>
		public static void ResponseWriteBlueMsg_SaveOK()
		{
			//this.Alert("保存成功!");
			 
			ResponseWriteBlueMsg("保存成功!");
		}
		/// <summary>
		/// ResponseWriteBlueMsg_DeleteOK
		/// </summary>
		public static void ResponseWriteBlueMsg_DeleteOK()
		{			
			//this.Alert("删除成功!");
			ResponseWriteBlueMsg("删除成功!");
		}
		/// <summary>
		/// ResponseWriteBlueMsg_UpdataOK
		/// </summary>
		public static void ResponseWriteBlueMsg_UpdataOK()
		{			
			// this.Alert("更新成功!");
			ResponseWriteBlueMsg("更新成功!");
		}
		/// <summary>
		/// 输出到页面上黑色的信息。
		/// </summary>
		/// <param name="msg">消息</param>
		public static void ResponseWriteBlackMsg(string msg)
		{			
			System.Web.HttpContext.Current.Response.Write("<font color='Black' size=5 ><b>"+msg+"</b></font>");
		}
		public static void ResponseSript(string Sript)
		{
			System.Web.HttpContext.Current.Response.Write( Sript );
		}
		public static void ToSignInPage()
		{		 
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/SignIn.aspx?url=/Wel.aspx");
		}
		public static void ToWelPage()
		{
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Wel.aspx");
		}
		/// <summary>
		/// 切换到信息也面。
		/// </summary>
		/// <param name="mess"></param>
        public static void ToErrorPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (path == "/" || path == "")
                path = "";

            System.Web.HttpContext.Current.Response.Redirect(path + "/Comm/Port/InfoPage.aspx");
        }
		/// <summary>
		/// 切换到信息也面。
		/// </summary>
		/// <param name="mess"></param>
		public static void ToMsgPage(string mess)
		{		
			mess=mess.Replace("@","<BR>@");
			System.Web.HttpContext.Current.Session["info"]=mess;
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Port/InfoPage.aspx?d="+DateTime.Now.ToString(),false);

			//System.Web.HttpContext.Current.Session["info"]=mess;
			//System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Port/InfoPage.aspx",true);
		}
		#endregion 
 
		/// <summary>
		///转到一个页面上。 '_top'
		/// </summary>
		/// <param name="mess"></param>
		/// <param name="target">'_top'</param>
		public static void ToErrorPage(string mess, string target)
		{
			System.Web.HttpContext.Current.Session["info"]=mess;

            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (path == "/" || path == "")
                path = "";

			System.Web.HttpContext.Current.Response.Redirect(path+"/Comm/Port/InfoPage.aspx target='_top'");
		}
        //public static void AlertSaveOK()
        //{
        //    this.ToE("SaveOK", "保存成功");
        //}

        
		/// <summary>
		/// 不用page 参数，show message
		/// </summary>
		/// <param name="mess"></param>
        public static void Alert(string mess)
        {
            //string msg1 = "<script language=javascript>alert('" + msg + "');</script>";
            //if (! System.Web.HttpContext.Current.ClientScript.IsClientScriptBlockRegistered("a "))
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "a ", msg1);


            string script = "<script language=JavaScript>alert('" + mess + "');</script>";
            System.Web.HttpContext.Current.Response.Write(script);

            //	System.Web.HttpContext.Current.Response.aps ( script );
            //  System.Web.HttpContext.Current.Response.Write(script);
        }
        
		public static void ResponseWriteScript(string script )
		{
			script= "<script language=JavaScript> "+script+"</script>";
			System.Web.HttpContext.Current.Response.Write( script );
		}
		#endregion

	}
}
