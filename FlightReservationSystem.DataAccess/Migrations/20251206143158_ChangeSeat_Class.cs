using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSeat_Class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Airplanes_AirplaneId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "PriceMultiplier",
                table: "Seats");

            migrationBuilder.AlterColumn<int>(
                name: "AirplaneId",
                table: "Seats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Airplanes_AirplaneId",
                table: "Seats",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Airplanes_AirplaneId",
                table: "Seats");

            migrationBuilder.AlterColumn<int>(
                name: "AirplaneId",
                table: "Seats",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceMultiplier",
                table: "Seats",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Airplanes_AirplaneId",
                table: "Seats",
                column: "AirplaneId",
                principalTable: "Airplanes",
                principalColumn: "Id");
        }
    }
}
