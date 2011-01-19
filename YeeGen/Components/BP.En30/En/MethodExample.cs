using System; 
using System.Collections;
using BP.DA; 
using BP.Web.Controls;
using System.Reflection;
using BP.Port;


namespace BP.En
{
	/// <summary>
	/// Method 的摘要说明
	/// </summary>
    public class MethodExample1 : Method
    {
        /// <summary>
        /// 不带有参数的方法
        /// </summary>
        public MethodExample1()
        {
            this.Title = "修改密码";
            this.Help = "修改自己的登录密码，为了您的数据安全，建议您定期的修改密码，密码不要过于简单。";
        }
        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            this.Warning = "您确定要执行吗？";
            HisAttrs.AddTBString("P1", null, "原密码", true, false, 0, 10, 10);
            HisAttrs.AddTBString("P2", null, "新密码", true, false, 0, 10, 10);
            HisAttrs.AddTBString("P3", null, "确认", true, false, 0, 10, 10);
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
            string p1 = this.GetValStrByKey("P1");
            string p2 = this.GetValStrByKey("P2");
            string p3 = this.GetValStrByKey("P3");

            if (p2 != p3)
                return "新密码不一致。";

            Emp emp = new Emp();
            emp.No = BP.Web.WebUser.No;
            if (emp.Pass == p1)
            {
                emp.Update("Pass",p2);
                return "执行成功，请记好您的新密码。";
            }
            else
            {
                return "老密码不正确。";
            }
        }
    }
}
