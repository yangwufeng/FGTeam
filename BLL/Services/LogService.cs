using Models.BLLModel;
using Models.Enums;
using Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    /// <summary>
    /// 日志相关，取代LogExecute
    /// </summary>
    public class LogService : BaseService
    {

        public LogService(string path)
        {
            LogPath = path;
        }

        /// <summary>
        /// 设置日志保存路径
        /// </summary>
        public string LogPath { get; set; }

        public const string ExceptionTag = "ExceptionTag";

        /// <summary>
        /// 信息跟踪  Info  记录形式为： 时间+信息
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="IsSucc"></param>
        public void WriteInfoLog(string Message, bool IsSucc)
        {
            string s = IsSucc ? "成功" : "失败";
            WriteInfoLog(Message + ",操作结果[" + s + "]");
        }

        /// <summary>
        /// 数据库操作异常信息跟踪 DBExecute 
        /// </summary>
        /// <param name="ex"></param>
        public void WriteDBExceptionLog(Exception ex)
        {
            WriteExceptionLog("DBExecute", ex);
        }

        /// <summary>
        /// 自定义文件名称的数据操作异常信息跟踪 title
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="ex"></param>
        public void WriteExceptionLog(string tile, Exception ex)
        {
            try
            {
                if (ex != null && ex.Message != ExceptionTag)
                {
                    StringBuilder sb = new StringBuilder();
                    string NowDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    sb.AppendLine(string.Format("****************************{0},Exception[{1}]****************************", NowDateTime, tile));
                    sb.AppendLine(ex.ToString());
                    WriteLogExecute("Exception", sb.ToString());
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Info 日志记录函数
        /// </summary>
        /// <param name="Message"></param>
        public void WriteInfoLog(string Message)
        {
            WriteLogExecute("Info", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
        }

        /// <summary>
        /// 自定义日志文件名称的日志记录函数 title开头
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Message"></param>
        public void WriteLog(string title, string Message)
        {
            WriteLogExecute(title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
        }

        /// <summary>
        /// 重要数据记录日志函数 Data
        /// </summary>
        /// <param name="Message"></param>
        public void WriteLineDataLog(string Message)
        {
            WriteLogExecute("Data", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
        }


        void WriteLogExecute(string FileName, string Message)
        {
            //如果日志文件目录不存在,则创建
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            string filename = LogPath + "\\" + FileName + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt";

            try
            {
                FileStream fs = new FileStream(filename, FileMode.Append);
                StreamWriter strwriter = new StreamWriter(fs);
                try
                {
                    strwriter.WriteLine(Message);
                    strwriter.Flush();
                }
                catch
                {
                }
                finally
                {
                    strwriter.Close();
                    strwriter = null;
                    fs.Close();
                    fs = null;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 记录普通日志到数据库
        /// </summary>
        /// <param name="title"></param>
        /// <param name="log"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        //public BllResult<int?> LogContent(LogTitle logTitle, string log, string userCode, LogLevel flag)
        //{
        //    ContentLog contentLog = new ContentLog();
        //    contentLog.Title = logTitle.ToString();
        //    contentLog.Content = log;
        //    contentLog.Flag = flag.ToString();
        //    contentLog.Created = DateTime.Now;
        //    contentLog.CreatedBy = userCode.ToString();
        //    return AppSession.Dal.InsertCommonModel<ContentLog>(contentLog);
        //}

        /// / <summary>
        /// 记录接口日志到数据库
        // / </summary>
        /// <param name = "interfaceName" ></ param >
        // / < param name="request"></param>
        //  / <param name = "response" ></ param >
        // / < param name="flag"></param>
        /// <returns></returns>
        public BLLResult<int?> LogInterface(string interfaceName, string request, string response,LogLevel flag, string content, string remark)
        {
            InterfaceLog interfaceLog = new InterfaceLog();
            interfaceLog.InterfaceName = interfaceName;
            interfaceLog.Request = request;
            interfaceLog.Response = response;
            interfaceLog.Flag = flag.ToString();
            interfaceLog.Content = content;
            interfaceLog.Remark = remark;
            interfaceLog.CreatedBy = Accounts.YWF.ToString();
            interfaceLog.Created = DateTime.Now;
            return AppSession.DAL.InsertCommonModel<InterfaceLog>(interfaceLog);
        }

        /// <summary>
        /// 记录业务返回日志
        /// </summary>
        /// <param name="bllResult"></param>
        /// <param name="title"></param>
        /// <param name="userCode"></param>
        //public void LogBllResult(BllResult bllResult, LogTitle title, string userCode)
        //{
        //    LogContent(title, bllResult.Msg, userCode, bllResult.Success ? LogLevel.Success : LogLevel.Failure);
        //}

        /// <summary>
        /// 记录业务返回日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bllResult"></param>
        /// <param name="title"></param>
        /// <param name="userCode"></param>
        //public void LogBllResult<T>(BllResult<T> bllResult, LogTitle title, string userCode)
        //{
        //    LogContent(title, bllResult.Msg, userCode, bllResult.Success ? LogLevel.Success : LogLevel.Failure);
        //}
        //public BLLResult<int?> LogContent(LogTitle logTitle, string log, string userCode, LogLevel flag)
        //{
        //    ContentLog contentLog = new ContentLog();
        //    contentLog.Title = logTitle.ToString();
        //    contentLog.Content = log;
        //    contentLog.Flag = flag.ToString();
        //    contentLog.Created = DateTime.Now;
        //    contentLog.CreatedBy = userCode.ToString();
        //    return AppSession.Dal.InsertCommonModel<ContentLog>(contentLog);
        //}
    }
}
