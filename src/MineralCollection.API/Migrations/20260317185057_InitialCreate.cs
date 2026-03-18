using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineralCollection.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Minerals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Fundort = table.Column<string>(type: "TEXT", nullable: false),
                    Funddatum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Beschreibung = table.Column<string>(type: "TEXT", nullable: false),
                    Breitengrad = table.Column<double>(type: "REAL", nullable: false),
                    Laengengrad = table.Column<double>(type: "REAL", nullable: false),
                    BildUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minerals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Minerals");
        }
    }
}
