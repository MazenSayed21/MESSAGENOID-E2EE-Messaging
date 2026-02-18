using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MESSAGENOID.Data.Migrations
{
    /// <inheritdoc />
    public partial class addpublickey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
        name: "PublicKey",
        table: "AspNetUsers",
        type: "nvarchar(max)",
        nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "PublicKey", table: "AspNetUsers");
        }
    }
}
