using System;
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

namespace FreeFrm.Web
{
    /// <summary>
    /// DA 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class FreeFrm : System.Web.Services.WebService
    {
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
        [WebMethod]
        public void NewDtl(string dtlNo, string fk_mapdata)
        {
            MapDtl dtl = new MapDtl();
            dtl.No = dtlNo;
            if (dtl.RetrieveFromDBSources() != 0)
                return;
            dtl.Name = dtlNo;
            dtl.FK_MapData = fk_mapdata;
            dtl.PTable = dtlNo;
            dtl.Insert();
            dtl.IntMapAttrs();
        }
        [WebMethod]
        public string BackUpFrm(string fk_mapdata)
        {
            try
            {
             //   string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Temp\\" + fk_mapdata + ".xml";
                string path =@"D:\ccflow\VisualFlow\Temp\" + fk_mapdata + ".xml";
                this.GenerFrm(fk_mapdata,0);
                ds.WriteXml(path);
                return null;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
      
        /// <summary>
        /// 装载表单模板
        /// </summary>
        /// <param name="fileByte">字节数</param>
        /// <param name="fk_mapData"></param>
        /// <returns></returns>
        [WebMethod]
        public string LoadFrmTemplete(byte[] fileByte, string fk_mapData)
        {
            try
            {
                string file = "\\Temp\\" + fk_mapData + ".xml";
                UploadFile(fileByte, file);
                string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + file;
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                MapData.ImpMapData(fk_mapData, ds);
                return null;
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 运行sqls
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        [WebMethod]
        public int RunSQLs(string sqls)
        {
            if (string.IsNullOrEmpty(sqls))
                return 0;

            int i = 0;
            string[] strs = sqls.Split('@');
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;
                i += BP.DA.DBAccess.RunSQL(str);
            }
            return i;
        }
        [WebMethod]
        public string DoType(string dotype, string v1, string v2, string v3, string v4,string v5)
        {
            string sql = "";
            try
            {
                switch (dotype)
                {
                    case "FrmTempleteExp":  //导出表单.
                        MapData mdfrmtem = new MapData(v1);
                        DataSet ds = mdfrmtem.GenerHisDataSet();
                        string  file = System.Web.HttpContext.Current.Request.PhysicalApplicationPath+"\\Temp\\" + v1 + ".xml";
                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                        ds.WriteXml(file);
                        return null;
                    case "FrmTempleteImp": //导入表单.
                        DataSet dsImp = new DataSet();
                        string fileImp = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Temp\\" + v1 + ".xml";
                        dsImp.ReadXml(fileImp); //读取文件.
                        MapData.ImpMapData(v1, dsImp);
                        return null;
                    case "NewHidF":
                        string fk_mapdata = v1;
                        string key = v2;
                        string name = v3;
                        int dataType  =int.Parse(v4);
                        MapAttr mdHid = new MapAttr();
                        mdHid.MyPK = fk_mapdata + "_" + key;
                        mdHid.FK_MapData = fk_mapdata;
                        mdHid.KeyOfEn = key;
                        mdHid.Name = name;
                        mdHid.MyDataType = dataType;
                        mdHid.HisEditType = EditType.Edit;
                        mdHid.MaxLen = 100;
                        mdHid.MinLen = 0;
                        mdHid.LGType = FieldTypeS.Normal;
                        mdHid.UIVisible = false;
                        mdHid.UIIsEnable=false; 
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
                        sql = "@DELETE WF_Frm WHERE No='" + delFK_Frm + "'";
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

                        bool isReadonly=false;
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
                            fn.IsReadonly = isReadonly;
                            fn.IsPrint = isPrint;
                            fn.Update();
                            return fk_frm;
                        }

                        fn.FK_Frm = fk_frm;
                        fn.FK_Flow = fk_flow;
                        fn.FK_Node = int.Parse(fk_Node);
                        fn.IsReadonly = isReadonly;
                        fn.IsPrint = isPrint;
                        fn.Idx = 100;
                        fn.Insert();

                        MapData md = new MapData();
                        md.No = fm.No;
                        md.Name = fm.Name;
                        md.PTable = "T" + md.No;
                        md.EnPK = "OID";
                        md.Insert();

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
                        return fk_frm;
                    default:
                        return "Error:";
                }
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }
        /// <summary>
        /// 
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
                    en.SetValByKey(key, ap.HisHT[key]);
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
        /// 运行sql返回table.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [WebMethod]
        public string RunSQLReturnTable(string sql)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(BP.DA.DBAccess.RunSQLReturnTable(sql));
            return Connector.ToXml(ds);
        }
        /// <summary>
        /// 运行sql返回table.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [WebMethod]
        public string RunSQLReturnTableS(string[] sqls)
        {
            DataSet ds = new DataSet();
            int i = 0;
            foreach (string sql in sqls)
            {
                if (string.IsNullOrEmpty(sql))
                    continue;
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                dt.TableName = "DT" + i;
                ds.Tables.Add(dt);
                i++;
            }
            return Connector.ToXml(ds);
        }
        [WebMethod]
        public string NewFields_del(string keyOfEn, string name, string fk_mapdata)
        {
            return null;
            try
            {
                string sql = "INSERT INTO Sys_MapAttr (MyPK,FK_MapData,KeyOfEn,Name,GroupID) VALUES ('" + fk_mapdata + "_" + keyOfEn + "','" + fk_mapdata + "','" + keyOfEn + "','" + name + "',-999)";
                DBAccess.RunSQL(sql);
                return null;
            }
            catch (Exception ex)
            {
                return "字段已存在，请用其它的字段名。" + ex.Message;
            }
        }
        [WebMethod]
        public string ParseStringToPinyin(string name)
        {
            try
            {
                return BP.DA.DataType.ParseStringToPinyin(name);
            }
            catch
            {
                return null;
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
        /// <param name="fk_mapdata"></param>
        /// <param name="workID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GenerFrm(string fk_mapdata,int workID)
        {
            return _GenerFrm(fk_mapdata, workID);
        }
        private string _GenerFrm(string fk_mapdata,int workID)
        {
            ds = new DataSet();
            // line.
            BP.Sys.FrmLines lins = new BP.Sys.FrmLines();
            lins.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            DataTable dt = lins.ToDataTableField();
            dt.TableName = "Sys_FrmLine";
            ds.Tables.Add(dt);

            //if (lins.Count == 0)
            //{
            //    this.InitFrm(fk_mapdata);
            //    return _GenerFrm(fk_mapdata,workID);
            //}

           // MapData md = new MapData(fk_mapdata);
           // ds= md.GenerHisDataSet();

            // link.
            BP.Sys.FrmLinks liks = new BP.Sys.FrmLinks();
            liks.Retrieve(FrmLinkAttr.FK_MapData, fk_mapdata);
            DataTable dtLink = liks.ToDataTableField();
            dtLink.TableName = "Sys_FrmLink";
            ds.Tables.Add(dtLink);

            // Img
            BP.Sys.FrmImgs imgs = new BP.Sys.FrmImgs();
            imgs.Retrieve(FrmImgAttr.FK_MapData, fk_mapdata);
            DataTable imgDt = imgs.ToDataTableField();
            imgDt.TableName = "Sys_FrmImg";
            ds.Tables.Add(imgDt);

            // Sys_FrmLab
            BP.Sys.FrmLabs labs = new BP.Sys.FrmLabs(fk_mapdata);
            labs.Retrieve(FrmLabAttr.FK_MapData, fk_mapdata);
            DataTable dtlabs = labs.ToDataTableField();
            dtlabs.TableName = "Sys_FrmLab";
            ds.Tables.Add(dtlabs);

            // Sys_FrmRB
            BP.Sys.FrmRBs rbs = new BP.Sys.FrmRBs(fk_mapdata);
            rbs.Retrieve(FrmRBAttr.FK_MapData, fk_mapdata);
            DataTable dtRB = rbs.ToDataTableField();
            dtRB.TableName = "Sys_FrmRB";
            ds.Tables.Add(dtRB);

            // MapAttrs
            BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs();
            QueryObject qo = new QueryObject(attrs);
            qo.AddWhere(BP.Sys.MapAttrAttr.FK_MapData, fk_mapdata);
            qo.addAnd();
            qo.AddWhere(BP.Sys.MapAttrAttr.UIVisible, 1);
            qo.addAnd();
            qo.AddWhereNotIn(BP.Sys.MapAttrAttr.KeyOfEn,
                "'BillNo','CDT','Emps','FID','FK_NY','MyNum','NodeState','OID','RDT','Rec','WFLog','WFState'");
            qo.DoQuery();
            ds.Tables.Add(attrs.ToDataTableField("Sys_MapAttr"));

            // MapDtl
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls();
            dtls.Retrieve(MapDtlAttr.FK_MapData, fk_mapdata);
            ds.Tables.Add(dtls.ToDataTableField("Sys_MapDtl"));

            // Map2m
            BP.Sys.MapM2Ms m2ms = new BP.Sys.MapM2Ms();
            m2ms.Retrieve(MapM2MAttr.FK_MapData, fk_mapdata);
            ds.Tables.Add(m2ms.ToDataTableField("Sys_MapM2M"));

            // FrmAttachments
            BP.Sys.FrmAttachments fjs = new BP.Sys.FrmAttachments();
            fjs.Retrieve(MapM2MAttr.FK_MapData, fk_mapdata);
            ds.Tables.Add(fjs.ToDataTableField("Sys_FrmAttachment"));

            // FrmImgAth
            BP.Sys.FrmImgAths imgaths = new BP.Sys.FrmImgAths();
            imgaths.Retrieve(MapM2MAttr.FK_MapData, fk_mapdata);
            ds.Tables.Add(imgaths.ToDataTableField("Sys_FrmImgAth"));

            // MapExt
            BP.Sys.MapExts exts = new BP.Sys.MapExts();
            exts.Retrieve(MapM2MAttr.FK_MapData, fk_mapdata);
            ds.Tables.Add(exts.ToDataTableField("Sys_MapExt"));

            // MapData
            BP.Sys.MapDatas mdatas = new BP.Sys.MapDatas();
            int i=mdatas.Retrieve(MapDataAttr.No, fk_mapdata);
            if (i != 0)
            {
                DataTable DTmdatas = mdatas.ToDataTableField("Sys_MapData");
                ds.Tables.Add(DTmdatas);
            }
            else
            {
                BP.Sys.MapDtls mdtls = new BP.Sys.MapDtls();
                mdtls.Retrieve(MapDtlAttr.No, fk_mapdata);

                DataTable DTmdtls = mdtls.ToDataTableField("Sys_MapDataDtl");
                ds.Tables.Add(DTmdtls);
            }

            //// MapData
            //BP.Sys.MapDatas enData = new BP.Sys.MapDatas();
            //mdatas.Retrieve(MapDataAttr.No, fk_mapdata);
            //ds.Tables.Add(mdatas.ToDataTableField("Sys_MapData"));
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
        [WebMethod(EnableSession = true)]
        public string CopyFrm(string fromMapData, string fk_mapdata)
        {
            this.LetAdminLogin();

            string timeKey = DateTime.Now.ToString("yyMMddhhmmss");

            #region 删除现有的当前节点数据, 并查询出来from节点数据.
            // line
            BP.Sys.FrmLines lins = new BP.Sys.FrmLines();
            lins.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            lins.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            int i = 0;
            foreach (BP.Sys.FrmLine item in lins)
            {
                BP.Sys.FrmLine toItem = new BP.Sys.FrmLine();
                toItem.Copy(item);
                toItem.MyPK = "Line" + timeKey + i;
                toItem.FK_MapData = fk_mapdata;
                toItem.DirectInsert();
                i++;
            }

            // link.
            BP.Sys.FrmLinks liks = new BP.Sys.FrmLinks();
            liks.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            liks.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            i = 0;
            foreach (BP.Sys.FrmLink item in liks)
            {
                BP.Sys.FrmLink toItem = new BP.Sys.FrmLink();
                toItem.Copy(item);
                toItem.MyPK = "Lik" + timeKey + i;
                //this.DealPK(item.MyPK, fromMapData, fk_mapdata);
                toItem.FK_MapData = fk_mapdata;
                toItem.DirectInsert();
                i++;
            }

            // Img
            i = 0;
            BP.Sys.FrmImgs imgs = new BP.Sys.FrmImgs();
            imgs.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            imgs.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            foreach (BP.Sys.FrmImg item in imgs)
            {
                BP.Sys.FrmImg toItem = new BP.Sys.FrmImg();
                toItem.Copy(item);
                toItem.MyPK = "Img" + timeKey + i;
                //this.DealPK(item.MyPK, fromMapData, fk_mapdata);
                toItem.FK_MapData = fk_mapdata;
                toItem.DirectInsert();
                i++;
            }

            // Sys_FrmLab
            BP.Sys.FrmLabs labs = new BP.Sys.FrmLabs();
            labs.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            labs.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            i = 0;
            foreach (BP.Sys.FrmLab item in labs)
            {
                BP.Sys.FrmLab toItem = new BP.Sys.FrmLab();
                toItem.Copy(item);
                toItem.MyPK = "Lab" + timeKey + i;
                //this.DealPK(item.MyPK, fromMapData, fk_mapdata);
                toItem.FK_MapData = fk_mapdata;
                toItem.DirectInsert();
                i++;
            }

            // Sys_FrmRB
            BP.Sys.FrmRBs rbs = new BP.Sys.FrmRBs();
            rbs.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            rbs.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            foreach (BP.Sys.FrmRB item in rbs)
            {
                BP.Sys.FrmRB toItem = new BP.Sys.FrmRB();
                toItem.Copy(item);
                toItem.MyPK = this.DealPK(item.MyPK, fromMapData, fk_mapdata);
                toItem.FK_MapData = fk_mapdata;
                toItem.DirectInsert();
            }


            // MapAttrs
            BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs();
            QueryObject qo = new QueryObject(attrs);
            qo.AddWhere(BP.Sys.MapAttrAttr.FK_MapData, fk_mapdata);
            qo.addAnd();
            qo.AddWhereNotIn(BP.Sys.MapAttrAttr.KeyOfEn,
                "'BillNo','CDT','Emps','FID','FK_Dept','FK_NY','MyNum','NodeState','OID','RDT','Rec','Title','WFLog','WFState'");
            qo.DoQuery();
            attrs.Delete();
            qo.clear();
            qo.AddWhere(BP.Sys.MapAttrAttr.FK_MapData, fromMapData);
            qo.addAnd();
            qo.AddWhereNotIn(BP.Sys.MapAttrAttr.KeyOfEn,
                "'BillNo','CDT','Emps','FID','FK_Dept','FK_NY','MyNum','NodeState','OID','RDT','Rec','Title','WFLog','WFState'");
            qo.DoQuery();
            foreach (BP.Sys.MapAttr attr in attrs)
            {
                BP.Sys.MapAttr attrNew = new BP.Sys.MapAttr();
                attrNew.Copy(attr);
                attrNew.FK_MapData = fk_mapdata;
                attrNew.UIIsEnable = false;
                if (attrNew.DefValReal.Contains("@"))
                    attrNew.DefValReal = "";
                attrNew.HisEditType = EditType.Edit;
                attrNew.Insert();
            }

            // MapDtl
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls();
            dtls.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            dtls.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            // 复制明细表.
            foreach (MapDtl dtl in dtls)
            {
                MapDtl dtlNew = new MapDtl();
                dtlNew.Copy(dtl);
                dtlNew.FK_MapData = fk_mapdata;
                dtlNew.No = dtl.No.Replace(fromMapData, fk_mapdata);

                dtlNew.IsInsert = false;
                dtlNew.IsUpdate = false;
                dtlNew.IsDelete = false;
                dtlNew.GroupID = 0;
                dtlNew.PTable = dtlNew.No;
                dtlNew.Insert();

                // 复制明细表里面的明细。
                int idx = 0;
                MapAttrs mattrs = new MapAttrs(dtl.No);
                mattrs.Delete(MapAttrAttr.FK_MapData, dtlNew.No);
                foreach (MapAttr attr in mattrs)
                {
                    MapAttr attrNew = new MapAttr();
                    attrNew.Copy(attr);
                    attrNew.FK_MapData = dtlNew.No;
                    attrNew.UIIsEnable = false;
                    if (attrNew.DefValReal.Contains("@"))
                        attrNew.DefValReal = "";

                    dtlNew.RowIdx = idx;
                    attrNew.HisEditType = EditType.Edit;
                    attrNew.Insert();
                }
            }

            // Map2m
            BP.Sys.MapM2Ms m2ms = new BP.Sys.MapM2Ms();
            m2ms.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            m2ms.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            foreach (MapM2M m2m in m2ms)
            {
                MapM2M mym2m = new MapM2M();
                mym2m.No = m2m.No.Replace(fromMapData, fk_mapdata);
                if (mym2m.IsExits)
                    continue;

                mym2m.Copy(m2m);
                mym2m.FK_MapData = fk_mapdata;
                mym2m.GroupID = 0;
                mym2m.No = m2m.No.Replace(fromMapData, fk_mapdata);
                mym2m.Insert();
            }


            // FrmAttachments
            BP.Sys.FrmAttachments aths = new BP.Sys.FrmAttachments();
            aths.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            aths.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            i = 0;
            foreach (FrmAttachment ath in aths)
            {
                FrmAttachment myath = new FrmAttachment();
                myath.Copy(ath);
                myath.FK_MapData = fk_mapdata;
                myath.MyPK = "Ath" + timeKey + i;
                myath.Insert();
            }

            // FrmImgAth
            BP.Sys.FrmImgAths imgAths = new BP.Sys.FrmImgAths();
            imgAths.Delete(BP.Sys.FrmLineAttr.FK_MapData, fk_mapdata);
            imgAths.Retrieve(BP.Sys.FrmLineAttr.FK_MapData, fromMapData);
            i = 0;
            foreach (FrmImgAth ath in imgAths)
            {
                FrmImgAth myath = new FrmImgAth();
                myath.Copy(ath);
                myath.FK_MapData = fk_mapdata;
                myath.MyPK = "ImgAth" + timeKey + i;
                myath.Insert();
            }
            #endregion 删除现有的当前节点数据. 并查询出来from节点数据.

            return "copy ok.";
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
