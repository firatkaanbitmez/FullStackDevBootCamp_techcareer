using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAppProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customerler",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerAd = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerSoyad = table.Column<string>(type: "TEXT", nullable: true),
                    Eposta = table.Column<string>(type: "TEXT", nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customerler", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "KursKayitlari",
                columns: table => new
                {
                    KayitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    KursId = table.Column<int>(type: "INTEGER", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursKayitlari", x => x.KayitId);
                });

            migrationBuilder.CreateTable(
                name: "Kurslar",
                columns: table => new
                {
                    KursId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Baslik = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurslar", x => x.KursId);
                });

            migrationBuilder.CreateTable(
                name: "Productler",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductTitle = table.Column<string>(type: "TEXT", nullable: true),
                    ProductDesc = table.Column<string>(type: "TEXT", nullable: true),
                    ProductSerial = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductPrice = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productler", x => x.ProductId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customerler");

            migrationBuilder.DropTable(
                name: "KursKayitlari");

            migrationBuilder.DropTable(
                name: "Kurslar");

            migrationBuilder.DropTable(
                name: "Productler");
        }
    }
}
