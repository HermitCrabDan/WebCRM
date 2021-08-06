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
                    MembershipCreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MembershipCreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    MembershipRemovalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MembershipRemovedBy = table.Column<string>(type: "TEXT", nullable: true),
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
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    NoteCreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NoteRemovalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NoteRemovedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalContractID = table.Column<int>(type: "INTEGER", nullable: true),
                    ContractName = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContractAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPaidAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    LastPaymentRecievedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContractCloseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClosedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => new { x.AccountID, x.MemberID });
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
                    ExpenseEnteredBy = table.Column<string>(type: "TEXT", nullable: true),
                    ExpenseEnteredDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpenseCancelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpenseCanceledBy = table.Column<string>(type: "TEXT", nullable: true)
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
                    TransactionEnteredBy = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionEnteredDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransactionCancelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TransactionCanceledBy = table.Column<string>(type: "TEXT", nullable: true)
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
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    AccountRetirementDate = table.Column<DateTime>(type: "TEXT", nullable: true)
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
                    MemberAddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MemberAddedBy = table.Column<string>(type: "TEXT", nullable: true),
                    MemberRemovalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MemberRemovedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UserID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberTestimonial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    TestimonialText = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    TestimonialClipStart = table.Column<int>(type: "INTEGER", nullable: true),
                    TestimonialClipEnd = table.Column<int>(type: "INTEGER", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: true),
                    TestimonialRemovedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TestimonialRemovedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastedUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTestimonial", x => x.Id);
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
                name: "IX_Contract_AccountID_MemberID",
                table: "Contract",
                columns: new[] { "AccountID", "MemberID" });

            migrationBuilder.CreateIndex(
                name: "IX_ContractExpense_ContractID",
                table: "ContractExpense",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTransaction_ContractID",
                table: "ContractTransaction",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTestimonial_MemberID",
                table: "MemberTestimonial",
                column: "MemberID");
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

            migrationBuilder.DropTable(
                name: "MemberTestimonial");
        }
    }
}
