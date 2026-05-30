using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaaSBillingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrganizationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_organization_organization_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organization",
                table: "organization");

            migrationBuilder.RenameTable(
                name: "organization",
                newName: "organizations");

            migrationBuilder.AddPrimaryKey(
                name: "pk_organizations",
                table: "organizations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_organizations_organization_id",
                table: "users",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_organizations_organization_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organizations",
                table: "organizations");

            migrationBuilder.RenameTable(
                name: "organizations",
                newName: "organization");

            migrationBuilder.AddPrimaryKey(
                name: "pk_organization",
                table: "organization",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_organization_organization_id",
                table: "users",
                column: "organization_id",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
