using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using System.Resources;
using System.ComponentModel;


using BP.En;
using BP.En ;
using BP.Port;


namespace BP.Win32.Controls
{
	
	[ToolboxBitmap(typeof(System.Windows.Forms.TreeView))]
	public class Tree : System.Windows.Forms.TreeView
	{
		#region 变量
		public Entities HisEns=null;
		#endregion 

		public Tree()
		{
		}

		protected void beforeBind()
		{
		}
		protected void endBind()
		{
		}
		/// <summary>
		/// 加载实体集
		/// </summary>
		/// <param name="ens">实体集</param>
		/// <param name="refText">相关的文本</param>
		/// <param name="refKey">相关的值</param>
		/// <param name="CheckBoxes">是否加复选框</param>
		public void BindEns(Entities ens, string refText, string refKey,bool CheckBoxes)
		{
			this.HisEns = ens ;
			if (ens.IsGradeEntities)
			{
				this.BindEns((GradeEntitiesNoNameBase)ens,CheckBoxes) ;
				return ;
			}			
			this.CheckBoxes = CheckBoxes ;
			this.beforeBind();
			
			foreach(Entity en in ens)
			{
				this.Nodes.Add( new Node(en.GetValStringByKey(refText),en.GetValStringByKey(refKey),en)) ;
			}
			this.endBind();
		}
		/// <summary>
		/// 加载实体集（第一级）
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="CheckBoxes"></param>
		public void BindEns(GradeEntitiesNoNameBase ens, bool CheckBoxes)
		{
			this.HisEns = ens ;

			this.CheckBoxes = CheckBoxes ;
			this.beforeBind();

			foreach(GradeEntityNoNameBase en  in ens)
			{				
				if (en.Grade==1)
				{
					Node nd1 = new Node();
					nd1.Text = en.Name ;
					nd1.Value = en.No;
					nd1.Tag =en;
					nd1.Grade=1 ; 		
					//nd1.ExpandAll() ;
					nd1.Expand();
					this.Nodes.Add(nd1);
					if (en.IsDtl)
					continue;
					this.NodeAdd(nd1,ens,en);
				}
			}

			
			this.endBind();

			foreach(Node nd in this.Nodes)
			{
				this.ExpandIt(nd);
				nd.ExpandAll();
			}
		}
		public void ExpandIt(Node nd)
		{
			foreach(Node mynd in nd.Nodes)
			{
				mynd.ExpandAll();
			}
		}
		/// <summary>
		///加载节点 
		/// </summary>
		/// <param name="nd"></param>
		/// <param name="ens"></param>
		/// <param name="en"></param>
		protected void NodeAdd(Node nd, GradeEntitiesNoNameBase ens,GradeEntityNoNameBase en )
		{
			if (en.IsDtl)
				return ;
			//Node childNode = new Node();
			foreach(GradeEntityNoNameBase childen in ens)
			{
				if (childen.Grade ==en.Grade+1 && childen.NoOfParent ==en.No )
				{					
					Node nd1 = new Node();
					nd1.Text = childen.Name ;
					nd1.Value = childen.No;
					nd1.Tag =childen;
					nd1.Grade=childen.Grade ;
					nd1.ExpandAll() ;
					nd1.Expand();
					nd.Nodes.Add(nd1) ;				 
					if (en.IsDtl)
					continue;
					this.NodeAdd(nd1,ens,childen);					
				}
			}
		}
		/// <summary>
		/// 获得当前选中实体集
		/// </summary>
		/// <returns></returns>
		public Entities GetCurrentSelectedEns()
		{
			Entities ens=this.HisEns.CreateInstance();//创建实体
			this.GetCurrentSelectedEns(this.Nodes,ens) ;
			return ens;
		}
		protected void GetCurrentSelectedEns(TreeNodeCollection nds, Entities ens)
		{
			foreach(Node nd in nds)
			{
				if (nd.Checked)
				{
					ens.AddEntity( (Entity)nd.Tag );
				}
				GetCurrentSelectedEns(nd.Nodes,ens);
			}
		}

		/// <summary>
		/// 设置选中实体状态
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="refKey"></param>
		public void SetChecked(Entities ens,  string refKey)
		{
			SetChecked(this.Nodes,ens,refKey);
		}
		protected void SetChecked(TreeNodeCollection nds , Entities ens,  string refKey)
		{
			if (nds.Count==0)
				return ;
			foreach(Node nd in nds)
			{
				foreach(Entity en in ens)
				{
					if ( nd.Value==en.GetValStringByKey(refKey) )					 
						nd.Checked =true;
				}
				SetChecked(nd.Nodes,ens,refKey);
			}
		}
		
		#region 增加
		
		/// <summary>
		///  返回节点的目录路径。
		/// </summary>
		/// <param name="node"></param>
		/// <returns>节点的目录路径。</returns>
		private string GetPathFromNode(TreeNode node) 
		{
			if (node.Parent == null) 
			{
				return node.Text;
			}
			return Path.Combine(GetPathFromNode(node.Parent), node.Text);
		}		
		/// <summary>
		/// 刷新以获取给定节点下的所有已展开的节点。
		/// </summary>
		/// <param name="Node"></param>
		/// <param name="ExpandedNodes"></param>
		/// <param name="StartIndex"></param>
		/// <returns></returns>
		private int Refresh_GetExpanded(TreeNode Node, string[] ExpandedNodes, int StartIndex) 
		{

			if (StartIndex < ExpandedNodes.Length) 
			{
				if (Node.IsExpanded) 
				{
					ExpandedNodes[StartIndex] = Node.Text;
					StartIndex++;
					for (int i = 0; i < Node.Nodes.Count; i++) 
					{
						StartIndex = Refresh_GetExpanded(Node.Nodes[i],
							ExpandedNodes,
							StartIndex);
					}
				}
				return StartIndex;
			}
			return -1;
		}	
	

		DataSet ds=new DataSet();
		// 递归添加树的节点
		public void AddTree(int ParentID,TreeNode pNode) 
		{
			DataView dvTree = new DataView(ds.Tables[0]);
			//过滤ParentID,得到当前的所有子节点
			dvTree.RowFilter =  "[PARENTID] = " + ParentID;
			foreach(DataRowView Row in dvTree) 
			{
				if(pNode == null) 
				{    //'?添加根节点
					//					TreeNode Node = treeView1.Nodes.Add(Row["ConText"].ToString());
					//					AddTree(Int32.Parse(Row["ID"].ToString()),Node);    //再次递归
				} 
				else 
				{   //添加当前节点的子节点
					TreeNode Node =  pNode.Nodes.Add(Row["ConText"].ToString());
					AddTree(Int32.Parse(Row["ID"].ToString()),Node);     //再次递归
				}
			}
		}
		#endregion
	}
	/// <summary>
	/// 树上的节点
	/// </summary>
	public class Node :TreeNode
	{
		/// <summary>
		/// 值
		/// </summary>
		public string Value=null;
		/// <summary>
		///级别
		/// </summary>
		public int Grade=0;

		public Node()
		{
			 
		}
		/// <summary>
		///树上的节点 
		/// </summary>
		/// <param name="text">节点标签中显示的文本</param>
		/// <param name="val">值</param>
		/// <param name="en">对象</param>
		public Node(string text, string val, Entity en)
		{
			this.Text = text;
			this.Value = val;
			this.Tag = en;
			
		}
		/// <summary>
		///树上的节点 
		/// </summary>
		/// <param name="text">节点标签中显示的文本</param>
		/// <param name="val">值</param>
		public Node(string text, string val )
		{
			this.Text = text;
			this.Value = val;			 
		}
	}
	 
}
