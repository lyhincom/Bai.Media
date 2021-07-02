using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai.Media.Migrations.Migrations
{
    public partial class AddImageDatabaseAndFileSystemUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatabaseUrl",
                schema: "dbo",
                table: "Logo",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileSystemUrl",
                schema: "dbo",
                table: "Logo",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatabaseUrl",
                schema: "dbo",
                table: "Image",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileSystemUrl",
                schema: "dbo",
                table: "Image",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatabaseUrl",
                schema: "dbo",
                table: "Avatars",
                type: "nvarchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileSystemUrl",
                schema: "dbo",
                table: "Avatars",
                type: "nvarchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatabaseUrl",
                schema: "dbo",
                table: "Logo");

            migrationBuilder.DropColumn(
                name: "FileSystemUrl",
                schema: "dbo",
                table: "Logo");

            migrationBuilder.DropColumn(
                name: "DatabaseUrl",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "FileSystemUrl",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "DatabaseUrl",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "FileSystemUrl",
                schema: "dbo",
                table: "Avatars");
        }
    }
}
