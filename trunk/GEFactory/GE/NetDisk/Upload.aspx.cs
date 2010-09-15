using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.DA;
using BP.GE;
using BP.En;
using BP.Web;
using BP.Edu;

public partial class GE_NetDisk_Upload : WebPage
{
    public string GetDKDir()
    {
        return  Request.QueryString["DirOID"];
    }
    /// <summary>
    /// GetEmpDept
    /// </summary>
    /// <returns></returns>
    public string GetEmpDept()
    {

        if (BP.Web.WebUser.FK_Dept == null)
        {
            this.Alert("用户部门信息不存在，请重新登录系统！");
            return null;
        }
        else
        {
            return BP.Web.WebUser.FK_Dept;
        }
    }
    public string GetEmpNo()
    {
        if (BP.Web.WebUser.No == null)
        {
            this.Alert("用户信息不存在，请重新登录系统！");
            return null;
        }
        else
        {
            return BP.Web.WebUser.No;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DKFiles dkfiles = new DKFiles();
        QueryObject qo = new QueryObject(dkfiles);
        qo.AddWhere(DKFileAttr.FK_Emp, BP.Web.WebUser.No);
        qo.DoQuery();

        int totalRL = 0;
        foreach (DKFile dkfile in dkfiles)
        {
            totalRL += dkfile.MyFileSize;
        }
        int RL = 52428800 - totalRL;
        lbRLiang.Text = RL.ToString ();

        string userDept = "";
        if (BP.Web.WebUser.FK_Dept == null)
        {
            EduEmp emp = new EduEmp(BP.Web.WebUser.No);
            userDept = emp.FK_Dept;
        }
        else
        {
            userDept = BP.Web.WebUser.FK_Dept;
        }

        // 为webservice提供文件上传信息
        DKFileAdds.FK_Dept = userDept;
        DKFileAdds.FK_Emp = BP.Web.WebUser.No;
        DKFileAdds.FK_DKDir = GetDKDir();
        DKFileAdds.FileAddress = "\\" + DKFileAdds.FK_Dept + "\\" + DKFileAdds.FK_Emp;
    }
}
