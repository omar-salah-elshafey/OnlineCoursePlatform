using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineCoursePlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a330b209-871f-45fc-9a8d-f357f9bff3b1", "1", "Admin", "ADMIN" },
                    { "b331b209-871f-45fc-9a8d-f357f9bff3b1", "2", "Instructor", "SELLER" },
                    { "c332b209-871f-45fc-9a8d-f357f9bff3b1", "3", "Student", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7e53a491-a9de-4c75-af44-ff3271a5176c", 0, "3513ff76-42a5-4b6c-b325-9d9b3263be45", new DateTime(2024, 11, 28, 21, 46, 0, 661, DateTimeKind.Local).AddTicks(979), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "super@admin.com", true, "Super", false, false, "Admin", false, null, "SUPER@ADMIN.COM", "SUPER_ADMIN", "AQAAAAIAAYagAAAAEPjaBwveCwa6oLB3KGdNHR/aLwTnb8I1NhZC+jkVYSdQONpxVrH7ALRXbjZQ+p8DTg==", null, false, "ca600690-e820-4495-8f45-02ef2aeb2cff", false, "super_admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a330b209-871f-45fc-9a8d-f357f9bff3b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b331b209-871f-45fc-9a8d-f357f9bff3b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c332b209-871f-45fc-9a8d-f357f9bff3b1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e53a491-a9de-4c75-af44-ff3271a5176c");
        }
    }
}
