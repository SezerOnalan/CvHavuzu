using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class editToStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResumeFullName",
                table: "Stats",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResumeFile",
                table: "Resumes",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeFullName",
                table: "Stats");

            migrationBuilder.AlterColumn<string>(
                name: "ResumeFile",
                table: "Resumes",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
