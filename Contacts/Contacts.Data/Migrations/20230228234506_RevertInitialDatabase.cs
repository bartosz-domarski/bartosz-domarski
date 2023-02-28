using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Contacts.Data.Migrations
{
    /// <inheritdoc />
    public partial class RevertInitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Subcategory_SubcategoryId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Subcategory");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_SubcategoryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Contacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Contacts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Contacts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subcategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SubcategoryId",
                table: "Contacts",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Subcategory_SubcategoryId",
                table: "Contacts",
                column: "SubcategoryId",
                principalTable: "Subcategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
