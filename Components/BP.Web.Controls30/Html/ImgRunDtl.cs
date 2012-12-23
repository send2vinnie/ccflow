using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// ��λ����
	/// </summary>
    public class ImgRunDtlAttr : EntityNoNameAttr
    {
        /// <summary>
        /// FK_Dept
        /// </summary>
        public const string FK_ImgRun = "FK_ImgRun";
        public const string Title = "Title";
        public const string URLOfDtl = "URLOfDtl";
        public const string URLOfFile = "URLOfFile";
        public const string Doc = "Doc";
        public const string MyFileName = "MyFileName";
        public const string MyFileExt = "MyFileExt";
        public const string MyFileW = "MyFileW";
        public const string MyFileH = "MyFileH";
        public const string Tag = "Tag";
    }
	/// <summary>
	/// ��λ����
	/// </summary>
    public class ImgRunDtl : EntityOID
    {
        #region  ����
        public string GenerHtml(int imgW, int imgH)
        {

            string html = "<a href='" + this.URLOfDtl + "' target=_blank >";

            if (this.IsImg)
            {
                html += "<img src='" + this.URLOfFile + "'  width='" + imgW + "' height='" + PubClass.GenerImgH(imgW, this.MyFileH, this.MyFileW, imgH) + "' border='0' />";
            }
            else
            {
                if (this.MyFileExt.Length == 0)
                    html += "<img src='/Img/UNImg.gif' width='" + imgW + "' height='" + PubClass.GenerImgH(imgW, this.MyFileH, this.MyFileW, imgH) + "' border='0' />";
                else
                    html += this.MyFileName + "<img src='/Images/Sys/File/" + this.MyFileExt + ".gif' border='0' />";
            }
            html += "<br> " + this.Title + "</A>";
            return html;
        }
        /// <summary>
        ///  �������ű��
        /// </summary>
        public string FK_ImgRun
        {
            get
            {
                return this.GetValStringByKey(ImgRunDtlAttr.FK_ImgRun);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.FK_ImgRun, value);
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey(ImgRunDtlAttr.Title);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.Title, value);
            }
        }
        public string URLOfDtl
        {
            get
            {
                return this.GetValStringByKey(ImgRunDtlAttr.URLOfDtl);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.URLOfDtl, value);
            }
        }
        public string URLOfFile
        {
            get
            {
                return this.GetValStringByKey(ImgRunDtlAttr.URLOfFile);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.URLOfFile, value);
            }
        }


        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey(ImgRunDtlAttr.MyFileExt);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.MyFileExt, value);
            }
        }

        public string MyFileName
        {
            get
            {
                return this.GetValStringByKey(ImgRunDtlAttr.MyFileName);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.MyFileName, value);
            }
        }

        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey(ImgRunDtlAttr.MyFileW);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.MyFileW, value);
            }
        }

        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey(ImgRunDtlAttr.MyFileH);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.MyFileH, value);
            }
        }

        public bool IsImg
        {
            get
            {
                return DataType.IsImgExt(this.MyFileExt); 
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValHtmlStringByKey(ImgRunDtlAttr.Doc);
            }
        }

        public int Tag
        {
            get
            {
                return this.GetValIntByKey(ImgRunDtlAttr.Tag);
            }
            set
            {
                SetValByKey(ImgRunDtlAttr.Tag, value);
            }
        }

        public string DocHtmlS3
        {
            get
            {
                string doc = this.Doc;
                if (doc.Length > 50)
                    doc = doc.Substring(0, 49) + "...";
                return doc;
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ��λ����
        /// </summary>
        public ImgRunDtl() { }
        public ImgRunDtl(int no)
            : base(no)
        { }

        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_ImgRunDtl");

                map.EnDesc = "ͼƬ��ϸ";

                map.EnType = EnType.App;
                map.DepositaryOfMap = Depositary.Application;
                map.AddTBIntPKOID();

                map.AddTBString(ImgRunDtlAttr.FK_ImgRun, null, "����PK", true, false, 0, 20, 100);
                map.AddTBString(ImgRunDtlAttr.Title, null, "����", true, false, 0, 200, 100);
                map.AddTBString(ImgRunDtlAttr.URLOfDtl, null, "���ӵ�", true, false, 0, 200, 100);
                map.AddTBString(ImgRunDtlAttr.URLOfFile, null, "���ӵ�", true, false, 0, 200, 100);

                map.AddTBInt(ImgRunDtlAttr.Tag, 0, "Tag", true, false);
                map.AddTBStringDoc(ImgRunDtlAttr.Doc, null, "����", true, false);
                map.AddMyFile();
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��λ���ͼ���
	/// </summary>

    public class ImgRunDtls : EntitiesNoName
    {
        public ImgRunDtls(string FK_ImgRun)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(ImgRunDtlAttr.FK_ImgRun, FK_ImgRun);
            qo.DoQuery();
        }
        /// <summary>
        /// GetNewEntity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgRunDtl();
            }
        }
        /// <summary>
        /// ��λ���ͼ���()
        /// </summary>
        public ImgRunDtls() { }
    }
}
 