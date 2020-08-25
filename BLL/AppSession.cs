using BLL.Services;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class AppSession
    {
        static AppSession()
        {
            Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.MySQL);
        }
        public static string ConnectionString { get; set; } = ConfigurationManager.AppSettings["ConnectionStr"];

        public static DALBase DAL { get; set; } = new DapperDAL(ConnectionString);

        public static CommonService CommonService { get; set; } = new CommonService();


        public static TaskService TaskService { get; set; } = new TaskService();
        #region 日志记录
        public static string LogPath { get; set; } = ConfigurationManager.AppSettings["LogPath"] == null ? "D//FG//WCS/Log" : ConfigurationManager.AppSettings["LogPath"];
        public static LogService LogService { get; set; } = new LogService(LogPath);
        #endregion
    }
}
