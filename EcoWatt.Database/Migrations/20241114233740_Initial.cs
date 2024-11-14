using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoWatt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ecowatt_usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_usuario = table.Column<string>(type: "varchar(150)", nullable: false),
                    ds_senha = table.Column<string>(type: "varchar(225)", nullable: false),
                    ds_nome_completo = table.Column<string>(type: "varchar(250)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ecowatt_usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "ecowatt_baterias",
                columns: table => new
                {
                    id_bateria = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false),
                    ds_tipo_bateria = table.Column<string>(type: "varchar(50)", nullable: false),
                    ds_nome_conjunto = table.Column<string>(type: "varchar(100)", nullable: false),
                    vl_capacidade = table.Column<long>(type: "number(11)", nullable: false),
                    vl_quantidade = table.Column<long>(type: "number(11)", nullable: false),
                    ds_status = table.Column<string>(type: "varchar(50)", nullable: false),
                    dt_timestamp_ultima_leitura = table.Column<DateTime>(type: "date", nullable: true),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ecowatt_baterias", x => x.id_bateria);
                    table.ForeignKey(
                        name: "FK_ecowatt_baterias_ecowatt_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "ecowatt_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ecowatt_usuario_use",
                columns: table => new
                {
                    id_use = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false),
                    id_bateria = table.Column<long>(type: "number(11)", nullable: false),
                    used_at = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ecowatt_usuario_use", x => x.id_use);
                    table.ForeignKey(
                        name: "FK_ecowatt_usuario_use_ecowatt_baterias_id_bateria",
                        column: x => x.id_bateria,
                        principalTable: "ecowatt_baterias",
                        principalColumn: "id_bateria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ecowatt_usuario_use_ecowatt_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "ecowatt_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ecowatt_baterias_id_usuario",
                table: "ecowatt_baterias",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_ecowatt_usuario_use_id_bateria",
                table: "ecowatt_usuario_use",
                column: "id_bateria");

            migrationBuilder.CreateIndex(
                name: "IX_ecowatt_usuario_use_id_usuario",
                table: "ecowatt_usuario_use",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ecowatt_usuario_use");

            migrationBuilder.DropTable(
                name: "ecowatt_baterias");

            migrationBuilder.DropTable(
                name: "ecowatt_usuario");
        }
    }
}
