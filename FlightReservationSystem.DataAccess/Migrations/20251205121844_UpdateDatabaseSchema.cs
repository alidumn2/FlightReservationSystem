using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airplane_AirplaneId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Airplane_AirplaneId",
                table: "Seat");

            migrationBuilder.DropTable(
                name: "Airplane");

            migrationBuilder.CreateTable(
                name: "Airplanes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplanes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airplanes_AirplaneId",
                table: "Flights",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Airplanes_AirplaneId",
                table: "Seat",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airplanes_AirplaneId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Airplanes_AirplaneId",
                table: "Seat");

            migrationBuilder.DropTable(
                name: "Airplanes");

            migrationBuilder.CreateTable(
                name: "Airplane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplane", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airplane_AirplaneId",
                table: "Flights",
                column: "AirplaneId",
                principalTable: "Airplane",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Airplane_AirplaneId",
                table: "Seat",
                column: "AirplaneId",
                principalTable: "Airplane",
                principalColumn: "Id");
        }
    }
}
