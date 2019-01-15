using _02_BOL;
using _03_BLL;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace _04_UIL.Controllers
{
    [EnableCors("*", "*", "*")]
 
    public class CarController : ApiController
    {
        // GET: api/Car
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<CarModel[]>(CarManager.SelectAllCars(), new JsonMediaTypeFormatter())
            };
        }


        public HttpResponseMessage Get(int carNumber)
        {
            CarModel car = CarManager.SelectCarByCarNumber(carNumber);
            if (car != null)
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<CarModel>(car, new JsonMediaTypeFormatter())
                };

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }



        public HttpResponseMessage Post([FromBody]CarModel value)
        {
            bool insertResult = false;

            //ModelState is the parameter that we got to the Post function (value in our case)
            if (ModelState.IsValid)
            {
                insertResult = CarManager.InsertNewCar(value);
            }

            HttpStatusCode responseCode = insertResult ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(insertResult, new JsonMediaTypeFormatter()) };
        }



       
        public HttpResponseMessage Put(int carNumber, [FromBody]CarModel value)
        {
            bool updateResult = false;

            //ModelState is the parameter that we got to the Post function (value in our case)
            if (ModelState.IsValid)
            {
                updateResult = CarManager.UpdateCarByCarNumber(carNumber, value);
            }

            HttpStatusCode responseCode = updateResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(updateResult, new JsonMediaTypeFormatter()) };

        }


        // DELETE: api/Car/
        public HttpResponseMessage Delete(int carNumber)
        {
            bool deleteResult = CarManager.DeleteCarByCarNumber(carNumber);

            HttpStatusCode responseCode = deleteResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(deleteResult, new JsonMediaTypeFormatter()) };
        }

        public HttpResponseMessage Get(DateTime startRent, DateTime endRent)
        {
            CarModel[] availableCars = CarManager.CheckAvailibility(startRent, endRent);

            if (availableCars != null)
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<CarModel[]>(availableCars, new JsonMediaTypeFormatter())
                };

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}

