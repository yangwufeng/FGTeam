using BLL.Services;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// ClientServer.xaml 的交互逻辑
    /// </summary>
    public partial class ClientServer : Window
    {
        private BackgroundWorker demoBGWorker = new BackgroundWorker();
        static TcpClient tcpClient;
        static NetworkStream stream;
        bool Stop = true;
        /// <summary>
        /// 通讯协议格式
        /// </summary>
        private static Encoding encode = Encoding.Default;

        private Socket SocketServer;
        private Socket Socketclient;
        private static object obj = new object();

        //public Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();

        ThreadSafeDictionary<string, Socket> dicSocket = new ThreadSafeDictionary<string, Socket>();
        public ClientServer()
        {
            InitializeComponent();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                open.IsEnabled = false;
                //IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip.Text), int.Parse(port.Text));//接收端所监听的接口,ip也可以用IPAddress.Any
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, int.Parse(port.Text));//接收端所监听的接口,ip也可以用IPAddress.Any

                SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//初始化一个Socket对象
                SocketServer.Bind(ipEnd);//绑定套接字到一个IP地址和一个端口上(bind())；
                SocketServer.Listen(10);



                new Thread(delegate ()
            {
                //lock (obj)
                //{
                object o = new object();
                Socket clientSocket = o as Socket;
                while (Stop)
                {


                    Stopwatch sw = new Stopwatch();
                    // 开始计时
                    sw.Start();

                    clientSocket = SocketServer.Accept(); //一旦接受连接，创建一个客户端
                    var RemoteEndPoint = string.Empty;
                    dicSocket.Add(RemoteEndPoint = clientSocket.RemoteEndPoint.ToString(), clientSocket);

                    if (dicSocket.ContainsKey(RemoteEndPoint))
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            list_box.Items.Add(NewText(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "连接ip：" + RemoteEndPoint + "\n"));
                        });

                    }

                    Task.Run(() =>
                         {
                             while (Stop)
                             {
                                 var str = "";
                                 //服务端接收
                                 byte[] buffer = new byte[1024 * 1024 * 2];
                                 int r = clientSocket.Receive(buffer);
                                 if (r == 0)
                                 {
                                     break;
                                 }

                                 Dispatcher.Invoke(() =>
                                            {
                                                list_box.Items.Add(NewText(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "内容：" + encode.GetString(buffer, 0, r) + "\n"));
                                                this.list_box.ScrollIntoView(this.list_box.ItemContainerGenerator);
                                            });
                             }

                         });

                }


                //}

            })
                { IsBackground = true }.Start();



                Task.Run(() =>
                {

                    while (Stop)
                    {

                        Thread.Sleep(1000);

                        this.Dispatcher.Invoke(() =>
                        {
                            List<string> keys = new List<string>();
                            keys.AddRange(dicSocket.Keys);
                            foreach (var item in keys)
                            {
                                if (dicSocket[item].Poll(10, SelectMode.SelectRead))
                                {
                                    dicSocket.Remove(item);
                                }
                                else
                                {
                                    lab_Count.Content = dicSocket.Values.Count.ToString();
                                }
                            }
                        });
                    }
                });



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void Btn_Send_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in dicSocket)
            {
                //需要加上延迟 才能显示数据源发送成功
                if (item.Value.Poll(10, SelectMode.SelectRead))
                {
                    continue;
                }
                byte[] msgbyte = encode.GetBytes(this.txt_msg.Text);
                Thread.Sleep(1000);
                item.Value.Send(msgbyte);
            }

        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {

            Thread.Sleep(10);

            try
            {
                Dispatcher.Invoke(() =>
                {
                    Socketclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(this.ip.Text), Convert.ToInt32(port.Text));
                    Socketclient.Connect(ipep);
                    if (Socketclient.Connected)
                    {
                        Task.Run(() =>
                        {
                            while (Stop)
                            {

                                var str = "";
                                //客户端接收
                                byte[] buffer = new byte[1024 * 1024 * 2];
                                int r = Socketclient.Receive(buffer);
                                if (r == 0)
                                {
                                    break;
                                }

                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    list_box1.Items.Add(NewText(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "内容：" + encode.GetString(buffer, 0, r) + "\n"));
                                    this.list_box1.ScrollIntoView(this.list_box1.SelectedItem);
                                });

                            }
                        });

                    }
                    else
                    {

                    }
                });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {


            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //try
            //{
            //    open.IsEnabled = true;
            //    Stop = false;
            //    Thread.Sleep(1000);
            //    if (SocketServer == null)
            //        return;
            //    if (!SocketServer.Connected)
            //        return;
            //    SocketServer?.Shutdown(SocketShutdown.Both);
            //    Socketclient?.Shutdown(SocketShutdown.Both);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    SocketServer?.Close();
            //    Socketclient?.Close();
            //}

        }

        private void btn_Out_Click(object sender, RoutedEventArgs e)
        {
            byte[] msgbyte = encode.GetBytes(this.txt_info.Text);
            Thread.Sleep(10);
            Socketclient.Send(msgbyte);
        }


        public TextBlock NewText(string log)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = log;
            textBlock.TextWrapping = TextWrapping.Wrap;
            return textBlock;

        }
        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="log"></param>
        /// <param name="level">1显示绿色，2显示红色</param>
        public void AddAlarm(string log, int level)
        {
            //先找存不存在
            foreach (var item in list_box.Items)
            {
                var a = (TextBlock)item;
                if (a.Text.Contains(log))
                {
                    //存在就不再次添加
                    return;
                }
            }
            TextBlock textBlock = new TextBlock
            {
                Text = DateTime.Now.ToLongTimeString() + ":" + log
            };
            switch (level)
            {
                case 1:
                    textBlock.Background = Brushes.Green;
                    break;
                case 2:
                    textBlock.Background = Brushes.Red;
                    break;
            }
            textBlock.MaxWidth = this.Width;
            textBlock.TextWrapping = TextWrapping.Wrap;
            this.list_box.Items.Add(textBlock);
            this.list_box.SelectedIndex = this.list_box.Items.Count - 1;
            this.list_box.ScrollIntoView(this.list_box.SelectedItem);
        }
    }

}
