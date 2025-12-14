using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bovime.Migrations
{
    /// <inheritdoc />
    public partial class baslangic2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hedefURL",
                table: "AnaSayfaManset",
                type: "nvarchar(200)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hedefURL",
                table: "AnaSayfaManset");
        }
    }
}
