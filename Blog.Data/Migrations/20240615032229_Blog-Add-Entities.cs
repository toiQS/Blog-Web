using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class BlogAddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Posters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Posters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CommentID",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterID = table.Column<int>(type: "int", nullable: false),
                    CommentContext = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyTo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ReplyTo",
                        column: x => x.ReplyTo,
                        principalTable: "Comments",
                        principalColumn: "CommentID");
                    table.ForeignKey(
                        name: "FK_Comments_Posters_PosterID",
                        column: x => x.PosterID,
                        principalTable: "Posters",
                        principalColumn: "PosterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageID",
                keyValue: 1,
                column: "CommentID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageID",
                keyValue: 2,
                column: "CommentID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageID",
                keyValue: 3,
                column: "CommentID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "ImageID",
                keyValue: 4,
                column: "CommentID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5007), new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5020), "", "" });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 2,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5023), new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5024), "", "" });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 3,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5026), new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5027), "", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CommentID",
                table: "Images",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PosterID",
                table: "Comments",
                column: "PosterID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReplyTo",
                table: "Comments",
                column: "ReplyTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Comments_CommentID",
                table: "Images",
                column: "CommentID",
                principalTable: "Comments",
                principalColumn: "CommentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Comments_CommentID",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Images_CommentID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Posters");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Posters");

            migrationBuilder.DropColumn(
                name: "CommentID",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 10, 14, 16, 48, 10, DateTimeKind.Local).AddTicks(1092), new DateTime(2024, 6, 10, 14, 16, 48, 10, DateTimeKind.Local).AddTicks(1104) });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 2,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 10, 14, 16, 48, 10, DateTimeKind.Local).AddTicks(1106), new DateTime(2024, 6, 10, 14, 16, 48, 10, DateTimeKind.Local).AddTicks(1107) });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 3,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 10, 14, 16, 48, 10, DateTimeKind.Local).AddTicks(1108), new DateTime(2024, 6, 10, 14, 16, 48, 10, DateTimeKind.Local).AddTicks(1108) });
        }
    }
}
