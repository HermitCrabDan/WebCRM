using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCRM.Data.Migrations
{
    public partial class TransactionFeesAndPaymentMonths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFee",
                table: "ContractTransaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMonth",
                table: "ContractTransaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentYear",
                table: "ContractTransaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFee",
                table: "ContractTransaction");

            migrationBuilder.DropColumn(
                name: "PaymentMonth",
                table: "ContractTransaction");

            migrationBuilder.DropColumn(
                name: "PaymentYear",
                table: "ContractTransaction");
        }
    }
}
