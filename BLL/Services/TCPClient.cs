using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BLL.Services
{
    public class TCPClient
    {



        #region MyRegion
        #region 广播消息
        static List<TCPClient> clientList = new List<TCPClient>();
        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"></param>
        public static void BroadcastMessage(string message)   //找到相对应的客户端发送消息
        {
            var notConnectedList = new List<TCPClient>();
            foreach (var client in clientList)
            {
                if (client.Connected)
                    client.SendMessage(message);  //调用服务端发送消息方法
                else
                {
                    notConnectedList.Add(client);
                }
            }
            foreach (var temp in notConnectedList)
            {
                clientList.Remove(temp);
            }
        }

        static void Open()
        {
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));

            tcpServer.Listen(100);
            Console.WriteLine("server running...");

            while (true)
            {
                Socket clientSocket = tcpServer.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Console.WriteLine("a client is connected !");
                TCPClient client = new TCPClient(clientSocket);      //把与每个客户端通信的逻辑(收发消息)放到client类里面进行处理
                clientList.Add(client);
            }
        }

        #endregion
        #region 处理数据类
        private Socket clientSocket;
        private Thread t;
        private byte[] data = new byte[1024];//这个是一个数据容器

        public TCPClient(Socket s)
        {
            clientSocket = s;
            //启动一个线程 处理客户端的数据接收
            t = new Thread(ReceiveMessage);
            t.Start();
        }

        private void ReceiveMessage()
        {
            //一直接收客户端的数据
            while (true)
            {
                //在接收数据之前  判断一下socket连接是否断开
                if (clientSocket.Poll(10, SelectMode.SelectRead))
                {
                    clientSocket.Close();
                    break;//跳出循环 终止线程的执行
                }

                int length = clientSocket.Receive(data);
                string message = Encoding.UTF8.GetString(data, 0, length);
                //接收到数据的时候 要把这个数据 分发到客户端
                //广播这个消息
                BroadcastMessage(message);
                Console.WriteLine("收到了消息:" + message);
            }
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);    //将客户端发送过来的消息返回给服务端
        }

        public bool Connected
        {
            get { return clientSocket.Connected; }
        }
        #endregion

        #region 客户端代码
        private static byte[] result = new byte[1024];
        static void OpenConnect()
        {
            //设定服务器IP地址
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 8888)); //配置服务器IP与端口
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //通过clientSocket接收数据
            int receiveLength = clientSocket.Receive(result);
            Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            //通过 clientSocket 发送数据
            string str = "";
            while (str != "exit")
            {
                try
                {
                    Thread.Sleep(1000); //等待1秒钟
                    Console.WriteLine("向服务器发送消息：");
                    str = Console.ReadLine();
                    clientSocket.Send(Encoding.ASCII.GetBytes(str + "  " + DateTime.Now + "1"));
                    int receiveL = clientSocket.Receive(result);
                    Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveL));
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
            Console.ReadKey();
        }
        #endregion
        #endregion


    }



}
