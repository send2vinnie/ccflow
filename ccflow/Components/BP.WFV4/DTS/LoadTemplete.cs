using System;
using System.IO;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
namespace BP.WF.DTS
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class LoadTemplete : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public LoadTemplete()
        {
            this.Title = "װ��������ʾģ��";
            this.Help = "Ϊ�˰�����λ������ѧϰ������ccflow, ���ṩһЩ����ģ�����ģ���Է���ѧϰ��";
            this.Help += "@��Щģ���λ��" + SystemConfig.PathOfData + "\\FlowDemo\\";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        public override object Do()
        {
            string msg = "";
            FlowSorts sorts = new FlowSorts();
            sorts.ClearTable();

            DirectoryInfo dirInfo = new DirectoryInfo(SystemConfig.PathOfData + "\\FlowDemo\\Flow\\");
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if (dir.FullName.Contains(".svn"))
                    continue;

                string[] fls = System.IO.Directory.GetFiles(dir.FullName);
                if (fls.Length == 0)
                    continue;

                FlowSort fs = new FlowSort();
                fs.No = dir.Name.Substring(0, 2);
                fs.Name = dir.Name.Substring(3);
                fs.Insert();
                foreach (string f in fls)
                {
                    try
                    {
                        msg += "@��ʼ��������ģ���ļ�:" + f;
                        Flow myflow = BP.WF.Flow.DoLoadFlowTemplate(fs.No, f);
                        msg += "@����:" + myflow.Name + "װ�سɹ���";
                        System.IO.FileInfo info = new System.IO.FileInfo(f);
                        myflow.Name = info.Name.Replace(".xml", "");
                        myflow.DirectUpdate();
                    }
                    catch (Exception ex)
                    {
                        msg += "@����ʧ��" + ex.Message;
                    }
                }
            }
 
            // ���ȱ��ļ���
            FrmSorts fss = new FrmSorts();
            fss.ClearTable();

            string frmPath = SystemConfig.PathOfData + "\\FlowDemo\\Form\\";
              dirInfo =new DirectoryInfo(frmPath);
              dirs = dirInfo.GetDirectories();
            foreach (DirectoryInfo item in dirs)
            {
                if (item.FullName.Contains(".svn"))
                    continue;

                string[] fls = System.IO.Directory.GetFiles(item.FullName);
                if (fls.Length == 0)
                    continue;
                FrmSort fs = new FrmSort();
                fs.No = item.Name.Substring(0, 2);
                fs.Name = item.Name.Substring(3);
                fs.Insert();

                foreach (string f in fls)
                {
                    try
                    {
                        msg += "@��ʼ���ȱ�ģ���ļ�:" + f;
                        System.IO.FileInfo info = new System.IO.FileInfo(f);
                        if (info.Extension != ".xml")
                            continue;
                        DataSet ds = new DataSet();
                        ds.ReadXml(f);

                        MapData md = MapData.ImpMapData(ds);
                        md.FK_FrmSort = fs.No;
                        md.Update();

                    }
                    catch (Exception ex)
                    {
                        msg += "@����ʧ��" + ex.Message;
                    }
                }
            }

            BP.DA.Log.DefaultLogWriteLineInfo(msg);
            return msg;
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public   object Do11()
        {
            #region 2011-08-02 ������ mapdata �����б仯.
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            foreach (Node nd in nds)
            {
                MapData md = new MapData();
                md.No = "ND" + nd.NodeID;
                if (md.RetrieveFromDBSources() == 0)
                {
                    md.Name = nd.Name;
                    md.EnPK = "OID";
                    md.PTable = md.No;
                    md.Insert();
                }
                else
                {
                    md.Name = nd.Name;
                    md.EnPK = "OID";
                    md.PTable = md.No;
                    md.Update();
                }
            }
            MapDtls dtls = new MapDtls();
            dtls.RetrieveAll();
            foreach (MapDtl dtl in dtls)
            {
                MapData md = new MapData();
                md.No = dtl.No;
                if (md.RetrieveFromDBSources() == 0)
                {
                    md.Name = dtl.Name;
                    md.EnPK = "OID";
                    md.PTable = dtl.PTable;
                    md.Insert();
                }
                else
                {
                    md.Name = dtl.Name;
                    md.EnPK = "OID";
                    md.PTable = dtl.PTable;
                    md.Update();
                }
            }
            #endregion 2011-08-02 ������ mapdata �����б仯.

            return "ִ�гɹ�...";
        }
    }
}
