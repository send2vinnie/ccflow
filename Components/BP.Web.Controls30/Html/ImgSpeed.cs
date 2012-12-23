using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// Img�����ٶ�
	/// </summary>
    public class ImgSpeedAttr : EntityNoNameAttr
    {
        /// <summary>
        /// FK_Dept
        /// </summary>
        public const string FK_Dept = "FK_Dept";
    }
	/// <summary>
	/// Img�����ٶ�
	/// </summary>
	public class ImgSpeed : EntityNoName
	{
		#region  ����
		/// <summary>
		///  �������ű��
		/// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(ImgSpeedAttr.FK_Dept);
			}
			set
			{
				SetValByKey(ImgSpeedAttr.FK_Dept,value);
			}
		}		
		#endregion 
		 
		#region ���캯��
		/// <summary>
		/// Img�����ٶ�
		/// </summary>
		public ImgSpeed(){}
		/// <summary>
		/// Img�����ٶ�
		/// </summary>
		/// <param name="_No"></param>
		public ImgSpeed(string _No) :base(_No)
		{ 
		}
		/// <summary>
		/// Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_ImgSpeed");
                map.EnDesc = "Img�����ٶ�";

                map.EnType = EnType.App;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;

                map.AddTBStringPK(ImgSpeedAttr.No, null, "���", true, false, 4, 20, 100);
                map.AddTBString(ImgSpeedAttr.Name, null, "����", true, false, 0, 50, 200);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

	}
	/// <summary>
	/// Img�����ٶȼ���
	/// </summary>
    public class ImgSpeeds : EntitiesNoName
    {
        /// <summary>
        /// GetNewEntity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ImgSpeed();
            }
        }
        /// <summary>
        /// Img�����ٶȼ���()
        /// </summary>
        public ImgSpeeds() { }
    }
}
 