using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class ResumeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Resumes",
                newName: "ImagePath");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Resumes",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Resumes",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Resumes",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Resumes");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Resumes",
                newName: "Location");

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Settings",
                nullable: true);
        }
    }
}
