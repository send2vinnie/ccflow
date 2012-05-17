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

    public XEmailTool(int type)
    {
        this.m_Type = type;
        m_Dict = new Dictionary<string, object>();
        m_Dict.Add(OA_EmailAttr.PriorityLevel, type);
    }

    public int GetQueryRowsCount(string searchValue)
    {
        return XQueryTool.GetRowCount<BP.CCOA.OA_Email>(email, columns, searchValue, this.m_Dict);
    }

    private static IDictionary<string, object> GetEmailDicts(int type)
    {
        IDictionary<string, object> dicts = new Dictionary<string, object>();
        dicts.Add(OA_EmailAttr.PriorityLevel, type);
        return dicts;
    }

    public DataTable Query(string searchValue, int pageIndex, int pageSize)
    {
        string[] columns = { OA_EmailAttr.Addressee, OA_EmailAttr.Addresser, OA_EmailAttr.Subject };
        DataTable OA_EmailTable = XQueryTool.Query<BP.CCOA.OA_Email>(email, columns, searchValue, pageIndex, pageSize,
            this.m_Dict);
        return OA_EmailTable;
    }
}