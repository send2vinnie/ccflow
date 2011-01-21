using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using BP.GE;

public partial class GE_PJ_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int sum = 0;
        string strOID = Convert.ToString(Request.QueryString["RefOID"]);
        StringBuilder sbSql = new StringBuilder();
        sbSql.Append(" select * from GE_PJSubject,GE_PJTotal,GE_PJType ");
        sbSql.Append(" where GE_PJSubject.PJGroup=GE_PJType.PJGroup ");
        sbSql.Append(" and GE_PJSubject.ID=GE_PJTotal.FK_Subject ");
        sbSql.Append(" and GE_PJTotal.FK_Num=GE_PJType.PJNum ");
        sbSql.Append(" and GE_PJSubject.ID='" + strOID + "' ");

        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sbSql.ToString());
        if (dt.Rows.Count > 0)
        {
            sum = Convert.ToInt32(dt.Compute("sum(Total)", "1=1"));
        }

        this.Pub1.AddTable("width='100%'");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("width='10%'","投票项");
        this.Pub1.AddTDTitle("width='10%'","比例");
        this.Pub1.AddTDTitle("width='80%'","图示");
        this.Pub1.AddTREnd();
        foreach (DataRow dr in dt.Rows)
        {
            int width = Convert.ToInt32(dr["Total"]) * 100 / sum;
            this.Pub1.AddTR();
            this.Pub1.AddTD(dr["Title"].ToString());
            this.Pub1.AddTD((Convert.ToInt32(dr["Total"]) * 100 / sum).ToString() + "%");
            this.Pub1.AddTD("<div style='background-color:blue; width:" + width + "% '></div>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }

    private void myInit()
    {
        PJEmpInfos pjemps = new PJEmpInfos();
        pjemps.RetrieveAll();
        PJSubjects pjsubjects = new PJSubjects();
        pjsubjects.RetrieveAll();
        PJTotals pjtotals = new PJTotals();
        pjtotals.RetrieveAll();
        PJTypes pjtypes = new PJTypes();
        pjtypes.RetrieveAll();

        PJType pjtype = new PJType();
        pjtype.PJGroup = "1";
        pjtype.PJNum = "1";
        pjtype.Title = "一星";
        pjtype.Pic = "GE/PJ/PJImg/star11.gif";
        pjtype.Score = 2;
        pjtype.Note = string.Empty;
        pjtype.Insert();

        pjtype = new PJType();
        pjtype.PJGroup = "1";
        pjtype.PJNum = "2";
        pjtype.Title = "二星";
        pjtype.Pic = "GE/PJ/PJImg/star12.gif";
        pjtype.Score = 4;
        pjtype.Note = string.Empty;
        pjtype.Insert();

        pjtype = new PJType();
        pjtype.PJGroup = "1";
        pjtype.PJNum = "3";
        pjtype.Title = "三星";
        pjtype.Pic = "GE/PJ/PJImg/star13.gif";
        pjtype.Score = 6;
        pjtype.Note = string.Empty;
        pjtype.Insert();

        pjtype = new PJType();
        pjtype.PJGroup = "1";
        pjtype.PJNum = "4";
        pjtype.Title = "四星";
        pjtype.Pic = "GE/PJ/PJImg/star14.gif";
        pjtype.Score = 8;
        pjtype.Note = string.Empty;
        pjtype.Insert();

        pjtype = new PJType();
        pjtype.PJGroup = "1";
        pjtype.PJNum = "5";
        pjtype.Title = "五星";
        pjtype.Pic = "GE/PJ/PJImg/star15.gif";
        pjtype.Score = 10;
        pjtype.Note = string.Empty;
        pjtype.Insert();
    }
}
