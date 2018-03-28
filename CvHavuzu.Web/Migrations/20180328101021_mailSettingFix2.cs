using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CvHavuzu.Web.Migrations
{
    public partial class mailSettingFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FromAddressPassword",
                table: "MailSettings",
                newName: "MailUsername");

            migrationBuilder.AddColumn<string>(
                name: "MailPassword",
                table: "MailSettings",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailPassword",
                table: "MailSettings");

            migrationBuilder.RenameColumn(
                name: "MailUsername",
                table: "MailSettings",
                newName: "FromAddressPassword");
        }
    }
}
