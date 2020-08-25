using BLL;
using Models.APIModel;
using Models.BLLModel;
using Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace API.Common
{
    public class ApiResultAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                return;
            }
            base.OnActionExecuted(actionExecutedContext);
            string requestString = "";
            var stream = actionExecutedContext.Request.Content.ReadAsStreamAsync().Result;
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                requestString = reader.ReadToEnd();
            }
            ApiResultModel result = new ApiResultModel();
            var bllResule = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<BLLResult>().Result;
            if (bllResule.Success)
            {
                result.Code = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.Code = System.Net.HttpStatusCode.BadRequest;
            }
            result.Data = bllResule.Data;
            result.Msg = bllResule.Msg;

            AppSession.LogService.LogInterface(actionExecutedContext.ActionContext.ActionDescriptor.ActionName, requestString, JsonConvert.SerializeObject(result), result.Code == System.Net.HttpStatusCode.OK ? LogLevel.Success : LogLevel.Error, "", "");
            //回复结果 以及数据
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}