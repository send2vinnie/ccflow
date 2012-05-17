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

        if (EnumType.ToLower() == "news")
        {
            BP.CCOA.OA_NewsAttachs list = new BP.CCOA.OA_NewsAttachs();
            list.RetrieveByAttr("News_Id", id);

            if (list.Count > 0)
            {
                this.Visible = true;

                foreach (OA_NewsAttach item in list)
                {
                    arrList += "'" + item.Attachment_Id + "'" + ",";
                }
                arrList = arrList.Substring(0, arrList.Length - 1);

                string sql = string.Format(@"select * from oa_attachment where no in({0})", arrList);

                DataTable dt = DBAccess.RunSQLReturnTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    OA_Attachment obj = new OA_Attachment();
                    obj.FileNeme = dr["FileNeme"].ToString();
                    obj.FilePath = dr["FilePath"].ToString();
                    AttachList.Add(obj);
                }
            }
            else
            {
                this.Visible = false;
            }
        }

        divAttachment.Visible = this.Visible;
    }

    public List<OA_Attachment> AttachList
    {
        get;
        set;
    }

    public bool Visible { get; set; }
}