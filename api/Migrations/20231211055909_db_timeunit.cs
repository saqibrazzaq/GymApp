using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class db_timeunit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Plan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeUnitId",
                table: "Plan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeUnit",
                columns: table => new
                {
                    TimeUnitId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeUnit", x => x.TimeUnitId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plan_TimeUnitId",
                table: "Plan",
                column: "TimeUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_TimeUnit_TimeUnitId",
                table: "Plan",
                column: "TimeUnitId",
                principalTable: "TimeUnit",
                principalColumn: "TimeUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_TimeUnit_TimeUnitId",
                table: "Plan");

            migrationBuilder.DropTable(
                name: "TimeUnit");

            migrationBuilder.DropIndex(
                name: "IX_Plan_TimeUnitId",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "TimeUnitId",
                table: "Plan");
        }
    }
}
