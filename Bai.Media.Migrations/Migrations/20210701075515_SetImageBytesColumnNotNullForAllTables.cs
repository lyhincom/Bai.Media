using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai.Media.Migrations.Migrations
{
    public partial class SetImageBytesColumnNotNullForAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Image",
                schema: "dbo",
                table: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Logo",
                schema: "dbo",
                table: "Logo",
                newName: "IX_Logo_QueryFields");

            migrationBuilder.RenameIndex(
                name: "IX_Avatar",
                schema: "dbo",
                table: "Avatars",
                newName: "IX_Avatar_QueryFields");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Logo",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PageType",
                schema: "dbo",
                table: "Image",
                type: "NVarChar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PageId",
                schema: "dbo",
                table: "Image",
                type: "UniqueIdentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Image",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageGroupId",
                schema: "dbo",
                table: "Image",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Avatars",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logo_Key",
                schema: "dbo",
                table: "Logo",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Key",
                schema: "dbo",
                table: "Image",
                columns: new[] { "UserId", "PageId", "PageType", "ImageGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_QueryFields",
                schema: "dbo",
                table: "Image",
                columns: new[] { "UserId", "PageId", "PageType", "ImageGroupId", "FileExtension", "FileSize", "CreatedDt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Avatar_Key",
                schema: "dbo",
                table: "Avatars",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Logo_Key",
                schema: "dbo",
                table: "Logo");

            migrationBuilder.DropIndex(
                name: "IX_Image_Key",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_QueryFields",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Avatar_Key",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "ImageGroupId",
                schema: "dbo",
                table: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Logo_QueryFields",
                schema: "dbo",
                table: "Logo",
                newName: "IX_Logo");

            migrationBuilder.RenameIndex(
                name: "IX_Avatar_QueryFields",
                schema: "dbo",
                table: "Avatars",
                newName: "IX_Avatar");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Logo",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PageType",
                schema: "dbo",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVarChar(100)");

            migrationBuilder.AlterColumn<Guid>(
                name: "PageId",
                schema: "dbo",
                table: "Image",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Image",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageBytes",
                schema: "dbo",
                table: "Avatars",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Image",
                schema: "dbo",
                table: "Image",
                columns: new[] { "UserId", "FileExtension", "FileSize", "CreatedDt", "Deleted" });
        }
    }
}
