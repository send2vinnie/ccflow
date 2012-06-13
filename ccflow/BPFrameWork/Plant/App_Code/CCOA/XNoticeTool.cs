using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BP.CCOA;
using System.Text;
using BP.EIP;

/// <summary>
///XNoticeTool 的摘要说明
/// </summary>
public class XNoticeTool
{

    /// <summary>
    /// 获得通知公告ID的接收方ID集合
    /// </summary>
    /// <param name="noticeId"></param>
    /// <returns></returns>
    public static string GetSelectedIds(string noticeId)
    {
        StringBuilder sb = new StringBuilder();

        OA_NoticeAuths auths = GetAuthsByNoticeId(noticeId);

        int loopNo = 0;

        foreach (OA_NoticeAuth auth in auths)
        {
            loopNo += 1;
            sb.Append(auth.FK_Id);
            if (loopNo != auths.Count)
            {
                sb.Append(",");
            }
        }

        return sb.ToString();
    }


    /// <summary>
    /// 通过通知公告ID获取接收方名称集合
    /// </summary>
    /// <param name="noticeId"></param>
    /// <param name="accessType"></param>
    /// <returns></returns>
    public static string GetSelecedNames(string noticeId, string accessType)
    {
        OA_NoticeAuths auths = GetAuthsByNoticeId(noticeId);

        StringBuilder sb = new StringBuilder();

        int loopNo = 0;

        foreach (OA_NoticeAuth auth in auths)
        {
            loopNo += 1;
            string name = GetAuthNameByAuthId(auth.FK_Id, accessType);
            sb.Append(name);
            if (loopNo != auths.Count)
            {
                sb.Append(",");
            }
        }

        return sb.ToString();

    }

    private static string GetAuthNameByAuthId(string authId, string accessType)
    {
        switch (accessType)
        {
            case "部门":
                Port_Dept portDept = new Port_Dept(authId);
                return portDept.Name;
            case "角色":
                Port_Station portStation = new Port_Station(authId);
                return portStation.Name;
            case "人员":
                Port_Emp portEmp = new Port_Emp(authId);
                return portEmp.Name;
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// 获取公告的发布接收方信息
    /// </summary>
    /// <param name="noticeId"></param>
    /// <returns></returns>
    public static OA_NoticeAuths GetAuthsByNoticeId(string noticeId)
    {
        OA_NoticeAuths auths = new OA_NoticeAuths();
        auths.Retrieve(OA_NoticeAuthAttr.FK_Notice, noticeId);
        return auths;
    }

    /// <summary>
    /// 删除接收方信息集合
    /// </summary>
    /// <param name="noticeId"></param>
    public static void DeleteNoticeAuths(string noticeId)
    {
        OA_NoticeAuths auths = GetAuthsByNoticeId(noticeId);
        auths.Delete();
    }

    /// <summary>
    /// 插入接收方信息
    /// </summary>
    /// <param name="noticeId"></param>
    /// <param name="selecedAuthIds"></param>
    public static void InsertNoticeAuths(string noticeId, string selecedAuthIds)
    {
        string[] selectedIds = selecedAuthIds.Split(',');
        foreach (string selectedId in selectedIds)
        {
            BP.CCOA.OA_NoticeAuth noticeAuth = new BP.CCOA.OA_NoticeAuth();
            noticeAuth.No = Guid.NewGuid().ToString();
            noticeAuth.FK_Notice = noticeId;
            noticeAuth.FK_Id = selectedId;
            noticeAuth.Insert();
        }
    }
}