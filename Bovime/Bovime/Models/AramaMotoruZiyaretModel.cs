using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class AramaMotoruZiyaretModel : ModelTabani
    {
        public AramaMotoruZiyaret kartVerisi { get; set; }
        public List<AramaMotoruZiyaretAYRINTI> dokumVerisi { get; set; }
        public AramaMotoruZiyaretAYRINTIArama aramaParametresi { get; set; }


        public AramaMotoruZiyaretModel()
        {
            this.kartVerisi = new AramaMotoruZiyaret();
            this.dokumVerisi = new List<AramaMotoruZiyaretAYRINTI>();
            this.aramaParametresi = new AramaMotoruZiyaretAYRINTIArama();
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
                    AramaMotoruZiyaret? silinecek = await AramaMotoruZiyaret.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.AramaMotoruZiyaretModel modeli = new Models.AramaMotoruZiyaretModel();
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




        public async Task<AramaMotoruZiyaret> kaydetKos(Sayfa sayfasi)
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
                var kart = await AramaMotoruZiyaret.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<AramaMotoruZiyaretAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<AramaMotoruAYRINTI> _ayAramaMotoruAYRINTI { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { _ayAramaMotoruAYRINTI = await AramaMotoruAYRINTI.ara(vari); }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                AramaMotoruZiyaretAYRINTIArama kosul = new AramaMotoruZiyaretAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new AramaMotoruZiyaret();
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
                    AramaMotoruZiyaretAYRINTIArama kosul = JsonConvert.DeserializeObject<AramaMotoruZiyaretAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new AramaMotoruZiyaretAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new AramaMotoruZiyaret();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
