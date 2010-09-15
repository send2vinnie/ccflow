using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.En;
using BP.DA;
using BP.Web;
using BP.Port;

public partial class Admin_Welcome : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string msg = "您的单位：" + BP.Web.WebUser.FK_DeptName;
        msg += "<br>单位编号：" + BP.Web.WebUser.FK_Dept;
        msg += "<br>用户帐号：" + BP.Web.WebUser.No;

        msg += "<hr><a href='/Edu/Comm/Port/ChangePass.aspx'>修改密码</a>";
        //  msg += "<br>工作级次：" + BP.Edu.EduUser.HisWorkGrade;
        // this.UCSys1.AddFieldSet("<b>您好："+WebUser.Name+"</b>", msg);

        this.UCSys1.AddDivRound();
        this.UCSys1.Add(msg);
        this.UCSys1.AddDivRoundEnd();


        //  // 要处理的工作.
        //  //this.UCSys2.AddFieldSet("<b>以下工作需要处理：</b>");
        //  this.UCSys2.AddTable();
        ////  this.UCSys2.AddCaptionLeft( this.GenerCaption("以下工作需要处理"));
        //  this.UCSys2.AddCaptionLeft( "以下工作需要处理" );
        //  this.UCSys2.AddTR();

        //  this.UCSys2.AddTDGroupTitle("项目");
        //  this.UCSys2.AddTDGroupTitle("数量");
        //  this.UCSys2.AddTDGroupTitle("查看");
        //  this.UCSys2.AddTREnd();

        //  this.UCSys2.AddTR();
        //  this.UCSys2.AddTD("等待审核的教师");
        //  this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where UseSta='" + (int)BP.Edu.UseSta.UnCheck + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        //  this.UCSys2.AddTDA("../Comm/Batch.aspx?EnsName=BP.Edu.EduEmps&UseSta=" + (int)BP.Edu.UseSta.UnCheck, "查看...");
        //  this.UCSys2.AddTREnd();


        //  this.UCSys2.AddTR();
        //  this.UCSys2.AddTD("等待审核的[共享资源]");
        //  this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM Edu_ShareFile WHERE IsRel=0 AND FK_Dept='" + EduUser.FK_Dept + "'"));
        //  this.UCSys2.AddTDA("../Comm/Batch.aspx?EnsName=BP.Edu.ShareFiles&IsRel=0", "查看...");
        //  this.UCSys2.AddTREnd();

        //  this.UCSys2.AddTR();
        //  this.UCSys2.AddTD("等待审核的[资源请求]");
        //  this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM FAQ_Question WHERE IsRel=0 AND FK_Dept='" + EduUser.FK_Dept + "'"));
        //  this.UCSys2.AddTDA("../Comm/Batch.aspx?EnsName=BP.Edu.Questions&IsRel=0", "查看...");
        //  this.UCSys2.AddTREnd();

        //  //this.UCSys2.AddTR();
        //  //this.UCSys2.AddTD("等待审核的[资源回复]");
        //  //this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where UseSta='" + (int)BP.Edu.UseSta.UnCheck + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        //  //this.UCSys2.AddTDA("../Comm/PanelEns.aspx?EnsName=BP.Edu.EduEmps&UseSta=" + (int)BP.Edu.UseSta.UnCheck, "现在执行...");
        //  //this.UCSys2.AddTREnd();

        //  this.UCSys2.AddTR();
        //  this.UCSys2.AddTD("等待审核的[共享备课] ");
        //  this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where UseSta='" + (int)BP.Edu.UseSta.UnCheck + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        //  this.UCSys2.AddTDA("../Comm/Batch.aspx?EnsName=BP.Edu.EduEmps&UseSta=" + (int)BP.Edu.UseSta.UnCheck, "查看...");
        //  this.UCSys2.AddTREnd();


        //  this.UCSys2.AddTableEnd();
        //  this.UCSys2.AddFieldSetEnd();



        //// 本周工作变化.
        ////this.UCSys2.AddFieldSet("<b>本月实时数据：</b>");
        //this.UCSys2.AddTable();
        //this.UCSys2.AddCaptionLeft("本月实时数据");

        //this.UCSys2.AddTR();
        //this.UCSys2.AddTDGroupTitle("项目");
        //this.UCSys2.AddTDGroupTitle("数量");
        //this.UCSys2.AddTDGroupTitle("查看");
        //this.UCSys2.AddTREnd();

        ////this.UCSys2.AddTR();
        ////this.UCSys2.AddTD("本单位的活跃度，排名。");
        ////this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where UseSta='" + (int)BP.Edu.UseSta.UnCheck + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        ////this.UCSys2.AddTDA("../Comm/PanelEns.aspx?EnsName=BP.Edu.EduEmps&UseSta=" + (int)BP.Edu.UseSta.UnCheck, "打开...");
        ////this.UCSys2.AddTREnd();


        //this.UCSys2.AddTR();
        //this.UCSys2.AddTD("新注册用户");
        //this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where FK_NY = '" + BP.DA.DataType.CurrentYearMonth + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        //this.UCSys2.AddTDA("../Comm/PanelEns.aspx?EnsName=BP.Edu.EduEmps&UseSta=all", "查看...");
        //this.UCSys2.AddTREnd();


        //this.UCSys2.AddTR();
        //this.UCSys2.AddTD("新上传的的资源");
        //this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where FK_NY = '" + BP.DA.DataType.CurrentYearMonth + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        //this.UCSys2.AddTDA("../Comm/PanelEns.aspx?EnsName=BP.Edu.EduEmps&UseSta=all", "查看...");
        //this.UCSys2.AddTREnd();



        //this.UCSys2.AddTR();
        //this.UCSys2.AddTD("新的[资源请求] ");
        //this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where FK_NY = '" + BP.DA.DataType.CurrentYearMonth + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        //this.UCSys2.AddTDA("../Comm/PanelEns.aspx?EnsName=BP.Edu.EduEmps&UseSta=all", "查看...");
        //this.UCSys2.AddTREnd();


        //this.UCSys2.AddTR();
        //this.UCSys2.AddTD("新的备课");
        //this.UCSys2.AddTD(BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM port_emp where FK_NY = '" + BP.DA.DataType.CurrentYearMonth + "' and fk_dept='" + EduUser.FK_Dept + "'"));
        //this.UCSys2.AddTDA("../Comm/PanelEns.aspx?EnsName=BP.Edu.EduEmps&UseSta=all", "查看...");
        //this.UCSys2.AddTREnd();

        //this.UCSys2.AddTableEnd();
        //  this.UCSys2.AddFieldSetEnd();

        //this.UCSys1.AddH1("&nbsp;&nbsp;我的岗位");
        //this.UCSys1.AddHR();
        //Stations sts = WebUser.HisStations;
        //foreach (Station st in sts)
        //{
        //    this.UCSys1.AddBR("&nbsp;&nbsp;"+st.Name);
        //    this.UCSys1.AddBR();
        //}
        //this.UCSys1.AddBR();
        //this.UCSys1.AddBR();
        //this.UCSys1.AddFieldSetEnd();
    }
}
