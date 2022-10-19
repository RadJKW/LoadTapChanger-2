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
                    DefaultName = table.Column<int>(type: "int", nullable: true, computedColumnSql: "('PLC-'+[PlcId])", stored: false),
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
                name: "MicrologixTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ConfiguredName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true),
                    TagType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlcDeviceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrologixTags", x => x.TagId);
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
                table: "MicrologixTags",
                column: "PlcDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_MicrologixTags_TagId_CustomName_ConfiguredName",
                table: "MicrologixTags",
                columns: new[] { "TagId", "CustomName", "ConfiguredName" },
                unique: true,
                filter: "([CustomName] IS NOT NULL AND [ConfiguredName] IS NOT NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MicrologixTags");

            migrationBuilder.DropTable(
                name: "MicrologixPlcs");
        }
    }
}
