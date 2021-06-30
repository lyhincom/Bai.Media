using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai.Media.Migrations.Migrations
{
    public partial class BaiMedia_InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Avatars",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FileExtension = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FileSize = table.Column<int>(type: "Int", nullable: false),
                    CreatedDt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FileExtension = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FileSize = table.Column<int>(type: "Int", nullable: false),
                    CreatedDt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_Id",
                schema: "dbo",
                table: "Avatars",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_Id",
                schema: "dbo",
                table: "Image",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avatars",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Image",
                schema: "dbo");
        }
    }
}
