using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class PrivacyPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrivacyPolicy",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermsOfUse",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivacyPolicy",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TermsOfUse",
                table: "Settings");
        }
    }
}
