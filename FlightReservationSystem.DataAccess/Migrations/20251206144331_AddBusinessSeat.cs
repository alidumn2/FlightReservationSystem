using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Seats",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Seats");
        }
    }
}
