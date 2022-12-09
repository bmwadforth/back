using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bmwadforth.Migrations
{
    public partial class FixUsernameUniqueUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_Users_Username");
        }
    }
}
