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
using BP.En;
using BP.DA;
using BP.KG;
using BP.GE;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        MainMenuXmls xmls =new MainMenuXmls();
        xmls.RetrieveAll();

        foreach(MainMenuXml xml in xmls)
        {
            this.Response.Write( xml.Name);
        }
        return;

        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable("select * from port_stu");
        BP.DA.DBAccess.RunSQL("delete  from port_stu");

        BP.DA.DBAccess.RunSP("port_stu");
        return;

        
        Stu en1 = new Stu("002");

        Stu en3 = new Stu();
        en3.Copy(en1);
        en3.Insert();




        Stu en = new Stu();
        en.No =en.GenerNewNoByKey;

        en.No = "peng";
        en.Name = "sss";
        en.FK_BJ = "01";
        en.Insert();

        Stu en1 = new Stu();
        en1.No = "peng";
        en1.Retrieve();
        this.Response.Write(en1.Name + en1.Blong +en1.FK_BJ + en1.FK_BJT );

        en1.FK_BJ = "01";
        en1.Update();
        en1.Delete();
        return;

        Stus ens = new Stus();
        ens.RetrieveAll();
        foreach (Stu en in ens)
        {
            this.Response.Write(en.Name + en.Blong + en.FK_BJ + en.FK_BJT);
        }


        Stus ens1 = new Stus();
        ens1.Retrieve(StuAttr.FK_BJ, "01");
        foreach (Stu en1 in ens1)
        {
            this.Response.Write(en1.Name + en1.Blong + en1.FK_BJ + en1.FK_BJT);
        }



        Stus ens2 = new Stus();
        QueryObject qo = new QueryObject(ens2);
        qo.AddWhere(StuAttr.FK_BJ, "01");
        qo.DoQuery();
        foreach (Stu en2 in ens2)
        {
            this.Response.Write(en2.Name + en2.Blong + en2.FK_BJ + en2.FK_BJT);
        }




        QueryObject qo = new QueryObject(ens);
        qo.AddWhere("FK_BJ", "01");
        qo.addAnd();
        qo.AddWhere("Nsss", "sss01");
        qo.DoQuery();

        return;


        ens.Retrieve("FK_BJ", "01");

        ens.RetrieveAll();

        foreach (Stu en in ens)
        {
            this.Response.Write(en.Name);

        }
        return;


        this.Pub1.DivInfoBlockRed(" <a href='./App/'>后台维护</a>");


        this.Pub1.DivInfoBlockBegin();
        this.Pub1.Add("我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦我觉得中间可以显示这个东西哦");
        this.Pub1.DivInfoBlockEnd();
         
        this.Pub2.DivInfoBlockRed("看到吗？红色的是这样的~");

        this.Pub3.DivInfoBlockGreen("<table border=1 width=100%><tr><td align=center>asdsa</td><td align=center>sdsda</td><td align=center>sdasd</td></tr></table>");
        this.Pub4.DivInfoBlockBlue("<table  width=100% border=1><tr><td align=center>武侠小说</td><td align=center>古典名著</td><td align=center>侦探小说</td></tr><tr><td align=center>感情小说</td><td align=center>经典剧本</td><td align=center>杂志集合</td></tr></table>");
        this.Pub5.DivInfoBlockYellow("<table  width=100%><tr><td align=center>☏&nbsp;市区电话</td><td align=center>☏&nbsp;县区电话</td><td align=center>☏&nbsp;城镇电话</td></tr><tr><td align=center>☏&nbsp;乡村电话</td><td align=center>经典剧本</td><td align=center>杂志集合</td></tr></table>");

    }
}
