using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.Migrations
{
    /// <inheritdoc />
    public partial class Add_Pass_To_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pass_num",
                table: "ticket",
                type: "char(4)",
                unicode: false,
                fixedLength: true,
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pass_series",
                table: "ticket",
                type: "char(6)",
                unicode: false,
                fixedLength: true,
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pass_num",
                table: "ticket");

            migrationBuilder.DropColumn(
                name: "pass_series",
                table: "ticket");
        }
    }
}
