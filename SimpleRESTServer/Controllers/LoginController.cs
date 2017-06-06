using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleRESTServer.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Used by visitors to login.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        // POST: api/Login
        public void Post([FromBody]string username, [FromBody]string password)
        {
            LoginPersistence lp = new LoginPersistence();
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
