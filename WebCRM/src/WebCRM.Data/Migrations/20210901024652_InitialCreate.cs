using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCRM.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountMembership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPrimaryAccountMember = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMembership", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteText = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountMembershipID = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalContractID = table.Column<int>(type: "INTEGER", nullable: true),
                    ContractName = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentDate = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContractAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPaidAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    LastPaymentRecievedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalExpenseAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    LastExpensePaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractExpense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractID = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpenseAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractExpense", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractID = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CRMAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountName = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRMAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberName = table.Column<string>(type: "TEXT", nullable: true),
                    UserID = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMembership_AccountID_MemberID",
                table: "AccountMembership",
                columns: new[] { "AccountID", "MemberID" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNote_AccountID",
                table: "AccountNote",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_AccountMembershipID",
                table: "Contract",
                column: "AccountMembershipID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractExpense_ContractID",
                table: "ContractExpense",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTransaction_ContractID",
                table: "ContractTransaction",
                column: "ContractID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMembership");

            migrationBuilder.DropTable(
                name: "AccountNote");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "ContractExpense");

            migrationBuilder.DropTable(
                name: "ContractTransaction");

            migrationBuilder.DropTable(
                name: "CRMAccount");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
