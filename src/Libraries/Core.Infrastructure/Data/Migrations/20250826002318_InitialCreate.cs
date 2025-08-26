using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RENTALS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MovieId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CustomerName = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    RentedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DueAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ReturnedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RENTALS", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RENTALS");
        }
    }
}
