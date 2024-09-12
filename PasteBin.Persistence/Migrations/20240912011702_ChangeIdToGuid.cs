using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBin.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortUrl",
                table: "TopicMetadatas",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "TopicMetadatas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicMetadatas_ShortUrl",
                table: "TopicMetadatas",
                column: "ShortUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TopicMetadatas_ShortUrl",
                table: "TopicMetadatas");

            migrationBuilder.AlterColumn<string>(
                name: "ShortUrl",
                table: "TopicMetadatas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "TopicMetadatas",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
