using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Seat_SelectedSeatId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Airplanes_AirplaneId",
                table: "Seat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.RenameTable(
                name: "Seat",
                newName: "Seats");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_AirplaneId",
                table: "Seats",
                newName: "IX_Seats_AirplaneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Seats_SelectedSeatId",
                table: "Reservations",
                column: "SelectedSeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Airplanes_AirplaneId",
                table: "Seats",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Seats_SelectedSeatId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Airplanes_AirplaneId",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "Seat");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_AirplaneId",
                table: "Seat",
                newName: "IX_Seat_AirplaneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Seat_SelectedSeatId",
                table: "Reservations",
                column: "SelectedSeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Airplanes_AirplaneId",
                table: "Seat",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id");
        }
    }
}
