using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class SettingsTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Settings");
        }
    }
}
