//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\comm\uc\Stub_ucsys_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���comm\uc\ucsys.ascx.cs���������ࡰMigrated_UCSys���Ļ��ࡣ
// ��������������Ŀ�е����д����ļ����øû��ࡣ
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================

namespace BP.Web.Comm.UC
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Web.UI;
    using BP.En;
    using BP.Sys;
    using BP.DA;
    using BP.Web;
    using BP.Web.Controls;
    using BP.Web.UC;
    using BP.XML;
    using BP.Sys.Xml;

    abstract public class Migrated_En : BP.Web.UC.UCBase
    {
        abstract public void BindXmlEns(XmlEns ens);
        abstract public void GenerOutlookMenuV2(string cate);
        abstract public void ClearViewState();
        abstract public void GenerOutlookMenuV2();
        abstract public void ShowTableGroupEns(DataTable dt, Map map, int top, string url, bool isShowNoCol);
        abstract public void ShowTable(DataTable dt, Map map);
        abstract public void ShowTable(DataTable dt);
        abstract public void GenerOutlookMenu(string xmlFile);
        abstract public void GenerOutlookMenu();
        abstract public void GenerOutlookMenu_Img(string xmlFile);
        abstract public void BindSystems();
        abstract public void BindWel();
        abstract public void BindMsgInfo(string msg);
        abstract public void BindMsgWarning(string msg);
        abstract public void GenerMenuMain();
        abstract public void DataPanel(Entities ens, string ctrlId, string key, ShowWay sh);
        abstract public void DataPanelDtl(Entities ens, string ctrlId, string colName, string urlAttrKey, string colUrl);
        abstract public void DataPanelDtl(Entities ens, string ctrlId);
        abstract public void DataPanelDtl(Entities ens, string ctrlId, string groupkey);
        abstract public void DataPanelDtl(Entities ens, string ctrlId, string groupkey, string groupkey2);
        abstract public void UIEn1ToMGroupKey(Entities ens, string showVal, string showText, Entities selectedEns, string selecteVal, string groupKey);
        abstract public void UIEn1ToMGroupKey_Line(Entities ens, string showVal, string showText, Entities selectedEns, string selecteVal, string groupKey);
        abstract public void UIEn1ToM(Entities ens, string showVal, string showText, Entities selectedEns, string selecteVal);
        abstract public void UIEn1ToM_OneLine(Entities ens, string showVal, string showText, Entities selectedEns, string selecteVal);
        abstract public void FilesView(string enName, string pk);
        public static string FilesViewStr(string enName, object pk)
        {
            string url = System.Web.HttpContext.Current.Request.ApplicationPath + "/Comm/FileManager.aspx?EnsName=" + enName + "&PK=" + pk.ToString();

            //string strs="<a href=\"javascript:WinOpen("") \" >����</>";
            //string strs="<a href=\"javascript:WinOpen('"+url+"') \" >�༭����</>";
            string strs = "";
            SysFileManagers ens = new SysFileManagers(enName, pk.ToString());
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;

            foreach (SysFileManager file in ens)
            {
                strs += "<img src='" + path + "/Images/FileType/" + file.MyFileExt.Replace(".", "") + ".gif' border=0 /><a href='" + path + file.MyFilePath + "' target='_blank' >" + file.MyFileName + file.MyFileExt + "</a>&nbsp;";
                if (file.Rec == WebUser.No)
                {
                    strs += "<a title='����' href=\"javascript:DoAction('" + path + "/Comm/Do.aspx?ActionType=" + (int)ActionType.DeleteFile + "&OID=" + file.OID + "&EnsName=" + enName + "&PK=" + pk + "','ɾ���ļ���" + file.MyFileName + file.MyFileExt + "��')\" ><img src='" + path + "/Images/Btn/delete.gif' border=0 alt='ɾ���˸���' /></a>&nbsp;";
                }
            }
            return strs;
        }
        public static string FilesViewStr1(string enName, object pk)
        {
            string url = System.Web.HttpContext.Current.Request.ApplicationPath + "/Comm/FileManager.aspx?EnsName=" + enName + "&PK=" + pk.ToString();

            //string strs="<a href=\"javascript:WinOpen("") \" >����</>";
            string strs = "<a href=\"javascript:WinOpen('" + url + "') \" >�༭����</>";
            SysFileManagers ens = new SysFileManagers(enName, pk.ToString());
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;

            foreach (SysFileManager file in ens)
            {
                strs += "<img src='" + path + "/Images/FileType/" + file.MyFileExt.Replace(".", "") + ".gif' border=0 /><a href='" + path + file.MyFilePath + "' target='_blank' >" + file.MyFileName + file.MyFileExt + "</a>&nbsp;";
            }
            return strs;
        }
    }
}
