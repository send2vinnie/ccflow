using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.GE;
using BP.Web;
using BP.DA;
using BP.En;
using FtpSupport;
using Microsoft.Win32;
using BP.Web.Controls;

public partial class GE_NetDisk_SaveName : WebPage
{
    public string FK_GradeNo
    {
        get
        {
            return Request.QueryString["FK_GradeNo"];
        }

    }

    public string DoType
    {
        get
        {
            return Request.QueryString["DoType"];

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (IsPostBack == false && this.DoType == "EditName")
        {
            DKDir newDir = new DKDir();
            string selectedID = this.FK_GradeNo;

            if (selectedID.Length % 2 == 1)
            {
                selectedID = "0" + selectedID;
            }

            // 创建本节点
            DKDir dirSelected = new DKDir();
            dirSelected.RetrieveByAttrAnd(DKDirAttr.GradeNo, selectedID, DKDirAttr.FK_Emp, BP.Web.WebUser.No);

            tbDirName.Text = dirSelected.Name;
        }


    }

    public void btnSave_Click(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "SaveName":
                SaveName();
                break;
            case "SaveChildName":
                SaveChildName();
                break;
            case "EditName":
                EditName();
                break;
        }
        this.WinClose();
    }

    void EditName()
    {
        DKDir newDir = new DKDir();
        string selectedID = this.FK_GradeNo;

        if (selectedID.Length % 2 == 1)
        {
            selectedID = "0" + selectedID;
        }

        // 创建本节点
        DKDir dirSelected = new DKDir();
        dirSelected.RetrieveByAttrAnd(DKDirAttr.GradeNo, selectedID, DKDirAttr.FK_Emp, BP.Web.WebUser.No);

        dirSelected.Name = tbDirName.Text;
        dirSelected.DirectUpdate();
    }

    void SaveChildName()
    {
        DKDir newDir = new DKDir();
        string selectedID = this.FK_GradeNo;

        if (selectedID.Length % 2 == 1)
        {
            selectedID = "0" + selectedID;
        }

        // 创建本节点
        DKDir dirSelected = new DKDir();
        dirSelected.RetrieveByAttrAnd(DKDirAttr.GradeNo, selectedID,DKDirAttr.FK_Emp,BP.Web.WebUser.No);

        // 获取所有孩子节点
        DKDirs dirs = new DKDirs();
        int dirCount = dirs.ReNextChild(BP.Web.WebUser.No, dirSelected.GradeNo);
        if (dirCount == 0)
        {
            newDir.GradeNo = dirSelected.GradeNo + "01";
            newDir.FK_Emp = BP.Web.WebUser.No;
            newDir.Name = tbDirName.Text;
            newDir.Insert();
        }
        else
        {
            // 获取最后节点的编号
            int temp = int.Parse(((DKDir)dirs[dirCount - 1]).GradeNo);
            temp++;
            string newGradeNo = temp.ToString();
            if (newGradeNo.Length % 2 == 1)
                newGradeNo = "0" + newGradeNo;


            newDir.GradeNo = newGradeNo;
            newDir.FK_Emp = BP.Web.WebUser.No;
            newDir.Name = tbDirName.Text;
            newDir.Insert();
        }
    }

    void SaveName()
    {
        string selectedID = this.FK_GradeNo;
        DKDir newDir = new DKDir();

        // 如果没有选择目录节点， 将新建目录添加到最后
        if (selectedID == "" || selectedID == null)
        {
            DKDirs dirRoot = new DKDirs();
            int dirCount = dirRoot.ReGrade1(BP.Web.WebUser.No);

            if (dirCount == 0)
            {
                newDir.GradeNo = "01";
                newDir.FK_Emp = BP.Web.WebUser.No;
                newDir.Name = tbDirName.Text;
                newDir.Insert();
            }
            else
            {
                // 获取根目录列表中，最后的值
                int lastGradeNo = int.Parse(((DKDir)dirRoot[dirCount - 1]).GradeNo);
                lastGradeNo++;
                string newGradeNo = lastGradeNo.ToString();

                if (newGradeNo.Length % 2 == 1)
                    newGradeNo = "0" + newGradeNo;

                newDir.GradeNo = newGradeNo;
                newDir.FK_Emp = BP.Web.WebUser.No;
                newDir.Name = tbDirName.Text;
                newDir.Insert();
            }
        }
        else // 选择了目标节点， 将新建目录添加到选定节点的前面
        {
            if (selectedID.Length % 2 == 1)
            {
                selectedID = "0" + selectedID;
            }

            // 创建本节点
            DKDir dirSelected = new DKDir();
            dirSelected.RetrieveByAttrAnd(DKDirAttr.GradeNo, selectedID,DKDirAttr.FK_Emp,BP.Web.WebUser.No);

            // 获取所有兄弟节点
            DKDirs dirs = dirSelected.HisBrotherNodesDescOrder;

            foreach (DKDir dir in dirs)
            {
                if (dir.GradeNo != selectedID)
                {
                    // 获取新的编号
                    int temp = int.Parse(dir.GradeNo);
                    temp++;
                    string newGrade = temp.ToString();
                    if (newGrade.Length % 2 == 1)
                        newGrade = "0" + newGrade;

                    // 更新本节点和子节点的编号
                    string sql = "update GE_DKDir set GradeNo= '" + newGrade + "'+substring(GradeNo,len('" + dir.GradeNo + "')+1,len(GradeNo)) where GradeNo like '" + dir.GradeNo + "'+'%' ";
                    DBAccess.RunSQL(sql);
                }
                else
                {
                    // 更新选定节点和子节点的编号
                    int temp = int.Parse(selectedID);
                    temp++;
                    string newGradeNo = temp.ToString();
                    if (newGradeNo.Length % 2 == 1)
                        newGradeNo = "0" + newGradeNo;

                    string sql = "update GE_DKDir set GradeNo= '" + newGradeNo + "'+substring(GradeNo,len('" + selectedID + "')+1,len(GradeNo)) where GradeNo like '" + selectedID + "'+'%' ";
                    DBAccess.RunSQL(sql);

                    // 创建新节点

                    newDir.GradeNo = selectedID;
                    newDir.FK_Emp = BP.Web.WebUser.No;
                    newDir.Name = tbDirName.Text;
                    newDir.Insert();
                    break;
                }
            }
        }

    }

    public void btnCancel_Click(object sender, EventArgs e)
    {
        this.WinClose();
    }
}
