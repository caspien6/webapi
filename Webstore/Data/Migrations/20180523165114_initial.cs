using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Webstore.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kategoria",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Alkategoria = table.Column<int>(nullable: true),
                    Nev = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Kategoria__Szulo__2645B050",
                        column: x => x.Alkategoria,
                        principalTable: "Kategoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statusz",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nev = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statusz", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vevo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IdentityId = table.Column<string>(nullable: true),
                    Jelszo = table.Column<string>(maxLength: 50, nullable: true),
                    Login = table.Column<string>(maxLength: 50, nullable: true),
                    Nev = table.Column<string>(maxLength: 50, nullable: true),
                    Szamlaszam = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vevo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vevo_AspNetUsers_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Termek",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ar = table.Column<double>(nullable: true),
                    KategoriaID = table.Column<int>(nullable: true),
                    KepUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Leiras = table.Column<string>(nullable: true),
                    Nev = table.Column<string>(maxLength: 50, nullable: true),
                    Raktarkeszlet = table.Column<int>(nullable: true),
                    Views = table.Column<int>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termek", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Termek__Kategori__2A164134",
                        column: x => x.KategoriaID,
                        principalTable: "Kategoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kosar",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatuszID = table.Column<int>(nullable: true),
                    TelephelyID = table.Column<int>(nullable: true),
                    VevoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kosar", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Kosar__Statu__339FAB6E",
                        column: x => x.StatuszID,
                        principalTable: "Statusz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kosar_Vevo",
                        column: x => x.VevoID,
                        principalTable: "Vevo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KosarTetel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ar = table.Column<double>(nullable: true),
                    KosarID = table.Column<int>(nullable: true),
                    Mennyiseg = table.Column<int>(nullable: true),
                    StatuszID = table.Column<int>(nullable: true),
                    TermekID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KosarTetel", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Kosar__KosarTetel__37703C52",
                        column: x => x.KosarID,
                        principalTable: "Kosar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Megrendel__Statu__395884C4",
                        column: x => x.StatuszID,
                        principalTable: "Statusz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Megrendel__Terme__3864608B",
                        column: x => x.TermekID,
                        principalTable: "Termek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Kategoria_Alkategoria",
                table: "Kategoria",
                column: "Alkategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Kosar_StatuszID",
                table: "Kosar",
                column: "StatuszID");

            migrationBuilder.CreateIndex(
                name: "IX_Kosar_VevoID",
                table: "Kosar",
                column: "VevoID");

            migrationBuilder.CreateIndex(
                name: "IX_KosarTetel_KosarID",
                table: "KosarTetel",
                column: "KosarID");

            migrationBuilder.CreateIndex(
                name: "IX_KosarTetel_StatuszID",
                table: "KosarTetel",
                column: "StatuszID");

            migrationBuilder.CreateIndex(
                name: "IX_KosarTetel_TermekID",
                table: "KosarTetel",
                column: "TermekID");

            migrationBuilder.CreateIndex(
                name: "IX_Termek_KategoriaID",
                table: "Termek",
                column: "KategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vevo_IdentityId",
                table: "Vevo",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "KosarTetel");

            migrationBuilder.DropTable(
                name: "Kosar");

            migrationBuilder.DropTable(
                name: "Termek");

            migrationBuilder.DropTable(
                name: "Statusz");

            migrationBuilder.DropTable(
                name: "Vevo");

            migrationBuilder.DropTable(
                name: "Kategoria");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
