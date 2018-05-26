using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace viacinema.Data.Migrations
{
    public partial class PaymentChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Payments_ScreeningId",
                table: "Payments",
                column: "ScreeningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Screenings_ScreeningId",
                table: "Payments",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Screenings_ScreeningId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ScreeningId",
                table: "Payments");
        }
    }
}
