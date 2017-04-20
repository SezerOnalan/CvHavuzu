using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CvHavuzu.Web.Data.Migrations
{
    public partial class district : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_Cities_CityId",
                table: "District");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Resumes");

            migrationBuilder.RenameTable(
                name: "District",
                newName: "Districts");

            migrationBuilder.RenameIndex(
                name: "IX_District_CityId",
                table: "Districts",
                newName: "IX_Districts_CityId");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Resumes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_DistrictId",
                table: "Resumes",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Cities_CityId",
                table: "Districts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Districts_DistrictId",
                table: "Resumes",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Cities_CityId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Districts_DistrictId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_DistrictId",
                table: "Resumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Resumes");

            migrationBuilder.RenameTable(
                name: "Districts",
                newName: "District");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_CityId",
                table: "District",
                newName: "IX_District_CityId");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Resumes",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Cities_CityId",
                table: "District",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
