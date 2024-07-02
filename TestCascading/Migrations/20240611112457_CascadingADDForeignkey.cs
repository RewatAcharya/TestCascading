using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestCascading.Migrations
{
    /// <inheritdoc />
    public partial class CascadingADDForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cascadings_DistrictId",
                table: "Cascadings",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Cascadings_StateId",
                table: "Cascadings",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cascadings_WardId",
                table: "Cascadings",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cascadings_Districts_DistrictId",
                table: "Cascadings",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cascadings_States_StateId",
                table: "Cascadings",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cascadings_Wards_WardId",
                table: "Cascadings",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cascadings_Districts_DistrictId",
                table: "Cascadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Cascadings_States_StateId",
                table: "Cascadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Cascadings_Wards_WardId",
                table: "Cascadings");

            migrationBuilder.DropIndex(
                name: "IX_Cascadings_DistrictId",
                table: "Cascadings");

            migrationBuilder.DropIndex(
                name: "IX_Cascadings_StateId",
                table: "Cascadings");

            migrationBuilder.DropIndex(
                name: "IX_Cascadings_WardId",
                table: "Cascadings");
        }
    }
}
