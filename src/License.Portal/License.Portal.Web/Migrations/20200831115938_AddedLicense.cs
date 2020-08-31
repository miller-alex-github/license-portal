using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace License.Portal.Web.Migrations
{
    public partial class AddedLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Product = table.Column<string>(maxLength: 200, nullable: false),
                    Package = table.Column<string>(maxLength: 200, nullable: false),
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    GeneratedAt = table.Column<DateTimeOffset>(nullable: false),
                    RegisteredAt = table.Column<DateTimeOffset>(nullable: true),
                    HardwareKey = table.Column<string>(nullable: true),
                    LicenseRaw = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licenses");
        }
    }
}
