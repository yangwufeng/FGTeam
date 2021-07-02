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
        public BllResult CreateUser(User user)
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
                        return BllResultFactory.Success(result.Msg);
                    }
                    else
                    {
                        trans.Rollback();
                        return BllResultFactory.Error(result.Msg);
                    }
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    return BllResultFactory.Error($"创建任务的时候出现异常{ex.Message}");
                }
            }
        }

        public BllResult CreateSRMInfor(SRMStatusUpdateRequest srminfo)
        {
            using (IDbConnection connection = AppSession.DAL.GetConnection())
            {
                IDbTransaction trans = null;
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                    var result = CreateSRMInfos(connection, trans, srminfo);
                    if (result.Success)
                    {
                        trans.Commit();
                        return BllResultFactory.Success();
                    }
                    else
                    {
                        trans.Rollback();
                        return BllResultFactory.Error(result.Msg);
                    }
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    return BllResultFactory.Error($"创建任务的时候出现异常{ex.Message}");
                }
            }
        }


        public BllResult AddSize(EquipmentSiteRequest srminfo)
        {
            using (IDbConnection connection = AppSession.DAL.GetConnection())
            {
                IDbTransaction trans = null;
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                    var result = AddSizeS(connection, trans, srminfo);
                    if (result.Success)
                    {
                        trans.Commit();
                        return BllResultFactory.Success();
                    }
                    else
                    {
                        trans.Rollback();
                        return BllResultFactory.Error(result.Msg);
                    }
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    return BllResultFactory.Error($"创建任务的时候出现异常{ex.Message}");
                }
            }
        }

        private BllResult CreateUser(IDbConnection connection, IDbTransaction transaction, User user)
        {
            var temp = AppSession.DAL.InsertCommonModel<User>(user);
            if (temp.Success)
            {
                return BllResultFactory.Success();
            }
            else
            {
                return BllResultFactory.Error(temp.Msg);
            }
        }

        private BllResult AddSizeS(IDbConnection connection, IDbTransaction transaction, EquipmentSiteRequest srminfos)
        {

            try
            {
                var temp = AppSession.DAL.InsertCommonModel<EquipmentSiteRequest>(srminfos);
                if (temp.Success)
                {
                    return BllResultFactory.Success();
                }
                else
                {
                    return BllResultFactory.Error(temp.Msg);
                }
            }
            catch (Exception ex)
            {

                return BllResultFactory.Error(ex.Message);
            }

        }

        private BllResult CreateSRMInfos(IDbConnection connection, IDbTransaction transaction, SRMStatusUpdateRequest srminfos)
        {

            try
            {
                var temp = AppSession.DAL.InsertCommonModel<SRMStatusUpdateRequest>(srminfos);
                if (temp.Success)
                {
                    return BllResultFactory.Success();
                }
                else
                {
                    return BllResultFactory.Error(temp.Msg);
                }
            }
            catch (Exception ex)
            {

                return BllResultFactory.Error(ex.Message);
            }

        }
        public BllResult<QueryUser> SelectUser(int Id)
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
                    return BllResultFactory<QueryUser>.Success(item);
                }
                else
                {
                    return BllResultFactory<QueryUser>.Error(result.Msg);
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<QueryUser>.Error(ex.Message);
            }
        }




        private BllResult<User> SelectByUserId(int Id)
        {
            var temp = AppSession.DAL.GetCommonModelBy<User>($"where Id={Id}");
            if (temp.Success && temp.Data.Count > 0)
            {
                return BllResultFactory<User>.Success(temp.Data[0]);
            }
            return BllResultFactory<User>.Error(temp.Msg);
        }
    }
}
