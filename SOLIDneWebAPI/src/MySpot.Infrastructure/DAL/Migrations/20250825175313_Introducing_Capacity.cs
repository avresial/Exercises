using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySpot.Infrastructure.Dal.Migrations
{
    public partial class Introducing_Capacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "week",
                table: "WeeklyParkingSpots",
                newName: "Week");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WeeklyParkingSpots",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "WeeklyParkingSpots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "WeeklyParkingSpots");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Week",
                table: "WeeklyParkingSpots",
                newName: "week");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WeeklyParkingSpots",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
