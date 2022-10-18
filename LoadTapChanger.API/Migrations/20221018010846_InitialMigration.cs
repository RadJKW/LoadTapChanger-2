using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadTapChanger.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MicrologixPlc",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    Gateway = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    PlcType = table.Column<int>(type: "INTEGER", unicode: false, maxLength: 50, nullable: false),
                    Protocol = table.Column<int>(type: "INTEGER", unicode: false, maxLength: 50, nullable: false),
                    Timeout = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrologixPlc", x => new { x.Name, x.TagID });
                });

            migrationBuilder.CreateTable(
                name: "MicrologixTag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    PlcID = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    LookupName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    TagType = table.Column<int>(type: "INTEGER", unicode: false, maxLength: 50, nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrologixTag", x => new { x.ID, x.PlcID });
                });

            migrationBuilder.InsertData(
                table: "MicrologixPlc",
                columns: new[] { "TagID", "Name", "Gateway", "Path", "PlcType", "Protocol", "Timeout" },
                values: new object[] { 1, "Micrologix1100", "192.168.0.23", "1,0", 2, 0, new TimeSpan(0, 0, 0, 5, 0) });

            migrationBuilder.InsertData(
                table: "MicrologixTag",
                columns: new[] { "ID", "PlcID", "CustomName", "LookupName", "TagType", "Value" },
                values: new object[] { 1, 1, "Output-1", "O0:0/1", 0, 0 });

            migrationBuilder.InsertData(
                table: "MicrologixTag",
                columns: new[] { "ID", "PlcID", "CustomName", "LookupName", "TagType", "Value" },
                values: new object[] { 2, 1, "Input-1", "I1:0/1", 1, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MicrologixPlc");

            migrationBuilder.DropTable(
                name: "MicrologixTag");
        }
    }
}
