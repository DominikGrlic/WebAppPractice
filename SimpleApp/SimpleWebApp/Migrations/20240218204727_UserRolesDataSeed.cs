using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UserRolesDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "092ccefc-1e85-40df-888d-15bbcc180fce", null, "User", "USER" },
                    { "ed70f95f-f713-43d3-9af8-e0947be13d79", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d6b6e5a5-8fd9-465d-b043-ace7d30878f7", 0, "649d9ef1-8355-4680-97fc-982f3a81d176", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEKZBijdpKJad/W/BPFuVIY1NUQyFAhxGeQsVaCIWWySRRGi7DIWVacbrtfYdQJjJRw==", null, false, "45143cd8-8522-4f28-b1aa-ebaad507989f", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ed70f95f-f713-43d3-9af8-e0947be13d79", "d6b6e5a5-8fd9-465d-b043-ace7d30878f7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "092ccefc-1e85-40df-888d-15bbcc180fce");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ed70f95f-f713-43d3-9af8-e0947be13d79", "d6b6e5a5-8fd9-465d-b043-ace7d30878f7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed70f95f-f713-43d3-9af8-e0947be13d79");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b6e5a5-8fd9-465d-b043-ace7d30878f7");
        }
    }
}
