using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class BovimisayfasiModel : ModelTabani
    {
        public Bovimisayfasi kartVerisi { get; set; }
        public List<BovimisayfasiAYRINTI> dokumVerisi { get; set; }
        public BovimisayfasiAYRINTIArama aramaParametresi { get; set; }


        public BovimisayfasiModel()
        {
            this.kartVerisi = new Bovimisayfasi();
            this.dokumVerisi = new List<BovimisayfasiAYRINTI>();
            this.aramaParametresi = new BovimisayfasiAYRINTIArama();
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
        private async Task ekkosulEkle(veri.Varlik vari, Yonetici kime, BovimisayfasiAYRINTIArama kosul)
        {
        }
        public async Task silKos(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    Bovimisayfasi? silinecek = await Bovimisayfasi.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.BovimisayfasiModel modeli = new Models.BovimisayfasiModel();
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




        public async Task<Bovimisayfasi> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi.aciklama = Sayfa.dosyaKonumDuzelt(kartVerisi.aciklama, Genel.yazilimAyari);
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
                var kart = await Bovimisayfasi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<BovimisayfasiAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                BovimisayfasiAYRINTIArama kosul = new BovimisayfasiAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Bovimisayfasi();
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
                    BovimisayfasiAYRINTIArama kosul = JsonConvert.DeserializeObject<BovimisayfasiAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new BovimisayfasiAYRINTIArama();
                    await ekkosulEkle(vari, kime, kosul);
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Bovimisayfasi();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
