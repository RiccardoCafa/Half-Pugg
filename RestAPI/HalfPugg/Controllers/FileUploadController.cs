using System;
using System.Collections.Generic;
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
        [HttpPost]
        [Route("api/ImageUpload")]
        public async Task<string> UploadImage()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data");
            var multiPartProvider = new MultipartFileStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(multiPartProvider);

                foreach(var file in multiPartProvider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;

                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    File.Move(localFileName, filePath);

                    return filePath;
                }
            } catch(Exception e)
            {
                return "Erro: " + e.Message;
            }

            return "File Not Uploaded";
        }

    }
}
