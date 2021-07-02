using Models.BLLModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class DALBase
    {
        public string ConnectStr { get; set; }

        public abstract IDbConnection GetConnection();

        protected DALBase(string connectStr)
        {
            ConnectStr = connectStr;
        }
        #region Data
        public abstract BllResult<List<T>> GetCommonModelBy<T>(string v);

        public abstract BllResult<int?> InsertCommonModel<T>(T model);
        #endregion
    }
}
