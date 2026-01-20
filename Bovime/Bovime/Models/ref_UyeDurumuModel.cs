using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class ref_UyeDurumuModel : ModelTabani
    {
        public ref_UyeDurumu kartVerisi { get; set; }
        public List<ref_UyeDurumu> dokumVerisi { get; set; }
        public ref_UyeDurumuArama aramaParametresi { get; set; }


        public ref_UyeDurumuModel()
        {
            this.kartVerisi = new ref_UyeDurumu();
            this.dokumVerisi = new List<ref_UyeDurumu>();
            this.aramaParametresi = new ref_UyeDurumuArama();
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
        private async Task ekkosulEkle(veri.Varlik vari, Yonetici kime, ref_UyeDurumuArama kosul)
        {
        }
        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await ref_UyeDurumu.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<ref_UyeDurumu>();
                await baglilariCek(vari, kime);
            }
        }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                ref_UyeDurumuArama kosul = new ref_UyeDurumuArama();
                kartVerisi = new ref_UyeDurumu();
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
                    ref_UyeDurumuArama kosul = JsonConvert.DeserializeObject<ref_UyeDurumuArama>(talep.talepAyrintisi ?? "") ?? new ref_UyeDurumuArama();
                    await ekkosulEkle(vari, kime, kosul);
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new ref_UyeDurumu();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
