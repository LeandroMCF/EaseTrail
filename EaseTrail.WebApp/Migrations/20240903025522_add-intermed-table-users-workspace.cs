using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EaseTrail.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class addintermedtableusersworkspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaces_Users_OwnerId",
                table: "WorkSpaces");

            migrationBuilder.AddColumn<int>(
                name: "UserCount",
                table: "WorkSpaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UsersWorkSpaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColaboratorType = table.Column<int>(type: "int", nullable: false),
                    InviteStatus = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExclusionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWorkSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersWorkSpaces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersWorkSpaces_WorkSpaces_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkSpaces_UserId",
                table: "UsersWorkSpaces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWorkSpaces_WorkSpaceId",
                table: "UsersWorkSpaces",
                column: "WorkSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaces_Users_OwnerId",
                table: "WorkSpaces",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaces_Users_OwnerId",
                table: "WorkSpaces");

            migrationBuilder.DropTable(
                name: "UsersWorkSpaces");

            migrationBuilder.DropColumn(
                name: "UserCount",
                table: "WorkSpaces");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaces_Users_OwnerId",
                table: "WorkSpaces",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
