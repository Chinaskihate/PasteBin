using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicMetadatas",
                columns: table => new
                {
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShortUrl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicMetadatas", x => x.TopicId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicMetadatas_ShortUrl",
                table: "TopicMetadatas",
                column: "ShortUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicMetadatas");
        }
    }
}
