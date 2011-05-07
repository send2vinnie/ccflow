using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using System.IO;
using System.Text;
using BP.Web ; 
using BP.Win32.Comm;
using BP.DTS;
//using Cells = SourceGrid2.Cells.Real;  
 
namespace BP.Win32
{  
	/// <summary>  
	/// FrmBase 的摘要说明。
	/// </summary>
    public class PageBase : System.Windows.Forms.Form
    {
        public string ToE(string no, string chVal)
        {
            return BP.Sys.Language.GetValByUserLang(no, chVal);
        }
        //D:\市场\南京税通软件产品手册 

        public void ShowIt(System.Windows.Forms.Form parentFrm)
        {
            if (parentFrm == null)
                return;

            foreach (System.Windows.Forms.Form f in parentFrm.MdiChildren)
            {
                if (f.Tag == this.Tag)
                {
                    f.Activate();
                    return;
                }
            }
            this.MdiParent = parentFrm;
            this.Show();
        }
        public void RunTextFile(string file, string msg)
        {
        }

        public void RunExeFile(string file, string msg)
        {
            try
            {
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception ex)
            {
                this.ResponseWriteRedMsg(msg + " \n异常信息:" + ex.Message + " filename:" + file);
            }
        }

        public void OpenWord(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " file= " + fileName);
            }
            //System.Diagnostics.Process.Start("word.exe", fileName)  ;

            //System.Diagnostics.Process( fileName); 
        }
        protected bool Question(string msg)
        {
            return PubClass.Question(msg);
        }
        protected bool Warning(Exception ex)
        {
            return PubClass.Warning(ex.Message);
        }

        protected bool Warning(string msg)
        {
            return PubClass.Warning(msg);
        }
        private void ShowInTaskBarEx()
        {

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            // Do not allow form to be displayed in taskbar.
            this.ShowInTaskbar = false;

        }

        private DataTable _Table = null;

        protected DataTable Table
        {
            get
            {
                if (this._Table == null)
                    this._Table = new DataTable();

                return this._Table;
            }
            set
            {
                this._Table = value;

            }
        }


        #region 调用方法
        /// <summary>
        /// 数据库清除
        /// </summary>
        protected void InvokSqlLogClear()
        {
            this.RunExeFile("D:\\WebApp\\tools\\SqlLogClear\\SqlLogClear.exe", "数据库清除");
        }
        protected void InvokUIDTS()
        {
            FrmDTS dts = new FrmDTS();
            dts.ShowDialog();
            return;
        }
        protected void InvokUIDTS(System.Windows.Forms.Form frm)
        {
            FrmDTS dts = new FrmDTS();
            dts.MdiParent = frm;
            dts.Show();
            return;
        }
        protected void InvokUIUserEns(EnType type)
        {
            UIUserEns ui = new UIUserEns();
            ui.BindByEnType(type);
            return;
        }

        public void InvokUIEn(Entity en, bool IsReadonly)
        {
            try
            {
                BP.Win32.Comm.En ui = new BP.Win32.Comm.En();
                ui.Bind2(en);
                ui.ShowDialog();
            }
            catch (Exception ex)
            {
                en.CheckPhysicsTable();
                this.Warning(ex);
            }
            return;
        }
        public void InvokUIEn_del(Entity en, UAC uac)
        {
            try
            {
                BP.Win32.Comm.En ui = new BP.Win32.Comm.En();
                //ui.Bind2(en, uac.IsUpdate );
                ui.MdiParent = this;
                ui.Show();
            }
            catch (Exception ex)
            {
                en.CheckPhysicsTable();
                this.Warning(ex);
            }
            return;
        }
        public void InvokUIEn(Entity en, UAC uac)
        {
            try
            {
                UIEn ui = new UIEn();
                ui.SetData(en, uac.IsUpdate);
                ui.ShowDialog();

            }
            catch (Exception ex)
            {
                en.CheckPhysicsTable();
                this.Warning(ex);
            }
            return;
        }
        /// <summary>
        /// 用这种方法，交给系统自动判断权限
        /// </summary>
        /// <param name="ens">要编辑的Entities</param>
        public void InvokUIEns(Entities ens, System.Windows.Forms.Form frm)
        {
            UAC uac = ens.GetNewEntity.HisUAC; //  new SysEnsUAC(ens,WebUser.No) ; 
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.MdiParent = frm;
            ui.Show();
            return;
        }
        public void InvokUIEns(Entities ens, BP.En.UAC uac, System.Windows.Forms.Form frm)
        {
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.MdiParent = frm;
            ui.Show();
            return;
        }
        protected void InvokUIEns(Entities ens)
        {
            UAC uac = ens.GetNewEntity.HisUAC; //, WebUser.No) ; 
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.Show();
            return;
        }
        /// <summary>
        /// InvokUIEns 权限控制
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="uac">权限控制</param>
        protected void InvokUIEns(Entities ens, UAC uac)
        {
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.ShowDialog();
            return;
        }

        //		/// <summary>
        //		/// 调用UIEns
        //		/// </summary>
        //		/// <param name="ens"></param>
        //		/// <param name="IsReadonly"></param>
        //		protected void InvokUIEns(Entities ens,bool IsReadonly)
        //		{
        //			UIEns ui = new UIEns() ;
        //			ui.BindEns(ens,IsReadonly,true);
        //			ui.ShowDialog();
        //			return;			
        //		}
        #endregion

        #region  导出各种文件
        /// <summary>
        /// 导出文件的路径
        /// </summary>
        protected string ExportFilePath
        {
            get
            {
                return "d:\\Report\\Exported\\";
            }
        }

        /// <summary>
        /// 输出文件格式文档
        /// </summary>
        /// <param name="en"></param>
        public void ExportExcelExcelTemplate(Entity en, string file)
        {
            Attrs attrs = en.EnMap.Attrs;

            //string filename = en.EnDesc+".xls";
            //string file=en.EnDesc;
            //string filepath= filePath;

            //			//如果导出目录没有建立，则建立.
            //			if (Directory.Exists(filepath) == false) 
            //				Directory.CreateDirectory(filepath);
            //
            //			//filename = filepath + filename;
            FileStream objFileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
        #endregion

            #region 生成导出文件
            string strLine = "";
            foreach (Attr attr in attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                strLine += attr.Desc + Convert.ToChar(9);
            }
            objStreamWriter.WriteLine(strLine);
            objStreamWriter.Close();
            objFileStream.Close();
            PubClass.Information("文件格式已经存放在: \n" + file);
            //this.ResponseWriteBlueMsg("文件格式已经存放在: "+filename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="file"></param>
        public void ExportDGToExcel(DG dg, string file)
        {
            Map map = dg.HisEn.EnMap;

            #region 定义导出的文件
            //			string filename = map.EnDesc +DateTime.Now.ToString("yyyyMMddhhss") + ".xls";
            //			string file=filename;
            //			//bool flag = true;
            //			string filepath=this.ExportFilePath;
            //			
            //			//如果导出目录没有建立，则建立.
            //			if (Directory.Exists(filepath) == false) 
            //				Directory.CreateDirectory(filepath);
            //
            //			filename = filepath + filename;
            FileStream objFileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            //			int i =0 ;
            //objStreamWriter.WriteLine();
            //objStreamWriter.WriteLine(Convert.ToChar(9) + Convert.ToChar(9)+ "数据导出1.0" + Convert.ToChar(9) );
            //objStreamWriter.WriteLine();

            DataSet ds = new DataSet();
            try
            {
                DataTable dt = (DataTable)dg.DataSource;
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                this.Alert("不能向DataSet转换" + ex.Message);
            }

            //数据表样式
            foreach (DataGridTableStyle dts in dg.TableStyles)
            {
                //objStreamWriter.WriteLine(Convert.ToChar(9) + "导出"+dts.MappingName + Convert.ToChar(9) );
                /*输出标题*/
                foreach (Attr attr in map.Attrs)
                {

                    if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        continue;

                    if (attr.UIVisible == false)
                    {
                        if (attr.MyFieldType != FieldType.RefText)
                            continue;
                    }
                    if (attr.MyFieldType == FieldType.RefText)
                        objStreamWriter.Write(attr.Desc.Substring(0, attr.Desc.Length - 2));
                    else
                        objStreamWriter.Write(attr.Desc);

                    objStreamWriter.Write("\t");
                }
                objStreamWriter.WriteLine();

                DataTable dt = ds.Tables[0];

                //				objStreamWriter.Write("\r\n");
                /*输出内容*/
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (Attr attr in map.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.RefText)
                        {
                            objStreamWriter.Write(dr[attr.Key]);
                            objStreamWriter.Write('\t');
                            continue;
                        }

                        if (attr.UIVisible == false)
                            continue;
                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                            continue;
                        objStreamWriter.Write(dr[attr.Key]);
                        objStreamWriter.Write('\t');
                    }
                    objStreamWriter.WriteLine();
                }
            }

            #region 处理目录
            objStreamWriter.Close();
            objFileStream.Close();
            PubClass.Information("数据导出成功,存放位置:\n" + file);
            #endregion

        }

        /// <summary>
        /// ExportToXml
        /// </summary>
        /// <param name="ens"></param>
        public void ExportToXml(BP.En.Entities ens)
        {
            string file = ens.GetNewEntity.EnMap.PhysicsTable + ".xml";
            string filepath = this.ExportFilePath;

            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

            string fileName = filepath + file;

            DataSet ds = ens.RetrieveAllToDataSet();
            /*WriteSchema 以关系结构作为内联 XML 架构
            IgnoreSchema 以 XML 数据形式编写 DataSet 的当前内容，不包含 XML 架构
             DiffGram 当前和初始版本的 XML 格式*/
            ds.WriteXml(fileName, System.Data.XmlWriteMode.DiffGram);
            //			this.Alert("数据导出成功!!"+fileName);
            MessageBox.Show(fileName, "数据导出成功!");


        }

            #endregion  导出文件


        #region ShowDefaultValue
        /// <summary>
        /// 显示输入默认值
        /// </summary>
        /// <param name="en">实体</param>
        /// <param name="attrKey">属性</param>
        protected void ShowDefaultValue(Entity en, string attrKey)
        {
            UIWinDefaultValues window = new UIWinDefaultValues(en, attrKey);
        }
        #endregion

        #region 实体变量 Ens
        private Entity _HisEn = null;
        public Entity HisEn
        {
            get
            {
                if (this._HisEn == null)
                {
                    // throw new Exception("@没有为 HisEn 设置值。");
                    //  _HisEn = new Emp();
                }
                return _HisEn;
            }
            set
            {
                _HisEn = value;
            }
        }
        private Entities _HisEns = null;
        public Entities HisEns
        {
            get
            {
                return _HisEns;
            }
            set
            {
                _HisEns = value;
            }
        }
        #endregion



        #region  提示信息

        protected void ResponseWriteRedMsg(Exception ex)
        {
            PubClass.Alert(ex.Message);
            //MessageBox.Show(ex.Message,"错误");
        }
        protected void ResponseWriteRedMsg(string msg)
        {
            PubClass.Alert(msg);

            //MessageBox.Show(msg.Replace("@","\n"),"错误");
        }
        protected void ResponseWriteBlueMsg(string msg)
        {
            MessageBox.Show(this, msg.Replace("@", "\n"), "提示");
        }
        protected void ResponseWriteBlueMsg_DeleteOK()
        {
            MessageBox.Show("删除成功!", "提示");
        }
        protected void ResponseWriteBlueMsg_DeleteOK(int delNum)
        {
            MessageBox.Show("记录" + delNum + "删除成功!", "信息删除提示");

        }
        protected void ResponseWriteBlueMsg_UpdataOK()
        {
            MessageBox.Show("更新成功!", "信息更新提示");

        }
        protected void ResponseWriteBlueMsg_UpdataOK(int updateNum)
        {
            MessageBox.Show("更新" + updateNum + "成功!", "信息更新提示");

        }
        protected void ResponseWriteBlueMsg_SaveOK()
        {
            MessageBox.Show("保存成功!", "信息保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        protected void ResponseWriteBlueMsg_InsertOK()
        {
            MessageBox.Show("插入成功!", "提示");

        }
        protected void ResponseWriteBlueMsg_SaveOK(int saveNum)
        {
            MessageBox.Show("保存" + saveNum + "成功!", "提示");
        }
        protected void Alert(string msg)
        {
            PubClass.Alert(msg);

        }
        protected void Information(string msg)
        {
            PubClass.Information(msg);
        }
        protected void Alert(Exception ex)
        {
            this.Alert(ex.Message);
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        protected void Exit()
        {
            if (this.Question("您确定要退出本系统吗？"))
                Application.Exit();
        }




        #endregion

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public PageBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }



        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // PageBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "PageBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmBase";
            this.Load += new System.EventHandler(this.PageBase_Load);

        }
        #endregion

        private void PageBase_Load(object sender, System.EventArgs e)
        {

        }
    }
}
