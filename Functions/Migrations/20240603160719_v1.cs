using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Functions.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Name" },
                values: new object[,]
                {
                    { 1, "Ayşe" },
                    { 2, "Hilmi" },
                    { 3, "Raziye" },
                    { 4, "Süleyman" },
                    { 5, "Fadime" },
                    { 6, "Şuayip" },
                    { 7, "Lale" },
                    { 8, "Jale" },
                    { 9, "Rıfkı" },
                    { 10, "Muaviye" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Description", "PersonId", "Price" },
                values: new object[,]
                {
                    { 1, "...", 1, 0 },
                    { 2, "...", 2, 0 },
                    { 3, "...", 4, 0 },
                    { 4, "...", 5, 0 },
                    { 5, "...", 1, 0 },
                    { 6, "...", 6, 0 },
                    { 7, "...", 7, 0 },
                    { 8, "...", 1, 0 },
                    { 9, "...", 8, 0 },
                    { 10, "...", 9, 0 },
                    { 11, "...", 1, 0 },
                    { 12, "...", 2, 0 },
                    { 13, "...", 2, 0 },
                    { 14, "...", 3, 0 },
                    { 15, "...", 1, 0 },
                    { 16, "...", 4, 0 },
                    { 17, "...", 1, 0 },
                    { 18, "...", 1, 0 },
                    { 19, "...", 5, 0 },
                    { 20, "...", 6, 0 },
                    { 21, "...", 1, 0 },
                    { 22, "...", 7, 0 },
                    { 23, "...", 7, 0 },
                    { 24, "...", 8, 0 },
                    { 25, "...", 1, 0 },
                    { 26, "...", 1, 0 },
                    { 27, "...", 9, 0 },
                    { 28, "...", 9, 0 },
                    { 29, "...", 9, 0 },
                    { 30, "...", 2, 0 },
                    { 31, "...", 3, 0 },
                    { 32, "...", 1, 0 },
                    { 33, "...", 1, 0 },
                    { 34, "...", 1, 0 },
                    { 35, "...", 5, 0 },
                    { 36, "...", 1, 0 },
                    { 37, "...", 5, 0 },
                    { 38, "...", 1, 0 },
                    { 39, "...", 1, 0 },
                    { 40, "...", 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PersonId",
                table: "Orders",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
