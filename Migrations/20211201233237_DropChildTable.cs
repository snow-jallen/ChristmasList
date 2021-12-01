using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ChristmasList.Migrations
{
    public partial class DropChildTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesiredItems_Children_ChildId",
                table: "DesiredItems");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropIndex(
                name: "IX_DesiredItems_ChildId",
                table: "DesiredItems");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "DesiredItems");

            migrationBuilder.AddColumn<string>(
                name: "ChildEmail",
                table: "DesiredItems",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildEmail",
                table: "DesiredItems");

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "DesiredItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesiredItems_ChildId",
                table: "DesiredItems",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesiredItems_Children_ChildId",
                table: "DesiredItems",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
