using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace viacinema.Data.Migrations
{
    public partial class ScreeningMovieColumnFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Movie",
                table: "Screenings");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings");

            migrationBuilder.DropIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings");

            migrationBuilder.AddColumn<int>(
                name: "Movie",
                table: "Screenings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
