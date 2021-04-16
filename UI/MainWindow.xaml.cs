using BLL;
using Microsoft.Win32;
using Models.APIModel;
using Models.BLLModel;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

                var temp = AppSession.DAL.GetCommonModelBy<EquipmentSiteRequest>("order by Id desc limit 10,50");


                if (!temp.Success && !data.Success)
                {
                    return;
                }
                this.dgv_1.ItemsSource = data.Data.OrderByDescending(x => x.Id);

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
                var s = AppSession.DAL.InsertCommonModel<SRMStatusUpdateRequest>(sRMStatusUpdateRequest);
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

        private void 导入_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "表格文件 (*.xls,*.xlsx)|*.xls;*.xlsx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog()==true)
            {
                DataTable dt = NopiModel.ExcelImport(openFileDialog.FileName);
                if (dt == null)
                {
                    MessageBox.Show("读取失败");
                    return;
                }
                //}
                //var result = ExcelReader<TestModel>.ReadDataTable(dt, c =>
                //{
                //    #region dev
                //    //c.For((k, v) => { k.No = v; }, "案件號");
                //    //c.For((k, v) =>
                //    //{
                //    //    k.BeginTime = DateTime.Parse(v);
                //    //}, "派工時間");
                //    //c.For((k, v) => { k.EndTime = DateTime.Parse(v); ; }, "結案時間");
                //    //c.For((k, v) =>
                //    //{
                //    //    k.Result = v;
                //    //}, "處理時效");
                //    #endregion
                //    for (var i = 1; i < dt.Rows.Count; i++)
                //    {
                //        c.For((k, v) => { k.Result += v + Environment.NewLine; }, i);
                //    }
                //});
                Thread t = new Thread(() =>
                {
                    //txtResult.BeginInvoke(new Action(() =>
                    //{
                    //    foreach (var item in result)
                    //    {
                    //        //txtResult.AppendText(item.No + "----------" + new TestModelExt().GetJishuanResult(item) + Environment.NewLine);
                    //        txtResult.AppendText(item.Result + Environment.NewLine);
                    //    }
                    //}));

                });

                t.Start();
            }

        }

    }



}

