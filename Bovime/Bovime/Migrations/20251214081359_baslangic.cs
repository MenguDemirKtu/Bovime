using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bovime.Migrations
{
    /// <inheritdoc />
    public partial class baslangic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnaSayfaManset");

            migrationBuilder.DropTable(
                name: "AnaSayfaMansetAYRINTI");

            migrationBuilder.DropTable(
                name: "Galeri");

            migrationBuilder.DropTable(
                name: "GaleriAYRINTI");

            migrationBuilder.DropTable(
                name: "GaleriFotosu");

            migrationBuilder.DropTable(
                name: "GaleriFotosuAYRINTI");

            migrationBuilder.DropTable(
                name: "Sektor");

            migrationBuilder.DropTable(
                name: "SektorAYRINTI");

            migrationBuilder.DropTable(
                name: "Simge");

            migrationBuilder.DropTable(
                name: "SimgeAYRINTI");

            migrationBuilder.DropTable(
                name: "SiteDuyuru");

            migrationBuilder.DropTable(
                name: "SiteDuyuruAYRINTI");

            migrationBuilder.DropTable(
                name: "SosyalMedya");

            migrationBuilder.DropTable(
                name: "SosyalMedyaAYRINTI");

            migrationBuilder.DropColumn(
                name: "sehirAdi",
                table: "SehirAYRINTI");

            migrationBuilder.DropColumn(
                name: "sehirAdi",
                table: "Sehir");

            migrationBuilder.DropColumn(
                name: "dilAdi",
                table: "EPostaKalibiAYRINTI");

            migrationBuilder.DropColumn(
                name: "i_dilKimlik",
                table: "EPostaKalibiAYRINTI");

            migrationBuilder.AlterColumn<string>(
                name: "surumNumarasi",
                table: "YazilimAyariAYRINTI",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "surumNumarasi",
                table: "YazilimAyari",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "telefonKodu",
                table: "SehirAYRINTI",
                type: "nvarchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "plakaKodu",
                table: "SehirAYRINTI",
                type: "nvarchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adi",
                table: "SehirAYRINTI",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "i_sehirTuruKimlik",
                table: "SehirAYRINTI",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "i_ulkeKimlik",
                table: "SehirAYRINTI",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "telefonKodu",
                table: "Sehir",
                type: "nvarchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "plakaKodu",
                table: "Sehir",
                type: "nvarchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adi",
                table: "Sehir",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "i_sehirTuruKimlik",
                table: "Sehir",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "i_ulkeKimlik",
                table: "Sehir",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "eklemeIzniVarmi",
                table: "RolWebSayfasiIzniAYRINTI",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AlterColumn<bool>(
                name: "e_silmeIzniVarmi",
                table: "RolWebSayfasiIzniAYRINTI",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "e_guncellemeIzniVarmi",
                table: "RolWebSayfasiIzniAYRINTI",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "e_gormeIzniVarmi",
                table: "RolWebSayfasiIzniAYRINTI",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "e_eklemeIzniVarmi",
                table: "RolWebSayfasiIzniAYRINTI",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "e_genislik4Varmi",
                table: "ResimAyari",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "e_genislik3Varmi",
                table: "ResimAyari",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "e_genislik2Varmi",
                table: "ResimAyari",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "e_anaKullaniciGorsunmu",
                table: "BysMenuAYRINTI",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "hataAYRINTI",
                columns: table => new
                {
                    hataKimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ifadesi = table.Column<string>(type: "text", nullable: true),
                    tarih = table.Column<DateTime>(type: "datetime", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hataAYRINTI", x => x.hataKimlik);
                });

            migrationBuilder.CreateTable(
                name: "HizliKazananEkleme",
                columns: table => new
                {
                    hizliKazananEklemekimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    birimAdi = table.Column<string>(type: "nvarchar(400)", nullable: true),
                    e_gecerliMi = table.Column<bool>(type: "bit", nullable: true),
                    ogrenciNumaralari = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizliKazananEkleme", x => x.hizliKazananEklemekimlik);
                });

            migrationBuilder.CreateTable(
                name: "HizliKazananEklemeAYRINTI",
                columns: table => new
                {
                    hizliKazananEklemekimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    birimAdi = table.Column<string>(type: "nvarchar(400)", nullable: true),
                    e_gecerliMi = table.Column<bool>(type: "bit", nullable: true),
                    ogrenciNumaralari = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizliKazananEklemeAYRINTI", x => x.hizliKazananEklemekimlik);
                });

            migrationBuilder.CreateTable(
                name: "HizliKullanici",
                columns: table => new
                {
                    hizliKullaniciKimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    i_kullaniciTuruKimlik = table.Column<int>(type: "int", nullable: true),
                    tcKimlikNo = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizliKullanici", x => x.hizliKullaniciKimlik);
                });

            migrationBuilder.CreateTable(
                name: "HizliKullaniciAYRINTI",
                columns: table => new
                {
                    hizliKullaniciKimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    i_kullaniciTuruKimlik = table.Column<int>(type: "int", nullable: true),
                    kullaniciTuru = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    tcKimlikNo = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizliKullaniciAYRINTI", x => x.hizliKullaniciKimlik);
                });

            migrationBuilder.CreateTable(
                name: "SayfaKilavuzu",
                columns: table => new
                {
                    sayfaKilavuzuKimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_gecerlimi = table.Column<bool>(type: "bit", nullable: true),
                    i_tanitimVideosuKimlik = table.Column<int>(type: "int", nullable: false),
                    i_webSayfasiKimlik = table.Column<int>(type: "int", nullable: false),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SayfaKilavuzu", x => x.sayfaKilavuzuKimlik);
                });

            migrationBuilder.CreateTable(
                name: "SayfaKilavuzuAYRINTI",
                columns: table => new
                {
                    sayfaKilavuzuKimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_gecerlimi = table.Column<bool>(type: "bit", nullable: true),
                    gecerlimi = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    hamAdresi = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    i_tanitimVideosuKimlik = table.Column<int>(type: "int", nullable: false),
                    i_webSayfasiKimlik = table.Column<int>(type: "int", nullable: false),
                    sayfaBasligi = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true),
                    videoBasligi = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    youtubeAdresi = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SayfaKilavuzuAYRINTI", x => x.sayfaKilavuzuKimlik);
                });

            migrationBuilder.CreateTable(
                name: "SayfaUrl",
                columns: table => new
                {
                    sayfaUrlKimlik = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adi = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    e_kullaniliyormu = table.Column<bool>(type: "bit", nullable: true),
                    ilgiliCizelgeAdi = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ilgiliCizelgeKimlik = table.Column<long>(type: "bigint", nullable: true),
                    olusturmaTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    urlAdres = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SayfaUrl", x => x.sayfaUrlKimlik);
                });

            migrationBuilder.CreateTable(
                name: "SayfaUrlAYRINTI",
                columns: table => new
                {
                    sayfaUrlKimlik = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adi = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    e_kullaniliyormu = table.Column<bool>(type: "bit", nullable: true),
                    ilgiliCizelgeAdi = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ilgiliCizelgeKimlik = table.Column<long>(type: "bigint", nullable: true),
                    olusturmaTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    urlAdres = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SayfaUrlAYRINTI", x => x.sayfaUrlKimlik);
                });

            migrationBuilder.CreateTable(
                name: "TanitimVideosu",
                columns: table => new
                {
                    tanitimVideosuKimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    baslik = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    i_dosyaKimlik = table.Column<long>(type: "bigint", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true),
                    youtubeAdresi = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TanitimVideosu", x => x.tanitimVideosuKimlik);
                });

            migrationBuilder.CreateTable(
                name: "TanitimVideosuAYRINTI",
                columns: table => new
                {
                    tanitimVideosuKimlik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    baslik = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    dosyasi = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    i_dosyaKimlik = table.Column<long>(type: "bigint", nullable: true),
                    varmi = table.Column<bool>(type: "bit", nullable: true),
                    youtubeAdresi = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TanitimVideosuAYRINTI", x => x.tanitimVideosuKimlik);
                });
        }
    }
}
