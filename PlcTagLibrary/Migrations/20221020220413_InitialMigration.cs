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
                    DefaultName = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "('PLC' + '-' + CAST([PlcId] as varchar(10)))", stored: false),
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

            migrationBuilder.InsertData(
                table: "MicrologixPlcs",
                columns: new[] { "PlcId", "Gateway", "Name", "PlcType", "Protocol", "TimeoutSeconds" },
                values: new object[] { 1, "192.168.0.23", "Micrologix1100", "Slc500", "ab_eip", (short)3 });

            migrationBuilder.InsertData(
                table: "MicrologixPlcs",
                columns: new[] { "PlcId", "Gateway", "Name", "PlcType", "Protocol", "TimeoutSeconds" },
                values: new object[] { 2, "192.168.0.200", "Micrologix1200", "ControlLogix", "ab_eip", (short)5 });

            migrationBuilder.InsertData(
                table: "PlcTags",
                columns: new[] { "TagId", "CustomName", "PlcDeviceId", "RslinxTagName", "TagType", "Value" },
                values: new object[,]
                {
                    { 1, "Output:1", 1, "O0:0/1", 0, null },
                    { 2, "Input:1", 1, "I1:0/1", 1, null },
                    { 3, "Output:1", 2, "O0:0/1", 0, null },
                    { 4, "Input:1", 2, "I1:0/1", 1, null }
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
