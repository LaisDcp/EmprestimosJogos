using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmprestimosJogos.Infra.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Key = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    NormalizedRoleName = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TokenType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Key = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PerfilId = table.Column<Guid>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    NormalizedEmail = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Amigo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    CEP = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Numero = table.Column<int>(maxLength: 9, nullable: true),
                    TelefoneCelular = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amigo_Usuario_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    TokenTypeId = table.Column<Guid>(nullable: false),
                    AutenticacaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_Usuario_AutenticacaoId",
                        column: x => x.AutenticacaoId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Token_TokenType_TokenTypeId",
                        column: x => x.TokenTypeId,
                        principalTable: "TokenType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataEmprestimo = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsEmprestado = table.Column<bool>(nullable: false, defaultValue: false),
                    AmigoId = table.Column<Guid>(nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogo_Amigo_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "Amigo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jogo_Usuario_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Perfil",
                columns: new[] { "Id", "CreatedDate", "Descricao", "Key", "ModifiedDate", "Nome", "NormalizedRoleName" },
                values: new object[] { new Guid("8907d860-ceb1-4345-b798-8757200e90c9"), new DateTime(2021, 1, 9, 19, 18, 11, 589, DateTimeKind.Local).AddTicks(7909), "Perfil para usuários administradores do sistema.", "ADM", null, "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "TokenType",
                columns: new[] { "Id", "CreatedDate", "Key", "ModifiedDate", "Value" },
                values: new object[] { new Guid("652f2144-7b66-4ac4-967f-e0bae568ebb1"), new DateTime(2021, 1, 9, 19, 18, 11, 588, DateTimeKind.Local).AddTicks(5821), "RESE", null, "Token para reset de senha (recuperação de senha)." });

            migrationBuilder.CreateIndex(
                name: "IX_Amigo_CreatorId",
                table: "Amigo",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_AmigoId",
                table: "Jogo",
                column: "AmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_CreatorId",
                table: "Jogo",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_AutenticacaoId",
                table: "Token",
                column: "AutenticacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_TokenTypeId",
                table: "Token",
                column: "TokenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Id",
                table: "Usuario",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PerfilId",
                table: "Usuario",
                column: "PerfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "Amigo");

            migrationBuilder.DropTable(
                name: "TokenType");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Perfil");
        }
    }
}
