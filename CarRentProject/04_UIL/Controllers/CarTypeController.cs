using _02_BOL;
using _03_BLL;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace _04_UIL.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CarTypeController : ApiController
    {

        // GET: api/CarType
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<CarTypeModel[]>(CarTypeManager.SelectAllCarTypes(), new JsonMediaTypeFormatter())
            };
        }

        // GET: api/CarType/1
        public HttpResponseMessage Get(string make, string model, int year)
        {
            CarTypeModel carType = CarTypeManager.GetCarTypeByMakeModelYear(make, model, year);
            if (carType != null)
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<CarTypeModel>(carType, new JsonMediaTypeFormatter())
                };

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        // POST: api/CarType
        public HttpResponseMessage Post([FromBody]CarTypeModel value)
        {
            bool insertResult = false;

            if (ModelState.IsValid)
            {
                insertResult = CarTypeManager.InsertNewCarType(value);
            }

            HttpStatusCode responseCode = insertResult ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(insertResult, new JsonMediaTypeFormatter()) };
        }

        // PUT: api/CarType/
        public HttpResponseMessage Put(string make, string model, int year, [FromBody]CarTypeModel value)
        {
            bool updateResult = false;


            if (ModelState.IsValid)
            {
                updateResult = CarTypeManager.UpdateCarTypeByMakeModelYear(make, model, year, value);
            }

            HttpStatusCode responseCode = updateResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(updateResult, new JsonMediaTypeFormatter()) };

        }

        // DELETE: api/CarType/
        public HttpResponseMessage Delete(string make, string model, int year)
        {
            bool deleteResult = CarTypeManager.DeleteCarTypeByMakeModelYear(make, model, year);

            HttpStatusCode responseCode = deleteResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(deleteResult, new JsonMediaTypeFormatter()) };
        }
    }
}

