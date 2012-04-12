using System;
using System.Collections.Generic;
using org.smslib;
using System.Threading;
using org.smslib.modem;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;

namespace BP.SMS
{
    /// <summary>
    /// 短信发送类。使用前需先初始化默认端口 SendPort 例如："COM1"
    /// </summary>
    public class SMSMessage 
    {
        
        /// <summary>
        /// 默认使用的发送端口
        /// </summary>
        public static string SendPort
        { get; set; }
        /// <summary>
        /// 短信文本内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送或接收时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 发送短信时表示接收人手机号码；
        /// 接收短信时表示发送人手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 发件人短信中心号码
        /// </summary>
        public string SendmscNumber { get; set; }

        /// <summary>
        /// 信息存储位置： SM 电话卡；
        /// </summary>
        public string MemLocation { get; set; }

        /// <summary>
        ///存储位置索引
        /// </summary>
        public int MemIndex { get; set; }

        /// <summary>
        /// 信息ID
        /// </summary>
        public int MessageId { get; set; }
        /// <summary>
        /// PDU Date
        /// </summary>
        public string PDUData { get; set; }

        /// <summary>
        /// 发送短信构造函数
        /// </summary>
        /// <param name="msg"></param>
        public SMSMessage(OutboundMessage msg)
        {
            Content = msg.getText();
            PhoneNumber = msg.getRecipient();
            Time = DateTime.Now;
        }

        /// <summary>
        /// 接收短信
        /// </summary>
        /// <param name="msg"></param>
        public SMSMessage(InboundMessage msg)
        {
            Content = msg.getText();
            Time = DateTime.Parse(msg.getDate().toLocaleString()).AddHours(8);
            PhoneNumber = msg.getOriginator().ToString();
            SendmscNumber = msg.getSmscNumber().ToString();
            MemLocation = msg.getMemLocation().ToString();
            MemIndex = int.Parse(msg.getMemIndex().ToString());
            MessageId = int.Parse(msg.getMessageId().ToString());
            PDUData = msg.getPduUserData().ToString();
        }

        /// <summary>
        /// 删除短信息
        /// </summary>
        /// <param name="srv">服务对象</param>
        /// <param name="msg">接收的短信息</param>
        /// <returns></returns>
        public static bool DeletMsg(InboundMessage msg)
        {
            try
            {
                // Uncomment following line if you wish to delete the message upon arrival.
                return srv.deleteMessage(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine("发生错误...");
                return false;
            }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="telephoneNumber">电话号</param>
        /// <param name="content">短信内容</param>
        public static void Send(string telephoneNumber, string content, string myPK)
        {
            try
            {
                if (srv.getServiceStatus() != Service.ServiceStatus.STARTED)
                    StartService();
            }
            catch (System.Exception)
            {
                StartService();
            }
            OutboundMessage outmsg = new OutboundMessage(telephoneNumber, content);
            // Send more than one message at once.
            //OutboundMessage[] msgArray = new OutboundMessage[2];
            //msgArray[0] = new OutboundMessage("+306948494037", "Hello from SMSLib for .NET (#1)");
            //msgArray[1] = new OutboundMessage("+306948494037", "Hello from SMSLib for .NET (#2)");
            //srv.sendMessages(msgArray);
            outmsg.setEncoding(Message.MessageEncodings.ENCUCS2);
            //开启状态报告
            outmsg.setStatusReport(true);
            //只有使用queueMessage(s) 方法，才会调用回调方法，使用sendMessage(s)方法不会调用，因此无法检测短信接收状态
            srv.queueMessage(outmsg);
            //Scope.Transaction.Begin();
            SMSMessage sendmsg = new SMSMessage(outmsg);
            SendSMSList.Add(sendmsg);
            if (SendSMSList.Count >= 2000)
            {
                SendSMSList.RemoveAt(0);
            }

            SaveSendSms(myPK);

        }

        /// <summary>
        /// 发送短信保存容器
        /// </summary>
        public static List<SMSMessage> SendSMSList = new List<SMSMessage>();
        /// <summary>
        ///  接收短信保存容器
        /// </summary>
        public static List<SMSMessage> ResiveSMSList = new List<SMSMessage>();

        private static Service srv;
        private static Comm2IP.Comm2IP comPort;
        /// <summary>
        /// 启动服务
        /// </summary>
        public static bool StartService()
        {
            try
            {
                srv = Service.getInstance();
                if (srv.getServiceStatus() == Service.ServiceStatus.STARTED)
                { }
                else
                {
                    //srv.stopService();
                    if (string.IsNullOrEmpty(SendPort))
                    {
                        SendPort = "COM6";// SMSConfig.GetDefaultPort();
                    }
                    // *** The tricky part ***
                    // *** Comm2IP Driver ***
                    // Create (and start!) as many Comm2IP threads as the modems you are using.
                    // Be careful about the mappings - use the same mapping in the Gateway definition.
                    //多串口发送，smslib会自动分配串口发送
                    List<string> ports = SerialPort();
                    for (int i = 0; i < ports.Count; i++)
                    {
                        comPort = new Comm2IP.Comm2IP(new byte[] { 127, 0, 0, 1 }, 12000 + i, ports[i], 9600);
                        new Thread(new ThreadStart(comPort.Run)).Start();
                    }

                    // Create new Service object - the parent of all and the main interface to you.
                    srv = Service.getInstance();

                    // Lets set some callbacks.
                    srv.setInboundMessageNotification(new MessageProcess());
                    srv.setCallNotification(new MessageProcess());
                    srv.setOutboundMessageNotification(new MessageProcess());
                    srv.setGatewayStatusNotification(new MessageProcess());

                    // Create the Gateway representing the serial GSM modem.
                    // Due to the Comm2IP bridge, in SMSLib for .NET all modems are considered IP modems.
                    IPModemGateway gateway = new IPModemGateway("modem.com1", "127.0.0.1", 12000, "Nokia", "");
                    gateway.setIpProtocol(ModemGateway.IPProtocols.BINARY);

                    // 设置文本协议，PDU可接收中文
                    gateway.setProtocol(AGateway.Protocols.PDU);

                    //设置可接收短信
                    gateway.setInbound(true);

                    // 设置可以发送短信
                    gateway.setOutbound(true);

                    // 设置 SIM卡 PIN密码.
                    gateway.setSimPin("0000");

                    //gateway.setSmscNumber("+8613800220500");

                    // 添加网关至服务对象
                    srv.addGateway(gateway);
                    // Similarly, you may define as many Gateway objects, representing
                    // various GSM modems, add them in the Service object and control all of them.

                    // Start! (i.e. connect to all defined Gateways)
                    srv.startService();
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }


        /// <summary>
        /// 停止服务
        /// </summary>
        public static void StopService()
        {
            srv.stopService();
            comPort.Stop();
        }

        /// <summary>
        /// 读取短信卡中所有短信
        /// </summary>
        /// <returns></returns>
        public static List<SMSMessage> ReadAllMessages()
        {
            InboundMessage[] msgList = srv.readMessages(org.smslib.InboundMessage.MessageClasses.ALL);
            Array.ForEach(msgList, Console.WriteLine);
            List<SMSMessage> smgList = new List<SMSMessage>();
            foreach (var item in msgList)
            {
                smgList.Add(new SMSMessage(item));
            }
            return smgList;
        }

        /// <summary>
        /// 删除所有短信息
        /// </summary>
        public void DeleteAllMessages()
        {
            InboundMessage[] msgList = srv.readMessages(org.smslib.InboundMessage.MessageClasses.ALL);
            Array.ForEach(msgList, msg => DeletMsg(msg));
        }


        /// <summary>
        /// 获取短信猫串口
        /// 请手动配置短信猫后更改配置文件
        /// </summary>
        /// <returns></returns>
        private static List<string> SerialPort()
        {
            NameValueCollection SerialPortSettings =
             (NameValueCollection)ConfigurationManager.GetSection("appSettings");

            List<string> ports = SerialPortSettings["SerialPort"].Split(',', '，').ToList<string>();
            return ports;
        }

        public override string ToString()
        {
            return String.Format("号码：{0}/n时间：{1}/n信息内容：{2}", PhoneNumber, Time, Content);
        }
      
        /// <summary>
        /// 保存已发送短信，变更发送状态
        /// 请自己完善
        /// </summary>
        /// <param name="myPK"></param>
        public static void SaveSendSms(string myPK)
        {
            
       
        }

        /// <summary>
        /// 保存接收到的短信到数据库
        /// </summary>
        /// <param name="param"></param>
        public static void SaveResivedSms(SMSMessage param)
        {
           
        }
    }
}
