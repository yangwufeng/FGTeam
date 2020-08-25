using Models.BLLModel;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TaskService
    {
        public BLLResult CreateUser(User user)
        {
            using (IDbConnection connection = AppSession.DAL.GetConnection())
            {
                IDbTransaction trans = null;
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                    var result = CreateUser(connection, trans, user);
                    if (result.Success)
                    {
                        trans.Commit();
                        return BLLResultFactory.Success(result.Msg);
                    }
                    else
                    {
                        trans.Rollback();
                        return BLLResultFactory.Error(result.Msg);
                    }
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    return BLLResultFactory.Error($"创建任务的时候出现异常{ex.Message}");
                }
            }
        }

        private BLLResult CreateUser(IDbConnection connection, IDbTransaction transaction, User user)
        {
            var temp = AppSession.DAL.InsertCommonModel<User>(user);
            if (temp.Success)
            {
                return BLLResultFactory.Success();
            }
            {
                return BLLResultFactory.Error(temp.Msg);
            }
        }

    }
}
