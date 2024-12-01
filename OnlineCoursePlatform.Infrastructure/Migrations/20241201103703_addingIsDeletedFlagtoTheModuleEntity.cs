using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingIsDeletedFlagtoTheModuleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);

         }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Modules");

        }
    }
}
