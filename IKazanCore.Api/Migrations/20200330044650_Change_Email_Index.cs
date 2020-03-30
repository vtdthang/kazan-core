using Microsoft.EntityFrameworkCore.Migrations;

namespace IKazanCore.Api.Migrations
{
    public partial class Change_Email_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_users_email",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "idx_users_email",
                table: "users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_users_email",
                table: "users");

            migrationBuilder.CreateIndex(
                name: "idx_users_email",
                table: "users",
                column: "Email");
        }
    }
}
