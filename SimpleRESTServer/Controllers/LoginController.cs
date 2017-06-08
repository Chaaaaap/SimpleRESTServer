using SimpleRESTServer.Models;
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
        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        //GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Used by visitors to login.
        /// </summary>
        /// <param name="login"></param>
        // POST: api/Login
        [HttpPost]
        public User Post(Login login)
        {
            LoginPersistence lp = new LoginPersistence();
            User userLoggedIn = lp.Login(login.UserName, login.Password);

            if (userLoggedIn != null)
            {
                return userLoggedIn;
            } else
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
        }
        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="id"></param>
        //// DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
