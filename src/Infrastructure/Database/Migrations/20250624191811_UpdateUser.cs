using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class UpdateUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "email",
            schema: "public",
            table: "users",
            newName: "username");

        migrationBuilder.RenameIndex(
            name: "ix_users_email",
            schema: "public",
            table: "users",
            newName: "ix_users_username");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "username",
            schema: "public",
            table: "users",
            newName: "email");

        migrationBuilder.RenameIndex(
            name: "ix_users_username",
            schema: "public",
            table: "users",
            newName: "ix_users_email");
    }
}
