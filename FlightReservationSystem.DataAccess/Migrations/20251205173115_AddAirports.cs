using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAirports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalCity",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureCity",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "ArrivalAirportId",
                table: "Flights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartureAirportId",
                table: "Flights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalAirportId",
                table: "Flights",
                column: "ArrivalAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureAirportId",
                table: "Flights",
                column: "DepartureAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_ArrivalAirportId",
                table: "Flights",
                column: "ArrivalAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_DepartureAirportId",
                table: "Flights",
                column: "DepartureAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_ArrivalAirportId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_DepartureAirportId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_Flights_ArrivalAirportId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DepartureAirportId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ArrivalAirportId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureAirportId",
                table: "Flights");

            migrationBuilder.AddColumn<string>(
                name: "ArrivalCity",
                table: "Flights",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureCity",
                table: "Flights",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
