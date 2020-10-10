using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Db.Hackathon.Core.Migrations
{
    public partial class AddedAnalyticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analytics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Period = table.Column<DateTime>(nullable: false),
                    ReceivedCases = table.Column<int>(nullable: false),
                    SolvedCases = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analytics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analytics");
        }
    }
}
