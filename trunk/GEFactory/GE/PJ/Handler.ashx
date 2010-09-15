<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using BP.GE;
using BP.En;

public class Handler : IHttpHandler,System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (System.Web.HttpContext.Current.Session["UserName"] == null)
        {
            context.Response.Write("对不起请登录！");
        }
        else if (context.Request.Form["ID"] != null && context.Request.Form["Num"] != null)
        {
            string NewsGroup = context.Request.Form["NewsGroup"].ToString();
            string RefOID = context.Request.Form["RefOID"].ToString();
            int PJGroup = Convert.ToInt32(context.Request.Form["PJGroup"]);
            string MyOID = context.Request.Form["ID"].ToString();
            string Emp = System.Web.HttpContext.Current.Session["No"].ToString();
            string EmpT = System.Web.HttpContext.Current.Session["UserName"].ToString();
            string Num = context.Request.Form["Num"].ToString();
            string strIP = context.Request.Form["IP"].ToString();
            PJEmpInfo pjEmp = new PJEmpInfo();
            pjEmp.FK_Emp = Emp;
            pjEmp.FK_EmpT = EmpT;
            PJSubject pjsubject = new PJSubject();
            pjsubject.NewsGroup = NewsGroup;
            pjsubject.RefOID = RefOID;
            pjsubject.PJGroup = PJGroup;
            pjsubject.ID = MyOID;
            context.Response.Write(save(pjEmp, pjsubject, Num, strIP));
        }
        else
        {
            context.Response.Write("所需数据不全!");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    /// <summary>
    /// 保存
    /// </summary>
    public string save(PJEmpInfo pjEmp,PJSubject pjsubject , string Num,string IP)
    {

        if (pjEmp.FK_Emp == string.Empty)
        {
            return "对不起请先登录!";
        }
        else
        {
            return saveAll(pjEmp, pjsubject, Num, IP);
        }
    }
    /// <summary>
    /// 保存评价信息
    /// </summary>
    /// <param name="key"></param>
    /// <param name="pjEmp"></param>
    private string saveAll(PJEmpInfo pjEmp, PJSubject pjsubject, string key, string IP)
    {
        //查询该用户是否已经参加过此投票
        string strSql = "Select Count(*) from GE_PJEmpInfo where FK_Emp='" + pjEmp.FK_Emp + "' and FK_Subject='" + pjsubject.ID + "'";
        int j = BP.DA.DBAccess.RunSQLReturnValInt(strSql);
        if (j <= 0)
        {
            //是否已经添加过该组投票
            strSql = "select count(*) from GE_PJSubject where ID='" + pjsubject.ID + "'";
            int i = BP.DA.DBAccess.RunSQLReturnValInt(strSql);
            if (i <= 0)
            {
                //保存评价主体
                pjsubject.Save();
                //初始化总票数
                strSql = "insert into GE_PJTotal select '" + pjsubject.ID + "', PJNum,0 from GE_PJType where PJGroup=" + pjsubject.PJGroup;
                i = BP.DA.DBAccess.RunSQL(strSql);
            }
            strSql = "update GE_PJTotal set Total=Total+1" + " where Fk_Subject='" + pjsubject.ID + "' and FK_Num='" + key + "'";
            int result = BP.DA.DBAccess.RunSQL(strSql);
            //保存评价人信息
            pjEmp.FK_Subject = pjsubject.ID;
            pjEmp.IP = IP;
            pjEmp.RDT = DateTime.Now;
            pjEmp.Save();

            return "评价成功!";
        }
        else
        {
            //已经评价过
            return "只能评价一次!";
        }
    }

}