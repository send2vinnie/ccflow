using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
	///  ���ڶ�Entity��չ���ķ�����
	/// </summary>
    public class EnExt
    {
        ///   <summary>
        ///   �ж�һ���ַ����Ƿ�ȫ��ΪӢ����ĸ��ɵģ����Ƿ���true�����򷵻�false   
        ///   </summary>   
        ///   <param   name="sQYMC">�ַ���</param>   
        ///   <returns></returns>   
        public static bool IsZM(string sQYMC)
        {
            //���ַ�����ΪСд��ʽ����תΪ�ַ�������   
            char[] cQYMC = sQYMC.ToLower().ToCharArray();

            //�������飬����ĸ��ascii��ֵ�Ƚϣ������һ�������䷶Χ֮�࣬��Ǻ���ƴ��   
            //a--97,z--122   
            for (int i = 0; i < sQYMC.Length; i++)
            {
                if (!(cQYMC[i] >= 97 && cQYMC[i] <= 122))
                    return false;
            }
            return true;
        }


        #region �õ�ddl �ķ�����
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
                throw new Exception("@ʵ�弯������û������.");
            else
                throw new Exception("@û���ҵ�No=" + no + "��ʵ������");
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

        #region ���ڶ�entity �Ĵ���

        #region ת��dataset
        /// <summary>
        /// ��ָ����ens ת��Ϊ dataset
        /// </summary>
        /// <param name="spen">ָ����ens</param>
        /// <returns>���ع�ϵdataset</returns>
        public static DataSet ToDataSet(Entities spens)
        {

            DataSet ds = new DataSet(spens.ToString());

            /* ���������DataSet */
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
            dt.TableName = en.EnDesc; //�趨��������ơ�
            dt.RowChanged += new DataRowChangeEventHandler(dt_RowChanged);

            //dt.RowChanged+=new DataRowChangeEventHandler(dt_RowChanged);

            ds.Tables.Add(DealBoolTypeInDataTable(en, dt));


            foreach (EnDtl ed in en.EnMap.DtlsAll)
            {
                /* ѭ���������ϸ���༭�ù�ϵ�������Ƿ��� DataSet ���档*/
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
                    /* �������ϸ����ϸ��*/
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
        /// ��������Ϣ,��vlaue ת��ΪTable
        /// </summary>
        /// <param name="en">Ҫת����entity</param>
        /// <param name="editStyle">�༭���</param>
        /// <returns>datatable</returns>
        public static DataTable ToTable(Entity en, int editStyle)
        {
            if (editStyle == 0)
                return EnExt.ToTable0(en);
            else
                return EnExt.ToTable1(en);
        }
        /// <summary>
        /// �û����0
        /// </summary>
        /// <returns></returns>
        private static DataTable ToTable0(Entity en)
        {
            string nameOfEnterInfo = en.EnDesc;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("������Ŀ", typeof(string)));
            dt.Columns.Add(new DataColumn(nameOfEnterInfo, typeof(string)));
            dt.Columns.Add(new DataColumn("��Ϣ����Ҫ��", typeof(string)));

            foreach (Attr attr in en.EnMap.Attrs)
            {
                DataRow dr = dt.NewRow();
                dr["������Ŀ"] = attr.Desc;
                //if (attr.Key=="NodeState" && this.GetValByKey(attr.Key).ToString()=="&nbsp;")
                //throw new Exception("@error ");
                dr[nameOfEnterInfo] = en.GetValByKey(attr.Key);
                dr["��Ϣ����Ҫ��"] = attr.EnterDesc;
                dt.Rows.Add(dr);
            }
            // ���ʵ����Ҫ������
            if (en.EnMap.AdjunctType != AdjunctType.None)
            {
                // ���븽����Ϣ��
                DataRow dr1 = dt.NewRow();
                dr1["������Ŀ"] = "����";
                dr1[nameOfEnterInfo] = "";
                dr1["��Ϣ����Ҫ��"] = "�༭����";
                dt.Rows.Add(dr1);
            }
            // ��ϸ
            foreach (EnDtl dtl in en.EnMap.Dtls)
            {
                DataRow dr = dt.NewRow();
                dr["������Ŀ"] = dtl.Desc;
                dr[nameOfEnterInfo] = "EnsName_" + dtl.Ens.ToString() + "_RefKey_" + dtl.RefKey;
                dr["��Ϣ����Ҫ��"] = "�����༭��ϸ";
                dt.Rows.Add(dr);
            }
            foreach (AttrOfOneVSM attr in en.EnMap.AttrsOfOneVSM)
            {
                DataRow dr = dt.NewRow();
                dr["������Ŀ"] = attr.Desc;
                dr[nameOfEnterInfo] = "OneVSM" + attr.EnsOfMM.ToString();
                dr["��Ϣ����Ҫ��"] = "�����༭��ѡ";
                dt.Rows.Add(dr);
            }
            return dt;

        }
        /// <summary>
        /// �û����1
        /// </summary>
        /// <returns></returns>
        private static DataTable ToTable1(Entity en)
        {

            string col1 = "�ֶ���1";
            string col2 = "����1";
            string col3 = "�ֶ���2";
            string col4 = "����2";

            //string enterNote=null;
            //			if (this.EnMap.Dtls.Count==0 || this.EnMap.AttrsOfOneVSM.Count==0)
            //				enterNote="����1";
            //			else
            //				enterNote="�������ܱ༭������Ϣ";


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


            // ���ʵ����Ҫ������
            if (en.EnMap.AdjunctType != AdjunctType.None)
            {
                // ���븽����Ϣ��
                DataRow dr1 = dt.NewRow();
                dr1[col1] = "����";
                dr1[col2] = "�༭����";
                //dr["������Ŀ2"]="������Ϣ";

                dt.Rows.Add(dr1);
            }
            // ��ϸ
            foreach (EnDtl dtl in en.EnMap.Dtls)
            {
                DataRow dr = dt.NewRow();
                dr[col1] = dtl.Desc;
                dr[col2] = "EnsName_" + dtl.Ens.ToString() + "_RefKey_" + dtl.RefKey;
                //dr["������Ŀ2"]="��ϸ��Ϣ";
                dt.Rows.Add(dr);
            }
            // ��Զ�Ĺ�ϵ
            foreach (AttrOfOneVSM attr in en.EnMap.AttrsOfOneVSM)
            {
                DataRow dr = dt.NewRow();
                dr[col1] = attr.Desc;
                dr[col2] = "OneVSM" + attr.EnsOfMM.ToString();
                //dr["������Ŀ2"]="��ѡ";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region ��
        /// <summary>
        /// ͨ��һ�����ϣ�һ��key��һ���ָ���ţ����������Ե��Ӵ���
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
        /// ͨ��һ�����ϣ�һ���ָ���ţ����������Ե��Ӵ���
        /// </summary>		
        /// <param name="listspt"></param>
        /// <returns></returns>
        public static string GetEnsString(Entities ens, string listspt)
        {
            return GetEnsString(ens, ens.GetNewEntity.PK, listspt);
        }
        /// <summary>
        /// ͨ��һ�����ϻ��������Ե��Ӵ���
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
	/// ���ݼ�鼶��
	/// </summary>
    public enum DBLevel
    {
        /// <summary>
        /// ��,ֻ������,�������κ�����
        /// </summary>
        Low = 1,
        /// <summary>
        /// ��,����鱨��,ɾ����������ҿո�.
        /// </summary>
        Middle = 2,
        /// <summary>
        /// ��,ɾ����Ӧ���ϵ�����.
        /// </summary>
        High = 3,
    }
	/// <summary>
	/// PageBase ��ժҪ˵����
	/// </summary>
	public class PubClass 
	{
        public static string ToHtmlColor(string colorName)
        {
            try
            {
                if (colorName.StartsWith("#"))
                    colorName = colorName.Replace("#", string.Empty);
                int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);

                Color col = Color.FromArgb
               (
                     Convert.ToByte((v >> 24) & 255),
                     Convert.ToByte((v >> 16) & 255),
                     Convert.ToByte((v >> 8) & 255),
                     Convert.ToByte((v >> 0) & 255)
                );

                int alpha = col.A;
                var red = Convert.ToString(col.R, 16); ;
                var green = Convert.ToString(col.G, 16);
                var blue = Convert.ToString(col.B, 16);
                return string.Format("#{0}{1}{2}", red, green, blue);
            }
            catch
            {
                return "black";
            }
        }
        public static void InitFrm(string fk_mapdata)
        {
            // ɾ������.
            FrmLabs labs = new FrmLabs();
            labs.Delete(FrmLabAttr.FK_MapData, fk_mapdata);

            FrmLines lines = new FrmLines();
            lines.Delete(FrmLabAttr.FK_MapData, fk_mapdata);

            MapData md = new MapData();
            md.No = fk_mapdata;
            if (md.RetrieveFromDBSources() == 0)
            {
                MapDtl mdtl = new MapDtl();
                mdtl.No = fk_mapdata;
                if (mdtl.RetrieveFromDBSources() == 0)
                {
                    throw new Exception("@��:" + fk_mapdata+"��ӳ����Ϣ������.");
                }
                else
                {
                    md.Copy(mdtl);
                }
            }

            MapAttrs mattrs = new MapAttrs(fk_mapdata);
            GroupFields gfs = new GroupFields(fk_mapdata);

            int tableW = 700;
            int padingLeft = 3;
            int leftCtrlX = 700 / 100 * 20;
            int rightCtrlX = 700 / 100 * 60;

            string keyID = DateTime.Now.ToString("yyMMddhhmmss");
            // table ���⡣
            int currX = 0;
            int currY = 0;
            FrmLab lab = new FrmLab();
            lab.Text = md.Name;
            lab.FontSize = 20;
            lab.X = 200;
            currY += 30;
            lab.Y = currY;
            lab.FK_MapData = fk_mapdata;
            lab.FontWeight = "Bold";
            lab.MyPK = "Lab"+keyID + "1";
            lab.Insert();

            // ���ͷ���ĺ���.
            currY += 20;
            FrmLine lin = new FrmLine();
            lin.X1 = 0;
            lin.X2 = tableW;
            lin.Y1 = currY;
            lin.Y2 = currY;
            lin.BorderWidth = 2;
            lin.FK_MapData = fk_mapdata;
            lin.MyPK = "Lin"+keyID + "1";
            lin.Insert();
            currY += 5;

            bool isLeft = false;
            int i = 2;
            foreach (GroupField gf in gfs)
            {
                i++;
                lab = new FrmLab();
                lab.X = 0;
                lab.Y = currY;
                lab.Text = gf.Lab;
                lab.FK_MapData = fk_mapdata;
                lab.FontWeight = "Bold";
                lab.MyPK = "Lab" + keyID + i.ToString();
                lab.Insert();

                currY += 15;
                lin = new FrmLine();
                lin.X1 = padingLeft;
                lin.X2 = tableW;
                lin.Y1 = currY;
                lin.Y2 = currY;
                lin.FK_MapData = fk_mapdata;
                lin.BorderWidth = 3;
                lin.MyPK = "Lin" + keyID + i.ToString();
                lin.Insert();

                isLeft = true;
                int idx = 0;
                foreach (MapAttr attr in mattrs)
                {
                    if (gf.OID != attr.GroupID || attr.UIVisible == false)
                        continue;

                    idx++;
                    if (isLeft)
                    {
                        lin = new FrmLine();
                        lin.X1 = 0;
                        lin.X2 = tableW;
                        lin.Y1 = currY;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx;
                        lin.Insert();
                        currY += 14; /* ��һ���� .*/

                        lab = new FrmLab();
                        lab.X = lin.X1 + padingLeft;
                        lab.Y = currY;
                        lab.Text = attr.Name;
                        lab.FK_MapData = fk_mapdata;
                        lab.MyPK = "Lab" + keyID + i.ToString() + idx;
                        lab.Insert();

                        lin = new FrmLine();
                        lin.X1 = leftCtrlX;
                        lin.Y1 = currY - 14;

                        lin.X2 = leftCtrlX;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx + "R";
                        lin.Insert(); /*��һ ���� */

                        attr.X = leftCtrlX + padingLeft;
                        attr.Y = currY - 3;
                        attr.UIWidth = 150;
                        attr.Update();
                        currY += 14;
                    }
                    else
                    {
                        currY = currY - 14;
                        lab = new FrmLab();
                        lab.X = tableW / 2 + padingLeft;
                        lab.Y = currY;
                        lab.Text = attr.Name;
                        lab.FK_MapData = fk_mapdata;
                        lab.MyPK = "Lab" + keyID + i.ToString() + idx;
                        lab.Insert();

                        lin = new FrmLine();
                        lin.X1 = tableW / 2;
                        lin.Y1 = currY - 14;

                        lin.X2 = tableW / 2;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx;
                        lin.Insert(); /*��һ ���� */

                        lin = new FrmLine();
                        lin.X1 = rightCtrlX;
                        lin.Y1 = currY - 14;
                        lin.X2 = rightCtrlX;
                        lin.Y2 = currY;
                        lin.FK_MapData = fk_mapdata;
                        lin.MyPK = "Lin" + keyID + i.ToString() + idx + "R";
                        lin.Insert(); /*��һ ���� */

                        attr.X = rightCtrlX + padingLeft;
                        attr.Y = currY - 3;
                        attr.UIWidth = 150;
                        attr.Update();
                        currY += 14;
                    }
                    isLeft = !isLeft;
                }
            }
            // table bottom line.
            lin = new FrmLine();
            lin.X1 = 0;
            lin.Y1 = currY;

            lin.X2 = tableW;
            lin.Y2 = currY;
            lin.FK_MapData = fk_mapdata;
            lin.BorderWidth = 3;
            lin.MyPK = "Lin" + keyID + "eR";
            lin.Insert();

            currY = currY - 28 - 18;
            // �����β. table left line
            lin = new FrmLine();
            lin.X1 = 0;
            lin.Y1 = 50;
            lin.X2 = 0;
            lin.Y2 = currY;
            lin.FK_MapData = fk_mapdata;
            lin.BorderWidth = 3;
            lin.MyPK = "Lin" + keyID + "eRr";
            lin.Insert();

            // table right line.
            lin = new FrmLine();
            lin.X1 = tableW;
            lin.Y1 = 50;
            lin.X2 = tableW;
            lin.Y2 = currY;
            lin.FK_MapData = fk_mapdata;
            lin.BorderWidth = 3;
            lin.MyPK = "Lin" + keyID + "eRr4";
            lin.Insert();
        }
        public static String ColorToStr(System.Drawing.Color color)
        {
            try
            {
                string color_s = System.Drawing.ColorTranslator.ToHtml(color);
                color_s = color_s.Substring(1, color_s.Length - 1);
                return "#" + Convert.ToString(Convert.ToInt32(color_s, 16) + 40000, 16);
            }
            catch
            {
                return "black";
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <param name="fd"></param>
        /// <returns></returns>
        public static string DealToFieldOrTableNames(string fd)
        {
            string keys = "~!@#$%^&*()+{}|:<>?`=[];,./�����������������������������������������������࣭���ۣݣ���������";
            char[] cc = keys.ToCharArray();
            foreach (char c in cc)
                fd = fd.Replace(c.ToString(), "");

            string s = fd.Substring(0, 1);
            try
            {
                int a = int.Parse(s);
                fd = "F" + fd;
            }
            catch
            {
            }
            return fd;
        }
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
        /// ���ձ�����С
        /// </summary>
        /// <param name="ObjH">Ŀ��߶�</param>
        /// <param name="factH">ʵ�ʸ߶�</param>
        /// <param name="factW">ʵ�ʿ��</param>
        /// <returns>Ŀ����</returns>
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
        /// ���ձ�����С
        /// </summary>
        /// <param name="ObjH">Ŀ��߶�</param>
        /// <param name="factH">ʵ�ʸ߶�</param>
        /// <param name="factW">ʵ�ʿ��</param>
        /// <returns>Ŀ����</returns>
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
                    strs += "<a title='����' href=\"javascript:DoAction('" + path + "/Comm/Do.aspx?ActionType=1&OID=" + file.OID + "&EnsName=" + enName + "&PK=" + pk + "','ɾ���ļ���" + file.MyFileName + file.MyFileExt + "��')\" ><img src='" + path + "/Images/Btn/delete.gif' border=0 alt='ɾ���˸���' /></a>&nbsp;";
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
        /// ������ת����ƴ��
        /// </summary>
        /// <param name="str">Ҫת���ĺ���</param>
        /// <returns>���ص�ƴ��</returns>
        public string Chs2Pinyin(string str)
        {
            return BP.DA.chs2py.convert(str); 
        }

		public static string GenerTablePage(DataTable dt , string title )
		{
		
			string str="<Table id='tb' class=Table >";

            str += "<caption>" + title + "</caption>";
           

			// ����
			str+="<TR>";
			foreach(DataColumn dc in dt.Columns)
			{
				str+= "<TD class='DGCellOfHeader"+BP.Web.WebUser.Style+"' nowrap >"+dc.ColumnName+"</TD>";
			}
			str+="</TR>";
			 
			//����
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
		/// �õ�һ�����xml ��������
		/// </summary>
		/// <param name="tbname">�����</param>
		/// <returns></returns>
		public static DataTable GetTableFromXmlByTBName(string xmlname, string tbname)
		{
			DataSet ds = new DataSet();
			ds.ReadXml("D:\\WebApp\\WF\\Data\\XML\\KHZB.xml");
			return  ds.Tables[tbname];
		}
		/// <summary>
		/// ������ʱ�ļ�����
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
		/// ���½�������
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
        /// ������е������
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
		/// ���ݴ���
		/// </summary>
		/// <param name="dbtype">����</param>
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
                            DBAccessOfOracle9i.RunSQL(SqlBuilder.GenerCreateTableSQLOfOra_OK(myen));
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
                    case DBType.Informix:
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
                        DBAccessOfMSSQL2000.RunSQL(SqlBuilder.GenerCreateTableSQLOfInfoMix(myen));
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
                            case DBType.Informix:
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
                        Log.DefaultLogWriteLineError( dbtype.ToString()+ "bak���ִ���" + ex.Message);
                    }
                }
                #endregion
            }
        }

		#region ϵͳ����
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
            // ȡ��ȫ����ʵ��
            ArrayList als = ClassFactory.GetObjects("BP.En.Entities");
            string msg = "";
            foreach (object obj in als)
            {
                Entities ens = (Entities)obj;
                try
                {
                    msg += DBRpt1(level, ens);
                }
                catch (Exception ex)
                {
                    msg += "<hr>" + ens.ToString() + "���ʧ��:" + ex.Message;
                }
            }

            MapDatas mds = new MapDatas();
            mds.RetrieveAllFromDBSource();
            foreach (MapData md in mds)
            {
                try
                {
                    md.HisGEEn.CheckPhysicsTable();
                    PubClass.AddComment(md.HisGEEn);
                }
                catch (Exception ex)
                {
                    msg += "<hr>" + md.No + "���ʧ��:" + ex.Message;
                }
            }

            MapDtls dtls = new MapDtls();
            dtls.RetrieveAllFromDBSource();
            foreach (MapDtl dtl in dtls)
            {
                try
                {
                    dtl.HisGEDtl.CheckPhysicsTable();
                    PubClass.AddComment(dtl.HisGEDtl);
                }
                catch (Exception ex)
                {
                    msg += "<hr>" + dtl.No + "���ʧ��:" + ex.Message;
                }
            }

            #region ��鴦���Ҫ�Ļ������� Pub_Day .
            string sql = "";
            string sqls = "";
            sql = "SELECT count(*) Num FROM Pub_Day";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        string d = i.ToString().PadLeft(2, '0');
                        sqls += "@INSERT INTO Pub_Day(No,Name)VALUES('" + d.ToString() + "','" + d.ToString() + "')";
                    }
                }
            }
            catch
            {
            }

            sql = "SELECT count(*) Num FROM Pub_YF";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        string d = i.ToString().PadLeft(2, '0');
                        sqls += "@INSERT INTO Pub_YF(No,Name)VALUES('" + d.ToString() + "','" + d.ToString() + "')";
                    }
                }
            }
            catch
            {
            }

            sql = "SELECT count(*) Num FROM Pub_ND";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 2010; i < 2015; i++)
                    {
                        string d = i.ToString();
                        sqls += "@INSERT INTO Pub_ND(No,Name)VALUES('" + d.ToString() + "','" + d.ToString() + "')";
                    }
                }
            }
            catch
            {

            }
            sql = "SELECT count(*) Num FROM Pub_NY";
            try
            {
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    for (int i = 2010; i < 2015; i++)
                    {

                        for (int yf = 1; yf <= 12; yf++)
                        {
                            string d = i.ToString() + "-" + yf.ToString().PadLeft(2,'0');
                            sqls += "@INSERT INTO Pub_NY(No,Name)VALUES('" + d + "','" + d + "')";
                        }
                    }
                }
            }
            catch
            {
            }

            DBAccess.RunSQLs(sqls);
            #endregion ��鴦���Ҫ�Ļ������ݡ�
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
        /// Ϊ������ע��
        /// </summary>
        /// <returns></returns>
        public static string AddComment()
        {
            // ȡ��ȫ����ʵ��
            ArrayList als = ClassFactory.GetObjects("BP.En.Entities");
            string msg = "";
            Entity en = null;
            Entities ens = null;
            foreach (object obj in als)
            {
                try
                {
                    ens = (Entities)obj;
                    en = ens.GetNewEntity;
                    if (en.EnMap.EnType == EnType.View || en.EnMap.EnType == EnType.ThirdPartApp)
                        continue;
                }
                catch
                {
                    continue;
                }
               msg+= AddComment(en);
            }
            return msg;
        }
        public static string AddComment(Entity en)
        {
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
                return "";
            }
            catch (Exception ex)
            {
                return "<hr>" + en.ToString() + "���ʧ��:" + ex.Message;
            }
        }
        public static void AddCommentForTable_Ora(Entity en)
        {
            en.RunSQL("comment on table " + en.EnMap.PhysicsTable + " IS '" + en.EnDesc + "'");
            SysEnums ses = new SysEnums();
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                switch (attr.MyFieldType)
                {
                    case FieldType.PK:
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + " - ����'");
                        break;
                    case FieldType.Normal:
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + "'");
                        break;
                    case FieldType.Enum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + ",ö������:" + ses.ToDesc() + "'");
                        break;
                    case FieldType.PKEnum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + ", ����:ö������:" + ses.ToDesc() + "'");
                        break;
                    case FieldType.FK:
                        Entity myen = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS " + attr.Desc + ", ���:��Ӧ�����:" + myen.EnMap.PhysicsTable + ",������:" + myen.EnDesc);
                        break;
                    case FieldType.PKFK:
                        Entity myen1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        en.RunSQL("comment on column  " + en.EnMap.PhysicsTable + "." + attr.Field + " IS '" + attr.Desc + ", �����:��Ӧ�����:" + myen1.EnMap.PhysicsTable + ",������:" + myen1.EnDesc + "'");
                        break;
                    default:
                        break;
                }
            }
        }
        private static void AddColNote(Entity en, string table, string col, string note)
        {
            try
            {
                string sql = "execute  sp_dropextendedproperty 'MS_Description','user',dbo,'table','" + table + "','column'," + col;
                en.RunSQL(sql);
            }
            catch(Exception ex)
            {
            }

            try
            {
                string sql = "execute  sp_addextendedproperty 'MS_Description', '" + note + "', 'user', dbo, 'table', '" + table + "', 'column', '" + col + "'";
                en.RunSQL(sql);
            }
            catch (Exception ex)
            {
            }
        }
		/// <summary>
		/// Ϊ�����ӽ���
		/// </summary>
		/// <param name="en"></param>
        public static void AddCommentForTable_MS(Entity en)
        {
            if (en.EnMap.EnType == EnType.View || en.EnMap.EnType == EnType.ThirdPartApp )
            {
                return;
            }

            try
            {
                string sql = "execute  sp_dropextendedproperty 'MS_Description','user',dbo,'table','" + en.EnMap.PhysicsTable + "'";
                en.RunSQL(sql);
            }
            catch (Exception ex)
            {
            }

            try
            {
                string sql = "execute  sp_addextendedproperty 'MS_Description', '" + en.EnDesc + "', 'user', dbo, 'table', '" + en.EnMap.PhysicsTable + "'";
                en.RunSQL(sql);
            }
            catch (Exception ex)
            {

            }


            SysEnums ses = new SysEnums();
            foreach (Attr attr in en.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                if (attr.Key == attr.Desc)
                    continue;

                switch (attr.MyFieldType)
                {
                    case FieldType.Normal:
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc);
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+"'");
                        break;
                    case FieldType.Enum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        //	en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"++"'" );
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ",ö������:" + ses.ToDesc());
                        break;
                    case FieldType.PKEnum:
                        ses = new SysEnums(attr.Key, attr.UITag);
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ",����:ö������:" + ses.ToDesc());
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+en.EnDesc+", ����:ö������:"+ses.ToDesc()+"'" );
                        break;
                    case FieldType.FK:
                        Entity myen = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ", ���:��Ӧ�����:" + myen.EnMap.PhysicsTable + ",������:" + myen.EnDesc);
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS "+  );
                        break;
                    case FieldType.PKFK:
                        Entity myen1 = attr.HisFKEn; // ClassFactory.GetEns(attr.UIBindKey).GetNewEntity;
                        AddColNote(en, en.EnMap.PhysicsTable, attr.Field, attr.Desc + ", �����:��Ӧ�����:" + myen1.EnMap.PhysicsTable + ",������:" + myen1.EnDesc);
                        //en.RunSQL("comment on table "+ en.EnMap.PhysicsTable+"."+attr.Field +" IS '"+  );
                        break;
                    default:
                        break;
                }
            }
        }
		/// <summary>
		/// ����ϵͳ��������������⣬��д����־���档
		/// </summary>
		/// <returns></returns>
        public static string DBRpt1(DBLevel level, Entities ens)
        {

            Entity en = ens.GetNewEntity;
            if (en.EnMap.EnDBUrl.DBUrlType != DBUrlType.AppCenterDSN)
                return null;

            if (en.EnMap.EnType == EnType.ThirdPartApp)
                return null;

            if (en.EnMap.EnType == EnType.View)
                return null;

            if (en.EnMap.EnType == EnType.Ext)
                return null;

            // ����������ֶΡ�
            en.CheckPhysicsTable();
            
            PubClass.AddComment(en);

            string msg = "";
            //if (level == DBLevel.High)
            //{
            //    try
            //    {
            //        DBAccess.RunSQL("update pub_emp set AuthorizedAgent='1' WHERE AuthorizedAgent='0' ");
            //    }
            //    catch
            //    {
            //    }
            //}
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
                    #region �������ǣ�ȥ�����ҿո���Ϊ������ܰ������ҿո�
                    if (level == DBLevel.Middle || level == DBLevel.High)
                    {
                        /*����Ǹ��м���,��ȥ�����ҿո�*/
                        if (attr.MyDataType == DataType.AppString)
                        {
                            DBAccess.RunSQL("UPDATE " + en.EnMap.PhysicsTable + " SET " + attr.Field + " = rtrim( ltrim(" + attr.Field + ") )");
                        }
                    }
                    #endregion

                    #region �������������.
                    Entities refEns = attr.HisFKEns; // ClassFactory.GetEns(attr.UIBindKey);
                    Entity refEn = refEns.GetNewEntity;

                    //ȡ�������ı�
                    string reftable = refEn.EnMap.PhysicsTable;
                    //sql="SELECT COUNT(*) FROM "+en.EnMap.PhysicsTable+" WHERE "+attr.Key+" is null or len("+attr.Key+") < 1 ";
                    // �ж�������Ƿ���ڡ�

                    sql = "SELECT COUNT(*) FROM  sysobjects  WHERE  name = '" + reftable + "'";
                    //num=DA.DBAccess.RunSQLReturnValInt(sql,0);
                    if (DBAccess.IsExitsObject(reftable) == false)
                    {
                        //���������Ϣ
                        enMsg += "<br>@���ʵ�壺" + en.EnDesc + ",�ֶ� " + attr.Key + " , �ֶ�����:" + attr.Desc + " , ��������:" + reftable + "������:" + sql;
                    }
                    else
                    {
                        Attr attrRefKey = refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue); // ȥ������������ �ո�
                        if (attrRefKey.MyDataType == DataType.AppString)
                        {
                            if (level == DBLevel.Middle || level == DBLevel.High)
                            {
                                /*����Ǹ��м���,��ȥ�����ҿո�*/
                                DBAccess.RunSQL("UPDATE " + reftable + " SET " + attrRefKey.Field + " = rtrim( ltrim(" + attrRefKey.Field + ") )");
                            }
                        }

                        Attr attrRefText = refEn.EnMap.GetAttrByKey(attr.UIRefKeyText);  // ȥ������ Text ������ �ո�

                        if (level == DBLevel.Middle || level == DBLevel.High)
                        {
                            /*����Ǹ��м���,��ȥ�����ҿո�*/
                            DBAccess.RunSQL("UPDATE " + reftable + " SET " + attrRefText.Field + " = rtrim( ltrim(" + attrRefText.Field + ") )");
                        }

                    }
                    #endregion

                    #region �����ʵ���Ƿ�Ϊ��
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
                        enMsg += "<br>@���ʵ�壺" + en.EnDesc + ",�����:" + en.EnMap.PhysicsTable + "����" + attr.Key + "," + attr.Desc + "����ȷ,����[" + num + "]�м�¼û�����ݡ�" + sql;
                    }
                    #endregion

                    #region �Ƿ��ܹ���Ӧ�����
                    //�Ƿ��ܹ���Ӧ�������
                    sql = "SELECT COUNT(*) FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " NOT IN ( SELECT " + refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue).Field + " FROM " + reftable + "	 ) ";
                    num = DA.DBAccess.RunSQLReturnValInt(sql, 0);
                    if (num == 0)
                    {
                    }
                    else
                    {
                        /*����Ǹ��м���.*/
                        string delsql = "DELETE FROM " + en.EnMap.PhysicsTable + " WHERE " + attr.Field + " NOT IN ( SELECT " + refEn.EnMap.GetAttrByKey(attr.UIRefKeyValue).Field + " FROM " + reftable + "	 ) ";
                        //int i =DBAccess.RunSQL(delsql);
                        enMsg += "<br>@" + en.EnDesc + ",�����:" + en.EnMap.PhysicsTable + "����" + attr.Key + "," + attr.Desc + "����ȷ,����[" + num + "]�м�¼û�й��������ݣ�����������������" + sql + "�������ɾ����Щ��Ӧ���ϵ���������������SQL: " + delsql + " ������ִ��.";
                    }
                    #endregion

                    #region �ж� ����
                    //DBAccess.IsExits("");
                    #endregion
                }
                catch (Exception ex)
                {
                    enMsg += "<br>@" + ex.Message;
                }

                if (enMsg != "")
                {
                    msg += "<BR><b>-- ���[" + en.EnDesc + "," + en.EnMap.PhysicsTable + "]������������,������:" + en.ToString() + "</b>";
                    msg += enMsg;
                }
            }
            return msg;
        }
		#endregion

		#region ת����ʽ  chen
		/// <summary>
		/// ��ĳ�ؼ��е�����ת��ΪExcel�ļ�
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
		/// ��ĳ�ؼ��е�����ת��ΪWord�ļ�
		/// </summary>
		/// <param name="ctl"></param>
		public static void ToWord(System.Web.UI.Control ctl ,string filename)  
		{
            filename = HttpUtility.UrlEncode(filename);
			HttpContext.Current.Response.Charset ="GB2312";	
			HttpContext.Current.Response.AppendHeader("Content-Disposition","attachment;filename="+ filename +".doc");
			HttpContext.Current.Response.ContentEncoding =System.Text.Encoding.GetEncoding("GB2312"); 
			HttpContext.Current.Response.ContentType ="application/ms-msword";//image/JPEG;text/HTML;image/GIF;application/ms-excel
			ctl.Page.EnableViewState =false;			
			System.IO.StringWriter  tw = new System.IO.StringWriter() ;
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter (tw);
			ctl.RenderControl(hw);
			HttpContext.Current.Response.Write(tw.ToString());
		}

        public static void OpenExcel(string filepath, string tempName)
        {
            tempName = HttpUtility.UrlEncode(tempName);
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + tempName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/ms-excel"; 
            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }
        public static void DownloadFile(string filepath, string tempName)
        {
            tempName = HttpUtility.UrlEncode(tempName);
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + tempName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            //HttpContext.Current.Response.ContentType = "application/ms-msword";  //image/JPEG;text/HTML;image/GIF;application/ms-excel
            //HttpContext.Current.EnableViewState =false;

            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }
        public static void DownloadFileV2(string filepath, string tempName)
        {
             
            FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                byte[] buffer = new byte[102400];
                HttpContext.Current.Response.Clear();
                using (FileStream iStream = File.OpenRead(fileInfo.FullName))
                {
                    long dataLengthToRead = iStream.Length; //��ȡ���ص��ļ��ܴ�С

                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" +
                                       HttpUtility.UrlEncode(tempName, System.Text.Encoding.UTF8));
                    while (dataLengthToRead > 0 && HttpContext.Current.Response.IsClientConnected)
                    {
                        int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(102400));//'��ȡ�Ĵ�С

                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                        HttpContext.Current.Response.Flush();
                        dataLengthToRead = dataLengthToRead - lengthRead;
                    }
                    HttpContext.Current.Response.Close();
                    HttpContext.Current.Response.End();
                }
            }
        }
        public static void OpenWordDoc(string filepath, string tempName)
        {
            tempName = HttpUtility.UrlEncode(tempName);

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
            tempName = HttpUtility.UrlEncode(tempName);

            FileInfo fileInfo = new FileInfo(filepath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(tempName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AppendHeader("Content-Length", fileInfo.Length.ToString());
            HttpContext.Current.Response.WriteFile(fileInfo.FullName);
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
            title = title.Replace("\"", "��");
            if (top == 0 && left == 0)
                System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",scrollbars=yes,resizable=yes,toolbar=false,location=false') ; </script> ");
            else
                System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'> var newWindow =window.open('" + url + "','" + winName + "','width=" + width + ",top=" + top + ",left=" + left + ",height=" + height + ",scrollbars=yes,resizable=yes,toolbar=false,location=false');</script>");
        }
		/// <summary>
		/// �����ҳ���Ϻ�ɫ�ľ��档
		/// </summary>
		/// <param name="msg">��Ϣ</param>
        protected void ResponseWriteRedMsg(string msg)
        {
            //this.Response.Write("<BR><font color='red' size='"+MsgFontSize.ToString()+"' > <b>"+msg+"</b></font>");
            //if (msg.Length < 200)
            //	return ;
            msg = msg.Replace("@", "<BR>@");
            System.Web.HttpContext.Current.Session["info"] = msg;
            string url = System.Web.HttpContext.Current.Request.ApplicationPath + "/Comm/Port/ErrorPage.aspx";
            WinOpen(url, "����", msg + DateTime.Now.ToString("mmss"), 500, 400, 150, 270);
        }
		/// <summary>
		/// �����ҳ������ɫ����Ϣ��
		/// </summary>
		/// <param name="msg">��Ϣ</param>
		public static void ResponseWriteBlueMsg(string msg)
		{ 
			 
			if (SystemConfig.IsBSsystem)
			{
				msg=msg.Replace("@","<BR>@");
				System.Web.HttpContext.Current.Session["info"]=msg;
				string url=System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Port/InfoPage.aspx";
				WinOpen(url,"��Ϣ","sysmsg",500,400,150,270);
			}
			else
			{
				Log.DebugWriteInfo(msg);
			}
		}
		/// <summary>
		/// ����ɹ�
		/// </summary>
		public static void ResponseWriteBlueMsg_SaveOK()
		{
			//this.Alert("����ɹ�!");
			 
			ResponseWriteBlueMsg("����ɹ�!");
		}
		/// <summary>
		/// ResponseWriteBlueMsg_DeleteOK
		/// </summary>
		public static void ResponseWriteBlueMsg_DeleteOK()
		{			
			//this.Alert("ɾ���ɹ�!");
			ResponseWriteBlueMsg("ɾ���ɹ�!");
		}
		/// <summary>
		/// ResponseWriteBlueMsg_UpdataOK
		/// </summary>
		public static void ResponseWriteBlueMsg_UpdataOK()
		{			
			// this.Alert("���³ɹ�!");
			ResponseWriteBlueMsg("���³ɹ�!");
		}
		/// <summary>
		/// �����ҳ���Ϻ�ɫ����Ϣ��
		/// </summary>
		/// <param name="msg">��Ϣ</param>
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
		/// �л�����ϢҲ�档
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
		/// �л�����ϢҲ�档
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
		///ת��һ��ҳ���ϡ� '_top'
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
        //    this.ToE("SaveOK", "����ɹ�");
        //}

        
		/// <summary>
		/// ����page ������show message
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
