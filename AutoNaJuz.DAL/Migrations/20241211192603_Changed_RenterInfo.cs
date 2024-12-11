using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoNaJuz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Changed_RenterInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "DriversLicenseIdent",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "Pesel",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Renter_Infos");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Renter_Infos",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Renter_Infos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Renter_Infos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Renter_Infos");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Renter_Infos");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Renter_Infos",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "ApartmentNumber",
                table: "Renter_Infos",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Renter_Infos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BuildingNumber",
                table: "Renter_Infos",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Renter_Infos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenseIdent",
                table: "Renter_Infos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Renter_Infos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Renter_Infos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pesel",
                table: "Renter_Infos",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Renter_Infos",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
