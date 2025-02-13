using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaquinhaOnline.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Progress",
                table: "Project",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
