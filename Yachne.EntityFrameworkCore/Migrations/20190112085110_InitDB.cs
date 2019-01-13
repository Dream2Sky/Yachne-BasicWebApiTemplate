using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yachne.EntityFrameworkCore.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    FID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FIsForbidden = table.Column<bool>(nullable: false),
                    FForbiddenTime = table.Column<DateTime>(nullable: false),
                    FUserName = table.Column<string>(maxLength: 50, nullable: false),
                    FPassword = table.Column<string>(maxLength: 50, nullable: false),
                    FSalt = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.FID);
                });

            migrationBuilder.InsertData(
                table: "T_User",
                columns: new[] { "FID", "FCreateTime", "FForbiddenTime", "FIsForbidden", "FPassword", "FSalt", "FUserName" },
                values: new object[] { 100001, new DateTime(2019, 1, 12, 16, 51, 10, 544, DateTimeKind.Local), new DateTime(2019, 1, 12, 16, 51, 10, 545, DateTimeKind.Local), false, "198ae64180289b7dbfedf665a4268f0b", "X8(oQ=a&:XO(&g%i", "Yachne" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_User");
        }
    }
}
