using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoNaJuz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Added_CarImage_CarFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Rentals_Renter_Info_RenterId",
                table: "Car_Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarCarFeature_CarFeature_FeaturesId",
                table: "CarCarFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Renter_Info",
                table: "Renter_Info");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFeature",
                table: "CarFeature");

            migrationBuilder.RenameTable(
                name: "Renter_Info",
                newName: "Renter_Infos");

            migrationBuilder.RenameTable(
                name: "CarFeature",
                newName: "Car_Features");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Car_Features",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Renter_Infos",
                table: "Renter_Infos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car_Features",
                table: "Car_Features",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Car_Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Blob = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarCarImage",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "int", nullable: false),
                    ImagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCarImage", x => new { x.CarsId, x.ImagesId });
                    table.ForeignKey(
                        name: "FK_CarCarImage_Car_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Car_Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarCarImage_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarCarImage_ImagesId",
                table: "CarCarImage",
                column: "ImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Rentals_Renter_Infos_RenterId",
                table: "Car_Rentals",
                column: "RenterId",
                principalTable: "Renter_Infos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarCarFeature_Car_Features_FeaturesId",
                table: "CarCarFeature",
                column: "FeaturesId",
                principalTable: "Car_Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Rentals_Renter_Infos_RenterId",
                table: "Car_Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarCarFeature_Car_Features_FeaturesId",
                table: "CarCarFeature");

            migrationBuilder.DropTable(
                name: "CarCarImage");

            migrationBuilder.DropTable(
                name: "Car_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Renter_Infos",
                table: "Renter_Infos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car_Features",
                table: "Car_Features");

            migrationBuilder.RenameTable(
                name: "Renter_Infos",
                newName: "Renter_Info");

            migrationBuilder.RenameTable(
                name: "Car_Features",
                newName: "CarFeature");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CarFeature",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Renter_Info",
                table: "Renter_Info",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFeature",
                table: "CarFeature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Rentals_Renter_Info_RenterId",
                table: "Car_Rentals",
                column: "RenterId",
                principalTable: "Renter_Info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarCarFeature_CarFeature_FeaturesId",
                table: "CarCarFeature",
                column: "FeaturesId",
                principalTable: "CarFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
