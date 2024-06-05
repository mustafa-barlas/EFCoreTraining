using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Functions.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"CREATE FUNCTION  GETPERSONTOTALPRICEe(@PERSONID INT)

                        RETURNS INT
                        AS 

                        BEGIN
                        DECLARE @TOTALPRICE INT

                        SELECT  @TOTALPRICE = SUM(O.Price) FROM Persons P
                        JOIN Orders O 
                        ON P.PersonId = O.PersonId
                        WHERE P.PersonId = @PERSONID
                        RETURN @TOTALPRICE
                        END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP FUNCTION GETPERSONTOTALPRICE");
        }
    }
}
