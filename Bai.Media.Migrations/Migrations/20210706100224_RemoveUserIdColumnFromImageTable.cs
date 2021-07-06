using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai.Media.Migrations.Migrations
{
    public partial class RemoveUserIdColumnFromImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Image");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ImageGroupPriority",
                schema: "dbo",
                table: "Image",
                columns: new[] { "PageId", "PageType", "ImageGroupId", "Priority" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_Key",
                schema: "dbo",
                table: "Image",
                columns: new[] { "PageId", "PageType" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_QueryFields",
                schema: "dbo",
                table: "Image",
                columns: new[] { "PageId", "PageType", "FileExtension", "FileSizeInBytes", "CreatedDt", "Deleted" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "dbo",
                table: "Image",
                type: "UniqueIdentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }
    }
}
