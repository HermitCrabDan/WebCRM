using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCRM.Data.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Member",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CRMUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletionBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRMUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_UserID",
                table: "Member",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRMUser");

            migrationBuilder.DropIndex(
                name: "IX_Member_UserID",
                table: "Member");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Member",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
