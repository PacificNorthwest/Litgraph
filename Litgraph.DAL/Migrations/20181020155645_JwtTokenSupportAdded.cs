using Microsoft.EntityFrameworkCore.Migrations;

namespace Litgraph.DAL.Migrations
{
    public partial class JwtTokenSupportAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JwtToken",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JwtToken",
                table: "AspNetUsers");
        }
    }
}
