using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class MailSettingAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BodyContent = table.Column<string>(maxLength: 200, nullable: false),
                    FromAddress = table.Column<string>(maxLength: 200, nullable: false),
                    FromAddressPassword = table.Column<string>(maxLength: 200, nullable: false),
                    FromAddressTitle = table.Column<string>(maxLength: 200, nullable: false),
                    SmptPortNumber = table.Column<int>(nullable: false),
                    SmptServer = table.Column<string>(maxLength: 200, nullable: false),
                    Subject = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailSettings");
        }
    }
}
