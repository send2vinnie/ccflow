using System.Web.UI;
using System.ComponentModel;
using System.Data;
using BP.En;
using System;
using System.Web.UI.WebControls;
using System.IO;
using BP.GE;
using System.Text;

namespace BP.GE.Ctrl
{
    [ToolboxData("<{0}:GEMsg runat='server'></{0}:GEMsg>")]
    public class GEMsg : Control
    {
        [Bindable(true), Description("消息页面的位置")]
        public string Url
        {
            get;
            set;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {
                writer.Write("(?/?)");
            }
            else
            {
                if (Context.Session["No"] != null)
                {
                    string strNo = Context.Session["No"].ToString();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select(Select count(*) from Ge_message where Receiver='" + strNo + "' and StaR=0) as Total,");
                    sbSql.Append("(select count(*) from Ge_message where Receiver='" + strNo + "' AND ReadSta=0) as UnRead");
                    DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sbSql.ToString());

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, Url);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write("(" + dt.Rows[0]["UnRead"] + "/" + dt.Rows[0]["Total"] + ")");
                    writer.RenderEndTag();

                }
            }
        }
    }
}