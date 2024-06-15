using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class Blogseedingdata3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 56, 29, 596, DateTimeKind.Local).AddTicks(9846), new DateTime(2024, 6, 15, 22, 56, 29, 596, DateTimeKind.Local).AddTicks(9863), "user-default", "Admin" });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 2,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 56, 29, 596, DateTimeKind.Local).AddTicks(9866), new DateTime(2024, 6, 15, 22, 56, 29, 596, DateTimeKind.Local).AddTicks(9867), "user-default", "Admin" });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 3,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 56, 29, 596, DateTimeKind.Local).AddTicks(9870), new DateTime(2024, 6, 15, 22, 56, 29, 596, DateTimeKind.Local).AddTicks(9870), "user-default", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4864), new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4882), "", "" });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 2,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4886), new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4887), "", "" });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 3,
                columns: new[] { "CreateAt", "UpdateAt", "UserID", "UserName" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4888), new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4889), "", "" });
        }
    }
}
