using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BP.CCOA;
using System.Data;

/// <summary>
///XEmailTool 的摘要说明
/// </summary>
public class XEmailTool
{
    private int m_Type;

    private IDictionary<string, object> m_Dict;

    private string[] columns = { OA_EmailAttr.Addressee, OA_EmailAttr.Addresser, OA_EmailAttr.Subject };

    private BP.CCOA.OA_Email email = new BP.CCOA.OA_Email();

    public XEmailTool(XEmailType emailType, string loginUser)
    {
        this.m_Type = (int)emailType;
        m_Dict = new Dictionary<string, object>();
        switch (emailType)
        {
            //收件箱和垃圾箱的类型应该是发件箱，发件箱和草稿箱需要加以区分
            case XEmailType.InBox:
            case XEmailType.RecycleBox:
                m_Dict.Add(OA_EmailAttr.Category, (int)XEmailType.OutBox);
                break;
            default:
                m_Dict.Add(OA_EmailAttr.Category, (int)emailType);
                break;
        }

        this.FillDictByLoginUser(loginUser, emailType);
    }


    /// <summary>
    /// 填充操作人
    /// </summary>
    /// <param name="loginUser"></param>
    /// <param name="emailType"></param>
    private void FillDictByLoginUser(string loginUser, XEmailType emailType)
    {
        switch (emailType)
        {
            case XEmailType.InBox:
            case XEmailType.RecycleBox:
                this.m_Dict.Add(OA_EmailAttr.Addressee, loginUser);
                break;
            case XEmailType.OutBox:
            case XEmailType.DraftBox:
                this.m_Dict.Add(OA_EmailAttr.Addresser, loginUser);
                break;
        }
    }

    public int GetQueryRowsCount(string searchValue)
    {
        return XQueryTool.GetRowCount<BP.CCOA.OA_Email>(email, columns, searchValue, this.m_Dict);
    }

    public DataTable Query(string queryType, string userId, string searchValue, int pageIndex, int pageSize, IDictionary<string, object> whereCondition = null)
    {
        string[] columns = { OA_EmailAttr.Addressee, OA_EmailAttr.Addresser, OA_EmailAttr.Subject };
        if (whereCondition != null && whereCondition.Count > 0)
        {
            foreach (KeyValuePair<string, object> keyAndValue in whereCondition)
            {
                this.m_Dict.Add(keyAndValue.Key, keyAndValue.Value);
            }
        }
        XReadHelperBase readerHelper = XReadHelpManager.GetReadHelper(BP.CCOA.Enum.ClickObjType.Email, m_Type.ToString());
        DataTable OA_EmailTable = readerHelper.QueryByType(queryType, userId, columns, searchValue, pageIndex, pageSize, m_Dict);
        return OA_EmailTable;
    }

    /// <summary>
    /// 插入接收方信息
    /// </summary>
    /// <param name="noticeId"></param>
    /// <param name="selecedAuthIds"></param>
    public static void InsertEmailAuths(string emailId, string selecedAuthIds)
    {
        string[] selectedIds = selecedAuthIds.Split(',');
        foreach (string selectedId in selectedIds)
        {
            BP.CCOA.OA_EmailAuth emailAuth = new BP.CCOA.OA_EmailAuth();
            emailAuth.No = Guid.NewGuid().ToString();
            emailAuth.FK_Email = emailId;
            emailAuth.FK_Id = selectedId;
            emailAuth.Insert();
        }
    }

    public static string GetEmailTypeByCode(object code)
    {
        //：0-普通1-重要2-紧急
        string strCode = code.ToString();
        switch (strCode)
        {
            case "0":
                return "普通";
            case "1":
                return "重要";
            case "2":
                return "紧急";
            default:
                return "普通";
        }
    }

    /// <summary>
    /// 获取发送人
    /// </summary>
    /// <param name="sendPeopleId"></param>
    /// <returns></returns>
    public static string GetSendPeople(object sendPeopleId)
    {
        string strPeopleId = sendPeopleId.ToString();
        BP.EIP.Port_Emp portEmp = new BP.EIP.Port_Emp(strPeopleId);
        return portEmp.Name;
    }
}