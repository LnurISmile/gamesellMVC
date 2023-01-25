using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gamesell.data.Migrations
{
    public partial class InitialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    PayBtnInfo = table.Column<bool>(type: "bit", nullable: false),
                    SE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 151, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3001));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3016));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3174));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 6,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3176));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 7,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 8,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3310));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 9,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3311));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 10,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3312));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 11,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3314));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 12,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3315));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 13,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3316));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 14,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3317));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 15,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3318));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 16,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 48, 23, 152, DateTimeKind.Local).AddTicks(3319));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bs");

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 613, DateTimeKind.Local).AddTicks(4199));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(1928));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(1943));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2076));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 6,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2079));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 7,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2205));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 8,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2207));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 9,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2208));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 10,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2210));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 11,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2211));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 12,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2212));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 13,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2214));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 14,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2215));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 15,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2216));

            migrationBuilder.UpdateData(
                table: "Pros",
                keyColumn: "Id",
                keyValue: 16,
                column: "ReleaseDate",
                value: new DateTime(2022, 6, 5, 10, 46, 44, 614, DateTimeKind.Local).AddTicks(2217));
        }
    }
}
