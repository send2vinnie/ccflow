using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ͼƬ��
    /// </summary>
    public class ImgRunAttr
    {
        /// <summary>
        /// ͼƬ��Key
        /// </summary>
        public const string NumOfDtl = "NumOfDtl";
        /// <summary>
        /// ������Ա
        /// </summary>
        public const string Speed = "Speed";
        /// <summary>
        /// ������ͼƬ
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// Tag
        /// </summary>
        public const string Tag = "Tag";

        public const string ImgH = "ImgH";
        public const string ImgW = "ImgW";
        public const string Dir = "Dir";
        public const string NumOfRec = "NumOfRec";
    }
    /// <summary>
    /// ������ͼƬ
    /// </summary>
    public class ImgRun : EntityMyPK
    {
        #region ����ͼƬ��
        /// <summary>
        /// ������ͼƬ
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(ImgRunAttr.Title);
            }
            set
            {
                this.SetValByKey(ImgRunAttr.Title, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Dir
        {
            get
            {
                return this.GetValStringByKey(ImgRunAttr.Dir);
            }
            set
            {
                this.SetValByKey(ImgRunAttr.Dir, value);
            }
        }
       
        public string Tag
        {
            get
            {
                return this.GetValStringByKey(ImgRunAttr.Tag);
            }
            set
            {
                this.SetValByKey(ImgRunAttr.Tag, value);
            }
        }
        /// <summary>
        /// �����ٶ�
        /// </summary>
        public int Speed
        {
            get
            {
                int i = this.GetValIntByKey(ImgRunAttr.Speed);
                if (i == 0)
                    return 18;

                return i;

            }
            set
            {
                this.SetValByKey(ImgRunAttr.Speed, value);
            }
        }
        /// <summary>
        /// ͼƬ��
        /// </summary>
        public int NumOfDtl
        {
            get
            {
                return this.GetValIntByKey(ImgRunAttr.NumOfDtl);
            }
            set
            {
                this.SetValByKey(ImgRunAttr.NumOfDtl, value);
            }
        }
        public int NumOfRec
        {
            get
            {
                return this.GetValIntByKey(ImgRunAttr.NumOfRec);
            }
            set
            {
                this.SetValByKey(ImgRunAttr.NumOfRec, value);
            }
        }
        public int ImgH
        {
            get
            {
                return this.GetValIntByKey(ImgRunAttr.ImgH, 160);
            }
            set
            {
                this.SetValByKey(ImgRunAttr.ImgH, value);
            }
        }
        public int ImgW
        {
            get
            {
                return this.GetValIntByKey(ImgRunAttr.ImgW, 120);
            }
            set
            {
                this.SetValByKey(ImgRunAttr.ImgW, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ������ͼƬ
        /// </summary>
        public ImgRun()
        {
        }
        public ImgRun(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();

        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_ImgRun");
                map.EnType = EnType.Sys;
                map.EnDesc = "������ͼƬ";
                map.DepositaryOfEntity = Depositary.None;

                map.AddMyPK();

                map.AddTBString(ImgRunAttr.Title, null, "ʵ��������", false, true, 0, 200, 10);
                map.AddTBInt(ImgRunAttr.NumOfDtl, 0, "����", false, true);

                map.AddTBInt(ImgRunAttr.NumOfRec, 0, "������ʾ����", false, true);

                map.AddTBInt(ImgRunAttr.Speed, 0, "�ٶ�", false, true);

                map.AddTBInt(ImgRunAttr.ImgW, 0, "ͼƬ��", false, true);
                map.AddTBInt(ImgRunAttr.ImgH, 0, "ͼƬ��", false, true);

                map.AddTBString(ImgRunAttr.Tag, null, "Tag", false, true, 0, 20, 10);
                map.AddTBString(ImgRunAttr.Dir, null, "Dir����", false, true, 0, 20, 10);

                // map.AddTBInt(ImgRunAttr.ShowStyle, 0, "��ʾ����", false, true);

                map.AddTBStringDoc();

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        /// <summary>
        /// ����һ��ͼƬ�ַŹ���
        /// </summary>
        /// <param name="dtls">Ҫ���ŵ�����</param>
        /// <param name="title">����</param>
        /// <param name="speed">�ٶ�</param>
        public void MakeAddDtl(ImgRuns dtls, string title, int speed, int ImgH, int ImgW)
        {
        }
        public string GenerHtml_Pict(string dir, int speed)
        {
            dir = dir.ToLower();
            switch (dir)
            {
                case "left":
                case "right":
                    return GenerHtml_PictLeftRight(dir, speed);
                case "up":
                case "down":
                    return GenerHtml_PictUpDown(dir, speed);
                default:
                    break;
            }
            throw new Exception("sdsdsdsssssssssss");
        }
        public string GenerHtml_PictLeftRight(string dir, int speed)
        {
            ImgRunDtls dtls = new ImgRunDtls(this.MyPK);

            string html = "<marquee scrollamount='" + speed + "' direction='" + dir + "'>";

            #region  //����new table
            html += "<table border=0>";
            html += "<tr>";
            foreach (ImgRunDtl dtl in dtls)
            {
                html += "<TD  width='" + this.ImgW + "' > <a href='" + dtl.URLOfDtl + "' target=_blank >";
                if (dtl.IsImg)
                {
                    html += "<img src='" + dtl.URLOfFile + "'  width='" + this.ImgW + "' height='" + PubClass.GenerImgH(this.ImgW, dtl.MyFileH, dtl.MyFileW, this.ImgH) + "' border='0' />";
                }
                else
                {
                    if (dtl.MyFileExt.Length == 0)
                        html += "<img src='/Img/UNImg.gif'  width='" + this.ImgW + "' height='" + PubClass.GenerImgH(this.ImgW, dtl.MyFileH, dtl.MyFileW, this.ImgH) + "' border='0' />";
                    else
                        html += dtl.MyFileName + "<img src='/Images/Sys/File/" + dtl.MyFileExt + ".gif' border='0' />";
                }
                html += "<br > " + dtl.Title + "</A></TD>";
            }
            html += "<tr></Table></marquee>";
            #endregion endScript
            return html;
        }
        public string GenerHtml_PictUpDown(string dir, int speed)
        {
            ImgRunDtls dtls = new ImgRunDtls(this.MyPK);

            string html = "<marquee scrollamount='" + speed + "' direction='" + dir + "'>";

            #region  //����new table
            html += "<table border=0>";


            if (this.NumOfRec == 1)
            {
                foreach (ImgRunDtl dtl in dtls)
                {
                    html += "<TR>";
                    html += "<TD width='" + this.ImgW + "'>" + dtl.GenerHtml(this.ImgW, this.ImgH) + "</TD>";
                    html += "</TR>";
                }
            }
            else
            {
                int i = -1;
                foreach (ImgRunDtl dtl in dtls)
                {
                    i++;
                    if (i == 0)
                    {
                        html += "<TR>";
                        html += "<TD width='" + this.ImgW + "'>" + dtl.GenerHtml(this.ImgW, this.ImgH) + "</TD>";
                        continue;
                    }

                    if (i == this.NumOfRec - 1)
                    {
                        html += "<TD width='" + this.ImgW + "'>" + dtl.GenerHtml(this.ImgW, this.ImgH) + "</TD>";
                        html += "</TR>";
                        i = -1;
                        continue;
                    }
                    html += "<TD width='" + this.ImgW + "'>" + dtl.GenerHtml(this.ImgW, this.ImgH) + "</TD>";
                }
            }

            html += "</Table>";
            html += "</marquee>";
            #endregion endScript
            return html;
        }
        /// <summary>
        /// ���ɳ����黭��ʽ��ͼƬ
        /// </summary>
        /// <returns></returns>
        public string GenerHtml_Pict1_del()
        {
            ImgRunDtls dtls = new ImgRunDtls(this.MyPK);

            string html = "";

            html += "<div id='demo' style='style=overflow:hidden;height:500px;width:400px;color:#ffffff' >"; // ��ʼDemo 
 
            #region  // ����new table
            html += "<div id=demo1>";

          //  html += "<Table width='122'><TR>";
            foreach (ImgRunDtl dtl in dtls)
            {
              //  html += " <td align='center' valign='middle'>";
                html += "<br><img src='" + dtl.URLOfFile + "'  width='40' height='50' border='0' style='border:1px solid #666' />";
               // html += "<br><img src='" + dtl.URLOfFile + "'  width='" + this.ImgW + "' height='" + this.ImgH + "' border='0' style='border:1px solid #666' />";
                // html += "</td>";
                continue;

                //html += " <a href='" + dtl.URLOfDtl + "' target=_blank >";

                //if (dtl.IsImg)
                //{
                //    html += "<img src='" + dtl.URLOfFile + "'  width='" + this.ImgW + "' height='" + this.ImgH + "' border='0' style='border:1px solid #666' />";
                //}
                //else
                //{
                //    if (dtl.MyFileExt.Length == 0)
                //        html += dtl.DocHtmlS3;
                //    else
                //        html += dtl.MyFileName + "<img src='/Images/Sys/File/" + dtl.MyFileExt + ".gif' border='0' />";
                //}

                //html += "<br > " + dtl.Title + "</A>";
                //html += "</td>";
            }

           // html += "</tr>";
           // html += "</table>";

            html += "</div><div id=demo2></div> ";

        //    html += "<div id=demo2></div> ";
            html += "</div>"; // end DIV demo ;

            #endregion endScript

            #region script


            #endregion endScript
            return html;
        }
    }
    /// <summary>
    /// ������ͼƬs
    /// </summary>
    public class ImgRuns : Entities
    {
        /// <summary>
        /// ������ͼƬs
        /// </summary>
        public ImgRuns()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgRun();
            }
        }
    }
}


 