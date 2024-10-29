using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadImageToAFolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        // Endpoint to handle image upload
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            // Check if no file is uploaded
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded.");

            // Directory where files will be uploaded
            var uploadDirectory = @"C:\MyUploads";

            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Ensure the uploads directory exists, create if it doesn't
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the file path as a response
            return Ok(new { filePath });
        }

       

    }
}
