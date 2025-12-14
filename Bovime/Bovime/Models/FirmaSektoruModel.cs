using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class FirmaSektoruModel : ModelTabani
    {
        public FirmaSektoru kartVerisi { get; set; }
        public List<FirmaSektoruAYRINTI> dokumVerisi { get; set; }
        public FirmaSektoruAYRINTIArama aramaParametresi { get; set; }


        public FirmaSektoruModel()
        {
            this.kartVerisi = new FirmaSektoru();
            this.dokumVerisi = new List<FirmaSektoruAYRINTI>();
            this.aramaParametresi = new FirmaSektoruAYRINTIArama();
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
                    FirmaSektoru? silinecek = await FirmaSektoru.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.FirmaSektoruModel modeli = new Models.FirmaSektoruModel();
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




        public async Task<FirmaSektoru> kaydetKos(Sayfa sayfasi)
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
                var kart = await FirmaSektoru.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<FirmaSektoruAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<FirmaAYRINTI> _ayFirmaAYRINTI { get; set; }
        public List<SektorAYRINTI> _aySektorAYRINTI { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _ayFirmaAYRINTI = await FirmaAYRINTI.ara(vari);
            _aySektorAYRINTI = await SektorAYRINTI.ara(vari);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                FirmaSektoruAYRINTIArama kosul = new FirmaSektoruAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new FirmaSektoru();
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
                    FirmaSektoruAYRINTIArama kosul = JsonConvert.DeserializeObject<FirmaSektoruAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new FirmaSektoruAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new FirmaSektoru();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
