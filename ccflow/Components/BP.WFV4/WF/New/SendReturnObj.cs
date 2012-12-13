using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.WF
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum SendReturnMsgType
    {
        /// <summary>
        /// 消息
        /// </summary>
        Info,
        /// <summary>
        /// 系统消息
        /// </summary>
        SystemMsg
    }
    /// <summary>
    /// 消息标记
    /// </summary>
    public class SendReturnMsgFlag
    {
        public const string a = "";
    }
    /// <summary>
    /// 工作发送返回对象
    /// </summary>
    public class SendReturnObj
    {
        /// <summary>
        /// 消息标记
        /// </summary>
        public string MsgFlag = null;
        /// <summary>
        /// 消息类型
        /// </summary>
        public SendReturnMsgType HisSendReturnMsgType = SendReturnMsgType.Info;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg = null;
        /// <summary>
        /// 消息内容Html
        /// </summary>
        public string MsgOfHtml = null;
        /// <summary>
        /// 发送消息
        /// </summary>
        public SendReturnObj()
        {
        }
    }
    /// <summary>
    /// 工作发送返回对象集合.
    /// </summary>
    public class SendReturnObjs:System.Collections.CollectionBase
    {
        /// <summary>
        /// 输出的消息
        /// </summary>
        public string OutMessageText = null;
        public string OutMessageHtml = null;

        /// <summary>
        /// 增加消息
        /// </summary>
        /// <param name="msgFlag">消息标记</param>
        /// <param name="msg">文本消息</param>
        /// <param name="msgOfHtml">html消息</param>
        /// <param name="type">消息类型</param>
        public void AddMsg(string msgFlag, string msg, string msgOfHtml, SendReturnMsgType type)
        {
            SendReturnObj obj = new SendReturnObj();
            obj.MsgFlag = msgFlag;
            obj.Msg = msg;
            obj.MsgOfHtml = msgOfHtml;
            obj.HisSendReturnMsgType = type;
            this.InnerList.Add(obj);
        }
        /// <summary>
        /// 转化成text方式的消息，以方便识别不出来html的设备输出.
        /// </summary>
        /// <returns></returns>
        public string ToMsgOfText()
        {
            if (this.OutMessageText != null)
                return this.OutMessageText;
            string msg = "";
            foreach (SendReturnObj item in this)
            {
                if (item.HisSendReturnMsgType == SendReturnMsgType.SystemMsg)
                    continue;

                if (item.Msg != null)
                {
                    msg += "@" + item.Msg;
                    continue;
                }
            }
            return msg;
        }
        /// <summary>
        /// 转化成html方式的消息，以方便html的信息输出.
        /// </summary>
        /// <returns></returns>
        public string ToMsgOfHtml()
        {
            if (this.OutMessageHtml != null)
                return this.OutMessageHtml;

            string msg = "";
            foreach (SendReturnObj item in this)
            {
                if (item.HisSendReturnMsgType == SendReturnMsgType.SystemMsg)
                    continue;

                if (item.MsgOfHtml != null)
                {
                    msg += "@" + item.MsgOfHtml;
                    continue;
                }

                if (item.Msg != null)
                {
                    msg += "@" + item.Msg;
                    continue;
                }
            }
            return msg;
        }
    }
}
