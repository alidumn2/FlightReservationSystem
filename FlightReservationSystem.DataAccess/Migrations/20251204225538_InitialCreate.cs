using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airplane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplane", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    TCKimlikNo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureCity = table.Column<string>(type: "TEXT", nullable: false),
                    ArrivalCity = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AirplaneId = table.Column<int>(type: "INTEGER", nullable: false),
                    BasePrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airplane_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplane",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeatNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceMultiplier = table.Column<decimal>(type: "TEXT", nullable: false),
                    AirplaneId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Airplane_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplane",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pnr = table.Column<string>(type: "TEXT", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PricePaid = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReservedCustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservedFlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedSeatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_ReservedFlightId",
                        column: x => x.ReservedFlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Seat_SelectedSeatId",
                        column: x => x.SelectedSeatId,
                        principalTable: "Seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_ReservedCustomerId",
                        column: x => x.ReservedCustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirplaneId",
                table: "Flights",
                column: "AirplaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservedCustomerId",
                table: "Reservations",
                column: "ReservedCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservedFlightId",
                table: "Reservations",
                column: "ReservedFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SelectedSeatId",
                table: "Reservations",
                column: "SelectedSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_AirplaneId",
                table: "Seat",
                column: "AirplaneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Airplane");
        }
    }
}
