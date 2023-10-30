using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopNews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNetworkAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NetworkAddresses",
                columns: new[] { "Id", "IpAddress" },
                values: new object[] { 2, "::1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NetworkAddresses",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
