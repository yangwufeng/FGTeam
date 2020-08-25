using Models.APIModel;
using Models.BLLModel;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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

        public BLLResult<QueryUser> SelectUser(int Id)
        {
            try
            {
                var result = SelectByUserId(Id);
                if (result.Success)
                {
                    QueryUser item = new QueryUser();
                    var data = result.Data;
                    item.Password = data.Password;
                    item.UserName = data.UserName;
                    item.Address = data.Address;
                    return BLLResultFactory<QueryUser>.Success(item);
                }
                else 
                {
                    return BLLResultFactory<QueryUser>.Error(result.Msg);
                }
            }
            catch (Exception ex)
            {
                return BLLResultFactory<QueryUser>.Error(ex.Message);
            }
        }
        private BLLResult<User> SelectByUserId(int Id)
        {
     var temp=AppSession.DAL.GetCommonModelBy<User>($"where Id={Id}");
            if (temp.Success&&temp.Data.Count>0)
            {
                return BLLResultFactory<User>.Success(temp.Data[0]);
            }
            return BLLResultFactory<User>.Error(temp.Msg);
        }
    }
}
