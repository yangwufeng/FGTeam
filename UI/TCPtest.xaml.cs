using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Services;
using TestFramenWork.SocketObj;

namespace UI
{
    /// <summary>
    /// TCPtest.xaml 的交互逻辑
    /// </summary>
    public partial class TCPtest : Window
    {
        public TCPtest()
        {
            InitializeComponent();
        }


        private void lianjie_Click(object sender, RoutedEventArgs e)
        {
            TcpServer.Start();
        }

        private void fason_Click(object sender, RoutedEventArgs e)
        {
            TcpServer.SendData(this.txt_name.Text);
            TcpServer.Print(this.txt_name.Text);
            //TcpServer.DeleMark();
        }
    }
}
