using System;
using System.Linq;
using org.smslib;

namespace BP.SMS
{
    public class MessageProcess : IOutboundMessageNotification, IInboundMessageNotification, ICallNotification, IGatewayStatusNotification
    {

        #region 发送短信事件

        /// <summary>
        /// 发送短信事件
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="om"></param>
        public void process(AGateway gateway, OutboundMessage outmsg)
        {
            if (outmsg.getType() == Message.MessageTypes.OUTBOUND)
            {
                Console.WriteLine(String.Format(">>>检测到发送的新短信,网关: {0}", gateway.getGatewayId()));
                //System.Windows.Forms.MessageBox.Show(String.Format(">>>检测到发送的新短信,网关: {0},内容{1},收信人{2}", gatewayId,msg.getText(),msg. getRecipient()));
            }
            else if (outmsg.getType() == Message.MessageTypes.STATUSREPORT)
            {
                Console.WriteLine(String.Format(">>> 状态报告：{0}/n{1}", gateway.getGatewayId(), outmsg.getFrom()));
                //System.Windows.Forms.MessageBox.Show(String.Format(">>> 状态报告：{0}/n{1}", gatewayId, msg.getFrom()));
            }
            else if (outmsg.getType() == Message.MessageTypes.UNKNOWN)
            {
                Console.WriteLine("未知状态");
                Console.WriteLine(String.Format(">>> 应该是状态报告：{0}/n{1}", gateway.getGatewayId(), outmsg.getFrom()));
                //System.Windows.Forms.MessageBox.Show(String.Format(">>> 应该是状态报告：{0}/n{1}", gatewayId, msg.getFrom()));

            }
            Console.WriteLine(outmsg);
            //Console.WriteLine(String.Format("状态回复{0}", msg.getType()));
        }
        #endregion
        /// <summary>
        /// 接收短信事件
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="mmt"></param>
        /// <param name="im"></param>
        public void process(AGateway gateway, Message.MessageTypes msgType, InboundMessage inmsg)
        {
            if (msgType == Message.MessageTypes.INBOUND)
            {
                Console.WriteLine(String.Format(">>> 检测到新短消息,网关: {0}", gateway.getGatewayId()));

                //将接收到短信保存到数据库
                //Scope.Transaction.Begin();
                SMSMessage resivedmsg = new SMSMessage(inmsg);

                //同一时刻，同一号码短信视为同一短信！
                int count = 0;
                count = (from s in SMSMessage.ResiveSMSList
                         where s.Time == resivedmsg.Time && s.PhoneNumber == resivedmsg.PhoneNumber
                         select s).Count();
                if (count > 0)
                {
                    return;
                }

                //随时删除接收到的短信，防止短信卡满后无法接收短信
                SMSMessage.DeletMsg(inmsg);
                SMSMessage.ResiveSMSList.Add(resivedmsg);
                if (SMSMessage.ResiveSMSList.Count >= 500)
                {
                    SMSMessage.ResiveSMSList.RemoveAt(0);
                }
                SMSMessage.SaveResivedSms(resivedmsg);
            }
            else if (msgType == Message.MessageTypes.STATUSREPORT)
            {
                Console.WriteLine(String.Format(">>> 新短信New Inbound Status Report message detected from Gateway: {0}", gateway.getGatewayId()));
                SMSMessage.DeletMsg(inmsg);
            }
        }

        /// <summary>
        /// 新来电处理
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="str"></param>
        public void process(AGateway gateway, string callerId)
        {
            Console.WriteLine(String.Format(">>> 检测到新来电,网关: {0};电话号码: {1}", gateway.getGatewayId(), callerId));

        }

        /// <summary>
        /// 网关变化通知事件
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="aggs1"></param>
        /// <param name="aggs2"></param>
        public void process(AGateway gateway, AGateway.GatewayStatuses oldStatus, AGateway.GatewayStatuses newStatus)
        {
            Console.WriteLine(String.Format(">>> Gateway Status change for {0}, OLD: {1} -> NEW: {2}", gateway.getGatewayId(), oldStatus, newStatus));

        }

    }

}
