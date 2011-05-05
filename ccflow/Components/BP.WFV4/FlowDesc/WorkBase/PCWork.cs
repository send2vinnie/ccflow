
using System;
using System.Collections;
using System.Data ; 
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Web; 

namespace BP.WF
{

	/// <summary>
	/// 计算机工作基类属性
	/// </summary>
	public class PCWorkAttr:WorkAttr
	{		 
	}
	/// <summary>	 
	/// 计算机工作基类,所有计算机工作都要从这里继承
	/// 这个工作的特点是,
	/// 1,计算机通过调度获取外部的属性.
	/// 2,产生的消息不加入消息列表.
	/// 3,完成这个工作的人员是系统自动的随机的取出.
	/// 4,
	/// </summary>
	abstract public class PCWork : Work, IDTS
	{
		#region 基本属性		 
		#endregion

		#region 需要子类实现的方法
		/// <summary>
		/// 执行获取外部数据
		/// </summary>
		public abstract void InitData();
		
		#endregion

		#region 构造函数
		/// <summary>
		/// 工作流程
		/// </summary>
		protected PCWork()
		{
		}
		/// <summary>
		/// 工作流程
		/// </summary>
		/// <param name="wfID">工作流程的ID</param>
        protected PCWork(Int64 wfID)
            : base(wfID)
		{
		}		 
		#endregion

		#region   beforeUpdate 
		protected override bool beforeUpdate()
		{
			this.InitData();
			return base.beforeUpdate ();
		}
		#endregion

		 
	}
	/// <summary>
	/// 工作流程采集信息的基类 集合
	/// </summary>
	abstract public class PCWorks : Works
	{
		#region 扩展属性
		
		#endregion

		#region 构造方法
		/// <summary>
		/// 信息采集基类
		/// </summary>
		public PCWorks()
		{
		}
		#endregion 

		#region 公共方法
		/// <summary>
		/// 执行初始化数据
		/// </summary>
		public  void DoInitData()
		{
			//PCWorks ens = new PCWorks();
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkAttr.NodeState ,(int) NodeState.Init);
			if (qo.DoQuery()==0)
				return;

			string currEmpId= BP.Web.WebUser.No;
			bool isAu = BP.Web.WebUser.IsAuthorize ; 
			int myempid=0;
		 	foreach(PCWork wk in this)
			{
				try
				{
					
					string empid = wk.Recorder ; //取出当前的工作人员。

					if (Web.WebUser.No!=empid)
					{
						/* 如果当前的人员与 查找到的人员不相等. */
						Web.WebUser.Exit();
						Emp mp= new Emp( empid ) ; 
						BP.Web.WebUser.SignInOfWFQH(mp,false);
					}

					wk.Update();
					wk.InitData();


					Node nd = new Node( this.ToString()  ) ;
					WorkNode wn = new WorkNode(wk,nd);
                    string msg = wn.AfterNodeSave(false, false, DateTime.Now);
					 
					Log.DefaultLogWriteLineInfo( msg );
				}
				catch(Exception ex)
				{
					Log.DefaultLogWriteLineError(ex.Message+" : User="+WebUser.No);
				}
			}

			Web.WebUser.Exit();
			Emp emp = new Emp(currEmpId); //当前的人员在登录进来.
			BP.Web.WebUser.SignInOfWFQH(emp,false);
			return ;
		}
		#endregion 
	}
}
