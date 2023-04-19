using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABBY.DATAACCESS.Migrations
{
    public partial class renameTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "OrderHeader",
                newName: "SessionPaymentIntentId");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "OrderHeader");

            migrationBuilder.RenameColumn(
                name: "SessionPaymentIntentId",
                table: "OrderHeader",
                newName: "TransactionId");
        }
    }
}
