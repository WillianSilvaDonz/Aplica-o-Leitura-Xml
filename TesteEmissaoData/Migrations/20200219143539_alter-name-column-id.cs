using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteEmissaoData.Migrations
{
    public partial class alternamecolumnid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "documentoxml",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "documentoxml",
                newName: "Id");
        }
    }
}
