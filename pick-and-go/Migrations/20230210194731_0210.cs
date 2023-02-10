using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickAndGo.Migrations
{
    public partial class _0210 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lineStatus",
                table: "OrderLine",
                type: "char(1)",
                unicode: false,
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                defaultValueSql: "('O')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "pickupTime",
                table: "OrderHeader",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lineStatus",
                table: "OrderLine");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "pickupTime",
                table: "OrderHeader",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
