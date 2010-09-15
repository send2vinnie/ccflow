using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    public enum BadWordDealWay
    {
        /// <summary>
        /// 禁止提交
        /// </summary>
        UnEableSubmit,
        /// <summary>
        /// 替换字串
        /// </summary>
        ReplaceKey,
        /// <summary>
        /// 不处理
        /// </summary>
        Pass
    }
    /// <summary>
    /// 屏蔽的关键字
    /// </summary>
    public class BadWordAttr : EntityNoNameAttr
    {
        public const string ReplaceWord = "ReplaceWord";
        public const string BadWordDealWay = "BadWordDealWay";
    }
    /// <summary>
    /// 屏蔽的关键字
    /// </summary>
    public class BadWord : EntityNoName
    {
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }


        #region 基本属性
        public string ReplaceWord
        {
            get
            {
                return this.GetValStringByKey(BadWordAttr.ReplaceWord);
            }
            set
            {
                this.SetValByKey(BadWordAttr.ReplaceWord, value);
            }
        }
        public BadWordDealWay BadWordDealWay
        {
            get
            {
                return (BadWordDealWay)this.GetValIntByKey(BadWordAttr.BadWordDealWay);
            }
            set
            {
                this.SetValByKey(BadWordAttr.BadWordDealWay, value);
            }
        }
        #endregion

        /// <summary>
        /// 屏蔽的关键字
        /// </summary>
        public BadWord(string no)
            : base(no)
        {
        }
        /// <summary>
        /// 屏蔽的关键字
        /// </summary>
        public BadWord()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("GE_BadWord");
                map.EnType = EnType.Sys;
                map.EnDesc = "屏蔽的关键字";
                map.DepositaryOfEntity = Depositary.None;
                map.IsAutoGenerNo = true;
                map.IsAllowRepeatName = false;
                map.CodeStruct = "3";

                map.AddTBStringPK(BadWordAttr.No, null, "编号", true, true, 3, 3, 3);
                map.AddTBString(BadWordAttr.Name, null, "字", true, false, 1, 100, 10);
                map.AddDDLSysEnum(BadWordAttr.BadWordDealWay, 0, "处理方式", true, true, BadWordAttr.BadWordDealWay,
                    "@0=禁止提交@2=替换字串@3=不处理");

                map.AddTBString(BadWordAttr.ReplaceWord, null, "替换的字串", true, false, 0, 100, 10);
                //  map.AddTBString(BadWordAttr.AlertMsg, null, "提示的信息", true, false, 0, 100, 10);
                map.AddSearchAttr(BadWordAttr.BadWordDealWay);
                //  map.AddTBString(BadWordAttr.BadWordDealWay, null, "屏蔽次数", true, false);
                //  map.AddTBStringDoc(BadWordAttr.ReplaceWord, null, "来源IP", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    /// <summary>
    /// 屏蔽的关键字s
    /// </summary>
    public class BadWords : EntitiesNoName
    {
        /// <summary>
        /// 屏蔽的关键字s
        /// </summary>
        public BadWords()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new BadWord();
            }
        }
    }
}
