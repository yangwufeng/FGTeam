using BLL;
using Models.BLLModel;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class APICareteController : ApiController
    {
        // GET: api/APICarete
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/APICarete/5
        public string Get(int id)
        {
            return "value";
        }

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

        // PUT: api/APICarete/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/APICarete/5
        public void Delete(int id)
        {
        }
    }
}
