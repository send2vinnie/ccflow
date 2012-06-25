using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BP.CCOA;

/// <summary>
///XMessageTool 的摘要说明
/// </summary>
public class XMessageTool
{
    /// <summary>
    /// 插入接收方信息
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="selecedAuthIds"></param>
    public static void InsertMessageAuths(string messageId, string selecedAuthIds)
    {
        string[] selectedIds = selecedAuthIds.Split(',');
        foreach (string selectedId in selectedIds)
        {
            BP.CCOA.OA_MessageAuth noticeAuth = new BP.CCOA.OA_MessageAuth();
            noticeAuth.No = Guid.NewGuid().ToString();
            noticeAuth.FK_Message = messageId;
            noticeAuth.FK_Id = selectedId;
            noticeAuth.Insert();
        }
    }

    /// <summary>
    /// 通过消息ID获取接收方名称集合
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="accessType"></param>
    /// <returns></returns>
    public static string GetSelecedNames(string messageId, string accessType)
    {
        OA_MessageAuths auths = GetAuthsByMessageId(messageId);
        StringBuilder sb = new StringBuilder();
        int loopNo = 0;
        foreach (OA_MessageAuth auth in auths)
        {
            loopNo += 1;
            string name = XAuthTool.GetAuthNameByAuthId(auth.FK_Id, accessType);
            sb.Append(name);
            if (loopNo != auths.Count)
            {
                sb.Append(",");
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 获取公告的发布接收方信息
    /// </summary>
    /// <param name="noticeId"></param>
    /// <returns></returns>
    public static OA_MessageAuths GetAuthsByMessageId(string messageId)
    {
        OA_MessageAuths auths = new OA_MessageAuths();
        auths.Retrieve(OA_MessageAuthAttr.FK_Message, messageId);
        return auths;
    }


    /// <summary>
    /// 删除接收方信息集合
    /// </summary>
    /// <param name="noticeId"></param>
    public static void DeleteMessgaeAuths(string messageId)
    {
        OA_MessageAuths auths = GetAuthsByMessageId(messageId);
        auths.Delete();
    }

    /// <summary>
    /// 获得通知公告ID的接收方ID集合
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    public static string GetSelectedIds(string messageId)
    {
        StringBuilder sb = new StringBuilder();

        OA_MessageAuths auths = GetAuthsByMessageId(messageId);

        int loopNo = 0;

        foreach (OA_MessageAuth auth in auths)
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
}