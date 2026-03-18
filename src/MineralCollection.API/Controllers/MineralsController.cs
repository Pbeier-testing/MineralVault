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
}