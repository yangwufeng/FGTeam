using BLL;
using Models.APIModel;
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
using System.Windows.Threading;

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
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(1);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var data = AppSession.DAL.GetCommonModelBy<SRMStatusUpdateRequest>("order by Id desc limit 10,50");
                this.dgv_1.ItemsSource = data.Data.OrderByDescending(x => x.Id);

                var temp = AppSession.DAL.GetCommonModelBy<EquipmentSiteRequest>("order by Id desc limit 10,50");
                this.mm.ItemsSource = temp.Data.OrderByDescending(x => x.Id);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message}");
            }
          

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SRMStatusUpdateRequest sRMStatusUpdateRequest = new SRMStatusUpdateRequest();
                sRMStatusUpdateRequest.Fault = "1";
                sRMStatusUpdateRequest.FaultCode = "1";
                sRMStatusUpdateRequest.SrmNo = "1";
                sRMStatusUpdateRequest.SrmStatus = 0;
               var s= AppSession.DAL.InsertCommonModel<SRMStatusUpdateRequest>(sRMStatusUpdateRequest);
                if (s.Success)
                {
                    MessageBox.Show($"{s.Msg}");






                }
            }
            catch (Exception ex)
            {

               MessageBox.Show($"{ex.ToString()}");
            }
          


        }

        private void xx_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EquipmentSiteRequest site = new EquipmentSiteRequest();
                site.SrmNo = "1";
                site.Y = 1;
                site.X = 1;
                var s = AppSession.DAL.InsertCommonModel<EquipmentSiteRequest>(site);
                if (!s.Success)
                {
                    MessageBox.Show($"{s.Msg}");

                }

                var temp = AppSession.DAL.GetCommonModelBy<EquipmentSiteRequest>("");
                this.mm.ItemsSource = temp.Data;
                MessageBox.Show($"{s.Msg}");

            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.ToString()}");
            }
        }
    }
}

