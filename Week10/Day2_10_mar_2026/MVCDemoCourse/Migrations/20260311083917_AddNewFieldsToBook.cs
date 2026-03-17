using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCDemoCourse.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublishedYear",
                table: "tblBook",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "tblBook",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedYear",
                table: "tblBook");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "tblBook");
        }
    }
}
