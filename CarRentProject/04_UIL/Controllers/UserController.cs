using _02_BOL;
using _03_BLL;
using _04_UIL.Filters;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace _04_UIL.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        // GET: api/User
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<UserModel[]>(UserManager.SelectAllUsers(), new JsonMediaTypeFormatter())
            };
        }


        public HttpResponseMessage Get(string userName)
        {
            UserModel user = UserManager.SelectUserByUserName(userName);
            if (user != null)
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<UserModel>(user, new JsonMediaTypeFormatter())
                };

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }



        public HttpResponseMessage Get(string email, string password)
        {
            UserModel user = UserManager.SelectUserByEmailAndPassword(email, password);
            if (user != null)
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<UserModel>(user, new JsonMediaTypeFormatter())
                };

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        // POST: api/User
        public HttpResponseMessage Post([FromBody]UserModel value)
        {
            bool insertResult = false;


            if (ModelState.IsValid)
            {
                insertResult = UserManager.InsertUser(value);
            }

            HttpStatusCode responseCode = insertResult ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(insertResult, new JsonMediaTypeFormatter()) };
        }


        // PUT: api/User/
        public HttpResponseMessage Put(string userName, [FromBody]UserModel value)
        {
            bool updateResult = false;


            if (ModelState.IsValid)
            {
                updateResult = UserManager.UpdateUserByUserName(userName, value);
            }

            HttpStatusCode responseCode = updateResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(updateResult, new JsonMediaTypeFormatter()) };

        }

        // DELETE: api/User/
        public HttpResponseMessage Delete(string userName)
        {
            bool deleteResult = UserManager.DeleteUserByUserName(userName);

            HttpStatusCode responseCode = deleteResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(deleteResult, new JsonMediaTypeFormatter()) };
        }
    }
}

