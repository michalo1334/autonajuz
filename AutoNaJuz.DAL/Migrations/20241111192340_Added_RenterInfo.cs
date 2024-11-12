using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoNaJuz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Added_RenterInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Rentals_AspNetUsers_UserId",
                table: "Car_Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Car_Rentals_UserId",
                table: "Car_Rentals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Car_Rentals");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasDriversLicense",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RenterId",
                table: "Car_Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Renter_Info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriversLicenseIdent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ApartmentNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renter_Info", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_Rentals_RenterId",
                table: "Car_Rentals",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Rentals_Renter_Info_RenterId",
                table: "Car_Rentals",
                column: "RenterId",
                principalTable: "Renter_Info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Rentals_Renter_Info_RenterId",
                table: "Car_Rentals");

            migrationBuilder.DropTable(
                name: "Renter_Info");

            migrationBuilder.DropIndex(
                name: "IX_Car_Rentals_RenterId",
                table: "Car_Rentals");

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Car_Rentals");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Car_Rentals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasDriversLicense",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_Rentals_UserId",
                table: "Car_Rentals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Rentals_AspNetUsers_UserId",
                table: "Car_Rentals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
