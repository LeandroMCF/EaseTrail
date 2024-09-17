using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EaseTrail.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class updateuserwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersWorkSpaces_Users_UserId",
                table: "UsersWorkSpaces");

            migrationBuilder.DropIndex(
                name: "IX_UsersWorkSpaces_UserId",
                table: "UsersWorkSpaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersWorkSpaces");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "UsersWorkSpaces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "UsersWorkSpaces");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UsersWorkSpaces",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkSpaces_UserId",
                table: "UsersWorkSpaces",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersWorkSpaces_Users_UserId",
                table: "UsersWorkSpaces",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
