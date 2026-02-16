using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RustedMods.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModName = table.Column<string>(type: "TEXT", nullable: false),
                    ModAuthor = table.Column<string>(type: "TEXT", nullable: false),
                    ModRequirements = table.Column<string>(type: "TEXT", nullable: false),
                    ModVersion = table.Column<string>(type: "TEXT", nullable: false),
                    ModDate = table.Column<string>(type: "TEXT", nullable: false),
                    ModDesc = table.Column<string>(type: "TEXT", nullable: false),
                    ModSize = table.Column<string>(type: "TEXT", nullable: false),
                    ModRating = table.Column<string>(type: "TEXT", nullable: false),
                    ModDownload = table.Column<string>(type: "TEXT", nullable: false),
                    ModThumbnail = table.Column<string>(type: "TEXT", nullable: false),
                    ModScreenshots = table.Column<string>(type: "TEXT", nullable: false),
                    ModTrailer = table.Column<string>(type: "TEXT", nullable: false),
                    ModComments = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Socials = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Verified = table.Column<bool>(type: "INTEGER", nullable: false),
                    Picture = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addons");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
