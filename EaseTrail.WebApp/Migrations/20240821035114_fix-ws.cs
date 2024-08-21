using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EaseTrail.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class fixws : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpace_Users_OwnerId",
                table: "WorkSpace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkSpace",
                table: "WorkSpace");

            migrationBuilder.RenameTable(
                name: "WorkSpace",
                newName: "WorkSpaces");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSpace_OwnerId",
                table: "WorkSpaces",
                newName: "IX_WorkSpaces_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkSpaces",
                table: "WorkSpaces",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaces_Users_OwnerId",
                table: "WorkSpaces",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaces_Users_OwnerId",
                table: "WorkSpaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkSpaces",
                table: "WorkSpaces");

            migrationBuilder.RenameTable(
                name: "WorkSpaces",
                newName: "WorkSpace");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSpaces_OwnerId",
                table: "WorkSpace",
                newName: "IX_WorkSpace_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkSpace",
                table: "WorkSpace",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpace_Users_OwnerId",
                table: "WorkSpace",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
