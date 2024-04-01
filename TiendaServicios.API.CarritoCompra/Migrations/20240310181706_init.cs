using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TiendaServices.API.CarritoCompra.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarritoSession",
                columns: table => new
                {
                    CarritoSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSession", x => x.CarritoSessionId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarritoSessionDetalle",
                columns: table => new
                {
                    CarritoSessionDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaOperacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProductoSelecioado = table.Column<string>(type: "longtext", nullable: true),
                    CarritoSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSessionDetalle", x => x.CarritoSessionDetalleId);
                    table.ForeignKey(
                        name: "FK_CarritoSessionDetalle_CarritoSession_CarritoSessionId",
                        column: x => x.CarritoSessionId,
                        principalTable: "CarritoSession",
                        principalColumn: "CarritoSessionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoSessionDetalle_CarritoSessionId",
                table: "CarritoSessionDetalle",
                column: "CarritoSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoSessionDetalle");

            migrationBuilder.DropTable(
                name: "CarritoSession");
        }
    }
}
