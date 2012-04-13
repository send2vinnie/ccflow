using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.CCOA
{
    public class ArticleTypeAttr : EntityNoNameAttr
    {
        public const string Description = "Description";
    }
    public class ArticleType : EntityNoName
    {
        public string Description
        {
            get
            {
                return this.GetValStringByKey(ArticleTypeAttr.Description);
            }
            set
            {
                this.SetValByKey(ArticleTypeAttr.Description, value);
            }
        }

        #region 构造方法

        public ArticleType()
        {
        }

        public ArticleType(string no)
        {
            this.No = no;
            this.Retrieve();
        }

        #endregion

        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("OA_ArticleType");
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "文章类型";
                map.IsAutoGenerNo = true;

                map.AddTBIntPK(ArticleTypeAttr.No, 1, "主键NO", true, false);
                map.AddTBString(ArticleTypeAttr.Name, null, "类型名称", true, true, 0, 20, 20);
                map.AddTBString(ArticleTypeAttr.Description, null, "类型描述", true, true, 0, 100, 100);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
    public class ArticleTypes : Entities
    {

        public override Entity GetNewEntity
        {
            get
            {
                return new ArticleType();
            }
        }
    }
}
