using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// Method ��ժҪ˵��
    /// </summary>
    public class OneKeyBackWF : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public OneKeyBackWF()
        {
            this.Title = "һ���������������";
            this.Help = "���������������xml�ĵ����ݵ�C:\\CCFlowTemplete���档";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "��ȷ��Ҫִ����";
            //HisAttrs.AddTBString("P1", null, "ԭ����", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "������", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "ȷ��", true, false, 0, 10, 10);
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
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string path = "C:\\CCFlowTemplete" + DateTime.Now.ToString("yy��MM��dd��HHʱmm��ss��");
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
                FlowSort fs = new FlowSort(fl.FK_FlowSort);
                string pathDir = path + "\\Flow\\" + fs.No + "." + fs.Name;
                if (System.IO.Directory.Exists(pathDir) == false)
                    System.IO.Directory.CreateDirectory(pathDir);

              //  fl.GenerFlowXmlTemplete(pathDir + "\\" + fl.Name + ".xml");
                fl.GenerFlowXmlTemplete(pathDir);
            }

            MapDatas mds = new MapDatas();
            mds.RetrieveAllFromDBSource();
            foreach (MapData md in mds)
            {
                if (md.FK_FrmSort.Length < 2)
                    continue;

                FrmSort fs = new FrmSort(md.FK_FrmSort);
                string pathDir = path + "\\Form\\" + fs.No + "." + fs.Name;
                if (System.IO.Directory.Exists(pathDir) == false)
                    System.IO.Directory.CreateDirectory(pathDir);
                DataSet ds = md.GenerHisDataSet();
                ds.WriteXml(pathDir + "\\" + md.Name + ".xml");
            }
            return "ִ�гɹ�,���·��:" + path;
        }
    }
}
