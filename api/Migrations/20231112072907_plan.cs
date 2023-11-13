using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanCategoryId = table.Column<int>(type: "int", nullable: true),
                    PlanTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_Plan_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_PlanCategory_PlanCategoryId",
                        column: x => x.PlanCategoryId,
                        principalTable: "PlanCategory",
                        principalColumn: "PlanCategoryId");
                    table.ForeignKey(
                        name: "FK_Plan_PlanType_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalTable: "PlanType",
                        principalColumn: "PlanTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plan_AccountId",
                table: "Plan",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_PlanCategoryId",
                table: "Plan",
                column: "PlanCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_PlanTypeId",
                table: "Plan",
                column: "PlanTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plan");
        }
    }
}
