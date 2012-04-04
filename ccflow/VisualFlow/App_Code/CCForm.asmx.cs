﻿using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using BP.DA;
using BP.Sys;
using BP.Web;
using BP.En;
using BP.WF;
using Silverlight.DataSetConnector;
using System.Drawing.Imaging;
using System.Drawing;
using System.Configuration;

namespace BP.Web
{
    /// <summary>
    /// DA 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class CCForm : WSBase
    {
        [WebMethod]
        public string SaveImageAsFile(byte[] img, string pkval, string fk_Frm_Ele)
        {
            FrmEle fe = new FrmEle(fk_Frm_Ele);
            System.Drawing.Image newImage;
            using (MemoryStream ms = new MemoryStream(img, 0, img.Length))
            {
                ms.Write(img, 0, img.Length);
                newImage = Image.FromStream(ms, true);
                Bitmap bitmap = new Bitmap(newImage, new Size(fe.WOfInt, fe.HOfInt));

                if (System.IO.Directory.Exists(fe.HandSigantureSavePath + "\\" + fe.FK_MapData + "\\") == false)
                    System.IO.Directory.CreateDirectory(fe.HandSigantureSavePath + "\\" + fe.FK_MapData + "\\");

                string saveTo = fe.HandSigantureSavePath + "\\" + fe.FK_MapData + "\\" + pkval + ".jpg";
                bitmap.Save(saveTo, ImageFormat.Jpeg);

                string pathFile = System.Web.HttpContext.Current.Request.ApplicationPath + fe.HandSiganture_UrlPath + fe.FK_MapData + "/" + pkval + ".jpg";
                FrmEleDB ele = new FrmEleDB();
                ele.FK_MapData = fe.FK_MapData;
                ele.EleID = fe.EleID;
                ele.RefPKVal = pkval;
                ele.Tag1 = pathFile.Replace("\\\\","\\");
                ele.Tag1 = pathFile.Replace("////", "//");

                ele.Tag2 = saveTo.Replace("\\\\", "\\");
                ele.Tag2 = saveTo.Replace("////", "//");

                ele.GenerPKVal();
                ele.Save();
                
                return pathFile;
                // return "../DataUser/" + realpath + strFileName + ".png";
            }
            //FrmEleDB db = new FrmEleDB();
            //db.MyPK= 
            //return "error";
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="kev"></param>
        /// <returns></returns>
        [WebMethod]
        public string CfgKey(string kev)
        {
            return BP.SystemConfig.AppSettings[kev];
        }
        /// <summary>
        /// 装载表单模板
        /// </summary>
        /// <param name="fileByte">字节数</param>
        /// <param name="fk_mapData"></param>
        /// <param name="isClear"></param>
        /// <returns></returns>
        [WebMethod]
        public string LoadFrmTemplete(byte[] fileByte, string fk_mapData, bool isClear)
        {
            try
            {
                string file = "\\Temp\\" + fk_mapData + ".xml";
                UploadFile(fileByte, file);
                string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + file;
                this.LoadFrmTempleteFile(path, fk_mapData, isClear);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileByte"></param>
        /// <param name="fk_mapData"></param>
        /// <param name="isClear"></param>
        /// <returns></returns>
        [WebMethod]
        public string LoadFrmTempleteFile(string file, string fk_mapData, bool isClear)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(file);
                MapData.ImpMapData(fk_mapData, ds);
                if (fk_mapData.Contains("ND"))
                {
                    /* 判断是否是节点表单 */
                    int nodeID = 0;
                    try
                    {
                        nodeID = int.Parse(fk_mapData.Replace("ND", ""));
                    }
                    catch
                    {
                        return null;
                    }
                    Node nd = new Node(nodeID);
                    nd.RepareMap();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="FileByte">文件字节</param>
        /// <param name="fileName">存储的文件名</param>
        /// <returns>null or exception</returns>
        [WebMethod]
        public string UploadFile(byte[] FileByte, String fileName)
        {
            try
            {
                string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
                string filePath = path + "\\" + fileName;
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                //这里使用绝对路径来索引
                FileStream stream = new FileStream(filePath, FileMode.CreateNew);
                stream.Write(FileByte, 0, FileByte.Length);
                stream.Close();
                DataSet ds = new DataSet();
                ds.ReadXml(filePath);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 获取xml数据
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetXmlData(string xmlFileName)
        {
            string sql = "";
            sql = "SELECT 'HandSiganture' AS DFor, '@Label=存储路径@FType=String@DefVal=D:\\ccflow\\VisualFlow\\DataUser\\BPPaint\\' as Tag1, '@Label=窗口打开高度@FType=Int@DefVal=300' as Tag2, '@Label=窗口打开宽度@FType=Int@DefVal=450' as Tag3, '@Label=UrlPath@FType=String@DefVal=/DataUser/BPPaint/' as Tag4 @Form";
            sql += " UNION ";
            sql += "SELECT 'EleSiganture' AS DFor, '@Label=位置@FType=String' as Tag1, '@Label=高度@FType=Int' as Tag2, '@Label=宽度@FType=Int' as Tag3, '' as  Tag4 @Form";

            if (BP.SystemConfig.AppCenterDBType == DBType.Oracle9i)
                sql = sql.Replace("@Form", " FROM DUAL");
            else
                sql = sql.Replace("@Form", "");

            return this.RunSQLReturnTable(sql);

            //string path = SystemConfig.PathOfXML + xmlFileName;
            //DataSet ds = new DataSet();
            //ds.ReadXml(path);

            //DataSet myds = new DataSet();
            //foreach (DataTable dt in myds.Tables)
            //{
            //    DataTable dtNew = new DataTable();
            //    dtNew.TableName = dt.TableName;
            //    foreach (DataColumn dc in dt.Columns)
            //    {
            //        dtNew.Columns.Add(dc.ColumnName);
            //    }
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow myDr = dtNew.NewRow();
            //        foreach (DataColumn dc in dt.Columns)
            //        {
            //            myDr[dc.ColumnName] = dr[dc.ColumnName];
            //        }
            //        dtNew.Rows.Add(myDr);
            //    }
            //    myds.Tables.Add(dtNew);
            //}

            //return Connector.ToXml(myds);
        }
        [WebMethod]
        public string DoType(string dotype, string v1, string v2, string v3, string v4, string v5)
        {
            string sql = "";
            try
            {
                switch (dotype)
                {
                    //case "NewM2Mss":
                    //MapM2M m2m = new MapM2M();
                    //m2m.FK_MapData = v1;
                    //m2m.NoOfObj = v2;
                    //m2m.MyPK=m2m.FK_MapData+"_"+m2m.NoOfObj;
                    //if (m2m.IsExits == false)
                    //    return null;
                    //return "名称("+v2+")已经存在.";
                    case "NewDtl":
                        MapDtl dtlN = new MapDtl();
                        dtlN.No = v1;
                        if (dtlN.RetrieveFromDBSources() != 0)
                            return "明细表已存在";
                        dtlN.Name = v1;
                        dtlN.FK_MapData = v2;
                        dtlN.PTable = v1;
                        dtlN.Insert();
                        dtlN.IntMapAttrs();
                        return null;
                    case "DelM2M":
                        MapM2M m2mDel = new MapM2M();
                        m2mDel.MyPK = v1;
                        m2mDel.Delete();
                        //M2M m2mData = new M2M();
                        //m2mData.Delete(M2MAttr.FK_MapData, v1);
                        return null;
                    case "NewAthM": // 新建 NewAthM. 
                        string fk_mapdataAth = v1;
                        string athName = v2;

                        BP.Sys.FrmAttachment athM = new FrmAttachment();
                        athM.MyPK = athName;
                        if (athM.IsExits)
                            return "多选名称:" + athName + "，已经存在。";

                        athM.X = float.Parse(v3);
                        athM.Y = float.Parse(v4);
                        athM.Name = "多文件上传";
                        athM.FK_MapData = fk_mapdataAth;
                        athM.Insert();
                        return null;
                    case "NewM2M":
                        string fk_mapdataM2M = v1;
                        string m2mName = v2;
                        MapM2M m2m = new MapM2M();
                        m2m.FK_MapData = v1;
                        m2m.NoOfObj = v2;
                        if (v3 == "0")
                        {
                            m2m.HisM2MType = M2MType.M2M;
                            m2m.Name = "新建一对多";
                        }
                        else
                        {
                            m2m.HisM2MType = M2MType.M2MM;
                            m2m.Name = "新建一对多多";
                        }

                        m2m.X = float.Parse(v4);
                        m2m.Y = float.Parse(v5);
                        m2m.MyPK = m2m.FK_MapData + "_" + m2m.NoOfObj;
                        if (m2m.IsExits)
                            return "多选名称:" + m2mName + "，已经存在。";
                        m2m.Insert();
                        return null;
                    case "DelEnum":
                        // 检查这个物理表是否被使用。
                        sql = "SELECT  * FROM Sys_MapAttr WHERE UIBindKey='" + v1 + "'";
                        DataTable dtEnum = DBAccess.RunSQLReturnTable(sql);
                        string msgDelEnum = "";
                        foreach (DataRow dr in dtEnum.Rows)
                        {
                            msgDelEnum += "\n 表单编号:" + dr["FK_MapData"] + " , 字段:" + dr["KeyOfEn"] + ", 名称:" + dr["Name"];
                        }

                        if (msgDelEnum != "")
                            return "该枚举已经被如下字段所引用，您不能删除它。" + msgDelEnum;

                        sql = "DELETE Sys_EnumMain WHERE No='" + v1 + "'";
                        sql += "@DELETE Sys_Enum WHERE EnumKey='" + v1 + "' ";
                        DBAccess.RunSQLs(sql);
                        return null;
                    case "DelSFTable": /* 删除自定义的物理表. */
                        // 检查这个物理表是否被使用。
                        sql = "SELECT  * FROM Sys_MapAttr WHERE UIBindKey='" + v1 + "'";
                        DataTable dt = DBAccess.RunSQLReturnTable(sql);
                        string msgDel = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            msgDel += "\n 表单编号:" + dr["FK_MapData"] + " , 字段:" + dr["KeyOfEn"] + ", 名称:" + dr["Name"];
                        }

                        if (msgDel != "")
                            return "该数据表已经被如下字段所引用，您不能删除它。" + msgDel;

                        SFTable sfDel = new SFTable();
                        sfDel.No = v1;
                        sfDel.DirectDelete();
                        return null;
                    case "CreateTable":
                        string enName = v1;
                        string chName = v2;
                        if (string.IsNullOrEmpty(v1) || string.IsNullOrEmpty(v2))
                            return "创建或者视图中的中英文名称不能为空。";

                        SFTable sf = new SFTable();
                        sf.No = enName;
                        if (sf.IsExits == true)
                            return "表名:" + sf.No + "已经存在.";

                        sf.No = enName;
                        sf.Name = chName;
                        sf.FK_Val = enName;
                        sf.Insert();
                        if (DBAccess.IsExitsObject(enName))
                        {
                            /*已经存在此对象，检查一下是否有No,Name列。*/
                            sql = "SELECT No,Name FROM " + enName;
                            try
                            {
                                DBAccess.RunSQLReturnTable(sql);
                            }
                            catch (Exception ex)
                            {
                                return "您指定的表或视图(" + enName + ")，不包含No,Name两列，不符合ccflow约定的规则。技术信息:" + ex.Message;
                            }
                            return null;
                        }

                        try
                        {
                            // 如果没有该表或者视图，就要创建它。
                            sql = "CREATE TABLE " + enName + "(No varchar(30) NOT NULL,Name varchar(50) NULL)";
                            DBAccess.RunSQL(sql);
                            DBAccess.RunSQL("INSERT INTO " + enName + " (No,Name) VALUES('001','Item1')");
                            DBAccess.RunSQL("INSERT INTO " + enName + " (No,Name) VALUES('002','Item2')");
                            DBAccess.RunSQL("INSERT INTO " + enName + " (No,Name) VALUES('003','Item3')");
                        }
                        catch (Exception ex)
                        {
                            sf.DirectDelete();
                            return "创建物理表期间出现错误,可能是非法的物理表名.技术信息:" + ex.Message;
                        }
                        return null; /*创建成功后返回空值*/
                    case "FrmTempleteExp":  //导出表单.
                        MapData mdfrmtem = new MapData();
                        mdfrmtem.No = v1;
                        if (mdfrmtem.RetrieveFromDBSources() == 0)
                        {
                            if (v1.Contains("ND"))
                            {
                                int nodeId = int.Parse(v1.Replace("ND", ""));
                                Node nd = new Node(nodeId);
                                mdfrmtem.Name = nd.Name;
                                mdfrmtem.PTable = v1;
                                mdfrmtem.EnPK = "OID";
                                mdfrmtem.Insert();
                            }
                        }

                        DataSet ds = mdfrmtem.GenerHisDataSet();
                        string file = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Temp\\" + v1 + ".xml";
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                        ds.WriteXml(file);

                        // BP.PubClass.DownloadFile(file, mdfrmtem.Name + ".xml");
                        //this.DownLoadFile(System.Web.HttpContext.Current, file, mdfrmtem.Name);
                        return null;
                    case "FrmTempleteImp": //导入表单.
                        DataSet dsImp = new DataSet();
                        string fileImp = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Temp\\" + v1 + ".xml";
                        dsImp.ReadXml(fileImp); //读取文件.
                        MapData.ImpMapData(v1, dsImp);
                        return null;
                    case "NewHidF":
                        string fk_mapdataHid = v1;
                        string key = v2;
                        string name = v3;
                        int dataType = int.Parse(v4);
                        MapAttr mdHid = new MapAttr();
                        mdHid.MyPK = fk_mapdataHid + "_" + key;
                        mdHid.FK_MapData = fk_mapdataHid;
                        mdHid.KeyOfEn = key;
                        mdHid.Name = name;
                        mdHid.MyDataType = dataType;
                        mdHid.HisEditType = EditType.Edit;
                        mdHid.MaxLen = 100;
                        mdHid.MinLen = 0;
                        mdHid.LGType = FieldTypeS.Normal;
                        mdHid.UIVisible = false;
                        mdHid.UIIsEnable = false;
                        mdHid.Insert();
                        return null;
                    case "DelDtl":
                        MapDtl dtl = new MapDtl(v1);
                        dtl.Delete();
                        return null;
                    case "DeleteFrm":
                        string delFK_Frm = v1;
                        MapData mdDel = new MapData(delFK_Frm);
                        mdDel.Delete();
                        sql = "@DELETE Sys_MapData WHERE No='" + delFK_Frm + "'";
                        sql = "@DELETE WF_FrmNode WHERE FK_Frm='" + delFK_Frm + "'";
                        DBAccess.RunSQLs(sql);
                        return null;
                    case "FrmUp":
                    case "FrmDown":
                        FrmNode myfn = new FrmNode();
                        myfn.Retrieve(FrmNodeAttr.FK_Node, v1, FrmNodeAttr.FK_Frm, v2);
                        if (dotype == "FrmUp")
                            myfn.DoUp();
                        else
                            myfn.DoDown();
                        return null;
                    case "SaveFlowFrm":
                        // 转化参数意义.
                        string vals = v1;
                        string fk_Node = v2;
                        string fk_flow = v3;
                        bool isPrint = false;
                        if (v5 == "1")
                            isPrint = true;

                        bool isReadonly = false;
                        if (v4 == "1")
                            isReadonly = true;

                        string msg = this.SaveEn(vals);
                        if (msg.Contains("Error"))
                            return msg;

                        string fk_frm = msg;
                        Frm fm = new Frm();
                        fm.No = fk_frm;
                        fm.Retrieve();

                        FrmNode fn = new FrmNode();
                        if (fn.Retrieve(FrmNodeAttr.FK_Frm, fk_frm,
                            FrmNodeAttr.FK_Node, fk_Node) == 1)
                        {
                            fn.IsEdit = !isReadonly;
                            fn.IsPrint = isPrint;
                            fn.FK_Flow = fk_flow;
                            fn.Update();
                            BP.DA.DBAccess.RunSQL("UPDATE Sys_MapData SET FK_FrmSort='01',AppType=1  WHERE No='" + fk_frm + "'");
                            return fk_frm;
                        }

                        fn.FK_Frm = fk_frm;
                        fn.FK_Flow = fk_flow;
                        fn.FK_Node = int.Parse(fk_Node);
                        fn.IsEdit = !isReadonly;
                        fn.IsPrint = isPrint;
                        fn.Idx = 100;
                        fn.FK_Flow = fk_flow;
                        fn.Insert();

                        MapData md = new MapData();
                        md.No = fm.No;
                        if (md.RetrieveFromDBSources() == 0)
                        {
                            md.Name = fm.Name;
                            md.EnPK = "OID";
                            md.Insert();

                        }

                        MapAttr attr = new MapAttr();
                        attr.FK_MapData = md.No;
                        attr.KeyOfEn = "OID";
                        attr.Name = "WorkID";
                        attr.MyDataType = BP.DA.DataType.AppInt;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = false;
                        attr.UIIsEnable = false;
                        attr.DefVal = "0";
                        attr.HisEditType = BP.En.EditType.Readonly;
                        attr.Insert();

                        attr = new MapAttr();
                        attr.FK_MapData = md.No;
                        attr.KeyOfEn = "FID";
                        attr.Name = "FID";
                        attr.MyDataType = BP.DA.DataType.AppInt;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = false;
                        attr.UIIsEnable = false;
                        attr.DefVal = "0";
                        attr.HisEditType = BP.En.EditType.Readonly;
                        attr.Insert();

                        attr = new MapAttr();
                        attr.FK_MapData = md.No;
                        attr.KeyOfEn = "RDT";
                        attr.Name = "记录日期";
                        attr.MyDataType = BP.DA.DataType.AppDateTime;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = false;
                        attr.UIIsEnable = false;
                        attr.DefVal = "@RDT";
                        attr.HisEditType = BP.En.EditType.Readonly;
                        attr.Insert();
                        return fk_frm;
                    default:
                        return "Error:"+ dotype+" , 未设置此标记.";
                }
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }
        /// <summary>
        /// 保存entity.
        /// 事例: @EnName=BP.Sys.FrmLabel@PKVal=Lin13b@X=100@Y=299@Text=我的标签
        /// </summary>
        /// <param name="vals"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveEn(string vals)
        {
            Entity en = null;
            try
            {
                AtPara ap = new AtPara(vals);
                string enName = ap.GetValStrByKey("EnName");
                string pk = ap.GetValStrByKey("PKVal");
                  en = ClassFactory.GetEn(enName);
                en.ResetDefaultVal();

                if (en == null)
                    throw new Exception("无效的类名:" + enName);

                if (string.IsNullOrEmpty(pk) == false)
                {
                    en.PKVal = pk;
                    en.RetrieveFromDBSources();
                }

                foreach (string key in ap.HisHT.Keys)
                {
                    if (key == "PKVal")
                        continue;
                    en.SetValByKey(key, ap.HisHT[key].ToString().Replace('^', '@') );
                }
                en.Save();
                return en.PKVal as string;
            }
            catch (Exception ex)
            {
                if (en != null)
                    en.CheckPhysicsTable();
                return "Error:" + ex.Message;
            }
        }
    
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [WebMethod]
        public string FtpMethod(string doType, string v1, string v2, string v3)
        {
            try
            {
                FtpSupport.FtpConnection conn = new FtpSupport.FtpConnection("192.168.1.138", "administrator", "jiaozi");
                switch (doType)
                {
                    case "ShareFrm": /*共享模板*/
                        MapData md = new MapData();
                        DataSet ds = md.GenerHisDataSet();
                        string file =  BP.SystemConfig.PathOfTemp + v1 + "_" + v2 +"_"+DateTime.Now.ToString("MM-dd hh-mm")+ ".xml";
                        ds.WriteXml(file);
                        conn.SetCurrentDirectory("/");
                        conn.SetCurrentDirectory("/Upload.Form/");
                        conn.SetCurrentDirectory(v3);
                        conn.PutFile(file, md.Name+".xml");
                        conn.Close();
                        return null;
                    case "GetDirs":
                        //   return "@01.日常办公@02.人力资源@03.其它类";
                        conn.SetCurrentDirectory(v1);
                        FtpSupport.Win32FindData[] dirs = conn.FindFiles();
                        conn.Close();
                        string dirsStr = "";
                        foreach (FtpSupport.Win32FindData dir in dirs)
                        {
                            dirsStr += "@" + dir.FileName;
                        }
                        return dirsStr;
                    case "GetFls":
                        conn.SetCurrentDirectory(v1);
                        FtpSupport.Win32FindData[] fls = conn.FindFiles();
                        conn.Close();
                        string myfls = "";
                        foreach (FtpSupport.Win32FindData fl in fls)
                        {
                            myfls += "@" + fl.FileName;
                        }
                        return myfls;
                    case "LoadTempleteFile":
                        string fileFtpPath = v1;
                        conn.SetCurrentDirectory("/Form.表单模版/");
                        conn.SetCurrentDirectory(v3);

                         /*下载文件到指定的目录: */
                        string tempFile = BP.SystemConfig.PathOfTemp+"\\"+v2+".xml";
                        conn.GetFile(v1, tempFile, false, FileAttributes.Archive, FtpSupport.FtpTransferType.Ascii);
                        return this.LoadFrmTempleteFile(tempFile, v2, true);
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                return "Error:"+ex.Message;
            }
        }
      
        [WebMethod]
        public string RequestSFTable(string ensName)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (ensName.Contains("."))
            {
                Entities ens = BP.DA.ClassFactory.GetEns(ensName);
                if (ens==null)
                    ens = BP.DA.ClassFactory.GetEns(ensName);

                if (ens==null)
                    ens = BP.DA.ClassFactory.GetEns(ensName);

                ens.RetrieveAllFromDBSource();
                dt = ens.ToDataTableField();
                ds.Tables.Add(dt);
            }
            else
            {

                string sql = "SELECT No,Name FROM " + ensName;
                ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
            }
            return Connector.ToXml(ds);
        }

        #region 产生 page 菜单
        public void InitFrm(string fk_mapdata)
        {
            try
            {
                BP.PubClass.InitFrm(fk_mapdata);
            }
            catch (Exception ex)
            {
                FrmLines lines = new FrmLines();
                lines.Delete(FrmLabAttr.FK_MapData, fk_mapdata);
                throw ex;
            }
        }
        private DataSet ds = null;
        /// <summary>
        /// 获取一个Frm
        /// </summary>
        /// <param name="fk_mapdata">map</param>
        /// <param name="workID">可以为0</param>
        /// <returns>form描述</returns>
        [WebMethod]
        public string GenerFrm(string fk_mapdata, int workID)
        {
            MapData md = new MapData(fk_mapdata);
            this.ds = md.GenerHisDataSet();
            return Connector.ToXml(ds);
        }
        #endregion 产生 frm

        private string DealPK(string pk, string fromMapdata, string toMapdata)
        {
            if (pk.Contains("*" + fromMapdata))
                return pk.Replace("*" + toMapdata, "*" + toMapdata);
            else
                return pk + "*" + toMapdata;
        }
        public void LetAdminLogin()
        {
            BP.Port.Emp emp = new BP.Port.Emp("admin");
            BP.Web.WebUser.SignInOfGener(emp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromMapData"></param>
        /// <param name="fk_mapdata"></param>
        /// <param name="isClear"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public string CopyFrm(string fromMapData, string fk_mapdata, bool isClear)
        {
            this.LetAdminLogin();

            MapData md = new MapData(fromMapData);
            MapData.ImpMapData(fk_mapdata, md.GenerHisDataSet());

            // 如果是节点表单，就要执行一次修复，以免漏掉应该有的系统字段。
            if (fk_mapdata.Contains("ND") == true )
            {
                try
                {
                    string fk_node = fk_mapdata.Replace("ND", "");
                    Node nd = new Node(int.Parse(fk_node));
                    nd.RepareMap();
                }
                catch
                {
                    // 不处理异常。
                }
            }
            return null;
        }
        [WebMethod]
        public string SaveFrm(string xml, string sqls)
        {
            try
            {
                return SaveFrm_Pri(xml, sqls);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string SaveFrm_Pri(string xml, string sqls)
        {
            StringReader sr = new StringReader(xml);
            DataSet ds = new DataSet();
            ds.ReadXml(sr);
            string str = "";
            foreach (DataTable dt in ds.Tables)
            {
                try
                {
                    str += this.SaveDT(dt);
                }
                catch (Exception ex)
                {
                    str += "@保存"+dt.TableName+"失败:"+ex.Message;
                }
            }

            #region 处理数据库兼容的问题
            if (BP.SystemConfig.AppCenterDBType == DBType.Oracle9i)
            {
                sqls = sqls.Replace("LEN(", "LENGTH(");
            }
                DBAccess.RunSQL("UPDATE Sys_MapAttr SET Name='' WHERE Name IS NULL ");
            #endregion 处理数据库兼容的问题

            this.RunSQLs(sqls);
            if (string.IsNullOrEmpty(str))
                return null;
            return str;
        }
        public string SaveDT(DataTable dt)
        {
            string igF = "@RowIndex@RowState@";
            if (dt.Rows.Count == 0)
            {
                return "";
            }

            string tableName = dt.TableName.Replace("CopyOf", "");

            if (tableName == "Sys_MapData")
            {
                int i = 0;
            }

            #region gener sql.
            //生成updataSQL.
            string updataSQL = "UPDATE " + tableName + " SET ";
            foreach (DataColumn dc in dt.Columns)
            {
                if (igF.Contains("@" + dc.ColumnName + "@"))
                    continue;

                try
                {
                    updataSQL += dc.ColumnName + "=" + BP.SystemConfig.AppCenterDBVarStr + dc.ColumnName + ",";
                }
                catch(Exception ex)
                {
                }
            }
            updataSQL = updataSQL.Substring(0, updataSQL.Length - 1);
            string pk = "";
            if (dt.Columns.Contains("MyPK"))
                pk = "MyPK";
            if (dt.Columns.Contains("OID"))
                pk = "OID";
            if (dt.Columns.Contains("No"))
                pk = "No";
            updataSQL += " WHERE " + pk + "=" + BP.SystemConfig.AppCenterDBVarStr + pk;

            //生成INSERT SQL.
            string insertSQL = "INSERT INTO " + tableName + " ( ";
            foreach (DataColumn dc in dt.Columns)
            {
                if (igF.Contains("@" + dc.ColumnName + "@"))
                    continue;
                insertSQL += dc.ColumnName + ",";
            }
            insertSQL = insertSQL.Substring(0, insertSQL.Length - 1);
            insertSQL += ") VALUES (";
            foreach (DataColumn dc in dt.Columns)
            {
                if (igF.Contains("@" + dc.ColumnName + "@"))
                    continue;
                insertSQL += BP.SystemConfig.AppCenterDBVarStr + dc.ColumnName + ",";
            }
            insertSQL = insertSQL.Substring(0, insertSQL.Length - 1);
            insertSQL += ")";
            #endregion gener sql.

            #region save to data.
            foreach (DataRow dr in dt.Rows)
            {
                BP.DA.Paras ps = new BP.DA.Paras();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (updataSQL.Contains(BP.SystemConfig.AppCenterDBVarStr + dc.ColumnName))
                        ps.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                ps.SQL = updataSQL;
                try
                {
                    if (BP.DA.DBAccess.RunSQL(ps) == 0)
                    {
                        ps.Clear();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (updataSQL.Contains(BP.SystemConfig.AppCenterDBVarStr + dc.ColumnName))
                                ps.Add(dc.ColumnName, dr[dc.ColumnName]);
                        }
                        ps.SQL = insertSQL;
                        BP.DA.DBAccess.RunSQL(ps);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    string pastrs = "";
                    foreach (Para p in ps)
                    {
                        pastrs += "\t\n@" + p.ParaName + "=" + p.val;
                    }
                    throw new Exception("@执行sql=" + ps.SQL + "失败." + ex.Message + "\t\n@paras=" + pastrs);
                }
            }
            #endregion save to data.
            return null;
        }

        [WebMethod]
        public string SaveEnum(string enumKey, string enumLab, string cfg)
        {
            SysEnumMain sem = new SysEnumMain();
            sem.No = enumKey;
            if (sem.RetrieveFromDBSources() == 0)
            {
                sem.Name = enumLab;
                sem.CfgVal = cfg;
                sem.Lang = WebUser.SysLang;
                sem.Insert();
            }
            else
            {
                sem.Name = enumLab;
                sem.CfgVal = cfg;
                sem.Lang = WebUser.SysLang;
                sem.Update();
            }

            string[] strs = cfg.Split('@');
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;
                string[] kvs = str.Split('=');
                SysEnum se = new SysEnum();
                se.EnumKey = enumKey;
                se.Lang = WebUser.SysLang;
                se.IntKey = int.Parse(kvs[0]);
                se.Lab = kvs[1];
                se.Insert();
            }
            return "save ok.";
        }
    }
}
