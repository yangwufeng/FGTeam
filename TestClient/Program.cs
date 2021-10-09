using System;
using System.Linq;
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
    public class B
    {
        public delegate void WriteMethod(string x);
        public WriteMethod delegate1;
        public static event WriteMethod s;
        public void MianRun(string ss)
        {
            s += B_s1; ;
            s.Invoke("123");

        }

        private void B_s1(string x)
        {
            Console.WriteLine("123456" + x);
        }

        private void B_s(string x)
        {
            Console.WriteLine("sss" + x);
        }

        public void Run(string x)
        {
            Console.WriteLine("开始执行委托：");
            delegate1(x);
        }
    }
    class Program
    {
        public static void MethodA(string x)
        {
            Console.WriteLine("执行方法A成功" + x);
        }
        public static void MethodB(string x)
        {
            Console.WriteLine("执行方法B成功" + x);
        }
        // 提到递归，我们可能会想到的一个实例便是斐波那契数列。斐波那契数列就是如下的数列：

        // 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, …，总之，就是第N(N > 2)个数等于第(N - 1)个数和(N - 2)个数的和。用递归算法实现如下：
        public static int Foot(int num)
        {
            if (num == 0 || num < 0)
            {
                return 0;
            }
            else if (num == 1)
            {
                return 1;
            }
            else if (num < 0)
            {
                return -1;
            }
            else
            {
                return Foot(num - 1) + Foot(num - 2);
            }
        }

        public static void Test3()
        {
            //for (int i = 1; i < 9; i++)
            //{
            //    for (int j = 1; j <= i; j++)
            //    {
            //        Console.Write("*");
            //    }
            //    Console.WriteLine();
            //}
            string str1 = "";
            string str2 = "";

            for (int i = 1; i <= 4; i++)
            {
                string s1 = "";
                string s2 = "";
                for (int j = 4 - i; j > 0; j--)
                {
                    Console.Write(" ");
                    s1 = s1 + " ";
                }
                for (int k = 1; k <= i * 2 - 1; k++)
                {

                    Console.Write("*");
                    s2 += "*";
                }
                str1 = s1 + s2;
                Console.Write("\n");

            }

            for (int x = 1; x <= 3; x++)
            {
                string s3 = "";
                string s4 = "";

                for (int y = 1; y <= x; y++)
                {
                    Console.Write(" ");
                    s3 += " ";

                }
                for (int z = 7 - x * 2; z >= 1; z--)
                {
                    Console.Write("*");
                }
                s4 += "*";
                str2 = s3 + s4;
                Console.Write("\n");



            }
            Console.ReadLine();


        }
        public static void Test2()
        {
            int[] list = new int[] { 5, 12, 2, 35, 11, 33, 551, 3556 };
            //防止数组超出
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = 0; j < list.Length - 1 - i; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);

            }
            for (int i = 9; i >= 1; i--)
            {
                for (int j = i; j >= 1; j--)
                {
                    Console.Write("{0}*{1}={2}\t", i, j, i * j);
                }
                Console.WriteLine();
            }
            Console.ReadKey();

        }
        static void Main(string[] args)
        {
            Test3();
            //Console.WriteLine(Foot(2));
            //B b = new B();
            //b.delegate1 = MethodA;
            //b.delegate1 += MethodB;
            //b.delegate1.Invoke("123456");
            //b.Run("1111");
            //b.MianRun("1231546545465454");
            Console.Read();


            //mainRun.Invoke(ss);
            //MainRun mainRun = ss;
            //while (true)
            //{
            //    B m = new B();
            //    m.delegate1 = MethodA;//也可以new 
            //    m.delegate1 += MethodB;
            //    string str = Console.ReadLine();
            //    m.Run(str);
            //    //Console.ReadLine();
            //}
            //test1();
            //RedisClient redis = new RedisClient("127.0.0.1", 6379);
            //var setResult = redis.Set("mykey", "Hello,world");
            //Console.WriteLine($"写入结果：{setResult}");
            //var readResult = redis.Get<string>("mykey");
            //Console.WriteLine($"读取结果：{readResult}");
            ////验证是否区分大小写
            //var read = redis.Get<string>("MYKEY");
            //Console.WriteLine($"大写结果：{read}");
            //Console.ReadKey();

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
            //M1();

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

        public static void test1()
        {
            string[] text = { "Albert was here", "Burke slept late", "Connor is happy" };

            //Select输出为一个大对象
            var tokens = text.Select(s => s.Split(' '));

            foreach (string[] line in tokens)

                foreach (string token in line)
                    Console.Write("{0}.", token);


            string[] text1 = { "Albert was here", "Burke slept late", "Connor is happy" };
            //SelectMany把所有的数据进行输出一个对象
            var tokens1 = text1.SelectMany(s => s.Split(' '));

            foreach (string token in tokens1)
                Console.Write("{0}.", token);
        }
    }
}
