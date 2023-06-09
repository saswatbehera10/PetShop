using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    public partial class ImgUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "PetID",
                keyValue: 5,
                column: "ImgUrl",
                value: "https://scx2.b-cdn.net/gfx/news/hires/2018/2-dog.jpg");

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "PetID",
                keyValue: 10,
                column: "ImgUrl",
                value: "https://wallpaperaccess.com/full/275808.jpg");

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "PetID",
                keyValue: 44,
                column: "ImgUrl",
                value: "https://static.businessinsider.com/image/5484d9d1eab8ea3017b17e29/image.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Pets");
        }
    }
}
