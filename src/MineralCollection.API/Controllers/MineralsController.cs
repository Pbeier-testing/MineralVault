using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineralCollection.API.Data;
using MineralCollection.Domain;

namespace MineralCollection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MineralsController : ControllerBase
{
    private readonly AppDbContext _context;

    // Datenbank-Kontext wird injiziert, um Verbindung zur DB herzustellen
    public MineralsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/minerals
    [HttpGet]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mineral>>> GetMinerals()
    {
        return await _context.Minerals
                             .Include(m => m.Images) // Eager Loading: Bilder direkt mitladen
                             .ToListAsync();
    }

    // GET: api/minerals/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Mineral>> GetMineral(int id)
    {
        var mineral = await _context.Minerals.FindAsync(id);

        if (mineral == null)
        {
            // Falls die ID nicht existiert: 404 Not Found
            return NotFound();
        }

        return mineral;
    }

    // POST: api/minerals
    [HttpPost]
    public async Task<ActionResult<Mineral>> PostMineral(Mineral mineral)
    {
        // Fügt ein neues Mineral der DB hinzu
        _context.Minerals.Add(mineral);
        await _context.SaveChangesAsync();

        // Gibt einen 201 Statuscode zurück
        return CreatedAtAction(nameof(GetMinerals), new { id = mineral.Id }, mineral);
    }

    // DELETE: api/minerals/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMineral(int id)
    {
        var mineral = await _context.Minerals.FindAsync(id);
        if (mineral == null)
        {
            return NotFound(); // 404, falls die ID nicht existiert
        }

        _context.Minerals.Remove(mineral);
        await _context.SaveChangesAsync();

        return NoContent(); // 204, erfolgreiches Löschen ohne Rückgabewert
    }

    // PUT: api/minerals/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMineral(int id, Mineral mineral)
    {
        // 1. Check: Stimmt die ID in der URL mit der ID im Body überein?
        if (id != mineral.Id)
        {
            return BadRequest("ID mismatch"); // 400
        }

        // Markiert das Objekt als modified, damit EF Core ein SQL UPDATE generiert
        _context.Entry(mineral).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // 2. Check: Existiert das Mineral überhaupt noch?
            if (!_context.Minerals.Any(e => e.Id == id))
            {
                return NotFound(); // 404
            }
            else
            {
                throw;
            }
        }

        return NoContent(); // 204: Erfolgreich aktualisiert, kein Rückgabe-Inhalt nötig
    }

    [HttpPost("import-csv")]
    public async Task<IActionResult> ImportCsv()
    {
        var path = "Mineralien3.csv"; // Liegt im API-Ordner
        if (!System.IO.File.Exists(path)) return NotFound("CSV nicht gefunden");

        using var reader = new System.IO.StreamReader(path, System.Text.Encoding.Default);
        // Header überspringen
        await reader.ReadLineAsync();

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            // Dieser Regex-Trick splittet nur an Kommas, die NICHT in Anführungszeichen stehen
            var parts = System.Text.RegularExpressions.Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            try
            {
                var mineral = new Mineral
                {
                    Nummer = parts[1].Trim('"'),
                    Name = parts[2].Trim('"'),
                    Begleitmineral = parts[3].Trim('"'),
                    Fundort = parts[4].Trim('"'),
                    Region = parts[5].Trim('"'),
                    Land = parts[6].Trim('"'),
                    Bemerkungen = parts[11].Trim('"')
                };

                // Koordinaten splitten: "lat, lon" -> 47.65, 23.82
                var coords = parts[7].Trim('"').Split(',');
                if (coords.Length == 2 && double.TryParse(coords[0], System.Globalization.CultureInfo.InvariantCulture, out var lat))
                {
                    mineral.Breitengrad = lat;
                    if (double.TryParse(coords[1], System.Globalization.CultureInfo.InvariantCulture, out var lon))
                        mineral.Laengengrad = lon;
                }

                // Bilder 1 bis 6 prüfen
                for (int i = 12; i <= 17; i++)
                {
                    var imgName = parts[i].Trim('"');
                    if (!string.IsNullOrWhiteSpace(imgName))
                    {
                        mineral.Images.Add(new MineralImage { FileName = imgName + ".JPG" });
                    }
                }

                _context.Minerals.Add(mineral);
            }
            catch (Exception ex)
            {
                // Ein einzelner Fehler soll nicht den ganzen Import stoppen
                Console.WriteLine($"Fehler in Zeile: {line}. Fehler: {ex.Message}");
            }
        }

        await _context.SaveChangesAsync();
        return Ok("Import abgeschlossen!");
    }

}