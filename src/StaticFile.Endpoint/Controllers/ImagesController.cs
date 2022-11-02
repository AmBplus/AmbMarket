using ambMarket.Application.Interfaces.UriComposer;
using Microsoft.AspNetCore.Mvc;

namespace StaticFile.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IUriImageComposer UriImageComposer;
        public ImagesController(IWebHostEnvironment hostingEnvironment, IUriImageComposer uriImageComposer)
        {
            _environment = hostingEnvironment;
            UriImageComposer = uriImageComposer;
        }
        [HttpPost]
        public IActionResult Post(string apiKey)
        {
            if (apiKey != "mysecretkey")
            {
                return BadRequest();
            }
            try
            {
                var files = Request.Form.Files;
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files != null)
                {
                    //upload
                    return Ok(UploadFile(files));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error");
            }


        }
        [NonAction]
        private UploadDto UploadFile(IFormFileCollection files)
        {
            string newName = Guid.NewGuid().ToString();
            var date = DateTime.Now;
            string folder = $@"Resources\images\{date.Year}\{date.Year}-{date.Month}\";
            var webRootPath = _environment.WebRootPath;
            var uploadsRootFolder = Path.Combine(webRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder)) 
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }
            List<string> address = new List<string>();
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = newName + file.FileName;
                    var filePath = Path.Combine(uploadsRootFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    address.Add(UriImageComposer.UriComposer(folder + fileName));
                }
            }
            return new UploadDto()
            {
                FileNameAddress = address,
                Status = true,
            };
        }

    }
    
    public class UploadDto
    {
        public bool Status { get; set; }
        public List<string> FileNameAddress { get; set; }
    }
}

