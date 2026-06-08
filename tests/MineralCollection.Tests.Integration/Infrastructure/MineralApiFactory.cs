using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MineralCollection.API.Data;

namespace MineralCollection.Tests.Integration.Infrastructure;

internal sealed class MineralApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databasePath = Path.Combine(Path.GetTempPath(), $"mineralvault-{Guid.NewGuid():N}.db");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextOptions = services.SingleOrDefault(
                service => service.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (dbContextOptions != null)
            {
                services.Remove(dbContextOptions);
            }

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite($"Data Source={_databasePath}");
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        SqliteConnection.ClearAllPools();

        for (var attempt = 0; attempt < 5; attempt++)
        {
            try
            {
                if (File.Exists(_databasePath))
                {
                    File.Delete(_databasePath);
                }

                return;
            }
            catch (IOException) when (attempt < 4)
            {
                Thread.Sleep(100);
            }
        }
    }
}
