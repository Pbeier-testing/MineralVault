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
    public async Task<ActionResult<IEnumerable<Mineral>>> GetMinerals()
    {
        // Abruf aller Mineralien aus der DB und Rückgabe als Liste
        return await _context.Minerals.ToListAsync();
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

}