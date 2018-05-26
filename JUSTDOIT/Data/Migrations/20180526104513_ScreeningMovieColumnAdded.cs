using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace viacinema.Data.Migrations
{
    public partial class ScreeningMovieColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Movie",
                table: "Screenings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Movie",
                table: "Screenings");
        }
    }
}
