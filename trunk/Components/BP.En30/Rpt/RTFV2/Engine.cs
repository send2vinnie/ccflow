using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;
using BP.En;
using BP.DA;
using BP.Port;

namespace BP.Rpt.RTF
{
    public class RepBook : BP.DTS.DataIOEn
    {
        public RepBook()
        {
            this.Title = "WFV3.0文书自动修复线。";
        }
        public override void Do()
        {
            string msg = "";
            string sql = "  SELECT * FROM WF_BOOKTEMPLATE";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string file = SystemConfig.PathOfCyclostyleFile + dr["URL"].ToString() + ".rtf";
                msg += RepBook.RepairBook(file);
            }
            PubClass.ResponseWriteBlueMsg(msg);
        }

        public static string RepairBook(string file)
        {
            string msg = "";
            string docs;

            // 读取文件。
            try
            {
                StreamReader read = new StreamReader(file, System.Text.Encoding.ASCII); // 文件流.
                docs = read.ReadToEnd();  //读取完毕。
                read.Close(); // 关闭。
            }
            catch (Exception ex)
            {
                return "@读取文书模板时出现错误。cfile=" + file + " @Ex=" + ex.Message;
            }

            // 修复。
            docs = RepairLineV2(docs);

            // 写入。
            try
            {
                StreamWriter mywr = new StreamWriter(file, false);
                mywr.Write(docs);
                mywr.Close();
            }
            catch (Exception ex)
            {
                return "@写入文书模板时出现错误。cfile=" + file + " @Ex=" + ex.Message;
            }
            msg += "@文书:[" + file + "]成功修复。";

            return msg;


        }

        public static string RepairLine(string line)//str
        {
            char[] chs = line.ToCharArray();
            string str = "";
            foreach (char ch in chs)
            {
                if (ch == '\\')
                {
                    line = line.Replace("\\" + str, "");
                    str = "";
                }
                else if (ch == ' ')
                {
                    /* 如果等于空格， 直接替换原来的 str */
                    line = line.Replace("\\" + str + ch, "");
                    str = "sssssssssssssssssss";
                }
                else
                    str += ch.ToString();
            }

            line = line.Replace("{", "");
            line = line.Replace("}", "");
            line = line.Replace("\r", "");
            line = line.Replace("\n", "");
            line = line.Replace(" ", "");
            line = line.Replace("..", ".");
            return line;
        }
        /// <summary>
        /// RepairLineV2
        /// </summary>
        /// <param name="docs"></param>
        /// <returns></returns>
        public static string RepairLineV2(string docs)//str
        {
            char[] chars = docs.ToCharArray();
            string strs = "";
            foreach (char c in chars)
            {
                if (c == '<')
                {
                    strs = "<";
                    continue;
                }
                if (c == '>')
                {
                    strs += c.ToString();
                    string line = strs.Clone().ToString();
                    line = RepairLine(line);
                    docs = docs.Replace(strs, line);
                    strs = "";
                    continue;
                }
                strs += c.ToString();
            }
            return docs;
        }
    }
	/// <summary>
	/// WebRtfReport 的摘要说明。
	/// </summary>
	public class RTFEngine 
	{
		#region 数据实体
		private Entities _HisEns=null;
		public Entities HisEns
		{
			get
			{
				if (_HisEns==null)
					_HisEns = new Emps();

				return _HisEns;
			}
		}
		#endregion 数据实体

		#region 数据明细实体
		private System.Text.Encoding _encoder= System.Text.Encoding.GetEncoding("GB2312") ;

        public string GetCode(string str)
        {
            if (str == "")
                return str;

            string rtn = "";
            byte[] rr = _encoder.GetBytes(str);
            foreach (byte b in rr)
            {
                if (b > 122)
                    rtn += "\\'" + b.ToString("x");
                else
                    rtn += (char)b;
            }
            return rtn.Replace("\n", " \\par ");
        }
		 
		private ArrayList _EnsDataDtls=null;
		public ArrayList EnsDataDtls
		{
			get
			{
				if (_EnsDataDtls==null)
					_EnsDataDtls =  new ArrayList();  
				return _EnsDataDtls;
			}
		}
	
		#endregion 数据明细实体

		/// <summary>
		/// 增加一个数据实体
		/// </summary>
		/// <param name="en"></param>
		public void AddEn(Entity en)
		{
			this.HisEns.AddEntity(en);
		}
		/// <summary>
		/// 增加一个Ens
		/// </summary>
		/// <param name="ens"></param>
		public void AddEns(Entities DtlEns)
		{
			this.EnsDataDtls.Add(DtlEns);
		}
		public string CyclostyleFilePath="";
		public string TempFilePath="";

		#region 获取特殊要处理的流程节点信息.
		public string GetValueByKeyOfCheckNode(string[] strs )
		{
			foreach(Entity en in this.HisEns)
			{
				if (en.ToString()=="BP.WF.NumCheck" || en.ToString()=="BP.WF.GECheckStand" || en.ToString()=="BP.WF.NoteWork"  )
				{
					if (en.GetValStringByKey("NodeID")!=strs[1])
						continue;
				}
				else
				{
					continue;
				}

				string val=en.GetValStringByKey(strs[2]);
				switch(strs.Length)
				{
					case 1:
					case 2:
						throw new Exception("step1参数设置错误"+strs.ToString());
					case 3: // S.9001002.Rec
						return val;
					case 4: // S.9001002.RDT.Year
					switch(strs[3])
					{
						case "Text":
							if ( val=="0")
								return "否";
							else
								return "是";
						case "Year":
							return val.Substring(0,4);
						case "Month":
							return val.Substring(5,2);
						case "Day":
							return val.Substring(8,2);
						case "NYR":
							return DA.DataType.ParseSysDate2DateTime(val).ToString("yyyy年MM月dd日");
						case "RMB":
							return float.Parse(val).ToString("0.00");
						case "RMBDX":
							return DA.DataType.ParseFloatToCash( float.Parse(val)) ;
						default:
							throw new Exception("step2参数设置错误"+strs );
					}
					default:
						throw new Exception("step3参数设置错误"+strs );
				}
			} 
			throw new Exception("step4参数设置错误"+strs  );
		}
		/// <summary>
		/// 审核节点的表示方法是 节点ID.Attr.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetValueByKey(string key)
		{
			key=key.Replace(" ","");
			key=key.Replace("\r\n","");

			string[] strs=key.Split('.');
            if (strs[0] == "C")
            {
                /* 如果是审核节点 */
                return this.GetValueByKeyOfCheckNode(strs);
            }

			foreach(Entity en in this.HisEns)
			{
                if (key.IndexOf(en.GetType().Name + ".") < 0)
                {
                    string enName = en.GetType().Name;
                    switch (enName)
                    {
                        case "Work":
                        case "GEWork":
                        case "GEStartWork":
                            if (key.IndexOf(en.EnMap.PhysicsTable + ".") < 0)
                                continue;
                            break;
                        case "GECheckStand":
                            if (key.IndexOf(en.GetValStrByKey("NodeID") + ".") < 0)
                                continue;
                            break;
                        default:
                            continue;
                    }
                }
				 
				/*说明就在这个字段内*/
				if (strs.Length==1)
					throw new Exception("参数设置错误，strs.length=1 。"+key);
				if (strs.Length==2)
					return en.GetValStringByKey(strs[1].Trim() );

				if (strs.Length==3)
				{
					string val=en.GetValStringByKey(strs[1].Trim() );
					switch( strs[2].Trim() )
					{
						case "Text":
							if ( val=="0")
								return "否";
							else
								return "是";
						case "Year":
							return val.Substring(0,4);
						case "Month":
							return val.Substring(5,2);
						case "Day":
							return val.Substring(8,2);
						case "NYR":
							return DA.DataType.ParseSysDate2DateTime(val).ToString("yyyy年MM月dd日");
						case "RMB":
							return float.Parse(val).ToString("0.00");
						case "RMBDX":
							return DA.DataType.ParseFloatToCash( float.Parse(val)) ;
						default:
							throw new Exception("参数设置错误，特殊方式取值错误："+key);
					}
				}
			}
			throw new Exception("参数设置错误 GetValueByKey ："+key);
		}
		#endregion

		#region 生成文书
        /// <summary>
        /// 生成文书
        /// </summary>
        /// <param name="cfile">模板文件</param>
        public void MakeDoc(string cfile, string replaceVal)
        {
            string file = PubClass.GenerTempFileName("doc");
            this.MakeDoc(cfile, SystemConfig.PathOfTemp, file,replaceVal, true);
        }
        public string ensStrs = "";
        /// <summary>
        /// 文书生成 
        /// </summary>
        /// <param name="cfile">模板文件</param>
        /// <param name="path">生成路径</param>
        /// <param name="file">生成文件</param>
        /// <param name="isOpen">是否用IE打开？</param>
        public void MakeDoc(string cfile, string path, string file, string replaceVals, bool isOpen)
        {
            string str = Cash.GetBookStr(cfile).Substring(0);

            //string ensStrs = "";
            //foreach (Entity en in this.HisEns)
            //    ensStrs += en.ToString.ToString();

            string error = "";
            string[] paras = Cash.GetBookParas(cfile, ensStrs);

            this.TempFilePath = path + file;
            try
            {
                string key = "";

                #region 主表
                foreach (string para in paras)
                {
                    if (para == null || para == "")
                        continue;

                    try
                    {
                        str = str.Replace("<" + para + ">", this.GetCode(this.GetValueByKey(para)) );
                    }
                    catch (Exception ex)
                    {
                        error += "取参数[" + para + "]出现错误：有以下情况导致此错误;1你用Text取值时间，此属性不是外键。2,类无此属性。<br>更详细的信息：<br>" + ex.Message;
                        if (SystemConfig.IsDebug)
                            throw new Exception(error);

                        Log.DebugWriteError(error);
                    }
                }
                #endregion

                #region 明细表
                string shortName = "";
                ArrayList al = this.EnsDataDtls;
                foreach (Entities dtls in al)
                {
                    shortName = dtls.GetNewEntity.ToString().Substring(dtls.GetNewEntity.ToString().LastIndexOf(".") + 1);
                    if (str.IndexOf(shortName) == -1)
                        continue;

                    int pos_rowKey = str.IndexOf(shortName);
                    int row_start = -1, row_end = -1;
                    if (pos_rowKey != -1)
                    {
                        row_start = str.Substring(0, pos_rowKey).LastIndexOf("\\row");
                        row_end = str.Substring(pos_rowKey).IndexOf("\\row");
                    }

                    if (row_start != -1 && row_end != -1)
                    {
                        string row = str.Substring(row_start, (pos_rowKey - row_start) + row_end);
                        str = str.Replace(row, "");

                        Map map = dtls.GetNewEntity.EnMap;
                        int i = dtls.Count;
                        while (i > 0)
                        {
                            i--;
                            string rowData = row.Clone() as string;
                            Entity dtl = dtls[i];
                            foreach (Attr attr in map.Attrs)
                            {
                                switch (attr.MyDataType)
                                {
                                    case DataType.AppDouble:
                                    case DataType.AppFloat:
                                    case DataType.AppRate:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValStringByKey(attr.Key));
                                        break;
                                    case DataType.AppMoney:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValDecimalByKey(attr.Key).ToString("0.00"));
                                        break;
                                    case DataType.AppInt:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValStringByKey(attr.Key));
                                        break;
                                    default:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", GetCode(dtl.GetValStringByKey(attr.Key)));
                                        break;
                                }
                            }
                            str = str.Insert(row_start, rowData);
                        }
                    }
                }
                #endregion 明细表

                #region 明细 合计信息。
                al = this.EnsDataDtls;
                foreach (Entities dtls in al)
                {
                    shortName = dtls.ToString().Substring(dtls.ToString().LastIndexOf(".") + 1);
                    Map map = dtls.GetNewEntity.EnMap;

                    foreach (Attr attr in map.Attrs)
                    {
                        switch (attr.MyDataType)
                        {
                            case DataType.AppDouble:
                            case DataType.AppFloat:
                            case DataType.AppMoney:
                            case DataType.AppRate:
                                key = "<" + shortName + "." + attr.Key + ".SUM>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumFloatByKey(attr.Key).ToString());

                                key = "<" + shortName + "." + attr.Key + ".SUM.RMB>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumFloatByKey(attr.Key).ToString("0.00"));

                                key = "<" + shortName + "." + attr.Key + ".SUM.RMBDX>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key,
                                        GetCode(DA.DataType.ParseFloatToCash(dtls.GetSumFloatByKey(attr.Key))));
                                break;
                            case DataType.AppInt:
                                key = "<" + shortName + "." + attr.Key + ".SUM>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumIntByKey(attr.Key).ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion 明细表合计



                #region 要替换的字段
                if (replaceVals != null && replaceVals.Contains("@"))
                {
                    string[] vals = replaceVals.Split('@');
                    foreach (string val in vals)
                    {
                        if (val == null || val == "")
                            continue;

                        if (val.Contains("=") == false)
                            continue;

                        string myRep = val.Clone() as string;

                        myRep = myRep.Trim();
                        myRep = myRep.Replace("null", "");
                        string[] myvals = myRep.Split('=');
                        str = str.Replace("<" + myvals[0] + ">", "<" + myvals[1] + ">");
                    }
                }
                #endregion

                StreamWriter wr = new StreamWriter(this.TempFilePath, false, Encoding.ASCII);
                wr.Write(str);
                wr.Close();
            }
            catch (Exception ex)
            {
                string msg = "";
                if (SystemConfig.IsDebug)
                {  // 异常可能与文书的配置有关系。
                    try
                    {
                        this.CyclostyleFilePath = SystemConfig.PathOfData + "\\CyclostyleFile\\" + cfile;
                        str = Cash.GetBookStr(cfile);
                        string s = RepBook.RepairBook(this.CyclostyleFilePath);
                        msg = "@已经成功的执行修复线  RepairLineV2，您重新发送一次或者，退后重新在发送一次，是否可以解决此问题。@" + s;
                    }
                    catch (Exception ex1)
                    {
                        msg = "执行修复线失败.  RepairLineV2 " + ex1.Message;
                    }
                }
                throw new Exception("生成文档失败：文书名称[" + this.CyclostyleFilePath + "] 异常信息：" + ex.Message + " @自动修复文书信息：" + msg);
            }
            if (isOpen)
                PubClass.Print(System.Web.HttpContext.Current.Request.ApplicationPath + "/Temp/" + file);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfile"></param>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="isOpen"></param>
        public void MakeDoc2(string cfile, string path, string file, bool isOpen)
        {
            string str = Cash.GetBookStr(cfile);
            this.TempFilePath = path + file;
            try
            {
                string key = "";

                #region 如果是debug状态就修复它。
              
                #endregion


                #region 主表
                char[] chars = str.ToCharArray();
                string para = "";
                string strs = "";
                foreach (Entity en in this.HisEns)
                {
                    strs += en.ToString();
                }
                //string mainEnName=this.hisen
                foreach (char c in chars)
                {
                    if (c == '>')
                    {
                        if (strs.IndexOf("." + para.Substring(0, para.IndexOf('.'))) == -1)
                            if (para.IndexOf("C.") == -1)
                                continue;
                        try
                        {
                            /* 读到了最后，就开始执行替换 */
                            str = str.Replace("<" + para + ">", this.GetCode(this.GetValueByKey(para)));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("取参数[" + para + "]出现错误：有以下情况导致此错误;1你用Text取值时间，此属性不是外键。2,类无此属性。<br>更详细的信息：<br>" + ex.Message);
                        }
                    }

                    if (c == '<')
                        para = ""; // 如果遇到了 '<' 开始记录
                    else
                    {
                        if (c.ToString() == "")
                            continue;
                        para += c.ToString();
                    }
                }
                #endregion

                #region 明细表
                string shortName = "";
                ArrayList al = this.EnsDataDtls;
                foreach (Entities dtls in al)
                {
                    shortName = dtls.GetNewEntity.ToString().Substring(dtls.GetNewEntity.ToString().LastIndexOf(".") + 1);
                    if (str.IndexOf(shortName) == -1)
                        continue;

                    int pos_rowKey = str.IndexOf(shortName);
                    int row_start = -1, row_end = -1;
                    if (pos_rowKey != -1)
                    {
                        row_start = str.Substring(0, pos_rowKey).LastIndexOf("\\row");
                        row_end = str.Substring(pos_rowKey).IndexOf("\\row");
                    }

                    if (row_start != -1 && row_end != -1)
                    {
                        string row = str.Substring(row_start, (pos_rowKey - row_start) + row_end);
                        str = str.Replace(row, "");

                        Map map = dtls.GetNewEntity.EnMap;
                        int i = dtls.Count;
                        while (i > 0)
                        {
                            i--;
                            string rowData = row.Clone() as string;
                            Entity dtl = dtls[i];
                            foreach (Attr attr in map.Attrs)
                            {
                                switch (attr.MyDataType)
                                {
                                    case DataType.AppDouble:
                                    case DataType.AppFloat:
                                    case DataType.AppRate:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValStringByKey(attr.Key));
                                        break;
                                    case DataType.AppMoney:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValDecimalByKey(attr.Key).ToString("0.00"));
                                        break;
                                    case DataType.AppInt:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValStringByKey(attr.Key));
                                        break;
                                    default:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", GetCode(dtl.GetValStringByKey(attr.Key)));
                                        break;
                                }
                            }
                            str = str.Insert(row_start, rowData);
                        }
                    }
                }
                #endregion 明细表

                #region 明细 合计信息。
                al = this.EnsDataDtls;
                foreach (Entities dtls in al)
                {
                    shortName = dtls.ToString().Substring(dtls.ToString().LastIndexOf(".") + 1);
                    Map map = dtls.GetNewEntity.EnMap;

                    foreach (Attr attr in map.Attrs)
                    {
                        switch (attr.MyDataType)
                        {
                            case DataType.AppDouble:
                            case DataType.AppFloat:
                            case DataType.AppMoney:
                            case DataType.AppRate:
                                key = "<" + shortName + "." + attr.Key + ".SUM>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumFloatByKey(attr.Key).ToString());

                                key = "<" + shortName + "." + attr.Key + ".SUM.RMB>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumFloatByKey(attr.Key).ToString("0.00"));

                                key = "<" + shortName + "." + attr.Key + ".SUM.RMBDX>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key,
                                        GetCode(DA.DataType.ParseFloatToCash(dtls.GetSumFloatByKey(attr.Key))));
                                break;
                            case DataType.AppInt:
                                key = "<" + shortName + "." + attr.Key + ".SUM>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumIntByKey(attr.Key).ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion 明细表合计

                StreamWriter wr = new StreamWriter(this.TempFilePath, false, Encoding.ASCII);
                wr.Write(str);
                wr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("生成文档失败：文书名称[" + this.CyclostyleFilePath + "] 异常信息：" + ex.Message);
            }

            if (isOpen)
                PubClass.Print(System.Web.HttpContext.Current.Request.ApplicationPath + "/Temp/" + file);
        }
        public void MakeDoc_bak(string cfile, string path, string file, bool isOpen)
        {
            this.CyclostyleFilePath = SystemConfig.PathOfData + "\\CyclostyleFile\\" + cfile;
            this.TempFilePath = path + file;

            try
            {
                System.IO.File.Copy(this.CyclostyleFilePath, this.TempFilePath, true); //复制一个样本到临时目录里面。
            }
            catch (Exception ex)
            {
                throw new Exception("文书生成期间复制文件错误: from=" + this.CyclostyleFilePath + " To=" + this.TempFilePath + " more:" + ex.Message);
            }

            StreamReader read = null;
            StreamWriter wr = null;
            try
            {
                #region 前期设置.
                read = new StreamReader(this.TempFilePath, Encoding.ASCII); // 文件流.
               
                string str = read.ReadToEnd();  //读取完毕。

                read.Close(); // 关闭。
                wr = new StreamWriter(this.TempFilePath, false, Encoding.ASCII);
                string key = "";
                #endregion


                #region 如果是debug状态就修复它。
                if (SystemConfig.IsDebug)
                {
                    try
                    {
                      //  str = this.RepairLineV2(str); // 修复线。
                        /* update template .*/
                        StreamWriter mywr = new StreamWriter(this.CyclostyleFilePath, false);
                        
                      // #warning  删除了它。
                        // mywr.Write(str); 
                        mywr.Close();
                    }
                    catch
                    {
                    }
                }
                #endregion


                #region 主表
                char[] chars = str.ToCharArray();
                string para = "";
                string strs = "";
                foreach (Entity en in this.HisEns)
                {
                    strs += en.ToString();
                }
                //string mainEnName=this.hisen
                foreach (char c in chars)
                {
                    if (c == '>')
                    {
                        if (strs.IndexOf("." + para.Substring(0, para.IndexOf('.'))) == -1)
                            if (para.IndexOf("C.") == -1)
                                continue;
                        try
                        {
                            /* 读到了最后，就开始执行替换 */
                            str = str.Replace("<" + para + ">", this.GetCode(this.GetValueByKey(para)));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("取参数[" + para + "]出现错误：有以下情况导致此错误;1你用Text取值时间，此属性不是外键。2,类无此属性。<br>更详细的信息：<br>" + ex.Message);
                        }
                    }

                    if (c == '<')
                        para = ""; // 如果遇到了 '<' 开始记录
                    else
                    {
                        if (c.ToString() == "")
                            continue;
                        para += c.ToString();
                    }
                }
                #endregion

                #region 明细表
                string shortName = "";
                ArrayList al = this.EnsDataDtls;
                foreach (Entities dtls in al)
                {
                    shortName = dtls.GetNewEntity.ToString().Substring(dtls.GetNewEntity.ToString().LastIndexOf(".") + 1);
                    if (str.IndexOf(shortName) == -1)
                        continue;

                    int pos_rowKey = str.IndexOf(shortName);
                    int row_start = -1, row_end = -1;
                    if (pos_rowKey != -1)
                    {
                        row_start = str.Substring(0, pos_rowKey).LastIndexOf("\\row");
                        row_end = str.Substring(pos_rowKey).IndexOf("\\row");
                    }

                    if (row_start != -1 && row_end != -1)
                    {
                        string row = str.Substring(row_start, (pos_rowKey - row_start) + row_end);
                        str = str.Replace(row, "");

                        Map map = dtls.GetNewEntity.EnMap;
                        int i = dtls.Count;
                        while (i > 0)
                        {
                            i--;
                            string rowData = row.Clone() as string;
                            Entity dtl = dtls[i];
                            foreach (Attr attr in map.Attrs)
                            {
                                switch (attr.MyDataType)
                                {
                                    case DataType.AppDouble:
                                    case DataType.AppFloat:
                                    case DataType.AppRate:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValStringByKey(attr.Key));
                                        break;
                                    case DataType.AppMoney:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValDecimalByKey(attr.Key).ToString("0.00"));
                                        break;
                                    case DataType.AppInt:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", dtl.GetValStringByKey(attr.Key));
                                        break;
                                    default:
                                        rowData = rowData.Replace("<" + shortName + "." + attr.Key + ">", GetCode(dtl.GetValStringByKey(attr.Key)));
                                        break;
                                }
                            }
                            str = str.Insert(row_start, rowData);
                        }
                    }
                }
                #endregion 明细表

                #region 明细 合计信息。
                al = this.EnsDataDtls;
                foreach (Entities dtls in al)
                {
                    shortName = dtls.ToString().Substring(dtls.ToString().LastIndexOf(".") + 1);
                    Map map = dtls.GetNewEntity.EnMap;

                    foreach (Attr attr in map.Attrs)
                    {
                        switch (attr.MyDataType)
                        {
                            case DataType.AppDouble:
                            case DataType.AppFloat:
                            case DataType.AppMoney:
                            case DataType.AppRate:
                                key = "<" + shortName + "." + attr.Key + ".SUM>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumFloatByKey(attr.Key).ToString());

                                key = "<" + shortName + "." + attr.Key + ".SUM.RMB>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumFloatByKey(attr.Key).ToString("0.00"));

                                key = "<" + shortName + "." + attr.Key + ".SUM.RMBDX>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key,
                                        GetCode(DA.DataType.ParseFloatToCash(dtls.GetSumFloatByKey(attr.Key))));
                                break;
                            case DataType.AppInt:
                                key = "<" + shortName + "." + attr.Key + ".SUM>";
                                if (str.IndexOf(key) != -1)
                                    str = str.Replace(key, dtls.GetSumIntByKey(attr.Key).ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion 明细表合计



                wr.Write(str);
            }
            catch (Exception ex)
            {
                if (read != null)
                    read.Close();
                if (wr != null)
                    wr.Close();
                throw new Exception("生成文档失败：文书名称[" + this.CyclostyleFilePath + "] 异常信息：" + ex.Message);
            }
            read.Close();
            wr.Close();

            if (isOpen)
                PubClass.Print(System.Web.HttpContext.Current.Request.ApplicationPath + "/Temp/" + file);
        }
		#endregion

		#region 方法
        /// <summary>
        /// RTFEngine
        /// </summary>
        public RTFEngine()
        {
            this._EnsDataDtls = null;
            this._HisEns = null;
        }
        /// <summary>
        /// 修复线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
		
	
		#endregion
	}

     
}
