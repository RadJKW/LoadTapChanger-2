using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlcTagLibrary.Migrations
{
    public partial class SeedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MicrologixPlcs",
                columns: new[] { "PlcId", "Gateway", "Name", "PlcType", "Protocol", "TimeoutSeconds" },
                values: new object[] { 1, "192.168.0.23", "Micrologix1100", "Slc500", "ab_eip", (short)3 });

            migrationBuilder.InsertData(
                table: "PlcTags",
                columns: new[] { "TagId", "CustomName", "PlcDeviceId", "RslinxTagName", "TagType", "Value" },
                values: new object[] { 1, "Output:1", 1, "O0:0/1", 0, null });

            migrationBuilder.InsertData(
                table: "PlcTags",
                columns: new[] { "TagId", "CustomName", "PlcDeviceId", "RslinxTagName", "TagType", "Value" },
                values: new object[] { 2, "Input:1", 1, "I1:0/1", 1, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlcTags",
                keyColumn: "TagId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlcTags",
                keyColumn: "TagId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MicrologixPlcs",
                keyColumn: "PlcId",
                keyValue: 1);
        }
    }
}
