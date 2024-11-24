using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoNaJuz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Added_Car_RentCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerDayCost",
                table: "Car_Rentals");

            migrationBuilder.DropColumn(
                name: "PerHourCost",
                table: "Car_Rentals");

            migrationBuilder.AddColumn<decimal>(
                name: "RentCostPerDay",
                table: "Cars",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentCostPerDay",
                table: "Cars");

            migrationBuilder.AddColumn<decimal>(
                name: "PerDayCost",
                table: "Car_Rentals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PerHourCost",
                table: "Car_Rentals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }
    }
}
