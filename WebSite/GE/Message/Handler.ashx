<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.Services;
using BP.DA;
using System.Data;
using System.Text;
using BP.GE.Ctrl;
using BP.En;
using BP.GE;

public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //根据用户名查找用户的详细信息
        if (context.Request.Form["EmpName"] != null)
        {
            string strName = System.Web.HttpUtility.UrlDecode(context.Request.Form["EmpName"].ToString());
            if (!string.IsNullOrEmpty(strName))
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" select Port_Emp.* ,Edu_school.[Name] as Dept from Port_Emp,Edu_school ");
                sbSql.Append(" where Port_Emp.FK_Dept=Edu_school.[No] and Port_Emp.[Name]='" + strName + "'");
                sbSql.Append(" union ");
                sbSql.Append(" select Port_Emp.* ,Edu_jyj.[Name] as Dept from Port_Emp,Edu_jyj ");
                sbSql.Append(" where Port_Emp.FK_Dept=Edu_jyj.[No] and Port_Emp.[Name]='" + strName + "'");
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sbSql.ToString());
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sbResult = new StringBuilder();
                    sbResult.Append(dt.Rows[0]["No"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Name"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Addr"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Phone"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["MobileTel"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Email"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Dept"].ToString() + ",");
                    context.Response.Write(sbResult.ToString());
                }
                else
                {
                    context.Response.Write("查无此用户!");
                }
            }
        }
        //根据编号查找用户的详细信息
        else if (context.Request.Form["EmpNo"] != null)
        {
            string EmpNo = System.Web.HttpUtility.UrlDecode(context.Request.Form["EmpNo"].ToString());
            if (!string.IsNullOrEmpty(EmpNo))
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" select Port_Emp.* ,Edu_school.[Name] as Dept from Port_Emp,Edu_school ");
                sbSql.Append(" where Port_Emp.FK_Dept=Edu_school.[No] and Port_Emp.[No]='" + EmpNo + "'");
                sbSql.Append(" union ");
                sbSql.Append(" select Port_Emp.* ,Edu_jyj.[Name] as Dept from Port_Emp,Edu_jyj ");
                sbSql.Append(" where Port_Emp.FK_Dept=Edu_jyj.[No] and Port_Emp.[No]='" + EmpNo + "'");
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sbSql.ToString());
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sbResult = new StringBuilder();
                    sbResult.Append(dt.Rows[0]["No"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Name"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Addr"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Phone"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["MobileTel"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Email"].ToString() + ",");
                    sbResult.Append(dt.Rows[0]["Dept"].ToString() + ",");
                    context.Response.Write(sbResult.ToString());
                }
                else
                {
                    context.Response.Write("查无此用户!");
                }
            }
        }
        //保存好友信息
        else if (context.Request.Form["strSave"] != null)
        {
            string strSave = System.Web.HttpUtility.UrlDecode(Convert.ToString(context.Request.Form["strSave"]));
            if (!string.IsNullOrEmpty(strSave))
            {
                string[] strs = strSave.Split(',');
                BP.GE.GeFriend myFriend = new BP.GE.GeFriend();
                myFriend.FK_Emp2 = strs[0];
                myFriend.Name = strs[1];
                myFriend.Email = strs[2];
                myFriend.Mobile = strs[3];
                myFriend.Phone = strs[4];
                myFriend.Birthday = strs[5];
                myFriend.Address = strs[6];
                myFriend.Company = strs[7];
                myFriend.Note = strs[8];
                myFriend.FK_Emp1 = strs[9];
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" select count(*) from GE_Friend ");
                sbSql.Append(" where FK_Emp1='" + myFriend.FK_Emp1 + "'");
                sbSql.Append(" and FK_Emp2='" + myFriend.FK_Emp2 + "'");
                int j = BP.DA.DBAccess.RunSQLReturnValInt(sbSql.ToString());
                if (j <= 0)
                {
                    int i = myFriend.Save();
                    if (i > 0)
                    {
                        context.Response.Write("添加成功!");
                    }
                    else
                    {
                        context.Response.Write("添加失败!");
                    }
                }
                else
                {
                    context.Response.Write("该好友已存在!");
                }
            }
        }
        //更新好友信息
        else if (context.Request.Form["strUpdate"] != null)
        {
            string strSave = System.Web.HttpUtility.UrlDecode(Convert.ToString(context.Request.Form["strUpdate"]));
            if (!string.IsNullOrEmpty(strSave))
            {
                string[] strs = strSave.Split(',');
                BP.GE.GeFriend myFriend = new BP.GE.GeFriend();
                myFriend.FK_Emp2 = strs[0];
                myFriend.Name = strs[1];
                myFriend.Email = strs[2];
                myFriend.Mobile = strs[3];
                myFriend.Phone = strs[4];
                myFriend.Birthday = strs[5];
                myFriend.Address = strs[6];
                myFriend.Company = strs[7];
                myFriend.Note = strs[8];
                myFriend.FK_Emp1 = strs[9];
                myFriend.OID = Convert.ToInt32(strs[10]);
                int i = myFriend.Update();
                if (i > 0)
                {
                    context.Response.Write("修改成功!");
                }
                else
                {
                    context.Response.Write("修改失败!");
                }
            }
        }
        //发送邮件
        else if (context.Request.Form["OID"] == null && context.Request.Form["Emps"] != null && context.Request.Form["Title"] != null && context.Request.Form["CUser"] != null)
        {
            string strEmps = System.Web.HttpUtility.UrlDecode(context.Request.Form["Emps"].ToString());
            string strTitle = System.Web.HttpUtility.UrlDecode(context.Request.Form["Title"].ToString());
            string strDoc = System.Web.HttpUtility.UrlDecode(context.Request.Form["Doc"].ToString());
            string Emp = System.Web.HttpUtility.UrlDecode(context.Request.Form["CUser"].ToString());
            string[] strs = strEmps.Split(';');
            string Name = Emp.Substring(0, Emp.IndexOf('<'));
            string No = Emp.Substring(Emp.IndexOf('<') + 1, Emp.IndexOf('>') - Emp.IndexOf('<') - 1);
            for (int i = 0; i < strs.Length - 1; i++)
            {
                //发送人信息
                GeMessage message = new GeMessage();
                string strName = strs[i].Substring(0, strs[i].IndexOf('<'));
                string strNo = strs[i].Substring(strs[i].IndexOf('<') + 1, strs[i].IndexOf('>') - strs[i].IndexOf('<') - 1);
                message.Sender = No;
                message.SenderT = Name;
                message.StaS = 0;
                message.SendDT = DateTime.Now.ToString();
                //接收人信息
                message.Receiver = strNo;
                message.ReceiverT = strName;
                message.ReadSta = 0;
                message.StaR = 0;
                message.ReadDT = string.Empty;                
                //邮件正文
                message.Title = strTitle;
                message.Doc = strDoc;
                message.Insert();
                
            }
            context.Response.Write("信息发送成功!");
        }
        //回复邮件
        else if (context.Request.Form["OID"]!=null && context.Request.Form["Emps"] != null && context.Request.Form["Title"] != null && context.Request.Form["CUser"] != null)
        {
            string strEmps = System.Web.HttpUtility.UrlDecode(context.Request.Form["Emps"].ToString());
            string strTitle = System.Web.HttpUtility.UrlDecode(context.Request.Form["Title"].ToString());
            string strDoc = System.Web.HttpUtility.UrlDecode(context.Request.Form["Doc"].ToString());
            string Emp = System.Web.HttpUtility.UrlDecode(context.Request.Form["CUser"].ToString());
            string[] strs = strEmps.Split(';');
            string Name = Emp.Substring(0, Emp.IndexOf('<'));
            string No = Emp.Substring(Emp.IndexOf('<') + 1, Emp.IndexOf('>') - Emp.IndexOf('<') - 1);
            GeMessage message = new GeMessage();
            for (int i = 0; i < strs.Length - 1; i++)
            {
                //发送人信息
                string strName = strs[i].Substring(0, strs[i].IndexOf('<'));
                string strNo = strs[i].Substring(strs[i].IndexOf('<') + 1, strs[i].IndexOf('>') - strs[i].IndexOf('<') - 1);
                message.Sender = No;
                message.SenderT = Name;
                message.StaS = 0;
                message.SendDT = DateTime.Now.ToString();
                //接收人信息
                message.Receiver = strNo;
                message.ReceiverT = strName;
                message.ReadSta = 0;
                message.StaR = 0;
                message.ReadDT = string.Empty;
                //邮件正文
                message.Title = strTitle;
                message.Doc = strDoc;
                message.Insert();
            }
            string strSql = "Update GE_Message set ReadSta=2 where OID=" + context.Request.Form["OID"].ToString();
            BP.DA.DBAccess.RunSQL(strSql);
            context.Response.Write("信息回复成功!");
        }
        //存草稿
        else if (context.Request.Form["DraftEmps"] != null && context.Request.Form["DraftTitle"] != null && context.Request.Form["DraftCUser"] != null)
        {
            string strEmps = System.Web.HttpUtility.UrlDecode(context.Request.Form["DraftEmps"].ToString());
            string strTitle = System.Web.HttpUtility.UrlDecode(context.Request.Form["DraftTitle"].ToString());
            string strDoc = System.Web.HttpUtility.UrlDecode(context.Request.Form["DraftDoc"].ToString());
            string Emp = System.Web.HttpUtility.UrlDecode(context.Request.Form["DraftCUser"].ToString());
            string Name = Emp.Substring(0, Emp.IndexOf('<'));
            string No = Emp.Substring(Emp.IndexOf('<') + 1, Emp.IndexOf('>') - Emp.IndexOf('<') - 1);
            GeMessage message = new GeMessage();
            //发送人信息
            message.Sender = No;
            message.SenderT = Name;
            message.StaS = 1;
            message.SendDT = DateTime.Now.ToString();
            //接收人信息
            message.Receiver = strEmps;
            message.ReadSta = 0;
            message.StaR = 1;
            message.ReadDT = string.Empty;
            //邮件正文
            message.Title = strTitle;
            message.Doc = strDoc;
            message.Insert();
            context.Response.Write("保存草稿成功!");
        }
        //删除邮件
        else if (context.Request.Form["DelOID"] != null)
        {
            string[] strOIDs = System.Web.HttpUtility.UrlDecode(context.Request.Form["DelOID"].ToString()).Split(';');
            string strResult = string.Empty;
            for (int i = 0; i < strOIDs.Length - 1; i++)
            {
                string strSql = "UPDATE GE_Inbox SET OPSta = 2 WHERE oid= " + Convert.ToString(strOIDs[i]);
                BP.DA.DBAccess.RunSQL(strSql);
            }
            context.Response.Write("操作成功!");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}