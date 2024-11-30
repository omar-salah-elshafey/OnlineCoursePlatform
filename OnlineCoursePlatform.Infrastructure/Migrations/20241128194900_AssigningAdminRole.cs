using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AssigningAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a330b209-871f-45fc-9a8d-f357f9bff3b1", "7e53a491-a9de-4c75-af44-ff3271a5176c" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a330b209-871f-45fc-9a8d-f357f9bff3b1", "7e53a491-a9de-4c75-af44-ff3271a5176c" });

        }
    }
}
