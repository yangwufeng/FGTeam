using BLL;
using Models.APIModel;
using Models.BLLModel;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    public class APICareteController : ApiController
    {
        // GET: api/APICarete
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

       
       /// <summary>
       /// 查询用户
       /// </summary>
       /// <param name="Id"></param>
       /// <returns></returns>
       [HttpPost]
         public BLLResult<QueryUser> SelectUserById([FromBody]QueryUserId Id)
        {
            try
            {
                var result = AppSession.TaskService.SelectUser(Id.Id);
                return result;
            }
            catch (Exception ex)
            {
                return BLLResultFactory<QueryUser>.Error(ex.Message);
            }
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public BLLResult CreateUser([FromBody] User user)
        {
            try
            {
                var result = AppSession.TaskService.CreateUser(user);
                if (result.Success)
                {
                    return result;
                }
                else
                {
                    return BLLResultFactory.Error(result.Msg);
                }
            }
            catch (Exception ex)
            {
                return BLLResultFactory.Error(ex.Message);
            }
        }



      
    
       /// <summary>
       /// 添加报警信息
       /// </summary>
       /// <param name="srminfo"></param>
       /// <returns></returns>
        [HttpPost]
        public BLLResult SrmStatusUpdate([FromBody] SRMStatusUpdateRequest srminfo)
        {
            try
            {
                var result = AppSession.TaskService.CreateSRMInfor(srminfo);
                if (result.Success)
                {
                    return result;
                }
                else
                {
                    return BLLResultFactory.Error(result.Msg);
                }
            }
            catch (Exception ex)
            {
                return BLLResultFactory.Error(ex.Message);
            }
        }



        /// <summary>
        /// 添加位置
        /// </summary>
        /// <param name="srminfo"></param>
        /// <returns></returns>
        [HttpPost]
        public BLLResult AddSize([FromBody] EquipmentSiteRequest srminfo)
        {
            try
            {
                var result = AppSession.TaskService.AddSize(srminfo);
                if (result.Success)
                {
                    return result;
                }
                else
                {
                    return BLLResultFactory.Error(result.Msg);
                }
            }
            catch (Exception ex)
            {
                return BLLResultFactory.Error(ex.Message);
            }
        }

        // PUT: api/APICarete/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/APICarete/5
        public void Delete(int id)
        {
        }
    }
}
