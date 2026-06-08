using Microsoft.AspNetCore.Mvc;

namespace MineralCollection.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ImageController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Keine Datei");

        var imagesPath = GetImagesPath();

        // Erstelle den Ordner, falls er fehlt
        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        var fileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + file.FileName;
        var fullPath = Path.Combine(imagesPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { fileName });
    }

    [HttpDelete("{fileName}")]
    public IActionResult DeleteImage(string fileName)
    {
        var imagesPath = GetImagesPath();
        var filePath = Path.Combine(imagesPath, fileName);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return Ok();
        }
        return NotFound();
    }

    private string GetImagesPath()
    {
        var configuredPath = _configuration["ImageStorage:Path"];
        if (!string.IsNullOrWhiteSpace(configuredPath))
        {
            return Path.GetFullPath(configuredPath);
        }

        var currentDir = Directory.GetCurrentDirectory();
        return Path.GetFullPath(Path.Combine(currentDir, "..", "MineralCollection.Frontend", "wwwroot", "images"));
    }
}
