using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace UI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            TCPtest tCPtest = new TCPtest();
            tCPtest.ShowDialog();
            //UI线程未捕获异常处理事件（UI主线程）
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            string msg = String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace);//异常信息 和 调用堆栈信息
            MessageBox.Show(msg, "UI线程异常");
            e.Handled = true;//表示异常已处理，可以继续运行
        }

        //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
        //如果UI线程异常DispatcherUnhandledException未注册，则如果发生了UI线程未处理异常也会触发此异常事件
        //此机制的异常捕获后应用程序会直接终止。没有像DispatcherUnhandledException事件中的Handler=true的处理方式，可以通过比如Dispatcher.Invoke将子线程异常丢在UI主线程异常处理机制中处理
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                string msg = String.Format("发生异常，程序即将停止\n\n{0}\n\n{1}", ex.Message, ex.StackTrace);//异常信息 和 调用堆栈信息
                MessageBox.Show(msg, "非UI线程异常");
            }
        }

        //Task线程内未捕获异常处理事件
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            string msg = String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace);
        }
    }
}
