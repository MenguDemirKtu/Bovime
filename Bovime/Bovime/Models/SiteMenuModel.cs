using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class SiteMenuModel : ModelTabani
    {
        public SiteMenu kartVerisi { get; set; }
        public List<SiteMenuAYRINTI> dokumVerisi { get; set; }
        public SiteMenuAYRINTIArama aramaParametresi { get; set; }


        public List<SiteMenuAYRINTI> _aySiteMenuler { get; set; }
        public SiteMenuModel()
        {
            this.kartVerisi = new SiteMenu();
            this.dokumVerisi = new List<SiteMenuAYRINTI>();
            this.aramaParametresi = new SiteMenuAYRINTIArama();
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
                    SiteMenu? silinecek = await SiteMenu.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.SiteMenuModel modeli = new Models.SiteMenuModel();
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




        public async Task<SiteMenu> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);

                List<SiteMenu> menuler = vari.SiteMenus.Where(p => p.e_altMenuMu == false).OrderBy(p => p.sirasi).ToList();
                int sirasi = 10;
                foreach (var siradaki in menuler)
                {
                    siradaki.sirasi = sirasi;
                    await veriTabani.SiteMenuCizelgesi.kaydetKos(siradaki, vari, false);
                    sirasi += 10;
                }


                return kartVerisi;
            }
        }


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await SiteMenu.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<SiteMenuAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _aySiteMenuler = await SiteMenuAYRINTI.ara(p => p.e_altMenuMu == false);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                SiteMenuAYRINTIArama kosul = new SiteMenuAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new SiteMenu();
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
                    SiteMenuAYRINTIArama kosul = JsonConvert.DeserializeObject<SiteMenuAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new SiteMenuAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new SiteMenu();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
