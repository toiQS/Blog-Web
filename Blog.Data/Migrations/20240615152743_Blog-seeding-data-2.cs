using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class Blogseedingdata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4864), new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4882) });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 2,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4886), new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4887) });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 3,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4888), new DateTime(2024, 6, 15, 22, 27, 42, 549, DateTimeKind.Local).AddTicks(4889) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5007), new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 2,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5023), new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5024) });

            migrationBuilder.UpdateData(
                table: "Posters",
                keyColumn: "PosterID",
                keyValue: 3,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5026), new DateTime(2024, 6, 15, 10, 22, 28, 100, DateTimeKind.Local).AddTicks(5027) });
        }
    }
}
