using Models.APIModel;
using Models.BLLModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommonService : BaseService
    {

        public async Task<BllResult<T>> PostJson<T>(Object obj)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string postJsonString = JsonConvert.SerializeObject(obj);
                    StringContent content = new StringContent(postJsonString, Encoding.UTF8, "application/json");
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(20));
                    HttpResponseMessage response = await client.PostAsync("http://172.16.27.52:120/api/AGVMutual/Get", content, cancellationTokenSource.Token).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();//用来抛异常的
                    string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //AppSession.LogService.LogInterface(title, postJsonString, responseBody, LogLevel.Info, "", "");
                    var temp = JsonConvert.DeserializeObject<ApiResultModel<T>>(responseBody);
                    if (temp.Code == System.Net.HttpStatusCode.NoContent)//成功
                    {
                        return BllResultFactory<T>.Success(temp.Data, temp.Msg);
                    }
                    else
                    {
                        //AppSession.LogService.LogInterface(title, postJsonString, responseBody, LogLevel.Failure, "", "");
                        return BllResultFactory<T>.Error(temp.Data, temp.Msg);
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Log($"请求错误：{ex.Message}", LogLevel.Exception);
                    //AppSession.LogService.WriteExceptionLog(title, ex);
                    return BllResultFactory<T>.Error(ex.Message); ;
                }
            }
        }

    }
}
