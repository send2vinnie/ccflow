using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Web;
using BP.En;
using BP.Web.Controls;
using BP.Sys;

public partial class WF_FreeFrm : BP.Web.UC.UCBase3
{
    public string FK_MapData
    {
        get
        {
            return "ND501";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Init();

        BP.WF.Node nd = new BP.WF.Node("501");
        Entity en = nd.HisWork;
        en.SetValByKey("OID", 5484);
        en.Retrieve();
        this.UCEn1.BindFreeFrm(en, "ND501");
    }

    public void Init()
    {
        // 删除数据.
        FrmLabs labs = new FrmLabs();
        labs.Delete(FrmLabAttr.FK_MapData, this.FK_MapData);
        FrmLines lines = new FrmLines();
        lines.Delete(FrmLabAttr.FK_MapData, this.FK_MapData);

        MapData md = new MapData(this.FK_MapData);
        MapAttrs mattrs = new MapAttrs(this.FK_MapData);
        GroupFields gfs = new GroupFields(this.FK_MapData);

        int tableW = 700;
        int padingLeft = 3;
        int leftCtrlX = 700 / 100 * 20;
        int rightCtrlX = 700 / 100 * 60;

        // table 标题。
        int currX = 0;
        int currY = 0;
        FrmLab lab = new FrmLab();
        lab.Name = md.Name;
        lab.X = 200;
        currY += 30;
        lab.Y = currY;
        lab.FK_MapData = this.FK_MapData;
        lab.FrontWeight = "bold";
        lab.Insert();

        // 表格头部的横线.
        currY += 20;
        FrmLine lin = new FrmLine();
        lin.X1 = 0;
        lin.X2 = tableW;
        lin.Y1 = currY;
        lin.Y2 = currY;
        lin.BorderWidth = 2;
        lin.FK_MapData = this.FK_MapData;
        lin.Insert();
        currY += 5;

        bool isLeft = false;
        foreach (GroupField gf in gfs)
        {
            lab = new FrmLab();
            lab.X = 0;
            lab.Y = currY;
            lab.Name = gf.Lab;
            lab.FK_MapData = this.FK_MapData;
            lab.FrontWeight = "bold";
            lab.Insert();

            currY += 15;
            lin = new FrmLine();
            lin.X1 = padingLeft;
            lin.X2 = tableW;
            lin.Y1 = currY;
            lin.Y2 = currY;
            lin.FK_MapData = this.FK_MapData;
            lin.BorderWidth = 3;
            lin.Insert();

            isLeft = true;
            foreach (MapAttr attr in mattrs)
            {
                if (gf.OID != attr.GroupID || attr.UIVisible == false)
                    continue;

                if (isLeft)
                {
                    lin = new FrmLine();
                    lin.X1 = 0;
                    lin.X2 = tableW;
                    lin.Y1 = currY;
                    lin.Y2 = currY;
                    lin.FK_MapData = this.FK_MapData;
                    lin.Insert();
                    currY += 14; /* 画一横线 .*/

                    lab = new FrmLab();
                    lab.X = lin.X1 + padingLeft;
                    lab.Y = currY;
                    lab.Name = attr.Name;
                    lab.FK_MapData = this.FK_MapData;
                    lab.Insert();

                    lin = new FrmLine();
                    lin.X1 = leftCtrlX;
                    lin.Y1 = currY-14;

                    lin.X2 = leftCtrlX;
                    lin.Y2 = currY;
                    lin.FK_MapData = this.FK_MapData;
                    lin.Insert(); /*画一 竖线 */

                    attr.X = leftCtrlX + padingLeft;
                    attr.Y = currY - 3;
                    attr.Update();
                    currY += 14;
                }
                else
                {
                    currY = currY -14;
                    lab = new FrmLab();
                    lab.X = tableW / 2+padingLeft;
                    lab.Y = currY;
                    lab.Name = attr.Name;
                    lab.FK_MapData = this.FK_MapData;
                    lab.Insert();

                    lin = new FrmLine();
                    lin.X1 = tableW / 2;
                    lin.Y1 = currY - 14;

                    lin.X2 = tableW / 2;
                    lin.Y2 = currY;
                    lin.FK_MapData = this.FK_MapData;
                    lin.Insert(); /*画一 竖线 */

                    lin = new FrmLine();
                    lin.X1 = rightCtrlX;
                    lin.Y1 = currY - 14;
                    lin.X2 = rightCtrlX;
                    lin.Y2 = currY;
                    lin.FK_MapData = this.FK_MapData;
                    lin.Insert(); /*画一 竖线 */

                    attr.X = rightCtrlX+padingLeft;
                    attr.Y = currY - 3;
                    attr.Update();
                    currY += 14;
                }

                isLeft = !isLeft;
            }
        }
        // table bottom line.
        lin = new FrmLine();
        lin.X1 = 0;
        lin.Y1 = currY;

        lin.X2 = tableW;
        lin.Y2 = currY;
        lin.FK_MapData = this.FK_MapData;
        lin.BorderWidth = 3;
        lin.Insert();


        currY = currY - 28-18;
        // 处理结尾. table left line
        lin = new FrmLine();
        lin.X1 = 0;
        lin.Y1 = 50;
        lin.X2 = 0;
        lin.Y2 = currY ;
        lin.FK_MapData = this.FK_MapData;
        lin.BorderWidth = 3;
        lin.Insert();

        // table right line.
        lin = new FrmLine();
        lin.X1 = tableW;
        lin.Y1 = 50;

        lin.X2 = tableW;
        lin.Y2 = currY;
        lin.FK_MapData = this.FK_MapData;
        lin.BorderWidth = 3;
        lin.Insert();


        //开始画左右边的竖线.
    }
}