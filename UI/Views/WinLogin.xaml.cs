using BLL;
using Models.Model;
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

namespace UI.Views
{
    /// <summary>
    /// WinLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Password.Password))
            {
                MessageBox.Show("密码为空！！！");
            }
            else if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                MessageBox.Show("账户为空！！！");
            }
            else
            {
                var a = AppSession.DAL.GetCommonModelBy<User>($"where userCode='{txt_Name.Text}'  and password='{txt_Password.Password}'");
                if (a.Success)
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("密码错误");
                }
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
