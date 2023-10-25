using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG_Elfshock.Migrations
{
    public partial class DefaultValueForDatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CharacterCreationDate",
                table: "Characters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 14, 0, 55, 434, DateTimeKind.Local).AddTicks(4764),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CharacterCreationDate",
                table: "Characters",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 25, 14, 0, 55, 434, DateTimeKind.Local).AddTicks(4764));
        }
    }
}
