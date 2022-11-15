using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlcTagLib.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MicrologixPlcs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Program = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlcType = table.Column<int>(type: "int", nullable: false),
                    Protocol = table.Column<int>(type: "int", nullable: false),
                    Timeout = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    DebugLevel = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrologixPlcs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagTypes",
                columns: table => new
                {
                    TagTypeId = table.Column<int>(type: "int", nullable: false),
                    TagType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTypes", x => x.TagTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PlcTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlcId = table.Column<int>(type: "int", nullable: false),
                    SymbolName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlcTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlcTags_MicrologixPlcs_PlcId",
                        column: x => x.PlcId,
                        principalTable: "MicrologixPlcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlcTags_TagTypes_TagTypeId",
                        column: x => x.TagTypeId,
                        principalTable: "TagTypes",
                        principalColumn: "TagTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TagTypes",
                columns: new[] { "TagTypeId", "TagType" },
                values: new object[,]
                {
                    { 0, "Output" },
                    { 1, "Input" },
                    { 2, "Status" },
                    { 3, "Binary" },
                    { 4, "Timer" },
                    { 5, "Counter" },
                    { 6, "Control" },
                    { 7, "Integer" },
                    { 8, "Float" },
                    { 99, "Unknown" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicrologixPlcs_IpAddress",
                table: "MicrologixPlcs",
                column: "IpAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlcTags_PlcId",
                table: "PlcTags",
                column: "PlcId");

            migrationBuilder.CreateIndex(
                name: "IX_PlcTags_SymbolName_PlcId",
                table: "PlcTags",
                columns: new[] { "SymbolName", "PlcId" },
                unique: true,
                filter: "[SymbolName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlcTags_TagTypeId",
                table: "PlcTags",
                column: "TagTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTypes_TagType",
                table: "TagTypes",
                column: "TagType",
                unique: true,
                filter: "[TagType] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlcTags");

            migrationBuilder.DropTable(
                name: "MicrologixPlcs");

            migrationBuilder.DropTable(
                name: "TagTypes");
        }
    }
}
