using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleRESTServer.Models;
using System.Collections;

namespace SimpleRESTServer.Controllers
{
    /// <summary>
    /// Handles everything about users.
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        // GET: api/User
        public ArrayList Get()
        {
            UserPersistence up = new UserPersistence();

            return up.getUsers();
        }

        /// <summary>
        /// Get specific user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/User/5
        public User Get(int id)
        {
            UserPersistence up = new UserPersistence();

            User user = up.getUser(id);
            if(user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return user;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/User
        public HttpResponseMessage Post([FromBody]User value)
        {
            UserPersistence up = new UserPersistence();
            long id;
            id = up.saveUser(value);
            value.Id = (int)id;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("user/{0}", id));
            return response;
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/User/5
        public HttpResponseMessage Put(int id, [FromBody]User value)
        {
            UserPersistence up = new UserPersistence();
            bool existed = up.updateUser(id, value);
            HttpResponseMessage response;

            if (existed)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/User/5
        public HttpResponseMessage Delete(int id)
        {
            UserPersistence up = new UserPersistence();
            bool existed = up.deleteUser(id);
            HttpResponseMessage response;

            if (existed)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            } else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}
