using System;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using HslCommunication;
using HslCommunication.Enthernet;
using HslCommunication.MQTT;
using HslCommunication.Profinet.Melsec;
using HslCommunication.Profinet.Siemens;
using Newtonsoft.Json.Linq;
using ServiceStack.Redis;

namespace TestClient
{
    class Program
    {

        static void Main(string[] args)
        {

            RedisClient redis = new RedisClient("127.0.0.1", 6379);
            var setResult = redis.Set("mykey", "Hello,world");
            Console.WriteLine($"写入结果：{setResult}");
            var readResult = redis.Get<string>("mykey");
            Console.WriteLine($"读取结果：{readResult}");
            //验证是否区分大小写
            var read = redis.Get<string>("MYKEY");
            Console.WriteLine($"大写结果：{read}");
            Console.ReadKey();

            #region MyRegion
            //SiemensS7Net plc = new SiemensS7Net(SiemensPLCS.S1200, "127.0.0.1"); // 此处拿了本地虚拟的PLC测试

            //plc.SetPersistentConnection(); // 设置了长连接

            //MqttServer mqttServer = new MqttServer();
            //mqttServer.ServerStart(1883);
            //mqttServer.RegisterMqttRpcApi("MainPlc", plc);
            //// 需要管理员启动

            //while (true)
            //{
            //    Thread.Sleep(1000); // 每秒读取一次
            //    OperateResult<short> read = plc.ReadInt16("M100");
            //    if (read.IsSuccess)
            //    {
            //        // 读取成功后，进行业务处理，存入数据库，或是其他的分析
            //        Console.WriteLine("读取成功，M100：" + read.Content);
            //    }
            //    else
            //    {
            //        // 读取失败之后，显示下状态
            //        Console.WriteLine("读取PLC失败，原因：" + read.Message);
            //    }
            //}
            #endregion

            //MqttServer server = new MqttServer();
            //server.ServerStart(1883);
            //int i = 0;
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    server.PublishTopicPayload("A", Encoding.UTF8.GetBytes(i.ToString()));
            //    i++;
            //}

            //MqttServer server = new MqttServer();
            //server.ServerStart(1883);
            //int i = 0;
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);

            //    JObject json = new JObject();
            //    json.Add("温度1", new JValue(i));
            //    json.Add("温度2", new JValue(i + 1));
            //    json.Add("温度3", new JValue(i + 2));

            //    server.PublishTopicPayload("A", Encoding.UTF8.GetBytes(json.ToString()));
            //    i++;
            //}

            //MqttServer server = new MqttServer();
            //server.ServerStart(1883);
            //int i = 0;
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);

            //    XElement element = new XElement("Data");
            //    element.SetAttributeValue("温度1", i);
            //    element.SetAttributeValue("温度2", i + 1);
            //    element.SetAttributeValue("温度3", i + 2);

            //    server.PublishTopicPayload("A", Encoding.UTF8.GetBytes(element.ToString()));

            //    server.PublishTopicPayload("B", Encoding.UTF8.GetBytes(i.ToString()));
            //    i++;
            //}
            M1();

        }
        /// <summary>
        /// 是否发布数据的标记
        /// </summary>
        static void M1()
        {
            MqttServer server = new MqttServer();
            server.ServerStart(1883);
            bool isPublish = true;               // 是否发布数据的标记。

            server.OnClientApplicationMessageReceive += (MqttSession session, MqttClientApplicationMessage message) =>
            {
                if (session.Protocol == "HUSL")
                {
                    // 对同步网络进行处理
                    if (message.Topic == "STOP")
                    {
                        isPublish = false;      // 停止发布数据
                        server.PublishTopicPayload(session, "SUCCESS", null);   // 返回操作成功的说明
                    }
                    else if (message.Topic == "CONTINUE")
                    {
                        isPublish = true;       // 继续发布数据
                        server.PublishTopicPayload(session, "SUCCESS", null);   // 返回操作成功的说明
                    }
                    else
                    {
                        server.PublishTopicPayload(session, message.Topic, message.Payload);   // 其他的命令不处理，把原数据返回去
                    }
                }
            };

            int i = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(1000);

                if (isPublish)
                {
                    XElement element = new XElement("Data");
                    element.SetAttributeValue("温度1", i);
                    element.SetAttributeValue("温度2", i + 1);
                    element.SetAttributeValue("温度3", i + 2);

                    server.PublishTopicPayload("A", Encoding.UTF8.GetBytes(element.ToString()));

                    server.PublishTopicPayload("B", Encoding.UTF8.GetBytes(i.ToString()));
                    i++;
                }
            }
        }
    }
}
