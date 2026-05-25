using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineralCollection.API.Data;
using MineralCollection.Domain;

namespace MineralCollection.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public ImageController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Keine Datei");

        var currentDir = Directory.GetCurrentDirectory();
        var imagesPath = Path.GetFullPath(Path.Combine(currentDir, "..", "MineralCollection.Frontend", "wwwroot", "images"));

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
        var currentDir = Directory.GetCurrentDirectory();
        var imagesPath = Path.GetFullPath(Path.Combine(currentDir, "..", "MineralCollection.Frontend", "wwwroot", "images"));
        var filePath = Path.Combine(imagesPath, fileName);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return Ok();
        }
        return NotFound();
    }

}