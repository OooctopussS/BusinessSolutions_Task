using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.WebApi.Migrations
{
    public partial class AddProviderToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProviderId",
                table: "Orders",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Providers_ProviderId",
                table: "Orders",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Providers_ProviderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProviderId",
                table: "Orders");
        }
    }
}
