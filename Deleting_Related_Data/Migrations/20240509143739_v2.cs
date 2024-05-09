using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deleting_Related_Data.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorForDefaultBookForDefault");

            migrationBuilder.DropTable(
                name: "AuthorForDefaults");

            migrationBuilder.DropTable(
                name: "BookForDefaults");

            migrationBuilder.CreateTable(
                name: "AuthorFors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorFors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookFors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookFors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorForBookFor",
                columns: table => new
                {
                    AuthorForsId = table.Column<int>(type: "int", nullable: false),
                    BookForsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorForBookFor", x => new { x.AuthorForsId, x.BookForsId });
                    table.ForeignKey(
                        name: "FK_AuthorForBookFor_AuthorFors_AuthorForsId",
                        column: x => x.AuthorForsId,
                        principalTable: "AuthorFors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorForBookFor_BookFors_BookForsId",
                        column: x => x.BookForsId,
                        principalTable: "BookFors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorForBookFor_BookForsId",
                table: "AuthorForBookFor",
                column: "BookForsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorForBookFor");

            migrationBuilder.DropTable(
                name: "AuthorFors");

            migrationBuilder.DropTable(
                name: "BookFors");

            migrationBuilder.CreateTable(
                name: "AuthorForDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorForDefaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookForDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookForDefaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorForDefaultBookForDefault",
                columns: table => new
                {
                    AuthorForDefaultsId = table.Column<int>(type: "int", nullable: false),
                    BookForDefaultsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorForDefaultBookForDefault", x => new { x.AuthorForDefaultsId, x.BookForDefaultsId });
                    table.ForeignKey(
                        name: "FK_AuthorForDefaultBookForDefault_AuthorForDefaults_AuthorForDefaultsId",
                        column: x => x.AuthorForDefaultsId,
                        principalTable: "AuthorForDefaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorForDefaultBookForDefault_BookForDefaults_BookForDefaultsId",
                        column: x => x.BookForDefaultsId,
                        principalTable: "BookForDefaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorForDefaultBookForDefault_BookForDefaultsId",
                table: "AuthorForDefaultBookForDefault",
                column: "BookForDefaultsId");
        }
    }
}
