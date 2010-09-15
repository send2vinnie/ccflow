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
using BP.Web;
using BP.DA;
using BP.En;
using BP.GE;
using FtpSupport;
using Microsoft.Win32;
using BP.Web.Controls;

public partial class GE_NetDisk_NetDiskDir : BP.Web.UC.UCBase3
{
    public string FK_Dir
    {
        get
        {
            return Request.QueryString["FK_Dir"];

        }
    }
    public string FK_GradeNo
    {
        get
        {
            return Request.QueryString["FK_GradeNo"];

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ////引用css和js
        //this.Page.Response.Write("<link href='" + this.Request.ApplicationPath + "/GE/NetDisk/dtree.css' rel='stylesheet' type='text/css' />");
        //this.Response.Write("<script src='" + this.Request.ApplicationPath + "/GE/NetDisk/dtree.js' type='text/javascript'></script>");

        //绑定左侧目录
        this.BindDir();

        //绑定右侧目录资源
        this.BindDtl();
    }

    //添加目录子节点
    public void AddChildDirs(DKDirs roots, string pNo)
    {
        DKDirs dirs = new DKDirs();
        foreach (DKDir root in roots)
        {
            this.Pub1.Add("d.add(" + root.GradeNo + "," + pNo + ",'" + "<a href=#  oncontextmenu=\"return false\" onmousedown=SetHidValue(\"" + root.GradeNo + "\",\"" + "NetDiskDtl.aspx?FK_Dir=" + root.OID + "" + "\"," + root.OID + ")>" + root.Name + "</a>" + "','NetDiskDtl.aspx?FK_Dir=" + root.OID + "');");

            dirs.ReNextChild(BP.Web.WebUser.No , root.GradeNo);
            AddChildDirs(dirs, root.GradeNo);
        }
    }
    //绑定目录结构
    public void BindDir()
    {
        // 获取第一级目录
        DKDirs roots = new DKDirs();
        int rootCount = roots.ReGrade1(BP.Web.WebUser.No);

        this.Pub1.Add("<script type='text/javascript'>");
        string baseUrl = this.Request.ApplicationPath + "/GE/NetDisk/";
        this.Pub1.Add("d = new dTree('d','" + baseUrl + "','fm');");
        string title = "网络硬盘";
        this.Pub1.Add("d.add(10000,-1,'" + title + "');");

        DKDirs dirs = new DKDirs();

        //添加根目录
        foreach (DKDir root in roots)
        {
            this.Pub1.Add("d.add(" + root.GradeNo + ",10000,'" + "<a href=#   oncontextmenu=\"return false\" onmousedown=SetHidValue(\"" + root.GradeNo + "\",\"" + "NetDiskDtl.aspx?FK_Dir=" + root.OID + "" + "\"," + root.OID + ")>" + root.Name + "</a>" + "','NetDiskDtl.aspx?FK_Dir=" + root.OID + "');");

            // 获取孩子子节点
            dirs.ReNextChild(BP.Web.WebUser.No, root.GradeNo);
            AddChildDirs(dirs, root.GradeNo);
        }
        this.Pub1.Add("document.write(d);");
        this.Pub1.Add("</script>");

    }


    public void BindDtl()
    {
        this.Pub2.Add(String.Format("<iframe id='fm' name='fm' class='iframe' scrolling=\"auto\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\" width=\"100%\" height='100%' src=\"NetDiskDtl.aspx?FK_Dir={0}\"></iframe>", this.FK_Dir));

    }


    
   public  void btnAdd_Click(string dirName)
    {
        string selectedID = hidNodeID.Value;
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
                newDir.Name = dirName;
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
                newDir.Name = dirName;
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
                    newDir.Name = dirName;
                    newDir.Insert();
                    break;
                }
            }
        }
        Response.Redirect(this.PageID + ".aspx?FK_Dir=" + newDir.OID);
    }

    public  void btnDelete_Click(object sender, EventArgs e)
    {

        string selectedID = hidNodeID.Value;
        if (selectedID.Length % 2 == 1)
        {
            selectedID = "0" + selectedID;
        }

        DKDir dirSelected = new DKDir();
        dirSelected.RetrieveByAttrAnd(DKDirAttr.GradeNo, selectedID,DKDirAttr.FK_Emp,BP.Web.WebUser.No);

        // 获取父节点的目录编号
        string gradeNoOfParent = dirSelected.GradeNoOfParent;

        //// 修改本节点一下的所有兄弟节点的编号
        //DKDir dirSelect  = new DKDir();
        //dirSelect.RetrieveByAttr(DKDirAttr.GradeNo, selectedID);

        //DKDirs dirs = dirSelect.HisBrotherNodesDescOrder;

        //foreach (DKDir dir in dirs)
        //{
        //    if (dir.GradeNo == selectedID)
        //        break;

        //    int temp = int.Parse(dir.GradeNo);
        //    temp--;
        //    string newGradeNo = temp.ToString();
        //    if (newGradeNo.Length % 2 == 1)
        //        newGradeNo = "0" + newGradeNo;

        //    string sql = "update GE_DKDir set GradeNo= '" + newGradeNo + "'+substring(GradeNo,len('" + dir.GradeNo  + "')+1,len(GradeNo)) where GradeNo like '" + dir.GradeNo + "'+'%' ";
        //    DBAccess.RunSQL(sql);
        //}


        // 获取选定节点的所有子节点
        DKDirs ens = new DKDirs();
        ens.ReHisChilds(BP.Web.WebUser.No, selectedID);

        // 获取所有节点的DKFile的信息
        foreach (DKDir en in ens)
        {
            DKFiles dkFiles = new DKFiles();
            dkFiles.RetrieveByAttr(DKFileAttr.FK_DKDir, en.OID);

            // 删除所有服务器上的文件
            foreach (DKFile dkFile in dkFiles)
            {
                DeleteFileFromFTP(dkFile);
                dkFile.DirectDelete();
            }

        }

        // 从数据库中删除本节点和子节点
        string delSql = "delete from GE_DKDir where GradeNo like '" + selectedID + "%'";
        DBAccess.RunSQL(delSql);

       

        // 刷新页面
        if (gradeNoOfParent == "")
        {
            Response.Redirect(this.PageID + ".aspx");
        }
        else
        {
                DKDir dirParent = new DKDir();
            dirParent.RetrieveByAttrAnd(DKDirAttr.GradeNo, gradeNoOfParent,DKDirAttr.FK_Emp ,BP.Web.WebUser.No);

            Response.Redirect(this.PageID + ".aspx?FK_Dir=" + dirParent.OID );
        
        }
    }

    public void btnUp_Click(object sender, EventArgs e)
    {
        string selectedID = hidNodeID.Value;
        if (selectedID.Length % 2 == 1)
        {
            selectedID = "0" + selectedID;
        }

        // 创建本节点
        DKDir dirSelected = new DKDir();
        dirSelected.RetrieveByAttrAnd(DKDirAttr.GradeNo, selectedID,DKDirAttr.FK_Emp,BP.Web.WebUser.No);

        // 获取所有兄弟节点
        DKDirs dirs = dirSelected.HisBrotherNodes;

        // 记录前一个节点
        DKDir pre = dirSelected;
        foreach (DKDir dir in dirs)
        {
            if (dir.GradeNo == selectedID)
                break;
            pre = dir;
        }

        if (pre.GradeNo == selectedID)
        {
            this.Alert("本节点已经是最上层节点，不能被移动！");

        }
        else
        {
            string tempSql = "update GE_DKDir set GradeNo= 'tp'+GradeNo where GradeNo like '" + selectedID + "'+'%'";
            string preSql = "update GE_DKDir set GradeNo= '" + selectedID + "'+substring(GradeNo,len('" + selectedID + "')+1,len(GradeNo)) where GradeNo like '" + pre.GradeNo + "'+'%' ";
            string thisSql = "update GE_DKDir set GradeNo= '" + pre.GradeNo + "'+substring(GradeNo,len('" + selectedID + "')+3,len(GradeNo)) where GradeNo like 'tp'+'" + selectedID + "'+" + "'%'";

            DBAccess.RunSQL(tempSql);
            DBAccess.RunSQL(preSql);
            DBAccess.RunSQL(thisSql);
            Response.Redirect(this.PageID + ".aspx?FK_Dir=" + hidDirOID.Value+"&FK_GradeNo="+hidNodeID.Value );
        }
    }

    public void  btnDown_Click(object sender, EventArgs e)
    {
        string selectedID = hidNodeID.Value;
        if (selectedID.Length % 2 == 1)
        {
            selectedID = "0" + selectedID;
        }

        // 创建本节点
        DKDir dirSelected = new DKDir();
        dirSelected.RetrieveByAttrAnd(DKDirAttr.GradeNo, selectedID, DKDirAttr.FK_Emp, BP.Web.WebUser.No);


        // 获取所有兄弟节点
        DKDirs dirs = dirSelected.HisBrotherNodes;

        // 记录前一个节点
        DKDir after = dirSelected;
        bool flag = false;

        foreach (DKDir dir in dirs)
        {
            if (flag)
            {
                after = dir;
                break;
            }

            if (dir.GradeNo == selectedID)
            {
                flag = true;
            }
        }

        if (after.GradeNo == selectedID)
        {
            this.Alert("本节点已经是最下层节点，不能被移动！");
        }
        else
        {

            string tempSql = "update GE_DKDir set GradeNo= 'tp'+GradeNo where GradeNo like '" + selectedID + "'+'%'";
            string preSql = "update GE_DKDir set GradeNo= '" + selectedID + "'+substring(GradeNo,len('" + selectedID + "')+1,len(GradeNo)) where GradeNo like '" + after.GradeNo + "'+'%' ";
            string thisSql = "update GE_DKDir set GradeNo= '" + after.GradeNo + "'+substring(GradeNo,len('" + selectedID + "')+3,len(GradeNo)) where GradeNo like 'tp'+'" + selectedID + "'+" + "'%'";

            DBAccess.RunSQL(tempSql);
            DBAccess.RunSQL(preSql);
            DBAccess.RunSQL(thisSql);
            Response.Redirect(this.PageID + ".aspx?FK_Dir=" + hidDirOID.Value + "&FK_GradeNo=" + hidNodeID.Value);
           
        }
    }

    public void DeleteFileFromFTP(DKFile dkFile)
    {
        string fileName = dkFile.OID + "." + dkFile.MyFileExt;

        // 文件的存放目录 
        string basePath = GloDK.NetDiskFtpPath + "/" + BP.Web.WebUser.FK_Dept + "/" + BP.Web.WebUser.No + "/";
        string filePath = basePath + fileName;

        try
        {
            // 删除FTP服务器上的文件
            FtpConnection ftpConn = GloDK.FileFtpConn;
            ftpConn.SetCurrentDirectory(basePath);
            if (ftpConn.FileExist(fileName))
            {
                ftpConn.DeleteFile(fileName);
            }
        }
        catch
        {
            this.Alert("不能正确连接FTP服务器，请检查服务器链接！");
        }
        // 删除数据库的信息
        dkFile.DirectDelete();
    }
}
