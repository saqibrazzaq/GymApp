using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class curr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessLicense",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyEmail",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyWebsite",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookGroup",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookPage",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUsername",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterHandle",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CurrencyId",
                table: "Account",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_StateId",
                table: "Account",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Code",
                table: "Currency",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Name",
                table: "Currency",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Currency_CurrencyId",
                table: "Account",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_State_StateId",
                table: "Account",
                column: "StateId",
                principalTable: "State",
                principalColumn: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Currency_CurrencyId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_State_StateId",
                table: "Account");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Account_CurrencyId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_StateId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "BusinessLicense",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "CompanyEmail",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "CompanyWebsite",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "FacebookGroup",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "FacebookPage",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "InstagramUsername",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "TwitterHandle",
                table: "Account");
        }
    }
}
