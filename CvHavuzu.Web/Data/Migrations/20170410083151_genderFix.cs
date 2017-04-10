using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class genderFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cinsiyet",
                table: "Resumes");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Resumes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Resumes");

            migrationBuilder.AddColumn<int>(
                name: "Cinsiyet",
                table: "Resumes",
                nullable: false,
                defaultValue: 0);
        }
    }
}
