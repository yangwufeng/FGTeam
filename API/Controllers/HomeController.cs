using Models.APIModel;
using Models.BLLModel;
using Models.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {

            public ActionResult Index1()
            {
                ViewBag.Msg = "成功！！！";
                return View();
            }
            public ActionResult Index()
            {
                using (var client = new HttpClient())
                {
                    //try
                    //{
                    //请求数据
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("https://localhost:44367/api/AGVMutual/Get").Result;
                    var list = response.Content.ReadAsAsync<List<User>>().Result;
                    ViewData.Model = list;
                    return View();
                    //}
                    //catch (Exception ex)
                    //{
                    //    return BLLResultFactory.Sucess($"{ex.Message}");
                    //}
                }
            }
            public async Task<BLLResult> add(User user)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        user.Id = 121;
                        user.UserName = "yangwufeng";
                        user.UserCode = "12";
                        user.Address = "311";
                        user.Created = null;
                        user.CreatedBy = "fg";
                        user.Disable = false;
                        user.Partment = "4";
                        user.Password = "123456";
                        user.Phone = "cs";
                        user.Remark = "测试";
                        user.Updated = null;
                        user.Updatedby = "cs";
                        string postjson = JsonConvert.SerializeObject(user);
                        StringContent content = new StringContent(postjson, Encoding.UTF8, "application/json");
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(20));
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44367/api/AGVMutual/Get", content, cancellationTokenSource.Token).ConfigureAwait(false);

                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        //AppSession
                        var temp = JsonConvert.DeserializeObject<ApiResultModel>(responseBody);
                        if (temp.Code == HttpStatusCode.OK)
                        {
                            return BLLResultFactory.Success(temp.Data, temp.Msg);
                        }
                        else
                        {
                            return BLLResultFactory.Success(temp.Data, temp.Msg);
                        }


                    }
                    catch (Exception ex)
                    {

                        return BLLResultFactory.Error(ex.Message); ;
                    }

                    //HttpResponseMessage response = await client.PostAsync("https://localhost:44367/api/AGVMutual/Get", content, cancellationTokenSource.Token).ConfigureAwait(false);
                }

            }

            //public async Task<BLLResult<T>> Insert<T>(User user)
            //  {
            //  //public List<User> User;
            //      using (var client = new HttpClient())
            //      {
            //          try
            //          {
            //              user.Id = 121;
            //              user.UserName = "YANGWUFENG";
            //              user.UserCode = "12";
            //              user.Address = "311";
            //              user.Created = null;
            //              user.CreatedBy = "FG";
            //              user.Disable = false;
            //              user.Partment = "4";
            //              user.Password = "123456";
            //              user.Phone = "Cs";
            //              user.Remark = "测试";
            //              user.Updated = null;
            //              user.Updatedby = "CS";
            //              string jsonpost = JsonConvert.SerializeObject(user);
            //              StringContent content = new StringContent(jsonpost, Encoding.UTF8, "application/json");
            //              CancellationTokenSource cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            //         HttpResponseMessage response = await client.PostAsync("https://localhost:44367/api/AGVMutual/Get", content, cancellation.Token).ConfigureAwait(false);
            //              response.EnsureSuccessStatusCode();
            //              string resonseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //              var temp = JsonConvert.DeserializeObject<ApiResultModel<T>>(resonseBody);
            //              if (temp.Code == HttpStatusCode.OK)
            //              {
            //                  return BLLResultFactory<T>.Success<T>(temp.Data, temp.Msg);
            //              }
            //              else 
            //              {
            //                  return BLLResultFactory<T>.Success<T>(temp.Data, temp.Msg);
            //              }
            //          }
            //          catch (Exception ex)
            //          {

            //              return BLLResultFactory<T>.Error(ex.Message);

            //          }
            //      }


            //  }
        }
    }