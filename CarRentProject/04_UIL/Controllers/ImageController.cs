using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Formatting;

namespace _04_UIL.Controllers
{
    public class ImageController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [HttpPost]
        [Route("api/UploadImage")]
        public HttpResponseMessage UploadImage()
        {
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            string imageFolder = httpRequest.Form["ImageFolder"];
            var postedFile = httpRequest.Files["Image"];
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff");
            var filePath = HttpContext.Current.Server.MapPath($"~/{imageFolder}/" + imageName);
            postedFile.SaveAs(filePath);
            //return Request.CreateResponse(HttpStatusCode.Created);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
              Content = new ObjectContent<string>(imageName, new JsonMediaTypeFormatter())
            };
        }

    }

}

