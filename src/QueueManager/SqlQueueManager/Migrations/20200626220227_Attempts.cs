using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlQueueManager.Migrations
{
    public partial class Attempts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "QueueItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "QueueItems");
        }
    }
}
