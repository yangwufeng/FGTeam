using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestFramenWork
{
    class Program
    {

        static void Main(string[] args)
        {
            //INog 日志操作。
            //M1();

            //抽象类实现
            //M2();


        }

        #region INog记录到数据库

        //public static Logger logger = LogManager.GetLogger("SimpleDemo");
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("执行开始");
        //    logger.Error("Hello World");
        //    Console.WriteLine("执行结束");
        //    Console.ReadKey();
        //}

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// INog记录到数据库
        /// </summary>
        static void M1()
        {
            try
            {
                string Ip = null;
                List<Task> tasks = new List<Task>();

                var task = Task.Run(() =>
                {
                    while (true)
                    {
                        var ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(t => t.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToList().Aggregate("", (n, m) =>
                        {
                            return n += m.ToString() + ";";
                        }, n => n.TrimEnd(';'));
                        logger.Info(ips);
                    }
                });
                logger.Trace("输出一条记录信息成功！");//最常见的记录信息，一般用于普通输出   
                logger.Debug("输出一条Debug信息成功！"); //同样是记录信息，不过出现的频率要比Trace少一些，一般用来调试程序   
                logger.Info("输出一条消息类型信息成功！");//信息类型的消息   
                logger.Warn("输出一条警告信息成功");//警告信息，一般用于比较重要的场合   
                logger.Error("输出一条错误信息成功！");//错误信息  
                logger.Fatal("输出一条致命信息成功！");//致命异常信息。一般来讲，发生致命异常之后程序将无法继续执行。   
                tasks.Add(task);
                Task.WhenAll(tasks.ToArray());
                WriteLog(LogLevel.Error, "AA", "A", "a", "aa", "a", "a");
                Console.ReadKey();
            }
            catch (Exception EX)
            {

                throw new Exception(EX.Message);
            }
 
            void WriteLog(LogLevel levle, string appName, string moduleName, string procName, string logLevel, string logTitle, string logMessage)
            {
                LogEventInfo ei = new LogEventInfo(levle, "", "");
                ei.Properties["appName"] = appName;
                ei.Properties["moduleName"] = moduleName;
                ei.Properties["procName"] = procName;
                ei.Properties["logLevel"] = logLevel.ToUpper();
                ei.Properties["logTitle"] = logTitle;
                ei.Properties["logMessage"] = logMessage;
                logger.Log(ei);
            }

        }

        #endregion

        #region 抽象类实现
        /// <summary>
        /// 抽象类实现
        /// </summary>
        static void M2()
        {
            try
            {
                List<Fruit> list = new List<Fruit>();
                Fruit f = new Apple();
                //添加抽象类的子方法。
                list.Add(f);
                var s = new Orange();
                list.Add(s);
                list.ForEach(t =>
                {
                    //添加子类后 那么就开始执行子类实现的方法。列如添加两个子类那么就进入两个子类的实现当中
                    //进入的子类 也是 按顺序进入的
                    t.vendor = "红富士";
                    t.GrowInArea();
                    t.vendor = "柑橘";
                    t.GrowInArea();
                });
                //Fruit f = new Apple();
                //f.vendor = "红富士";
                //f.GrowInArea();

                //f = new Orange();
                //f.vendor = "柑橘";
                //f.GrowInArea();

                Console.ReadKey();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion



    }
}
