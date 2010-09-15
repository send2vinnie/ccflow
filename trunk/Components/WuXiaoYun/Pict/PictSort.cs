

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 属性
    /// </summary>
	public class PictSortAttr:EntityNoNameAttr
	{
		 public const string FK_Dept="FK_Dept";
	}
	/// <summary>
	/// 图片类别
	/// </summary>
    public class PictSort : EntityNoName
    {
        #region 构造函数

        /// <summary>
        /// 图片类别
        /// </summary>
        public PictSort()
        {

        }

        public PictSort(string no)
        {
            this.No = no;
            try
            {
                this.Retrieve();
            }
            catch
            {

            }
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_PictSort");
                map.EnDesc = "图片类别";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Picts' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.Picts", "AppName") + "</a>";

                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(PictSortAttr.No, null, "编号", true, true, 2, 2, 2);
                map.AddTBString(PictSortAttr.Name, null, "名称", true, false, 0, 50, 300);
                //     map.AddTBString(PictSortAttr.FK_Dept, null, "分组", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 图片类别 
	/// </summary>
	public class PictSorts: EntitiesNoName
	{
		/// <summary>
		/// 获取图片类别
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PictSort();
			}
		}

		#region 构造函数		
		/// <summary>
		/// 图片类别
		/// </summary>
		public PictSorts()
		{
		}		 
		#endregion
		 
		
	}
}
 

