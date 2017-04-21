using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class ContactAddedToSettindbTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Settings");
        }
    }
}
