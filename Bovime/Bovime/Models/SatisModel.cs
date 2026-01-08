using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class SatisModel : ModelTabani
    {
        public Satis kartVerisi { get; set; }
        public List<SatisAYRINTI> dokumVerisi { get; set; }
        public SatisAYRINTIArama aramaParametresi { get; set; }


        public SatisModel()
        {
            this.kartVerisi = new Satis();
            this.dokumVerisi = new List<SatisAYRINTI>();
            this.aramaParametresi = new SatisAYRINTIArama();
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
                    Satis? silinecek = await Satis.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.SatisModel modeli = new Models.SatisModel();
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




        public async Task<Satis> kaydetKos(Sayfa sayfasi)
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
                var kart = await Satis.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<SatisAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<FirmaAYRINTI> _ayFirmaAYRINTI { get; set; }
        public List<UyeAYRINTI> _ayUyeAYRINTI { get; set; }
        public List<ref_SatisDurumu> _ayref_SatisDurumu { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _ayFirmaAYRINTI = await FirmaAYRINTI.ara(vari);
            _ayUyeAYRINTI = await UyeAYRINTI.ara(vari);
            _ayref_SatisDurumu = await ref_SatisDurumu.ara(vari);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                SatisAYRINTIArama kosul = new SatisAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Satis();
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
                    SatisAYRINTIArama kosul = JsonConvert.DeserializeObject<SatisAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new SatisAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Satis();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
