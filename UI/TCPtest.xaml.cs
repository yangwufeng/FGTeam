using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Amqp.Types;
using BLL.Services;
using TestFramenWork.SocketObj;

namespace UI
{
    /// <summary>
    /// TCPtest.xaml 的交互逻辑
    /// </summary>
    public partial class TCPtest : Window
    {
        private BackgroundWorker demoBGWorker = new BackgroundWorker();
        static TcpClient tcpClient;
        static NetworkStream stream;
        /// <summary>
        /// 按照这种格式
        /// </summary>
        private static Encoding encode = Encoding.Default;

        public TCPtest()
        {
            InitializeComponent();
        }

     
        private void BGWorker_DoWork()
        {

            var serverIPEndPoint = new IPEndPoint(IPAddress.Parse(ip.Text), 5052); // 当前服务器使用的ip和端口
            TcpListener tcpListener = new TcpListener(serverIPEndPoint);
            tcpListener.Start();
            Console.WriteLine("服务端已启用......"); // 阻塞线程的执行，直到一个客户端连接
            tcpClient = tcpListener.AcceptTcpClient();
            Console.WriteLine("已连接.");
            stream = tcpClient.GetStream();          // 创建用于发送和接受数据的NetworkStream
            var t1 = new Thread(ReceiveMsg);
            t1.IsBackground = true;
            t1.Start();

        }
        private void BGWorker_DoWork1()
        {
            var serverIPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.99"), 8234); // 当前服务器使用的ip和端口
            TcpListener tcpListener = new TcpListener(serverIPEndPoint);
            tcpListener.Start();
            Console.WriteLine("服务端已启用......"); // 阻塞线程的执行，直到一个客户端连接
            tcpClient = tcpListener.AcceptTcpClient();
            Console.WriteLine("已连接.");
            stream = tcpClient.GetStream();          // 创建用于发送和接受数据的NetworkStream


            var t1 = new Thread(ReceiveMsg);
            t1.IsBackground = true;
            t1.Start();

        }
        /// <summary>
        /// 等待客户端的连接 并且创建与之通信的Socket
        /// </summary>
        Socket socketSend;
        void Listen(object o)
        {
            try
            {
                Socket socketWatch = o as Socket;
                while (true)
                {
                    socketSend = socketWatch.Accept();//等待接收客户端连接                  
                    //开启一个新线程，执行接收消息方法
                    Thread r_thread = new Thread(Received);
                    r_thread.IsBackground = true;
                    r_thread.Start(socketSend);
                }
            }
            catch { }
        }

        /// <summary>
        /// 服务器端不停的接收客户端发来的消息
        /// </summary>
        /// <param name="o"></param>
        void Received(object o)
        {
            try
            {
                Socket socketSend = o as Socket;
                while (true)
                {
                    //客户端连接服务器成功后，服务器接收客户端发送的消息
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    //实际接收到的有效字节数
                    int len = socketSend.Receive(buffer);
                    if (len == 0)
                    {
                        break;
                    }
                    // string str = Encoding.UTF8.GetString(buffer, 0, len);
                    string stringData = "0x" + BitConverter.ToString(buffer, 0, len).Replace("-", " 0x").ToLower();

                    App.Current.Dispatcher.Invoke(() =>
                    {

                        richTextBox1.Items.Add(DateTime.Now.ToString("yy-MM-dd hh:mm:ss -*- ") + stringData + "\n");
                    });

                }
            }
            catch { }
        }

        public Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        public string RemoteEndPointIP;
        private void BGWorker_DoWork2()
        {
            int recv;//定义接收数据长度变量
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip.Text), int.Parse(port.Text));//接收端所监听的接口,ip也可以用IPAddress.Any
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//初始化一个Socket对象
            socket.Bind(ipEnd);//绑定套接字到一个IP地址和一个端口上(bind())；
            socket.Listen(10);

            new Thread(delegate ()
            {
                Socket clientSocket = null;
                while (true)
                {
                    Stopwatch sw = new Stopwatch();
                    // 开始计时
                    sw.Start();

                    clientSocket = socket.Accept(); //一旦接受连接，创建一个客户端
                    dicSocket.Add(RemoteEndPointIP = clientSocket.RemoteEndPoint.ToString(), clientSocket);
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            var str = "";
                            var mark = "";
                            //客户端接收消息
                            byte[] buffer = new byte[1024 * 1024 * 2];
                            int r = clientSocket.Receive(buffer);
                            if (r == 0)
                            {
                                break;
                            }
                            if (encode.GetString(buffer, 0, r - 1) == "newmark")
                            {
                                str = encode.GetString(buffer, 0, r);
                              
                            }
                            else
                            {
                                str = encode.GetString(buffer, 0, r);
                                if (str.Contains("数据"))
                                {
                                    qingiqu();
                                }
                                else if(str.Contains("无标题"))
                                {
                                    fason();
                                }
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    richTextBox1.Items.Add(DateTime.Now.ToString("yy-MM-dd hh:mm:ss -*- ") + str + "\n");
                                });
                            }



                           // byte[] res = new byte[1024];
                           // byte[] data = new byte[1024 * 1024 * 2];//对data清零
                           // recv = clientSocket.Receive(data, 0, data.Length, SocketFlags.None);
                           // //if (recv == 0) //如果收到的数据长度小于0，则退出
                           // //    break;
                           // //string stringData = "0x" + BitConverter.ToString(data, 0, recv).Replace("-", " 0x").ToLower();
                           ////var ss= encode.GetString(recv);
                           // string stringData = encode.GetString(res, 0, recv);
                           // App.Current.Dispatcher.Invoke(() =>
                           // {
                           //     richTextBox1.Items.Add(DateTime.Now.ToString("yy-MM-dd hh:mm:ss -*- ") + stringData + "\n");
                           // });
                           // //结束计时  
                           // sw.Stop();
                           // long times = sw.ElapsedMilliseconds;

                           // App.Current.Dispatcher.Invoke(() =>
                           // {
                           //     richTextBox1.Items.Add("执行查询总共使用了" + times + "毫秒" + "\n");
                           // });

                        }
                    });
                }
            })
            { IsBackground = true }.Start();
        }

        void ReceiveMsg()
        {
            byte[] buffer = new byte[1024]; // 预设最大接受1024个字节长度，可修改
            int count = 0;
            try
            {
                while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string stringData = "0x" + BitConverter.ToString(buffer, 0, count).Replace("-", " 0x").ToLower();
                    Console.WriteLine($"{tcpClient.Client.LocalEndPoint.ToString()}:{DateTime.Now.ToString("yy-MM-dd hh:mm:ss -*- ") + stringData + "\n"}");
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        richTextBox1.Items.Add(DateTime.Now.ToString("yy-MM-dd hh:mm:ss -*- ") + stringData + "\n");
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private void SendData(IPAddress remoteIP, int Port, byte[] bits)
        {
            //实例化socket               
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(remoteIP, Port);
            socket.Connect(ipep);
            //socket.Send(bits, 8, SocketFlags.None);

            string str4 = "set content=" + s.Text;

            var ss = encode.GetBytes(str4);
            List<byte> list6 = new List<byte>();

            list6.AddRange(ss);
            byte[] newBuffer6 = list6.ToArray();
            socket.Send(newBuffer6);
            socket.Close();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            //demoBGWorker.DoWork += BGWorker_DoWork;
            //demoBGWorker.RunWorkerAsync();
            //Task.Run(() =>
            // {
            BGWorker_DoWork2();
            //});
        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] order = new byte[8];
            order = new byte[] { 0x80, 0x04, 0x00, 0x7F };
            SendData(IPAddress.Parse(ip.Text), int.Parse(port.Text), order);
            MessageBox.Show("指令发送成功");
        }

        private void btnListen_Click(object sender, RoutedEventArgs e)
        {
            BGWorker_DoWork2();
   
        }
     


        private void fs_Click(object sender, RoutedEventArgs e)
        {
            List<byte> list6 = new List<byte>();
            string str10 = "open D:\\桌面\\数据.Tx7";
            byte[] buffer10 = encode.GetBytes(str10);
            list6.AddRange(buffer10);
            byte[] newBuffer10 = list6.ToArray();
            //需要加上延迟 才能显示数据源发送成功
            Thread.Sleep(1000);
            dicSocket[RemoteEndPointIP].Send(newBuffer10);
            list6.Clear();
            //TcpServer.SendData(this.s.Text);
            //TcpServer.Print(this.s.Text);
            //Dictionary<string, string> dic = new Dictionary<string, string>();

            //dic.Add("项目", "123");
            //dic.Add("材质", "123");
            //dic.Add("预制管线生产线编号", "123");
            //dic.Add("尺寸", "123");
            //dic.Add("单管号", "123");
            //dic.Add("壁厚", "123");

            //    foreach (var item in dic)
            //    {

            //    string str1 = "new TxMark7PiContent_Text";
            //    byte[] buffer1 = encode.GetBytes(str1);
            //    list6.AddRange(buffer1);
            //    byte[] newBuffer1 = list6.ToArray();
            //    //需要加上延迟 才能显示数据源发送成功
            //    Thread.Sleep(1000);
            //    dicSocket[RemoteEndPointIP].Send(newBuffer1);
            //    list6.Clear();

            //    string str3 = "set content="+ item.Key;
            //    byte[] buffer3 = encode.GetBytes(str3);
            //    list6.AddRange(buffer3);
            //    byte[] newBuffer2 = list6.ToArray();
            //    //需要加上延迟 才能显示数据源发送成功
            //    Thread.Sleep(1000);
            //    dicSocket[RemoteEndPointIP].Send(newBuffer2);
            //    list6.Clear();

            //    string str6 = "new TxMark7PiContent_Text";
            //    byte[] buffer6 = encode.GetBytes(str6);
            //    list6.AddRange(buffer6);
            //    byte[] newBuffer6 = list6.ToArray();
            //    //需要加上延迟 才能显示数据源发送成功
            //    Thread.Sleep(1000);
            //    dicSocket[RemoteEndPointIP].Send(newBuffer6);
            //    list6.Clear();

            //    string str7 = "set Arrange=" +2;
            //    byte[] buffer7 = encode.GetBytes(str7);
            //    list6.AddRange(buffer7);
            //    byte[] newBuffer7 = list6.ToArray();
            //    //需要加上延迟 才能显示数据源发送成功
            //    Thread.Sleep(1000);
            //    dicSocket[RemoteEndPointIP].Send(newBuffer7);
            //    list6.Clear();


            //    string str4 = "set content="+ item.Value;
            //    byte[] buffer4 = encode.GetBytes(str4);
            //    list6.AddRange(buffer4);
            //    byte[] newBuffer4 = list6.ToArray();
            //    //需要加上延迟 才能显示数据源发送成功
            //    Thread.Sleep(1000);
            //    dicSocket[RemoteEndPointIP].Send(newBuffer4);
            //    list6.Clear();
            //}
            string str4 = "setall content=项目,123,材质,123,预制管线生产线编号：,123,尺寸,123,单管号,123,壁厚,123,管段编号,123,炉批号,123,123";
            byte[] buffer4 = encode.GetBytes(str4);
            list6.AddRange(buffer4);
            byte[] newBuffer4 = list6.ToArray();
            //需要加上延迟 才能显示数据源发送成功
            Thread.Sleep(1000);
            dicSocket[RemoteEndPointIP].Send(newBuffer4);
            list6.Clear();
            ////删除标志
            //string str5 = "del newmark" + dic.Values.Count;
            //byte[] buffer5 = encode.GetBytes(str5);
            //List<byte> list5 = new List<byte>();
            //list5.AddRange(buffer5);
            //byte[] newBuffer5 = list5.ToArray();
            //Thread.Sleep(1000);

            //dicSocket[RemoteEndPointIP].Send(newBuffer5);

            //string str4 = "set content=" + s.Text;
            //string str4 =$"set content=项目,材质,预制管线生产线编号：,尺寸：,单管号：,壁厚," ;

            //var ss = encode.GetBytes(str4);

            //list6.AddRange(ss);
            //byte[] newBuffer6 = list6.ToArray();
            //需要加上延迟 才能显示数据源发送成功
            //Thread.Sleep(1000);
            //dicSocket[RemoteEndPointIP].Send(newBuffer6);

            //var sss = encode.GetString(newBuffer6);

            //App.Current.Dispatcher.Invoke(() =>
            //{
            //    richTextBox1.Items.Add(DateTime.Now.ToString("yy-MM-dd hh:mm:ss -*- ") + sss + "\n");
            //});
        }

        private void qingqiu_Click(object sender, RoutedEventArgs e)
        {
            List<byte> list6 = new List<byte>();
            string str10 = "open D:\\桌面\\无标题.Tx7";
            byte[] buffer10 = encode.GetBytes(str10);
            list6.AddRange(buffer10);
            byte[] newBuffer10 = list6.ToArray();
            //需要加上延迟 才能显示数据源发送成功
            Thread.Sleep(1000);
            dicSocket[RemoteEndPointIP].Send(newBuffer10);
            list6.Clear();


   
        }

        public void qingiqu() 
        {
            List<byte> list6 = new List<byte>();
            string str4 = "setall content=项目,123,材质,123,预制管线生产线编号：,123,尺寸,123,单管号,123,壁厚,123,管段编号,123,炉批号,123,123";

            byte[] buffer4 = encode.GetBytes(str4);
            list6.AddRange(buffer4);
            byte[] newBuffer4 = list6.ToArray();
            //需要加上延迟 才能显示数据源发送成功
            Thread.Sleep(1000);
            dicSocket[RemoteEndPointIP].Send(newBuffer4);
            list6.Clear();

        }
        public void fason() 
        {
            List<byte> list6 = new List<byte>();

            string str4 = "setall content=项目";
            byte[] buffer4 = encode.GetBytes(str4);
            list6.AddRange(buffer4);
            byte[] newBuffer4 = list6.ToArray();
            //需要加上延迟 才能显示数据源发送成功
            Thread.Sleep(1000);
            dicSocket[RemoteEndPointIP].Send(newBuffer4);
            list6.Clear();
        }
    }
}