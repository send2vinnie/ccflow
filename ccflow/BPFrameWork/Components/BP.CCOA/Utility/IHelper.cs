using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CCOA.Utility
{
    /// <summary>
    /// 助手类接口
    /// </summary>
    public interface IHelper
    {
        string Name { get; set; }

        string Description { get; set; }

        //ObjectAssistant Assistant { get; set; }

        string Root { get; set; }
    }
}
