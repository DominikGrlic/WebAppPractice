using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebApp.Migrations
{
    /// <inheritdoc />
    public partial class FixAdminEmailConfirm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b6e5a5-8fd9-465d-b043-ace7d30878f7",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9937ea6-a94a-4195-8c40-054ade59cc62", true, "AQAAAAIAAYagAAAAEGtQKF1qCeg84GFA9qCqF0QxdO2ONQk9oa9ZIMb5IDoYUyoiaMk+d75o05ySWSy8CA==", "9647ed26-7e22-4e3c-8947-bd3447fd425d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b6e5a5-8fd9-465d-b043-ace7d30878f7",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "649d9ef1-8355-4680-97fc-982f3a81d176", false, "AQAAAAIAAYagAAAAEKZBijdpKJad/W/BPFuVIY1NUQyFAhxGeQsVaCIWWySRRGi7DIWVacbrtfYdQJjJRw==", "45143cd8-8522-4f28-b1aa-ebaad507989f" });
        }
    }
}
