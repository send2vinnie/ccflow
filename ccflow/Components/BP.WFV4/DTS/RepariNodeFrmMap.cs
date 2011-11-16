using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.Sys;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// 修复节点表单map 的摘要说明
    /// </summary>
    public class RepariNodeFrmMap : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public RepariNodeFrmMap()
        {
            this.Title = "修复节点表单";
            this.Help = "检查节点表单系统字段是否被非法删除，如果非法删除自动增加上它，这些字段包括:Rec,Title,OID,FID,NodeState,WFState,RDT,CDT";
        }
        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "您确定要执行吗？";
            //HisAttrs.AddTBString("P1", null, "原密码", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "新密码", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "确认", true, false, 0, 10, 10);
        }
        /// <summary>
        /// 当前的操纵员是否可以执行这个方法
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <returns>返回执行结果</returns>
        public override object Do()
        {
            Nodes nds = new Nodes();
            nds.RetrieveAllFromDBSource();

            string info = "";
            foreach (Node nd in nds)
            {
                string msg = "";
                Work wk = nd.HisWork;
                if (wk.EnMap.Attrs.Contains("Rec") == false)
                {
                    msg += "@字段Rec被非法删除了.";
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.Rec;
                    if (nd.IsStartNode == false)
                        attr.Name = BP.Sys.Language.GetValByUserLang("Rec", "记录人"); // "记录人";
                    else
                        attr.Name = BP.Sys.Language.GetValByUserLang("Sponsor", "发起人"); //"发起人";

                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MaxLen = 20;
                    attr.MinLen = 0;
                    attr.DefVal = "@WebUser.No";
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains("RDT") == false)
                {
                    msg += "@字段CDT被非法删除了.";
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.RDT;
                    if (nd.IsStartNode)
                        attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "发起时间"); //"发起时间";
                    else
                        attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "完成时间"); //"完成时间";

                    attr.MyDataType = BP.DA.DataType.AppDateTime;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "@RDT";
                    attr.Tag = "1";
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains("CDT") == false)
                {
                    msg += "@字段CDT被非法删除了.";
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.CDT;
                    if (nd.IsStartNode)
                        attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "发起时间"); //"发起时间";
                    else
                        attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "完成时间"); //"完成时间";
                    attr.MyDataType = BP.DA.DataType.AppDateTime;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "@RDT";
                    attr.Tag = "1";
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains(StartWorkAttr.NodeState) == false)
                {
                    msg += "@开始节点字段NodeState被非法删除了.";
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.KeyOfEn = WorkAttr.NodeState;
                    attr.Name = BP.Sys.Language.GetValByUserLang("NodeState", "节点状态"); //"节点状态";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.DefVal = "0";
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains(StartWorkAttr.FK_Dept) == false)
                {
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = StartWorkAttr.FK_Dept;
                    attr.Name = BP.Sys.Language.GetValByUserLang("OperDept", "操作员部门"); //"操作员部门";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.DDL;
                    attr.LGType = FieldTypeS.FK;
                    attr.UIBindKey = "BP.Port.Depts";
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 20;
                    attr.Insert();
                }


                if (wk.EnMap.Attrs.Contains(StartWorkAttr.Emps) == false)
                {
                    msg += "@开始节点字段Emps被非法删除了.";
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.Emps;
                    attr.Name = StartWorkAttr.Emps;
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MaxLen = 400;
                    attr.MinLen = 0;
                    attr.Insert();
                }

                if (nd.IsStartNode)
                {
                    if (wk.EnMap.Attrs.Contains("Title") == false)
                    {
                        msg += "@开始节点字段Title被非法删除了.";
                        MapAttr attr = new BP.Sys.MapAttr();
                        attr.FK_MapData = "ND" + nd.NodeID;
                        attr.HisEditType = BP.En.EditType.UnDel;
                        attr.KeyOfEn = "Title";
                        attr.Name = BP.Sys.Language.GetValByUserLang("Title", "标题"); // "流程标题";
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = false;
                        attr.UIIsEnable = false;
                        attr.UIIsLine = true;
                        attr.UIWidth = 251;

                        attr.MinLen = 0;
                        attr.MaxLen = 200;
                        attr.IDX = -100;
                        attr.X = (float)174.83;
                        attr.Y = (float)54.4;
                        attr.Insert();
                    }

                    if (wk.EnMap.Attrs.Contains(StartWorkAttr.WFState) == false)
                    {
                        msg += "@开始节点字段WFState被非法删除了.";
                        MapAttr attr = new BP.Sys.MapAttr();
                        attr.FK_MapData = "ND" + nd.NodeID;
                        attr.HisEditType = BP.En.EditType.Readonly;
                        attr.KeyOfEn = StartWorkAttr.WFState;
                        attr.DefVal = "0";
                        attr.Name = BP.Sys.Language.GetValByUserLang("FlowState", "流程状态"); //"流程状态";
                        attr.MyDataType = BP.DA.DataType.AppInt;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIBindKey = attr.KeyOfEn;
                        attr.UIVisible = false;
                        attr.UIIsEnable = false;
                        attr.Insert();
                    }
                }

                if (nd.FocusField != "")
                {
                    if (wk.EnMap.Attrs.Contains(nd.FocusField ) == false)
                    {
                        msg += "@焦点字段"+nd.FocusField+" 被非法删除了.";
                    }
                }

                if (msg != "")
                {
                    info += "<b>对流程" + nd.FlowName + ",节点("+nd.NodeID+")(" + nd.Name + "), 检查结果如下:</b>" + msg + "<hr>";
                }
            }
            return info + "执行完成...";
        }
    }
}
