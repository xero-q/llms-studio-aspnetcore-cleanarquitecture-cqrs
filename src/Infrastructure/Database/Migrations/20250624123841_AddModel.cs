using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AddModel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "models",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                identifier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                temperature = table.Column<double>(type: "double precision", nullable: false),
                environment_variable = table.Column<string>(type: "text", nullable: false),
                model_type_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_models", x => x.id);
                table.ForeignKey(
                    name: "fk_models_model_types_model_type_id",
                    column: x => x.model_type_id,
                    principalSchema: "public",
                    principalTable: "model_types",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_models_model_type_id",
            schema: "public",
            table: "models",
            column: "model_type_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "models",
            schema: "public");
    }
}
