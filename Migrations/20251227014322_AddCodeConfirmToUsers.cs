using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RustedMods.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeConfirmToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeConfirm",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeConfirm",
                table: "Users");
        }
    }
}
