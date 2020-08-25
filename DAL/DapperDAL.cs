using Dapper;
using Models.BLLModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DapperDAL : DALBase
    {
        public DapperDAL(string connectStr) : base(connectStr)
        {
        }

        public override IDbConnection GetConnection()
        {
            return new MySqlConnection(ConnectStr);
        }

        #region Data
        public override BLLResult<List<T>> GetCommonModelBy<T>(string v)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> list = connection.GetList<T>(v).ToList();
                    if (list != null && list.Count > 0)
                    {
                        return BLLResultFactory<List<T>>.Success(list, "成功");
                    }
                    else
                    {
                        return BLLResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BLLResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }


        public override BLLResult<int?> InsertCommonModel<T>(T model)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    return BLLResultFactory<int?>.Success(connection.Insert<T>(model), "成功");
                }
                catch (Exception ex)
                {
                    return BLLResultFactory<int?>.Error("发生异常:" + ex.Message);
                }
            }
        }
        #endregion
    }
}
