using Microsoft.EntityFrameworkCore.Migrations;

namespace Bai.Media.Migrations.Migrations
{
    public partial class AddImagePriorityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Image_Key",
                schema: "dbo",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_QueryFields",
                schema: "dbo",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "dbo",
                table: "Image",
                type: "Int",
                nullable: true);

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
                columns: new[] { "UserId", "PageId", "PageType", "FileExtension", "FileSize", "CreatedDt", "Deleted" });
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

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "dbo",
                table: "Image");

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
        }
    }
}
