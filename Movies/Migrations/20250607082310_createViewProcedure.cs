using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Migrations
{
    /// <inheritdoc />
    public partial class createViewProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW UserMovieView AS
                SELECT 
                    u.Id AS UserId,
                    u.Username,
                    u.Email,
                    t.Id AS MovieId,
                    t.Name AS MovieName
                FROM Users u
                JOIN Titles t ON u.Id = t.UserId;
            ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE AddUser
                    @Username NVARCHAR(100),
                    @Password NVARCHAR(100),
                    @Email NVARCHAR(100)
                AS
                BEGIN
                    INSERT INTO Users (Username, Password, Email)
                    VALUES (@Username, @Password, @Email)
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW UserMovieView");
            migrationBuilder.Sql("DROP PROCEDURE AddUser");
        }
    }
}
