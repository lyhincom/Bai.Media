using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai.Media.Migrations.Migrations
{
    public partial class UpdateAvatarImageLogoTablesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_Id",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatars",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_Id",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                schema: "dbo",
                table: "Image",
                type: "Bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Image",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PageId",
                schema: "dbo",
                table: "Image",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PageType",
                schema: "dbo",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                schema: "dbo",
                table: "Avatars",
                type: "Bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Avatars",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image_Id",
                schema: "dbo",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatar_Id",
                schema: "dbo",
                table: "Avatars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Logo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FileExtension = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FileSize = table.Column<int>(type: "Int", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PageId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreatedDt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GetUtcDate()"),
                    Deleted = table.Column<bool>(type: "Bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logo_Id", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image",
                schema: "dbo",
                table: "Image",
                columns: new[] { "UserId", "FileExtension", "FileSize", "CreatedDt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Avatar",
                schema: "dbo",
                table: "Avatars",
                columns: new[] { "UserId", "FileExtension", "FileSize", "CreatedDt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Logo",
                schema: "dbo",
                table: "Logo",
                columns: new[] { "PageId", "FileExtension", "FileSize", "CreatedDt", "Deleted" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logo",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image_Id",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avatar_Id",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Avatar",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "Deleted",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "PageId",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "PageType",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Deleted",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                schema: "dbo",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avatars",
                schema: "dbo",
                table: "Avatars",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Id",
                schema: "dbo",
                table: "Image",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_Id",
                schema: "dbo",
                table: "Avatars",
                column: "Id",
                unique: true);
        }
    }
}
