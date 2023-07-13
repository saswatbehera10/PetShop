using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    public partial class DestroyedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetID", "Age", "ImgUrl", "Name", "Price", "Species", "Status" },
                values: new object[,]
                {
                    { 5, 201, "https://scx2.b-cdn.net/gfx/news/hires/2018/2-dog.jpg", "haroooory", 220m, "dog", null },
                    { 10, 2, "https://wallpaperaccess.com/full/275808.jpg", "hooooarry", 220m, "cat", null },
                    { 44, 20, "https://static.businessinsider.com/image/5484d9d1eab8ea3017b17e29/image.jpg", "harry", 20m, "dog", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Name", "Password", "Phone", "RoleID" },
                values: new object[,]
                {
                    { 1, "alex@gmail.com", "Alex", "password", "123", 1 },
                    { 2, "adamx@gmail.com", "Adam", "password", "123", 2 },
                    { 3, "cust@gmail.com", "Cust", "password", "123", 2 }
                });
        }
    }
}
