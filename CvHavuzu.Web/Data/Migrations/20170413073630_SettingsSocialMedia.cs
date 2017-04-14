using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class SettingsSocialMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Settings");
        }
    }
}
