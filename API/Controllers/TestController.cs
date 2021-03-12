using BLL;
using Models.APIModel;
using Models.BLLModel;
using Models.RequestEntity;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class TestController : ApiController
    {
        private Response Result = new Response();
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public BLLResult<Response> GetCC(string  entity)
        {
            try
            {
                List<credentials> list = new List<credentials>();

                credentials model = new credentials();
                model.certificateNo = "123";
                model.flag = 0;
                model.idfCode = "123";
                model.isNG = "123";
                model.lineNo = "123";
                model.name = "123";
                model.remark = "123";
                model.url = "123";
                credentials model1 = new credentials();
                model1.certificateNo = "123";
                model1.flag = 1;
                model1.idfCode = "123";
                model1.isNG = "123";
                model1.lineNo = "123";
                model1.name = "123";
                model1.remark = "123";

                credentials model2 = new credentials();
                model2.certificateNo = "123";
                model2.flag = 2;
                model2.idfCode = "123";
                model2.isNG = "123";
                model2.lineNo = "123";
                model2.name = "123";
                model2.remark = "123";
                list.Add(model);
                list.Add(model1);
                list.Add(model2);

                Result.Result = list;
                return BLLResultFactory<Response>.Success(Result);
            }
            catch (Exception ex)
            {
                return BLLResultFactory<Response>.Error(Result.Message=ex.Message);
            }
        }
    }
}
