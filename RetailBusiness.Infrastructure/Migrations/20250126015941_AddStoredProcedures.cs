using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailBusiness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetProductsByCategory
                @CategoryId INT
            AS
            BEGIN
                SELECT p.Id, p.Name, p.Price, p.CategoryId
                FROM Products p
                WHERE p.CategoryId = @CategoryId
                ORDER BY p.Name
            END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetProductsByCategory");
        }
    }
}
