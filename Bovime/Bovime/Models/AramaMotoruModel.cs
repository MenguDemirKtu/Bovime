using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class AramaMotoruModel : ModelTabani
    {
        public AramaMotoru kartVerisi { get; set; }
        public List<AramaMotoruAYRINTI> dokumVerisi { get; set; }
        public AramaMotoruAYRINTIArama aramaParametresi { get; set; }


        public AramaMotoruModel()
        {
            this.kartVerisi = new AramaMotoru();
            this.dokumVerisi = new List<AramaMotoruAYRINTI>();
            this.aramaParametresi = new AramaMotoruAYRINTIArama();
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
        public async Task silKos(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    AramaMotoru? silinecek = await AramaMotoru.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.AramaMotoruModel modeli = new Models.AramaMotoruModel();
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




        public async Task<AramaMotoru> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);
                return kartVerisi;
            }
        }


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await AramaMotoru.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<AramaMotoruAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                AramaMotoruAYRINTIArama kosul = new AramaMotoruAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new AramaMotoru();
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
                    AramaMotoruAYRINTIArama kosul = JsonConvert.DeserializeObject<AramaMotoruAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new AramaMotoruAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new AramaMotoru();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
