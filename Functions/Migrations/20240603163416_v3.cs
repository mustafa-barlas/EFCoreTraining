using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Functions.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"CREATE FUNCTION BESTSELLING (@TOTALPRICE INT= 0)
                    RETURNS TABLE
                    AS
                    RETURN 
                    SELECT TOP 1 P.Name,COUNT(*) ORDERCOUNT, SUM(O.Price) TOTALPRICE FROM Persons P 
                    JOIN Orders O 
                    ON O.PersonId = P.PersonId
                    GROUP BY P.Name
                    HAVING SUM(O.Price) > @TOTALPRICE
                    ORDER BY ORDERCOUNT DESC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP FUNCTION BESTSELLING");
        }
    }
}
