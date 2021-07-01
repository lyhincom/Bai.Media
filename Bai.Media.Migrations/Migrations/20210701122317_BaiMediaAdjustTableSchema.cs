using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai.Media.Migrations.Migrations
{
    public partial class BaiMediaAdjustTableSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Logo_QueryFields",
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
                name: "IX_Avatar_QueryFields",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.RenameColumn(
                name: "FileSize",
                schema: "dbo",
                table: "Logo",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "FileSize",
                schema: "dbo",
                table: "Image",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "FileSize",
                schema: "dbo",
                table: "Avatars",
                newName: "Width");

            migrationBuilder.AlterColumn<string>(
                name: "FileExtension",
                schema: "dbo",
                table: "Logo",
                type: "NVarChar(10)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "dbo",
                table: "Logo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInBytes",
                schema: "dbo",
                table: "Logo",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                schema: "dbo",
                table: "Logo",
                type: "Int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FileExtension",
                schema: "dbo",
                table: "Image",
                type: "NVarChar(10)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "dbo",
                table: "Image",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInBytes",
                schema: "dbo",
                table: "Image",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                schema: "dbo",
                table: "Image",
                type: "Int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "dbo",
                table: "Image",
                type: "Int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileExtension",
                schema: "dbo",
                table: "Avatars",
                type: "NVarChar(10)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "dbo",
                table: "Avatars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInBytes",
                schema: "dbo",
                table: "Avatars",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                schema: "dbo",
                table: "Avatars",
                type: "Int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logo_QueryFields",
                schema: "dbo",
                table: "Logo",
                columns: new[] { "PageId", "FileExtension", "FileSizeInBytes", "CreatedDt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ImageGroupPriority",
                schema: "dbo",
                table: "Image",
                columns: new[] { "UserId", "PageId", "PageType", "ImageGroupId", "Priority" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_Key",
                schema: "dbo",
                table: "Image",
                columns: new[] { "UserId", "PageId", "PageType" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_QueryFields",
                schema: "dbo",
                table: "Image",
                columns: new[] { "UserId", "PageId", "PageType", "FileExtension", "FileSizeInBytes", "CreatedDt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Avatar_QueryFields",
                schema: "dbo",
                table: "Avatars",
                columns: new[] { "UserId", "FileExtension", "FileSizeInBytes", "CreatedDt", "Deleted" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Logo_QueryFields",
                schema: "dbo",
                table: "Logo");

            migrationBuilder.DropIndex(
                name: "IX_Image_ImageGroupPriority",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_Key",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_QueryFields",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Avatar_QueryFields",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "dbo",
                table: "Logo");

            migrationBuilder.DropColumn(
                name: "FileSizeInBytes",
                schema: "dbo",
                table: "Logo");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "dbo",
                table: "Logo");

            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "FileSizeInBytes",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "FileSizeInBytes",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "dbo",
                table: "Avatars");

            migrationBuilder.RenameColumn(
                name: "Width",
                schema: "dbo",
                table: "Logo",
                newName: "FileSize");

            migrationBuilder.RenameColumn(
                name: "Width",
                schema: "dbo",
                table: "Image",
                newName: "FileSize");

            migrationBuilder.RenameColumn(
                name: "Width",
                schema: "dbo",
                table: "Avatars",
                newName: "FileSize");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileExtension",
                schema: "dbo",
                table: "Logo",
                type: "UniqueIdentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVarChar(10)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileExtension",
                schema: "dbo",
                table: "Image",
                type: "UniqueIdentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVarChar(10)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileExtension",
                schema: "dbo",
                table: "Avatars",
                type: "UniqueIdentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVarChar(10)");

            migrationBuilder.CreateIndex(
                name: "IX_Logo_QueryFields",
                schema: "dbo",
                table: "Logo",
                columns: new[] { "PageId", "FileExtension", "FileSize", "CreatedDt", "Deleted" });

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
                name: "IX_Avatar_QueryFields",
                schema: "dbo",
                table: "Avatars",
                columns: new[] { "UserId", "FileExtension", "FileSize", "CreatedDt", "Deleted" });
        }
    }
}
