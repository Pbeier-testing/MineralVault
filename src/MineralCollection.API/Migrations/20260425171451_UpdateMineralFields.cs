using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineralCollection.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMineralFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beschreibung",
                table: "Minerals");

            migrationBuilder.DropColumn(
                name: "Funddatum",
                table: "Minerals");

            migrationBuilder.RenameColumn(
                name: "BildUrl",
                table: "Minerals",
                newName: "Region");

            migrationBuilder.AlterColumn<double>(
                name: "Laengengrad",
                table: "Minerals",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<string>(
                name: "Fundort",
                table: "Minerals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Breitengrad",
                table: "Minerals",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "Begleitmineral",
                table: "Minerals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bemerkungen",
                table: "Minerals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Erwerbsjahr",
                table: "Minerals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Fundjahr",
                table: "Minerals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Land",
                table: "Minerals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nummer",
                table: "Minerals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MineralImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Caption = table.Column<string>(type: "TEXT", nullable: true),
                    MineralId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MineralImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MineralImages_Minerals_MineralId",
                        column: x => x.MineralId,
                        principalTable: "Minerals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MineralImages_MineralId",
                table: "MineralImages",
                column: "MineralId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MineralImages");

            migrationBuilder.DropColumn(
                name: "Begleitmineral",
                table: "Minerals");

            migrationBuilder.DropColumn(
                name: "Bemerkungen",
                table: "Minerals");

            migrationBuilder.DropColumn(
                name: "Erwerbsjahr",
                table: "Minerals");

            migrationBuilder.DropColumn(
                name: "Fundjahr",
                table: "Minerals");

            migrationBuilder.DropColumn(
                name: "Land",
                table: "Minerals");

            migrationBuilder.DropColumn(
                name: "Nummer",
                table: "Minerals");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Minerals",
                newName: "BildUrl");

            migrationBuilder.AlterColumn<double>(
                name: "Laengengrad",
                table: "Minerals",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fundort",
                table: "Minerals",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Breitengrad",
                table: "Minerals",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Beschreibung",
                table: "Minerals",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Funddatum",
                table: "Minerals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
