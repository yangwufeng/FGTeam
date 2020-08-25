using BLL;
using Models.BLLModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Requse_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Id = 121;
            user.UserName = "YANGWUFENG";
            user.UserCode = "12";
            user.Address = "311";
            user.Created = null;
            user.CreatedBy = "FG";
            user.Disable = false;
            user.Partment = "4";
            user.Password = "123456";
            user.Phone = "Cs";
            user.Remark = "测试";
            user.Updated = null;
            user.Updatedby = "CS";

            var temp = AppSession.CommonService.PostJson<User>(user).Result;
            if (temp.Success)
            {
                BLLResultFactory.Success(temp.Data, null);
            }
        }
    }
    }

