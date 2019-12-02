using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/Images");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }
                var URL = Url.Content(Path.Combine("~/Images", files[0]));
                return Ok(URL);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return alfanumericoAleatorio(40) + "_" + headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
        public static string alfanumericoAleatorio(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
