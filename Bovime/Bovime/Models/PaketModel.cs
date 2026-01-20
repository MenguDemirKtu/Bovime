using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class PaketModel : ModelTabani
    {
        public Paket kartVerisi { get; set; }
        public List<PaketAYRINTI> dokumVerisi { get; set; }
        public PaketAYRINTIArama aramaParametresi { get; set; }


        public PaketModel()
        {
            this.kartVerisi = new Paket();
            this.dokumVerisi = new List<PaketAYRINTI>();
            this.aramaParametresi = new PaketAYRINTIArama();
        }


        public async Task<AramaTalebi> ayrintiliAraKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                AramaTalebi talep = new AramaTalebi();
                talep.kodu = Guid.NewGuid().ToString();
                talep.tarih = DateTime.Now;
                talep.varmi = true;
                talep.talepAyrintisi = Newtonsoft.Json.JsonConvert.SerializeObject(aramaParametresi);
                await veriTabani.AramaTalebiCizelgesi.kaydetKos(talep, vari, false);
                return talep;
            }
        }
        private async Task ekkosulEkle(veri.Varlik vari, Yonetici kime, PaketAYRINTIArama kosul)
        {
        }
        public async Task silKos(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    Paket? silinecek = await Paket.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.PaketModel modeli = new Models.PaketModel();
            await modeli.veriCekKos(silen);
        }
        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }




        public async Task<Paket> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                long fk = 0;
                if (long.TryParse(fotoKonumu, out fk))
                    kartVerisi.i_fotoKimlik = fk;
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);
                await fotoBicimlendirKos(vari, kartVerisi, kartVerisi.i_fotoKimlik);
                return kartVerisi;
            }
        }


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await Paket.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<PaketAYRINTI>();
                await baglilariCek(vari, kime);
                await fotoAyariBelirle(vari, kartVerisi._cizelgeAdi());
            }
        }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                PaketAYRINTIArama kosul = new PaketAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Paket();
                await ekkosulEkle(vari, kime, kosul);
                dokumVerisi = await kosul.cek(vari);
                await baglilariCek(vari, kime);
            }
        }
        public async Task kosulaGoreCek(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                var talep = vari.AramaTalebis.FirstOrDefault(p => p.kodu == id);
                if (talep != null)
                {
                    PaketAYRINTIArama kosul = JsonConvert.DeserializeObject<PaketAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new PaketAYRINTIArama();
                    await ekkosulEkle(vari, kime, kosul);
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Paket();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
