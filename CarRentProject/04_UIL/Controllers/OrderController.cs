using _02_BOL;
using _03_BLL;
using _04_UIL.Filters;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace _04_UIL.Controllers
{
    [EnableCors("*", "*", "*")]
    public class OrderController : ApiController
    {


        // GET: api/Order
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<OrderModel[]>(OrderManager.SelectAllOrders(), new JsonMediaTypeFormatter())
            };
        }


        public HttpResponseMessage Get(DateTime startRent, int carNumber)
        {
            OrderModel order = OrderManager.GetOrderByStartRentAndCarNumber(startRent, carNumber);
            if (order != null)
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<OrderModel>(order, new JsonMediaTypeFormatter())
                };

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        // POST: api/Order
        public HttpResponseMessage Post([FromBody]OrderModel value)
        {
            bool insertResult = false;

            if (ModelState.IsValid)
            {
                insertResult = OrderManager.InsertOrder(value);
            }

            HttpStatusCode responseCode = insertResult ? HttpStatusCode.Created : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(insertResult, new JsonMediaTypeFormatter()) };
        }


        // PUT: api/Order/5
        public HttpResponseMessage Put(DateTime startRent, int carNumber, [FromBody]OrderModel value)
        {
            bool updateResult = false;

            if (ModelState.IsValid)
            {
                updateResult = OrderManager.UpdateOrderByStartDateAndCarNumber(startRent, carNumber, value);
            }

            HttpStatusCode responseCode = updateResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(updateResult, new JsonMediaTypeFormatter()) };

        }

        // DELETE: api/Order/
        public HttpResponseMessage Delete(DateTime startRent, int carNumber)
        {
            bool deleteResult = OrderManager.DeleteOrderByStartDateAndCarNumber(startRent, carNumber);

            HttpStatusCode responseCode = deleteResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            return new HttpResponseMessage(responseCode) { Content = new ObjectContent<bool>(deleteResult, new JsonMediaTypeFormatter()) };
        }
    }
}

