using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EIP_SaveLayout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string json = Request.QueryString["params"];

        string[] arr = json.Split(',');

        IDictionary<string, string> dic = new Dictionary<string, string>();
        for (int i = 0; i < arr.Length; i++)
        {
            dic.Add(arr[i].Split(':')[0], arr[i].Split(':')[1]);
        }

        BP.CCOA.EIP_LayoutDetail dal = new BP.CCOA.EIP_LayoutDetail();

        bool issucc = dal.Update(dic);

        if (issucc)
        {
            Response.Write("保存成功！");
        }
        else
        {
            Response.Write("保存失败！");
        }
    }
}