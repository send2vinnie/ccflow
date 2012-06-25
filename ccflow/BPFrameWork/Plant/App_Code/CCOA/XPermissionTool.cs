using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using BP.EIP.Interface;
using BP.EIP.DALFactory;

/// <summary>
///XPermissionTool 的摘要说明
/// </summary>
public class XPermissionTool
{

    private static DataTable m_PermissionTable;

    public static void InitControlByPrivilege(Control control, string functionName, string userId)
    {
        if (control.HasControls())
        {
            foreach (Control childControl in control.Controls)
            {
                InitControlByPrivilege(childControl, functionName, userId);
            }
        }
        else
        {
            control.Visible = IsHavePrivilege(control, functionName, userId);
        }
    }

    private static bool IsHavePrivilege(Control control, string functionName, string userId)
    {
        IPermission permission = DataAccess.CreatePermission();
        if (m_PermissionTable == null)
        {
            m_PermissionTable = permission.GetDTByUser(userId);
        }
        string where = string.Format("FK_FUNCTION='{0}' AND CONTROL_NAME='{1}'", functionName, control.ID);
        DataRow[] findRows = m_PermissionTable.Select(where);
        return findRows.Length > 0;
    }

}