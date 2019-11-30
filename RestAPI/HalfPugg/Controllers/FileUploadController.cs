using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HalfPugg.Controllers
{
    public class FileUploadController : ApiController
    {
        public const string ImageDir = "~/App_Data/images/";

        [HttpPost]
        [Route("api/ImageUpload")]
        public async Task<IHttpActionResult> UploadImage()
        {

            if(!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath(ImageDir);
            var multiPartProvider = new MultipartFileStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(multiPartProvider);

                foreach(var file in multiPartProvider.FileData)
                {
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, localFileName);

                    File.Move(localFileName, filePath);
                    string imageName = filePath.Split('\\').Last();
                    return Ok(imageName);
                }
            } catch(Exception e)
            {
                return BadRequest("Erro: " + e.Message);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/GetImage")]
        public HttpResponseMessage GetImage(string imagePath)
        {
            string filePath = ImageDir + imagePath;

            var result = new HttpResponseMessage(HttpStatusCode.OK);

            FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath(filePath), FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream mstream = new MemoryStream();
            image.Save(mstream, ImageFormat.Jpeg);

            result.Content = new ByteArrayContent(mstream.ToArray());
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return result;
        }

    }
}
