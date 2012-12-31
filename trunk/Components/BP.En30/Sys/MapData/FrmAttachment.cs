using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// �����ϴ�����
    /// </summary>
    public enum AttachmentUploadType
    {
        /// <summary>
        /// ������
        /// </summary>
        Single,
        /// <summary>
        /// �����
        /// </summary>
        Multi,
        /// <summary>
        /// ָ����
        /// </summary>
        Specifically
    }
    /// <summary>
    /// ����
    /// </summary>
    public class FrmAttachmentAttr : EntityMyPKAttr
    {
        /// <summary>
        /// Name
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// X
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// Y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// ���
        /// </summary>
        public const string W = "W";
        /// <summary>
        /// �߶�
        /// </summary>
        public const string H = "H";
        /// <summary>
        /// Ҫ���ϴ��ĸ�ʽ
        /// </summary>
        public const string Exts = "Exts";
        /// <summary>
        /// �������
        /// </summary>
        public const string NoOfObj = "NoOfObj";
        /// <summary>
        /// �Ƿ�����ϴ�
        /// </summary>
        public const string IsUpload = "IsUpload";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsNote = "IsNote";
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public const string IsDelete = "IsDelete";
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public const string IsDownload = "IsDownload";
        /// <summary>
        /// ���浽
        /// </summary>
        public const string SaveTo = "SaveTo";
        /// <summary>
        /// ���
        /// </summary>
        public const string Sort = "Sort";
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public const string UploadType = "UploadType";
        /// <summary>
        /// RowIdx
        /// </summary>
        public const string RowIdx = "RowIdx";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// �Զ����ƴ�С
        /// </summary>
        public const string IsAutoSize = "IsAutoSize";
    }
    /// <summary>
    /// ����
    /// </summary>
    public class FrmAttachment : EntityMyPK
    {
        #region ����
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public AttachmentUploadType UploadType
        {
            get
            {
                return (AttachmentUploadType)this.GetValIntByKey(FrmAttachmentAttr.UploadType);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.UploadType, (int)value);
            }
        }
        /// <summary>
        /// �Ƿ�����ϴ�
        /// </summary>
        public bool IsUpload
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsUpload);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsUpload, value);
            }
        }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsDownload
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsDownload);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsDownload, value);
            }
        }
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public bool IsDelete
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsDelete);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsDelete, value);
            }
        }
        /// <summary>
        /// �Զ����ƴ�С
        /// </summary>
        public bool IsAutoSize
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsAutoSize);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsAutoSize, value);
            }
        }
        /// <summary>
        /// ��ע��
        /// </summary>
        public bool IsNote
        {
            get
            {
                return this.GetValBooleanByKey(FrmAttachmentAttr.IsNote);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.IsNote, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Name);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Name, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Sort
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Sort);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Sort, value);
            }
        }
        /// <summary>
        /// Ҫ��ĸ�ʽ
        /// </summary>
        public string Exts
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.Exts);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Exts, value);
            }
        }
        public string SaveTo
        {
            get
            {
                string s = this.GetValStringByKey(FrmAttachmentAttr.SaveTo);
                if (s == "" || s == null)
                    s = SystemConfig.PathOfDataUser + @"\UploadFile\" + this.FK_MapData + "\\";
                return s;
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.SaveTo, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string NoOfObj
        {
            get
            {
                return this.GetValStringByKey(FrmAttachmentAttr.NoOfObj);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.NoOfObj, value);
            }
        }
        /// <summary>
        /// Y
        /// </summary>
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.Y);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float X
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.X);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.X, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float W
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.W);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.W, value);
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float H
        {
            get
            {
                return this.GetValFloatByKey(FrmAttachmentAttr.H);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.H, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(FrmAttachmentAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.RowIdx, value);
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(FrmAttachmentAttr.GroupID);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.GroupID, value);
            }
        }
        /// <summary>
        /// FK_MapData
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(FrmAttachmentAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmAttachmentAttr.FK_MapData, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public FrmAttachment()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="mypk"></param>
        public FrmAttachment(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_FrmAttachment");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "����";
                map.EnType = EnType.Sys;
                map.AddMyPK();

                map.AddTBString(FrmAttachmentAttr.FK_MapData, null,"FK_MapData", true, false, 1, 30, 20);
                map.AddTBString(FrmAttachmentAttr.NoOfObj, null, "�������", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentAttr.Name, null,"����", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentAttr.Exts, null, "Ҫ���ϴ��ĸ�ʽ", true, false, 0, 50, 20);
                map.AddTBString(FrmAttachmentAttr.SaveTo, null, "���浽", true, false, 0, 150, 20);
                map.AddTBString(FrmAttachmentAttr.Sort, null, "���(��Ϊ��)", true, false, 0, 500, 20);

                map.AddTBFloat(FrmAttachmentAttr.X, 5, "X", true, false);
                map.AddTBFloat(FrmAttachmentAttr.Y, 5, "Y", false, false);
                map.AddTBFloat(FrmAttachmentAttr.W, 40, "TBWidth", false, false);
                map.AddTBFloat(FrmAttachmentAttr.H, 150, "H", false, false);

                map.AddBoolean(FrmAttachmentAttr.IsUpload, true, "�Ƿ�����ϴ�", false, false);
                map.AddBoolean(FrmAttachmentAttr.IsDelete, true, "�Ƿ����ɾ��", false, false);
                map.AddBoolean(FrmAttachmentAttr.IsDownload, true, "�Ƿ��������", false, false);

                map.AddBoolean(FrmAttachmentAttr.IsAutoSize, true, "�Զ����ƴ�С", false, false);
                map.AddBoolean(FrmAttachmentAttr.IsNote, true, "�Ƿ����ӱ�ע", false, false);

                map.AddTBInt(FrmAttachmentAttr.UploadType, 0, "�ϴ�����0����1���2ָ��", false, false);

                map.AddTBInt(FrmAttachmentAttr.RowIdx, 0, "RowIdx", false, false);
                map.AddTBInt(FrmAttachmentAttr.GroupID, 0, "GroupID", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public bool IsUse = false;
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_MapData + "_" + this.NoOfObj;
            return base.beforeUpdateInsertAction();
        }
    }
    /// <summary>
    /// ����s
    /// </summary>
    public class FrmAttachments : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ����s
        /// </summary>
        public FrmAttachments()
        {
        }
        /// <summary>
        /// ����s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmAttachments(string fk_mapdata)
        {
            this.Retrieve(FrmAttachmentAttr.FK_MapData, fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmAttachment();
            }
        }
        #endregion
    }
}
