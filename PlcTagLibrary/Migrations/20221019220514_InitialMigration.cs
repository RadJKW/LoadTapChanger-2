using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlcTagLibrary.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MicrologixPlcs",
                columns: table => new
                {
                    PlcId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DefaultName = table.Column<string>(type: "nvarchar(64)", nullable: true, computedColumnSql: "('PLC' + '-' + CAST([PlcId] as varchar(10)))", stored: false),
                    Gateway = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    TimeoutSeconds = table.Column<short>(type: "smallint", nullable: false),
                    PlcType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Protocol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrologixPlcs", x => x.PlcId);
                });

            migrationBuilder.CreateTable(
                name: "PlcTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RslinxTagName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true),
                    TagType = table.Column<int>(type: "int", nullable: false),
                    PlcDeviceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlcTags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_PlcTag_Plc",
                        column: x => x.PlcDeviceId,
                        principalTable: "MicrologixPlcs",
                        principalColumn: "PlcId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicrologixPlcs_PlcId_Name",
                table: "MicrologixPlcs",
                columns: new[] { "PlcId", "Name" },
                unique: true,
                filter: "([Name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_MicrologixTags_PlcDeviceId",
                table: "PlcTags",
                column: "PlcDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_MicrologixTags_TagId_CustomName_ConfiguredName",
                table: "PlcTags",
                columns: new[] { "TagId", "CustomName", "RslinxTagName" },
                unique: true,
                filter: "([CustomName] IS NOT NULL AND [RslinxTagName] IS NOT NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlcTags");

            migrationBuilder.DropTable(
                name: "MicrologixPlcs");
        }
    }
}
