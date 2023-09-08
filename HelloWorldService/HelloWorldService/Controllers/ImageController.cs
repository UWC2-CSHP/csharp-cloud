using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers; // ADD ME

namespace HelloWorldService.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        [HttpGet("{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var uploadsFileName = Path.Combine("Uploads", fileName.Trim());

            if (!System.IO.File.Exists(uploadsFileName)) 
            {
                return NotFound(null);
            }

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), uploadsFileName);

            // Option 1: READ ENTIRE FILE INTO MEMORY
            //var fileBytes = System.IO.File.ReadAllBytes(fullPath);

            //return File(fileBytes, "image/png", fileName);

            // Option 2: stream it
            var stream = new FileStream(fullPath, FileMode.Open);
            return File(stream, "image/png");
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage()
        {
            var formCollection = await Request.ReadFormAsync();

            var files = formCollection.Files;

            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            foreach (var file in files)
            {
                var fileName = file.FileName.Trim();

                var fullPath = Path.Combine(pathToSave, fileName);

                // Copies the file locally
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            var keys = formCollection.Keys;
            var v2 = formCollection[keys.First()];

            return Ok();
        }
    }
}
