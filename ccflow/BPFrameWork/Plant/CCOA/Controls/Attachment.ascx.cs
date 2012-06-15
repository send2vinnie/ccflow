using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using BP.DA;
using System.Data;

public partial class CCOA_Controls_Attachment : System.Web.UI.UserControl
{
    public string EnumType { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];

        string arrList = string.Empty;
        AttachList = new List<OA_Attachment>();
        this.FillAttachList(EnumType);
        this.Visible = this.AttachList.Count > 0;
        divAttachment.Visible = this.Visible;
    }

    private void FillAttachList(string type)
    {
        type = type.ToUpper();
        string id = Request.QueryString["id"];
        AttachList = new List<OA_Attachment>();
        switch (type)
        {
            case "news":
                this.FillNewsAttachList(id);
                break;
            case "notice":
                this.FillNoticeAttachList(id);
                break;
            case "email":
                this.FillEmailAttachList(id);
                break;
        }
    }

    private void FillEmailAttachList(string id)
    {
        string arrList = string.Empty;
        BP.CCOA.OA_EmailAttachs list = new BP.CCOA.OA_EmailAttachs();
        list.RetrieveByAttr(OA_EmailAttachAttr.Email_Id, id);
        if (list.Count > 0)
        {
            this.Visible = true;
        }
        else
        {
            return;
        }
        foreach (OA_EmailAttach item in list)
        {
            arrList += "'" + item.Attachment_Id + "'" + ",";
        }
        arrList = arrList.Substring(0, arrList.Length - 1);
        this.FillAttacheListByIds(arrList);
    }

    /// <summary>
    /// 填充新闻附件
    /// </summary>
    /// <param name="id"></param>
    private void FillNewsAttachList(string id)
    {
        string arrList = string.Empty;
        BP.CCOA.OA_NewsAttachs list = new BP.CCOA.OA_NewsAttachs();
        list.RetrieveByAttr("News_Id", id);
        if (list.Count > 0)
        {
            this.Visible = true;
        }
        else
        {
            return;
        }
        foreach (OA_NewsAttach item in list)
        {
            arrList += "'" + item.Attachment_Id + "'" + ",";
        }
        arrList = arrList.Substring(0, arrList.Length - 1);
        this.FillAttacheListByIds(arrList);
    }

    /// <summary>
    /// 填充通知公告附件
    /// </summary>
    /// <param name="id"></param>
    private void FillNoticeAttachList(string id)
    {
        string arrList = string.Empty;
        BP.CCOA.OA_NoticeAttachs list = new BP.CCOA.OA_NoticeAttachs();
        list.RetrieveByAttr(BP.CCOA.OA_NoticeAttachAttr.Notice_Id, id);
        if (list.Count > 0)
        {
            this.Visible = true;
        }
        else
        {
            return;
        }
        foreach (OA_NoticeAttach item in list)
        {
            arrList += "'" + item.Accachment_Id + "'" + ",";
        }
        arrList = arrList.Substring(0, arrList.Length - 1);
        this.FillAttacheListByIds(arrList);
    }

    private void FillAttacheListByIds(string attchmentIds)
    {
        string sql = string.Format(@"select * from oa_attachment where no in({0})", attchmentIds);
        DataTable dt = DBAccess.RunSQLReturnTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            OA_Attachment obj = new OA_Attachment();
            obj.FileNeme = dr["FileNeme"].ToString();
            obj.FilePath = dr["FilePath"].ToString();
            AttachList.Add(obj);
        }
    }

    public List<OA_Attachment> AttachList
    {
        get;
        set;
    }

    public bool Visible { get; set; }
}