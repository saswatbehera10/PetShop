using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[] { 2, "Customer" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Name", "Phone", "RoleID", "UserType" },
                values: new object[] { 1, "alex@gmail.com", "Alex", "123", 1, 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Name", "Phone", "RoleID", "UserType" },
                values: new object[] { 2, "adamx@gmail.com", "Adam", "123", 2, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Name", "Phone", "RoleID", "UserType" },
                values: new object[] { 3, "cust@gmail.com", "Cust", "123", 2, 1 });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetID", "Age", "Name", "Price", "Species", "UserID" },
                values: new object[] { 5, 201, "haroooory", 220m, "dog", 3 });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetID", "Age", "Name", "Price", "Species", "UserID" },
                values: new object[] { 10, 2, "hooooarry", 220m, "cat", 2 });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetID", "Age", "Name", "Price", "Species", "UserID" },
                values: new object[] { 44, 20, "harry", 20m, "dog", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 2);
        }
    }
}
