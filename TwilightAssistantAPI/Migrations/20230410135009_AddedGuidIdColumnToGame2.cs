﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwilightAssistantAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedGuidIdColumnToGame2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuidId",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuidId",
                table: "Games");
        }
    }
}
