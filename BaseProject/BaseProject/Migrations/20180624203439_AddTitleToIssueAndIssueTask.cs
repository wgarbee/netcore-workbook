using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProject.Migrations
{
    public partial class AddTitleToIssueAndIssueTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Issue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Issue");
        }
    }
}
