using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stored_Procedure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"CREATE PROCEDURE SP_BEST_SELLING_STAFF
AS

DECLARE @NAME VARCHAR(50),@COUNT INT

SELECT TOP 1 @NAME= P.Name, @COUNT= COUNT(*) FROM Persons P 
JOIN Orders O
ON O.PersonId = P.PersonId
GROUP BY P.Name
ORDER BY COUNT(*) DESC
RETURN @NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@" drop SP_PERSONORDERS");
        }
    }
}
